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
        [Header("Sensing")]
        [SerializeField] SensingProcessor m_sensingProcessor;
        [SerializeField] VHWebCam m_vhWebCam;
        [SerializeField] RideRawImage m_webcamRawImage;
        [SerializeField] SensingSystemAWSRekognition m_awsRekognitionSystem;
        [SerializeField] SensingSystemAzureFace m_azureFaceSystem;
        [SerializeField] DeepFaceRecognitionSystem m_localDeepFaceSystem;
        [SerializeField] Audio.MicrophoneAudioSystem m_microphoneAudio;
        [SerializeField] float m_microphoneThreshold = 0.001f;

        private ISensingSystem m_currentSensing; 

        #region Debug Menu
        private int m_webCamIndex = 0;      
        private int m_sensingMode = 0;      
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

            m_debugMenu = Globals.api.GetSystem<DebugMenu>();
            m_controller = FindAnyObjectByType<DemoController>();
            m_debugMenusBase = FindAnyObjectByType<DebugMenus>();

            // Set the default sensing system to AWS Rekognition.
            m_currentSensing = m_awsRekognitionSystem;
            m_sensingProcessor.SetSensingSystems(m_currentSensing);
        }


        /// <summary>
        /// Displays a selection grid for choosing the active sensing mode.
        /// </summary>
        public void OnGUISelectSensingMode()
        {
            m_debugMenu.Label("Sensing Selection");
            int sensingMode = m_debugMenu.SelectionGrid(m_sensingMode, new string[] { "AWS", "DeepFace" }, 2);
            if (sensingMode != m_sensingMode)
            {
                m_sensingMode = sensingMode;

                if (m_sensingMode == 0)
                    m_currentSensing = m_awsRekognitionSystem;
                else if (m_sensingMode == 1)
                    m_currentSensing = m_localDeepFaceSystem;
                else if (m_sensingMode == 2)
                    m_currentSensing = m_azureFaceSystem;
            }
        }


        /// <summary>
        /// Handles the GUI layout for webcam, sensing, and microphone-related configurations.
        /// </summary>
        public void OnGUISensing()
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
                bool isListening = listeningController.IsListening;

                // Adjust microphone threshold.
                using (new GUILayout.HorizontalScope())
                {
                    m_debugMenu.Label($"{m_microphoneThreshold:f2}", 65);
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
                    using (new GUILayout.HorizontalScope())
                    {
                        float recordingVolumeLevel = m_microphoneAudio.GetRecordingVolumeLevel();
                        m_debugMenu.Label($"{recordingVolumeLevel:f2}", 65);
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
