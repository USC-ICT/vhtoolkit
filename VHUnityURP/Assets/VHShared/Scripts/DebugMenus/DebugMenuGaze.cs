using System.Collections;
using UnityEngine;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for controlling gaze behavior of virtual characters.
    /// Allows setting gaze direction and adjusting gaze speed.
    /// </summary>
    public class DebugMenuGaze : RideMonoBehaviour
    {
        [SerializeField] private float m_gazeHeadSpeed = 50f; // Speed of the gaze movement.

        private DebugMenu m_debugMenu;       
        private DemoController m_controller; 
        private DebugMenus m_debugMenusBase; 


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
        /// Handles the GUI layout for gaze settings in the Debug Menu.
        /// Provides buttons for selecting gaze direction and adjusting speed.
        /// </summary>
        public void OnGUIGaze()
        {
            m_debugMenusBase.OnGUICharacterConfig();

            using (new GUILayout.HorizontalScope())
            {
                m_debugMenu.Label("Speed", 100);
                m_gazeHeadSpeed = m_debugMenu.HorizontalSlider(m_gazeHeadSpeed, 10, 100);
                m_debugMenu.Label($"{m_gazeHeadSpeed:f1}", 80);
            }

            var character = m_controller.CurrentCharacter;

            using (new GUILayout.HorizontalScope())
            {
                if (m_debugMenu.Button("UpLeft")) { GazeAt(character, "GazeTargetUpLeft"); }
                if (m_debugMenu.Button("Up")) { GazeAt(character, "GazeTargetUp"); }
                if (m_debugMenu.Button("UpRight")) { GazeAt(character, "GazeTargetUpRight"); }
            }

            using (new GUILayout.HorizontalScope())
            {
                if (m_debugMenu.Button("Left")) { GazeAt(character, "GazeTargetLeft"); }
                if (m_debugMenu.Button("Center")) { GazeAt(character, "GazeTargetUser"); }
                if (m_debugMenu.Button("Right")) { GazeAt(character, "GazeTargetRight"); }
            }

            using (new GUILayout.HorizontalScope())
            {
                if (m_debugMenu.Button("DownLeft")) { GazeAt(character, "GazeTargetDownLeft"); }
                if (m_debugMenu.Button("Down")) { GazeAt(character, "GazeTargetDown"); }
                if (m_debugMenu.Button("DownRight")) { GazeAt(character, "GazeTargetDownRight"); }
            }

            m_debugMenu.Space();

            if (m_debugMenu.Button("Off")) { StopGaze(character); }
        }


        /// <summary>
        /// Makes the character gaze at the specified target.
        /// </summary>
        /// <param name="character">The character that will gaze.</param>
        /// <param name="gazeTargetString">The name of the gaze target object.</param>
        public void GazeAt(MecanimCharacter character, string gazeTargetString)
        {
            StartCoroutine(GazeSequence(character, gazeTargetString));
        }


        /// <summary>
        /// Coroutine to handle gaze direction changes with a small delay.
        /// Fixes a bug where gaze control does not work immediately after activation.
        /// </summary>
        /// <param name="character">The character that will gaze.</param>
        /// <param name="gazeTargetString">The name of the gaze target object.</param>
        private IEnumerator GazeSequence(MecanimCharacter character, string gazeTargetString)
        {
            var gazeTarget = GameObject.Find(gazeTargetString);

            // There is a known issue where gaze needs a two-frame delay after activation.
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            // Set gaze target with specified speed.
            character.SetGazeTargetWithSpeed(gazeTarget, m_gazeHeadSpeed, m_gazeHeadSpeed, m_gazeHeadSpeed);
        }


        /// <summary>
        /// Stops the gaze movement of the character.
        /// </summary>
        /// <param name="character">The character whose gaze will be stopped.</param>
        public void StopGaze(MecanimCharacter character)
        {
            character.StopGaze();
        }
    }
}
