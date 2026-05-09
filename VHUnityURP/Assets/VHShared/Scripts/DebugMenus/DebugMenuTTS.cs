using System.Collections.Generic;
using System.Linq;
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
        private List<(DemoControllerBase.TtsMode mode, string label)> m_ttsOptions;
        private string[] m_ttsOptionsText;
        Vector2 m_ScrollPos = Vector2.zero;  
        bool m_voiceSelectionToggle = false; 
        #endregion


        /// <summary>
        /// Initializes references to the debug menu and demo controller.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Systems.Get<DebugMenu>();
            m_controller = FindAnyObjectByType<DemoController>();

            m_ttsOptions = new()
            {
                (DemoControllerBase.TtsMode.Polly, "Polly"),
                (DemoControllerBase.TtsMode.ElevenLabs, "11Labs"),
            };

            m_ttsOptionsText = new string[m_ttsOptions.Count];
            for (int i = 0; i < m_ttsOptions.Count; i++)
                m_ttsOptionsText[i] = m_ttsOptions[i].label;
        }


        /// <summary>
        /// Handles the GUI drawing for the TTS section in the Debug Menu.
        /// Calls methods to display system and voice selection options.
        /// </summary>
        public void OnGUITts()
        {
            m_debugMenu.Label($"<b>TTS</b>");
            OnGUISystemSelection();
            OnGUIVoiceSelection();
        }


        /// <summary>
        /// Displays a selection grid for choosing the TTS system (e.g., Polly, 11Labs).
        /// </summary>
        public void OnGUISystemSelection()
        {
            int currentUiIndex = GetUiIndexFromAsrMode(m_controller.m_ttsMode);
            int newUiIndex = m_debugMenu.SelectionGrid(currentUiIndex, m_ttsOptionsText, 2);

            if (newUiIndex != currentUiIndex)
                m_controller.ChangeTts(GetAsrModeFromUiIndex(newUiIndex));
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
                const int maxDisplayedVoiceNameLength = 10;
                string[] displayVoices = m_controller.m_currentTTS.GetAvailableVoices()?
                    .Select(voiceName => string.IsNullOrEmpty(voiceName) || voiceName.Length <= maxDisplayedVoiceNameLength
                        ? voiceName
                        : voiceName.Substring(0, maxDisplayedVoiceNameLength) + "~")
                    .ToArray();

                m_ScrollPos = GUILayout.BeginScrollView(m_ScrollPos, GUILayout.MinHeight(100));
                m_controller.m_ttsVoice = m_debugMenu.SelectionGrid(m_controller.m_ttsVoice, displayVoices, 4);
                GUILayout.EndScrollView();
            }

            m_debugMenu.Space();
        }

        private DemoControllerBase.TtsMode GetAsrModeFromUiIndex(int uiIndex)
        {
            if (m_ttsOptions == null || m_ttsOptions.Count == 0)
                return DemoControllerBase.TtsMode.Polly;

            if (uiIndex < 0) uiIndex = 0;
            if (uiIndex >= m_ttsOptions.Count) uiIndex = m_ttsOptions.Count - 1;

            return m_ttsOptions[uiIndex].mode;
        }

        private int GetUiIndexFromAsrMode(DemoControllerBase.TtsMode mode)
        {
            if (m_ttsOptions != null)
            {
                for (int i = 0; i < m_ttsOptions.Count; i++)
                {
                    if (m_ttsOptions[i].mode == mode)
                        return i;
                }
            }

            return 0;
        }
    }
}
