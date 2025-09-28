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
        [Header("Controller names (optional)")]
        private string m_ictMaleControllerName = "IctMaleAnimatorController";
        private string m_ictFemaleControllerName = "IctFemaleAnimatorController";
        private string m_rocketboxMaleControllerName = "RocketboxMaleAnimatorController";
        private string m_rocketboxFemaleControllerName = "RocketboxFemaleAnimatorController";
        private string m_kevinControllerName = "ChrKevin_AnimatorController";
        private string m_mariaControllerName = "ChrMaria_AnimatorController";

        [Header("Idle hubs (hard-coded tokens; gestures discovered at runtime)")]
        private string[] m_maleIdleTokens = new[]
        {
            // Hub tokens; idle state names inferred as Token unless overridden below.
            // These match your library naming; adjust if you rename hubs.
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
        private string[] m_maleIdleOverrides = new[]
        {
            // Format: Token=IdleStateName
            // e.g. "Standing01=Standing01_Idle" (_Idle suffix in your project)
            "Standing01=Standing01_Idle",
        };

        private string[] m_femaleIdleTokens = new[]
        {
            "IdleStandingUpright01",
            "IdleStandingLeanRt01",
            "IdleStandingLeanRtHandsInBack01",
            "IdleStandingLeanRtHandsInFront01",
            "IdleStandingLeanRtHandsOnHips01",
            "IdleSeatedBack01",
            "IdleSeatedForward01",
            "IdleSeatedUpright01",
        };

        private string[] m_femaleIdleOverrides = Array.Empty<string>();
        #endregion

        #region State
        private DebugMenu m_debugMenu;

        // Controller -> animators (all found in scene)
        private readonly Dictionary<RuntimeAnimatorController, List<Animator>> m_controllerToAnimators = new();
        // Controller -> sorted unique state names on layer 0 (union of clips)
        private readonly Dictionary<RuntimeAnimatorController, List<string>> m_controllerToStates = new();

        // Per-tab UI state
        private readonly Dictionary<string, bool> m_kevinExpanded = new();
        private readonly Dictionary<string, bool> m_mariaExpanded = new();
        private readonly Dictionary<string, bool> m_maleExpanded = new();
        private readonly Dictionary<string, bool> m_femaleExpanded = new();
        private readonly Dictionary<string, bool> m_rocketboxMaleExpanded = new();
        private readonly Dictionary<string, bool> m_rocketboxFemaleExpanded = new();
        private Vector2 m_kevinScroll;
        private Vector2 m_mariaScroll;
        private Vector2 m_maleScroll;
        private Vector2 m_femaleScroll;
        private Vector2 m_rocketboxMaleScroll;
        private Vector2 m_rocketboxFemaleScroll;

        // Idle override map (Token -> IdleState)
        private readonly Dictionary<string, string> m_maleIdleOverrideMap = new(StringComparer.Ordinal);
        private readonly Dictionary<string, string> m_femaleIdleOverrideMap = new(StringComparer.Ordinal);
        #endregion

        GUIStyle m_guiButtonLeftJustify;


        #region Unity
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Systems.Get<DebugMenu>();

            // Precompute override maps
            ParseOverrideList(m_maleIdleOverrides, m_maleIdleOverrideMap);
            ParseOverrideList(m_femaleIdleOverrides, m_femaleIdleOverrideMap);

            // Build initial maps of controllers/animators/states
            RebuildControllerMaps();

            // Insert two menus (Male / Female). Keep ordering stable.
            m_debugMenu.InsertMenu(0, "Camera", OnGUICamera);
            m_debugMenu.InsertMenu(1, "Animations: ICT Kevin", OnGUIKevin);
            m_debugMenu.InsertMenu(2, "Animations: ICT Maria", OnGUIMaria);
            m_debugMenu.InsertMenu(3, "Animations: ICT Male", OnGUIMale);
            m_debugMenu.InsertMenu(4, "Animations: ICT Female", OnGUIFemale);
            m_debugMenu.InsertMenu(5, "Animations: Rocketbox Male", OnGUIRocketboxMale);
            m_debugMenu.InsertMenu(6, "Animations: Rocketbox Female", OnGUIRocketboxFemale);

            // Show and size to right pane by default
            m_debugMenu.SetMenu(0);
            m_debugMenu.ShowMenu(true);
            m_debugMenu.SetMenuSize(0, 0, 0.4f, 1f);
            m_debugMenu.SetWideMenuSize(0, 0, 0.5f, 1f);
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
        }
        #endregion

        #region Build / Query
        private void RebuildControllerMaps()
        {
            m_controllerToAnimators.Clear();
            m_controllerToStates.Clear();

            var animators = FindObjectsByType<Animator>(FindObjectsSortMode.None);
            foreach (var a in animators)
            {
                if (a == null)
                    continue;

                var ctrl = a.runtimeAnimatorController;
                if (ctrl == null)
                    continue;

                if (!m_controllerToAnimators.TryGetValue(ctrl, out var list))
                {
                    list = new List<Animator>();
                    m_controllerToAnimators[ctrl] = list;
                }

                if (!list.Contains(a))
                    list.Add(a);

                // States (names) per controller
                if (!m_controllerToStates.ContainsKey(ctrl))
                {
                    var names = new HashSet<string>(StringComparer.Ordinal);
                    foreach (var clip in ctrl.animationClips)
                    {
                        if (clip == null)
                            continue;

                        names.Add(clip.name);
                    }

                    var sorted = names.ToList();
                    sorted.Sort(StringComparer.Ordinal);
                    m_controllerToStates[ctrl] = sorted;
                }
            }
        }

        private static void ParseOverrideList(string[] entries, Dictionary<string, string> map)
        {
            map.Clear();
            if (entries == null)
                return;

            foreach (var e in entries)
            {
                if (string.IsNullOrWhiteSpace(e))
                    continue;

                var ix = e.IndexOf('=');
                if (ix <= 0 || ix >= e.Length - 1)
                    continue;

                var token = e.Substring(0, ix).Trim();
                var idle = e.Substring(ix + 1).Trim();
                if (token.Length == 0 || idle.Length == 0)
                    continue;

                map[token] = idle;
            }
        }

        private string InferControllerNameForRoots()
        {
            var counts = new Dictionary<string, int>(StringComparer.Ordinal);
            foreach (var kvp in m_controllerToAnimators)
            {
                var ctrl = kvp.Key;
                if (ctrl == null)
                    continue;

                var name = ctrl.name ?? "";
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

        public void CameraMale() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(34, 1.6f, 2), Quaternion.Euler(8, 180, 0));
        public void CameraFemale() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(13, 1.6f, 2), Quaternion.Euler(8, 180, 0));
        public void CameraMaleHead() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(34, 1.6f, 0.5f), Quaternion.Euler(8, 180, 0));
        public void CameraMaleHands() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(33.3f, 0.9f, 0), Quaternion.Euler(8, 90, 0));
        public void CameraFemaleHead() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(13, 1.6f, 0.5f), Quaternion.Euler(8, 180, 0));
        public void CameraFemaleHands() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(12.3f, 0.9f, 0), Quaternion.Euler(8, 90, 0));
        public void CameraReset() => FindAnyObjectByType<Camera>().transform.SetPositionAndRotation(new Vector3(34, 1.6f, 2), Quaternion.Euler(8, 180, 0));

        private void OnGUICamera()
        {
            m_debugMenu.Label("Keyboard keys mapped to each button");

            if (m_debugMenu.Button("1) Male")) CameraMale();
            if (m_debugMenu.Button("2) Female")) CameraFemale();
            if (m_debugMenu.Button("3) Male Head")) CameraMaleHead();
            if (m_debugMenu.Button("4) Male Hands")) CameraMaleHands();
            if (m_debugMenu.Button("5) Female Head")) CameraFemaleHead();
            if (m_debugMenu.Button("6) Female Hands")) CameraFemaleHands();
            if (m_debugMenu.Button("7) Reset")) CameraReset();
        }

        private void OnGUIKevin()
        {
            LeftJustifySetup();

            RenderAnimationPanel(
                idleTokens: m_maleIdleTokens,
                idleOverride: m_maleIdleOverrideMap,
                expanded: m_kevinExpanded,
                requiredControllerName: m_kevinControllerName,
                ref m_kevinScroll);
        }

        private void OnGUIMaria()
        {
            LeftJustifySetup();

            RenderAnimationPanel(
                idleTokens: m_maleIdleTokens,
                idleOverride: m_maleIdleOverrideMap,
                expanded: m_mariaExpanded,
                requiredControllerName: m_mariaControllerName,
                ref m_mariaScroll);
        }

        private void OnGUIMale()
        {
            LeftJustifySetup();

            RenderAnimationPanel(
                idleTokens: m_maleIdleTokens,
                idleOverride: m_maleIdleOverrideMap,
                expanded: m_maleExpanded,
                requiredControllerName: m_ictMaleControllerName,
                ref m_maleScroll);
        }

        private void OnGUIFemale()
        {
            LeftJustifySetup();

            RenderAnimationPanel(
                idleTokens: m_femaleIdleTokens,
                idleOverride: m_femaleIdleOverrideMap,
                expanded: m_femaleExpanded,
                requiredControllerName: m_ictFemaleControllerName,
                ref m_femaleScroll);
        }

        private void OnGUIRocketboxMale()
        {
            LeftJustifySetup();

            RenderAnimationPanel(
                idleTokens: m_maleIdleTokens,
                idleOverride: m_maleIdleOverrideMap,
                expanded: m_rocketboxMaleExpanded,
                requiredControllerName: m_rocketboxMaleControllerName,
                ref m_rocketboxMaleScroll);
        }

        private void OnGUIRocketboxFemale()
        {
            LeftJustifySetup();

            RenderAnimationPanel(
                idleTokens: m_femaleIdleTokens,
                idleOverride: m_femaleIdleOverrideMap,
                expanded: m_rocketboxFemaleExpanded,
                requiredControllerName: m_rocketboxFemaleControllerName,
                ref m_rocketboxFemaleScroll);
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

            using (var scrollViewScope = new GUILayout.ScrollViewScope(scroll))
            {
                scroll = scrollViewScope.scrollPosition;

                // Only iterate controllers that match the required name
                foreach (var kvp in m_controllerToAnimators)
                {
                    var controller = kvp.Key;
                    if (controller == null)
                        continue;

                    if (!string.Equals(controller.name, controllerName, StringComparison.Ordinal))
                        continue;

                    var animators = kvp.Value;
                    if (!m_controllerToStates.TryGetValue(controller, out var stateNames))
                        continue;

                    foreach (var token in idleTokens)
                    {
                        var idleState = ResolveIdleStateName(token, idleOverride);
                        var hasIdle = stateNames.Contains(idleState);

                        var gestures = stateNames
                            .Where(n => n.StartsWith(token + "_", StringComparison.Ordinal))
                            .Where(n => n != idleState)
                            .ToList();

                        var key = controller.name + "::" + token;
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
    }
}
