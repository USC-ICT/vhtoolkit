using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Project specific demo controller class, based on DemoControllerBase.
    /// </summary>
    public class DemoController : DemoControllerBase
    {
        [Header("Cameras")]
        [SerializeField] private Camera m_camera;
        [SerializeField] private GameObject m_xrOrigin;

        [Header("UI")]
        [SerializeField] private DemoController_UI m_uiController;

        protected override IDemoControllerUI BindUI() => m_uiController;

        private HashSet<String> m_hasVHIntroducedThemselvesYet;
        private int m_numberOfIntroductions = 0;

        // An introduction is driven by an instruction ("introduce yourself using your profile"),
        // which providers record as the user's turn and replay on every later turn. Left in place
        // the model treats it as a standing request and introduces itself again on the next reply,
        // and will explain the instruction if asked about it. Once the greeting is in, the recorded
        // input is rewritten to something a person could have said.
        private const string IntroductionRecordedAsUserTurn = "Hello.";
        private bool m_introductionPending;

        // Each introduction is asked for a different angle, cycled by index. Telling a model to
        // "vary the wording" cannot work here: every character has its own conversation history,
        // so the model has never seen the previous introductions and has nothing to vary from.
        // Current models are also far more consistent than earlier ones, so given one instruction
        // they settle on the single most likely phrasing and every character says it. Changing
        // what is asked, rather than hoping for different wording of the same request, is what
        // keeps consecutive characters distinct - and it holds at any temperature.
        private static readonly string[] s_introductionAngles =
        {
            "Greet the user, give your name, and say what you do. ",
            "Greet the user, give your name, and say what you'd like to talk about. ",
            "Greet the user, give your name, and ask them one thing about themselves. ",
            "Greet the user, give your name, and mention where you are from. ",
            "Greet the user in your own manner without a question, then give your name. ",
            "Greet the user, give your name, and offer to talk about something you know well. ",
            "Greet the user, give your name, and say how you are right now. ",
            "Open with a short remark of your own, then give your name. ",
        };
        private RideCatalogAsset m_pendingCharacterLoadAsset;
        private string m_pendingCharacterName;
        private int m_lastPendingLoadPercent = -10;
        private string m_lastPendingLoadStatus = string.Empty;

        public List<MecanimCharacter> m_selectableCharacters;
        private int m_currentCharacterIndex = 0;


        public bool CharacterLoadPending => m_pendingCharacterLoadAsset != null;
        public string PendingCharacterName => m_pendingCharacterName;
        public string PendingCharacterStatus => m_lastPendingLoadStatus;
        public float PendingCharacterProgress => m_pendingCharacterLoadAsset != null ? m_pendingCharacterLoadAsset.CurrentLoadProgress : 0f;


        protected override void Start()
        {
            m_hasVHIntroducedThemselvesYet = new HashSet<string>();

            base.Start();

            OnCharacterResponse += RewriteIntroductionHistoryEntry;
        }

        private void OnDestroy()
        {
            OnCharacterResponse -= RewriteIntroductionHistoryEntry;
        }

        /// <summary>
        /// Once an introduction has been spoken, replaces the instruction that produced it with a
        /// plain greeting in the conversation history, so the character does not read the
        /// instruction as a standing request on later turns.
        /// </summary>
        /// <param name="response">The character's response text (unused; only the timing matters).</param>
        private void RewriteIntroductionHistoryEntry(string response)
        {
            if (!m_introductionPending)
                return;

            m_introductionPending = false;
            if (m_currentLLM == null)
                return;

            // Rewrite through the provider's public history API. Newest first, skipping index 0,
            // where providers such as ChatGPT keep the system prompt; the entry to fix is the
            // newest completed turn.
            var history = m_currentLLM.GetHistory();
            for (int i = history.Count - 1; i >= 1; i--)
            {
                if (string.IsNullOrEmpty(history[i].response))
                    continue;

                var turn = history[i];
                turn.input = IntroductionRecordedAsUserTurn;
                history[i] = turn;
                m_currentLLM.SetHistory(history);
                return;
            }
        }

        protected override void AfterSystemsInitialized()
        {
#if UNITY_STANDALONE_WIN
            if (m_camera) m_camera.gameObject.SetActive(true);
            if (m_xrOrigin) m_xrOrigin.SetActive(false);
#elif ENABLE_XR
            if (m_camera) m_camera.gameObject.SetActive(false);
            if (m_xrOrigin) m_xrOrigin.SetActive(true);
#endif

            StartCoroutine(LoadCachedCatalogsLogged());
        }

        // Subclasses can override to be notified when LoadCachedCatalogs finishes.
        protected virtual void OnCatalogsLoaded() { }

        private IEnumerator LoadCachedCatalogsLogged()
        {
            var bundleLoader = Systems.Get<AssetLoadingSystemAssetBundles>();
            if (bundleLoader == null)
                yield break;

            float start = Time.realtimeSinceStartup;
            float lastLog = 0f;

            var e = bundleLoader.LoadCachedCatalogs();
            while (e.MoveNext())
            {
                float now = Time.realtimeSinceStartup;
                if ((now - lastLog) >= 3f)
                {
                    Debug.Log($"[DemoController] LoadCachedCatalogs HEARTBEAT elapsed={(now - start):0.000}s");
                    lastLog = now;
                }

                yield return e.Current;
            }

            Debug.Log($"[DemoController] LoadCachedCatalogs COMPLETE elapsed=... catalogsLoaded={bundleLoader.NumCatalogsLoaded} catalogCurrentlyLoading={bundleLoader.CatalogCurrentlyLoading}");
            OnCatalogsLoaded();
        }

        protected override void CollectCharacters()
        {
            m_characters.Clear();
            if (m_charactersParent == null)
            {
                Debug.LogWarning("DemoController: m_charactersParent is not assigned.");
                return;
            }
            foreach (Transform category in m_charactersParent)
                foreach (Transform child in category)
                    if (child.TryGetComponent(out MecanimCharacter mc))
                        m_characters.Add(mc);
        }

        /// <summary>
        /// Selects and activates the character by name. Loads character asset if needed.
        /// </summary>
        /// <param name="characterName">The name of the character to activate.</param>
        public override void SelectCharacter(string characterName)
        {
            // Find selected character
            MecanimCharacter selected = m_characters.FirstOrDefault(c => c != null && c.name == characterName);
            if (selected == null)
            {
                Debug.LogError($"SelectCharacterInternal: character not found: {characterName}");
                return;
            }

            // If switching to a different character, unload the old one BEFORE disabling it.
            if (m_currentCharacter != null && m_currentCharacter != selected)
            {
                SaveCharacterNlpHistory(m_currentCharacter);
                UnloadCharacter(m_currentCharacter);
            }

            // Disable all non-selected characters.
            foreach (var character in m_characters)
            {
                if (character != null && character != selected)
                    character.gameObject.SetActive(false);
            }

            // Ensure the selected character is active.
            selected.gameObject.SetActive(true);

            SetCharacterConfigUIEnabled(false);
            if (m_currentASR != null) m_currentASR.StopRecognizing();
            SetASR(false);

            // Two-pass behavior:
            // 1) If loadable + not initialized, start the load and exit.
            if (selected.TryGetComponent<RideCatalogAsset>(out var loadable) && !loadable.AssetInitialized)
            {
                BeginPendingCharacterLoad(selected, loadable);
                loadable.LoadAsset();  // When loaded, the existing callback path re-invokes selection.
                return;
            }

            ClearPendingCharacterLoadState(selected.name);

            // 2) If already initialized (or not loadable), do full setup.
            m_currentCharacter = selected;

            DisableCharacterWrinkles(m_currentCharacter);

            m_thinkingController = selected.GetComponent<ThinkingController>();

            var profile = selected.GetComponent<VHCharacterProfile>();
            if (profile != null && profile.NVBG != null)
                m_nvbgSystem = profile.NVBG;

            if (m_nvbgSystem != null) m_nvbgSystem.StartProcess(selected.CharacterName);

            ChangeTts(m_ttsMode);
            SetCharacterConfigUIEnabled(true);
            StartCoroutine(SetSmile());

            // Introduce character on first load
            if (IntroduceOnLoad && !m_hasVHIntroducedThemselvesYet.Contains(characterName))
            {
                if (m_thinkingController != null)
                    m_thinkingController.StartThinkingBehavior(true);

                Debug.Log($"[DemoController] Introduction queued character='{characterName}' realtime={UsingRealtimeConversationMode} startButtonPressed={m_startButtonPressed}");
                StartCoroutine(WaitToIntroduceYourself(characterName));
            }
            else
            {
                SetPrompt(m_currentCharacter);
                if (m_gaze != null) m_gaze.GazeAt("GazeTargetUser");
            }

            // Seam for subclasses (e.g. the study player): character is fully set up.
            RaiseCharacterReady(m_currentCharacter);
        }

        /// <summary>
        /// Disables Reallusion wrinkle rendering for the specified character on iOS to avoid face artifacts
        /// caused by the head wrinkle shader path. This only affects the loaded runtime instance.
        /// </summary>
        /// <param name="character">The loaded character instance to patch.</param>
        private static void DisableCharacterWrinkles(MecanimCharacter character)
        {
            if (!RideUtils.IsIOS())
                return;

            if (character == null)
                return;

            var wrinkleManagers = character.GetComponentsInChildren<Reallusion.Runtime.WrinkleManager>(true);
            foreach (var wrinkleManager in wrinkleManagers)
            {
                if (wrinkleManager == null)
                    continue;

                Material headMaterial = wrinkleManager.headMaterial;
                if (headMaterial == null)
                    continue;

                if (headMaterial.HasProperty("ENUM_WRINKLE_MODE"))
                    headMaterial.SetFloat("ENUM_WRINKLE_MODE", 0f);

                headMaterial.DisableKeyword("ENUM_WRINKLE_MODE_WRINKLE");
                headMaterial.DisableKeyword("ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT");
                headMaterial.EnableKeyword("ENUM_WRINKLE_MODE_NONE");
                headMaterial.DisableKeyword("BOOLEAN_USE_WRINKLE_ON");

                wrinkleManager.enabled = false;
            }
        }

        /// <summary>
        /// Selects the next character from the selectable character list set in the Editor
        /// </summary>
        public void SelectNextCharacter()
        {
            if (m_currentCharacterIndex == m_selectableCharacters.Count - 1)
                m_currentCharacterIndex = 0;
            else
                m_currentCharacterIndex++;

            SelectCharacter(m_selectableCharacters[m_currentCharacterIndex].name);
        }

        private IEnumerator WaitToIntroduceYourself(string characterName)
        {
            yield return new WaitUntil(() => m_startButtonPressed);

            if (m_currentCharacter == null || !string.Equals(m_currentCharacter.name, characterName, StringComparison.Ordinal))
            {
                Debug.Log($"[DemoController] Introduction skipped queuedCharacter='{characterName}' currentCharacter='{(m_currentCharacter != null ? m_currentCharacter.name : "null")}'");
                yield break;
            }

            IntroduceYourself(characterName);
        }

        private void IntroduceYourself(string characterName)
        {
            SetPrompt(m_currentCharacter);

            // Character introductions should get ever simpler when selecting a new character within the same session
            // Phrased as a natural conversational cue rather than a setup instruction: wording like
            // "as the character named in your profile" reads to some models as an attempt to make them
            // adopt a persona, which the safety prompt tells them to refuse - Gemini did exactly that.
            string introductionPromptPre =      "Someone has just walked up to you. Greet them and introduce yourself " +
                                                    "warmly and briefly, using your own name. It's good to not be verbose. ";
            string introductionPromptWords =    "Use less than 14 words. ";  // Default after several subsequent introductions
            if (m_currentCharacter.name.ToLower().Contains("kevin"))        // Special case for Kevin, since his ElevenLabs voice produces gibberish on short utterances with commas
                introductionPromptWords =       "Use between 10 and 15 words. Do not use more than 1 comma. ";
            string introductionPromptPost =     "Don't mention you're a virtual human. Don't mention the " +
                                                    "Virtual Human Toolkit or VHToolkit.";

            switch (m_numberOfIntroductions)
            {
                case 0:
                    introductionPromptWords = "Use less than 25 words. ";
                    introductionPromptPost = "";
                    break;
                case 1:
                    introductionPromptWords = "Use less than 20 words. ";
                    introductionPromptPost = "Don't mention you're a virtual human. ";
                    break;
                case 2:
                    introductionPromptWords = "Use less than 17 words. ";
                    introductionPromptPost = "Don't mention you're a virtual human. ";
                    break;
            }

            string introductionAngle = s_introductionAngles[m_numberOfIntroductions % s_introductionAngles.Length];

            string introductionPrompt = introductionPromptPre + introductionAngle + introductionPromptWords + introductionPromptPost;
            Debug.Log($"[DemoController] Introduction sending character='{characterName}' " +
                $"angle={m_numberOfIntroductions % s_introductionAngles.Length} of {s_introductionAngles.Length} " +
                $"realtime={UsingRealtimeConversationMode} promptLength={introductionPrompt.Length}");
            m_introductionPending = !UsingRealtimeConversationMode;
            AskNLPQuestion(introductionPrompt);
            m_hasVHIntroducedThemselvesYet.Add(characterName);
            m_numberOfIntroductions++;
        }

        IEnumerator SetSmile()
        {
            yield return new WaitForEndOfFrame();

            Debug.Log($"Current character: {m_currentCharacter}, name: {m_currentCharacter.name}");

            // Set smile; expression needs to exist as character art or mapped in character's Facial Animation Player
            m_currentCharacter.PlayViseme("_112_happy", 0.6f); // 60% for slight smile
        }

        private void UnloadCharacter(MecanimCharacter character)
        {
            if (character == null)
                return;

            // Stop behaviors that may be referencing the loaded hierarchy.
            character.StopAnim();

            // If the character is loadable, unload/destroy the instantiated art.
            if (character.TryGetComponent<RideCatalogAsset>(out var loadable))
                loadable.ResetAsset();

            StartCoroutine(UnloadUnusedAssetsNextFrame());
        }

        public void CancelCharacterLoad()
        {
            if (m_pendingCharacterLoadAsset == null)
                return;

            Debug.Log($"[DemoController] Cancelling character load '{m_pendingCharacterName}'.");
            m_pendingCharacterLoadAsset.ResetAsset();
        }

        private void RegisterLoadableCharacter(MecanimCharacter character)
        {
            if (character == null || !character.TryGetComponent<RideCatalogAsset>(out var loadable))
                return;

            loadable.AssetLoadStarted -= OnCharacterAssetLoadStarted;
            loadable.AssetLoadProgressChanged -= OnCharacterAssetLoadProgressChanged;
            loadable.AssetLoadFailed -= OnCharacterAssetLoadFailed;
            loadable.AssetLoadCancelled -= OnCharacterAssetLoadCancelled;
            loadable.AssetInstanceReset -= OnCharacterAssetInstanceReset;

            loadable.AssetLoadStarted += OnCharacterAssetLoadStarted;
            loadable.AssetLoadProgressChanged += OnCharacterAssetLoadProgressChanged;
            loadable.AssetLoadFailed += OnCharacterAssetLoadFailed;
            loadable.AssetLoadCancelled += OnCharacterAssetLoadCancelled;
            loadable.AssetInstanceReset += OnCharacterAssetInstanceReset;
        }

        private void BeginPendingCharacterLoad(MecanimCharacter character, RideCatalogAsset loadable)
        {
            RegisterLoadableCharacter(character);
            m_pendingCharacterLoadAsset = loadable;
            m_pendingCharacterName = character != null ? character.name : string.Empty;
            m_lastPendingLoadPercent = -10;
            m_lastPendingLoadStatus = string.Empty;
            loadable.gameObject.SendMessage("ClearLoadedAssetStatus", SendMessageOptions.DontRequireReceiver);
        }

        private void ClearPendingCharacterLoadState(string characterName = null, bool clearVisualStatus = true)
        {
            if (clearVisualStatus && m_pendingCharacterLoadAsset != null)
                m_pendingCharacterLoadAsset.gameObject.SendMessage("ClearLoadedAssetStatus", SendMessageOptions.DontRequireReceiver);

            if (characterName != null && !string.IsNullOrEmpty(characterName))
                m_pendingCharacterName = characterName;
            else
                m_pendingCharacterName = string.Empty;

            m_pendingCharacterLoadAsset = null;
            m_lastPendingLoadPercent = -10;
            m_lastPendingLoadStatus = string.Empty;
        }

        private void OnCharacterAssetLoadStarted(RideCatalogAsset loadable)
        {
            if (loadable == null || loadable != m_pendingCharacterLoadAsset)
                return;

            loadable.gameObject.SendMessage("UpdateLoadedAssetStatus", "Preparing download...", SendMessageOptions.DontRequireReceiver);
        }

        private void OnCharacterAssetLoadProgressChanged(RideCatalogAsset loadable, float progress)
        {
            if (loadable == null || loadable != m_pendingCharacterLoadAsset)
                return;

            var bundleLoader = Systems.Get<AssetLoadingSystemAssetBundles>();
            string status = BuildPendingLoadStatus(progress, bundleLoader);
            if (!string.Equals(status, m_lastPendingLoadStatus, StringComparison.Ordinal))
            {
                loadable.gameObject.SendMessage("UpdateLoadedAssetStatus", status, SendMessageOptions.DontRequireReceiver);
                m_lastPendingLoadStatus = status;
            }

            int percent = Mathf.Clamp(Mathf.FloorToInt(progress * 100f), 0, 100);
            if (!ShouldReportProgressPercent(percent, m_lastPendingLoadPercent, out int reportedPercent))
                return;

            m_lastPendingLoadPercent = reportedPercent;

            if (bundleLoader != null && bundleLoader.RemoteBundleDownloadActive)
            {
                Debug.Log(
                    $"[DemoController] {loadable.gameObject.name} loading - {reportedPercent}% " +
                    $"({FormatBytes(bundleLoader.RemoteBundleDownloadedBytes)} at {FormatBytesPerSecond(bundleLoader.RemoteBundleAverageBytesPerSecond)})");
            }
            else
            {
                Debug.Log($"[DemoController] {loadable.gameObject.name} loading - {reportedPercent}%");
            }
        }

        private void OnCharacterAssetLoadFailed(RideCatalogAsset loadable, string error)
        {
            if (loadable == null || loadable != m_pendingCharacterLoadAsset)
                return;

            string userMessage = BuildFriendlyLoadFailureStatus(error);
            loadable.gameObject.SendMessage("UpdateLoadedAssetStatus", userMessage, SendMessageOptions.DontRequireReceiver);
            m_lastPendingLoadStatus = userMessage;
            SetCharacterConfigUIEnabled(true);
            Debug.LogWarning($"[DemoController] Character load failed for '{loadable.gameObject.name}': {error}");
            ClearPendingCharacterLoadState(clearVisualStatus: false);
        }

        private void OnCharacterAssetLoadCancelled(RideCatalogAsset loadable)
        {
            if (loadable == null || loadable != m_pendingCharacterLoadAsset)
                return;

            const string message = "Load cancelled.\nSelect again to retry.";
            loadable.gameObject.SendMessage("UpdateLoadedAssetStatus", message, SendMessageOptions.DontRequireReceiver);
            m_lastPendingLoadStatus = message;
            SetCharacterConfigUIEnabled(true);
            Debug.Log($"[DemoController] Character load cancelled for '{loadable.gameObject.name}'.");
            ClearPendingCharacterLoadState(clearVisualStatus: false);
        }

        private void OnCharacterAssetInstanceReset(RideCatalogAsset loadable, GameObject instance)
        {
            if (loadable == null || loadable != m_pendingCharacterLoadAsset)
                return;

            loadable.gameObject.SendMessage("ClearLoadedAssetStatus", SendMessageOptions.DontRequireReceiver);
        }

        private static string BuildPendingLoadStatus(float progress, AssetLoadingSystemAssetBundles bundleLoader)
        {
            int percent = Mathf.Clamp(Mathf.FloorToInt(progress * 100f), 0, 100);
            if (bundleLoader == null || !bundleLoader.RemoteBundleDownloadActive)
                return percent < 20 ? "Preparing download..." : "Finalizing...";

            return $"{FormatBytes(bundleLoader.RemoteBundleDownloadedBytes)} at {FormatBytesPerSecond(bundleLoader.RemoteBundleAverageBytesPerSecond)}";
        }

        private static string BuildFriendlyLoadFailureStatus(string error)
        {
            if (string.IsNullOrEmpty(error))
                return "Load failed.\nSelect again to retry.";

            string lower = error.ToLowerInvariant();
            if (lower.Contains("timed out") || lower.Contains("timeout"))
                return "Download timed out.\nSelect again to retry.";

            return "Load failed.\nSelect again to retry.";
        }

        private static bool ShouldReportProgressPercent(int percent, int lastReportedPercent, out int reportedPercent)
        {
            const int ProgressReportIncrementPercent = 10;

            reportedPercent = Mathf.Clamp(percent, 0, 100);
            if (reportedPercent >= 100)
                return reportedPercent > lastReportedPercent;

            return (reportedPercent - lastReportedPercent) >= ProgressReportIncrementPercent;
        }

        private static string FormatBytes(ulong bytes)
        {
            const float KB = 1024f;
            const float MB = KB * 1024f;
            const float GB = MB * 1024f;

            if (bytes >= GB) return $"{bytes / GB:0.00} GB";
            if (bytes >= MB) return $"{bytes / MB:0.00} MB";
            if (bytes >= KB) return $"{bytes / KB:0.0} KB";
            return $"{bytes} B";
        }

        private static string FormatBytesPerSecond(float bytesPerSecond)
        {
            if (bytesPerSecond <= 0f) return "0 B/s";

            const float KB = 1024f;
            const float MB = KB * 1024f;
            const float GB = MB * 1024f;

            if (bytesPerSecond >= GB) return $"{bytesPerSecond / GB:0.00} GB/s";
            if (bytesPerSecond >= MB) return $"{bytesPerSecond / MB:0.00} MB/s";
            if (bytesPerSecond >= KB) return $"{bytesPerSecond / KB:0.0} KB/s";
            return $"{bytesPerSecond:0} B/s";
        }

        private IEnumerator UnloadUnusedAssetsNextFrame()
        {
            yield return null; // allow Destroy() to complete
            yield return Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        /// <summary>
        /// Updates the ASR button color based on recognition state or audio playback status.
        /// </summary>
        protected override void UpdateAsrButtonColor()
        {
            if (m_demoControllerUI == null || m_currentASR == null) return;

            var voice = m_currentCharacter != null ? m_currentCharacter.Voice : null;

            if (string.IsNullOrEmpty(m_currentASR.SelectedMicrophone) || (!UsingRealtimeConversationMode && !m_characterConfigUIEnabled))
                m_demoControllerUI.SetAsrButtonColor(Color.gray);
            else if (m_currentASR.IsRecognizing)
                m_demoControllerUI.SetAsrButtonColor(Color.red);
            else if (UsingRealtimeConversationMode && m_openAIRealtimeConversationSystem != null && m_openAIRealtimeConversationSystem.IsAssistantSpeaking)
                m_demoControllerUI.SetAsrButtonColor(Color.gray);
            else if (!UsingRealtimeConversationMode && voice != null && voice.isPlaying)
                m_demoControllerUI.SetAsrButtonColor(Color.gray);
            else
                m_demoControllerUI.SetAsrButtonColor(Color.white);
        }

        /// <summary>
        /// Stops the current character's audio and lipsync playback.
        /// </summary>
        public override void StopUtterance()
        {
            base.StopUtterance();
            CurrentCharacter.StopAnim();

            var cutscenes = CurrentCharacter.transform.GetComponentsInChildren<Cutscene>();
            foreach (Cutscene cutscene in cutscenes) { cutscene.Stop(); }
        }

        /// <summary>
        /// Updates the load next character button color based on whether character is loading or audio playback status.
        /// </summary>
        protected override void UpdateNextCharacterButtonColor()
        {
            if (m_demoControllerUI == null || m_currentASR == null) return;

            var voice = m_currentCharacter != null ? m_currentCharacter.Voice : null;

            if (!m_characterConfigUIEnabled)
                m_demoControllerUI.SetNextCharacterButtonColor(Color.gray);
            else if (voice != null && voice.isPlaying)
                m_demoControllerUI.SetNextCharacterButtonColor(Color.gray);
            else
                m_demoControllerUI.SetNextCharacterButtonColor(Color.white);
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Escape))
                RideUtils.QuitApplication();
        }

        public override void SetLipsyncMethod(LipsyncOptions method)
        {
            base.SetLipsyncMethod(method);

            // this version of script doesn't have support for OVR since it's non-distributable
            // always use VH version
            var character = CurrentCharacter;
            var animator = character.GetComponentInChildren<Animator>(true);

            if (animator != null)
                SetMouthLayer(animator, true);
        }

        protected static void SetMouthLayer(Animator animator, bool enabled)
        {
            int mouthLayerIndex = animator.GetLayerIndex("Mouth Layer");
            if (mouthLayerIndex >= 0)
                animator.SetLayerWeight(mouthLayerIndex, enabled ? 1 : 0);
            else
                Debug.LogWarning("Animator layer 'Mouth Layer' not found.");
        }
    }
}
