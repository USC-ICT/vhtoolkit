using System;
using System.Linq;
using UnityEngine;
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
        [SerializeField] DebugMenuLLM m_llm;
        [SerializeField] DebugMenuTimeline m_timeline;
        [SerializeField] DebugMenuTTS m_tts;
        [SerializeField] Camera m_camera;

        [Header("UI")]
        [SerializeField] Transform m_uiWebcam;
        [SerializeField] Transform m_uiInputField;
        [SerializeField] Transform m_uiChatHistory;


        #region Debug menu variables
        DebugMenu m_debugMenu;
        DemoControllerBase m_controller;
        [NonSerialized] public GUIStyle m_guiButtonLeftJustify;
        [NonSerialized] public GUIStyle m_guiToggleLeftJustify;
        bool m_settingsToggle = false;
        DebugOnScreenLogVHAssets m_onScreenLog;
        Vector2 m_dialogScroll;
        Vector2 m_scroll;
        bool m_fps60LockToggle;
        bool m_characterSelectionToggle = true;
        bool m_characterToggle_Ride = false;
        bool m_characterToggle_Rocketbox = false;
        bool m_asrToggle = true;
        bool m_ttsToggle = true;
        bool m_lipsyncToggle = true;
        bool m_llmToggle = true;
        bool m_sensingToggle = true;
        bool m_inputoutputToggle = true;
        bool m_toggleUI_webcam = true;
        bool m_toggleUI_inputField = true;
        bool m_toggleUI_chatHistory = true;
        string m_nlpInput = "Hello, how are you?";
        string m_nlpResult = "I'm fine, how are you?";
        Vector3 m_cameraInitialPosition;
        Quaternion m_cameraInitialRotation;
        #endregion


        /// <summary>
        /// Initializes the debug menu, sets default UI configurations, and retrieves system references.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            // Get references to DebugMenu, DemoController, and OnScreenLog systems.
            m_debugMenu = Globals.api.GetSystem<DebugMenu>();
            m_controller = FindAnyObjectByType<DemoControllerBase>();
            m_onScreenLog = Globals.api.GetSystem<DebugOnScreenLogVHAssets>();

            // Insert various debug menu categories.
            m_debugMenu.InsertMenu(0, "Overview", OnGUIVHDemo);
            m_debugMenu.InsertMenu(1, "Main", OnGUIDialog);
            //m_debugMenu.InsertMenu(2, "NLP", OnGUINLP);
            m_debugMenu.InsertMenu(2, "Animation", m_animation.OnGUIAnimation);
            m_debugMenu.InsertMenu(3, "Face", m_face.OnGUIFace);
            m_debugMenu.InsertMenu(4, "Gaze", m_gaze.OnGUIGaze);
            m_debugMenu.InsertMenu(5, "Sensing", m_sensing.OnGUISensing);
            m_debugMenu.InsertMenu(6, "Timeline", m_timeline.OnGUITimeline);
            //m_debugMenu.InsertMenu(7, "CC Animation", m_ccAnimation.OnGUICCAnimation);
            //m_debugMenu.InsertDebugMenu(9, "OVR Lipsync", m_ovr.OnGuiOvrLipsync);
            //m_debugMenu.InsertDebugMenu(9, "OVR Lipsync", m_ovr.OnGuiOvrLipsync);

            m_debugMenu.SetMenu(0);
            m_debugMenu.ShowMenu(true);
            m_debugMenu.SetMenuSize(0, 0, 0.3f, 1f);
            m_debugMenu.SetWideMenuSize(0, 0, 0.4f, 1f);

            // Initialize the main camera if not assigned.
            if (m_camera == null)
                m_camera = Camera.main;

            m_cameraInitialPosition = m_camera.transform.localPosition;
            m_cameraInitialRotation = m_camera.transform.localRotation;
        }

        protected override void Update()
        {
            base.Update();
            //if (Input.GetKeyDown(KeyCode.F11))
            //    m_debugMenu.ShowMenu(!m_debugMenu.IsShowing());
        }


        /// <summary>
        /// Handles GUI layout for the Virtual Human demo tab in the debug menu.
        /// Provides options for UI elements, settings, and camera reset.
        /// </summary>
        void OnGUIVHDemo()
        {
            OnGUICustomStylesSetup();

            m_debugMenu.Label($"<b>Virtual Human Toolkit Demo</b>");
            m_debugMenu.Space();
            m_debugMenu.Label($"Interaction:");
            m_debugMenu.Label($"• Type in text below and hit Enter or click Send");
            m_debugMenu.Label($"• Alternatively, click Use Mic to toggle speech recognition");
            m_debugMenu.Label($"• Click Toggle Webcam to turn sensing on/off");
            m_debugMenu.Label($"• Click Stop to halt all character behaviors");
            m_debugMenu.Space();
            m_debugMenu.Label($"Debug functionality:");
            m_debugMenu.Label($"• Click '<' and '>' above to cycle through debug menus");
            m_debugMenu.Label($"• In the Main debug menu, select the Character and its");
            m_debugMenu.Label($"  Sensing, ASR, NLP, TTS, and lipsync technologies");
            m_debugMenu.Label($"• Click '<>' to toggle debug menu width");
            m_debugMenu.Label($"• Click '>>' to toggle debug log");
            m_debugMenu.Label($"• Press F11 to toggle this debug menu on/off");
            m_debugMenu.Label($"• Press J to toggle mouse look on/off; move the camera");
            m_debugMenu.Label($"  with the arrow keys");
            m_debugMenu.Space();
            m_debugMenu.Space();

            if (m_debugMenu.Button("Hide Window"))
                m_debugMenu.ToggleMenu();

            // Settings toggle button.
            m_settingsToggle = GUILayout.Toggle(m_settingsToggle, m_settingsToggle ? $"- Settings" : $"+ Settings", m_guiToggleLeftJustify);
            if (m_settingsToggle)
            {
                var onScreenLog = m_debugMenu.Toggle(m_onScreenLog.m_log.IsShowing, m_onScreenLog.m_log.IsShowing ? "OnScreenDebugLog ON" : "OnScreenDebugLog OFF");
                if (onScreenLog != m_onScreenLog.m_log.IsShowing)
                    m_onScreenLog.m_log.ShowLog(!m_onScreenLog.m_log.IsShowing);

                // Toggle UI elements.
                m_toggleUI_webcam = m_debugMenu.Toggle(m_toggleUI_webcam, m_toggleUI_webcam ? "Webcam UI ON" : "Webcam UI OFF");
                m_uiWebcam.gameObject.SetActive(m_toggleUI_webcam);

                m_toggleUI_inputField = m_debugMenu.Toggle(m_toggleUI_inputField, m_toggleUI_inputField ? "Input Field UI ON" : "Input Field UI OFF");
                m_uiInputField.gameObject.SetActive(m_toggleUI_inputField);

                m_toggleUI_chatHistory = m_debugMenu.Toggle(m_toggleUI_chatHistory, m_toggleUI_chatHistory ? "Chat History UI ON" : "Chat History OFF");
                m_uiChatHistory.gameObject.SetActive(m_toggleUI_chatHistory);

                // Toggle FPS lock.
                m_fps60LockToggle = m_debugMenu.Toggle(m_fps60LockToggle, Application.targetFrameRate == 60 ? "Locked at 60fps" : "Unlocked frame rate");
                Application.targetFrameRate = m_fps60LockToggle ? 60 : -1;

                // Reset camera button.
                if (m_debugMenu.Button("Reset Camera"))
                    m_camera.transform.SetLocalPositionAndRotation(m_cameraInitialPosition, m_cameraInitialRotation);
            }
        }


        /// <summary>
        /// Handles GUI layout for the dialog management tab in the debug menu.
        /// Includes ASR, TTS, sensing, and input/output settings.
        /// </summary>
        void OnGUIDialog()
        {
            OnGUICustomStylesSetup();

            using (var dialogScrollView = new GUILayout.ScrollViewScope(m_dialogScroll))
            {
                m_dialogScroll = dialogScrollView.scrollPosition;

                OnGUICharacterConfig();
                m_sensingToggle = GUILayout.Toggle(m_sensingToggle, m_sensingToggle ? $"- <b>Sensing</b>" : $"+ <b>Sensing</b>", m_guiToggleLeftJustify);
                if (m_sensingToggle) { m_sensing.OnGUISelectSensingMode(); }

                m_asrToggle = GUILayout.Toggle(m_asrToggle, m_asrToggle ? $"- <b>Automated Speech Recognition (ASR)</b>" : $"+ <b>ASR</b>", m_guiToggleLeftJustify);
                if (m_asrToggle) m_asr.OnGUISystemSelection();

                m_llmToggle = GUILayout.Toggle(m_llmToggle, m_llmToggle ? $"- <b>Natural Language Processing (NLP)</b>" : $"+ <b>NLP</b>", m_guiToggleLeftJustify);
                if (m_llmToggle) { m_llm.OnGUISystemSelection(); m_llm.OnGUIPrompt(); }

                m_ttsToggle = GUILayout.Toggle(m_ttsToggle, m_ttsToggle ? $"- <b>Text-To-Speech (TTS)</b>" : $"+ <b>TTS</b>", m_guiToggleLeftJustify);
                if (m_ttsToggle) { m_tts.OnGUISystemSelection(); m_tts.OnGUIVoiceSelection(); }

                m_lipsyncToggle = GUILayout.Toggle(m_lipsyncToggle, m_lipsyncToggle ? $"- <b>Lipsync</b>" : $"+ <b>Lipsync</b>", m_guiToggleLeftJustify);
                if (m_lipsyncToggle) m_lipsync.OnGUISystemSelection();

                m_inputoutputToggle = GUILayout.Toggle(m_inputoutputToggle, m_inputoutputToggle ? $"- <b>Input / Output</b>" : $"+ <b>Input / Output</b>", m_guiToggleLeftJustify);
                if (m_inputoutputToggle) { OnGUIInput(); OnGUIStopUtterance(); OnGUIOutput(); }
            }
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

            if (m_controller.CurrentCharacter != null && m_controller.CurrentCharacter.Voice.isPlaying)
                GUI.enabled = false;


            var ictCharacters = m_controller.CharactersParent.Find("ICT").GetComponentsInChildren<MecanimCharacter>(true);
            var rbCharacters = m_controller.CharactersParent.Find("Rocketbox").GetComponentsInChildren<MecanimCharacter>(true);

            m_characterToggle_Ride = GUILayout.Toggle(m_characterToggle_Ride, m_characterToggle_Ride ? $"- <b>ICT</b>" : $"+ <b>ICT</b>", m_guiToggleLeftJustify);
            if (m_characterToggle_Ride) { DrawCharacterGroup("ICT", ictCharacters); }

            m_characterToggle_Rocketbox = GUILayout.Toggle(m_characterToggle_Rocketbox, m_characterToggle_Rocketbox ? $"- <b>Rocketbox</b>" : $"+ <b>Rocketbox</b>", m_guiToggleLeftJustify);
            if (m_characterToggle_Rocketbox) { DrawCharacterGroup("Rocketbox", rbCharacters); }




            if (m_controller.CurrentCharacter != null && m_controller.CurrentCharacter.Voice.isPlaying)
                GUI.enabled = true;

            //Draw line
            m_debugMenu.Space();
            Rect rect = GUILayoutUtility.GetRect(1, 1, GUILayout.ExpandWidth(true));
            GUI.DrawTexture(rect, Texture2D.whiteTexture);
            m_debugMenu.Space();
        }


        /// <summary>
        /// Displays a selection grid for choosing a character from the available list.
        /// </summary>
        void DrawCharacterGroup(string label, MecanimCharacter[] group)
        {
            if (group.Length == 0) return;

            m_debugMenu.Label(label);

            string[] names = group.Select(c => c.name).ToArray();
            int currentIndex = Array.FindIndex(names, name =>
                m_controller.CurrentCharacter != null && m_controller.CurrentCharacter.name == name);

            int selectedIndex = m_debugMenu.SelectionGrid(currentIndex, names, 2);
            if (selectedIndex != currentIndex && selectedIndex >= 0)
            {
                m_controller.SelectCharacter(names[selectedIndex]);
                m_animation.SetAnimationList();
            }

            m_debugMenu.Space();
        }


        /// <summary>
        /// Handles GUI elements related to user input (microphone and text input).
        /// </summary>
        void OnGUIInput()
        {
            if (m_controller.m_currentASR.SelectedMicrophone == string.Empty)
            {
                GUI.enabled = false;
                m_debugMenu.Button("No Detected Microphone");
                GUI.enabled = true;
            }
            else
            {
                if (m_controller.m_currentASR.IsRecognizing)
                {
                    if (m_debugMenu.Button("<color=red>Stop</color>"))
                        m_controller.m_currentASR.StopRecognizing();
                }
                else if (m_controller.CurrentCharacter != null && m_controller.CurrentCharacter.Voice.isPlaying)
                {
                    // Don't allow user to use asr if VH is talking
                    GUI.enabled = false;
                    m_debugMenu.Button("Speak with Microphone");
                    GUI.enabled = true;
                }
                else
                {
                    if (m_debugMenu.Button("Speak with Microphone"))
                        m_controller.m_currentASR.StartRecognizing();
                }
            }

            GUI.SetNextControlName("NLPInput");
            m_nlpInput = m_debugMenu.TextField(m_nlpInput);
            if (m_debugMenu.Button("Send"))
                m_controller.AskLLMQuestion(m_nlpInput);
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

                m_llm.OnGUILlm();
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

            int fontSize = (int)(22.0f * ((float)Screen.height / (float)1080));
            m_guiButtonLeftJustify.fontSize = fontSize;


            if (m_guiToggleLeftJustify == null)
            {
                m_guiToggleLeftJustify = new GUIStyle(GUI.skin.button);
                m_guiToggleLeftJustify.alignment = TextAnchor.MiddleLeft;

                Texture2D transparentTexture = new Texture2D(1, 1);
                transparentTexture.SetPixel(0, 0, new Color(0, 0, 0, 0));
                transparentTexture.Apply();

                m_guiToggleLeftJustify.normal.background = transparentTexture;      // Remove the background for the normal state
                m_guiToggleLeftJustify.onNormal.background = transparentTexture;    // Remove the background for the toggled (on) state
            }

            m_guiToggleLeftJustify.fontSize = fontSize;
        }

        public void SetNlpInput(string input)
        {
            m_nlpInput = input;
        }

        public void SetNlpResponse(string response)
        {
            m_nlpResult = response;
        }
    }
}
