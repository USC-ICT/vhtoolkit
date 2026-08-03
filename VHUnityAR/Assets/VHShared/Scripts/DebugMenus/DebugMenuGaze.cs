using System.Collections;
using UnityEngine;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for controlling gaze behavior of virtual humans.
    /// Allows setting gaze direction and adjusting gaze speed.
    /// </summary>
    public class DebugMenuGaze : RideMonoBehaviour
    {
        private DebugMenu m_debugMenu;
        private DemoController m_controller;
        private DebugMenus m_debugMenusBase;

        private bool m_useEyes = true;
        private bool m_useHead = true;
        private bool m_useBody = true;

        private float m_eyeSpeed = 70f;
        private float m_headSpeed = 50f;
        private float m_bodySpeed = 20f;

        /// <summary>
        /// Initializes references to the necessary systems when the script starts.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Systems.Get<DebugMenu>();
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

            OnGUIGazeInternal();
            OnGUIBlink();
            OnGUIHead();
        }

        public void OnGUIGazeInternal()
        {
            using (m_debugMenu.Horizontal())
            {
                m_debugMenu.Label("Parts:", 80);
                m_useEyes = m_debugMenu.Toggle(m_useEyes, "Eyes");
                m_useHead = m_debugMenu.Toggle(m_useHead, "Head");
                m_useBody = m_debugMenu.Toggle(m_useBody, "Body");
            }

            m_debugMenu.Space();


            m_debugMenu.Label("Gaze Weights (0..1):");
            var gazeController = m_controller.CurrentCharacter.GetComponent<GazeController>();
            float eyeWeight  = gazeController.EyeGazeWeight;
            float headWeight = gazeController.HeadGazeWeight;
            float bodyWeight = gazeController.BodyGazeWeight;
            float newEyeWeight;
            float newHeadWeight;
            float newBodyWeight;
            using (m_debugMenu.Horizontal())
            {
                m_debugMenu.Label("Eyes", 50);
                newEyeWeight = m_debugMenu.HorizontalSlider(eyeWeight, 0f, 1f);
                m_debugMenu.Label($"{eyeWeight:F2}", 50);
            }
            using (m_debugMenu.Horizontal())
            {
                m_debugMenu.Label("Head", 60);
                newHeadWeight = m_debugMenu.HorizontalSlider(headWeight, 0f, 1f);
                m_debugMenu.Label($"{headWeight:F2}", 50);
            }
            using (m_debugMenu.Horizontal())
            {
                m_debugMenu.Label("Body", 50);
                newBodyWeight = m_debugMenu.HorizontalSlider(bodyWeight, 0f, 1f);
                m_debugMenu.Label($"{bodyWeight:F2}", 50);
            }

            if (newEyeWeight != eyeWeight || newHeadWeight != headWeight || newBodyWeight != bodyWeight)
                m_controller.CurrentCharacter.SetGazeWeights(newHeadWeight, newEyeWeight, newBodyWeight);

            m_debugMenu.Space();

            m_debugMenu.Label("Fade-in Speeds:");
            using (m_debugMenu.Horizontal())
            {
                m_debugMenu.Label("Eyes", 50);
                m_eyeSpeed = m_debugMenu.HorizontalSlider(m_eyeSpeed, 0f, 100f);
                m_debugMenu.Label($"{m_eyeSpeed:F1}", 50);
            }
            using (m_debugMenu.Horizontal())
            {
                m_debugMenu.Label("Head", 60);
                m_headSpeed = m_debugMenu.HorizontalSlider(m_headSpeed, 0f, 100f);
                m_debugMenu.Label($"{m_headSpeed:F1}", 50);
            }
            using (m_debugMenu.Horizontal())
            {
                m_debugMenu.Label("Body", 50);
                m_bodySpeed = m_debugMenu.HorizontalSlider(m_bodySpeed, 0f, 100f);
                m_debugMenu.Label($"{m_bodySpeed:F1}", 50);
            }

            m_debugMenu.Space();

            m_debugMenu.Label("Gaze at (offset from camera):");
            using (m_debugMenu.Horizontal())
            {
                if (m_debugMenu.Button("Center")) { GazeAt("GazeTargetUser"); }
                if (m_debugMenu.Button("Up"))     { GazeAt("GazeTargetUp"); }
                if (m_debugMenu.Button("Down"))   { GazeAt("GazeTargetDown"); }
                if (m_debugMenu.Button("Left"))   { GazeAt("GazeTargetLeft"); }
                if (m_debugMenu.Button("Right"))  { GazeAt("GazeTargetRight"); }
            }

            using (m_debugMenu.Horizontal())
            {
                if (m_debugMenu.Button("UpLeft"))    { GazeAt("GazeTargetUpLeft"); }
                if (m_debugMenu.Button("UpRight"))   { GazeAt("GazeTargetUpRight"); }
                if (m_debugMenu.Button("DownLeft"))  { GazeAt("GazeTargetDownLeft"); }
                if (m_debugMenu.Button("DownRight")) { GazeAt("GazeTargetDownRight"); }
            }

            m_debugMenu.Space();

            if (m_debugMenu.Button("Off")) { m_controller.CurrentCharacter.StopGaze(); }
        }

        /// <summary>
        /// Draw debug menu for triggering a blink on the character.
        /// </summary>
        private void OnGUIBlink()
        {
            using (m_debugMenu.Horizontal()) //Todo: Investigate soft look
            {
                m_debugMenu.Label("Blink", 150);
                if (m_debugMenu.Button("Blink")) { m_controller.CurrentCharacter.GetComponent<BlinkController>().Blink(); }
            }
        }

        /// <summary>
        /// Draw debug menu for nodding and shaking the character's head.
        /// </summary>
        private void OnGUIHead()
        {
            using (m_debugMenu.Horizontal())
            {
                m_debugMenu.Label("Head Control", 150);
                if (m_debugMenu.Button("Nod"))
                {
                    float amount = 0.5f;
                    float numTimes = 2.0f;
                    float duration = 2.0f;
                    m_controller.CurrentCharacter.Nod(amount, numTimes, duration);
                }
                if (m_debugMenu.Button("Shake"))
                {
                    float amount = 0.5f;
                    float numTimes = 2.0f;
                    float duration = 1.0f;
                    m_controller.CurrentCharacter.Shake(amount, numTimes, duration);
                }
            }
        }

        /// <summary>
        /// Makes the character gaze at the specified target.
        /// </summary>        
        /// <param name="gazeTargetString">The name of the gaze target object.</param>
        public void GazeAt(string gazeTargetString)
        {
            StartCoroutine(GazeSequence(m_controller.CurrentCharacter, gazeTargetString));
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
            if (gazeTarget == null)
                yield break;

            // Then compute speeds. If a part is toggled off, speed 0 will cause fade-out.
            float eyeSpeed  = m_useEyes ? m_eyeSpeed  : 0f;
            float headSpeed = m_useHead ? m_headSpeed : 0f;
            float bodySpeed = m_useBody ? m_bodySpeed : 0f;

            character.SetGazeTargetWithSpeed(gazeTarget, headSpeed, eyeSpeed, bodySpeed);
        }
    }
}
