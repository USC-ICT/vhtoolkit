using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Manages various debug menus for controlling different aspects of the demo,
    /// such as animation, sensing, timeline, speech recognition, and more.
    /// </summary>
    public class DebugMenus : RideMonoBehaviour
    {
        [Header("Debug Menus")]
        [SerializeField] DebugMenuAnimation m_animation;
        [SerializeField] DebugMenuCCAnimation m_ccAnimation;
        [SerializeField] DebugMenuASR m_asr;
        [SerializeField] DebugMenuFace m_face;
        [SerializeField] DebugMenuGaze m_gaze;
        [SerializeField] DebugMenuLipsync m_lipsync;
        [SerializeField] DebugMenuSensing m_sensing;
        [SerializeField] DebugMenuNLP m_nlp;
        [SerializeField] DebugMenuTimeline m_timeline;
        [SerializeField] DebugMenuTTS m_tts;
        [SerializeField] Camera m_camera;
        [SerializeField] DemoControllerBase m_controller;

        [Header("UI")]
        [SerializeField] Transform m_uiWebcam;
        [SerializeField] Transform m_uiInputField;
        [SerializeField] Transform m_uiChatHistory;
        [SerializeField] private GameObject m_startScreenCanvasWidescreen;
        [SerializeField] private GameObject m_startScreenCanvasPortrait;


        #region Debug menu variables
        DebugMenu m_debugMenu;
        [NonSerialized] public GUIStyle m_guiButtonLeftJustify;
        [NonSerialized] public GUIStyle m_guiToggleLeftJustify;
        private Texture2D m_guiToggleLeftJustifyTransparentTexture = null;

        bool m_menuToggle = false;
        bool m_settingsToggle = false;
        Vector2 m_settingsScroll;
        int m_selectedLightingIndex = -1;
        List<GameObject> m_lightingChoices;
        const string lightingPrefix = "LightingConfig-";
        DebugOnScreenLogVHAssets m_onScreenLog;
        Vector2 m_dialogScroll;
        Vector2 m_scroll;
        bool m_fps60LockToggle;
        bool m_characterSelectionToggle = true;
        bool m_characterToggle_Ride = false;
        bool m_characterToggle_Rocketbox = false;
        bool m_characterToggle_CC = false;
        bool m_sensingToggle = false;
        bool m_asrToggle = false;                
        bool m_nlpToggle = false;        
        bool m_ttsToggle = false;
        bool m_lipsyncToggle = false;
        bool m_unifiedToggle = false;
        bool m_inputoutputToggle = false;
        bool m_miscToggle = false;
        bool m_toggleUI_webcam = true;
        bool m_toggleUI_inputField = true;
        bool m_toggleUI_chatHistory = true;
        string m_nlpInput = "Hello, how are you?";
        string m_nlpResult = "I'm fine, how are you?";
        [NonSerialized] public Vector3 m_cameraInitialPosition;
        [NonSerialized] public Quaternion m_cameraInitialRotation;
        #endregion

        // Switch to the portrait welcome-screen layout when the current screen aspect ratio is narrower than this threshold.
        private const float PortraitStartScreenAspectThreshold = 1f;
        private bool m_usePortraitStartScreenLayout;

        bool DisableWebcamUiForPlatform => RideUtils.IsWebGL();


        /// <summary>
        /// Initializes the debug menu, sets default UI configurations, and retrieves system references.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            // Get references to DebugMenu, DemoController, and OnScreenLog systems.
            m_debugMenu = Systems.Get<DebugMenu>();
            if (m_controller == null)
                m_controller = FindAnyObjectByType<DemoControllerBase>();
            m_onScreenLog = Systems.Get<DebugOnScreenLogVHAssets>();

            // Insert various debug menu categories for non XR projects.
#if !RIDEVH_XR
            m_debugMenu.InsertMenu(0, "Overview", OnGUIVHDemo);
            m_debugMenu.InsertMenu(1, "Main", OnGUIMain);
            //m_debugMenu.InsertMenu(2, "NLP", OnGUINLP);            
            if (m_animation != null) m_debugMenu.InsertMenu(2, "Animation", m_animation.OnGUIAnimation);
            if (m_face != null) m_debugMenu.InsertMenu(3, "Face", m_face.OnGUIFace);
            if (m_gaze != null) m_debugMenu.InsertMenu(4, "Gaze", m_gaze.OnGUIGaze);
            if (m_sensing != null) m_debugMenu.InsertMenu(5, "Sensing", m_sensing.OnGUISensing);
            //m_debugMenu.InsertMenu(6, "Timeline", m_timeline.OnGUITimeline);
            //m_debugMenu.InsertMenu(7, "CC Animation", m_ccAnimation.OnGUICCAnimation);

            m_debugMenu.SetMenu(0);
            m_debugMenu.ShowMenu(true);

            if (RideUtils.IsAndroid() || RideUtils.IsIOS())
            {
                m_debugMenu.SetMenuSize(0, 0, 0.3f, .90f);
                m_debugMenu.SetWideMenuSize(0, 0, 0.4f, 1f);
            }
            else
            {
                m_debugMenu.SetMenuSize(0, 0, 0.3f, 1f);
                m_debugMenu.SetWideMenuSize(0, 0, 0.4f, 1f);
            }
#endif

            // Initialize the main camera if not assigned.
            if (m_camera == null)
                m_camera = Camera.main;

            m_cameraInitialPosition = m_camera.transform.localPosition;
            m_cameraInitialRotation = m_camera.transform.localRotation;
            m_toggleUI_webcam = m_uiWebcam != null && m_uiWebcam.gameObject.activeSelf;
            m_toggleUI_inputField = m_uiInputField != null && m_uiInputField.gameObject.activeSelf;
            m_toggleUI_chatHistory = m_uiChatHistory != null && m_uiChatHistory.gameObject.activeSelf;
            m_fps60LockToggle = Application.targetFrameRate == 60;
            if (DisableWebcamUiForPlatform && m_uiWebcam != null)
            {
                m_toggleUI_webcam = false;
                m_uiWebcam.gameObject.SetActive(false);
            }

            // Find all lighting configurations
            var allGameobjects = FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            m_lightingChoices = allGameobjects
                        .Select(t => t.gameObject)
                        .Where(go => go.name.StartsWith(lightingPrefix, StringComparison.Ordinal))
                        .Distinct()
                        .ToList();
            int activeIndex = m_lightingChoices.FindIndex(g => g.activeSelf);
            m_selectedLightingIndex = activeIndex >= 0 ? activeIndex : 0;

            // Show start screen on WebGL builds
            if (RideUtils.IsWebGL() || RideUtils.IsIOS() || RideUtils.IsAndroid())
                EnableStartScreen();
            else
                DisableStartScreen();
        }

        protected override void Update()
        {
            base.Update();

            UpdateStartScreenLayout();

            if (!RideUtils.IsIOS() && !RideUtils.IsAndroid())
            {
                var toggleKey = RideUtils.IsWebGL() ? KeyCode.F9 : KeyCode.F11;
                if (Input.GetKeyDown(toggleKey))
                    m_debugMenu.ShowMenu(!m_debugMenu.IsShowing());
            }
        }

        /// <summary>
        /// Handles GUI layout for the Virtual Human demo tab in the debug menu.
        /// Provides options for UI elements, settings, and camera reset.
        /// </summary>
        void OnGUIVHDemo()
        {
            OnGUICustomStylesSetup();

            m_menuToggle = GUILayout.Toggle(m_menuToggle, m_menuToggle ? $"- <b>VHToolkit Demo Instructions</b>" : $"+ <b>VHToolkit Demo Instructions</b>", m_guiToggleLeftJustify);

            if (m_menuToggle)
            {
                m_debugMenu.Space();
                m_debugMenu.Label($"Interaction:");
                m_debugMenu.Label($"\u2022 Type text below and hit Enter / Return or click Send");
                m_debugMenu.Label($"\u2022 Click Use Mic to toggle speech recognition");
                m_debugMenu.Label($"\u2022 Click Next Character to cycle through characters");
                m_debugMenu.Label($"\u2022 Click Toggle Webcam to turn sensing on/off");
                m_debugMenu.Label($"\u2022 Click Stop to halt character behaviors");
                m_debugMenu.Space();
                m_debugMenu.Label($"Debug functionality:");
                m_debugMenu.Label($"\u2022 Click < and > above to cycle through debug menus");
                m_debugMenu.Label($"\u2022 In the Main debug menu, select a character and its");
                m_debugMenu.Label($"  Sensing, ASR, NLP, and TTS technologies");
                m_debugMenu.Label($"\u2022 Click <> to toggle debug menu width");
                m_debugMenu.Label($"\u2022 Click >> to toggle debug log");
                var toggleKeyLabel = RideUtils.IsWebGL() ? "F9" : "F11";
                m_debugMenu.Label($"\u2022 Press {toggleKeyLabel} to toggle this debug menu on/off");
                m_debugMenu.Label($"\u2022 Press J to toggle mouse look on/off; move the");
                m_debugMenu.Label($"  camera with the arrow or W, A, S, D keys");
                m_debugMenu.Space();
                m_debugMenu.Space();

                if (m_debugMenu.Button("Hide Window"))
                    m_debugMenu.ToggleMenu();

                if (m_miscToggle)
                    OnGUIIntroduceToggle();

                // Settings toggle button.
                m_settingsToggle = GUILayout.Toggle(m_settingsToggle, m_settingsToggle ? $"- Settings" : $"+ Settings", m_guiToggleLeftJustify);
                if (m_settingsToggle)
                {
                    using (var settingsScroll = new GUILayout.ScrollViewScope(m_settingsScroll))
                    {
                        m_settingsScroll = settingsScroll.scrollPosition;

                        var onScreenLog = m_debugMenu.Toggle(m_onScreenLog.m_log.IsShowing, m_onScreenLog.m_log.IsShowing ? "OnScreenDebugLog ON" : "OnScreenDebugLog OFF");
                        if (onScreenLog != m_onScreenLog.m_log.IsShowing)
                            m_onScreenLog.m_log.ShowLog(!m_onScreenLog.m_log.IsShowing);

                        // Toggle UI elements.
                        bool oldGuiEnabled = GUI.enabled;
                        if (DisableWebcamUiForPlatform)
                            GUI.enabled = false;

                        var webcamEnabled = m_debugMenu.Toggle(m_toggleUI_webcam, m_toggleUI_webcam ? "Webcam UI ON" : "Webcam UI OFF");
                        if (webcamEnabled != m_toggleUI_webcam)
                        {
                            m_toggleUI_webcam = webcamEnabled;
                            m_uiWebcam.gameObject.SetActive(m_toggleUI_webcam);
                        }

                        GUI.enabled = oldGuiEnabled;

                        var inputFieldEnabled = m_debugMenu.Toggle(m_toggleUI_inputField, m_toggleUI_inputField ? "Input Field UI ON" : "Input Field UI OFF");
                        if (inputFieldEnabled != m_toggleUI_inputField)
                        {
                            m_toggleUI_inputField = inputFieldEnabled;
                            m_uiInputField.gameObject.SetActive(m_toggleUI_inputField);
                        }

                        var chatHistoryEnabled = m_debugMenu.Toggle(m_toggleUI_chatHistory, m_toggleUI_chatHistory ? "Chat History UI ON" : "Chat History OFF");
                        if (chatHistoryEnabled != m_toggleUI_chatHistory)
                        {
                            m_toggleUI_chatHistory = chatHistoryEnabled;
                            m_uiChatHistory.gameObject.SetActive(m_toggleUI_chatHistory);
                        }

                        // Toggle FPS lock.
                        var fps60LockEnabled = m_debugMenu.Toggle(m_fps60LockToggle, m_fps60LockToggle ? "Locked at 60fps" : "Unlocked frame rate");
                        if (fps60LockEnabled != m_fps60LockToggle)
                        {
                            m_fps60LockToggle = fps60LockEnabled;
                            Application.targetFrameRate = m_fps60LockToggle ? 60 : -1;
                        }

                        // Reset camera button.
                        if (m_debugMenu.Button("Reset Camera"))
                            m_camera.transform.SetLocalPositionAndRotation(m_cameraInitialPosition, m_cameraInitialRotation);

                        // Lighting. Note that if the parent of a lighting configuration is inactive, enabling that lighting configuration will have no effect
                        m_debugMenu.Space();
                        m_debugMenu.Label("<b>Lighting</b>");
                        m_debugMenu.Label("These lighting configurations are found in the scene dynamically. " +
                            "If not part of the active environment, selecting them will have no effect. ");

                        if (m_lightingChoices.Count == 0)
                        {
                            m_debugMenu.Label("No LightingConfig- objects found in scene.");
                        }
                        else
                        {
                            for (int i = 0; i < m_lightingChoices.Count; i++)
                            {
                                var go = m_lightingChoices[i];

                                string display = go.name.Length > lightingPrefix.Length ? go.name.Substring(lightingPrefix.Length) : go.name;
                                bool isSelected = m_selectedLightingIndex == i;
                                bool toggled = m_debugMenu.Toggle(isSelected, display);
                                if (toggled && !isSelected)
                                {
                                    m_selectedLightingIndex = i;

                                    // Disable all, enable only the selected
                                    foreach (var g in m_lightingChoices)
                                        g.SetActive(false);

                                    go.SetActive(true);
                                }
                            }
                        }
                    }
                }
            }

            using (new GUILayout.HorizontalScope())
            {
                m_debugMenu.Label("Quality:", 110);
                if (m_debugMenu.Button($"{QualitySettings.names[QualitySettings.GetQualityLevel()]}"))
                    QualitySettings.SetQualityLevel((QualitySettings.GetQualityLevel() + 1) % QualitySettings.names.Length, true);
            }
        }

        /// <summary>
        /// Handles GUI layout for the main VH configuration tab in the debug menu.
        /// Includes ASR, TTS, sensing, and input/output settings.
        /// </summary>
        void OnGUIMain()
        {
            OnGUICustomStylesSetup();

            using (var dialogScrollView = new GUILayout.ScrollViewScope(m_dialogScroll))
            {
                m_dialogScroll = dialogScrollView.scrollPosition;

                using (m_debugMenu.Horizontal())
                {
                    if (m_debugMenu.Button("Collapse")) { m_characterSelectionToggle = false; m_sensingToggle = false; m_asrToggle = false; m_nlpToggle = false; m_ttsToggle = false; m_lipsyncToggle = false; m_unifiedToggle = false; m_inputoutputToggle = false; m_miscToggle = false; }
                    if (m_debugMenu.Button("Expand")) { m_characterSelectionToggle = true; m_sensingToggle = true; m_asrToggle = true; m_nlpToggle = true; m_ttsToggle = true; m_lipsyncToggle = true; m_unifiedToggle = true; m_inputoutputToggle = true; m_miscToggle = true; }
                }

                OnGUICharacterConfig();

                bool oldGuiEnabled = GUI.enabled;
                if (DisableWebcamUiForPlatform)
                    GUI.enabled = false;

                m_sensingToggle = GUILayout.Toggle(m_sensingToggle, m_sensingToggle ? $"- <b>Sensing</b>" : $"+ <b>Sensing</b>", m_guiToggleLeftJustify);
                if (m_sensingToggle) m_sensing.OnGUISelectSensingMode();

                GUI.enabled = oldGuiEnabled;

                m_asrToggle = GUILayout.Toggle(m_asrToggle, m_asrToggle ? $"- <b>Automated Speech Recognition (ASR)</b>" : $"+ <b>ASR</b>", m_guiToggleLeftJustify);
                if (m_asrToggle) m_asr.OnGUISystemSelection();

                m_nlpToggle = GUILayout.Toggle(m_nlpToggle, m_nlpToggle ? $"- <b>Natural Language Processing (NLP)</b>" : $"+ <b>NLP</b>", m_guiToggleLeftJustify);
                if (m_nlpToggle) { m_nlp.OnGUISystemSelection(); m_nlp.OnGUIPrompt(); m_nlp.OnGUIDynamicLanguageDetection(); }

                m_ttsToggle = GUILayout.Toggle(m_ttsToggle, m_ttsToggle ? $"- <b>Text-To-Speech (TTS)</b>" : $"+ <b>TTS</b>", m_guiToggleLeftJustify);
                if (m_ttsToggle) { m_tts.OnGUISystemSelection(); m_tts.OnGUIVoiceSelection(); }

                m_lipsyncToggle = GUILayout.Toggle(m_lipsyncToggle, m_lipsyncToggle ? $"- <b>Nonverbal Behavior (NVB)</b>" : $"+ <b>NVB</b>", m_guiToggleLeftJustify);
                if (m_lipsyncToggle) m_lipsync.OnGUISystemSelection();

                m_unifiedToggle = GUILayout.Toggle(m_unifiedToggle, m_unifiedToggle ? $"- <b>Unified Cognition</b>" : $"+ <b>Unified Cognition</b>", m_guiToggleLeftJustify);
                if (m_unifiedToggle) OnGUIUnifiedSelection();

                m_inputoutputToggle = GUILayout.Toggle(m_inputoutputToggle, m_inputoutputToggle ? $"- <b>Input / Output</b>" : $"+ <b>Input / Output</b>", m_guiToggleLeftJustify);
                if (m_inputoutputToggle) { OnGUIInput(); OnGUIStopUtterance(); OnGUIOutput(); }

                m_miscToggle = GUILayout.Toggle(m_miscToggle, m_miscToggle ? $"- <b>Miscellaneous</b>" : $"+ <b>Miscellaneous</b>", m_guiToggleLeftJustify);
                if (m_miscToggle) { OnGUIIntroduceToggle(); }
            }
        }

        /// <summary>
        /// Toggles whether characters introduce themselves when first loaded
        /// </summary>
        public void OnGUIIntroduceToggle()
        {
            m_controller.IntroduceOnLoad = m_debugMenu.Toggle(m_controller.IntroduceOnLoad, m_controller.IntroduceOnLoad ? "VH Introduction on Load ON" : "VH Introduction on Load OFF");
        }

        /// <summary>
        /// Stops the current utterance being spoken.
        /// </summary>
        public void OnGUIStopUtterance()
        {
            if (m_debugMenu.Button("Stop"))
                m_controller.StopUtterance();
        }

        /// <summary>
        /// Displays the character selection menu in the debug interface.
        /// </summary>
        public void OnGUICharacterConfig()
        {
            m_characterSelectionToggle = GUILayout.Toggle(m_characterSelectionToggle, m_characterSelectionToggle ? $"- <b>Character</b>" : $"+ <b>Character</b>", m_guiToggleLeftJustify);
            if (!m_characterSelectionToggle)
                return;

            if (!m_controller.m_characterConfigUIEnabled)
                GUI.enabled = false;

            var ictCharacters = m_controller.CharactersParent.Find("ICT").GetComponentsInChildren<MecanimCharacter>(true);
            var rbCharacters = m_controller.CharactersParent.Find("Rocketbox").GetComponentsInChildren<MecanimCharacter>(true);
            var ccCharacters = m_controller.CharactersParent.Find("CC").GetComponentsInChildren<MecanimCharacter>(true);

            if (ictCharacters.Length > 0)
            {
                m_characterToggle_Ride = GUILayout.Toggle(m_characterToggle_Ride, m_characterToggle_Ride ? $"- <b>ICT</b>" : $"+ <b>ICT</b>", m_guiToggleLeftJustify);
                if (m_characterToggle_Ride) { DrawCharacterGroup("ICT", ictCharacters); }
            }

            if (rbCharacters.Length > 0)
            {
                m_characterToggle_Rocketbox = GUILayout.Toggle(m_characterToggle_Rocketbox, m_characterToggle_Rocketbox ? $"- <b>Rocketbox</b>" : $"+ <b>Rocketbox</b>", m_guiToggleLeftJustify);
                if (m_characterToggle_Rocketbox) { DrawCharacterGroup("Rocketbox", rbCharacters); }
            }

            if (ccCharacters.Length > 0)
            {
                m_characterToggle_CC = GUILayout.Toggle(m_characterToggle_CC, m_characterToggle_CC ? $"- <b>CC</b>" : $"+ <b>CC</b>", m_guiToggleLeftJustify);
                if (m_characterToggle_CC) { DrawCharacterGroup("CC", ccCharacters); }
            }

            GUI.enabled = true;

            DemoController demoController = m_controller as DemoController;
            if (demoController != null && demoController.CharacterLoadPending)
            {
                m_debugMenu.Space();
                m_debugMenu.Label("<b>Character Load Status</b>");

                if (!string.IsNullOrEmpty(demoController.PendingCharacterName))
                    m_debugMenu.Label($"Loading: {demoController.PendingCharacterName}");

                string status = demoController.PendingCharacterStatus;
                if (!string.IsNullOrEmpty(status))
                    m_debugMenu.Label(status);

                int percent = Mathf.Clamp(Mathf.RoundToInt(demoController.PendingCharacterProgress * 100f), 0, 100);
                m_debugMenu.Label($"Progress: {percent}%");

                if (m_debugMenu.Button("Cancel Character Load"))
                    demoController.CancelCharacterLoad();
            }

            //Draw line
            m_debugMenu.Space();
            Rect rect = GUILayoutUtility.GetRect(1, 1, GUILayout.ExpandWidth(true));
            GUI.DrawTexture(rect, Texture2D.whiteTexture);
            m_debugMenu.Space();
        }

        /// <summary>
        /// Displays a selection grid for choosing a character from the available list.
        /// </summary>
        // Longest character name shown on a selection button before truncation, per menu width.
        const int CharacterNameCharsNormal = 20;
        const int CharacterNameCharsWide = 30;

        void DrawCharacterGroup(string label, MecanimCharacter[] group)
        {
            if (group.Length == 0) return;

            //m_debugMenu.Label(label);

            string[] names = group.Select(c => c.name).ToArray();
            int currentIndex = Array.FindIndex(names, name =>
                m_controller.CurrentCharacter != null && m_controller.CurrentCharacter.name == name);

            // Buttons show a truncated label so long names cannot stretch the menu;
            // selection still uses the full name.
            int maxChars = m_debugMenu.IsWideMode() ? CharacterNameCharsWide : CharacterNameCharsNormal;
            string[] displayNames = names.Select(n =>
                n.Length <= maxChars ? n : n.Substring(0, maxChars) + "...").ToArray();

            int selectedIndex = m_debugMenu.SelectionGrid(currentIndex, displayNames, 2);
            if (selectedIndex != currentIndex && selectedIndex >= 0)
                m_controller.SelectCharacter(names[selectedIndex]);

            m_debugMenu.Space();
        }

        /// <summary>
        /// Handles GUI elements related to user input (microphone and text input).
        /// </summary>
        void OnGUIInput()
        {
            if (m_controller.m_currentASR == null)
            {
                GUI.enabled = false;
                m_debugMenu.Button("No ASR Provider");
                GUI.enabled = true;
                return;
            }

            if (m_controller.m_currentASR.SelectedMicrophone == string.Empty)
            {
                GUI.enabled = false;
                m_debugMenu.Button("No Detected Microphone");
                GUI.enabled = true;
            }
            else
            {
                var voice = m_controller.CurrentCharacter != null ? m_controller.CurrentCharacter.Voice : null;

                if (m_controller.m_currentASR.IsRecognizing)
                {
                    if (m_debugMenu.Button("<color=red>Stop</color>"))
                        m_controller.SetASR(false);
                }
                else if (voice != null && voice.isPlaying)
                {
                    // Don't allow user to use asr if VH is talking
                    GUI.enabled = false;
                    m_debugMenu.Button("Speak with Microphone");
                    GUI.enabled = true;
                }
                else
                {
                    if (m_debugMenu.Button("Speak with Microphone"))
                        m_controller.SetASR(true);
                }
            }

            GUI.SetNextControlName("NLPInput");
            m_nlpInput = m_debugMenu.TextField(m_nlpInput);
            if (m_debugMenu.Button("Send"))
                m_controller.AskNLPQuestion(m_nlpInput);
        }

        /// <summary>
        /// Displays the output of NLP responses and allows repeating them.
        /// </summary>
        void OnGUIOutput()
        {
            m_debugMenu.Label($"Result:");
            m_nlpResult = m_debugMenu.TextArea(m_nlpResult);

            if (m_debugMenu.Button("Repeat Response"))
                m_controller.SendResponse(m_nlpResult);
        }

        /// <summary>
        /// Displays GUI elements for NLP-related functions.
        /// </summary>
        void OnGUINLP()
        {
            using (var vhScrollView = new GUILayout.ScrollViewScope(m_scroll))
            {
                m_scroll = vhScrollView.scrollPosition;

                m_nlp.OnGUILlm();
                m_tts.OnGUITts();
                m_asr.OnGUIAsr();
                m_lipsync.OnGUILipsync();
            }
        }

        /// <summary>
        /// Sets up custom GUI styles for buttons and toggles.
        /// </summary>
        public void OnGUICustomStylesSetup()
        {
            // taken from DebugMenu, specialty case button, left justified
            if (m_guiButtonLeftJustify == null)
            {
                m_guiButtonLeftJustify = new GUIStyle(GUI.skin.button);
                m_guiButtonLeftJustify.alignment = TextAnchor.MiddleLeft;
            }

            int fontSize = (int)(22.0f * ((float)Screen.height / 1080f));
            m_guiButtonLeftJustify.fontSize = fontSize;

            if (m_guiToggleLeftJustify == null)
            {
                m_guiToggleLeftJustify = new GUIStyle(GUI.skin.button);
                m_guiToggleLeftJustify.alignment = TextAnchor.MiddleLeft;

                m_guiToggleLeftJustifyTransparentTexture = new Texture2D(1, 1);
                m_guiToggleLeftJustifyTransparentTexture.SetPixel(0, 0, new Color(0, 0, 0, 0));
                m_guiToggleLeftJustifyTransparentTexture.Apply();

                m_guiToggleLeftJustify.normal.background = m_guiToggleLeftJustifyTransparentTexture;      // Remove the background for the normal state
                m_guiToggleLeftJustify.onNormal.background = m_guiToggleLeftJustifyTransparentTexture;    // Remove the background for the toggled (on) state
            }

            m_guiToggleLeftJustify.fontSize = fontSize;

#if UNITY_ANDROID || UNITY_IOS
            GUI.skin.verticalScrollbar.fixedWidth = 30f;
            GUI.skin.verticalScrollbarThumb.fixedWidth = 30f;

            GUI.skin.horizontalScrollbar.fixedWidth = 30f;
            GUI.skin.horizontalScrollbarThumb.fixedWidth = 30f;
#endif
        }

        /// <summary>
        /// Displays Unified Cognition debug functionality.
        /// </summary>
        public void OnGUIUnifiedSelection()
        {
            if (m_controller == null)
                m_controller = FindAnyObjectByType<DemoControllerBase>();

            if (m_controller == null)
            {
                m_debugMenu.Label("No demo controller found.");
                m_debugMenu.Space();
                return;
            }

            bool oldGuiEnabled = GUI.enabled;
            GUI.enabled = true;

            m_debugMenu.Label($"Conversation mode: {(m_controller.UsingRealtimeConversationMode ? "Unified" : "Classic")}");
            if (m_debugMenu.Button(m_controller.UsingRealtimeConversationMode ? "Switch to Classic" : "Switch to OpenAI Realtime (Unified AI)"))
            {
                m_controller.ChangeConversationMode(
                    m_controller.UsingRealtimeConversationMode
                        ? DemoControllerBase.ConversationMode.Classic
                        : DemoControllerBase.ConversationMode.Unified);
            }

            GUI.enabled = oldGuiEnabled;

            m_debugMenu.Space();
        }

        public void SetNlpInput(string input)
        {
            m_nlpInput = input;
        }

        public void SetNlpResponse(string response)
        {
            m_nlpResult = response;
        }

        void EnableStartScreen()
        {
            m_controller.m_startButtonPressed = false;
            m_debugMenu.ShowMenu(false);
            UpdateStartScreenLayout(true);
            ApplyStartScreenVisibility(true);
        }

        void DisableStartScreen()
        {
            m_controller.m_startButtonPressed = true;
            m_debugMenu.ShowMenu(true);
            ApplyStartScreenVisibility(false);
        }

        /// <summary>
        /// Updates the start screen layout selection based on the current screen aspect ratio.
        /// Reapplies visibility when the active layout changes or a refresh is forced.
        /// </summary>
        /// <param name="force">True to refresh the active start screen layout even if the selected layout has not changed.</param>
        private void UpdateStartScreenLayout(bool force = false)
        {
            bool usePortraitLayout = ShouldUsePortraitStartScreenLayout();
            if (!force && usePortraitLayout == m_usePortraitStartScreenLayout)
                return;

            m_usePortraitStartScreenLayout = usePortraitLayout;
            ApplyStartScreenVisibility(m_controller != null && m_controller.m_startButtonPressed == false);
        }

        /// <summary>
        /// Determines whether the portrait start screen layout should be used for the current screen dimensions.
        /// </summary>
        /// <returns>True when the current aspect ratio is below the portrait layout threshold; otherwise, false.</returns>
        private bool ShouldUsePortraitStartScreenLayout()
        {
            if (m_startScreenCanvasPortrait == null || Screen.height <= 0)
                return false;

            float aspectRatio = (float)Screen.width / Screen.height;
            return aspectRatio < PortraitStartScreenAspectThreshold;
        }

        /// <summary>
        /// Applies the start screen visibility state to the widescreen and portrait layout roots.
        /// Activates only the layout that matches the current aspect ratio selection.
        /// </summary>
        /// <param name="isVisible">True to show the active start screen layout; otherwise, false.</param>
        private void ApplyStartScreenVisibility(bool isVisible)
        {
            if (m_startScreenCanvasWidescreen != null)
                m_startScreenCanvasWidescreen.SetActive(isVisible && !m_usePortraitStartScreenLayout);

            if (m_startScreenCanvasPortrait != null)
                m_startScreenCanvasPortrait.SetActive(isVisible && m_usePortraitStartScreenLayout);
        }

        public void OnClickStart() => DisableStartScreen();
    }
}
