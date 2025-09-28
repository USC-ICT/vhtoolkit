using System.Collections;
using UnityEngine;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for Automatic Speech Recognition (ASR).
    /// Allows users to select an ASR system and toggle speech recognition.
    /// </summary>
    public class DebugMenuASR : RideMonoBehaviour
    {
        private DebugMenu m_debugMenu;
        private DemoControllerBase m_controller;
        private string[] m_asrOptions;

        private bool m_micEnabled = false;
        private Coroutine m_waitForSpeechEndCoroutine;


        /// <summary>
        /// Initializes references to the necessary systems when the script starts.
        /// Configures ASR options based on the platform.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Globals.api.GetSystem<DebugMenu>();

            m_controller = FindAnyObjectByType<DemoControllerBase>();

            if (VHUtils.IsIOS() || VHUtils.IsAndroid())
                m_asrOptions = new string[] { "Azure", "Windows", "Mobile" };
            else
                m_asrOptions = new string[] { "Azure", "Windows" };
        }


        /// <summary>
        /// Updates the ASR system and stops recognition if the character is speaking.
        /// Ensures ASR does not interfere with voice playback.
        /// </summary>
        protected override void Update()
        {
            base.Update();

            if (m_controller.CurrentCharacter != null && m_controller.CurrentCharacter.Voice.isPlaying)
            {
                m_controller.m_currentASR.StopRecognizing();

                if (m_micEnabled && m_waitForSpeechEndCoroutine == null)
                    m_waitForSpeechEndCoroutine = StartCoroutine(WaitForSpeechEnd());

                return;
            }
        }


        /// <summary>
        /// Handles the GUI layout for ASR settings in the Debug Menu.
        /// Displays the ASR system selection options.
        /// </summary>
        public void OnGUIAsr()
        {
            m_debugMenu.Label($"<b>ASR</b>");
            OnGUISystemSelection();
        }


        /// <summary>
        /// Displays a selection grid for choosing the active ASR system.
        /// </summary>
        public void OnGUISystemSelection()
        {
            int asrMode = m_debugMenu.SelectionGrid(m_controller.m_asrMode, m_asrOptions, 2);

            if (m_controller.m_asrMode != asrMode)
                m_controller.ChangeASR(asrMode);

            m_debugMenu.Space();
        }


        /// <summary>
        /// Toggles the activation of ASR.
        /// If the ASR system is currently recognizing speech, it stops.
        /// If the ASR system is off, it starts recognizing speech unless the character is speaking.
        /// </summary>
        public void AsrActivateToggle()
        {
            if (m_controller.m_currentASR.IsRecognizing)
            {
                m_controller.m_currentASR.StopRecognizing();
                m_micEnabled = false;

                if (m_waitForSpeechEndCoroutine != null)
                {
                    StopCoroutine(m_waitForSpeechEndCoroutine);
                    m_waitForSpeechEndCoroutine = null;
                }

                return;
            }

            if (m_controller.CurrentCharacter.Voice.isPlaying)
            {
                m_controller.m_currentASR.StopRecognizing();
                return;
            }
            else
            {
                m_controller.m_currentASR.StartRecognizing();
                m_micEnabled = true;

                if (m_waitForSpeechEndCoroutine != null)
                {
                    StopCoroutine(m_waitForSpeechEndCoroutine);
                    m_waitForSpeechEndCoroutine = null;
                }
            }
        }


        /// <summary>
        /// Waits for the character's speech to end before re-enabling ASR.
        /// Ensures that ASR does not interfere with voice playback.
        /// </summary>
        private IEnumerator WaitForSpeechEnd()
        {
            Debug.Log("Waiting for speech to end");

            yield return new WaitUntil(() => !m_controller.CurrentCharacter.Voice.isPlaying);

            AsrActivateToggle();
        }
    }
}
