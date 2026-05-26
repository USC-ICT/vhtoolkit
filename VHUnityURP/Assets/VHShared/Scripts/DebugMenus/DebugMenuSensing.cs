using System.Collections.Generic;
using UnityEngine;
using Ride.Sensing;
using Ride.UI;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for configuring and monitoring the sensing system.
    /// Allows selection of sensing modes, webcam devices, and microphone threshold adjustments.
    /// </summary>
    public class DebugMenuSensing : RideMonoBehaviour
    {
        public enum SensingMode
        {
            Aws = 0,
            DeepFace = 1,
        }


        [Header("Sensing")]
        [SerializeField] SensingProcessor m_sensingProcessor;
        [SerializeField] VHWebCam m_vhWebCam;
        [SerializeField] RideRawImage m_webcamRawImage;
        [SerializeField] SensingSystemAWSRekognition m_awsRekognitionSystem;
        [SerializeField] SensingSystemAzureFace m_azureFaceSystem;
        [SerializeField] SensingSystemDeepFace m_deepFaceSystem;
        [SerializeField] Audio.MicrophoneAudioSystem m_microphoneAudio;
        [SerializeField] float m_microphoneThreshold = 0.05f;

        private SensingMode m_sensingMode;
        private ISensingSystem m_currentSensing; 
        private List<(SensingMode mode, string label)> m_sensingOptions;
        private string[] m_sensingOptionsText;

        #region Debug Menu
        private int m_webCamIndex = 0;
        private bool m_isMirroring = false;
        private bool m_webcamToggle = false;

        DebugMenu m_debugMenu;
        DemoController m_controller;
        DebugMenus m_debugMenusBase;
        #endregion


        /// <summary>
        /// Initializes the debug menu, controller, and sensing system on startup.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Systems.Get<DebugMenu>();
            m_controller = FindAnyObjectByType<DemoController>();
            m_debugMenusBase = FindAnyObjectByType<DebugMenus>();

            ChangeSensingMode(SensingMode.Aws);

            var sensingModes = new string[] { "AWS", "DeepFace" };
            if (RideUtils.IsAndroid() || RideUtils.IsWebGL())
                sensingModes = new string[] { "AWS" };

            if (RideUtils.IsAndroid() || RideUtils.IsIOS() || RideUtils.IsWebGL())
            {
                m_sensingOptions = new()
                {
                    (SensingMode.Aws, "AWS"),                  
                };
            }
            else
            {
                m_sensingOptions = new()
                {
                    (SensingMode.Aws, "AWS"),
                    (SensingMode.DeepFace, "DeepFace (Local)"),
                };
            }

            m_sensingOptionsText = new string[m_sensingOptions.Count];
            for (int i = 0; i < m_sensingOptions.Count; i++)
                m_sensingOptionsText[i] = m_sensingOptions[i].label;
        }


        /// <summary>
        /// Displays a selection grid for choosing the active sensing mode.
        /// </summary>
        public void OnGUISelectSensingMode()
        {
            m_debugMenu.Label("Sensing Selection");

            int currentUiIndex = GetUiIndexFromSensingMode(m_sensingMode);
            int newUiIndex = m_debugMenu.SelectionGrid(currentUiIndex, m_sensingOptionsText, 2);

            if (newUiIndex != currentUiIndex)
                ChangeSensingMode(GetSensingModeFromUiIndex(newUiIndex));
        }


        /// <summary>
        /// Handles the GUI layout for webcam, sensing, and microphone-related configurations.
        /// </summary>
        public void OnGUISensing()
        {
            bool oldGuiEnabled = GUI.enabled;
            if (RideUtils.IsWebGL())
                GUI.enabled = false;

            try
            {
            var character = m_controller.CurrentCharacter;

            m_debugMenusBase.OnGUICharacterConfig();

            // If no camera devices are found, display an error message and return.
            if (m_vhWebCam.deviceNames.Length <= 0)
            {
                m_debugMenu.Label($"No camera devices found");
                m_debugMenu.Label($"or not authorized");
                return;
            }

            // Webcam selection grid.
            m_debugMenu.Label("Webcam Selection");
            int webCamIndex = m_debugMenu.SelectionGrid(m_webCamIndex, m_vhWebCam.deviceNames, 2);
            if (webCamIndex != m_webCamIndex)
            {
                m_webCamIndex = webCamIndex;
                StopSensingProcessor();
                m_vhWebCam.SetCurrentDevice(m_webCamIndex);
            }

            m_debugMenu.Space();

            // Webcam toggle button.
            if (m_debugMenu.Button(m_webcamToggle ? "Webcam On" : "Webcam Off"))
                OnToggleWebcam();

            m_debugMenu.Space();

            // Sensing mode selection.
            OnGUISelectSensingMode();

            // Sensing system toggle button.
            if (m_debugMenu.Button(m_sensingProcessor.IsProcessing ? "Sensing On" : "Sensing Off"))
            {
                if (m_sensingProcessor.IsProcessing)
                    StopSensingProcessor();
                else
                    StartSensingProcessor();
            }

            // Display sensing data if processing is active.
            m_debugMenu.Label($"Sensing Results:");
            if (m_sensingProcessor.IsProcessing)
            {
                m_debugMenu.Label($"HeadRoll: {m_sensingProcessor.headResponse.roll:0.0}");
                m_debugMenu.Label($"Age: {m_sensingProcessor.characteristicsResponse.age}");
                m_debugMenu.Label($"Glasses: {m_sensingProcessor.characteristicsResponse.glasses}");
                m_debugMenu.Label($"Gender: {m_sensingProcessor.characteristicsResponse.gender}");
            }

            m_debugMenu.Space();

            // Mirroring toggle button.
            m_debugMenu.Label("Character Behaviors");
            if (m_debugMenu.Button(m_isMirroring ? "Mirroring On" : "Mirroring Off"))
            {
                if (m_isMirroring)
                {
                    m_sensingProcessor.onEmotionProcessed -= OnEmotionProcessedMirroring;
                    m_isMirroring = false;
                }
                else
                {
                    StartSensingProcessor();
                    m_sensingProcessor.onEmotionProcessed += OnEmotionProcessedMirroring;
                    m_isMirroring = true;
                }
            }

            m_debugMenu.Space();

            // Microphone controls.
            var listeningController = character.GetComponent<ListeningController>();
            if (listeningController != null)
            {
                const float listeningLabelWidth = 70f;
                const float listeningValueWidth = 65f;

                bool isListening = listeningController.IsListening;

                // Adjust microphone threshold.
                using (m_debugMenu.Horizontal())
                {
                    m_debugMenu.Label("Thresh", listeningLabelWidth);
                    m_debugMenu.Label($"{m_microphoneThreshold:f2}", listeningValueWidth);
                    float microphoneThreshold = m_debugMenu.HorizontalSlider(m_microphoneThreshold, 0, 1);

                    if (microphoneThreshold != m_microphoneThreshold)
                    {
                        m_microphoneThreshold = microphoneThreshold;
                        if (isListening)
                        {
                            listeningController.StopListening();
                            m_microphoneAudio.StopRecording();
                        }
                    }
                }

                // Display microphone volume level.
                if (isListening)
                {
                    using (m_debugMenu.Horizontal())
                    {
                        float recordingVolumeLevel = m_microphoneAudio.GetRecordingVolumeLevel();
                        m_debugMenu.Label("Vol", listeningLabelWidth);
                        m_debugMenu.Label($"{recordingVolumeLevel:f2}", listeningValueWidth);
                        m_debugMenu.HorizontalSlider(recordingVolumeLevel, 0, 1);
                    }
                }

                // Listening toggle button.
                if (m_debugMenu.Button(isListening ? "Listening On" : "Listening Off"))
                {
                    if (isListening)
                    {
                        listeningController.StopListening();
                        m_microphoneAudio.StopRecording();
                    }
                    else
                    {
                        m_microphoneAudio.StartRecording();
                        listeningController.StartListening(m_microphoneAudio, m_microphoneThreshold);
                    }
                }
            }
            }
            finally
            {
                GUI.enabled = oldGuiEnabled;
            }
        }

        void ChangeSensingMode(SensingMode mode)
        {
            m_sensingMode = mode;

            if (mode == SensingMode.Aws) m_currentSensing = m_awsRekognitionSystem;
            else if (mode == SensingMode.DeepFace) m_currentSensing = m_deepFaceSystem;

            m_sensingProcessor.SetSensingSystems(m_currentSensing);
        }

        private SensingMode GetSensingModeFromUiIndex(int uiIndex)
        {
            if (m_sensingOptions == null || m_sensingOptions.Count == 0)
                return SensingMode.Aws;

            if (uiIndex < 0) uiIndex = 0;
            if (uiIndex >= m_sensingOptions.Count) uiIndex = m_sensingOptions.Count - 1;

            return m_sensingOptions[uiIndex].mode;
        }

        private int GetUiIndexFromSensingMode(SensingMode mode)
        {
            if (m_sensingOptions != null)
            {
                for (int i = 0; i < m_sensingOptions.Count; i++)
                {
                    if (m_sensingOptions[i].mode == mode)
                        return i;
                }
            }

            return 0;
        }

        /// <summary>
        /// Stops the sensing processor.
        /// </summary>
        void StopSensingProcessor()
        {
            m_sensingProcessor.StopProcessing();
        }


        /// <summary>
        /// Starts the sensing processor and configures webcam rendering.
        /// </summary>
        void StartSensingProcessor()
        {
            if (m_sensingProcessor.IsProcessing)
                return;

            m_webcamRawImage.m_image.material = m_vhWebCam.renderMaterial;
            m_webcamRawImage.texture = m_vhWebCam.renderMaterial.mainTexture;

            Application.RequestUserAuthorization(UserAuthorization.WebCam);

            m_sensingProcessor.SetSensingSystems(m_currentSensing);
            m_sensingProcessor.StartProcessing();
        }


        /// <summary>
        /// Handles emotion mirroring when emotion processing is completed.
        /// </summary>
        void OnEmotionProcessedMirroring()
        {
            var character = m_controller.CurrentCharacter;
            Debug.Log($"OnEmotionProcessedMirroring() - {m_sensingProcessor.emotion}");

            var mirroringController = character != null ? character.GetComponent<MirroringController>() : default;
            if (mirroringController != default)
                mirroringController.MirrorEmotion(m_sensingProcessor.emotion);
        }


        /// <summary>
        /// Toggles the webcam on/off.
        /// </summary>
        public void OnToggleWebcam()
        {
            m_webcamToggle = !m_webcamToggle;
            if (m_webcamToggle && !m_sensingProcessor.IsProcessing)
                StartSensingProcessor();
            else if (!m_webcamToggle)
                StopSensingProcessor();

            m_webcamRawImage.Show(m_webcamToggle);
        }
    }
}
