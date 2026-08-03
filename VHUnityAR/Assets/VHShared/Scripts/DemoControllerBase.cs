using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Ride.Audio;
using Ride.Conversation;
using Ride.NLP;
using Ride.SpeechRecognition;
using Ride.TextToSpeech;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Base class for demo logic. Handles initialization and control of all services.
    /// </summary>
    public abstract class DemoControllerBase : RideMonoBehaviour
    {
        public enum AsrMode
        {
            Azure = 0,
            Windows = 1,
            Mobile = 2,
            AzureWebGL = 3,
            OpenAI = 4,
            FasterWhisper = 5,
            Gemini = 6,
        }

        public enum NlpMode
        {
            ChatGPT = 0,
            Claude = 1,
            AwsLex = 2,
            Rasa = 3,
            VLLM = 4,
            Gemini = 5,
            Ollama = 6,
        }

        public enum TtsMode
        {
            Polly = 0,
            ElevenLabs = 1,
            Piper = 2,
            Kokoro = 3,
            XTTS = 4,
            Gemini = 5,
        }

        public enum ConversationMode
        {
            Classic = 0,
            Unified = 1,
        }


        [Header("Debug Menus")]
        [SerializeField] protected DebugMenuGaze m_gaze;
        [SerializeField] protected DebugMenuLipsync m_lipsync;
        [SerializeField] protected DebugMenuNLP m_nlp;

        [Header("UI")]
        protected IDemoControllerUI m_demoControllerUI;

        [Header("VH")]
        [SerializeField] protected Transform m_charactersParent;
        public Transform CharactersParent { get => m_charactersParent; }
        protected readonly List<MecanimCharacter> m_characters = new();

        protected SpeechRecognitionSystemWindows m_windowsSpeechRecognitionSystem;
        protected SpeechRecognitionSystemAzure m_azureSpeechRecognitionSystem;
        protected SpeechRecognitionSystemAzureWebGL m_azureWebGLSpeechRecognitionSystem;
        protected SpeechRecognitionSystemOpenAI m_openAISpeechRecognitionSystem;
        protected SpeechRecognitionSystemFasterWhisper m_fasterWhisperSpeechRecognitionSystem;
        protected SpeechRecognitionSystemGemini m_geminiSpeechRecognitionSystem;
        protected UnifiedConversationSystemOpenAIRealtime m_openAIRealtimeConversationSystem;
        protected StreamingLipsyncSystem m_streamingLipsyncSystem;
        protected NlpSystemChatGPT m_chatGPTSystem;
        protected NlpSystemGemini m_geminiSystem;
        protected NlpSystemAnthropic m_anthropicSystem;
        protected NlpSystemRasa m_rasaNlpSystem;
        protected NlpSystemAWSLex m_lexSystem;
        protected NlpSystemVLLM m_vLLMSystem;
        protected NlpSystemOllama m_ollamaSystem;
        protected LanguageDetectionSystemOpenAI m_languageDetectionSystem;
        protected NonverbalBehaviorGeneratorSystem m_nvbgSystem;
        protected TextToSpeechSystemElevenLabs m_elevenTextToSpeechSystem;
        protected TextToSpeechSystemAWSPolly m_awsPollyTextToSpeechSystem;
        protected TextToSpeechSystemPiper m_piperTextToSpeechSystem;
        protected TextToSpeechSystemKokoro m_kokoroTextToSpeechSystem;
        protected TextToSpeechSystemXTTS m_xttsTextToSpeechSystem;
        protected TextToSpeechSystemGemini m_geminiTextToSpeechSystem;

        [SerializeField] protected TtsReader m_ttsReader;

        [NonSerialized] public NlpSystemUnity m_currentLLM;
        [NonSerialized] public NlpSystemUnity m_currentScripted;
        [NonSerialized] public ISpeechRecognitionSystem m_currentASR;
        [NonSerialized] public ILipsyncedTextToSpeechSystem m_currentTTS;
        [NonSerialized] public NlpMode m_nlpMode;
        [NonSerialized] public AsrMode m_asrMode;
        [NonSerialized] public TtsMode m_ttsMode;
        [NonSerialized] public ConversationMode m_conversationMode = ConversationMode.Classic;
        [NonSerialized] public int m_ttsVoice;

        protected MecanimCharacter m_currentCharacter;
        protected AudioClip m_audioClip;
        protected string m_audioFilePath;
        protected string m_lipsyncXML;
        protected string m_response;
        protected string m_lastNlpInput;
        // Upper bound on a single spoken turn. An application supplies its own prompt, so a response
        // can legitimately be long; this only guards against an unbounded one. Takes the pipeline's
        // own ceiling so the two cannot drift apart. The effective limit is this or the active speech
        // provider's per-request maximum, whichever is lower.
        protected int m_maxSpokenCharacters = TextToSpeechSystemUnity.DefaultMaxRequestCharacters;

        // Spoken when a response had to be shortened. Without it the character simply stops, which
        // sounds like a deliberate ending, and the listener never learns that content was dropped.
        protected string m_lengthCutoffNotice =
            " I had to stop there, because my full answer was longer than I can say out loud.";

        // Words speech synthesizers mispronounce, mapped to phonetic respellings. Applied only to
        // the text sent to TTS, so transcripts, history and the UI keep the correct spelling. This
        // is provider-agnostic; it does not reach the realtime conversation path, which produces
        // audio directly.
        protected (string written, string spoken)[] m_pronunciationOverrides =
        {
            ("SAIA", "saia"),
            ("VITA", "veetah"),
        };
        protected ThinkingController m_thinkingController;
        protected int m_classicPipelineGeneration = 0;
        protected MecanimCharacter m_realtimeBoundCharacter;
        protected AudioSource m_realtimeOutputAudioSource;
        protected string m_realtimeAssistantResponse = string.Empty;
        protected string m_realtimePendingAssistantUiText = string.Empty;
        protected string m_realtimePendingLipsyncText = string.Empty;
        protected string m_latestUserLanguageTag = string.Empty;
        protected string m_latestUserLanguageSource = "none";
        protected float m_latestUserLanguageConfidence = -1f;
        protected float m_realtimeLastChunkPlaybackSeconds = 0f;
        protected DebugMenus m_debugMenus;
        private const string RealtimeUserGazeTargetName = "GazeTargetUser";
        private const string UserInputSourceAsr = "ASR";
        private const string UserInputSourceRealtimeAsr = "OpenAI Realtime ASR";
        private const string UserInputSourceText = "text input";
        private readonly Dictionary<string, List<NlpInteraction>> m_characterConversationHistory = new();
        private int m_ttsVoiceAssignmentToken;
        private bool m_isCurrentCharacterVoiceReady;
        private const float TtsVoiceAssignmentTimeoutSeconds = 5f;
        private const string MissingVoiceMessage = "I'm sorry, but I can't seem to find my voice.";

        public bool IntroduceOnLoad = true;

        public MecanimCharacter CurrentCharacter => m_currentCharacter;
        public IReadOnlyList<MecanimCharacter> Characters => m_characters;
        /// <summary>True once the current character's TTS voice has been resolved (read-only; used by the study player to time its greeting).</summary>
        public bool IsCharacterVoiceReady => m_isCurrentCharacterVoiceReady;
        public bool UsingRealtimeConversationMode => m_conversationMode == ConversationMode.Unified;
        public string DetectedUserLanguageDisplay => string.IsNullOrWhiteSpace(m_latestUserLanguageTag) ? "unknown" : m_latestUserLanguageTag;
        public string EffectiveNvbgLanguageDisplay => string.IsNullOrWhiteSpace(GetEffectiveNvbgLanguageTag()) ? "en" : GetEffectiveNvbgLanguageTag();
        public string UserLanguageDebugDisplay => BuildUserLanguageDebugDisplay();
        public string AsrLanguageSupportDisplay => BuildAsrLanguageSupportDisplay();
        public bool LanguageDetectionFallbackEnabled
        {
            get => GetLanguageDetectionSettings().enabled;
            set
            {
                var settings = GetLanguageDetectionSettings();
                settings.enabled = value;
                LanguageDetectionSystemOpenAI.Settings = settings;
            }
        }

        [NonSerialized] public bool m_startButtonPressed = false;

        /// <summary>
        /// Controls whether character configuration UI and related interactions
        /// are currently enabled.
        ///
        /// This flag represents a global UI / interaction gate rather than a user
        /// preference. When false, character-related controls (including ASR,
        /// TTS configuration, and other interactive elements) are considered
        /// temporarily unavailable due to application state.
        ///
        /// Examples include:
        ///   - a character utterance is in progress,
        ///   - a modal interaction is active,
        ///   - or the application is intentionally preventing configuration changes.
        ///
        /// This value does not represent user intent and should not be interpreted
        /// as a desired on/off state. It is typically managed by higher-level
        /// application flow.
        /// </summary>
        [NonSerialized] public bool m_characterConfigUIEnabled = true;

        /// <summary>
        /// The user's requested ASR state.
        ///
        /// This value represents user intent only (e.g., pressing the mic button)
        /// and does not guarantee that ASR is currently recognizing. Whether ASR
        /// can actually start or continue recognizing is determined at runtime
        /// by <see cref="ApplyAsrState"/>, which also considers gating conditions
        /// such as <see cref="m_characterConfigUIEnabled"/>.
        ///
        /// In contrast to <see cref="m_characterConfigUIEnabled"/>, this flag
        /// persists across temporary blocking conditions and is reapplied
        /// automatically when those conditions clear.
        /// </summary>
        private bool m_asrDesiredEnabled;
        protected bool IsAsrDesiredEnabled => m_asrDesiredEnabled;


        protected void Awake()
        {
            // https://discussions.unity.com/t/on-play-dont-destroy-on-load-with-a-debug-updater-object-is-created-automatically/824863/12
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        }

        protected override void Start()
        {
            base.Start();

            var config = Systems.Get<ConfigurationSystemUnity>();
            if (config != null && !config.IsCorrectVersion()) { config.ResetConfig(); config.Save(); }

            m_windowsSpeechRecognitionSystem = Systems.Get<SpeechRecognitionSystemWindows>();
            m_azureSpeechRecognitionSystem = Systems.Get<SpeechRecognitionSystemAzure>();
            m_azureWebGLSpeechRecognitionSystem = Systems.Get<SpeechRecognitionSystemAzureWebGL>();
            m_openAISpeechRecognitionSystem = Systems.Get<SpeechRecognitionSystemOpenAI>();
            m_fasterWhisperSpeechRecognitionSystem = Systems.Get<SpeechRecognitionSystemFasterWhisper>();
            m_geminiSpeechRecognitionSystem = Systems.Get<SpeechRecognitionSystemGemini>();
            m_openAIRealtimeConversationSystem = Systems.Get<UnifiedConversationSystemOpenAIRealtime>();
            if (m_openAIRealtimeConversationSystem == null)
                m_openAIRealtimeConversationSystem = gameObject.AddComponent<UnifiedConversationSystemOpenAIRealtime>();

            m_streamingLipsyncSystem = Systems.Get<StreamingLipsyncSystem>();
            if (m_streamingLipsyncSystem == null)
                m_streamingLipsyncSystem = gameObject.AddComponent<StreamingLipsyncSystem>();
            m_chatGPTSystem = Systems.Get<NlpSystemChatGPT>();
            m_geminiSystem = Systems.Get<NlpSystemGemini>();
            m_anthropicSystem = Systems.Get<NlpSystemAnthropic>();
            m_rasaNlpSystem = Systems.Get<NlpSystemRasa>();
            m_lexSystem = Systems.Get<NlpSystemAWSLex>();
            m_vLLMSystem = Systems.Get<NlpSystemVLLM>();
            m_ollamaSystem = Systems.Get<NlpSystemOllama>();
            if (m_ollamaSystem == null)
                m_ollamaSystem = gameObject.AddComponent<NlpSystemOllama>();
            m_languageDetectionSystem = Systems.Get<LanguageDetectionSystemOpenAI>();
            if (m_languageDetectionSystem == null)
                m_languageDetectionSystem = gameObject.AddComponent<LanguageDetectionSystemOpenAI>();
            m_nvbgSystem = Systems.Get<NonverbalBehaviorGeneratorSystem>();
            m_elevenTextToSpeechSystem = Systems.Get<TextToSpeechSystemElevenLabs>();
            m_awsPollyTextToSpeechSystem = Systems.Get<TextToSpeechSystemAWSPolly>();
            m_piperTextToSpeechSystem = Systems.Get<TextToSpeechSystemPiper>();
            m_kokoroTextToSpeechSystem = Systems.Get<TextToSpeechSystemKokoro>();
            m_xttsTextToSpeechSystem = Systems.Get<TextToSpeechSystemXTTS>();
            m_geminiTextToSpeechSystem = Systems.Get<TextToSpeechSystemGemini>();
            if (!m_ttsReader) m_ttsReader = FindAnyObjectByType<TtsReader>();

            if (m_windowsSpeechRecognitionSystem != null) m_windowsSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_azureSpeechRecognitionSystem != null) m_azureSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_azureWebGLSpeechRecognitionSystem != null) m_azureWebGLSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_openAISpeechRecognitionSystem != null) m_openAISpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_fasterWhisperSpeechRecognitionSystem != null) m_fasterWhisperSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_geminiSpeechRecognitionSystem != null) m_geminiSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_openAIRealtimeConversationSystem != null)
            {
                m_openAIRealtimeConversationSystem.UserTranscriptFinalReceived += OnRealtimeUserTranscriptFinal;
                m_openAIRealtimeConversationSystem.UserTranscriptFinalReceivedWithLanguage += OnRealtimeUserTranscriptFinalWithLanguage;
                m_openAIRealtimeConversationSystem.AssistantTranscriptDeltaReceived += OnRealtimeAssistantTranscriptDelta;
                m_openAIRealtimeConversationSystem.AssistantTranscriptChunkReceived += OnRealtimeAssistantTranscriptChunk;
                m_openAIRealtimeConversationSystem.AssistantTranscriptFinalReceived += OnRealtimeAssistantTranscriptFinal;
                m_openAIRealtimeConversationSystem.AssistantAudioStarted += OnRealtimeAssistantAudioStarted;
                m_openAIRealtimeConversationSystem.AssistantAudioFinished += OnRealtimeAssistantAudioFinished;
                m_openAIRealtimeConversationSystem.UserSpeechStarted += OnRealtimeUserSpeechStarted;
                m_openAIRealtimeConversationSystem.UserSpeechEnded += OnRealtimeUserSpeechEnded;
                m_openAIRealtimeConversationSystem.ErrorReceived += OnRealtimeConversationError;
            }

            if (RideUtils.IsWebGL()) ChangeASR(AsrMode.AzureWebGL);
            else                     ChangeASR(AsrMode.OpenAI);
            if (m_chatGPTSystem != null) m_currentLLM = m_chatGPTSystem;
            else                         m_currentLLM = m_anthropicSystem;
            m_currentScripted = m_lexSystem;
            ChangeNlp(NlpMode.ChatGPT);
            if (m_elevenTextToSpeechSystem != null) ChangeTts(TtsMode.ElevenLabs);
            else if (m_piperTextToSpeechSystem != null) ChangeTts(TtsMode.Piper);
            else if (m_kokoroTextToSpeechSystem != null) ChangeTts(TtsMode.Kokoro);
            else if (m_xttsTextToSpeechSystem != null) ChangeTts(TtsMode.XTTS);
            else                                    ChangeTts(TtsMode.Polly);

#if UNITY_WEBGL
            // Turn off reflection probes for WebGL
            ReflectionProbe[] probes = FindObjectsByType<ReflectionProbe>(FindObjectsSortMode.None);
            foreach (ReflectionProbe probe in probes)
            {
                probe.gameObject.SetActive(false);
            }
#endif

            //Bind UI and collect characters (AR/Desktop specifics handled in overrides)
            m_demoControllerUI = BindUI();
            m_demoControllerUI.InitializeCanvasCamera();
            m_debugMenus = FindAnyObjectByType<DebugMenus>();
            CollectCharacters();
            AfterSystemsInitialized();

            // Pick the first already-active character if any
            foreach (var character in m_characters)
                if (character.gameObject.activeSelf) { SelectCharacter(character.name); break; }

            var onScreen = Systems.Get<DebugOnScreenLogVHAssets>();
            if (onScreen != null) onScreen.m_log.ShowLog(false);

            if (!RideIO.IsInternetConnectionAvailable())
                RideLog.LogError("Error: internet connection required for cloud services");
        }

        /// <summary>
        /// Changes the active Automatic Speech Recognition (ASR) system.
        /// </summary>
        /// <param name="mode">ASR mode.</param>
        public void ChangeASR(AsrMode mode)
        {
            ISpeechRecognitionSystem nextASR = null;

            if (mode == AsrMode.Azure) nextASR = m_azureSpeechRecognitionSystem;
            else if (mode == AsrMode.Windows) nextASR = m_windowsSpeechRecognitionSystem;
            else if (mode == AsrMode.AzureWebGL) nextASR = m_azureWebGLSpeechRecognitionSystem;
            else if (mode == AsrMode.OpenAI) nextASR = m_openAISpeechRecognitionSystem;
            else if (mode == AsrMode.FasterWhisper) nextASR = m_fasterWhisperSpeechRecognitionSystem;
            else if (mode == AsrMode.Gemini) nextASR = m_geminiSpeechRecognitionSystem;
#if RIDEVH_URP || RIDEVH_XR
            // else if (mode == AsrMode.Mobile) nextASR = m_mobileSpeechRecognitionSystem;
#endif
            else throw new NotImplementedException();

            if (nextASR == null)
            {
                Debug.LogWarning($"ASR mode '{mode}' is not available. Check that the provider system exists in RideSystemsCognition.");
                return;
            }

            if (m_asrMode != mode && !UsingRealtimeConversationMode)
                SetASR(false);

            m_asrMode = mode;

            if (UsingRealtimeConversationMode)
                return;

            m_currentASR = nextASR;
        }

        /// <summary>
        /// Changes the active Natural Language Processing (NLP) system.
        /// </summary>
        /// <param name="mode">NLP mode.</param>
        public void ChangeNlp(NlpMode mode)
        {
            m_nlpMode = mode;

            if (mode == NlpMode.ChatGPT) m_currentLLM = m_chatGPTSystem;
            else if (mode == NlpMode.Claude) m_currentLLM = m_anthropicSystem;
            else if (mode == NlpMode.Gemini) m_currentLLM = m_geminiSystem;
            else if (mode == NlpMode.AwsLex) m_currentScripted = m_lexSystem;
            else if (mode == NlpMode.Rasa) m_currentLLM = m_rasaNlpSystem;
            else if (mode == NlpMode.VLLM) m_currentLLM = m_vLLMSystem;
            else if (mode == NlpMode.Ollama) m_currentLLM = m_ollamaSystem;

            if (m_currentCharacter != null)
                ApplyCharacterPrompt(m_currentCharacter);
        }

        /// <summary>Hot-swaps the active Ollama model (no restart). No-op unless Ollama is the NLP system.</summary>
        public void ToggleOllamaModel()
        {
            if (m_ollamaSystem != null)
                m_ollamaSystem.ToggleModel();
        }

        /// <summary>The Ollama model id currently in use (for debug UI); empty if unavailable.</summary>
        public string OllamaActiveModelDisplay => m_ollamaSystem != null ? m_ollamaSystem.ActiveModelId : string.Empty;

        /// <summary>
        /// Sets the character prompt for LLM processing.
        /// </summary>
        /// <param name="character">The character whose prompt is being set.<see cref="MecanimCharacter"/></param>
        /// <param name="prompt">The text prompt to apply.</param>
        public void SetPrompt(MecanimCharacter character, string prompt = "")
            => ApplyCharacterPrompt(character, prompt);

        /// <summary>Raised when a user utterance (speech or typed) enters the conversation pipeline.</summary>
        public event Action<string> OnUserUtterance;

        /// <summary>Raised when the NLP system returns the character's response text.</summary>
        public event Action<string> OnCharacterResponse;

        /// <summary>
        /// Raised when a character has been fully selected and set up (after any async
        /// asset load completes). The study player subscribes to this to apply the study
        /// config (persona/voice) and speak the researcher's greeting, in place of the
        /// demo's built-in self-introduction. Concrete controllers call
        /// <see cref="RaiseCharacterReady"/> at the end of SelectCharacter.
        /// </summary>
        public event Action<MecanimCharacter> OnCharacterReady;

        /// <summary>Invokes <see cref="OnCharacterReady"/> (events can only be raised by the declaring type).</summary>
        protected void RaiseCharacterReady(MecanimCharacter character) => OnCharacterReady?.Invoke(character);

        /// <summary>
        /// Submits typed user text into the conversation pipeline, mirroring the
        /// speech-recognition path (UI transcript + NLP request). Used by external
        /// hosts such as the Study Wizard's web shell (StudyShellBridge).
        /// </summary>
        /// <param name="text">The user's text input.</param>
        public void SubmitUserText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            OnUserUtterance?.Invoke(text);
            m_demoControllerUI?.PopulateResponseUI("You", text);
            AskNLPQuestion(text);
            FindAnyObjectByType<DebugMenus>()?.SetNlpInput(text);
        }

        private static List<NlpInteraction> CloneHistory(List<NlpInteraction> history)
        {
            if (history == null)
                return new List<NlpInteraction>();

            return new List<NlpInteraction>(history);
        }

        protected void SaveCharacterNlpHistory(MecanimCharacter character)
        {
            string characterName = character != null ? character.name : null;
            if (string.IsNullOrEmpty(characterName))
                return;

            if (!m_characterConversationHistory.ContainsKey(characterName))
                m_characterConversationHistory[characterName] = new List<NlpInteraction>();
        }

        protected List<NlpInteraction> GetCharacterConversationHistory(MecanimCharacter character)
        {
            string characterName = character != null ? character.name : null;
            if (string.IsNullOrEmpty(characterName))
                return new List<NlpInteraction>();

            if (!m_characterConversationHistory.TryGetValue(characterName, out var history))
            {
                history = new List<NlpInteraction>();
                m_characterConversationHistory[characterName] = history;
            }

            return history;
        }

        private static List<NlpInteraction> BuildProviderHistory(string prompt, List<NlpInteraction> conversationHistory)
        {
            var providerHistory = new List<NlpInteraction>
            {
                new NlpInteraction { input = prompt }
            };
            providerHistory.AddRange(CloneHistory(conversationHistory));
            return providerHistory;
        }

        private void SetProviderHistory(NlpSystemUnity system, string prompt, List<NlpInteraction> conversationHistory)
        {
            if (system == null)
                return;

            system.SetHistory(BuildProviderHistory(prompt, conversationHistory));
            system.SetSystemPrompt(prompt);
        }

        protected bool ApplyCharacterPrompt(MecanimCharacter character, string prompt = "")
        {
            if (character == null)
                return false;

            var profile = character.GetComponent<VHCharacterProfile>();
            if (profile == null)
                return false;

            if (!string.IsNullOrEmpty(prompt))
                profile.llmPrompt = prompt;

            var conversationHistory = GetCharacterConversationHistory(character);

            if (m_nlp != null)
                m_nlp.SetUIPrompt(profile.llmPrompt);

            SetProviderHistory(m_currentLLM, profile.llmPrompt, conversationHistory);
            return true;
        }

        public void AskNLPQuestion(string q)
            => AskNLPQuestion(q, UserInputSourceText);

        public void AskNLPQuestion(string q, string inputSource)
        {
            if (UsingRealtimeConversationMode)
            {
                BindRealtimeConversationCharacter();
                if (m_openAIRealtimeConversationSystem == null)
                    return;

                StopUtterance();
                ResetRealtimeAssistantState();
                SetCharacterConfigUIEnabled(false);
                if (m_thinkingController != null)
                    m_thinkingController.StartThinkingBehavior(true);

                DetectLanguageForUserText(q, inputSource, () => m_openAIRealtimeConversationSystem.SubmitText(q));
                return;
            }

            StopUtterance();                    // Stop current character behaviors
            SetCharacterConfigUIEnabled(false); // Don't allow character change while interaction is processing and executing
            if (m_thinkingController != null)   // Start character thinking nonverbal behaviors after a small delay
                m_thinkingController.StartThinkingBehavior(true);

            m_lastNlpInput = q;
            int generation = ++m_classicPipelineGeneration;
            DetectLanguageForUserText(q, inputSource, () =>
            {
                if (generation != m_classicPipelineGeneration)
                    return;

                if (m_nlpMode == NlpMode.AwsLex) m_currentScripted.Request(new NlpRequest(q), response => QuestionResponse(response, generation));
                else m_currentLLM.Request(new NlpRequest(q), response => QuestionResponse(response, generation));
            });
        }

        /// <summary>
        /// Sends a string response through the application UI and systems.
        /// </summary>
        /// <param name="response">The response text to handle.</param>
        public void SendResponse(string response) => OnNlpResponseReceived(response);

        /// <summary>
        /// Changes the active TTS system and sets the voice for the current character.
        /// </summary>
        /// <param name="mode">TTS mode.</param>
        public void ChangeTts(TtsMode mode)
        {
            m_ttsMode = mode;

            if (mode == TtsMode.Polly) m_currentTTS = m_awsPollyTextToSpeechSystem;
            else if (mode == TtsMode.ElevenLabs) m_currentTTS = m_elevenTextToSpeechSystem;
            else if (mode == TtsMode.Piper) m_currentTTS = m_piperTextToSpeechSystem;
            else if (mode == TtsMode.Kokoro) m_currentTTS = m_kokoroTextToSpeechSystem;
            else if (mode == TtsMode.XTTS) m_currentTTS = m_xttsTextToSpeechSystem;
            else if (mode == TtsMode.Gemini) m_currentTTS = m_geminiTextToSpeechSystem;

            // A provider whose service was down at startup fell back to a single voice. Selecting it
            // now is the natural moment to look again: the container may have been started since.
            (m_currentTTS as TextToSpeechSystemUnity)?.RefreshVoicesIfUnavailable();

            m_isCurrentCharacterVoiceReady = false;
            SetCharacterVoice(m_currentTTS, m_currentCharacter);
        }

        /// <summary>
        /// Sets the voice used by the character from the current TTS system.
        /// </summary>
        /// <param name="ttsSystem">The TTS system to get the voice from.<see cref="ILipsyncedTextToSpeechSystem"/></param>
        /// <param name="character">The character to assign the voice to.<see cref="MecanimCharacter"/></param>
        public void SetCharacterVoice(ILipsyncedTextToSpeechSystem ttsSystem, MecanimCharacter character)
        {
            int assignmentToken = ++m_ttsVoiceAssignmentToken;
            if (!TryApplyCharacterVoice(ttsSystem, character, assignmentToken))
                StartCoroutine(SetCharacterVoiceCoroutine(ttsSystem, character, assignmentToken));
        }

        private string GetCharacterVoiceName(ILipsyncedTextToSpeechSystem ttsSystem, MecanimCharacter character)
        {
            if (ttsSystem == null || character == null)
                return string.Empty;

            var profile = character.GetComponent<VHCharacterProfile>();
            if (profile == null)
                return string.Empty;

            if ((object)ttsSystem == m_awsPollyTextToSpeechSystem)
                return profile.PollyVoiceName;
            if ((object)ttsSystem == m_elevenTextToSpeechSystem)
                return profile.ElevenLabVoiceName;
            if ((object)ttsSystem == m_piperTextToSpeechSystem)
                return profile.PiperVoiceName;
            if ((object)ttsSystem == m_kokoroTextToSpeechSystem)
                return profile.KokoroVoiceName;
            if ((object)ttsSystem == m_xttsTextToSpeechSystem)
                return profile.XTTSVoiceName;
            if ((object)ttsSystem == m_geminiTextToSpeechSystem)
                return profile.GeminiVoiceName;

            return string.Empty;
        }

        private bool TryApplyCharacterVoice(ILipsyncedTextToSpeechSystem ttsSystem, MecanimCharacter character, int assignmentToken)
        {
            if (ttsSystem == null || character == null)
                return false;
            if (assignmentToken != m_ttsVoiceAssignmentToken)
                return false;
            if (!ReferenceEquals(ttsSystem, m_currentTTS))
                return false;
            if (!ReferenceEquals(character, m_currentCharacter))
                return false;

            var voices = ttsSystem.GetAvailableVoices();
            if (voices == null || voices.Length == 0)
                return false;

            string voiceName = GetCharacterVoiceName(ttsSystem, character);
            int idx = -1;
            if (!string.IsNullOrEmpty(voiceName))
                idx = ttsSystem.GetVoiceIndex(voiceName);

            m_ttsVoice = (idx >= 0 && idx < voices.Length) ? idx : 0;
            m_isCurrentCharacterVoiceReady = true;
            return true;
        }

        /// <summary>
        /// Coroutine to wait until voices are loaded and apply the voice by name.
        /// </summary>
        /// <param name="ttsSystem">The TTS system.<see cref="ILipsyncedTextToSpeechSystem"/></param>
        /// <param name="character">The character to apply the voice to.<see cref="MecanimCharacter"/></param>
        /// <returns>Coroutine enumerator.</returns>
        protected IEnumerator SetCharacterVoiceCoroutine(ILipsyncedTextToSpeechSystem ttsSystem, MecanimCharacter character, int assignmentToken)
        {
            if (ttsSystem == null || character == null)
                yield break;

            yield return null;
            yield return null;
            float deadline = Time.realtimeSinceStartup + TtsVoiceAssignmentTimeoutSeconds;
            while (assignmentToken == m_ttsVoiceAssignmentToken &&
                   ReferenceEquals(ttsSystem, m_currentTTS) &&
                   ReferenceEquals(character, m_currentCharacter))
            {
                if (TryApplyCharacterVoice(ttsSystem, character, assignmentToken))
                    yield break;

                if (Time.realtimeSinceStartup >= deadline)
                {
                    OnCharacterVoiceAssignmentFailed(character, ttsSystem);
                    yield break;
                }

                yield return null;
            }
        }

        private void OnCharacterVoiceAssignmentFailed(MecanimCharacter character, ILipsyncedTextToSpeechSystem ttsSystem)
        {
            if (!ReferenceEquals(character, m_currentCharacter) || !ReferenceEquals(ttsSystem, m_currentTTS))
                return;

            m_isCurrentCharacterVoiceReady = false;
            Debug.LogWarning($"[{nameof(DemoControllerBase)}] Timed out assigning TTS voice for character '{character.name}' using provider '{ttsSystem.GetType().Name}'.");
        }

        /// <summary>
        /// Limits an utterance to what the character will actually say out loud, cutting at the last
        /// sentence end that fits rather than at an exact character count, so speech never stops
        /// part-way through a word. When text is dropped the character says so, because a listener
        /// otherwise cannot tell a deliberate ending from a truncated one. Returns the text unchanged
        /// when it already fits.
        /// </summary>
        /// <param name="utterance">The text a character is about to speak.</param>
        /// <returns>The utterance, shortened to a sentence boundary if it was too long.</returns>
        protected string TrimToSpokenLength(string utterance)
        {
            int limit = GetSpokenCharacterLimit();
            if (string.IsNullOrEmpty(utterance) || utterance.Length <= limit)
                return utterance;

            // The notice is spoken too, so it has to fit inside the same limit.
            string notice = m_lengthCutoffNotice ?? string.Empty;
            if (notice.Length >= limit)
                notice = string.Empty;

            string trimmed = utterance[..(limit - notice.Length)];

            int sentenceEnd = trimmed.LastIndexOfAny(new[] { '.', '!', '?' });
            if (sentenceEnd > 0)
                trimmed = trimmed[..(sentenceEnd + 1)];
            else
            {
                int wordEnd = trimmed.LastIndexOf(' ');
                if (wordEnd > 0)
                    trimmed = trimmed[..wordEnd];
            }

            int providerLimit = GetProviderCharacterLimit();
            string boundBy = providerLimit < m_maxSpokenCharacters
                ? $"the {m_currentTTS?.GetType().Name} limit of {providerLimit}"
                : $"the application limit of {m_maxSpokenCharacters}";

            Debug.LogWarning($"[{nameof(DemoControllerBase)}] Response was {utterance.Length} characters, " +
                $"over {boundBy}; spoken text trimmed to {trimmed.Length} at a sentence boundary and the " +
                $"listener told it was cut short. Tighten the character's style prompt if this recurs.");

            return trimmed + notice;
        }

        /// <summary>
        /// The most characters the current configuration can actually speak: the application's own
        /// limit, reduced to the active speech provider's per-request maximum when that is smaller.
        /// Speech services reject over-long text rather than shortening it, so the lower bound wins.
        /// </summary>
        /// <returns>The effective spoken-character limit.</returns>
        protected int GetSpokenCharacterLimit() => Mathf.Min(m_maxSpokenCharacters, GetProviderCharacterLimit());

        /// <summary>
        /// The per-request character maximum of the active speech provider, or no limit when the
        /// provider does not declare one.
        /// </summary>
        /// <returns>The provider's character limit.</returns>
        protected int GetProviderCharacterLimit()
            => (m_currentTTS as TextToSpeechSystemUnity)?.MaxRequestCharacters ?? int.MaxValue;

        /// <summary>
        /// Replaces words the speech synthesizer mispronounces with their phonetic respellings from
        /// <see cref="m_pronunciationOverrides"/>. Matching is whole-word and case-sensitive, so an
        /// acronym entry does not collide with an ordinary word spelled with the same letters.
        /// </summary>
        /// <param name="utterance">The text a character is about to speak.</param>
        /// <returns>The utterance with mispronounced words respelled for the synthesizer.</returns>
        protected string ApplyPronunciationOverrides(string utterance)
        {
            if (string.IsNullOrEmpty(utterance))
                return utterance;

            foreach (var (written, spoken) in m_pronunciationOverrides)
                utterance = Regex.Replace(utterance, $@"\b{Regex.Escape(written)}\b", spoken);

            return utterance;
        }

        /// <summary>
        /// Generates text-to-speech audio for the given utterance.
        /// </summary>
        /// <param name="utterance">The spoken text to convert to speech.</param>
        public void CreateTTS(string utterance)
        {
            if (string.IsNullOrEmpty(utterance)) return;
            utterance = ApplyPronunciationOverrides(utterance);
            utterance = TrimToSpokenLength(utterance);
            int generation = m_classicPipelineGeneration;
            if (!m_isCurrentCharacterVoiceReady || m_currentTTS == null)
            {
                HandleMissingVoice();
                return;
            }

            var voices = m_currentTTS.GetAvailableVoices();
            if (voices == null || voices.Length == 0 || m_ttsVoice < 0 || m_ttsVoice >= voices.Length)
            {
                HandleMissingVoice();
                return;
            }

            m_currentTTS.CreateTextToSpeech(voices[m_ttsVoice], utterance, (lipsyncXML, audioFilePath) => OnTtsGenerated(lipsyncXML, audioFilePath, generation));
        }

        private void HandleMissingVoice()
        {
            StopThinkingBehaviorAndRestoreUserGaze();

            m_demoControllerUI?.PopulateResponseUI("VH", MissingVoiceMessage);
            SetCharacterConfigUIEnabled(true);
        }

        /// <summary>
        /// Stops the current character's audio and lipsync playback.
        /// </summary>
        public virtual void StopUtterance()
        {
            if (UsingRealtimeConversationMode)
            {
                m_openAIRealtimeConversationSystem?.InterruptAssistant();
                m_streamingLipsyncSystem?.Interrupt();
            }

            if (CurrentCharacter != null)
            {
                CurrentCharacter.StopLipSyncPerformance();
                CurrentCharacter.StopAudio();
            }

            if (m_realtimeOutputAudioSource != null)
            {
                m_realtimeOutputAudioSource.Stop();
                m_realtimeOutputAudioSource.clip = null;
            }

            StopNonRealtimeCharacterAudioSources();

            SetCharacterConfigUIEnabled(true);
        }

        /// <summary>
        /// Called when the NLP system returns a response.
        /// </summary>
        /// <param name="response">The text response from the NLP system.</param>
        protected void OnNlpResponseReceived(string response)
        {
            if (UsingRealtimeConversationMode)
                return;

            // Trim once, here, so speech, nonverbal behavior and the transcript all describe the
            // same utterance. Trimming inside the speech path alone leaves the behavior schedule
            // running past the end of the audio that exists.
            m_response = TrimToSpokenLength(response);
            OnCharacterResponse?.Invoke(m_response);
            m_demoControllerUI?.PopulateResponseUI("VH", m_response);
            CreateTTS(m_response);
            FindAnyObjectByType<DebugMenus>()?.SetNlpResponse(m_response);
        }

        /// <summary>
        /// Handles recognized speech input and forwards it to the NLP system.
        /// </summary>
        /// <param name="sender">The sender of the speech recognition event.</param>
        /// <param name="e">The speech recognition result event arguments.</param>
        /// <see cref="SpeechRecognizedEventArgs"/>
        protected void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (UsingRealtimeConversationMode)
                return;

            SetLatestUserLanguage(e.Language, GetCurrentAsrLanguageSourceLabel());
            OnUserUtterance?.Invoke(e.Text);
            m_demoControllerUI?.PopulateResponseUI("You", e.Text);
            AskNLPQuestion(e.Text, UserInputSourceAsr);
            FindAnyObjectByType<DebugMenus>()?.SetNlpInput(e.Text);
        }

        /// <summary>
        /// Callback for when the TTS system has finished generating audio and lipsync XML.
        /// </summary>
        /// <param name="lipsyncXML">The lipsync XML data.</param>
        /// <param name="audioFilePath">The file path to the generated audio.</param>
        protected void OnTtsGenerated(string lipsyncXML, string audioFilePath)
            => OnTtsGenerated(lipsyncXML, audioFilePath, m_classicPipelineGeneration);

        protected void OnTtsGenerated(string lipsyncXML, string audioFilePath, int generation)
        {
            if (generation != m_classicPipelineGeneration || UsingRealtimeConversationMode)
                return;

            if (UsingRealtimeConversationMode)
                return;

            if (string.IsNullOrWhiteSpace(audioFilePath))
            {
                Debug.LogError($"[{nameof(DemoControllerBase)}] TTS generation failed; audio file path is empty.");
                StopThinkingBehaviorAndRestoreUserGaze();
                SetCharacterConfigUIEnabled(true);
                return;
            }

            m_audioFilePath = audioFilePath;
            m_lipsyncXML = lipsyncXML;
            GenerateNonverbalBehavior(m_currentCharacter, m_response);
        }
        /// <summary>
        /// Generates nonverbal behavior data from text for a given character.
        /// </summary>
        /// <param name="character">The character to animate.<see cref="MecanimCharacter"/></param>
        /// <param name="utterance">The utterance text to analyze.</param>
        public void GenerateNonverbalBehavior(MecanimCharacter character, string utterance)
        {
            int generation = m_classicPipelineGeneration;
            string languageTag = GetEffectiveNvbgLanguageTag();
            Debug.Log($"[NVBG] Classic request language='{(string.IsNullOrWhiteSpace(languageTag) ? "en/fallback" : languageTag)}' character='{character.CharacterName}'");
            m_nvbgSystem.GetNonverbalBehavior(character.CharacterName, utterance, languageTag, result => OnNvbgGenerated(result, generation));
        }

        public void ChangeConversationMode(ConversationMode mode)
        {
            if (m_conversationMode == mode)
            {
                if (UsingRealtimeConversationMode)
                    BindRealtimeConversationCharacter();
                return;
            }

            SetASR(false);
            StopUtterance();

            if (m_conversationMode == ConversationMode.Unified)
            {
                m_openAIRealtimeConversationSystem?.DeactivateConversation();
                m_streamingLipsyncSystem?.Interrupt();
                ResetRealtimeAssistantState();
                m_realtimeBoundCharacter = null;
            }

            m_conversationMode = mode;

            if (UsingRealtimeConversationMode)
            {
                ClearClassicSpeechPipelineState();
                BindRealtimeConversationCharacter();
                m_currentASR = m_openAIRealtimeConversationSystem;
            }
            else
            {
                ChangeASR(m_asrMode);
            }
        }

        /// <summary>
        /// Callback for when NVBG system completes processing.
        /// Loads audio and begins playback.
        /// </summary>
        /// <param name="result">The nonverbal behavior output string.</param>
        protected void OnNvbgGenerated(string result)
            => OnNvbgGenerated(result, m_classicPipelineGeneration);

        protected void OnNvbgGenerated(string result, int generation)
        {
            if (generation != m_classicPipelineGeneration || UsingRealtimeConversationMode)
                return;

            if (UsingRealtimeConversationMode)
                return;

            if (string.IsNullOrWhiteSpace(m_audioFilePath))
            {
                Debug.LogError($"[{nameof(DemoControllerBase)}] Cannot load TTS audio; path is empty.");
                StopThinkingBehaviorAndRestoreUserGaze();
                SetCharacterConfigUIEnabled(true);
                return;
            }

            var audio = Systems.Get<AudioSystemUnity>();
            m_audioClip = null;
            audio.LoadAudioFile(m_audioFilePath, clip =>
            {
                if (generation != m_classicPipelineGeneration || UsingRealtimeConversationMode)
                    return;

                m_audioClip = clip;
                StartCoroutine(PlayUtterance(result, generation));
            });
        }

        /// <summary>
        /// Plays the audio utterance with lipsync and nonverbal behavior.
        /// </summary>
        /// <param name="nvbgResult">The nonverbal behavior animation data.</param>
        /// <returns>Coroutine enumerator.</returns>
        protected IEnumerator PlayUtterance(string nvbgResult)
            => PlayUtterance(nvbgResult, m_classicPipelineGeneration);

        protected IEnumerator PlayUtterance(string nvbgResult, int generation)
        {
            if (generation != m_classicPipelineGeneration || UsingRealtimeConversationMode)
                yield break;

            if (UsingRealtimeConversationMode)
                yield break;

            string facefx = " ";
            if (!string.IsNullOrEmpty(m_lipsyncXML))
            {
                string xml = m_lipsyncXML.Substring(m_lipsyncXML.IndexOf('<'));
                var tts = m_ttsReader.ReadTtsXml(xml, out _);
                facefx = VisemeFormatConverter.ConvertTtsToFaceFx(tts);
            }

            yield return new WaitUntil(() => m_audioClip != null);

            if (generation != m_classicPipelineGeneration || UsingRealtimeConversationMode)
                yield break;

            var ttsFile = AudioSpeechFile.CreateAudioSpeechFile(facefx, nvbgResult, m_audioClip);
            MecanimManager.Get().FindAudioFiles();
            CurrentCharacter.PlayAudio(ttsFile);
            CurrentCharacter.PlayXml(ttsFile);

            StopThinkingBehaviorAndRestoreUserGaze();

            SetCharacterConfigUIEnabled(false);

            float waitTime = Math.Max(ttsFile.ClipLength - 0.2f, 0.1f);  // Wait until near the end of the audio clip
            yield return new WaitForSeconds(waitTime);

            SetCharacterConfigUIEnabled(true);
        }

        protected void RecordCharacterConversationTurn(string response)
        {
            if (m_currentCharacter == null || string.IsNullOrEmpty(m_lastNlpInput))
                return;

            var history = GetCharacterConversationHistory(m_currentCharacter);
            history.Add(new NlpInteraction
            {
                input = m_lastNlpInput,
                response = response,
                inputTimestamp = DateTime.Now,
                responseTimestamp = DateTime.Now
            });
            m_lastNlpInput = null;
        }

        /// <summary>
        /// Receives the response from the NLP and processes it.
        /// </summary>
        /// <param name="response">The NLP response data.</param><see cref="NlpResponse"/>
        protected void QuestionResponse(NlpResponse response) => QuestionResponse(response, m_classicPipelineGeneration);

        protected void QuestionResponse(NlpResponse response, int generation)
        {
            if (generation != m_classicPipelineGeneration || UsingRealtimeConversationMode)
                return;

            // Deflected turns never reached the LLM; keep the flagged input out of the
            // per-character history too, since that history is replayed into LLM context
            // on character switch (see SetProviderHistory).
            if (response.guardDisposition == GuardDisposition.Deflected)
            {
                m_lastNlpInput = null;
                SendResponse(response.content[0]);
                return;
            }

            RecordCharacterConversationTurn(response.content[0]);
            SendResponse(response.content[0]);
        }

        protected abstract IDemoControllerUI BindUI();
        protected abstract void CollectCharacters();                   //AR: direct children; Desktop: nested

        public abstract void SelectCharacter(string characterName);  //gaze target and catalog differences
        protected virtual void AfterSystemsInitialized() { }           //cameras, catalogs, etc.

        protected override void Update()
        {
            base.Update();

            if (UsingRealtimeConversationMode)
                BindRealtimeConversationCharacter();

            if (UsingRealtimeConversationMode && m_streamingLipsyncSystem != null && m_openAIRealtimeConversationSystem != null)
            {
                m_streamingLipsyncSystem.SetPlaybackSeconds(m_openAIRealtimeConversationSystem.AssistantPlaybackSeconds);
                m_streamingLipsyncSystem.SetAudioLevel(m_openAIRealtimeConversationSystem.AssistantOutputLevel);
            }

            ApplyAsrState();
            UpdateAsrButtonColor();
            UpdateNextCharacterButtonColor();
        }

        protected abstract void UpdateAsrButtonColor();

        protected abstract void UpdateNextCharacterButtonColor();


        /// <summary>
        /// Toggles the desired Automatic Speech Recognition (ASR) state.
        ///
        /// This represents a user intent change (e.g., clicking the mic button),
        /// not an immediate guarantee that recognition will start or stop.
        /// Actual recognition is gated by runtime conditions such as whether
        /// the character is speaking, a microphone is selected, or the character
        /// configuration UI is enabled.
        ///
        /// If ASR is currently not interactable, this call is ignored.
        /// </summary>
        public virtual void ToggleASR()
        {
            if (!IsAsrToggleInteractable())
                return;

            SetASR(!m_asrDesiredEnabled);
        }

        /// <summary>
        /// Sets the desired Automatic Speech Recognition (ASR) state explicitly.
        ///
        /// This allows callers to force ASR on or off without relying on toggle
        /// semantics. Disabling is always allowed; enabling is subject to the same
        /// interaction constraints as <see cref="ToggleASR"/>.
        ///
        /// The requested state is stored and applied opportunistically when
        /// runtime conditions allow (see <see cref="ApplyAsrState"/>).
        /// </summary>
        /// <param name="enabled">
        /// True to request ASR be enabled; false to request it be disabled.
        /// </param>
        public virtual void SetASR(bool enabled)
        {
            // Always allow disabling.
            if (enabled && !IsAsrToggleInteractable())
                return;

            m_asrDesiredEnabled = enabled;
            ApplyAsrState();
        }

        /// <summary>
        /// Reconciles the desired ASR state with the current runtime conditions
        /// and starts or stops speech recognition as needed.
        ///
        /// This method is the single authority that decides whether ASR should
        /// actually be recognizing at this moment. It compares:
        ///   - the user's desired ASR state, and
        ///   - whether recognition is currently allowed
        ///
        /// and issues StartRecognizing / StopRecognizing calls only when a
        /// transition is required.
        ///
        /// This method is safe to call repeatedly and is typically invoked
        /// from Update().
        /// </summary>
        private void ApplyAsrState()
        {
            if (m_currentASR == null)
                return;

            bool shouldRecognize = m_asrDesiredEnabled && IsAsrAllowedToRecognize();
            if (m_currentASR.IsRecognizing == shouldRecognize)
                return;

            if (shouldRecognize)
                m_currentASR.StartRecognizing();
            else
                m_currentASR.StopRecognizing();
        }

        /// <summary>
        /// Determines whether ASR is currently allowed to actively recognize speech.
        ///
        /// This reflects transient runtime conditions that may temporarily block
        /// recognition even if the user has requested ASR to be enabled. Examples
        /// include:
        ///   - the character is speaking,
        ///   - the character configuration UI is disabled,
        ///   - no microphone is selected,
        ///   - or no character is active.
        ///
        /// This method does not consider user intent; it only answers whether
        /// recognition is permitted right now.
        /// </summary>
        /// <returns>
        /// True if ASR may actively recognize speech; otherwise false.
        /// </returns>
        protected virtual bool IsAsrAllowedToRecognize()
        {
            if (UsingRealtimeConversationMode)
            {
                if (CurrentCharacter == null || m_currentASR == null)
                    return false;

                if (!m_characterConfigUIEnabled)
                    return false;

                if (m_openAIRealtimeConversationSystem != null && m_openAIRealtimeConversationSystem.IsAssistantSpeaking)
                    return false;

                return !string.IsNullOrEmpty(m_currentASR.SelectedMicrophone);
            }

            if (!m_characterConfigUIEnabled)
                return false;

            if (CurrentCharacter == null)
                return false;

            var voice = CurrentCharacter.Voice;
            if (voice != null && voice.isPlaying)
                return false;

            if (string.IsNullOrEmpty(m_currentASR.SelectedMicrophone))
                return false;

            return true;
        }

        /// <summary>
        /// Determines whether the ASR toggle control should be considered
        /// interactable by the user.
        ///
        /// This is used to gate user input (e.g., mic button presses) and may
        /// be stricter than <see cref="IsAsrAllowedToRecognize"/>. For example,
        /// ASR might be temporarily blocked from recognizing, but still allow
        /// the user to change their desired state, depending on policy.
        ///
        /// The default implementation disables interaction when the same
        /// conditions that block recognition are present.
        /// </summary>
        /// <returns>
        /// True if the ASR toggle should respond to user input; otherwise false.
        /// </returns>
        protected virtual bool IsAsrToggleInteractable()
        {
            if (m_currentASR == null)
                return false;

            if (UsingRealtimeConversationMode)
            {
                if (CurrentCharacter == null)
                    return false;

                if (m_openAIRealtimeConversationSystem != null && m_openAIRealtimeConversationSystem.IsAssistantSpeaking)
                    return false;

                return !string.IsNullOrEmpty(m_currentASR.SelectedMicrophone);
            }

            if (!m_characterConfigUIEnabled)
                return false;

            if (CurrentCharacter == null)
                return false;

            var voice = CurrentCharacter.Voice;
            if (voice != null && voice.isPlaying)
                return false;

            if (string.IsNullOrEmpty(m_currentASR.SelectedMicrophone))
                return false;

            return true;
        }

        public enum LipsyncOptions { VH = 0, OVR = 1, }
        public virtual void SetLipsyncMethod(LipsyncOptions method) { }

        protected void SetCharacterConfigUIEnabled(bool enabled) => m_characterConfigUIEnabled = enabled;

        protected void BindRealtimeConversationCharacter()
        {
            if (!UsingRealtimeConversationMode || m_openAIRealtimeConversationSystem == null || CurrentCharacter == null)
                return;

            if (m_realtimeBoundCharacter == CurrentCharacter)
                return;

            var profile = CurrentCharacter.GetComponent<VHCharacterProfile>();
            string prompt = profile != null ? profile.llmPrompt : string.Empty;
            string openAIRealtimeVoice = profile != null ? profile.OpenAIRealtimeVoiceName : string.Empty;
            CurrentCharacter.StopAudio();
            if (CurrentCharacter.Voice != null)
                CurrentCharacter.Voice.clip = null;

            m_realtimeOutputAudioSource = GetRealtimeOutputAudioSource(CurrentCharacter);
            m_openAIRealtimeConversationSystem.ConfigureCharacter(CurrentCharacter.CharacterName, prompt, openAIRealtimeVoice, m_realtimeOutputAudioSource);
            m_streamingLipsyncSystem?.BindTarget(CurrentCharacter);
            m_realtimeBoundCharacter = CurrentCharacter;
        }

        protected AudioSource GetRealtimeOutputAudioSource(MecanimCharacter character)
        {
            if (character == null)
                return null;

            Transform existing = character.transform.Find("OpenAIRealtimeVoice");
            if (existing != null)
                Destroy(existing.gameObject);

            AudioSource realtimeSource = character.Voice;
            if (realtimeSource == null)
                return null;

            realtimeSource.Stop();
            realtimeSource.clip = null;
            realtimeSource.enabled = true;
            return realtimeSource;
        }

        protected void OnRealtimeUserTranscriptFinal(string text)
        {
            if (!UsingRealtimeConversationMode || string.IsNullOrWhiteSpace(text))
                return;

            ResetRealtimeAssistantState();
            m_demoControllerUI?.PopulateResponseUI("You", text);
            m_debugMenus?.SetNlpInput(text);
        }

        protected void OnRealtimeUserTranscriptFinalWithLanguage(string text, string languageTag)
        {
            if (!UsingRealtimeConversationMode)
                return;

            SetLatestUserLanguage(languageTag, "OpenAI Realtime ASR (no explicit language tag)");
            DetectLanguageForUserText(text, UserInputSourceRealtimeAsr, null);
        }

        protected void OnRealtimeUserSpeechStarted()
        {
            if (!UsingRealtimeConversationMode)
                return;

            StopRealtimeThinkingBehavior();
        }

        protected void OnRealtimeUserSpeechEnded()
        {
            if (!UsingRealtimeConversationMode)
                return;

            if (m_thinkingController != null)
                m_thinkingController.StartThinkingBehavior(true);
        }

        protected void OnRealtimeAssistantTranscriptChunk(string chunk)
        {
            if (!UsingRealtimeConversationMode || string.IsNullOrWhiteSpace(chunk))
                return;

            m_realtimeAssistantResponse += chunk;
            m_response = m_realtimeAssistantResponse;

            m_realtimePendingAssistantUiText += chunk;
            if (ShouldFlushRealtimeAssistantUiChunk(chunk, m_realtimePendingAssistantUiText))
            {
                m_demoControllerUI?.PopulateResponseUI("VH", m_realtimePendingAssistantUiText.Trim());
                m_realtimePendingAssistantUiText = string.Empty;
            }

            m_debugMenus?.SetNlpResponse(m_realtimeAssistantResponse);
        }

        protected void OnRealtimeAssistantTranscriptDelta(string delta)
        {
            if (!UsingRealtimeConversationMode || string.IsNullOrWhiteSpace(delta) || m_streamingLipsyncSystem == null || m_openAIRealtimeConversationSystem == null)
                return;

            m_realtimePendingLipsyncText += delta;
            if (!ShouldFlushRealtimeLipsyncChunk(m_realtimePendingLipsyncText))
                return;

            FlushRealtimeLipsyncChunk();
        }

        protected void OnRealtimeAssistantTranscriptFinal(string text)
        {
            if (!UsingRealtimeConversationMode)
                return;

            if (!string.IsNullOrWhiteSpace(m_realtimePendingAssistantUiText))
            {
                m_demoControllerUI?.PopulateResponseUI("VH", m_realtimePendingAssistantUiText.Trim());
                m_realtimePendingAssistantUiText = string.Empty;
            }

            m_realtimeAssistantResponse = text ?? string.Empty;
            m_response = m_realtimeAssistantResponse;
            m_debugMenus?.SetNlpResponse(m_realtimeAssistantResponse);
            FlushRealtimeLipsyncChunk();
            GenerateRealtimeNonverbalBehavior(m_realtimeAssistantResponse);
        }

        protected void OnRealtimeAssistantAudioStarted()
        {
            if (!UsingRealtimeConversationMode)
                return;

            StopNonRealtimeCharacterAudioSources();
            StopNonRealtimeSceneAudioSources();
            m_streamingLipsyncSystem?.NotifyAudioPlaybackStarted();

            StopRealtimeThinkingBehavior();

            SetCharacterConfigUIEnabled(false);
        }

        protected void OnRealtimeAssistantAudioFinished()
        {
            if (!UsingRealtimeConversationMode)
                return;

            FlushRealtimeLipsyncChunk();
            m_streamingLipsyncSystem?.NotifyAudioPlaybackFinished();
            SetCharacterConfigUIEnabled(true);
            m_realtimeLastChunkPlaybackSeconds = 0f;
        }

        protected void OnRealtimeConversationError(string error)
        {
            Debug.LogError($"[OpenAI Realtime] {error}");
            if (UsingRealtimeConversationMode)
            {
                ResetRealtimeAssistantState();
                StopRealtimeThinkingBehavior();
                SetCharacterConfigUIEnabled(true);
            }
        }

        protected void StopRealtimeThinkingBehavior() => StopThinkingBehaviorAndRestoreUserGaze();

        /// <summary>
        /// Ends thinking behavior and returns the character's gaze to the user.
        /// </summary>
        /// <remarks>
        /// Thinking behavior repeatedly retargets gaze to "glance away" targets and does not
        /// restore the previous target when it ends, so the gaze must be aimed back at the user
        /// explicitly once the character is ready to address them again. Without this the
        /// character remains fixed on whichever target it glanced at last.
        /// </remarks>
        private void StopThinkingBehaviorAndRestoreUserGaze()
        {
            if (m_thinkingController == null)
                return;

            if (CurrentCharacter != null)
            {
                if (GameObject.Find(RealtimeUserGazeTargetName) != null)
                    CurrentCharacter.Gaze(RealtimeUserGazeTargetName, 90f);
                else
                    CurrentCharacter.StopGaze(0.2f);
            }

            m_thinkingController.StopThinkingBehavior();
        }

        protected void ResetRealtimeAssistantState()
        {
            m_realtimeAssistantResponse = string.Empty;
            m_realtimePendingAssistantUiText = string.Empty;
            m_realtimePendingLipsyncText = string.Empty;
            m_realtimeLastChunkPlaybackSeconds = 0f;
            m_streamingLipsyncSystem?.ResetStream();
        }

        protected void ClearClassicSpeechPipelineState()
        {
            m_classicPipelineGeneration++;
            m_audioClip = null;
            m_audioFilePath = null;
            m_lipsyncXML = null;
            m_response = null;
            if (m_currentCharacter != null)
            {
                m_currentCharacter.StopAudio();
                m_currentCharacter.StopLipSyncPerformance();
                m_currentCharacter.StopAnim();
            }

            StopNonRealtimeCharacterAudioSources();
        }

        protected void StopNonRealtimeCharacterAudioSources()
        {
            if (CurrentCharacter == null)
                return;

            var sources = CurrentCharacter.GetComponentsInChildren<AudioSource>(true);
            foreach (var source in sources)
            {
                if (source == null || source == m_realtimeOutputAudioSource)
                    continue;

                source.Stop();
                source.clip = null;
            }
        }

        protected void StopNonRealtimeSceneAudioSources()
        {
            var allSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var source in allSources)
            {
                if (source == null || source == m_realtimeOutputAudioSource)
                    continue;

                source.Stop();
                source.clip = null;
            }
        }

        protected static bool ShouldFlushRealtimeAssistantUiChunk(string latestChunk, string pendingText)
        {
            if (string.IsNullOrWhiteSpace(pendingText))
                return false;

            latestChunk = latestChunk?.TrimEnd() ?? string.Empty;
            if (latestChunk.EndsWith(".") || latestChunk.EndsWith("!") || latestChunk.EndsWith("?"))
                return true;

            return pendingText.Length >= 120;
        }

        private static string SanitizeRealtimeNvbgXml(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return xml;

            return Regex.Replace(
                xml,
                "<speech\\b([^>]*?)\\bref\\s*=\\s*\"[^\"]*\"",
                "<speech$1 ref=\"realtime_stream_silent\"",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }

        protected void GenerateRealtimeNonverbalBehavior(string text)
        {
            if (!UsingRealtimeConversationMode || m_nvbgSystem == null || CurrentCharacter == null || string.IsNullOrWhiteSpace(text))
                return;

            var character = CurrentCharacter;
            string characterName = character.CharacterName;
            string utterance = Regex.Replace(text.Trim(), "\\s+", " ");
            string languageTag = GetEffectiveNvbgLanguageTag();

            Debug.Log($"[NVBG] Realtime request language='{(string.IsNullOrWhiteSpace(languageTag) ? "en/fallback" : languageTag)}' character='{characterName}'");
            m_nvbgSystem.GetNonverbalBehavior(characterName, utterance, languageTag, result =>
            {
                if (!UsingRealtimeConversationMode || character != CurrentCharacter || string.IsNullOrWhiteSpace(result))
                    return;

                float playbackSeconds = m_openAIRealtimeConversationSystem != null ? m_openAIRealtimeConversationSystem.AssistantPlaybackSeconds : 0f;
                float receivedSeconds = m_openAIRealtimeConversationSystem != null ? m_openAIRealtimeConversationSystem.AssistantReceivedAudioSeconds : 0f;
                float durationSeconds = Mathf.Max(receivedSeconds, EstimateRealtimeChunkDurationSeconds(utterance));
                string sanitizedXml = SanitizeRealtimeNvbgXml(result);
                AudioSpeechFile speechFile = CreateRealtimeNvbgSpeechFile(utterance, sanitizedXml, playbackSeconds, durationSeconds);

                if (speechFile == null)
                    return;

                character.PlayXml(speechFile);
                Destroy(speechFile.gameObject, Mathf.Max(5f, durationSeconds + 5f));
            });
        }

        protected void DetectLanguageForUserText(string text, string inputSource, Action onComplete)
        {
            if (!ShouldRunLanguageDetectionFallback(inputSource))
            {
                if (string.Equals(inputSource, UserInputSourceText, StringComparison.OrdinalIgnoreCase))
                    SetLatestUserLanguage(string.Empty, "text input (fallback off)");

                onComplete?.Invoke();
                return;
            }

            if (m_languageDetectionSystem == null)
            {
                SetLatestUserLanguage(string.Empty, $"{inputSource} (fallback unavailable)");
                onComplete?.Invoke();
                return;
            }

            m_languageDetectionSystem.DetectLanguage(text, inputSource, result =>
            {
                if (result.success)
                    SetLatestUserLanguage(result.language, result.source, result.confidence);
                else if (string.Equals(inputSource, UserInputSourceText, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(m_latestUserLanguageTag))
                    SetLatestUserLanguage(string.Empty, $"{result.source}: {result.details}", result.confidence);

                onComplete?.Invoke();
            });
        }

        protected bool ShouldRunLanguageDetectionFallback(string inputSource)
        {
            if (!LanguageDetectionFallbackEnabled)
                return false;

            if (string.Equals(inputSource, UserInputSourceText, StringComparison.OrdinalIgnoreCase))
                return true;

            return string.IsNullOrWhiteSpace(m_latestUserLanguageTag);
        }

        protected void SetLatestUserLanguage(string languageTag, string source = "provider", float confidence = -1f)
        {
            string normalizedLanguage = NormalizeProviderLanguageTag(languageTag);
            m_latestUserLanguageTag = normalizedLanguage;
            m_latestUserLanguageSource = string.IsNullOrWhiteSpace(source) ? "unknown source" : source;
            m_latestUserLanguageConfidence = confidence;

            Debug.Log($"[Language] source='{m_latestUserLanguageSource}' language='{(string.IsNullOrWhiteSpace(languageTag) ? "unknown" : languageTag)}' effectiveNvbgLanguage='{(string.IsNullOrWhiteSpace(normalizedLanguage) ? "en" : normalizedLanguage)}'");

            if (m_nvbgSystem != null && CurrentCharacter != null && !string.IsNullOrWhiteSpace(normalizedLanguage))
                m_nvbgSystem.PrepareLanguageContext(CurrentCharacter.CharacterName, normalizedLanguage);
        }

        protected string GetEffectiveNvbgLanguageTag() => NormalizeProviderLanguageTag(m_latestUserLanguageTag);

        protected LanguageDetectionSettings GetLanguageDetectionSettings()
        {
            return LanguageDetectionSystemOpenAI.Settings;
        }

        protected string BuildUserLanguageDebugDisplay()
        {
            string language = string.IsNullOrWhiteSpace(m_latestUserLanguageTag) ? "unknown" : m_latestUserLanguageTag;
            string confidence = m_latestUserLanguageConfidence >= 0f ? $", confidence {m_latestUserLanguageConfidence:F2}" : string.Empty;
            string fallback = LanguageDetectionFallbackEnabled ? "fallback on" : "fallback off";
            return $"{language} ({m_latestUserLanguageSource}{confidence}; {fallback})";
        }

        protected string BuildAsrLanguageSupportDisplay()
        {
            return m_asrMode switch
            {
                AsrMode.Azure => "Azure ASR: explicit dynamic language detection",
                AsrMode.AzureWebGL => "Azure WebGL ASR: fixed configured language",
                AsrMode.OpenAI => "OpenAI ASR: no explicit language tag; uses text fallback when enabled",
                AsrMode.FasterWhisper => "FasterWhisper ASR: returns provider language when configured for detection",
                AsrMode.Windows => "Windows ASR: no explicit language tag",
                AsrMode.Mobile => "Mobile ASR: no explicit language tag",
                _ => "ASR language support: unknown",
            };
        }

        protected string GetCurrentAsrLanguageSourceLabel()
        {
            return m_asrMode switch
            {
                AsrMode.Azure => "Azure ASR",
                AsrMode.AzureWebGL => "Azure WebGL ASR",
                AsrMode.OpenAI => "OpenAI ASR (no explicit language tag)",
                AsrMode.FasterWhisper => "FasterWhisper ASR",
                AsrMode.Windows => "Windows ASR (no explicit language tag)",
                AsrMode.Mobile => "Mobile ASR (no explicit language tag)",
                _ => "ASR",
            };
        }

        protected static string NormalizeProviderLanguageTag(string languageTag)
        {
            if (string.IsNullOrWhiteSpace(languageTag))
                return string.Empty;

            string normalized = languageTag.Trim();
            if (string.Equals(normalized, "unknown", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(normalized, "missing", StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return normalized;
        }

        private static AudioSpeechFile CreateRealtimeNvbgSpeechFile(string text, string nvbgXml, float playbackSeconds, float durationSeconds)
        {
            string timingBml = BuildRealtimeNvbgTimingBml(text, playbackSeconds, durationSeconds);
            if (string.IsNullOrWhiteSpace(timingBml))
                return null;

            var go = new GameObject("OpenAIRealtimeNvbgTiming");
            var speechFile = go.AddComponent<AudioSpeechFile>();
            speechFile.BmlText = timingBml;
            speechFile.ConvertedXml = nvbgXml;
            speechFile.ReadBmlData();
            return speechFile;
        }

        private static string BuildRealtimeNvbgTimingBml(string text, float playbackSeconds, float durationSeconds)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            string[] words = Regex.Replace(text.Trim(), "\\s+", " ").Split(' ');
            if (words.Length == 0)
                return string.Empty;

            float duration = Mathf.Max(durationSeconds, EstimateRealtimeChunkDurationSeconds(text), words.Length * 0.12f);
            float wordStep = duration / words.Length;
            float wordDuration = Mathf.Clamp(wordStep * 0.78f, 0.07f, 0.32f);
            float relativePastWordStart = 0.05f;

            var builder = new StringBuilder();
            builder.Append("<bml><speech id=\"sp1\"><text>");

            for (int i = 0; i < words.Length; i++)
            {
                string escapedWord = System.Security.SecurityElement.Escape(words[i]) ?? string.Empty;
                float globalStart = i * wordStep;
                float globalEnd = Mathf.Min(duration, globalStart + wordDuration);
                float relativeStart = globalStart - playbackSeconds;
                float relativeEnd = globalEnd - playbackSeconds;

                if (relativeEnd < 0.05f)
                {
                    relativeStart = relativePastWordStart;
                    relativeEnd = relativeStart + 0.08f;
                    relativePastWordStart += 0.08f;
                }
                else
                {
                    relativeStart = Mathf.Max(0.05f, relativeStart);
                    relativeEnd = Mathf.Max(relativeStart + 0.05f, relativeEnd);
                }

                int startMarker = i == 0 ? 0 : i * 2;
                int endMarker = i == 0 ? 1 : (i * 2) + 1;

                builder.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "<sync id=\"T{0}\" time=\"{1:0.000}\"/>", startMarker, relativeStart);
                builder.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "<word start=\"{0:0.000}\" end=\"{1:0.000}\">{2}</word>", relativeStart, relativeEnd, escapedWord);
                builder.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "<sync id=\"T{0}\" time=\"{1:0.000}\"/>", endMarker, relativeEnd);
            }

            builder.Append("</text></speech></bml>");
            return builder.ToString();
        }

        private static float EstimateRealtimeChunkDurationSeconds(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0.25f;

            string trimmed = text.Trim();
            string[] words = trimmed.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            int wordCount = words.Length;
            int charCount = trimmed.Length;

            float wordDuration = wordCount * 0.34f;
            float charDuration = charCount / 11.0f;
            float punctuationPause = 0f;

            if (trimmed.EndsWith(".", StringComparison.Ordinal) ||
                trimmed.EndsWith("!", StringComparison.Ordinal) ||
                trimmed.EndsWith("?", StringComparison.Ordinal))
            {
                punctuationPause += 0.28f;
            }
            else if (trimmed.EndsWith(",", StringComparison.Ordinal) ||
                     trimmed.EndsWith(";", StringComparison.Ordinal) ||
                     trimmed.EndsWith(":", StringComparison.Ordinal))
            {
                punctuationPause += 0.16f;
            }

            float estimated = Mathf.Max(wordDuration, charDuration) + punctuationPause;
            return Mathf.Clamp(estimated, 0.25f, 2.75f);
        }

        private void FlushRealtimeLipsyncChunk()
        {
            if (string.IsNullOrWhiteSpace(m_realtimePendingLipsyncText) || m_streamingLipsyncSystem == null || m_openAIRealtimeConversationSystem == null)
                return;

            string chunk = m_realtimePendingLipsyncText.Trim();
            m_realtimePendingLipsyncText = string.Empty;
            if (string.IsNullOrWhiteSpace(chunk))
                return;

            m_streamingLipsyncSystem.AppendChunk(chunk, m_openAIRealtimeConversationSystem.AssistantReceivedAudioSeconds);
        }

        private static bool ShouldFlushRealtimeLipsyncChunk(string pendingText)
        {
            if (string.IsNullOrWhiteSpace(pendingText))
                return false;

            string trimmed = pendingText.TrimEnd();
            char last = trimmed[trimmed.Length - 1];
            if (char.IsWhiteSpace(last) || last == '.' || last == '!' || last == '?' || last == ',' || last == ';' || last == ':')
                return true;

            return trimmed.Length >= 12;
        }
    }
}
