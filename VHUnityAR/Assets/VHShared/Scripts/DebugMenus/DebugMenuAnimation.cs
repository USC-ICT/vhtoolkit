using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for controlling character animations.
    /// Provides options to change postures, standing animations, and sitting animations.
    /// </summary>
    public class DebugMenuAnimation : RideMonoBehaviour
    {
        private DebugMenu m_debugMenu;
        private DemoController m_controller;
        private DebugMenus m_debugMenusBase;

        private MecanimCharacter m_currentCharacterCached;

        private string m_ccMaleControllerName = "CCMaleAnimatorController";
        private string m_ccFemaleControllerName = "CCFemaleAnimatorController";
        private string m_ccJohnControllerName = "CCJohnAnimatorController";
        private string m_ictMaleControllerName = "IctMaleAnimatorController";
        private string m_ictFemaleControllerName = "IctFemaleAnimatorController";
        private string m_rocketboxMaleControllerName = "RocketboxMaleAnimatorController";
        private string m_rocketboxFemaleControllerName = "RocketboxFemaleAnimatorController";

        [Header("Idle hubs (hard-coded tokens; gestures discovered at runtime)")]
        private string[] m_ccMaleIdleTokens = new[]
        {
            // Hub tokens; idle state names inferred as Token unless overridden below.
            // These match your library naming; adjust if you rename hubs.
            "CC_ART_M_IdleStandingUpright01",
            "CC_ART_M_Alt_IdleStandingUpright01",
            "CC_IdleStandingUpright01",
            "IdleStandingUpright01",
            "IdleStandingLeanRt01",
            "IdleStandingLeanRtHandsOnHips01",
            "IdleSeatedBack01",
            "IdleSeatedBack02",
            "IdleSeatedForward01",
            "IdleSeatedUpright02",
            "PSA_IdleStandingUpright01",
            "MCU_af_StandConvB",
            "MCU_am_StandConvA",
            "CC_Fml_IdleStandingUpright01",
            "CC_Fml_IdleStandingLeanRt01",
            "CC_Fml_IdleSeatedUpright01",
            "CC_Fml_IdleSeatedForward01",
            "CC_Fml_IdleSeatedBack01",
        };

        [Tooltip("Optional explicit idle state names for hubs that do NOT use Token_Idle naming.")]
        private Dictionary<string,string> m_ccMaleIdleOverrides = new()
        {
            // Format: Token, IdleStateName
            // e.g. "Standing01, Standing01_Idle" (_Idle suffix in your project)
            { "Standing01", "Standing01_Idle" },
            { "MCU_af_StandConvB", "MCU_af_StandConvB_Idle_01" },
            { "MCU_am_StandConvA", "MCU_am_StandConvA_Idle_01" },
        };

        private string[] m_ccFemaleIdleTokens = new[]
        {
            "CC_ART_F_IdleStandingUpright01",
            "CC_Fml_IdleStandingUpright01",
            "CC_Fml_IdleStandingLeanRt01",
            "CC_Fml_IdleSeatedUpright01",
            "CC_Fml_IdleSeatedForward01",
            "CC_Fml_IdleSeatedBack01",
            "IdleStandingUpright01",
            "IdleStandingLeanRt01",
            "IdleStandingLeanRtHandsInBack01",
            "IdleStandingLeanRtHandsInFront01",
            "IdleStandingLeanRtHandsOnHips01",
            "IdleSeatedBack01",
            "IdleSeatedForward01",
            "IdleSeatedUpright01",
            "MCU_af_StandConvB",
            "MCU_am_StandConvA",
            "CC_IdleStandingUpright01",
        };

        private Dictionary<string, string> m_ccFemaleIdleOverrides = new()
        {
            { "MCU_af_StandConvB", "MCU_af_StandConvB_Idle_01" },
            { "MCU_am_StandConvA", "MCU_am_StandConvA_Idle_01" },
        };

        [Header("Idle hubs (hard-coded tokens; gestures discovered at runtime)")]
        private string[] m_ictMaleIdleTokens = new[]
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
        private Dictionary<string, string> m_ictMaleIdleOverrides = new()
        {
            // Format: Token=IdleStateName
            // e.g. "Standing01=Standing01_Idle" (_Idle suffix in your project)
            { "Standing01", "Standing01_Idle" },
        };

        private string[] m_ictFemaleIdleTokens = new[]
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

        private Dictionary<string, string> m_ictFemaleIdleOverrides = new();


        // Controller -> animators (all found in scene)
        private readonly Dictionary<string, List<Animator>> m_controllerNameToAnimators = new();
        // Controller -> sorted unique state names on layer 0 (union of clips)
        private readonly Dictionary<string, List<string>> m_controllerNameToStates = new();

        private readonly Dictionary<string, bool> m_expanded = new();
        private Vector2 m_scroll;


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
        /// Handles the GUI layout for animation settings in the Debug Menu.
        /// Provides options to change postures, standing animations, and sitting animations.
        /// </summary>
        public void OnGUIAnimation()
        {
            m_debugMenusBase.OnGUICustomStylesSetup();

            if (m_controller.CurrentCharacter != m_currentCharacterCached)
                RebuildControllerMaps();

            m_debugMenusBase.OnGUICharacterConfig();

            if (m_controller.CurrentCharacter != null)
            {
                var animator = m_controller.CurrentCharacter.GetComponent<Animator>();
                if (animator != null &&
                    animator.runtimeAnimatorController != null)
                {
                    var baseCtrl = GetBaseController(animator.runtimeAnimatorController);
                    var name = baseCtrl != null ? baseCtrl.name : animator.runtimeAnimatorController.name;
                    switch (name)
                    {
                        case "IctMaleAnimatorController": RenderAnimationPanel(idleTokens: m_ictMaleIdleTokens, idleOverride: m_ictMaleIdleOverrides, expanded: m_expanded, requiredControllerName: m_ictMaleControllerName, ref m_scroll); break;
                        case "IctFemaleAnimatorController": RenderAnimationPanel(idleTokens: m_ictFemaleIdleTokens, idleOverride: m_ictFemaleIdleOverrides, expanded: m_expanded, requiredControllerName: m_ictFemaleControllerName, ref m_scroll); break;
                        case "CCMaleAnimatorController": RenderAnimationPanel(idleTokens: m_ccMaleIdleTokens, idleOverride: m_ccMaleIdleOverrides, expanded: m_expanded, requiredControllerName: m_ccMaleControllerName, ref m_scroll); break;
                        case "CCFemaleAnimatorController": RenderAnimationPanel(idleTokens: m_ccFemaleIdleTokens, idleOverride: m_ccFemaleIdleOverrides, expanded: m_expanded, requiredControllerName: m_ccFemaleControllerName, ref m_scroll); break;
                        case "CCJohnAnimatorController": RenderAnimationPanel(idleTokens: m_ccMaleIdleTokens, idleOverride: m_ccMaleIdleOverrides, expanded: m_expanded, requiredControllerName: m_ccJohnControllerName, ref m_scroll); break;
                        case "RocketboxMaleAnimatorController": RenderAnimationPanel(idleTokens: m_ictMaleIdleTokens, idleOverride: m_ictMaleIdleOverrides, expanded: m_expanded, requiredControllerName: m_rocketboxMaleControllerName, ref m_scroll); break;
                        case "RocketboxFemaleAnimatorController": RenderAnimationPanel(idleTokens: m_ictFemaleIdleTokens, idleOverride: m_ictFemaleIdleOverrides, expanded: m_expanded, requiredControllerName: m_rocketboxFemaleControllerName, ref m_scroll); break;
                        default: break;
                    }
                }
            }
        }

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

            var animator = m_controller.CurrentCharacter != null ? m_controller.CurrentCharacter.GetComponent<Animator>() : null;
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
                                m_controller.CurrentCharacter.PlayPosture(idleState); //PlayOnAnimators(animators, idleState, 0.5f);
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
                                    if (GUILayout.Button(g, m_debugMenusBase.m_guiButtonLeftJustify))
                                        m_controller.CurrentCharacter.PlayAnim(g); //PlayOnAnimators(animators, g, 0.1f);
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

        private void RebuildControllerMaps()
        {
            m_currentCharacterCached = m_controller.CurrentCharacter;

            m_controllerNameToAnimators.Clear();
            m_controllerNameToStates.Clear();

            //foreach (var a in m_characters)
            var a = m_controller.CurrentCharacter.GetComponent<Animator>();
            {
                if (a == null)
                    return;

                var ctrl = a.runtimeAnimatorController;
                if (ctrl == null)
                    return;

                var baseCtrl = GetBaseController(ctrl);
                if (baseCtrl == null)
                    return;

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

        private static RuntimeAnimatorController GetBaseController(RuntimeAnimatorController controller)
        {
            if (controller == null)
                return null;

            var overrideController = controller as AnimatorOverrideController;
            return overrideController != null ? overrideController.runtimeAnimatorController : controller;
        }
    }
}
