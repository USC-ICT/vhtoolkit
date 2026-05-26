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
                UnloadCharacter(m_currentCharacter);

            // Disable all non-selected characters.
            foreach (var character in m_characters)
            {
                if (character != null && character != selected)
                    character.gameObject.SetActive(false);
            }

            // Ensure the selected character is active.
            selected.gameObject.SetActive(true);

            SetCharacterConfigUIEnabled(false);
            m_currentASR.StopRecognizing();
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

            m_nvbgSystem.StartProcess(selected.CharacterName);

            SetPrompt(m_currentCharacter);
            ChangeTts(m_ttsMode);
            SetCharacterConfigUIEnabled(true);
            StartCoroutine(SetSmile());

            // Introduce character on first load
            if (IntroduceOnLoad && !m_hasVHIntroducedThemselvesYet.Contains(characterName))
            {
                if (m_thinkingController != null)
                    m_thinkingController.StartThinkingBehavior(true);

                m_hasVHIntroducedThemselvesYet.Add(characterName);
                StartCoroutine(IntroduceYourself());
            }
            else
                m_gaze.GazeAt("GazeTargetUser");
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

        IEnumerator IntroduceYourself()
        {
            // Wait for all systems to initialize, in particular LLM prompt and TTS voice
            yield return new WaitUntil(() => m_startButtonPressed);
            yield return new WaitForEndOfFrame();

            // Character introductions should get ever simpler when selecting a new character within the same session
            string introductionPromptPre =      "Politely and concisely introduce yourself. This introduction will happen many times, " +
                                                    "so be creative. Do not make up any facts, including what your personal name is. " +
                                                    "It's good to not be verbose. You can ask how the user is doing. ";
            string introductionPromptWords =    "Use less than 9 words. ";  // Default after several subsequent introductions
            if (m_currentCharacter.name.ToLower().Contains("kevin"))        // Special case for Kevin, since his ElevenLabs voice produces gibberish on short utterances with commas
                introductionPromptWords =       "Use between 10 and 15 words. Do not use more than 1 comma. ";
            string introductionPromptPost =     "Don't mention you're a virtual human. Don't mention the " +
                                                    "Virtual Human Toolkit or VHToolkit. You can ask the user how they are.";

            switch (m_numberOfIntroductions)
            {
                case 0:
                    introductionPromptWords = "Use less than 25 words. ";
                    introductionPromptPost = "";
                    break;
                case 1:
                    introductionPromptWords = "Use less than 17 words. ";
                    introductionPromptPost = "Don't mention you're a virtual human. ";
                    break;
                case 2:
                    introductionPromptWords = "Use less than 12 words. ";
                    introductionPromptPost = "Don't mention you're a virtual human. ";
                    break;
            }

            AskNLPQuestion(introductionPromptPre + introductionPromptWords + introductionPromptPost);
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

            if (string.IsNullOrEmpty(m_currentASR.SelectedMicrophone) || !m_characterConfigUIEnabled)
                m_demoControllerUI.SetAsrButtonColor(Color.gray);
            else if (m_currentASR.IsRecognizing)
                m_demoControllerUI.SetAsrButtonColor(Color.red);
            else if (voice != null && voice.isPlaying)
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
