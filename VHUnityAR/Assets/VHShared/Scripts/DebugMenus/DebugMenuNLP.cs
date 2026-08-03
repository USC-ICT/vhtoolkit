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
        private bool m_promptToggle = false;
        private bool m_languageDetectionToggle = false;
        private GUIStyle m_languageDetectionCheckboxStyle;
        private string m_prompt;

        // Reasoning models spend part of this budget on internal reasoning, so a low ceiling can
        // return an empty answer; the range stays wide enough to show that effect deliberately.
        private const int MaxTokensMinimum = 100;
        private const int MaxTokensMaximum = 4000;

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
                    (DemoControllerBase.NlpMode.Gemini, "Gemini"),
                };
            }
            else
            {
                m_llmOptions = new()
                {
                    (DemoControllerBase.NlpMode.ChatGPT, "ChatGPT"),
                    (DemoControllerBase.NlpMode.Claude, "Claude"),
                    (DemoControllerBase.NlpMode.Gemini, "Gemini"),
                    (DemoControllerBase.NlpMode.AwsLex, "AWS Lex"),
                    (DemoControllerBase.NlpMode.Rasa, "Rasa (Local)"),
                    (DemoControllerBase.NlpMode.VLLM, "vLLM (Local)"),
                    (DemoControllerBase.NlpMode.Ollama, "Ollama (Local)"),
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
            OnGUIDynamicLanguageDetection();
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

            // Ollama keeps two models resident and swaps per request — offer a live hot-swap button.
            if (m_controller.m_nlpMode == DemoControllerBase.NlpMode.Ollama)
            {
                using (new GUILayout.HorizontalScope())
                {
                    m_debugMenu.Label($"Model: {m_controller.OllamaActiveModelDisplay}", 200f);
                    if (m_debugMenu.Button("Swap model"))
                        m_controller.ToggleOllamaModel();
                }
            }

            OnGUIGenerationSettings();

            m_debugMenu.Space();
        }

        /// <summary>
        /// Displays temperature and max-token sliders for the active LLM, reading and writing the
        /// provider's own settings so a change takes effect on the next request. Hidden for
        /// providers with no generation settings (scripted/intent-based systems).
        /// </summary>
        public void OnGUIGenerationSettings()
        {
            var llm = m_controller.m_currentLLM;
            if (llm == null || !llm.SupportsGenerationSettings)
                return;

            using (new GUILayout.HorizontalScope())
            {
                float temperature = llm.Temperature;
                m_debugMenu.Label($"Temperature: {temperature:F2}", 200f);
                float newTemperature = m_debugMenu.HorizontalSlider(temperature, 0f, 1f);
                if (!Mathf.Approximately(newTemperature, temperature))
                    llm.Temperature = newTemperature;
            }

            using (new GUILayout.HorizontalScope())
            {
                int maxTokens = llm.MaxTokens;
                m_debugMenu.Label($"Max Tokens: {maxTokens}", 200f);
                int newMaxTokens = (int)m_debugMenu.HorizontalSlider(maxTokens, MaxTokensMinimum, MaxTokensMaximum);
                if (newMaxTokens != maxTokens)
                    llm.MaxTokens = newMaxTokens;
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
        /// Display debug functionality for dynamic langauge detection, from either ASR or LLM fallback.
        /// </summary>
        public void OnGUIDynamicLanguageDetection()
        {
            m_languageDetectionToggle = GUILayout.Toggle(m_languageDetectionToggle, m_languageDetectionToggle ? $"- <b>Dynamic Language Detection:</b>" : $"+ <b>Dynamic Language Detection</b>", m_debugMenusBase.m_guiToggleLeftJustify);

            if (m_languageDetectionToggle)
            {
                bool languageFallbackEnabled = m_controller.LanguageDetectionFallbackEnabled;
                bool newLanguageFallbackEnabled = GUILayout.Toggle(languageFallbackEnabled, "OpenAI text fallback detection", GetLanguageDetectionCheckboxStyle());
                if (newLanguageFallbackEnabled != languageFallbackEnabled)
                    m_controller.LanguageDetectionFallbackEnabled = newLanguageFallbackEnabled;

                m_debugMenu.Label($"Detected: {m_controller.UserLanguageDebugDisplay}");
                m_debugMenu.Label($"NVBG: {m_controller.EffectiveNvbgLanguageDisplay}");
                m_debugMenu.Label($"Provider: {m_controller.AsrLanguageSupportDisplay}");
            }
            m_debugMenu.Space();
        }

        private GUIStyle GetLanguageDetectionCheckboxStyle()
        {
            if (m_languageDetectionCheckboxStyle == null)
                m_languageDetectionCheckboxStyle = new GUIStyle(GUI.skin.toggle);

            float scale = (float)Screen.height / 1080f;
            m_languageDetectionCheckboxStyle.fontSize = (int)(22.0f * scale);
            m_languageDetectionCheckboxStyle.fixedHeight = 30f * scale;

            return m_languageDetectionCheckboxStyle;
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
