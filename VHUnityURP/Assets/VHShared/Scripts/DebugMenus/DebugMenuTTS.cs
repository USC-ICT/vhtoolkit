using UnityEngine;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface related to TTS (Text-to-Speech) settings.
    /// Allows selecting the TTS system and voice.
    /// </summary>
    public class DebugMenuTTS : RideMonoBehaviour
    {
        #region Debug menu variables
        DebugMenu m_debugMenu;
        DemoController m_controller;
        Vector2 m_ScrollPos = Vector2.zero;  
        bool m_voiceSelectionToggle = false; 
        #endregion


        /// <summary>
        /// Initializes references to the debug menu and demo controller.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            // Get the DebugMenu instance from the global system.
            m_debugMenu = Globals.api.GetSystem<DebugMenu>();

            // Find an instance of DemoController in the scene.
            m_controller = FindAnyObjectByType<DemoController>();
        }


        /// <summary>
        /// Handles the GUI drawing for the TTS section in the Debug Menu.
        /// Calls methods to display system and voice selection options.
        /// </summary>
        public void OnGUITts()
        {
            // Draw a label indicating the TTS section.
            m_debugMenu.Label($"<b>TTS</b>");

            // Draw GUI for selecting the TTS system.
            OnGUISystemSelection();

            // Draw GUI for selecting the TTS voice.
            OnGUIVoiceSelection();
        }


        /// <summary>
        /// Displays a selection grid for choosing the TTS system (e.g., Polly, 11Labs).
        /// </summary>
        public void OnGUISystemSelection()
        {
            // Draw a selection grid for TTS mode and get the user's selection.
            int ttsMode = m_debugMenu.SelectionGrid(m_controller.m_ttsMode, new string[] { "Polly", "11Labs"}, 2);

            // If the selected TTS mode has changed, update it in the DemoController.
            if (m_controller.m_ttsMode != ttsMode)
                m_controller.ChangeTts(ttsMode);
        }


        /// <summary>
        /// Displays the voice selection UI within a collapsible toggle section.
        /// </summary>
        public void OnGUIVoiceSelection()
        {
            // Toggle button for expanding/collapsing the voice selection menu.
            m_voiceSelectionToggle = m_debugMenu.Toggle(
                m_voiceSelectionToggle,
                m_voiceSelectionToggle ? $"- Select TTS Voice" : $"+ Select TTS Voice"
            );

            // If the toggle is enabled, display the voice selection grid inside a scrollable view.
            if (m_voiceSelectionToggle)
            {
                m_ScrollPos = GUILayout.BeginScrollView(m_ScrollPos, GUILayout.MinHeight(100));
                m_controller.m_ttsVoice = m_debugMenu.SelectionGrid(m_controller.m_ttsVoice, m_controller.m_currentTTS.GetAvailableVoices(), 4);
                GUILayout.EndScrollView();
            }

            m_debugMenu.Space();
        }
    }
}
