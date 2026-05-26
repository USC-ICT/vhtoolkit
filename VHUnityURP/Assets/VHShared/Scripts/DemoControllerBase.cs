using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ride.Audio;
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
        }

        public enum NlpMode
        {
            ChatGPT = 0,
            Claude = 1,
            AwsLex = 2,
            Rasa = 3,
            VLLM = 4,
        }

        public enum TtsMode
        {
            Polly = 0,
            ElevenLabs = 1,
            Piper = 2,
            Kokoro = 3,
            XTTS = 4,
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
        protected NlpSystemChatGPT m_chatGPTSystem;
        protected NlpSystemAnthropic m_anthropicSystem;
        protected NlpSystemRasa m_rasaNlpSystem;
        protected NlpSystemAWSLex m_lexSystem;
        protected NlpSystemVLLM m_vLLMSystem;
        protected NonverbalBehaviorGeneratorSystem m_nvbgSystem;
        protected TextToSpeechSystemElevenLabs m_elevenTextToSpeechSystem;
        protected TextToSpeechSystemAWSPolly m_awsPollyTextToSpeechSystem;
        protected TextToSpeechSystemPiper m_piperTextToSpeechSystem;
        protected TextToSpeechSystemKokoro m_kokoroTextToSpeechSystem;
        protected TextToSpeechSystemXTTS m_xttsTextToSpeechSystem;

        [SerializeField] protected TtsReader m_ttsReader;

        [NonSerialized] public NlpSystemUnity m_currentLLM;
        [NonSerialized] public NlpSystemUnity m_currentScripted;
        [NonSerialized] public ISpeechRecognitionSystem m_currentASR;
        [NonSerialized] public ILipsyncedTextToSpeechSystem m_currentTTS;
        [NonSerialized] public NlpMode m_nlpMode;
        [NonSerialized] public AsrMode m_asrMode;
        [NonSerialized] public TtsMode m_ttsMode;
        [NonSerialized] public int m_ttsVoice;

        protected MecanimCharacter m_currentCharacter;
        protected AudioClip m_audioClip;
        protected string m_audioFilePath;
        protected string m_lipsyncXML;
        protected string m_response;
        protected int m_maxSpokenCharacters = 1000;
        protected ThinkingController m_thinkingController;

        public bool IntroduceOnLoad = true;

        public MecanimCharacter CurrentCharacter => m_currentCharacter;
        public IReadOnlyList<MecanimCharacter> Characters => m_characters;

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
            m_chatGPTSystem = Systems.Get<NlpSystemChatGPT>();
            m_anthropicSystem = Systems.Get<NlpSystemAnthropic>();
            m_rasaNlpSystem = Systems.Get<NlpSystemRasa>();
            m_lexSystem = Systems.Get<NlpSystemAWSLex>();
            m_vLLMSystem = Systems.Get<NlpSystemVLLM>();
            m_nvbgSystem = Systems.Get<NonverbalBehaviorGeneratorSystem>();
            m_elevenTextToSpeechSystem = Systems.Get<TextToSpeechSystemElevenLabs>();
            m_awsPollyTextToSpeechSystem = Systems.Get<TextToSpeechSystemAWSPolly>();
            m_piperTextToSpeechSystem = Systems.Get<TextToSpeechSystemPiper>();
            m_kokoroTextToSpeechSystem = Systems.Get<TextToSpeechSystemKokoro>();
            m_xttsTextToSpeechSystem = Systems.Get<TextToSpeechSystemXTTS>();
            if (!m_ttsReader) m_ttsReader = FindAnyObjectByType<TtsReader>();

            if (m_windowsSpeechRecognitionSystem != null) m_windowsSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_azureSpeechRecognitionSystem != null) m_azureSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_azureWebGLSpeechRecognitionSystem != null) m_azureWebGLSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_openAISpeechRecognitionSystem != null) m_openAISpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_fasterWhisperSpeechRecognitionSystem != null) m_fasterWhisperSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;

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
            if (m_asrMode != mode)
                SetASR(false);

            m_asrMode = mode;

            if (mode == AsrMode.Azure) m_currentASR = m_azureSpeechRecognitionSystem;
            else if (mode == AsrMode.Windows) m_currentASR = m_windowsSpeechRecognitionSystem;
            else if (mode == AsrMode.AzureWebGL) m_currentASR = m_azureWebGLSpeechRecognitionSystem;
            else if (mode == AsrMode.OpenAI) m_currentASR = m_openAISpeechRecognitionSystem;
            else if (mode == AsrMode.FasterWhisper) m_currentASR = m_fasterWhisperSpeechRecognitionSystem;
#if RIDEVH_URP || RIDEVH_XR
            // else if (mode == AsrMode.Mobile) m_currentASR = m_mobileSpeechRecognitionSystem;
#endif
            else throw new NotImplementedException();
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
            else if (mode == NlpMode.AwsLex) m_currentScripted = m_lexSystem;
            else if (mode == NlpMode.Rasa) m_currentLLM = m_rasaNlpSystem;
            else if (mode == NlpMode.VLLM) m_currentLLM = m_vLLMSystem; 
        }

        /// <summary>
        /// Sets the character prompt for LLM processing.
        /// </summary>
        /// <param name="character">The character whose prompt is being set.<see cref="MecanimCharacter"/></param>
        /// <param name="prompt">The text prompt to apply.</param>
        /// <seealso cref="WaitAndSetPrompt(MecanimCharacter, string)"/>
        public void SetPrompt(MecanimCharacter character, string prompt = "")
            => StartCoroutine(WaitAndSetPrompt(character, prompt));

        public void AskNLPQuestion(string q)
        {
            StopUtterance();                    // Stop current character behaviors
            SetCharacterConfigUIEnabled(false); // Don't allow character change while interaction is processing and executing
            if (m_thinkingController != null)   // Start character thinking nonverbal behaviors after a small delay
                m_thinkingController.StartThinkingBehavior(true);

            if (m_nlpMode == NlpMode.AwsLex) m_currentScripted.Request(new NlpRequest(q), QuestionResponse);
            else m_currentLLM.Request(new NlpRequest(q), QuestionResponse);
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

            SetCharacterVoice(m_currentTTS, m_currentCharacter);
        }

        /// <summary>
        /// Sets the voice used by the character from the current TTS system.
        /// </summary>
        /// <param name="ttsSystem">The TTS system to get the voice from.<see cref="ILipsyncedTextToSpeechSystem"/></param>
        /// <param name="character">The character to assign the voice to.<see cref="MecanimCharacter"/></param>
        public void SetCharacterVoice(ILipsyncedTextToSpeechSystem ttsSystem, MecanimCharacter character)
            => StartCoroutine(SetCharacterVoiceCoroutine(ttsSystem, character));

        /// <summary>
        /// Coroutine to wait until voices are loaded and apply the voice by name.
        /// </summary>
        /// <param name="ttsSystem">The TTS system.<see cref="ILipsyncedTextToSpeechSystem"/></param>
        /// <param name="character">The character to apply the voice to.<see cref="MecanimCharacter"/></param>
        /// <returns>Coroutine enumerator.</returns>
        protected IEnumerator SetCharacterVoiceCoroutine(ILipsyncedTextToSpeechSystem ttsSystem, MecanimCharacter character)
        {
            if (ttsSystem == null || character == null)
                yield break;

            m_currentTTS = ttsSystem;

            yield return new WaitUntil(() =>
                m_currentTTS != null &&
                m_currentTTS.GetAvailableVoices() != null &&
                m_currentTTS.GetAvailableVoices().Length > 0
            );

            string voiceName = string.Empty;
            var profile = character.GetComponent<VHCharacterProfile>();
            if (profile != null)
            {
                if ((object)m_currentTTS == m_awsPollyTextToSpeechSystem)
                    voiceName = profile.PollyVoiceName;
                else if ((object)m_currentTTS == m_elevenTextToSpeechSystem)
                    voiceName = profile.ElevenLabVoiceName;
                else if ((object)m_currentTTS == m_piperTextToSpeechSystem)
                    voiceName = profile.PiperVoiceName;
                else if ((object)m_currentTTS == m_kokoroTextToSpeechSystem)
                    voiceName = profile.KokoroVoiceName;
                else if ((object)m_currentTTS == m_xttsTextToSpeechSystem)
                    voiceName = profile.XTTSVoiceName;
            }

            var voices = m_currentTTS.GetAvailableVoices();
            int idx = -1;
            if (!string.IsNullOrEmpty(voiceName))
                idx = m_currentTTS.GetVoiceIndex(voiceName);

            if (voices != null && voices.Length > 0)
                m_ttsVoice = (idx >= 0 && idx < voices.Length) ? idx : 0;
        }

        /// <summary>
        /// Generates text-to-speech audio for the given utterance.
        /// </summary>
        /// <param name="utterance">The spoken text to convert to speech.</param>
        public void CreateTTS(string utterance)
        {
            if (string.IsNullOrEmpty(utterance)) return;
            if (utterance.Length > m_maxSpokenCharacters) utterance = utterance[..m_maxSpokenCharacters];
            m_currentTTS.CreateTextToSpeech(m_currentTTS.GetAvailableVoices()[m_ttsVoice], utterance, OnTtsGenerated);
        }

        /// <summary>
        /// Stops the current character's audio and lipsync playback.
        /// </summary>
        public virtual void StopUtterance()
        {
            if (CurrentCharacter != null)
            {
                CurrentCharacter.StopLipSyncPerformance();
                CurrentCharacter.StopAudio();
            }

            SetCharacterConfigUIEnabled(true);
        }

        /// <summary>
        /// Called when the NLP system returns a response.
        /// </summary>
        /// <param name="response">The text response from the NLP system.</param>
        protected void OnNlpResponseReceived(string response)
        {
            m_response = response;
            m_demoControllerUI?.PopulateResponseUI("VH", response);
            CreateTTS(response);
            FindAnyObjectByType<DebugMenus>().SetNlpResponse(response);
        }

        /// <summary>
        /// Handles recognized speech input and forwards it to the NLP system.
        /// </summary>
        /// <param name="sender">The sender of the speech recognition event.</param>
        /// <param name="e">The speech recognition result event arguments.</param>
        /// <see cref="SpeechRecognizedEventArgs"/>
        protected void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            m_demoControllerUI?.PopulateResponseUI("You", e.Text);
            AskNLPQuestion(e.Text);
            FindAnyObjectByType<DebugMenus>().SetNlpInput(e.Text);
        }

        /// <summary>
        /// Callback for when the TTS system has finished generating audio and lipsync XML.
        /// </summary>
        /// <param name="lipsyncXML">The lipsync XML data.</param>
        /// <param name="audioFilePath">The file path to the generated audio.</param>
        protected void OnTtsGenerated(string lipsyncXML, string audioFilePath)
        {
            if (string.IsNullOrWhiteSpace(audioFilePath))
            {
                Debug.LogError($"[{nameof(DemoControllerBase)}] TTS generation failed; audio file path is empty.");
                if (m_thinkingController != null)
                    m_thinkingController.StopThinkingBehavior();
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
            m_nvbgSystem.GetNonverbalBehavior(character.CharacterName, utterance, OnNvbgGenerated);
        }

        /// <summary>
        /// Callback for when NVBG system completes processing.
        /// Loads audio and begins playback.
        /// </summary>
        /// <param name="result">The nonverbal behavior output string.</param>
        protected void OnNvbgGenerated(string result)
        {
            if (string.IsNullOrWhiteSpace(m_audioFilePath))
            {
                Debug.LogError($"[{nameof(DemoControllerBase)}] Cannot load TTS audio; path is empty.");
                if (m_thinkingController != null)
                    m_thinkingController.StopThinkingBehavior();
                SetCharacterConfigUIEnabled(true);
                return;
            }

            var audio = Systems.Get<AudioSystemUnity>();
            m_audioClip = null;
            audio.LoadAudioFile(m_audioFilePath, clip =>
            {
                m_audioClip = clip;
                StartCoroutine(PlayUtterance(result));
            });
        }

        /// <summary>
        /// Plays the audio utterance with lipsync and nonverbal behavior.
        /// </summary>
        /// <param name="nvbgResult">The nonverbal behavior animation data.</param>
        /// <returns>Coroutine enumerator.</returns>
        protected IEnumerator PlayUtterance(string nvbgResult)
        {
            string facefx = " ";
            if (!string.IsNullOrEmpty(m_lipsyncXML))
            {
                string xml = m_lipsyncXML.Substring(m_lipsyncXML.IndexOf('<'));
                var tts = m_ttsReader.ReadTtsXml(xml, out _);
                facefx = VisemeFormatConverter.ConvertTtsToFaceFx(tts);
            }

            yield return new WaitUntil(() => m_audioClip != null);

            var ttsFile = AudioSpeechFile.CreateAudioSpeechFile(facefx, nvbgResult, m_audioClip);
            MecanimManager.Get().FindAudioFiles();
            CurrentCharacter.PlayAudio(ttsFile);
            CurrentCharacter.PlayXml(ttsFile);

            if (m_thinkingController != null)
                m_thinkingController.StopThinkingBehavior();

            SetCharacterConfigUIEnabled(false);

            float waitTime = Math.Max(ttsFile.ClipLength - 0.2f, 0.1f);  // Wait until near the end of the audio clip
            yield return new WaitForSeconds(waitTime);

            SetCharacterConfigUIEnabled(true);
        }

        /// <summary>
        /// Coroutine that sets the character's LLM prompt after a short delay.
        /// </summary>
        /// <param name="character">The character to apply the prompt to.<see cref="MecanimCharacter"/></param>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>Coroutine enumerator.</returns>
        protected IEnumerator WaitAndSetPrompt(MecanimCharacter character, string prompt)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            var profile = character.GetComponent<VHCharacterProfile>();
            if (!string.IsNullOrEmpty(prompt)) profile.llmPrompt = prompt;

            m_nlp.SetUIPrompt(profile.llmPrompt);
            if (m_chatGPTSystem != null)
                m_chatGPTSystem.SetSystemPrompt(profile.llmPrompt);
            if (m_anthropicSystem != null)
                m_anthropicSystem.SetSystemPrompt(profile.llmPrompt);
            if (m_vLLMSystem != null)
                m_vLLMSystem.SetSystemPrompt(profile.llmPrompt);
        }

        /// <summary>
        /// Receives the response from the NLP and processes it.
        /// </summary>
        /// <param name="response">The NLP response data.</param><see cref="NlpResponse"/>
        protected void QuestionResponse(NlpResponse response) => SendResponse(response.content[0]);

        protected abstract IDemoControllerUI BindUI();
        protected abstract void CollectCharacters();                   //AR: direct children; Desktop: nested

        public abstract void SelectCharacter(string characterName);  //gaze target and catalog differences
        protected virtual void AfterSystemsInitialized() { }           //cameras, catalogs, etc.

        protected override void Update()
        {
            base.Update();

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
    }
}
