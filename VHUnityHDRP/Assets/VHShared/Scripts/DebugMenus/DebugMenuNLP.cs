using UnityEngine;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for configuring and interacting with the LLM (Large Language Model).
    /// Provides options for selecting an LLM system, setting parameters, and modifying prompts.
    /// </summary>
    public class DebugMenuLLM : RideMonoBehaviour
    {
        #region Debug Menu Variables

        private DebugMenu m_debugMenu;        
        private DemoController m_controller;  
        private DebugMenus m_debugMenusBase;  
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

            m_debugMenu = Globals.api.GetSystem<DebugMenu>();
            m_controller = FindAnyObjectByType<DemoController>();
            m_debugMenusBase = FindAnyObjectByType<DebugMenus>();
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
            // Draw a selection grid for LLM modes and get the user's selection.
            int llmMode = m_debugMenu.SelectionGrid(m_controller.m_llmMode, new string[] { "ChatGPT", "Claude", "AWS Lex", "Rasa" }, 2);

            // If the selected LLM mode has changed, update it in the DemoController.
            if (m_controller.m_llmMode != llmMode)
                m_controller.ChangeLlm(llmMode);

            // Draw temperature slider.
            using (new GUILayout.HorizontalScope())
            {
                m_debugMenu.Label($"Temperature: {string.Format("{0:f1}", m_LLMTemperature)}", 200f);
                m_LLMTemperature = m_debugMenu.HorizontalSlider(m_LLMTemperature, 0f, 1f);
            }

            // Draw max token slider.
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
            if (m_controller.m_llmMode != 2)
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
    }
}
