using UnityEngine;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for configuring lipsync options.
    /// Allows selection between FaceFX and OVR lipsync systems.
    /// </summary>
    public class DebugMenuLipsync : RideMonoBehaviour
    {
        private DebugMenu m_debugMenu;        // Reference to the Debug Menu system.
        private DemoController m_controller;  // Reference to the Demo Controller for character management.
        private DemoControllerBase.LipsyncOptions m_lipsyncMode; // Stores the selected lipsync mode index.


        /// <summary>
        /// Initializes references to the necessary systems when the script starts.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Systems.Get<DebugMenu>();
            m_controller = FindAnyObjectByType<DemoController>();
        }

        /// <summary>
        /// Handles the GUI layout for Lipsync settings in the Debug Menu.
        /// Displays the system selection UI.
        /// </summary>
        public void OnGUILipsync()
        {
            m_debugMenu.Label($"<b>Lipsync</b>");

            OnGUISystemSelection();
        }

        /// <summary>
        /// Displays a selection grid for choosing the active lipsync system.
        /// </summary>
        public void OnGUISystemSelection()
        {
            int lipsync = m_debugMenu.SelectionGrid((int)m_lipsyncMode, new string[] { "VH", "OVR" /*, "Timeline" */ }, 2);
            if ((int)m_lipsyncMode != lipsync)
            {
                m_lipsyncMode = (DemoControllerBase.LipsyncOptions)lipsync;
                m_controller.SetLipsyncMethod(m_lipsyncMode);
            }

            m_debugMenu.Space();

            m_debugMenu.Label($"<b>NVBG</b>");
            var curCharacter = m_controller.CurrentCharacter;
            if (curCharacter != null)
            {
                var profile = m_controller.CurrentCharacter.GetComponent<VHCharacterProfile>();
                if (profile != null)
                {
                    m_debugMenu.Label(profile.NVBG.CharacterId);
                    m_debugMenu.Label(profile.NVBG.IdlePostureId);
                }
            }

            m_debugMenu.Space();
        }

#if false
        /// <summary>
        /// Plays audio using the selected lipsync system.
        /// </summary>
        /// <param name="character">The character performing the lipsync.</param>
        /// <param name="audioClip">The audio clip to be played.</param>
        /// <param name="ttsUtterance">The text-to-speech utterance object.</param>
        public void PlayAudio(MecanimCharacter character, AudioClip audioClip, AudioSpeechFile ttsUtterance)
        {
            // If FaceFX is selected, use the FaceFX system for lipsync animation.
            if (m_currentLipsync == LipsyncOptions.FaceFX)
            {
                character.PlayAudio(ttsUtterance);
            }
            // If OVR is selected, play the audio clip directly using the character's AudioSource.
            else if (m_currentLipsync == LipsyncOptions.OVR)
            {
                var audioSource = character.GetComponentInChildren<AudioSource>();
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }
#endif
    }
}
