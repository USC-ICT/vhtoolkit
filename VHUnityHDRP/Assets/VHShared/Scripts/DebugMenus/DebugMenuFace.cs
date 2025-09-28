using System;
using UnityEngine;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for controlling facial expressions and camera positioning.
    /// Provides sliders for adjusting facial animations and buttons for nodding/shaking gestures.
    /// </summary>
    public class DebugMenuFace : RideMonoBehaviour
    {
        [SerializeField] private Camera m_camera; 

        #region Debug Menu Variables

        private DebugMenu m_debugMenu;       
        private DemoController m_controller; 
        private DebugMenus m_debugMenusBase; 
        private Vector2 m_faceScroll;        

        #endregion


        /// <summary>
        /// Initializes references to the necessary systems when the script starts.
        /// Sets the default camera if not assigned.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Globals.api.GetSystem<DebugMenu>();

            m_controller = FindAnyObjectByType<DemoController>();

            m_debugMenusBase = FindAnyObjectByType<DebugMenus>();

            if (m_camera == null)
                m_camera = Camera.main;
        }


        /// <summary>
        /// Handles the GUI layout for facial animation settings in the Debug Menu.
        /// Provides controls for adjusting facial expressions, nodding, and shaking.
        /// </summary>
        public void OnGUIFace()
        {
            using (var faceScrollView = new GUILayout.ScrollViewScope(m_faceScroll))
            {
                m_faceScroll = faceScrollView.scrollPosition;

                m_debugMenusBase.OnGUICharacterConfig();

                using (new GUILayout.HorizontalScope())
                {
                    m_debugMenu.Label($"<b>Camera</b>", 100);

                    if (m_debugMenu.Button("Head"))
                    {
                        m_camera.transform.SetPositionAndRotation(
                            new Vector3(63.567f, 3.520f, 0.280f),
                            new Quaternion(0.0f, 0.717f, 0, 0.6972f));
                    }

                    if (m_debugMenu.Button("Body"))
                    {
                        m_camera.transform.SetPositionAndRotation(
                            new Vector3(62.420f, 3.283f, 0.280f),
                            new Quaternion(0.0f, 0.717f, 0, 0.6972f));
                    }
                }

                m_debugMenu.Space();

                var character = m_controller.CurrentCharacter;
                if (character != null)
                {
                    DrawGUIFaceSlider(character, "PBM");
                    DrawGUIFaceSlider(character, "ShCh");
                    DrawGUIFaceSlider(character, "W");
                    DrawGUIFaceSlider(character, "open");
                    DrawGUIFaceSlider(character, "tBack");
                    DrawGUIFaceSlider(character, "tRoof");
                    DrawGUIFaceSlider(character, "tTeeth");
                    DrawGUIFaceSlider(character, "FV");
                    DrawGUIFaceSlider(character, "wide");

                    m_debugMenu.Space();

                    if (m_debugMenu.Button("Nod"))
                    {
                        float amount = 0.5f;
                        float numTimes = 2.0f;
                        float duration = 2.0f;
                        character.Nod(amount, numTimes, duration);
                    }

                    if (m_debugMenu.Button("Shake"))
                    {
                        float amount = 0.5f;
                        float numTimes = 2.0f;
                        float duration = 1.0f;
                        character.Shake(amount, numTimes, duration);
                    }
                }
            }
        }


        /// <summary>
        /// Displays sliders for controlling specific facial animations.
        /// Allows setting viseme strength for facial expressions.
        /// </summary>
        /// <param name="character">The character whose facial expression is being controlled.</param>
        /// <param name="name">The name of the facial animation parameter.</param>
        private void DrawGUIFaceSlider(MecanimCharacter character, string name)
        {
            using (new GUILayout.HorizontalScope())
            {
                var facialAnimator = character.GetComponent<FacialAnimationPlayer>();
                m_debugMenu.Label($"{name.Substring(0, Math.Min(8, name.Length))}", 100);

                if (m_debugMenu.Button("0")) { character.PlayViseme(name, 0); character.PlayViseme("face_neutral", 1); }
                if (m_debugMenu.Button("25")) { character.PlayViseme(name, 0.25f); character.PlayViseme("face_neutral", 0.75f); }
                if (m_debugMenu.Button("50")) { character.PlayViseme(name, 0.50f); character.PlayViseme("face_neutral", 0.50f); }
                if (m_debugMenu.Button("75")) { character.PlayViseme(name, 0.75f); character.PlayViseme("face_neutral", 0.25f); }
                if (m_debugMenu.Button("100")) { character.PlayViseme(name, 1); character.PlayViseme("face_neutral", 0); }
            }
        }
    }
}
