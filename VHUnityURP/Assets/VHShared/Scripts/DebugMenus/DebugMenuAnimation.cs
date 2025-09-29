using System.Collections.Generic;
using UnityEngine;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for controlling character animations.
    /// Provides options to change postures, standing animations, and sitting animations.
    /// </summary>
    public class DebugMenuAnimation : RideMonoBehaviour
    {
        private Vector2 m_animationPostureScroll;  
        private Vector2 m_animationListScroll; 
        private Vector2 m_animationSittingScroll;  
        private Vector2 m_animationScroll;         

        //private bool m_animationPostureToggle = true;
        //private bool m_animationStandingToggle = false;
        //private bool m_animationSittingToggle = false; 

        private DebugMenu m_debugMenu;       
        private DemoController m_controller; 
        private DebugMenus m_debugMenusBase;

        private List<string> m_animations = new();
        //private List<string> m_animations_standing = new();
        //private List<string> m_animations_seated = new();

        private string m_currentPosture = string.Empty;
        private string[] m_postures = new string[]
        {
            "IdleStandingUpright01",
            "IdleStandingLeanRtHandsOnHips01",
            "IdleStandingLeanRt01",
            "IdleSeatedUpright02",
            "IdleSeatedForward01",
            "IdleSeatedBack01",
        };


        /// <summary>
        /// Initializes references to the necessary systems when the script starts.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            // Retrieve the Debug Menu system.
            m_debugMenu = Globals.api.GetSystem<DebugMenu>();

            // Find the DemoController instance in the scene.
            m_controller = FindAnyObjectByType<DemoController>();

            // Find the DebugMenus base instance in the scene.
            m_debugMenusBase = FindAnyObjectByType<DebugMenus>();

            m_currentPosture = m_postures[0];
        }


        /// <summary>
        /// Handles the GUI layout for animation settings in the Debug Menu.
        /// Provides options to change postures, standing animations, and sitting animations.
        /// </summary>
        public void OnGUIAnimation()
        {
            // Set up custom GUI styles.
            m_debugMenusBase.OnGUICustomStylesSetup();
            if(m_animations.Count <= 0) { SetAnimationList(); }

            using (var animationScrollView = new GUILayout.ScrollViewScope(m_animationScroll))
            {
                m_animationScroll = animationScrollView.scrollPosition;

                // Display character selection UI.
                m_debugMenusBase.OnGUICharacterConfig();

                m_debugMenu.Label($"<b>Postures</b>");
                foreach (var posture in m_postures)
                {
                    if (GUILayout.Button(posture, m_debugMenusBase.m_guiButtonLeftJustify))
                    {
                        SetPosture(posture);
                        m_currentPosture = posture;
                    }
                }
                m_debugMenu.Space();

                m_debugMenu.Label($"<b>Animations</b>");
                using (var animationScrollPosition = new GUILayout.ScrollViewScope(m_animationListScroll, GUILayout.MaxHeight(600)))
                {
                    m_animationListScroll = animationScrollPosition.scrollPosition;

                    foreach (var animation in m_animations)
                    {
                        if(animation.Contains(m_currentPosture) == false) { continue; }

                        if (GUILayout.Button(animation, m_debugMenusBase.m_guiButtonLeftJustify))
                        {
                            m_controller.CurrentCharacter.PlayAnim(animation);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Sets the posture of the current character.
        /// </summary>
        /// <param name="posture">The posture to be set.</param>
        private void SetPosture(string posture)
        {
            m_controller.CurrentCharacter.PlayPosture(posture);
        }


        public void SetAnimationList()
        {
            var animator = m_controller.CurrentCharacter.GetComponent<Animator>();
            var clips = animator.runtimeAnimatorController.animationClips;

            m_animations.Clear();

            foreach (var clip in clips)
            {
                if (clip == null) { continue; }
                m_animations.Add(clip.name);
            }
            m_animations.Sort();
        }
    }
}
