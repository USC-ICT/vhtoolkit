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
    public abstract class DemoControllerBase : RideBaseMinimal
    {
        [Header("Debug Menus")]
        [SerializeField] protected DebugMenuGaze m_gaze;
        [SerializeField] protected DebugMenuLipsync m_lipsync;
        [SerializeField] protected DebugMenuLLM m_llm;

        [Header("UI")]
        protected IDemoControllerUI m_demoControllerUI;

        [Header("VH")]
        [SerializeField] protected Transform m_charactersParent;
        public Transform CharactersParent { get => m_charactersParent; }
        protected readonly List<MecanimCharacter> m_characters = new();

        protected SpeechRecognitionSystemWindows m_windowsSpeechRecognitionSystem;
        protected SpeechRecognitionSystemAzure m_azureSpeechRecognitionSystem;
        protected NlpSystemChatGPT m_chatGPTSystem;
        protected NlpSystemAnthropic m_anthropicSystem;
        protected RasaNlpSystem m_rasaNlpSystem;
        protected NlpSystemAWSLex m_lexSystem;
        protected NonverbalBehaviorGeneratorSystem m_nvbgSystem;
        protected TextToSpeechSystemElevenLabs m_elevenTextToSpeechSystem;
        protected TextToSpeechSystemAWSPolly m_awsPollyTextToSpeechSystem;

        [SerializeField] protected TtsReader m_ttsReader;

        [System.NonSerialized] public NlpSystemUnity m_currentLLM, m_currentScripted;
        [System.NonSerialized] public ISpeechRecognitionSystem m_currentASR;
        [System.NonSerialized] public ILipsyncedTextToSpeechSystem m_currentTTS;
        [System.NonSerialized] public int m_llmMode, m_asrMode, m_ttsMode, m_ttsVoice;

        protected MecanimCharacter m_currentCharacter;
        protected AudioClip m_audioClip;
        protected string m_audioFilePath, m_lipsyncXML, m_response;
        protected int m_maxSpokenCharacters = 1000;

        public MecanimCharacter CurrentCharacter => m_currentCharacter;
        public IReadOnlyList<MecanimCharacter> Characters => m_characters;

        protected override void Start()
        {
            base.Start();

            var config = Systems.Get<ConfigurationSystemUnity>();
            if (config != null && !config.IsCorrectVersion()) { config.ResetConfig(); config.Save(); }

            m_windowsSpeechRecognitionSystem = Systems.Get<SpeechRecognitionSystemWindows>();
            m_azureSpeechRecognitionSystem = Systems.Get<SpeechRecognitionSystemAzure>();
            m_chatGPTSystem = Systems.Get<NlpSystemChatGPT>();
            m_anthropicSystem = Systems.Get<NlpSystemAnthropic>();
            m_rasaNlpSystem = Systems.Get<RasaNlpSystem>();
            m_lexSystem = Systems.Get<NlpSystemAWSLex>();
            m_nvbgSystem = Systems.Get<NonverbalBehaviorGeneratorSystem>();
            m_elevenTextToSpeechSystem = Systems.Get<TextToSpeechSystemElevenLabs>();
            m_awsPollyTextToSpeechSystem = Systems.Get<TextToSpeechSystemAWSPolly>();
            if (!m_ttsReader) m_ttsReader = FindAnyObjectByType<TtsReader>();

            if (m_windowsSpeechRecognitionSystem != null)
                m_windowsSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;
            if (m_azureSpeechRecognitionSystem != null)
                m_azureSpeechRecognitionSystem.SpeechRecognized += OnSpeechRecognized;

            ChangeASR(m_windowsSpeechRecognitionSystem ? 0 : 1);
            m_currentLLM = m_chatGPTSystem ? m_chatGPTSystem : m_anthropicSystem;
            m_currentScripted = m_lexSystem;
            ChangeLlm(0);
            m_currentTTS = m_awsPollyTextToSpeechSystem ? m_awsPollyTextToSpeechSystem : m_elevenTextToSpeechSystem;

            //Bind UI and collect characters (AR/Desktop specifics handled in overrides)
            m_demoControllerUI = BindUI();
            m_demoControllerUI.InitializeCanvasCamera();
            CollectCharacters();
            AfterSystemsInitialized();

            //Pick the first already-active character if any
            foreach (var character in m_characters)
                if (character.gameObject.activeSelf) { SelectCharacterInternal(character.name); break; }

            var onScreen = Systems.Get<DebugOnScreenLogVHAssets>();
            if (onScreen != null) onScreen.m_log.ShowLog(false);
        }

        /// <summary>
        /// Changes the active Automatic Speech Recognition (ASR) system.
        /// </summary>
        /// <param name="mode">ASR mode index: 0 = Windows, 1 = Azure, 2 = Mobile (if enabled).</param>
        public void ChangeASR(int mode)
        {
            m_asrMode = mode;
            if (mode == 0) m_currentASR = m_windowsSpeechRecognitionSystem;
            else if (mode == 1) m_currentASR = m_azureSpeechRecognitionSystem;
#if RIDEVH_URP || RIDEVH_XR
            // else if (mode == 2) m_currentASR = m_mobileSpeechRecognitionSystem;
#endif
            else throw new System.NotImplementedException();
        }

        /// <summary>
        /// Changes the active Large Language Model (LLM) system.
        /// </summary>
        /// <param name="mode">LLM mode index: 0 = ChatGPT, 1 = Anthropic, 2 = Lex, 3 = Rasa.</param>
        public void ChangeLlm(int mode)
        {
            m_llmMode = mode;
            if (mode == 0) m_currentLLM = m_chatGPTSystem;
            else if (mode == 1) m_currentLLM = m_anthropicSystem;
            else if (mode == 2) m_currentScripted = m_lexSystem;
            else if (mode == 3) m_currentLLM = m_rasaNlpSystem;
        }

        /// <summary>
        /// Sets the character prompt for LLM processing.
        /// </summary>
        /// <param name="character">The character whose prompt is being set.<see cref="MecanimCharacter"/></param>
        /// <param name="prompt">The text prompt to apply.</param>
        /// <seealso cref="WaitAndSetPrompt(MecanimCharacter, string)"/>
        public void SetPrompt(MecanimCharacter character, string prompt = "")
            => StartCoroutine(WaitAndSetPrompt(character, prompt));

        public void AskLLMQuestion(string q)
        {
            if (m_llmMode == 2) m_currentScripted.Request(new NlpRequest(q), QuestionResponse);
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
        /// <param name="mode">TTS mode index: 0 = AWS Polly, 1 = ElevenLabs.</param>
        public void ChangeTts(int mode)
        {
            m_ttsMode = mode;
            m_currentTTS = (mode == 0) ? m_awsPollyTextToSpeechSystem : m_elevenTextToSpeechSystem;
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
            CurrentCharacter?.StopLipSyncPerformance();
            CurrentCharacter?.StopAudio();
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
        /// Handles recognized speech input and forwards it to the LLM.
        /// </summary>
        /// <param name="sender">The sender of the speech recognition event.</param>
        /// <param name="e">The speech recognition result event arguments.</param>
        /// <see cref="SpeechRecognizedEventArgs"/>
        protected void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            m_demoControllerUI?.PopulateResponseUI("You", e.Text);
            AskLLMQuestion(e.Text);
            FindAnyObjectByType<DebugMenus>().SetNlpInput(e.Text);
        }

        /// <summary>
        /// Callback for when the TTS system has finished generating audio and lipsync XML.
        /// </summary>
        /// <param name="lipsyncXML">The lipsync XML data.</param>
        /// <param name="audioFilePath">The file path to the generated audio.</param>
        protected void OnTtsGenerated(string lipsyncXML, string audioFilePath)
        {
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

            yield return new WaitForSeconds(ttsFile.ClipLength);
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

            m_llm.SetUIPrompt(profile.llmPrompt);
            m_chatGPTSystem?.SetSystemPrompt(profile.llmPrompt);
            m_anthropicSystem?.SetSystemPrompt(profile.llmPrompt);
        }

        /// <summary>
        /// Receives the response from the LLM and processes it.
        /// </summary>
        /// <param name="response">The NLP response data.</param><see cref="NlpResponse"/>
        protected void QuestionResponse(NlpResponse response) => SendResponse(response.content[0]);

        protected abstract IDemoControllerUI BindUI();
        protected abstract void CollectCharacters();                   //AR: direct children; Desktop: nested

        public void SelectCharacter(string characterName)
        {
            SelectCharacterInternal(characterName);
        }
        protected abstract void SelectCharacterInternal(string name);  //gaze target and catalog differences
        protected virtual void AfterSystemsInitialized() { }           //cameras, catalogs, etc.

        protected override void Update()
        {
            base.Update();

            UpdateAsrButtonColorInternal();
        }

        protected abstract void UpdateAsrButtonColorInternal();
    }
}
