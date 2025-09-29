using UnityEngine;
using VHAssets;

namespace Ride.Examples
{
    public class DemoController : DemoControllerBase
    {
        [Header("Cameras")]
        [SerializeField] private Camera m_camera;
        [SerializeField] private GameObject m_xrOrigin;

        [Header("UI")]
        [SerializeField] private DemoController_UI m_uiController;

        protected override IDemoControllerUI BindUI() => m_uiController;

        protected override void AfterSystemsInitialized()
        {
#if UNITY_STANDALONE_WIN
            if (m_camera) m_camera.gameObject.SetActive(true);
            if (m_xrOrigin) m_xrOrigin.SetActive(false);
#elif ENABLE_XR
            if (m_camera) m_camera.gameObject.SetActive(false);
            if (m_xrOrigin) m_xrOrigin.SetActive(true);
#endif
            //load cached catalogs if available
            var bundleLoader = Systems.Get<AssetLoadingSystemAssetBundles>();
            if (bundleLoader != null)
                StartCoroutine(bundleLoader.LoadCachedCatalogs());
        }

        protected override void CollectCharacters()
        {
            m_characters.Clear();
            if (m_charactersParent == null)
            {
                Debug.LogWarning("DemoController: m_charactersParent is not assigned.");
                return;
            }
            foreach (Transform category in m_charactersParent)
                foreach (Transform child in category)
                    if (child.TryGetComponent(out MecanimCharacter mc))
                        m_characters.Add(mc);
        }

        /// <summary>
        /// Selects and activates the character by name. Loads character asset if needed.
        /// </summary>
        /// <param name="characterName">The name of the character to activate.</param>
        protected override void SelectCharacterInternal(string characterName)
        {
            foreach (var character in m_characters)
            {
                if (character.name == characterName)
                {
                    if (character.TryGetComponent<RideCatalogAsset>(out var loadable) && !loadable.AssetInitialized)
                    {
                        character.gameObject.SetActive(true);
                        loadable.LoadAsset();//once loaded it will loop back here, but this time be initialized
                    }
                    else
                    {
                        m_currentCharacter = character;
                        character.gameObject.SetActive(true);
                        m_nvbgSystem.StartProcess(character.CharacterName);
                        m_gaze.GazeAt(character, "GazeTargetUser");
                        SetPrompt(m_currentCharacter);
                        m_currentTTS = (m_ttsMode == 1)
                            ? m_elevenTextToSpeechSystem
                            : m_awsPollyTextToSpeechSystem;
                        SetCharacterVoice(m_currentTTS, m_currentCharacter);
                    }
                }
                else
                    character.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Updates the ASR button color based on recognition state or audio playback status.
        /// </summary>
        protected override void UpdateAsrButtonColorInternal()
        {
            if (m_demoControllerUI == null || m_currentASR == null) return;

            if (m_currentASR.SelectedMicrophone == string.Empty)
                m_demoControllerUI.SetAsrButtonColor(Color.gray);
            else if (m_currentASR.IsRecognizing)
                m_demoControllerUI.SetAsrButtonColor(Color.red);
            else if (m_currentCharacter != null && m_currentCharacter.Voice.isPlaying)
                m_demoControllerUI.SetAsrButtonColor(Color.gray);
            else
                m_demoControllerUI.SetAsrButtonColor(Color.white);
        }

        /// <summary>
        /// Stops the current character's audio and lipsync playback.
        /// </summary>
        public override void StopUtterance()
        {
            base.StopUtterance();
            CurrentCharacter.StopAnim();

            var cutscenes = CurrentCharacter.transform.GetComponentsInChildren<Cutscene>();
            foreach (Cutscene cutscene in cutscenes) { cutscene.Stop(); }
        }

        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                VHUtils.ApplicationQuit();

            base.Update();
        }
    }
}
