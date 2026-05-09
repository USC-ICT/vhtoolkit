using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ride;
using Ride.IO;

namespace VH
{
    /// <summary>
    /// Debug menu for playing Virtual Human animations using RIDE's DebugMenu chrome.
    /// - Two tabs: Male / Female (configurable by assigning root transforms)
    /// - Groups gestures by Idle "hub" token (hard-coded hub list, gestures discovered at runtime)
    /// - Doubles as a light validator that Animator state names match expected naming (hub_*)
    /// </summary>
    public class DebugMenu_Animations : RideMonoBehaviour
    {
        #region Config

        public Animator [] m_characters;
        int m_currentCharacter = 0;

        private Vector3[] m_startLocalPos;
        private float m_moveDistance = 1f;   // Units per left/right tap
        private float m_moveDuration = 0.1f; // Smoothing time (lower = snappier)
        private float m_targetX = 0f;
        private float m_currentX = 0f;
        private float m_velX = 0f;

        [Header("Controller names")]
        private string m_ictMaleControllerName = "IctMaleAnimatorController";
        private string m_ictFemaleControllerName = "IctFemaleAnimatorController";
        private string m_rocketboxMaleControllerName = "RocketboxMaleAnimatorController";
        private string m_rocketboxFemaleControllerName = "RocketboxFemaleAnimatorController";

        [Header("Idle hubs (hard-coded tokens; gestures discovered at runtime)")]
        private string[] m_maleIdleTokens = new[]
        {
            // Hub tokens; idle state names inferred as Token unless overridden below.
            // These match your library naming; adjust if you rename hubs.
            "OG_IdleStandingUpright01",
            "IdleStandingUpright01",
            "Standing01",
            "IdleStandingLeanRt01",
            "IdleStandingLeanRtHandsOnHips01",
            "IdleSeatedBack01",
            "IdleSeatedBack02",
            "IdleSeatedForward01",
            "IdleSeatedUpright02",
        };

        [Tooltip("Optional explicit idle state names for hubs that do NOT use Token_Idle naming.")]
        private Dictionary<string, string> m_maleIdleOverrides = new()
        {
            // Format: Token, IdleStateName
            // e.g. "Standing01, Standing01_Idle" (_Idle suffix in your project)
            { "Standing01", "Standing01_Idle" },
        };

        private string[] m_femaleIdleTokens = new[]
        {
            "OG_IdleStandingUpright01",
            "IdleStandingUpright01",
            "IdleStandingLeanRt01",
            "IdleStandingLeanRtHandsInBack01",
            "IdleStandingLeanRtHandsInFront01",
            "IdleStandingLeanRtHandsOnHips01",
            "IdleSeatedBack01",
            "IdleSeatedForward01",
            "IdleSeatedUpright01",
        };

        private Dictionary<string, string> m_femaleIdleOverrides = new();
        #endregion

        #region State
        private DebugMenu m_debugMenu;

        // Controller -> animators (all found in scene)
        private readonly Dictionary<string, List<Animator>> m_controllerNameToAnimators = new();
        // Controller -> sorted unique state names on layer 0 (union of clips)
        private readonly Dictionary<string, List<string>> m_controllerNameToStates = new();

        // Per-tab UI state
        private readonly Dictionary<string, bool> m_maleExpanded = new();
        private readonly Dictionary<string, bool> m_femaleExpanded = new();
        private Vector2 m_maleScroll;
        private Vector2 m_femaleScroll;
        private Vector2 m_faceScroll;

        private Vector3 m_camPosition;
        private Quaternion m_camRotation;

        private Dictionary<string, float> m_visemeValues = new ()
        {
            { "PBM", 0 },
            { "ShCh", 0 },
            { "W", 0 },
            { "open", 0 },
            { "tBack", 0 },
            { "tRoof", 0 },
            { "tTeeth", 0 },
            { "FV", 0 },
            { "wide", 0 },
        };
        #endregion

        private bool m_showBlendShapes;
        private readonly Dictionary<string, float> m_blendShapeValues = new();

        GUIStyle m_guiButtonLeftJustify;

        int m_selectedLightingIndex = -1;
        public List<GameObject> m_lightingChoices;
        const string lightingPrefix = "LightingConfig-";


        private void Awake()
        {
            // https://discussions.unity.com/t/on-play-dont-destroy-on-load-with-a-debug-updater-object-is-created-automatically/824863/12
            UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        }

        #region Unity
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Systems.Get<DebugMenu>();

            // Build initial maps of controllers/animators/states
            RebuildControllerMaps();

            // Insert two menus (Male / Female). Keep ordering stable.
            m_debugMenu.InsertMenu(0, "Main", OnGUIMain);
            m_debugMenu.InsertMenu(1, "Animations: ICT Male", OnGUIMale);
            m_debugMenu.InsertMenu(2, "Animations: ICT Female", OnGUIFemale);
            m_debugMenu.InsertMenu(3, "Animations: Rocketbox Male", OnGUIRocketboxMale);
            m_debugMenu.InsertMenu(4, "Animations: Rocketbox Female", OnGUIRocketboxFemale);
            m_debugMenu.InsertMenu(5, "Face", OnGUIFace);

            // Show and size to right pane by default
            m_debugMenu.SetMenu(0);
            m_debugMenu.ShowMenu(true);
            m_debugMenu.SetMenuSize(0, 0, 0.4f, 1f);
            m_debugMenu.SetWideMenuSize(0, 0, 0.5f, 1f);


            // store start position
            m_startLocalPos = new Vector3[m_characters.Length];
            for (int i = 0; i < m_characters.Length; i++)
            {
                if (m_characters[i] == null)
                    continue;
                m_startLocalPos[i] = m_characters[i].transform.localPosition;
            }

            var camera = FindAnyObjectByType<Camera>(FindObjectsInactive.Exclude);
            camera.transform.GetPositionAndRotation(out m_camPosition, out m_camRotation);

            int activeIndex = m_lightingChoices.FindIndex(g => g.activeSelf);
            m_selectedLightingIndex = activeIndex >= 0 ? activeIndex : 0;
        }

        protected override void Update()
        {
            if (Systems.Input.GetKeyDown(RideKeyCode.Escape))
                RideUtils.QuitApplication();

            if (Systems.Input.GetKeyDown(RideKeyCode.F11))
                m_debugMenu.ToggleMenu();

            if (Systems.Input.GetKeyDown(RideKeyCode.Alpha1)) CameraMale();
            if (Systems.Input.GetKeyDown(RideKeyCode.Alpha2)) CameraFemale();
            if (Systems.Input.GetKeyDown(RideKeyCode.Alpha3)) CameraMaleHead();
            if (Systems.Input.GetKeyDown(RideKeyCode.Alpha4)) CameraMaleHands();
            if (Systems.Input.GetKeyDown(RideKeyCode.Alpha5)) CameraFemaleHead();
            if (Systems.Input.GetKeyDown(RideKeyCode.Alpha6)) CameraFemaleHands();
            if (Systems.Input.GetKeyDown(RideKeyCode.Alpha7)) CameraReset();

            if (Input.GetKeyDown(KeyCode.Comma)) PreviousCharacter();
            if (Input.GetKeyDown(KeyCode.Period)) NextCharacter();

            ProcessCharacterSwap();
        }

        private void PreviousCharacter()
        {
            if (m_currentCharacter == 0)
                return;

            m_currentCharacter--;
            m_targetX -= m_moveDistance;

            var animator = m_characters[m_currentCharacter];
            if (animator != null &&
                animator.runtimeAnimatorController != null)
            {
                var baseCtrl = GetBaseController(animator.runtimeAnimatorController);
                var name = baseCtrl != null ? baseCtrl.name : animator.runtimeAnimatorController.name;
                switch (name)
                {
                    case "IctMaleAnimatorController": if (m_debugMenu.GetCurrentMenu() != 5) m_debugMenu.SetMenu(1); break;
                    case "IctFemaleAnimatorController": if (m_debugMenu.GetCurrentMenu() != 5) m_debugMenu.SetMenu(2); break;
                    case "RocketboxMaleAnimatorController": if (m_debugMenu.GetCurrentMenu() != 5) m_debugMenu.SetMenu(3); break;
                    case "RocketboxFemaleAnimatorController": if (m_debugMenu.GetCurrentMenu() != 5) m_debugMenu.SetMenu(4); break;
                    default: m_debugMenu.SetMenu(0); break;
                }
            }
        }

        private void NextCharacter()
        {
            if (m_currentCharacter == m_characters.Length - 1)
                return;

            m_currentCharacter++;
            m_targetX += m_moveDistance;

            var animator = m_characters[m_currentCharacter];
            if (animator != null &&
                animator.runtimeAnimatorController != null)
            {
                var baseCtrl = GetBaseController(animator.runtimeAnimatorController);
                var name = baseCtrl != null ? baseCtrl.name : animator.runtimeAnimatorController.name;
                switch (name)
                {
                    case "IctMaleAnimatorController": if (m_debugMenu.GetCurrentMenu() != 5) m_debugMenu.SetMenu(1); break;
                    case "IctFemaleAnimatorController": if (m_debugMenu.GetCurrentMenu() != 5) m_debugMenu.SetMenu(2); break;
                    case "RocketboxMaleAnimatorController": if (m_debugMenu.GetCurrentMenu() != 5) m_debugMenu.SetMenu(3); break;
                    case "RocketboxFemaleAnimatorController": if (m_debugMenu.GetCurrentMenu() != 5) m_debugMenu.SetMenu(4); break;
                    default: m_debugMenu.SetMenu(0); break;
                }
            }
        }

        private void ProcessCharacterSwap()
        {
            m_currentX = Mathf.SmoothDamp(m_currentX, m_targetX, ref m_velX, m_moveDuration);

            for (int i = 0; i < m_characters.Length; i++)
            {
                if (m_characters[i] == null)
                    continue;

                var desired = m_startLocalPos[i] + Vector3.right * m_currentX;

                if (m_characters[i].transform.localPosition == desired)
                    continue;

                m_characters[i].transform.localPosition = desired;
            }
        }

        #endregion

        #region Build / Query
        private void RebuildControllerMaps()
        {
            m_controllerNameToAnimators.Clear();
            m_controllerNameToStates.Clear();

            foreach (var a in m_characters)
            {
                if (a == null)
                    continue;

                var ctrl = a.runtimeAnimatorController;
                if (ctrl == null)
                    continue;

                var baseCtrl = GetBaseController(ctrl);
                if (baseCtrl == null)
                    continue;

                string keyName = baseCtrl.name;

                Debug.Log($"RebuildControllerMaps() - animator '{a.name}' uses controller '{ctrl.name}' (base: '{keyName}')");

                if (!m_controllerNameToAnimators.TryGetValue(keyName, out var list))
                {
                    list = new List<Animator>();
                    m_controllerNameToAnimators[keyName] = list;
                }

                if (!list.Contains(a))
                    list.Add(a);

                // States (names) per controller
                if (!m_controllerNameToStates.ContainsKey(keyName))
                {
                    var names = new HashSet<string>(StringComparer.Ordinal);
                    foreach (var clip in baseCtrl.animationClips)
                    {
                        if (clip == null)
                            continue;

                        //Debug.Log($"RebuildControllerMaps() -   controller '{ctrl.name}', adding anim '{clip.name}'");

                        names.Add(clip.name);
                    }

                    var sorted = names.ToList();
                    sorted.Sort(StringComparer.Ordinal);
                    m_controllerNameToStates[keyName] = sorted;
                }
            }
        }

        private string InferControllerNameForRoots()
        {
            var counts = new Dictionary<string, int>(StringComparer.Ordinal);
            foreach (var kvp in m_controllerNameToAnimators)
            {
                var name = kvp.Key;
                if (!counts.ContainsKey(name))
                    counts[name] = 0;

                counts[name] += kvp.Value?.Count ?? 0;
            }

            // pick the controller with the most animators under these roots
            string best = null;
            int bestCount = -1;
            foreach (var p in counts)
            {
                if (p.Value > bestCount)
                {
                    best = p.Key;
                    bestCount = p.Value;
                }
            }
            return best;
        }

        #endregion

        #region UI (tabs)

        public void CameraMale() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(0, 1.48f, 1.42f), Quaternion.Euler(8, 180, 0));
        public void CameraFemale() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(-2, 1.48f, 1.42f), Quaternion.Euler(8, 180, 0));
        public void CameraMaleHead() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(0, 1.6f, 0.5f), Quaternion.Euler(8, 180, 0));
        public void CameraMaleHands() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(-.5f, 0.9f, 0), Quaternion.Euler(8, 90, 0));
        public void CameraFemaleHead() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(-2, 1.6f, 0.5f), Quaternion.Euler(8, 180, 0));
        public void CameraFemaleHands() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(-2.5f, 0.9f, 0), Quaternion.Euler(8, 90, 0));
        public void CameraReset() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(0, 1.48f, 1.42f), Quaternion.Euler(8, 180, 0));

        private void OnGUIMain()
        {
            m_debugMenu.Label("Keyboard keys mapped to each button");

            if (m_debugMenu.Button("1) Male")) CameraMale();
            if (m_debugMenu.Button("2) Female")) CameraFemale();
            if (m_debugMenu.Button("3) Male Head")) CameraMaleHead();
            if (m_debugMenu.Button("4) Male Hands")) CameraMaleHands();
            if (m_debugMenu.Button("5) Female Head")) CameraFemaleHead();
            if (m_debugMenu.Button("6) Female Hands")) CameraFemaleHands();
            if (m_debugMenu.Button("7) Reset")) CameraReset();

            m_debugMenu.Label("Keyboard keys 'comma' and 'period'");
            m_debugMenu.Label("to cycle through characters");

            if (m_debugMenu.Button("Hide Window"))
                m_debugMenu.ToggleMenu();

            if (m_debugMenu.Button("Reset Camera"))
            {
                var camera = FindAnyObjectByType<Camera>(FindObjectsInactive.Exclude);
                camera.transform.SetPositionAndRotation(m_camPosition, m_camRotation);
            }

            // Lighting
            m_debugMenu.Space();
            m_debugMenu.Label("<b>Lighting</b>");

            if (m_lightingChoices.Count == 0)
            {
                m_debugMenu.Label("No LightingConfig- objects found in scene.");
            }
            else
            {
                for (int i = 0; i < m_lightingChoices.Count; i++)
                {
                    var go = m_lightingChoices[i];

                    string display = go.name.Length > lightingPrefix.Length ? go.name.Substring(lightingPrefix.Length) : go.name;
                    bool isSelected = m_selectedLightingIndex == i;
                    bool toggled = m_debugMenu.Toggle(isSelected, display);
                    if (toggled && !isSelected)
                    {
                        m_selectedLightingIndex = i;

                        // Disable all, enable only the selected
                        foreach (var g in m_lightingChoices)
                            g.SetActive(false);

                        go.SetActive(true);
                    }
                }
            }
        }

        private void OnGUIMale()
        {
            LeftJustifySetup();

            RenderAnimationPanel(
                idleTokens: m_maleIdleTokens,
                idleOverride: m_maleIdleOverrides,
                expanded: m_maleExpanded,
                requiredControllerName: m_ictMaleControllerName,
                ref m_maleScroll);
        }

        private void OnGUIFemale()
        {
            LeftJustifySetup();

            RenderAnimationPanel(
                idleTokens: m_femaleIdleTokens,
                idleOverride: m_femaleIdleOverrides,
                expanded: m_femaleExpanded,
                requiredControllerName: m_ictFemaleControllerName,
                ref m_femaleScroll);
        }

        private void OnGUIRocketboxMale()
        {
            LeftJustifySetup();

            RenderAnimationPanel(
                idleTokens: m_maleIdleTokens,
                idleOverride: m_maleIdleOverrides,
                expanded: m_maleExpanded,
                requiredControllerName: m_rocketboxMaleControllerName,
                ref m_maleScroll);
        }

        private void OnGUIRocketboxFemale()
        {
            LeftJustifySetup();

            RenderAnimationPanel(
                idleTokens: m_femaleIdleTokens,
                idleOverride: m_femaleIdleOverrides,
                expanded: m_femaleExpanded,
                requiredControllerName: m_rocketboxFemaleControllerName,
                ref m_femaleScroll);
        }

        public void OnGUIFace()
        {
            LeftJustifySetup();

            using (var faceScrollView = new GUILayout.ScrollViewScope(m_faceScroll))
            {
                m_faceScroll = faceScrollView.scrollPosition;

                using (m_debugMenu.Horizontal())
                {
                    m_debugMenu.Label($"<b>Camera</b>", 100);

                    if (m_debugMenu.Button("Head"))
                    {
                        var camera = FindAnyObjectByType<Camera>(FindObjectsInactive.Exclude);
                        var facePos = m_camPosition; facePos.y += 0.2f; facePos.z -= 0.9f;
                        camera.transform.SetPositionAndRotation(facePos, m_camRotation);
                    }

                    if (m_debugMenu.Button("Body"))
                    {
                        var camera = FindAnyObjectByType<Camera>(FindObjectsInactive.Exclude);
                        camera.transform.SetPositionAndRotation(m_camPosition, m_camRotation);
                    }
                }

                m_debugMenu.Space();

                using (m_debugMenu.Horizontal())
                {
                    m_debugMenu.Label("<b>Visemes</b>", 100);
                    if (m_debugMenu.Button("All Off", 60))
                        SetAllVisemes(0f);
                }

                foreach (var visemeName in m_visemeValues.Keys.ToList())
                    DrawGUIFaceSlider(visemeName);

                m_debugMenu.Space();

                using (m_debugMenu.Horizontal())
                {
                    m_debugMenu.Label("<b>Blendshapes</b>", 120);

                    if (m_debugMenu.Button(m_showBlendShapes ? "Hide" : "Show", 60))
                        m_showBlendShapes = !m_showBlendShapes;

                    if (m_debugMenu.Button("All 0", 60))
                        SetAllBlendShapes(0f);
                }

                if (m_showBlendShapes)
                {
                    foreach (string shapeName in GetAllBlendShapeNames())
                        DrawGUIBlendShapeSlider(shapeName);
                }

                //if (m_debugMenu.Button("Nod"))
                //{
                //    float amount = 0.5f;
                //    float numTimes = 2.0f;
                //    float duration = 2.0f;
                //    //character.Nod(amount, numTimes, duration);
                //    Nod(amount, numTimes, duration);
                //}

                //if (m_debugMenu.Button("Shake"))
                //{
                //    float amount = 0.5f;
                //    float numTimes = 2.0f;
                //    float duration = 1.0f;
                //    //character.Shake(amount, numTimes, duration);
                //    Shake(amount, numTimes, duration);
                //}

                //if (m_debugMenu.Button("Blink"))
                //    Blink();

                //SaccadeInfo();
            }
        }
        #endregion

        #region UI (panel + helpers)
        private void RenderAnimationPanel(
            string[] idleTokens,
            Dictionary<string, string> idleOverride,
            Dictionary<string, bool> expanded,
            string requiredControllerName,
            ref Vector2 scroll)
        {
            if (idleTokens == null || idleTokens.Length == 0)
            {
                m_debugMenu.Label("No idle hubs configured for this tab.");
                return;
            }

            // Determine which controller name to show on this tab
            var controllerName = requiredControllerName;
            if (string.IsNullOrWhiteSpace(controllerName))
                controllerName = InferControllerNameForRoots();

            if (string.IsNullOrWhiteSpace(controllerName))
            {
                m_debugMenu.Label("No matching controller found under these roots.");
                return;
            }

            m_debugMenu.Label($"Controller: {controllerName}");

            var animator = m_characters[m_currentCharacter];
            if (animator != null)
            {
                try
                {
                    int layer = 0;

                    var st = animator.GetCurrentAnimatorStateInfo(layer);
                    string stateName = "(unknown)";

                    // Look up the state by hash (Animator stores only hash)
                    foreach (var kvp in m_controllerNameToStates)
                    {
                        foreach (var s in kvp.Value)
                        {
                            if (Animator.StringToHash(s) == st.shortNameHash)
                            {
                                stateName = s;
                                break;
                            }
                        }
                    }

                    m_debugMenu.Label($"Current: {stateName}");
                    m_debugMenu.Label($"Time: {(st.normalizedTime % 1f):0.00}");
                    string transitionLabel = "";
                    if (animator.IsInTransition(layer))
                    {
                        var ts = animator.GetAnimatorTransitionInfo(layer);
                        transitionLabel = $"Transition t: {ts.duration:0.00}";
                    }

                    m_debugMenu.Label(transitionLabel);
                }
                catch { }  // Defensive: do nothing if animator not ready
            }

            using (var scrollViewScope = new GUILayout.ScrollViewScope(scroll))
            {
                scroll = scrollViewScope.scrollPosition;

                // Only iterate controllers that match the required name
                foreach (var kvp in m_controllerNameToAnimators)
                {
                    var kvpControllerName = kvp.Key;

                    if (!string.Equals(kvpControllerName, controllerName, StringComparison.Ordinal))
                        continue;

                    var animators = kvp.Value;
                    if (!m_controllerNameToStates.TryGetValue(kvpControllerName, out var stateNames))
                        continue;

                    foreach (var token in idleTokens)
                    {
                        var idleState = ResolveIdleStateName(token, idleOverride);
                        var hasIdle = stateNames.Contains(idleState);

                        var gestures = stateNames
                            .Where(n => n.StartsWith(token + "_", StringComparison.Ordinal))
                            .Where(n => n != idleState)
                            .ToList();

                        var key = kvpControllerName + "::" + token;
                        if (!expanded.ContainsKey(key)) expanded[key] = false;

                        using (m_debugMenu.Horizontal())
                        {
                            expanded[key] = m_debugMenu.Toggle(expanded[key], token + (hasIdle ? "" : " (idle missing)"));
                            if (m_debugMenu.Button("Set", 60))
                                PlayOnAnimators(animators, idleState, 0.5f);
                        }

                        if (expanded[key])
                        {
                            if (gestures.Count == 0)
                            {
                                m_debugMenu.Label("(no gestures found for this hub)");
                            }
                            else
                            {
                                foreach (var g in gestures)
                                {
                                    if (GUILayout.Button(g, m_guiButtonLeftJustify))
                                        PlayOnAnimators(animators, g, 0.1f);
                                }
                            }
                        }
                    }

                    m_debugMenu.Space();
                }
            }
        }

        private static string ResolveIdleStateName(string token, Dictionary<string, string> overrides)
        {
            if (overrides != null && overrides.TryGetValue(token, out var idle))
                return idle;
            return token; // default convention
        }

        private static void PlayOnAnimators(List<Animator> animators, string stateName, float blendTime)
        {
            if (animators == null || string.IsNullOrEmpty(stateName))
                return;

            foreach (var a in animators)
            {
                if (a == null)
                    continue;

                //a.Play(stateName, 0, 0f);
                a.CrossFadeInFixedTime(stateName, blendTime, 0);
            }
        }
        #endregion

        private void DrawGUIFaceSlider(string name)
        {
            using (m_debugMenu.Horizontal())
            {
                var currentValue = m_visemeValues[name];
                float newValue = currentValue;
                bool changed = false;

                m_debugMenu.Label(name.Substring(0, Math.Min(8, name.Length)), 60);

                if (m_debugMenu.Button("0", 30)) { newValue = 0f; changed = true; }
                if (m_debugMenu.Button("1", 30)) { newValue = 1f; changed = true; }

                float sliderValue = m_debugMenu.HorizontalSlider(newValue, 0f, 1f);
                if (!Mathf.Approximately(sliderValue, newValue))
                {
                    newValue = sliderValue;
                    changed = true;
                }

                string textValue = newValue.ToString("0.00");
                string newTextValue = m_debugMenu.TextField(textValue, 110);

                if (!string.Equals(newTextValue, textValue, StringComparison.Ordinal))
                {
                    if (float.TryParse(newTextValue, out var parsed))
                    {
                        newValue = Mathf.Clamp01(parsed);
                        changed = true;
                    }
                }

                if (changed && !Mathf.Approximately(newValue, currentValue))
                {
                    m_visemeValues[name] = newValue;
                    Viseme(name, newValue);
                }
            }
        }

        private void DrawGUIBlendShapeSlider(string shapeName)
        {
            using (m_debugMenu.Horizontal())
            {
                float currentValue = 0f;
                if (m_blendShapeValues.TryGetValue(shapeName, out var existing))
                    currentValue = existing;

                float newValue = currentValue;
                bool changed = false;

                string label = shapeName;

                if (label.StartsWith("blendShape1."))
                    label = label.Substring("blendShape1.".Length);

                // Keep the label length reasonable
                if (label.Length > 28)
                    label = label.Substring(0, 28);

                if (!SelectedCharacterHasBlendShape(shapeName))
                    label = $"<color=#666666>{label}</color>";

                m_debugMenu.Label(label, 200);

                if (m_debugMenu.Button("0", 30)) { newValue = 0f; changed = true; }
                if (m_debugMenu.Button("100", 45)) { newValue = 100f; changed = true; }

                float sliderValue = m_debugMenu.HorizontalSlider(newValue, 0f, 100f);
                if (!Mathf.Approximately(sliderValue, newValue))
                {
                    newValue = sliderValue;
                    changed = true;
                }

                string textValue = newValue.ToString("0.0");
                string newTextValue = m_debugMenu.TextField(textValue, 70);

                if (!string.Equals(newTextValue, textValue, StringComparison.Ordinal))
                {
                    if (float.TryParse(newTextValue, out var parsed))
                    {
                        newValue = Mathf.Clamp(parsed, 0f, 100f);
                        changed = true;
                    }
                }

                if (changed && !Mathf.Approximately(newValue, currentValue))
                {
                    m_blendShapeValues[shapeName] = newValue;
                    ApplyBlendShapeToAllCharacters(shapeName, newValue);
                }
            }
        }

        //void Nod(float amount, float numTimes, float duration)
        //{
        //    foreach (var c in m_characters)
        //    {
        //        var mecanim = c.GetComponent<MecanimCharacter>();
        //        if (mecanim != null)
        //            mecanim.Nod(amount, numTimes, duration);
        //    }
        //}

        //void Shake(float amount, float numTimes, float duration)
        //{
        //    foreach (var c in m_characters)
        //    {
        //        var mecanim = c.GetComponent<MecanimCharacter>();
        //        if (mecanim != null)
        //            mecanim.Shake(amount, numTimes, duration);
        //    }
        //}

        private void SetAllVisemes(float value)
        {
            foreach (var name in m_visemeValues.Keys.ToList())
            {
                m_visemeValues[name] = value;
                Viseme(name, value);
            }
        }

        void Viseme(string name, float amount)
        {
            m_visemeValues[name] = amount;
            float neutralAmount = ComputeNeutralAmountFromVisemes();

            foreach (var c in m_characters)
            {
                //var mecanim = c.GetComponent<MecanimCharacter>();
                //if (mecanim == null)
                //    continue;

                //var facialAnimator = character.GetComponent<FacialAnimationPlayer>();
                //mecanim.PlayViseme(name, amount);
                //mecanim.PlayViseme("face_neutral", neutralAmount);

                var anim = c.GetComponent<Animator>();
                if (anim == null)
                    continue;

                anim.SetFloat(name, amount);
                anim.SetFloat("face_neutral", neutralAmount);
            }
        }

        //void Blink()
        //{
        //    foreach (var c in m_characters)
        //    {
        //        var mecanim = c.GetComponent<MecanimCharacter>();
        //        if (mecanim == null)
        //            continue;
        //
        //        var blink = c.GetComponent<BlinkController>();
        //        if (blink == null)
        //            continue;
        //
        //        blink.Blink();
        //    }
        //}

        //void SaccadeInfo()
        //{
        //    var character = m_characters[m_currentCharacter];
        //    var saccade = character.GetComponent<SaccadeController>();
        //    if (saccade == null)
        //        return;
        //
        //    m_debugMenu.Label("<b>Saccades</b>", 100);
        //    m_debugMenu.Label($"Enabled: {saccade.AreSaccadesOn}");
        //    using (m_debugMenu.Horizontal())
        //    {
        //        var currentValue = saccade.MagnitudeScaler;
        //        float newValue = currentValue;
        //        bool changed = false;
        //
        //        m_debugMenu.Label("Magnitude", 120);
        //
        //        if (m_debugMenu.Button("0", 30)) { newValue = 0f; changed = true; }
        //
        //        float sliderValue = m_debugMenu.HorizontalSlider(newValue, 0f, 10f);
        //        if (!Mathf.Approximately(sliderValue, newValue))
        //        {
        //            newValue = sliderValue;
        //            changed = true;
        //        }
        //
        //        string textValue = newValue.ToString("0.00");
        //        string newTextValue = m_debugMenu.TextField(textValue, 110);
        //
        //        if (!string.Equals(newTextValue, textValue, StringComparison.Ordinal))
        //        {
        //            if (float.TryParse(newTextValue, out var parsed))
        //            {
        //                newValue = Mathf.Clamp01(parsed);
        //                changed = true;
        //            }
        //        }
        //
        //        if (changed && !Mathf.Approximately(newValue, currentValue))
        //            saccade.MagnitudeScaler = newValue;
        //    }
        //
        //    using (m_debugMenu.Horizontal())
        //    {
        //        if (m_debugMenu.Button("Listen")) saccade.SetBehaviourMode(CharacterDefines.SaccadeType.Listen);
        //        if (m_debugMenu.Button("Talk"))   saccade.SetBehaviourMode(CharacterDefines.SaccadeType.Talk);
        //        if (m_debugMenu.Button("Think"))  saccade.SetBehaviourMode(CharacterDefines.SaccadeType.Think);
        //    }
        //}

        private float ComputeNeutralAmountFromVisemes()
        {
            // Assumption: we treat all viseme weights as sharing the 0–1 budget,
            // so neutral = 1 - sum(visemeValues), clamped to [0,1].
            float total = 0f;

            foreach (var kvp in m_visemeValues)
                total += Mathf.Clamp01(kvp.Value);

            return Mathf.Clamp01(1f - total);
        }

        private void SetAllBlendShapes(float value)
        {
            foreach (string shapeName in GetAllBlendShapeNames())
            {
                m_blendShapeValues[shapeName] = value;
                ApplyBlendShapeToAllCharacters(shapeName, value);
            }
        }

        private IEnumerable<string> GetAllBlendShapeNames()
        {
            // Debug-only: recompute each draw; no caching.
            HashSet<string> names = new(StringComparer.Ordinal);

            foreach (var anim in m_characters)
            {
                if (anim == null)
                    continue;

                foreach (var smr in anim.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    if (smr == null)
                        continue;

                    Mesh mesh = smr.sharedMesh;
                    if (mesh == null)
                        continue;

                    int count = mesh.blendShapeCount;
                    for (int i = 0; i < count; i++)
                    {
                        string n = mesh.GetBlendShapeName(i);
                        if (!string.IsNullOrEmpty(n))
                            names.Add(n);
                    }
                }
            }

            List<string> sorted = names.ToList();
            sorted.Sort(StringComparer.Ordinal);

            // Ensure we have stable slider values for newly discovered shapes.
            foreach (string n in sorted)
            {
                if (!m_blendShapeValues.ContainsKey(n))
                    m_blendShapeValues[n] = 0f;
            }

            return sorted;
        }

        private bool SelectedCharacterHasBlendShape(string shapeName)
        {
            Animator anim = m_characters[m_currentCharacter];
            return CharacterHasBlendShape(anim, shapeName);
        }

        private static bool CharacterHasBlendShape(Animator anim, string shapeName)
        {
            foreach (var smr in anim.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (smr == null)
                    continue;

                Mesh mesh = smr.sharedMesh;
                if (mesh == null)
                    continue;

                int count = mesh.blendShapeCount;
                for (int i = 0; i < count; i++)
                {
                    if (string.Equals(mesh.GetBlendShapeName(i), shapeName, StringComparison.Ordinal))
                        return true;
                }
            }

            return false;
        }

        private void ApplyBlendShapeToAllCharacters(string shapeName, float value)
        {
            foreach (var anim in m_characters)
            {
                if (anim == null)
                    continue;

                foreach (var smr in anim.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    if (smr == null)
                        continue;

                    Mesh mesh = smr.sharedMesh;
                    if (mesh == null)
                        continue;

                    int count = mesh.blendShapeCount;
                    for (int i = 0; i < count; i++)
                    {
                        if (!string.Equals(mesh.GetBlendShapeName(i), shapeName, StringComparison.Ordinal))
                            continue;

                        smr.SetBlendShapeWeight(i, value);
                    }
                }
            }
        }

        void LeftJustifySetup()
        {
            // taken from DebugMenu, specialty case button, left justified
            if (m_guiButtonLeftJustify == null)
            {
                m_guiButtonLeftJustify = new GUIStyle(GUI.skin.button);
                m_guiButtonLeftJustify.alignment = TextAnchor.MiddleLeft;
            }
            int fontSize = (int)(22.0f * ((float)Screen.height / (float)1080));
            m_guiButtonLeftJustify.fontSize = fontSize;
        }

        private static RuntimeAnimatorController GetBaseController(RuntimeAnimatorController controller)
        {
            if (controller == null)
                return null;

            var overrideController = controller as AnimatorOverrideController;
            return overrideController != null ? overrideController.runtimeAnimatorController : controller;
        }
    }
}
