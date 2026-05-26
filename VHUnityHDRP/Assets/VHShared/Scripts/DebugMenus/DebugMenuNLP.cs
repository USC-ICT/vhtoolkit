using System.Collections.Generic;
using UnityEngine;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for configuring and interacting with the Natural Language Processing (NLP) 
    /// systems, including LLMs (Large Language Models).
    /// Provides options for selecting an NLP system, setting parameters, and modifying LLM prompts.
    /// </summary>
    public class DebugMenuNLP : RideMonoBehaviour
    {
        #region Debug Menu Variables

        private DebugMenu m_debugMenu;
        private DemoController m_controller;
        private DebugMenus m_debugMenusBase;
        private List<(DemoControllerBase.NlpMode mode, string label)> m_llmOptions;
        private string[] m_llmOptionsText;
        private RideVector2 m_promptScroll;
        private float m_LLMTemperature = 0.3f;
        private int m_LLMMaxToken = 200;
        private bool m_promptToggle = false;
        private string m_prompt;

        #endregion


        /// <summary>
        /// Initializes references to the necessary systems when the script starts.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Systems.Get<DebugMenu>();
            m_controller = FindAnyObjectByType<DemoController>();
            m_debugMenusBase = FindAnyObjectByType<DebugMenus>();

            if (RideUtils.IsAndroid() || RideUtils.IsIOS() || RideUtils.IsWebGL())
            {
                m_llmOptions = new()
                {
                    (DemoControllerBase.NlpMode.ChatGPT, "ChatGPT"),
                    (DemoControllerBase.NlpMode.Claude, "Claude"),
                };
            }
            else
            {
                m_llmOptions = new()
                {
                    (DemoControllerBase.NlpMode.ChatGPT, "ChatGPT"),
                    (DemoControllerBase.NlpMode.Claude, "Claude"),
                    (DemoControllerBase.NlpMode.AwsLex, "AWS Lex"),
                    (DemoControllerBase.NlpMode.Rasa, "Rasa"),
                    (DemoControllerBase.NlpMode.VLLM, "vLLM (Local)"),
                };
            }

            m_llmOptionsText = new string[m_llmOptions.Count];
            for (int i = 0; i < m_llmOptions.Count; i++)
                m_llmOptionsText[i] = m_llmOptions[i].label;
        }

        /// <summary>
        /// Handles the GUI layout for LLM settings in the Debug Menu.
        /// Displays the system selection and prompt configuration UI.
        /// </summary>
        public void OnGUILlm()
        {
            m_debugMenu.Label($"<b>LLM / Scripted</b>");

            OnGUISystemSelection();

            OnGUIPrompt();
        }

        /// <summary>
        /// Displays a selection grid for choosing the active LLM system.
        /// Also provides sliders for adjusting temperature and max tokens.
        /// </summary>
        public void OnGUISystemSelection()
        {
            int currentUiIndex = GetUiIndexFromLlmMode(m_controller.m_nlpMode);
            int newUiIndex = m_debugMenu.SelectionGrid(currentUiIndex, m_llmOptionsText, 2);

            if (newUiIndex != currentUiIndex)
                m_controller.ChangeNlp(GetLlmModeFromUiIndex(newUiIndex));

            using (new GUILayout.HorizontalScope())
            {
                m_debugMenu.Label($"Temperature: {m_LLMTemperature:F1}", 200f);
                m_LLMTemperature = m_debugMenu.HorizontalSlider(m_LLMTemperature, 0f, 1f);
            }

            using (new GUILayout.HorizontalScope())
            {
                m_debugMenu.Label($"Max Tokens: {m_LLMMaxToken}", 200f);
                m_LLMMaxToken = (int)m_debugMenu.HorizontalSlider(m_LLMMaxToken, 0, 200);
            }
        }

        /// <summary>
        /// Displays a prompt input field and allows setting a custom prompt for the LLM.
        /// </summary>
        public void OnGUIPrompt()
        {
            // Only display the prompt UI if the selected LLM mode is not "Lex".
            if (m_controller.m_nlpMode != DemoControllerBase.NlpMode.AwsLex)
            {
                m_promptToggle = GUILayout.Toggle(m_promptToggle, m_promptToggle ? $"- <b>Prompt:</b>" : $"+ <b>Prompt</b>", m_debugMenusBase.m_guiToggleLeftJustify);

                if (m_promptToggle)
                {
                    using (var scrollViewScope = new GUILayout.ScrollViewScope(m_promptScroll, GUILayout.Height(200)))
                    {
                        m_promptScroll = scrollViewScope.scrollPosition;
                        m_prompt = m_debugMenu.TextArea(m_prompt);
                    }

                    if (m_debugMenu.Button("Set Prompt"))
                    {
                        var character = m_controller.CurrentCharacter;
                        m_controller.SetPrompt(character, m_prompt);
                    }
                }
            }

            m_debugMenu.Space();
        }

        /// <summary>
        /// Sets the LLM prompt input field with a predefined value.
        /// </summary>
        /// <param name="prompt">The new prompt text.</param>
        public void SetUIPrompt(string prompt)
        {
            m_prompt = prompt;
        }

        private DemoControllerBase.NlpMode GetLlmModeFromUiIndex(int uiIndex)
        {
            if (m_llmOptions == null || m_llmOptions.Count == 0)
                return DemoControllerBase.NlpMode.ChatGPT;

            if (uiIndex < 0) uiIndex = 0;
            if (uiIndex >= m_llmOptions.Count) uiIndex = m_llmOptions.Count - 1;

            return m_llmOptions[uiIndex].mode;
        }

        private int GetUiIndexFromLlmMode(DemoControllerBase.NlpMode mode)
        {
            if (m_llmOptions != null)
            {
                for (int i = 0; i < m_llmOptions.Count; i++)
                {
                    if (m_llmOptions[i].mode == mode)
                        return i;
                }
            }

            return 0;
        }
    }
}
