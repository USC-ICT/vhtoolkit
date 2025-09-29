using System.Collections;
using UnityEngine;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for controlling character animations.
    /// Provides options to reset animations, preview animations, and configure animator settings.
    /// </summary>
    public class DebugMenuCCAnimation : RideMonoBehaviour
    {
        [Header("Default Animator")]
        [SerializeField] private RuntimeAnimatorController m_animator; 
        [SerializeField] private Avatar m_avatar;                      

        [Header("CC Animator")]
        [SerializeField] private RuntimeAnimatorController m_ccMaleAnimator;  
        [SerializeField] private RuntimeAnimatorController m_ccFemaleAnimator;
        [SerializeField] private Avatar m_ccMaleAvatar;                       
        [SerializeField] private Avatar m_ccFemaleAvatar;                     

        [Header("Position and Rotation")]
        [SerializeField] private Vector3 m_defaultPosition = new(); 
        [SerializeField] private Vector3 m_defaultRotation = new(); 
        [SerializeField] private Vector3 m_previewPosition = new(); 
        [SerializeField] private Vector3 m_previewRotation = new(); 

        #region Debug Menu Variables

        private DebugMenu m_debugMenu;       
        private DebugMenus m_debugMenusBase; 
        private DemoController m_controller; 

        #endregion

        private string[] m_animations = new[]
        {
            "SubtleIdleLoop",
            "WeightShiftLoop",
            "TalkingLoop",
            "LookingAroundLoop",
        };


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
        /// Handles the GUI layout for character animation settings in the Debug Menu.
        /// Provides controls for resetting animations and selecting animations to play.
        /// </summary>
        public void OnGUICCAnimation()
        {
            m_debugMenusBase.OnGUICharacterConfig();

            // Check if all required animator assets are available.
            if (!m_animator || !m_avatar || !m_ccMaleAnimator || !m_ccFemaleAnimator || !m_ccMaleAvatar || !m_ccFemaleAvatar)
            {
                m_debugMenu.Label("<b>Asset(s) is missing. Please check the Inspector window.</b>");
                return;
            }

            var character = m_controller.CurrentCharacter;

            // Ensure the character is one that supports CC animations.
            if (character.name != "John" &&
                character.name != "Maria" &&
                character.name != "ccMax" &&
                character.name != "JohnMilitary")
            {
                m_debugMenu.Label("<b>Please select a different character.</b>");
                return;
            }

            m_debugMenu.Label("<b>Reset:</b>");
            if (m_debugMenu.Button("Reset"))
            {
                StartCoroutine(Sequence_MoveAndRotateCharacter(m_defaultPosition, m_defaultRotation, 1f));

                character.GetComponent<Animator>().runtimeAnimatorController = m_animator;
                character.GetComponent<Animator>().avatar = m_avatar;
            }

            m_debugMenu.Label("<b>Animations:</b>");
            foreach (var animation in m_animations)
            {
                if (m_debugMenu.Button(animation))
                {
                    StartCoroutine(Sequence_PlayAnimation(animation, m_previewPosition, m_previewRotation));
                }
            }
        }


        /// <summary>
        /// Plays the selected animation while adjusting the character's position and rotation.
        /// </summary>
        /// <param name="animation">The animation to play.</param>
        /// <param name="startPosition">The starting position for the character.</param>
        /// <param name="startRotation">The starting rotation for the character.</param>
        private IEnumerator Sequence_PlayAnimation(string animation, Vector3 startPosition, Vector3 startRotation)
        {
            ConfigAnimator();
            var character = m_controller.CurrentCharacter;

            character.StopAnim();
            StartCoroutine(Sequence_MoveAndRotateCharacter(startPosition, startRotation, 0.5f));

            if (animation == "LookingAroundLoop")
            {
                character.StopGaze();
            }
            else
            {
                DebugMenuGaze gazeMenu = FindAnyObjectByType<DebugMenuGaze>();
                gazeMenu.GazeAt(character, "GazeTargetUser");
            }

            character.PlayAnim(animation);
            yield break;
        }


        /// <summary>
        /// Configures the animator settings based on the selected character.
        /// </summary>
        private void ConfigAnimator()
        {
            var character = m_controller.CurrentCharacter;

            if (character.name == "Maria")
            {
                character.GetComponent<Animator>().runtimeAnimatorController = m_ccFemaleAnimator;
                character.GetComponent<Animator>().avatar = m_ccFemaleAvatar;
            }
            else
            {
                character.GetComponent<Animator>().runtimeAnimatorController = m_ccMaleAnimator;
                character.GetComponent<Animator>().avatar = m_ccMaleAvatar;
            }
        }


        /// <summary>
        /// Moves and rotates the character smoothly over a set duration.
        /// </summary>
        /// <param name="targetPosition">The target position for the character.</param>
        /// <param name="targetEulerAngles">The target rotation (Euler angles) for the character.</param>
        /// <param name="duration">The duration over which the transition occurs.</param>
        private IEnumerator Sequence_MoveAndRotateCharacter(Vector3 targetPosition, Vector3 targetEulerAngles, float duration)
        {
            var character = m_controller.CurrentCharacter;

            character.transform.GetLocalPositionAndRotation(out Vector3 startPosition, out Quaternion startRotation);
            Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                character.transform.SetLocalPositionAndRotation(
                    Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration),
                    Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration));
                yield return null;
            }

            character.transform.localPosition = targetPosition;
            character.transform.localEulerAngles = targetEulerAngles;
        }
    }
}
