using Amazon.Runtime.Internal.Transform;
using Ride.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;
using UnityEngine.XR;
using VHAssets;
#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine.Android;
#endif

namespace Ride.Examples
{
    /// <summary>
    /// AR specific controller for VHToolkit demo
    /// </summary>
    public class DemoControllerAR : DemoControllerBase
    {
        public enum AsrControlMode { HoldToTalk, ToggleToTalk }

        [Header("UI")]
        [SerializeField] private DemoController_UIAR m_uiController;

        [Header("Input Settings")]
        public XRNode inputSourceRight = XRNode.RightHand;
        public XRNode inputSourceLeft = XRNode.LeftHand;

        public DemoController_UIAR UI_Controller {  get { return m_uiController; } }

        protected override IDemoControllerUI BindUI() => m_uiController;
#if UNITY_ANDROID && !UNITY_EDITOR
        bool HasMicPermission() => Permission.HasUserAuthorizedPermission(Permission.Microphone);
#endif

        private InputDevice m_rightHandInputDevice;
        private InputDevice m_leftHandInputDevice;

        private HashSet<String> m_hasVHIntroducedThemselvesYet;
        private int m_numberOfIntroductions = 0;
        private bool m_useElevenlabs = false;
        private PlacedExperienceRoot m_placementRoot;
        private readonly HashSet<RideCatalogAsset> m_registeredLoadableCharacters = new();
        private readonly Dictionary<RideCatalogAsset, List<Material>> m_runtimeMaterialOverrides = new();
        private RideCatalogAsset m_pendingCharacterLoadAsset;
        private string m_pendingCharacterName;
        private int m_lastPendingLoadPercent = -10;
        private string m_lastPendingLoadStatus = string.Empty;

        public bool CharacterLoadPending => m_pendingCharacterLoadAsset != null;
        public bool CharacterSelectionEnabled
        {
            get
            {
                if (CharacterLoadPending)
                    return false;

                if (CurrentCharacter == null)
                    return true;

                if (!m_characterConfigUIEnabled)
                    return false;

                var voice = CurrentCharacter.Voice;
                return voice == null || !voice.isPlaying;
            }
        }

        private readonly HashSet<MecanimCharacter> m_configuredCharacters = new();
        [Header("Runtime Performance")]
        [SerializeField] private bool m_optimizeBundledCharactersForMobile = true;
        [SerializeField] private bool m_disableBundledCharacterShadows = true;
        [SerializeField] private bool m_disableBundledCharacterWrinkleManagers = true;
        [SerializeField] private bool m_disableCharacterMotionVectors = true;
        [SerializeField] private bool m_rebuildBundledCharacterMaterialsForCurrentPipeline = true;
        [SerializeField] private bool m_disableBundledCharacterSecondPassHair = false;
        [SerializeField] private bool m_useBundledCharacterPassthroughHairShader = true;

        public bool IsPlacementBound => m_placementRoot != null && m_placementRoot.IsPlaced;

        protected override void Start()
        {
            m_hasVHIntroducedThemselvesYet = new HashSet<string>();

            m_rightHandInputDevice = InputDevices.GetDeviceAtXRNode(inputSourceRight);
            m_leftHandInputDevice = InputDevices.GetDeviceAtXRNode(inputSourceLeft);

            base.Start();
            
#if UNITY_ANDROID && !UNITY_EDITOR
            // Use Azure on Quest/Android
            m_currentASR = m_azureSpeechRecognitionSystem;
#endif
        }

        protected override void Update()
        {
            base.Update();

            UpdateAsrButtonColor();

            if (!m_rightHandInputDevice.isValid)
                m_rightHandInputDevice = InputDevices.GetDeviceAtXRNode(inputSourceRight);
            if (!m_leftHandInputDevice.isValid)
                m_leftHandInputDevice = InputDevices.GetDeviceAtXRNode(inputSourceLeft);

            // If A is pressed on right controller, load ElevenLabs instead of AWS Polly     
            m_useElevenlabs = false;
            m_rightHandInputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out m_useElevenlabs); 
        }

        public override void ToggleASR()
        {
            if (!IsPlacementBound)
                return;

            if (m_currentASR == null || m_currentCharacter == null) return;

            // While the character is speaking, the button interrupts them rather than starting
            // recognition. This applies whether or not recognition was previously requested,
            // since the user's intent in that moment is to stop the character.
            var voice = m_currentCharacter.Voice;
            if (voice != null && voice.isPlaying)
            {
                StopUtterance();
                return;
            }

            DisableMicrophones();

#if UNITY_ANDROID && !UNITY_EDITOR
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
                return; // wait for user response; try again on next press
            }
#endif      
            
            base.ToggleASR();
        }

        public override void StopUtterance()
        {
            base.StopUtterance();

            if (CurrentCharacter == null)
                return;

            CurrentCharacter.StopAnim();

            var cutscenes = CurrentCharacter.transform.GetComponentsInChildren<Cutscene>();
            foreach (Cutscene cutscene in cutscenes)
                cutscene.Stop();
        }

        /// <summary>
        /// Try to avoid having microphones locked, which prevents ASR from starting; known issue on Quest 3
        /// </summary>
        private void DisableMicrophones()
        {
            try
            {
                foreach (var dev in Microphone.devices) Microphone.End(dev);
                Microphone.End(null);
            }
            catch { }
        }

        protected override void CollectCharacters()
        {
            m_characters.Clear();

            if (m_charactersParent == null)
            {
                Debug.LogWarning("DemoControllerAR: m_charactersParent is not assigned.");
                return;
            }

            foreach (Transform child in m_charactersParent)
                if (child.TryGetComponent(out MecanimCharacter mc))
                {
                    m_characters.Add(mc);
                    RegisterLoadableCharacter(mc);
                }
        }

        public void BindPlacement(PlacedExperienceRoot placementRoot)
        {
            if (placementRoot == null)
                return;

            m_placementRoot = placementRoot;
            m_charactersParent = placementRoot.CharactersRoot;
            m_placementRoot.ShowPlacementContent();
            UI_Controller?.ShowPlacementUI();
            UI_Controller?.InitializeCanvasCamera();
            CollectCharacters();
            ConfigurePlacedCharacters();
            SetCharacterConfigUIEnabled(false);
            SetASR(false);
        }

        public void UnbindPlacement()
        {
            SetASR(false);
            StopUtterance();

            if (m_pendingCharacterLoadAsset != null)
                m_pendingCharacterLoadAsset.ResetAsset();

            ClearPendingCharacterLoadState();

            if (m_currentCharacter != null)
                UnloadCharacter(m_currentCharacter);

            m_currentCharacter = null;
            m_characters.Clear();
            m_configuredCharacters.Clear();
            m_charactersParent = null;
            m_placementRoot?.HidePlacementContent();
            UI_Controller?.HidePlacementUI();
            m_placementRoot = null;
            SetCharacterConfigUIEnabled(false);
        }

        public override void SelectCharacter(string characterName)
        {
            Debug.Log($"SelectCharacterInternal, characterName {characterName}");

            if (CharacterLoadPending && !string.Equals(m_pendingCharacterName, characterName, StringComparison.Ordinal))
            {
                Debug.Log($"[DemoControllerAR] Ignoring selection '{characterName}' while '{m_pendingCharacterName}' is loading.");
                return;
            }
    
            // Find selected character first
            MecanimCharacter selected = null;
            foreach (var c in m_characters)
            {
                if (c != null && c.name == characterName)
                {
                    selected = c;
                    break;
                }
            }

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
                loadable.LoadAsset();  // When loaded, your existing callback path re-invokes selection.
                return;
            }

            ClearPendingCharacterLoadState(selected.name);

            // 2) If already initialized (or not loadable), do full setup.
            m_currentCharacter = selected;
            m_thinkingController = selected.GetComponent<ThinkingController>();

            // A character can be activated in the same frame it is selected, before Unity has called
            // Start on its components. Initialize explicitly so that thinking behavior records the
            // character's authored gaze weights rather than defaults, and restores them when it ends.
            if (m_thinkingController != null)
                m_thinkingController.InitializeLoadedAsset();

            var profile = selected.GetComponent<VHCharacterProfile>();
            if (profile != null && profile.NVBG != null)
                m_nvbgSystem = profile.NVBG;

            if (m_nvbgSystem != null) m_nvbgSystem.StartProcess(selected.CharacterName);
            if (m_gaze != null) m_gaze.GazeAt(m_currentCharacter, "CenterEyeAnchor");

            // Use default TTS, typicaly AWS Polly, unless user selects ElevenLabs)
            TtsMode localTTSMode = m_ttsMode;                       
            if (m_useElevenlabs)
                localTTSMode = TtsMode.ElevenLabs;            
            ChangeTts(localTTSMode);
            SetCharacterConfigUIEnabled(true);
            StartCoroutine(SetSmile());

            // Introduce character on first load
            if (IntroduceOnLoad && !m_hasVHIntroducedThemselvesYet.Contains(characterName))
            {
                m_hasVHIntroducedThemselvesYet.Add(characterName); 
                IntroduceYourself();
            }
            else
                SetPrompt(m_currentCharacter);
        }

        void IntroduceYourself()
        {
            SetPrompt(m_currentCharacter);

            // Character introductions should get ever simpler when selecting a new character within the same session
            string introductionPromptPre    = "Politely and concisely introduce yourself as the character named in your profile. " +
                                              "Use your profile's name and description accurately. This introduction will happen many times, " +
                                              "so vary the wording and delivery. It's good to not be verbose. You can ask how the user is doing. ";
            string introductionPromptWords  = "Use less than 6 words. ";
            string introductionPromptPost   = "Don't mention you're a virtual human. Don't mention the " +
                                              "Virtual Human Toolkit or VHToolkit. You can ask the user how they are.";           

            switch (m_numberOfIntroductions)
            {
                case 0:
                    introductionPromptWords = "Use less than 25 words. ";
                    introductionPromptPost = "";
                    break;
                case 1:
                    introductionPromptWords = "Use less than 20 words. ";
                    introductionPromptPost = "";
                    break;
                case 2:
                    introductionPromptWords = "Use less than 10 words. ";
                    break;
            }

            // Kevin's ElevenLabs voice produces gibberish on short utterances with commas, so every
            // Kevin keeps a word floor. Applied after the ladder so it cannot be shortened below it;
            // the first two introductions already ask for more and are left as they are.
            if (m_currentCharacter.name.ToLower().Contains("kevin") && m_numberOfIntroductions >= 2)
                introductionPromptWords = "Use between 10 and 15 words. Do not use more than 1 comma. ";

            AskNLPQuestion(introductionPromptPre + introductionPromptWords + introductionPromptPost);
            m_numberOfIntroductions++;
        }

        IEnumerator SetSmile()
        {
            yield return new WaitForEndOfFrame();

            // Set smile; expression needs to exist as character art or mapped in character's Facial Animation Player
            m_currentCharacter.PlayViseme("_112_happy", 0.6f);
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

        private IEnumerator UnloadUnusedAssetsNextFrame()
        {
            yield return null; // allow Destroy() to complete
            yield return Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        private void RegisterLoadableCharacter(MecanimCharacter character)
        {
            if (character == null || !character.TryGetComponent<RideCatalogAsset>(out var loadable))
                return;

            if (!m_registeredLoadableCharacters.Add(loadable))
                return;

            loadable.AssetLoadStarted += OnCharacterAssetLoadStarted;
            loadable.AssetLoadProgressChanged += OnCharacterAssetLoadProgressChanged;
            loadable.AssetLoadFailed += OnCharacterAssetLoadFailed;
            loadable.AssetLoadCancelled += OnCharacterAssetLoadCancelled;
            loadable.AssetInstanceLoaded += OnCharacterAssetInstanceLoaded;
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

            m_pendingCharacterName = !string.IsNullOrEmpty(characterName) ? characterName : string.Empty;
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
            Debug.Log($"[DemoControllerAR] {loadable.gameObject.name} loading - {reportedPercent}%");
        }

        private void OnCharacterAssetLoadFailed(RideCatalogAsset loadable, string error)
        {
            if (loadable == null || loadable != m_pendingCharacterLoadAsset)
                return;

            string userMessage = BuildFriendlyLoadFailureStatus(error);
            loadable.gameObject.SendMessage("UpdateLoadedAssetStatus", userMessage, SendMessageOptions.DontRequireReceiver);
            m_lastPendingLoadStatus = userMessage;
            SetCharacterConfigUIEnabled(true);
            Debug.LogWarning($"[DemoControllerAR] Character load failed for '{loadable.gameObject.name}': {error}");
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
            Debug.Log($"[DemoControllerAR] Character load cancelled for '{loadable.gameObject.name}'.");
            ClearPendingCharacterLoadState(clearVisualStatus: false);
        }

        private void OnCharacterAssetInstanceLoaded(RideCatalogAsset loadable, GameObject instance)
        {
            if (instance == null)
                return;

            if (m_runtimeMaterialOverrides.TryGetValue(loadable, out var previousOverrides))
            {
                DestroyRuntimeMaterials(previousOverrides);
                m_runtimeMaterialOverrides.Remove(loadable);
            }

            float rebuildStart = Time.realtimeSinceStartup;
            int removedHairPassCount = m_disableBundledCharacterSecondPassHair
                ? RemoveSecondPassHairMaterials(instance)
                : 0;
            int reassignedCount = 0;
            List<Material> runtimeMaterials = null;
            if (m_rebuildBundledCharacterMaterialsForCurrentPipeline)
            {
                reassignedCount = RebuildLoadedCharacterMaterials(instance, out runtimeMaterials);
            }

            runtimeMaterials ??= new List<Material>();
            int passthroughHairShaderCount = m_useBundledCharacterPassthroughHairShader
                ? ReplaceFirstPassHairShaders(instance, runtimeMaterials)
                : 0;
            if (runtimeMaterials.Count > 0)
                m_runtimeMaterialOverrides[loadable] = runtimeMaterials;

            int optimizationCount = OptimizeCharacterHierarchy(instance, m_optimizeBundledCharactersForMobile);

            if (loadable.TryGetComponent(out MecanimCharacter character))
                ConfigurePlacedCharacter(character);

            if (removedHairPassCount > 0 || passthroughHairShaderCount > 0 ||
                reassignedCount > 0 || optimizationCount > 0)
            {
                Debug.Log(
                    $"[DemoControllerAR] Loaded bundled character '{instance.name}' " +
                    $"secondPassHairRemoved={removedHairPassCount} " +
                    $"passthroughHairShadersApplied={passthroughHairShaderCount} " +
                    $"materialsRebuilt={reassignedCount} " +
                    $"optimizations={optimizationCount} " +
                    $"elapsed={(Time.realtimeSinceStartup - rebuildStart):0.000}s");
            }
        }

        private static int ReplaceFirstPassHairShaders(
            GameObject instance,
            List<Material> runtimeMaterials)
        {
            const string PassthroughHairShaderResourceName = "ARHairPassthroughComposite";
            var passthroughHairShader = Resources.Load<Shader>(PassthroughHairShaderResourceName);
            if (passthroughHairShader == null)
            {
                Debug.LogWarning(
                    $"[DemoControllerAR] Shader resource '{PassthroughHairShaderResourceName}' was not found.");
                return 0;
            }

            int replacedCount = 0;

            foreach (var renderer in instance.GetComponentsInChildren<Renderer>(true))
            {
                var materials = renderer.sharedMaterials;
                bool changed = false;

                for (int i = 0; i < materials.Length; i++)
                {
                    var sourceMaterial = materials[i];
                    if (!IsFirstPassHairMaterial(sourceMaterial))
                        continue;

                    var passthroughHairMaterial = new Material(passthroughHairShader)
                    {
                        name = sourceMaterial.name,
                        enableInstancing = sourceMaterial.enableInstancing,
                        doubleSidedGI = sourceMaterial.doubleSidedGI
                    };
                    passthroughHairMaterial.CopyPropertiesFromMaterial(sourceMaterial);
                    passthroughHairMaterial.shaderKeywords = sourceMaterial.shaderKeywords;
                    passthroughHairMaterial.renderQueue = (int)RenderQueue.Transparent;

                    materials[i] = passthroughHairMaterial;
                    runtimeMaterials.Add(passthroughHairMaterial);
                    replacedCount++;
                    changed = true;
                }

                if (changed)
                    renderer.sharedMaterials = materials;
            }

            return replacedCount;
        }

        private static int RemoveSecondPassHairMaterials(GameObject instance)
        {
            int removedCount = 0;

            foreach (var renderer in instance.GetComponentsInChildren<Renderer>(true))
            {
                var materials = renderer.sharedMaterials;
                List<Material> retainedMaterials = null;

                for (int i = 0; i < materials.Length; i++)
                {
                    if (!IsSecondPassHairMaterial(materials[i]))
                    {
                        retainedMaterials?.Add(materials[i]);
                        continue;
                    }

                    if (retainedMaterials == null)
                    {
                        retainedMaterials = new List<Material>(materials.Length);
                        for (int retainedIndex = 0; retainedIndex < i; retainedIndex++)
                            retainedMaterials.Add(materials[retainedIndex]);
                    }

                    removedCount++;
                }

                if (retainedMaterials != null)
                    renderer.sharedMaterials = retainedMaterials.ToArray();
            }

            return removedCount;
        }

        private static bool IsSecondPassHairMaterial(Material material)
        {
            if (material == null)
                return false;

            string materialName = material.name ?? string.Empty;
            string shaderName = material.shader != null ? material.shader.name : string.Empty;
            bool isSecondPass = materialName.IndexOf("2nd_Pass", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                shaderName.IndexOf("HairShader_2nd_Pass", StringComparison.OrdinalIgnoreCase) >= 0;
            bool isHair = materialName.IndexOf("Hair", StringComparison.OrdinalIgnoreCase) >= 0 &&
                          materialName.IndexOf("Brow", StringComparison.OrdinalIgnoreCase) < 0 &&
                          materialName.IndexOf("Lash", StringComparison.OrdinalIgnoreCase) < 0;
            return isSecondPass && isHair;
        }

        private static bool IsFirstPassHairMaterial(Material material)
        {
            if (material == null)
                return false;

            string materialName = material.name ?? string.Empty;
            string shaderName = material.shader != null ? material.shader.name : string.Empty;
            bool isFirstPass = materialName.IndexOf("1st_Pass", StringComparison.OrdinalIgnoreCase) >= 0 ||
                               shaderName.IndexOf("HairShader_1st_Pass", StringComparison.OrdinalIgnoreCase) >= 0;
            bool isHair = materialName.IndexOf("Hair", StringComparison.OrdinalIgnoreCase) >= 0 &&
                          materialName.IndexOf("Brow", StringComparison.OrdinalIgnoreCase) < 0 &&
                          materialName.IndexOf("Lash", StringComparison.OrdinalIgnoreCase) < 0;
            return isFirstPass && isHair;
        }

        private void OnCharacterAssetInstanceReset(RideCatalogAsset loadable, GameObject instance)
        {
            if (m_runtimeMaterialOverrides.TryGetValue(loadable, out var runtimeMaterials))
            {
                DestroyRuntimeMaterials(runtimeMaterials);
                m_runtimeMaterialOverrides.Remove(loadable);
            }

            if (loadable != null && loadable == m_pendingCharacterLoadAsset)
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
            if (!string.IsNullOrEmpty(error) && error.ToLowerInvariant().Contains("timeout"))
                return "Download timed out.\nSelect again to retry.";

            return "Load failed.\nSelect again to retry.";
        }

        private static bool ShouldReportProgressPercent(int percent, int lastReportedPercent, out int reportedPercent)
        {
            const int ProgressReportIncrementPercent = 10;
            reportedPercent = Mathf.Clamp(percent, 0, 100);
            if (reportedPercent >= 100)
                return reportedPercent > lastReportedPercent;

            return reportedPercent - lastReportedPercent >= ProgressReportIncrementPercent;
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

        private static int RebuildLoadedCharacterMaterials(GameObject instance, out List<Material> runtimeMaterials)
        {
            runtimeMaterials = new List<Material>();
            int rebuiltCount = 0;
            var renderers = instance.GetComponentsInChildren<Renderer>(true);

            foreach (var renderer in renderers)
            {
                var materials = renderer.sharedMaterials;
                bool changed = false;

                for (int i = 0; i < materials.Length; i++)
                {
                    var sourceMaterial = materials[i];
                    if (sourceMaterial == null || sourceMaterial.shader == null)
                        continue;

                    var replacementShader = Shader.Find(sourceMaterial.shader.name);
                    if (replacementShader == null)
                        continue;

                    var rebuiltMaterial = new Material(replacementShader)
                    {
                        name = sourceMaterial.name,
                        renderQueue = sourceMaterial.renderQueue,
                        enableInstancing = sourceMaterial.enableInstancing,
                        doubleSidedGI = sourceMaterial.doubleSidedGI
                    };
                    rebuiltMaterial.CopyPropertiesFromMaterial(sourceMaterial);
                    rebuiltMaterial.shaderKeywords = sourceMaterial.shaderKeywords;

                    materials[i] = rebuiltMaterial;
                    runtimeMaterials.Add(rebuiltMaterial);
                    changed = true;
                    rebuiltCount++;
                }

                if (changed)
                    renderer.sharedMaterials = materials;
            }

            return rebuiltCount;
        }

        private static void DestroyRuntimeMaterials(List<Material> runtimeMaterials)
        {
            if (runtimeMaterials == null)
                return;

            foreach (var material in runtimeMaterials)
            {
                if (material != null)
                    UnityEngine.Object.Destroy(material);
            }
        }

        private int OptimizeCharacterHierarchy(GameObject root, bool enabledForPlatform)
        {
            if (!enabledForPlatform || root == null || Application.platform != RuntimePlatform.Android)
                return 0;

            int changes = 0;
            var renderers = root.GetComponentsInChildren<Renderer>(true);
            foreach (var renderer in renderers)
            {
                if (m_disableBundledCharacterShadows)
                {
                    if (renderer.shadowCastingMode != ShadowCastingMode.Off)
                    {
                        renderer.shadowCastingMode = ShadowCastingMode.Off;
                        changes++;
                    }

                    if (renderer.receiveShadows)
                    {
                        renderer.receiveShadows = false;
                        changes++;
                    }
                }

                if (renderer.reflectionProbeUsage != ReflectionProbeUsage.Off)
                {
                    renderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
                    changes++;
                }

                if (renderer.lightProbeUsage != LightProbeUsage.Off)
                {
                    renderer.lightProbeUsage = LightProbeUsage.Off;
                    changes++;
                }

                if (m_disableCharacterMotionVectors && renderer.motionVectorGenerationMode != MotionVectorGenerationMode.ForceNoMotion)
                {
                    renderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
                    changes++;
                }
            }

            if (m_disableBundledCharacterWrinkleManagers)
            {
                var behaviours = root.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (var behaviour in behaviours)
                {
                    if (behaviour == null || !behaviour.enabled)
                        continue;

                    if (behaviour.GetType().Name == "WrinkleManager")
                    {
                        behaviour.enabled = false;
                        changes++;
                    }
                }
            }

            return changes;
        }

        private void ConfigurePlacedCharacters()
        {
            foreach (var character in Characters)
                ConfigurePlacedCharacter(character);
        }

        private void ConfigurePlacedCharacter(MecanimCharacter character)
        {
            if (character == null || !m_configuredCharacters.Add(character))
                return;

            int optimizationCount = OptimizeCharacterHierarchy(character.gameObject, true);
            if (optimizationCount > 0)
                Debug.Log($"[DemoControllerAR] Optimized character hierarchy '{character.name}' changes={optimizationCount}");

            var mecanimManager = FindAnyObjectByType<MecanimManager>();
            if (mecanimManager != null)
                mecanimManager.AddCharacter(character);

            var bmlHandler = character.GetComponent<BMLEventHandler>();
            if (bmlHandler == null)
                return;

            bmlHandler.m_CharacterController = mecanimManager;

            var cutscene = GameObject.Find("Cutscene01")?.GetComponent<Cutscene>();
            if (cutscene != null)
                bmlHandler.m_CutscenePrefab = cutscene;

            bmlHandler.InitializeLoadedAsset();
        }

        protected override void UpdateAsrButtonColor()
        {
            if (m_demoControllerUI == null || m_currentASR == null) return;

            // Resolved once per update: the character's audio source is looked up on each access,
            // and is absent until the character's art is loaded and active.
            AudioSource characterVoice = m_currentCharacter != null ? m_currentCharacter.Voice : null;

            if (!IsPlacementBound)
            {
                m_demoControllerUI.SetAsrButtonColor(Color.gray);
                (m_demoControllerUI as DemoController_UIAR).SetAsrButtonText("Talk to Character");
            }
            else if (m_currentASR.IsRecognizing)
            {
                m_demoControllerUI.SetAsrButtonColor(Color.red);
                (m_demoControllerUI as DemoController_UIAR).SetAsrButtonText("Listening...");
            }
            else if (characterVoice != null && characterVoice.isPlaying)
            {
                m_demoControllerUI.SetAsrButtonColor(Color.gray);
                (m_demoControllerUI as DemoController_UIAR).SetAsrButtonText("Talk to Character");
            }
            else
            {
                m_demoControllerUI.SetAsrButtonColor(Color.white);
                (m_demoControllerUI as DemoController_UIAR).SetAsrButtonText("Talk to Character");
            }
        }

        /// <summary>
        /// Not used in AR; needed per the IDemoControllerUI.
        /// </summary>
        protected override void UpdateNextCharacterButtonColor()
        {

        }

        protected override void AfterSystemsInitialized() 
        {
            StartCoroutine(LoadCachedCatalogsLogged());
            StartCoroutine(TurnOffExitMenuUI());
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
                    Debug.Log($"[DemoControllerAR] LoadCachedCatalogs HEARTBEAT elapsed={(now - start):0.000}s");
                    lastLog = now;
                }

                yield return e.Current;
            }

            Debug.Log($"[DemoControllerAR] LoadCachedCatalogs COMPLETE elapsed={(Time.realtimeSinceStartup - start):0.000}s catalogsLoaded={bundleLoader.NumCatalogsLoaded} catalogCurrentlyLoading={bundleLoader.CatalogCurrentlyLoading}");
        }

        private IEnumerator TurnOffExitMenuUI()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            var menus = FindObjectsByType<ExitPromptMenu>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var m in menus)
                m.gameObject.SetActive(false);
        }

        public void SetInputFieldTextAndSubmit(string input)
        {
            if (m_demoControllerUI == null) return;
            m_demoControllerUI.InputFieldText = input;
            m_demoControllerUI.SubmitInputTextField();
        }

        public void ShowDebugTextInUI(string text)
        {
            m_demoControllerUI.PopulateResponseUI("You", text);            
        }

        private void OnApplicationQuit()
        {
            if (m_currentASR != null)
            {
                if (m_currentASR.IsRecognizing)
                    m_currentASR.StopRecognizing();
            }

            if (m_windowsSpeechRecognitionSystem != null)
                m_windowsSpeechRecognitionSystem.SpeechRecognized -= OnSpeechRecognized;
            if (m_azureSpeechRecognitionSystem != null)
                m_azureSpeechRecognitionSystem.SpeechRecognized -= OnSpeechRecognized;
            DisableMicrophones();
        }
    }
}
