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
        /// <summary>
        /// Available lipsync options.
        /// </summary>
        public enum LipsyncOptions
        {
            FaceFX = 0,
            OVR = 1,   
        }


        #region Debug Menu Variables

        private DebugMenu m_debugMenu;       // Reference to the Debug Menu system.
        private DemoController m_controller; // Reference to the Demo Controller for character management.
        private LipsyncOptions m_currentLipsync; // Tracks the currently selected lipsync system.
        private int m_lipsyncMode;           // Stores the selected lipsync mode index.

        #endregion


        /// <summary>
        /// Initializes references to the necessary systems when the script starts.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Globals.api.GetSystem<DebugMenu>();

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
            var character = m_controller.CurrentCharacter;

            int lipsync = m_debugMenu.SelectionGrid(m_lipsyncMode, new string[] { "FaceFX", "OVR" /*, "Timeline" */ }, 2);

            if (m_lipsyncMode == lipsync)
                return;

            m_lipsyncMode = lipsync;
            if (m_lipsyncMode == 0)
            {
                m_currentLipsync = LipsyncOptions.FaceFX;
            }
            else if (m_lipsyncMode == 1)
            {
                m_currentLipsync = LipsyncOptions.OVR;
            }
            else
            {
                Debug.LogWarning("DebugMenuLipsync::OnGUISystemSelection() - Failed to parse lipsync selection.");
            }

            m_debugMenu.Space();
        }


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
    }
}
