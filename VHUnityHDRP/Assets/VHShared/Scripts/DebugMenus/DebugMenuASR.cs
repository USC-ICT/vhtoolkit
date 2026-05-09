using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private List<(DemoControllerBase.AsrMode mode, string label)> m_asrOptions;
        private string[] m_asrOptionsText;


        /// <summary>
        /// Initializes references to the necessary systems when the script starts.
        /// Configures ASR options based on the platform.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Systems.Get<DebugMenu>();
            m_controller = FindAnyObjectByType<DemoControllerBase>();

            if (RideUtils.IsIOS() || RideUtils.IsAndroid())
            {
                m_asrOptions = new()
                {
                    (DemoControllerBase.AsrMode.Azure, "Azure"),
                    (DemoControllerBase.AsrMode.Windows, "Windows"),
                    (DemoControllerBase.AsrMode.Mobile, "Mobile"),
                    (DemoControllerBase.AsrMode.OpenAI, "OpenAI"),
                };
            }
            else if (RideUtils.IsWebGL())
            {
                m_asrOptions = new()
                {
                    (DemoControllerBase.AsrMode.AzureWebGL, "AzureWebGL"),
                };
            }
            else
            {
                m_asrOptions = new()
                {
                    (DemoControllerBase.AsrMode.Azure, "Azure"),
                    (DemoControllerBase.AsrMode.Windows, "Windows"),
                    (DemoControllerBase.AsrMode.OpenAI, "OpenAI"),
                };
            }

            m_asrOptionsText = new string[m_asrOptions.Count];
            for (int i = 0; i < m_asrOptions.Count; i++)
                m_asrOptionsText[i] = m_asrOptions[i].label;
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
            int currentUiIndex = GetUiIndexFromAsrMode(m_controller.m_asrMode);
            int newUiIndex = m_debugMenu.SelectionGrid(currentUiIndex, m_asrOptionsText, 2);

            if (newUiIndex != currentUiIndex)
                m_controller.ChangeASR(GetAsrModeFromUiIndex(newUiIndex));

            m_debugMenu.Space();
        }


        /// <summary>
        /// Toggles the activation of ASR.
        /// If the ASR system is currently recognizing speech, it stops.
        /// If the ASR system is off, it starts recognizing speech unless the character is speaking.
        /// This is called via the gameobject Button_UseASR callback
        /// </summary>
        public void AsrActivateToggle() => m_controller.ToggleASR();


        private DemoControllerBase.AsrMode GetAsrModeFromUiIndex(int uiIndex)
        {
            if (m_asrOptions == null || m_asrOptions.Count == 0)
                return DemoControllerBase.AsrMode.Azure;

            if (uiIndex < 0) uiIndex = 0;
            if (uiIndex >= m_asrOptions.Count) uiIndex = m_asrOptions.Count - 1;

            return m_asrOptions[uiIndex].mode;
        }

        private int GetUiIndexFromAsrMode(DemoControllerBase.AsrMode mode)
        {
            if (m_asrOptions != null)
            {
                for (int i = 0; i < m_asrOptions.Count; i++)
                {
                    if (m_asrOptions[i].mode == mode)
                        return i;
                }
            }

            return 0;
        }
    }
}
