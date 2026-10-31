using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

namespace Ride.VH
{
    /// <summary>
    /// Provides a guided Character Creator (CC) art-preparation checklist shared by URP and HDRP projects.
    /// </summary>
    public class CharacterAssetSetupWindow : EditorWindow
    {
        private const string ComponentSetupMenuItem = "Ride/CC Character Setup";
        private const string ComponentSetupWindowTypeName = "Ride.VH.CCCharacterSetupWindow";

        private enum MaterialSurfaceType
        {
            Opaque = 0,
            Transparent = 1,
        }

        private enum SoundNodeGender
        {
            Male = 0,
            Female = 1,
        }

        [Serializable]
        private class CharacterInfo
        {
            public string Guid;
            public string Name;
            public string FolderPath;
            public string AssetPath;
            public string PrefabPath;
            public string VhPrefabPath;
            public bool HasPrefab;
            public bool HasVhPrefab;
        }

        private static readonly string[] UnwantedObjectTerms =
        {
            "boxer",
            "brief",
            "underwear",
            "underpants",
        };

        private static readonly string[] TransparencyPropertyNames =
        {
            "_Surface",
            "_SurfaceType",
        };

        private static readonly string[] PreserveSpecularPropertyNames =
        {
            "_EnableBlendModePreserveSpecularLighting",
            "_BlendModePreserveSpecular",
        };

        private static readonly string[] SmoothnessPropertyNames =
        {
            "_Smoothness",
            "_SmoothnessMin",
            "_SmoothnessMax",
            "_SmoothnessContrast",
        };

        private static readonly string[] NonClothingRendererTerms =
        {
            "cc_base",
            "body",
            "skin",
            "head",
            "eye",
            "cornea",
            "tear",
            "teeth",
            "tongue",
            "nail",
            "hair",
            "brow",
            "lash",
            "beard",
            "mustache",
            "stubble",
            "scalp",
            "boxer",
            "underwear",
        };

        [SerializeField] private GameObject m_target;
        [SerializeField] private List<CharacterInfo> m_characters = new();
        [SerializeField] private Vector2 m_characterListScrollPosition;
        [SerializeField] private string m_selectedCharacterGuid = string.Empty;
        [SerializeField] private Vector2 m_scrollPosition;
        [SerializeField] private bool[] m_stepExpanded = new bool[15];
        [SerializeField] private bool m_showAdditionalAnalysis;
        [SerializeField] private bool m_showAdditionalImporterSettings;
        [SerializeField] private string m_importerSelectionStatus = string.Empty;
        [SerializeField] private bool m_showObjects = true;
        [SerializeField] private bool m_showMaterials = true;
        [SerializeField] private bool m_showAnchors = true;
        [SerializeField] private bool m_showValidation = true;
        [SerializeField] private SoundNodeGender m_soundNodeGender = SoundNodeGender.Male;

        [SerializeField] private bool m_hasAnalysis;
        [SerializeField] private string m_analyzedTargetName = string.Empty;
        [SerializeField] private string m_assetPath = string.Empty;
        [SerializeField] private string m_pipelineName = string.Empty;
        [SerializeField] private string m_pipelineVersion = string.Empty;
        [SerializeField] private string m_characterType = string.Empty;
        [SerializeField] private int m_objectCount;
        [SerializeField] private int m_rendererCount;
        [SerializeField] private int m_skinnedMeshRendererCount;
        [SerializeField] private int m_materialCount;
        [SerializeField] private string m_headPath = string.Empty;
        [SerializeField] private string m_soundNodePath = string.Empty;
        [SerializeField] private List<string> m_unwantedObjectCandidates = new();
        [SerializeField] private List<string> m_transparencyMaterialCandidates = new();
        [SerializeField] private List<string> m_preserveSpecularMaterialCandidates = new();
        [SerializeField] private List<string> m_smoothnessMaterialCandidates = new();
        [SerializeField] private List<string> m_validationMessages = new();

        /// <summary>
        /// Opens the character asset setup window.
        /// </summary>
        [MenuItem("Ride/VH/CC Character Asset Setup")]
        public static void ShowWindow()
        {
            var window = GetWindow<CharacterAssetSetupWindow>("VH CC Character Setup");
            window.minSize = new Vector2(620f, 480f);
            window.RefreshCharacterList();
            window.TryUseCurrentSelection();
        }

        private void OnEnable()
        {
            m_characters ??= new List<CharacterInfo>();
            EnsureChecklistState();
            RefreshCharacterList();
        }

        private void OnSelectionChange()
        {
            Repaint();
        }

        private void OnHierarchyChange()
        {
            Repaint();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("VH CC Character Asset Setup", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "A guided checklist for preparing Character Creator (CC) characters. Actions are explicit; nothing runs automatically.",
                MessageType.Info);
            using (var scroll = new EditorGUILayout.ScrollViewScope(m_scrollPosition))
            {
                m_scrollPosition = scroll.scrollPosition;
                DrawChecklistToolbar();
                DrawChecklistStep(0, GetCharacterSelectionStepTitle(), DrawChooseCharacterStep);
                DrawChecklistStep(1, "Open the CC/iC Importer", DrawOpenImporterStep);
                DrawChecklistStep(2, "Review CC/iC Build Options", DrawExpressionFeaturesStep);
                DrawChecklistStep(3, "Build Character", DrawBuildCharacterStep);
                DrawChecklistStep(4, "Wait for the Generated Prefab", DrawGeneratedPrefabStep);
                DrawChecklistStep(5, "Create the _VH Prefab Variant", DrawCreateVariantStep);
                DrawChecklistStep(6, "Review Boxer Objects", DrawBoxersStep);
                DrawChecklistStep(7, "Review Hair and Brow Transparency", DrawTransparencyStep);
                DrawChecklistStep(8, "Review Brow and Scalp Specular Lighting", DrawSpecularStep);
                DrawChecklistStep(9, "Review Scalp Smoothness", DrawScalpSmoothnessStep);
                DrawChecklistStep(10, "Review Clothing Smoothness", DrawClothingSmoothnessStep);
                DrawChecklistStep(11, "Review Tear Duct Smoothness", DrawTearDuctStep);
                DrawChecklistStep(12, "Review Eyelash Smoothness", DrawEyelashStep);
                DrawChecklistStep(13, "Add SoundNode", DrawSoundNodeStep);
                DrawChecklistStep(14, "Continue Component-Level Setup", DrawComponentSetupStep);
                DrawAdditionalAnalysis();
            }
        }

        private void EnsureChecklistState()
        {
            if (m_stepExpanded == null || m_stepExpanded.Length != 15)
            {
                m_stepExpanded = new bool[15];
                m_stepExpanded[0] = true;
            }

        }

        private void DrawChecklistToolbar()
        {
            EnsureChecklistState();
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField($"Pipeline: {GetRenderPipelineName()} {GetRenderPipelineVersion()}", EditorStyles.miniBoldLabel);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Expand All", GUILayout.Width(90f)))
                    SetAllStepsExpanded(true);
                if (GUILayout.Button("Collapse All", GUILayout.Width(90f)))
                    SetAllStepsExpanded(false);
            }

            CharacterInfo info = GetSelectedCharacter();
            using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField(
                    info != null ? $"Selected Character: {info.Name}" : "Selected Character: None",
                    EditorStyles.boldLabel);

                if (info != null)
                {
                    EditorGUILayout.LabelField(GetCharacterAssetState(info), EditorStyles.miniLabel, GUILayout.Width(72f));
                    if (GUILayout.Button("Focus", GUILayout.Width(60f)))
                        FocusAsset(GetPreferredSelectionPath(info));
                }
            }

            EditorGUILayout.Space();
        }

        private string GetCharacterSelectionStepTitle()
        {
            CharacterInfo info = GetSelectedCharacter();
            return info == null ? "Choose a Character" : $"Choose a Character — {info.Name}";
        }

        private void SetAllStepsExpanded(bool expanded)
        {
            for (int i = 0; i < m_stepExpanded.Length; i++)
                m_stepExpanded[i] = expanded;
        }

        private void DrawChecklistStep(int index, string title, Action drawContents)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    m_stepExpanded[index] = EditorGUILayout.Foldout(
                        m_stepExpanded[index],
                        $"{index + 1}. {title}",
                        true,
                        EditorStyles.foldout);
                }

                if (m_stepExpanded[index])
                {
                    EditorGUI.indentLevel++;
                    drawContents();
                    EditorGUI.indentLevel--;
                }
            }
        }

        private void DrawChooseCharacterStep()
        {
            DrawTargetSection();
            CharacterInfo info = GetSelectedCharacter();
            if (info != null)
                EditorGUILayout.LabelField("CC JSON", Path.ChangeExtension(info.AssetPath, ".json"), EditorStyles.wordWrappedMiniLabel);
        }

        private void DrawOpenImporterStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            EditorGUILayout.HelpBox(
                "Opens Reallusion's CC/iC Importer and selects this checklist's character.",
                MessageType.None);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                DrawExperimentalCcIntegrationHeader();
                using (new EditorGUI.DisabledScope(info == null))
                {
                    if (GUILayout.Button("Open CC/iC Importer for Selected Character", GUILayout.Height(26f)))
                        OpenImporter(info);
                }

                if (!string.IsNullOrEmpty(m_importerSelectionStatus))
                    EditorGUILayout.HelpBox(m_importerSelectionStatus, MessageType.Info);
            }
        }

        private void DrawExpressionFeaturesStep()
        {
            EditorGUILayout.HelpBox(
                "Confirm that Expression Driven Bones, Expression Transpose, and Use Constraints are unchecked before building.",
                MessageType.Info);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                DrawExperimentalCcIntegrationHeader();

                CharacterInfo info = GetSelectedCharacter();
                if (!TryGetMatchingImporterCharacter(info, out object importerWindow, out object importerCharacter, out string reason))
                {
                    EditorGUILayout.HelpBox(reason, MessageType.Warning);
                    return;
                }

                EditorGUILayout.LabelField("Live CC/iC Importer settings", EditorStyles.miniBoldLabel);
                DrawImporterFeatureToggle(importerWindow, importerCharacter, "Expression Driven Bones", "BoneDriver");
                DrawImporterFeatureToggle(importerWindow, importerCharacter, "Expression Transpose", "ExpressionTranspose");
                DrawImporterFeatureToggle(importerWindow, importerCharacter, "Use Constraints", "ConstraintData");

                EditorGUILayout.Space();
                m_showAdditionalImporterSettings = EditorGUILayout.Foldout(
                    m_showAdditionalImporterSettings,
                    "Additional Recommended Settings (optional)",
                    true);
                if (m_showAdditionalImporterSettings)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.HelpBox(
                        "These options may be enabled by features exported from Character Creator. We recommend leaving them unchecked before building.",
                        MessageType.None);
                    DrawImporterFeatureToggle(
                        importerWindow,
                        importerCharacter,
                        "Material Shader Features > Displacement",
                        "Displacement");
                    DrawImporterFeatureToggle(
                        importerWindow,
                        importerCharacter,
                        "Character Physics > Enable Cloth Physics",
                        "ClothPhysics");
                    DrawImporterFeatureToggle(
                        importerWindow,
                        importerCharacter,
                        "Character Physics > Enable Hair Physics",
                        "HairPhysics");
                    EditorGUI.indentLevel--;
                }
            }
        }

        private void DrawBuildCharacterStep()
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                DrawExperimentalCcIntegrationHeader();
                EditorGUILayout.HelpBox(
                    "This calls the same private BuildCharacter method used by the installed CC/iC Importer's Build Character button.",
                    MessageType.None);

                CharacterInfo info = GetSelectedCharacter();
                if (info != null && (info.HasPrefab || info.HasVhPrefab))
                    EditorGUILayout.HelpBox("Build Character regenerates CC/iC assets and may overwrite manual changes to the generated prefab, materials, and model import settings. If this character was previously customized, review or back up its SVN changes before continuing.", MessageType.Warning);

                bool canBuild = TryGetMatchingImporterCharacter(info, out object importerWindow, out _, out string reason);
                if (canBuild && importerWindow.GetType().GetMethod("BuildCharacter", BindingFlags.Instance | BindingFlags.NonPublic) == null)
                {
                    canBuild = false;
                    reason = "This CC/iC Importer version does not expose the expected BuildCharacter method.";
                }

                if (!canBuild)
                    EditorGUILayout.HelpBox(reason, MessageType.Warning);

                using (new EditorGUI.DisabledScope(!canBuild || EditorApplication.isPlaying))
                {
                    if (GUILayout.Button("Build Character Using CC/iC Importer", GUILayout.Height(30f)))
                        InvokeImporterBuild(importerWindow);
                }
            }
        }

        private static void DrawExperimentalCcIntegrationHeader()
        {
            EditorGUILayout.LabelField("Experimental CC/iC Integration", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(
                "Uses reflection against CC/iC Importer internals and may require updates when the importer changes.",
                EditorStyles.wordWrappedMiniLabel);
            EditorGUILayout.Space(2f);
        }

        private void DrawGeneratedPrefabStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            if (info == null)
            {
                EditorGUILayout.HelpBox("Choose a character first.", MessageType.Warning);
                return;
            }

            EditorGUILayout.LabelField("Expected prefab", info.PrefabPath, EditorStyles.wordWrappedMiniLabel);
            EditorGUILayout.HelpBox(
                info.HasPrefab
                    ? "The generated character prefab is present."
                    : "Wait for the CC/iC build and Unity import to finish, then refresh this step.",
                info.HasPrefab ? MessageType.Info : MessageType.Warning);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Refresh"))
                    RefreshCharacterList();
                using (new EditorGUI.DisabledScope(!info.HasPrefab))
                {
                    if (GUILayout.Button("Focus Generated Prefab"))
                        FocusAsset(info.PrefabPath);
                }
            }
        }

        private void DrawCreateVariantStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            if (info == null)
            {
                EditorGUILayout.HelpBox("Choose a character first.", MessageType.Warning);
                return;
            }

            EditorGUILayout.LabelField("VH variant", info.VhPrefabPath, EditorStyles.wordWrappedMiniLabel);
            EditorGUILayout.HelpBox(
                "Creates a prefab variant only. No hierarchy, material, or component changes are made in this step.",
                MessageType.None);

            if (info.HasVhPrefab)
            {
                EditorGUILayout.HelpBox("The _VH prefab already exists and will not be overwritten.", MessageType.Info);
                if (GUILayout.Button("Focus Existing _VH Prefab"))
                    FocusAsset(info.VhPrefabPath);
            }
            else
            {
                using (new EditorGUI.DisabledScope(!info.HasPrefab))
                {
                    if (GUILayout.Button("Create _VH Prefab Variant", GUILayout.Height(28f)))
                        CreateVhPrefabVariant(info);
                }
            }
        }

        private void DrawBoxersStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            Func<Renderer, bool> predicate = renderer =>
                renderer.name.IndexOf("boxer", StringComparison.OrdinalIgnoreCase) >= 0;
            List<GameObject> matches = GetMatchingRendererObjects(info, predicate);
            int enabledCount = matches.Count(match => match.activeSelf);
            int disabledCount = matches.Count - enabledCount;

            EditorGUILayout.LabelField($"Matching objects ({matches.Count})", EditorStyles.miniBoldLabel);
            if (matches.Count == 0)
            {
                EditorGUILayout.LabelField(
                    info != null && info.HasVhPrefab
                        ? "No renderer object names contain 'Boxer'."
                        : "Create the _VH prefab first.",
                    EditorStyles.wordWrappedMiniLabel);
            }
            else
            {
                GameObject prefab = GetVhPrefabRootForReview(info);
                foreach (GameObject match in matches)
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(GetTransformPath(match.transform, prefab.transform));
                        EditorGUILayout.LabelField(
                            match.activeSelf ? "Enabled" : "Disabled",
                            EditorStyles.miniLabel,
                            GUILayout.Width(60f));
                    }
                }
            }

            string state = matches.Count == 0
                ? "No matches"
                : enabledCount == matches.Count
                    ? $"Enabled ({enabledCount})"
                    : disabledCount == matches.Count
                        ? $"Disabled ({disabledCount})"
                        : $"Partial — {enabledCount} enabled, {disabledCount} disabled";
            EditorGUILayout.HelpBox($"Current state: {state}", MessageType.None);
            DrawPrefabReviewButtons(info, matches.Count, predicate);

            using (new EditorGUI.DisabledScope(info == null || !info.HasVhPrefab || matches.Count == 0))
            {
                if (enabledCount > 0 && disabledCount > 0)
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        if (GUILayout.Button($"Enable All ({disabledCount} disabled)"))
                            SetMatchingObjectsActive(info, predicate, true);
                        if (GUILayout.Button($"Disable All ({enabledCount} enabled)"))
                            SetMatchingObjectsActive(info, predicate, false);
                    }
                }
                else if (enabledCount > 0)
                {
                    if (GUILayout.Button($"Disable All Matching Boxer Objects ({enabledCount})"))
                        SetMatchingObjectsActive(info, predicate, false);
                }
                else if (GUILayout.Button($"Enable All Matching Boxer Objects ({disabledCount})"))
                {
                    SetMatchingObjectsActive(info, predicate, true);
                }
            }
        }

        private void DrawTransparencyStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            Func<Material, bool> predicate = material =>
                ContainsAny(material.name, new[] { "hair", "brow", "beard", "mustache", "scalp" });
            DrawMaterialCandidates(info, predicate, DrawSurfaceTypeControl);
            DrawPrefabReviewButtons(info, GetMatchingMaterialRendererObjects(info, predicate).Count,
                renderer => RendererUsesMaterial(renderer, predicate));
            EditorGUILayout.HelpBox(
                "Recommendation: first-pass materials are Opaque; base transparency and second-pass materials are Transparent. Fixed-surface shader graphs are reported but cannot be changed per material.",
                MessageType.None);
        }

        private void DrawSpecularStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            Func<Material, bool> predicate = material => ContainsAny(material.name, new[] { "brow", "scalp" });
            DrawMaterialCandidates(info, predicate, DrawPreserveSpecularControl);
            DrawPrefabReviewButtons(info, GetMatchingMaterialRendererObjects(info, predicate).Count,
                renderer => RendererUsesMaterial(renderer, predicate));
            EditorGUILayout.HelpBox(
                "Recommendation: Preserve Specular Lighting should be disabled. The checkbox is only shown when the shader exposes it as a material property.",
                MessageType.None);
        }

        private void DrawScalpSmoothnessStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            Func<Material, bool> predicate = material =>
                material.name.IndexOf("scalp", StringComparison.OrdinalIgnoreCase) >= 0;
            DrawMaterialCandidates(info, predicate, material => DrawSmoothnessControls(material, 0f));
            DrawPrefabReviewButtons(info, GetMatchingMaterialRendererObjects(info, predicate).Count,
                renderer => RendererUsesMaterial(renderer, predicate));
            EditorGUILayout.HelpBox(
                "Recommended direct Smoothness: 0. Smoothness Remapping controls are also shown when supported by the shader.",
                MessageType.None);
        }

        private void DrawClothingSmoothnessStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            Func<Renderer, bool> rendererPredicate = IsLikelyClothingRenderer;
            List<GameObject> matchingObjects = GetMatchingRendererObjects(info, rendererPredicate);
            List<Material> materials = GetMaterials(matchingObjects)
                .Where(material => material.HasProperty("_Smoothness"))
                .ToList();

            EditorGUILayout.HelpBox(
                "Recommended smoothness: 0.1. Candidates exclude standard CC body, face, hair, eye, and underwear renderer names; review each material before changing it.",
                MessageType.None);
            DrawMaterialList(info, materials, material => DrawSmoothnessControls(material, 0.1f));
            DrawPrefabReviewButtons(info, matchingObjects.Count, rendererPredicate);
        }

        private void DrawTearDuctStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            Func<Material, bool> predicate = material =>
                ContainsAny(material.name, new[] { "tear_duct", "tear duct", "tearduct" });
            DrawMaterialCandidates(info, predicate, material => DrawSmoothnessControls(material, null));
            DrawPrefabReviewButtons(info, GetMatchingMaterialRendererObjects(info, predicate).Count,
                renderer => RendererUsesMaterial(renderer, predicate));
            EditorGUILayout.HelpBox(
                "Adjust Smoothness Min/Max directly. Only properties exposed by the current URP or HDRP shader are shown.",
                MessageType.None);
        }

        private void DrawEyelashStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            Func<Material, bool> predicate = material => ContainsAny(material.name, new[] { "lash", "eyelash" });
            DrawMaterialCandidates(info, predicate, material => DrawSmoothnessControls(material, null));
            DrawPrefabReviewButtons(info, GetMatchingMaterialRendererObjects(info, predicate).Count,
                renderer => RendererUsesMaterial(renderer, predicate));
            EditorGUILayout.HelpBox(
                "Adjust the supported direct or remapped smoothness values for each lash pass independently.",
                MessageType.None);
        }

        private void DrawSoundNodeStep()
        {
            CharacterInfo info = GetSelectedCharacter();
            if (info == null || !info.HasVhPrefab)
            {
                EditorGUILayout.HelpBox("Create the _VH prefab variant first.", MessageType.Warning);
                return;
            }

            GameObject prefab = GetVhPrefabRootForReview(info);
            Transform existing = prefab != null
                ? prefab.GetComponentsInChildren<Transform>(true).FirstOrDefault(transform =>
                    string.Equals(transform.name, "SoundNode", StringComparison.OrdinalIgnoreCase))
                : null;

            m_soundNodeGender = (SoundNodeGender)EditorGUILayout.EnumPopup("Character", m_soundNodeGender);
            Vector3 soundNodePosition = GetSoundNodePosition();
            EditorGUILayout.LabelField(
                $"SoundNode local position: {soundNodePosition.ToString("F3")}",
                EditorStyles.miniLabel);

            EditorGUILayout.HelpBox(
                existing != null
                    ? $"SoundNode exists at {GetTransformPath(existing, prefab.transform)}."
                    : $"Adds SoundNode as a direct child of the prefab root at local position {soundNodePosition.ToString("F3")}, with an AudioSource.",
                existing != null ? MessageType.Info : MessageType.None);

            using (new EditorGUILayout.HorizontalScope())
            {
                DrawOpenPrefabButton(info);
                using (new EditorGUI.DisabledScope(existing == null))
                {
                    if (GUILayout.Button("Highlight SoundNode"))
                        HighlightSoundNode(info);
                }
            }

            using (new EditorGUI.DisabledScope(existing != null))
            {
                if (GUILayout.Button("Add SoundNode to _VH Prefab"))
                    AddSoundNode(info);
            }
        }

        private static void DrawComponentSetupStep()
        {
            bool windowAvailable = TypeCache.GetTypesDerivedFrom<EditorWindow>().Any(type =>
                string.Equals(type.FullName, ComponentSetupWindowTypeName, StringComparison.Ordinal));

            EditorGUILayout.HelpBox(
                "Please continue configuring the character in the component-level CC Character Setup window.",
                MessageType.Info);

            using (new EditorGUI.DisabledScope(!windowAvailable))
            {
                if (GUILayout.Button("Open CC Character Setup"))
                    EditorApplication.ExecuteMenuItem(ComponentSetupMenuItem);
            }

            if (!windowAvailable)
            {
                EditorGUILayout.HelpBox(
                    "The CC Character Setup window is not available in this project.",
                    MessageType.None);
            }
        }

        private void DrawAdditionalAnalysis()
        {
            EditorGUILayout.Space();
            m_showAdditionalAnalysis = EditorGUILayout.Foldout(
                m_showAdditionalAnalysis,
                "Additional Analysis (existing prototype tools)",
                true,
                EditorStyles.foldoutHeader);
            if (!m_showAdditionalAnalysis)
                return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUI.BeginChangeCheck();
                m_target = (GameObject)EditorGUILayout.ObjectField("Target", m_target, typeof(GameObject), true);
                if (EditorGUI.EndChangeCheck())
                    ClearAnalysis();

                using (new EditorGUI.DisabledScope(m_target == null))
                {
                    if (GUILayout.Button("Analyze Target"))
                        AnalyzeTarget();
                }

                if (m_hasAnalysis)
                {
                    DrawSummary();
                    DrawObjectsSection();
                    DrawMaterialsSection();
                    DrawAnchorsSection();
                    DrawValidationSection();
                }
            }
        }

        private void DrawTargetSection()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Characters with matching CC JSON", EditorStyles.miniBoldLabel);
                if (GUILayout.Button("Refresh", GUILayout.Width(80f)))
                    RefreshCharacterList();
            }

            DrawCharacterList();
        }

        private void DrawCharacterList()
        {
            using (new EditorGUILayout.VerticalScope("box"))
            {
                if (m_characters.Count == 0)
                {
                    EditorGUILayout.HelpBox(
                        "No Character Creator-style FBX assets were found. Expected a matching JSON file beside the FBX.",
                        MessageType.Info);
                    return;
                }

                float listHeight = Mathf.Min(180f, (m_characters.Count * 22f) + 4f);
                using (var scroll = new EditorGUILayout.ScrollViewScope(
                           m_characterListScrollPosition,
                           GUILayout.Height(listHeight)))
                {
                    m_characterListScrollPosition = scroll.scrollPosition;

                    foreach (CharacterInfo info in m_characters)
                    {
                        bool isSelected = string.Equals(
                            m_selectedCharacterGuid,
                            info.Guid,
                            StringComparison.Ordinal);

                        Color previousBackgroundColor = GUI.backgroundColor;
                        if (isSelected)
                            GUI.backgroundColor = new Color(0.35f, 0.8f, 0.45f);
                        else if (info.HasVhPrefab || info.HasPrefab)
                            GUI.backgroundColor = new Color(0.55f, 0.72f, 1f);

                        string state = GetCharacterAssetState(info);
                        string selectionPath = GetPreferredSelectionPath(info);
                        string selectedMarker = isSelected ? "▶ " : string.Empty;
                        if (GUILayout.Button(
                                new GUIContent($"{selectedMarker}{info.Name}  ({state})", selectionPath),
                                EditorStyles.miniButton))
                        {
                            SelectCharacter(info);
                        }

                        GUI.backgroundColor = previousBackgroundColor;
                    }
                }
            }
        }

        private void SelectCharacter(CharacterInfo info)
        {
            m_selectedCharacterGuid = info.Guid;
            m_importerSelectionStatus = string.Empty;

            string targetPath = GetPreferredSelectionPath(info);
            UnityEngine.Object selectedAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(targetPath);
            m_target = selectedAsset as GameObject;
            ClearAnalysis();

            if (selectedAsset != null)
            {
                Selection.activeObject = selectedAsset;
                EditorGUIUtility.PingObject(selectedAsset);
            }

            Repaint();
        }

        private static string GetCharacterAssetState(CharacterInfo info)
        {
            if (info.HasVhPrefab)
                return "VH Prefab";
            return info.HasPrefab ? "Prefab" : "Folder";
        }

        private static string GetPreferredSelectionPath(CharacterInfo info)
        {
            if (info.HasVhPrefab)
                return info.VhPrefabPath;
            if (info.HasPrefab)
                return info.PrefabPath;
            return info.FolderPath;
        }

        private void DrawSummary()
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Analysis Summary", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Target", m_analyzedTargetName);
                EditorGUILayout.LabelField("Asset Path", DisplayValue(m_assetPath));
                EditorGUILayout.LabelField("Render Pipeline", FormatPipelineLabel());
                EditorGUILayout.LabelField("Character Type", m_characterType);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("GameObjects", m_objectCount.ToString());
                EditorGUILayout.LabelField("Renderers", m_rendererCount.ToString());
                EditorGUILayout.LabelField("Skinned Mesh Renderers", m_skinnedMeshRendererCount.ToString());
                EditorGUILayout.LabelField("Unique Materials", m_materialCount.ToString());
            }
        }

        private void DrawObjectsSection()
        {
            m_showObjects = EditorGUILayout.Foldout(m_showObjects, "Objects", true);
            if (!m_showObjects)
                return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                DrawStringList(
                    "Possible unwanted objects",
                    m_unwantedObjectCandidates,
                    "No object names matched the initial boxer/underwear terms.");

                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(
                    "Placeholder: review matching rules before adding Disable or Remove operations.",
                    MessageType.None);
            }
        }

        private void DrawMaterialsSection()
        {
            m_showMaterials = EditorGUILayout.Foldout(m_showMaterials, "Materials", true);
            if (!m_showMaterials)
                return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField(
                    "These lists report shader properties only; they do not imply that a material should be changed.",
                    EditorStyles.wordWrappedMiniLabel);
                EditorGUILayout.Space();

                DrawStringList(
                    "Transparency-capable materials",
                    m_transparencyMaterialCandidates,
                    "No materials expose the initial URP/HDRP surface properties.");
                DrawStringList(
                    "Preserve-specular-capable materials",
                    m_preserveSpecularMaterialCandidates,
                    "No materials expose the initial preserve-specular property.");
                DrawStringList(
                    "Smoothness-capable materials",
                    m_smoothnessMaterialCandidates,
                    "No materials expose the initial smoothness properties.");

                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(
                    "Placeholder: material selection, surface changes, preserve-specular changes, and smoothness controls.",
                    MessageType.None);
            }
        }

        private void DrawAnchorsSection()
        {
            m_showAnchors = EditorGUILayout.Foldout(m_showAnchors, "Anchors", true);
            if (!m_showAnchors)
                return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Head Candidate", DisplayValue(m_headPath));
                EditorGUILayout.LabelField("Existing SoundNode", DisplayValue(m_soundNodePath));
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(
                    "Placeholder: create or reposition a plain SoundNode marker relative to the selected head transform.",
                    MessageType.None);
            }
        }

        private void DrawValidationSection()
        {
            m_showValidation = EditorGUILayout.Foldout(m_showValidation, "Validation", true);
            if (!m_showValidation)
                return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                DrawStringList("Notes", m_validationMessages, "No validation notes.");
            }
        }

        private static void DrawApplyPlaceholder()
        {
            EditorGUILayout.Space();
            using (new EditorGUI.DisabledScope(true))
                GUILayout.Button("Apply Selected Changes (Not Implemented)", GUILayout.Height(28f));
        }

        private static void DrawStringList(string label, List<string> values, string emptyMessage)
        {
            EditorGUILayout.LabelField($"{label} ({values.Count})", EditorStyles.miniBoldLabel);
            if (values.Count == 0)
            {
                EditorGUILayout.LabelField(emptyMessage, EditorStyles.wordWrappedMiniLabel);
                EditorGUILayout.Space();
                return;
            }

            foreach (string value in values)
                EditorGUILayout.SelectableLabel(value, EditorStyles.miniLabel, GUILayout.Height(EditorGUIUtility.singleLineHeight));

            EditorGUILayout.Space();
        }

        private CharacterInfo GetSelectedCharacter()
        {
            return m_characters.FirstOrDefault(character =>
                string.Equals(character.Guid, m_selectedCharacterGuid, StringComparison.Ordinal));
        }

        private void OpenImporter(CharacterInfo info)
        {
            GameObject fbx = AssetDatabase.LoadAssetAtPath<GameObject>(info.AssetPath);
            if (fbx == null)
            {
                EditorUtility.DisplayDialog("Character Not Found", $"Could not load:\n{info.AssetPath}", "OK");
                return;
            }

            Selection.activeObject = fbx;
            EditorGUIUtility.PingObject(fbx);
            if (!EditorApplication.ExecuteMenuItem("Reallusion/Import Characters"))
            {
                m_importerSelectionStatus = "The CC/iC Importer menu command is not available.";
                EditorUtility.DisplayDialog(
                    "CC/iC Importer Not Found",
                    "The Reallusion/Import Characters menu command is not available in this project.",
                    "OK");
                return;
            }

            if (TrySelectImporterCharacter(info.Guid, out string reason))
                m_importerSelectionStatus = $"CC/iC Importer opened with {info.Name} selected.";
            else
                m_importerSelectionStatus = reason;
        }

        private static bool TrySelectImporterCharacter(string characterGuid, out string reason)
        {
            reason = string.Empty;
            Type importerType = FindType("Reallusion.Import.ImporterWindow");
            if (importerType == null)
            {
                reason = "The CC/iC Importer type could not be found.";
                return false;
            }

            object importerWindow = importerType
                .GetProperty("Current", BindingFlags.Public | BindingFlags.Static)?
                .GetValue(null);
            if (importerWindow == null)
            {
                reason = "The CC/iC Importer opened, but its active window could not be found.";
                return false;
            }

            object characterList = importerType
                .GetProperty("CharacterList", BindingFlags.Public | BindingFlags.Static)?
                .GetValue(null);
            if (!(characterList is System.Collections.IEnumerable characters))
            {
                reason = "The CC/iC Importer opened, but its character list is unavailable.";
                return false;
            }

            bool containsCharacter = false;
            foreach (object character in characters)
            {
                FieldInfo guidField = character?.GetType().GetField("guid", BindingFlags.Public | BindingFlags.Instance);
                if (string.Equals(guidField?.GetValue(character) as string, characterGuid, StringComparison.Ordinal))
                {
                    containsCharacter = true;
                    break;
                }
            }

            if (!containsCharacter)
            {
                reason = "The importer did not include this FBX in its character list. Check that its expected textures folder is present.";
                return false;
            }

            MethodInfo setContextMethod = importerType.GetMethod(
                "SetContextCharacter",
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new[] { typeof(string) },
                null);
            if (setContextMethod == null)
            {
                reason = "This CC/iC Importer version does not expose the expected character-selection method.";
                return false;
            }

            try
            {
                setContextMethod.Invoke(importerWindow, new object[] { characterGuid });
                if (importerWindow is EditorWindow editorWindow)
                {
                    editorWindow.Focus();
                    editorWindow.Repaint();
                }

                return true;
            }
            catch (TargetInvocationException exception)
            {
                Debug.LogException(exception.InnerException ?? exception);
                reason = "The CC/iC Importer opened, but selecting the character failed. See the Console for details.";
                return false;
            }
        }

        private static bool TryGetMatchingImporterCharacter(
            CharacterInfo info,
            out object importerWindow,
            out object importerCharacter,
            out string reason)
        {
            importerWindow = null;
            importerCharacter = null;
            reason = string.Empty;

            if (info == null)
            {
                reason = "Choose a character first.";
                return false;
            }

            Type importerType = FindType("Reallusion.Import.ImporterWindow");
            if (importerType == null)
            {
                reason = "The CC/iC Importer is not installed in this project.";
                return false;
            }

            PropertyInfo currentProperty = importerType.GetProperty("Current", BindingFlags.Public | BindingFlags.Static);
            importerWindow = currentProperty?.GetValue(null);
            if (importerWindow == null)
            {
                reason = "Open the CC/iC Importer for the selected character first.";
                return false;
            }

            PropertyInfo characterProperty = importerType.GetProperty("Character", BindingFlags.Public | BindingFlags.Instance);
            importerCharacter = characterProperty?.GetValue(importerWindow);
            if (importerCharacter == null)
            {
                reason = "The CC/iC Importer does not currently have a character selected.";
                return false;
            }

            FieldInfo guidField = importerCharacter.GetType().GetField("guid", BindingFlags.Public | BindingFlags.Instance);
            string importerGuid = guidField?.GetValue(importerCharacter) as string;
            if (!string.Equals(importerGuid, info.Guid, StringComparison.Ordinal))
            {
                reason = "The CC/iC Importer is showing a different character. Reopen it from step 2.";
                return false;
            }

            return true;
        }

        private static void DrawImporterFeatureToggle(
            object importerWindow,
            object importerCharacter,
            string label,
            string flagName)
        {
            PropertyInfo flagsProperty = importerCharacter.GetType().GetProperty(
                "ShaderFlags",
                BindingFlags.Public | BindingFlags.Instance);
            if (flagsProperty == null || !flagsProperty.CanRead || !flagsProperty.CanWrite)
            {
                EditorGUILayout.LabelField(label, "Unavailable");
                return;
            }

            object currentValue = flagsProperty.GetValue(importerCharacter);
            if (!Enum.IsDefined(flagsProperty.PropertyType, flagName))
            {
                EditorGUILayout.LabelField(label, "Unavailable");
                return;
            }

            object parsedFlag = Enum.Parse(flagsProperty.PropertyType, flagName);
            long flag = Convert.ToInt64(parsedFlag);
            long flags = Convert.ToInt64(currentValue);
            bool enabled = (flags & flag) != 0;
            bool newValue = EditorGUILayout.ToggleLeft(label, enabled);
            if (newValue == enabled)
                return;

            long updatedFlags = newValue ? flags | flag : flags & ~flag;
            flagsProperty.SetValue(importerCharacter, Enum.ToObject(flagsProperty.PropertyType, updatedFlags));
            if (importerWindow is EditorWindow editorWindow)
                editorWindow.Repaint();
        }

        private static void InvokeImporterBuild(object importerWindow)
        {
            MethodInfo buildMethod = importerWindow.GetType().GetMethod(
                "BuildCharacter",
                BindingFlags.Instance | BindingFlags.NonPublic);
            if (buildMethod == null)
                return;

            EditorApplication.delayCall += () =>
            {
                try
                {
                    buildMethod.Invoke(importerWindow, null);
                }
                catch (TargetInvocationException exception)
                {
                    Debug.LogException(exception.InnerException ?? exception);
                }
            };
        }

        private static Type FindType(string fullName)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type type = assembly.GetType(fullName, false);
                if (type != null)
                    return type;
            }

            return null;
        }

        private static void FocusAsset(string assetPath)
        {
            UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
            if (asset == null)
                return;

            Selection.activeObject = asset;
            EditorGUIUtility.PingObject(asset);
        }

        private void CreateVhPrefabVariant(CharacterInfo info)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(info.PrefabPath);
            if (prefab == null)
                return;

            GameObject instance = null;
            try
            {
                instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                if (instance == null)
                    throw new InvalidOperationException($"Could not instantiate {info.PrefabPath}.");

                PrefabUtility.SaveAsPrefabAsset(instance, info.VhPrefabPath, out bool success);
                if (!success)
                    throw new InvalidOperationException($"Could not save {info.VhPrefabPath}.");
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                EditorUtility.DisplayDialog("Create Variant Failed", exception.Message, "OK");
                return;
            }
            finally
            {
                if (instance != null)
                    DestroyImmediate(instance);
            }

            AssetDatabase.SaveAssets();
            RefreshCharacterList();
            FocusAsset(info.VhPrefabPath);
        }

        private static GameObject GetVhPrefabAsset(CharacterInfo info)
        {
            return info != null && info.HasVhPrefab
                ? AssetDatabase.LoadAssetAtPath<GameObject>(info.VhPrefabPath)
                : null;
        }

        private static GameObject GetVhPrefabRootForReview(CharacterInfo info)
        {
            PrefabStage stage = PrefabStageUtility.GetCurrentPrefabStage();
            if (stage != null && info != null &&
                string.Equals(stage.assetPath, info.VhPrefabPath, StringComparison.OrdinalIgnoreCase))
            {
                return stage.prefabContentsRoot;
            }

            return GetVhPrefabAsset(info);
        }

        private static List<GameObject> GetMatchingRendererObjects(
            CharacterInfo info,
            Func<Renderer, bool> predicate)
        {
            GameObject prefab = GetVhPrefabRootForReview(info);
            if (prefab == null)
                return new List<GameObject>();

            return prefab.GetComponentsInChildren<Renderer>(true)
                .Where(predicate)
                .Select(renderer => renderer.gameObject)
                .Distinct()
                .OrderBy(gameObject => GetTransformPath(gameObject.transform, prefab.transform), StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static List<GameObject> GetMatchingMaterialRendererObjects(
            CharacterInfo info,
            Func<Material, bool> predicate)
        {
            return GetMatchingRendererObjects(info, renderer => RendererUsesMaterial(renderer, predicate));
        }

        private static bool RendererUsesMaterial(Renderer renderer, Func<Material, bool> predicate)
        {
            return (renderer.sharedMaterials ?? Array.Empty<Material>())
                .Any(material => material != null && predicate(material));
        }

        private void DrawPrefabReviewButtons(
            CharacterInfo info,
            int matchCount,
            Func<Renderer, bool> predicate)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                DrawOpenPrefabButton(info);
                using (new EditorGUI.DisabledScope(info == null || !info.HasVhPrefab || matchCount == 0))
                {
                    if (GUILayout.Button($"Highlight Matches ({matchCount})"))
                        HighlightMatchingRenderers(info, predicate);
                }
            }
        }

        private static void DrawOpenPrefabButton(CharacterInfo info)
        {
            using (new EditorGUI.DisabledScope(info == null || !info.HasVhPrefab))
            {
                if (GUILayout.Button("Open _VH Prefab"))
                    OpenVhPrefab(info);
            }
        }

        private static PrefabStage OpenVhPrefab(CharacterInfo info)
        {
            PrefabStage stage = info != null && info.HasVhPrefab
                ? PrefabStageUtility.OpenPrefab(info.VhPrefabPath)
                : null;
            if (stage != null)
                EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");

            return stage;
        }

        private static void HighlightMatchingRenderers(CharacterInfo info, Func<Renderer, bool> predicate)
        {
            PrefabStage stage = OpenVhPrefab(info);
            if (stage == null || stage.prefabContentsRoot == null)
                return;

            UnityEngine.Object[] matches = stage.prefabContentsRoot.GetComponentsInChildren<Renderer>(true)
                .Where(predicate)
                .Select(renderer => renderer.gameObject)
                .Distinct()
                .Cast<UnityEngine.Object>()
                .ToArray();
            SelectPrefabObjects(matches);
        }

        private static void HighlightSoundNode(CharacterInfo info)
        {
            PrefabStage stage = OpenVhPrefab(info);
            if (stage == null || stage.prefabContentsRoot == null)
                return;

            UnityEngine.Object[] matches = stage.prefabContentsRoot.GetComponentsInChildren<Transform>(true)
                .Where(transform => string.Equals(transform.name, "SoundNode", StringComparison.OrdinalIgnoreCase))
                .Select(transform => transform.gameObject)
                .Cast<UnityEngine.Object>()
                .ToArray();
            SelectPrefabObjects(matches);
        }

        private static void SelectPrefabObjects(UnityEngine.Object[] matches)
        {
            Selection.objects = matches;
            if (matches.Length == 0)
                return;

            EditorGUIUtility.PingObject(matches[0]);
            if (SceneView.lastActiveSceneView != null)
                SceneView.lastActiveSceneView.FrameSelected();
        }

        private void SetMatchingObjectsActive(
            CharacterInfo info,
            Func<Renderer, bool> predicate,
            bool active)
        {
            PrefabStage stage = PrefabStageUtility.GetCurrentPrefabStage();
            bool isOpenInPrefabStage = stage != null &&
                string.Equals(stage.assetPath, info.VhPrefabPath, StringComparison.OrdinalIgnoreCase);
            GameObject root = isOpenInPrefabStage
                ? stage.prefabContentsRoot
                : PrefabUtility.LoadPrefabContents(info.VhPrefabPath);
            try
            {
                foreach (GameObject match in root.GetComponentsInChildren<Renderer>(true)
                    .Where(predicate)
                    .Select(renderer => renderer.gameObject)
                    .Distinct())
                {
                    match.SetActive(active);
                }

                PrefabUtility.SaveAsPrefabAsset(root, info.VhPrefabPath);
            }
            finally
            {
                if (!isOpenInPrefabStage)
                    PrefabUtility.UnloadPrefabContents(root);
            }

            AssetDatabase.SaveAssets();
            Repaint();
        }

        private static List<Material> GetMaterials(CharacterInfo info)
        {
            GameObject prefab = GetVhPrefabRootForReview(info);
            if (prefab == null)
                return new List<Material>();

            return prefab.GetComponentsInChildren<Renderer>(true)
                .SelectMany(renderer => renderer.sharedMaterials ?? Array.Empty<Material>())
                .Where(material => material != null)
                .Distinct()
                .OrderBy(material => material.name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static List<Material> GetMaterials(IEnumerable<GameObject> gameObjects)
        {
            return gameObjects
                .Select(gameObject => gameObject.GetComponent<Renderer>())
                .Where(renderer => renderer != null)
                .SelectMany(renderer => renderer.sharedMaterials ?? Array.Empty<Material>())
                .Where(material => material != null)
                .Distinct()
                .OrderBy(material => material.name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static bool IsLikelyClothingRenderer(Renderer renderer)
        {
            return !ContainsAny(renderer.name, NonClothingRendererTerms) &&
                (renderer.sharedMaterials ?? Array.Empty<Material>())
                .Any(material => material != null && material.HasProperty("_Smoothness"));
        }

        private static void DrawMaterialCandidates(
            CharacterInfo info,
            Func<Material, bool> predicate,
            Action<Material> drawControls)
        {
            DrawMaterialList(info, GetMaterials(info).Where(predicate).ToList(), drawControls);
        }

        private static void DrawMaterialList(
            CharacterInfo info,
            List<Material> materials,
            Action<Material> drawControls)
        {
            EditorGUILayout.LabelField($"Candidate materials ({materials.Count})", EditorStyles.miniBoldLabel);
            if (materials.Count == 0)
            {
                EditorGUILayout.LabelField(
                    info != null && info.HasVhPrefab ? "No matching materials found." : "Create the _VH prefab first.",
                    EditorStyles.wordWrappedMiniLabel);
                return;
            }

            foreach (Material material in materials)
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.ObjectField(material, typeof(Material), false);
                        if (GUILayout.Button("Select", GUILayout.Width(58f)))
                        {
                            Selection.activeObject = material;
                            EditorGUIUtility.PingObject(material);
                        }
                    }

                    EditorGUILayout.LabelField(material.shader.name, EditorStyles.wordWrappedMiniLabel);
                    drawControls?.Invoke(material);
                }
            }
        }

        private static void DrawSurfaceTypeControl(Material material)
        {
            string propertyName = TransparencyPropertyNames.FirstOrDefault(material.HasProperty);
            bool recommendedTransparent =
                material.name.IndexOf("_1st_Pass", StringComparison.OrdinalIgnoreCase) < 0;
            string recommendation = recommendedTransparent ? "Transparent" : "Opaque";
            if (string.IsNullOrEmpty(propertyName))
            {
                EditorGUILayout.LabelField(
                    $"Surface Type: fixed by shader (render queue {material.renderQueue}). Recommended: {recommendation}.",
                    EditorStyles.wordWrappedMiniLabel);
                return;
            }

            var current = material.GetFloat(propertyName) > 0.5f
                ? MaterialSurfaceType.Transparent
                : MaterialSurfaceType.Opaque;
            EditorGUI.BeginChangeCheck();
            var next = (MaterialSurfaceType)EditorGUILayout.EnumPopup(
                new GUIContent("Surface Type", propertyName),
                current);
            if (EditorGUI.EndChangeCheck())
                SetMaterialFloat(material, propertyName, (float)next, true, "Change Material Surface Type");

            var recommended = recommendedTransparent
                ? MaterialSurfaceType.Transparent
                : MaterialSurfaceType.Opaque;
            using (new EditorGUI.DisabledScope(current == recommended))
            {
                if (GUILayout.Button($"Use Recommended: {recommendation}"))
                    SetMaterialFloat(material, propertyName, (float)recommended, true, "Set Recommended Surface Type");
            }
        }

        private static void DrawPreserveSpecularControl(Material material)
        {
            string propertyName = PreserveSpecularPropertyNames.FirstOrDefault(material.HasProperty);
            if (string.IsNullOrEmpty(propertyName))
            {
                EditorGUILayout.LabelField(
                    "Preserve Specular Lighting is fixed by this shader.",
                    EditorStyles.wordWrappedMiniLabel);
                return;
            }

            bool current = material.GetFloat(propertyName) > 0.5f;
            EditorGUI.BeginChangeCheck();
            bool next = EditorGUILayout.ToggleLeft(
                new GUIContent("Preserve Specular Lighting", propertyName),
                current);
            if (EditorGUI.EndChangeCheck())
                SetMaterialFloat(material, propertyName, next ? 1f : 0f, true, "Change Preserve Specular Lighting");
        }

        private static void DrawSmoothnessControls(Material material, float? recommendedSmoothness)
        {
            bool hasProperty = false;
            hasProperty |= DrawMaterialSlider(
                material,
                "_Smoothness",
                "Smoothness",
                0f,
                1f,
                recommendedSmoothness);
            hasProperty |= DrawMaterialSlider(material, "_SmoothnessMin", "Remapping Min", 0f, 1f, null);
            hasProperty |= DrawMaterialSlider(material, "_SmoothnessMax", "Remapping Max", 0f, 1f, null);
            hasProperty |= DrawMaterialSlider(material, "_SmoothnessContrast", "Remapping Contrast", 0f, 2f, null);

            if (!hasProperty)
                EditorGUILayout.LabelField("This shader exposes no supported smoothness properties.", EditorStyles.wordWrappedMiniLabel);
        }

        private static bool DrawMaterialSlider(
            Material material,
            string propertyName,
            string label,
            float minimum,
            float maximum,
            float? recommendedValue)
        {
            if (!material.HasProperty(propertyName))
                return false;

            float current = material.GetFloat(propertyName);
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUI.BeginChangeCheck();
                float next = EditorGUILayout.Slider(new GUIContent(label, propertyName), current, minimum, maximum);
                if (EditorGUI.EndChangeCheck())
                    SetMaterialFloat(material, propertyName, next, false, $"Change {label}");

                if (recommendedValue.HasValue)
                {
                    using (new EditorGUI.DisabledScope(Mathf.Approximately(current, recommendedValue.Value)))
                    {
                        if (GUILayout.Button($"Set {recommendedValue.Value:0.##}", GUILayout.Width(58f)))
                        {
                            SetMaterialFloat(
                                material,
                                propertyName,
                                recommendedValue.Value,
                                false,
                                $"Set Recommended {label}");
                        }
                    }
                }
            }

            return true;
        }

        private static void SetMaterialFloat(
            Material material,
            string propertyName,
            float value,
            bool validateMaterial,
            string undoName)
        {
            Undo.RecordObject(material, undoName);
            material.SetFloat(propertyName, value);
            if (validateMaterial)
                ValidateMaterial(material);

            EditorUtility.SetDirty(material);
            AssetDatabase.SaveAssetIfDirty(material);
        }

        private static void ValidateMaterial(Material material)
        {
            MaterialEditor materialEditor = UnityEditor.Editor.CreateEditor(material) as MaterialEditor;
            try
            {
                materialEditor?.customShaderGUI?.ValidateMaterial(material);
            }
            finally
            {
                if (materialEditor != null)
                    DestroyImmediate(materialEditor);
            }
        }

        private void AddSoundNode(CharacterInfo info)
        {
            PrefabStage stage = PrefabStageUtility.GetCurrentPrefabStage();
            bool isOpenInPrefabStage = stage != null &&
                string.Equals(stage.assetPath, info.VhPrefabPath, StringComparison.OrdinalIgnoreCase);
            GameObject root = isOpenInPrefabStage
                ? stage.prefabContentsRoot
                : PrefabUtility.LoadPrefabContents(info.VhPrefabPath);
            try
            {
                Transform existing = root.GetComponentsInChildren<Transform>(true).FirstOrDefault(transform =>
                    string.Equals(transform.name, "SoundNode", StringComparison.OrdinalIgnoreCase));
                if (existing == null)
                {
                    var node = new GameObject("SoundNode");
                    node.transform.SetParent(root.transform, false);
                    node.transform.SetLocalPositionAndRotation(GetSoundNodePosition(), Quaternion.identity);
                    node.AddComponent<AudioSource>();
                    PrefabUtility.SaveAsPrefabAsset(root, info.VhPrefabPath);
                }
            }
            finally
            {
                if (!isOpenInPrefabStage)
                    PrefabUtility.UnloadPrefabContents(root);
            }

            AssetDatabase.SaveAssets();
            HighlightSoundNode(info);
        }

        private Vector3 GetSoundNodePosition()
        {
            return m_soundNodeGender == SoundNodeGender.Female
                ? new Vector3(0f, 1.54f, 0.075f)
                : new Vector3(0f, 1.6f, 0.055f);
        }

        private void RefreshCharacterList()
        {
            m_characters ??= new List<CharacterInfo>();
            string selectedGuid = m_selectedCharacterGuid;

            m_characters.Clear();
            foreach (string guid in AssetDatabase.FindAssets("t:Model", new[] { "Assets" }))
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (TryBuildCharacterInfo(guid, assetPath, out CharacterInfo info))
                    m_characters.Add(info);
            }

            m_characters = m_characters
                .OrderBy(character => character.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            m_selectedCharacterGuid = m_characters.Any(character =>
                string.Equals(character.Guid, selectedGuid, StringComparison.Ordinal))
                ? selectedGuid
                : string.Empty;
        }

        private static bool TryBuildCharacterInfo(
            string guid,
            string assetPath,
            out CharacterInfo info)
        {
            info = null;
            if (string.IsNullOrEmpty(assetPath) ||
                !assetPath.EndsWith(".fbx", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            string folder = Path.GetDirectoryName(assetPath);
            string name = Path.GetFileNameWithoutExtension(assetPath);
            if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(name))
                return false;

            // A CC character entry requires its matching metadata file and sibling lowercase textures folder.
            string jsonPath = Path.Combine(folder, name + ".json");
            string texturesPath = Path.Combine(folder, "textures");
            if (!Directory.Exists(texturesPath) || !File.Exists(jsonPath))
            {
                return false;
            }

            string prefabPath = Path.Combine(folder, "Prefabs", name + ".prefab").Replace("\\", "/");
            string vhPrefabPath = Path.Combine(folder, "Prefabs", name + "_VH.prefab").Replace("\\", "/");

            info = new CharacterInfo
            {
                Guid = guid,
                Name = name,
                FolderPath = folder.Replace("\\", "/"),
                AssetPath = assetPath,
                PrefabPath = prefabPath,
                VhPrefabPath = vhPrefabPath,
                HasPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null,
                HasVhPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(vhPrefabPath) != null,
            };
            return true;
        }

        private void AnalyzeTarget()
        {
            ClearAnalysis();
            if (m_target == null)
                return;

            m_analyzedTargetName = m_target.name;
            m_assetPath = AssetDatabase.GetAssetPath(m_target);
            m_pipelineName = GetRenderPipelineName();
            m_pipelineVersion = GetRenderPipelineVersion();

            Transform[] transforms = m_target.GetComponentsInChildren<Transform>(true);
            Renderer[] renderers = m_target.GetComponentsInChildren<Renderer>(true);
            var materials = renderers
                .SelectMany(renderer => renderer.sharedMaterials ?? Array.Empty<Material>())
                .Where(material => material != null)
                .Distinct()
                .OrderBy(material => material.name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            m_objectCount = transforms.Length;
            m_rendererCount = renderers.Length;
            m_skinnedMeshRendererCount = renderers.Count(renderer => renderer is SkinnedMeshRenderer);
            m_materialCount = materials.Count;

            foreach (Transform transform in transforms)
            {
                if (ContainsAny(transform.name, UnwantedObjectTerms))
                    m_unwantedObjectCandidates.Add(GetTransformPath(transform, m_target.transform));
            }

            foreach (Material material in materials)
            {
                AddMaterialIfPropertyExists(material, TransparencyPropertyNames, m_transparencyMaterialCandidates);
                AddMaterialIfPropertyExists(material, PreserveSpecularPropertyNames, m_preserveSpecularMaterialCandidates);
                AddMaterialIfPropertyExists(material, SmoothnessPropertyNames, m_smoothnessMaterialCandidates);
            }

            Transform head = FindHeadCandidate(transforms);
            Transform soundNode = transforms.FirstOrDefault(
                transform => string.Equals(transform.name, "SoundNode", StringComparison.OrdinalIgnoreCase));

            m_headPath = head != null ? GetTransformPath(head, m_target.transform) : string.Empty;
            m_soundNodePath = soundNode != null ? GetTransformPath(soundNode, m_target.transform) : string.Empty;
            m_characterType = IsLikelyCharacterCreatorCharacter(transforms) ? "Likely Character Creator" : "Generic / Unknown";

            BuildValidationMessages();
            m_hasAnalysis = true;
        }

        private void BuildValidationMessages()
        {
            if (string.IsNullOrEmpty(m_assetPath))
                m_validationMessages.Add("Target is a scene object. Prefab-output behavior is not defined yet.");
            else if (!PrefabUtility.IsPartOfPrefabAsset(m_target))
                m_validationMessages.Add("Target is a project asset, but it is not recognized as a prefab asset.");

            if (m_pipelineName == "BuiltIn" || m_pipelineName == "Unknown")
                m_validationMessages.Add($"The active render pipeline is {m_pipelineName}; the initial target is URP and HDRP.");

            if (string.IsNullOrEmpty(m_headPath))
                m_validationMessages.Add("No likely head transform was found. SoundNode placement will require manual selection.");

            if (m_unwantedObjectCandidates.Count > 0)
                m_validationMessages.Add("Possible unwanted objects were found. Review every hierarchy path before implementing changes.");

            if (m_materialCount == 0)
                m_validationMessages.Add("No materials were found under the target.");
        }

        private void ClearAnalysis()
        {
            m_hasAnalysis = false;
            m_analyzedTargetName = string.Empty;
            m_assetPath = string.Empty;
            m_pipelineName = string.Empty;
            m_pipelineVersion = string.Empty;
            m_characterType = string.Empty;
            m_objectCount = 0;
            m_rendererCount = 0;
            m_skinnedMeshRendererCount = 0;
            m_materialCount = 0;
            m_headPath = string.Empty;
            m_soundNodePath = string.Empty;
            m_unwantedObjectCandidates.Clear();
            m_transparencyMaterialCandidates.Clear();
            m_preserveSpecularMaterialCandidates.Clear();
            m_smoothnessMaterialCandidates.Clear();
            m_validationMessages.Clear();
        }

        private void TryUseCurrentSelection()
        {
            if (m_target != null || Selection.activeObject == null)
                return;

            string selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            CharacterInfo matchingCharacter = m_characters.FirstOrDefault(character =>
                string.Equals(character.AssetPath, selectedPath, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(character.PrefabPath, selectedPath, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(character.VhPrefabPath, selectedPath, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(character.FolderPath, selectedPath, StringComparison.OrdinalIgnoreCase));
            if (matchingCharacter == null)
                return;

            m_target = Selection.activeObject as GameObject;
            m_selectedCharacterGuid = matchingCharacter != null ? matchingCharacter.Guid : string.Empty;
            ClearAnalysis();
        }

        private string FormatPipelineLabel()
        {
            return string.IsNullOrEmpty(m_pipelineVersion)
                ? m_pipelineName
                : $"{m_pipelineName} {m_pipelineVersion}";
        }

        private static string GetRenderPipelineName()
        {
            RenderPipelineAsset pipeline = GraphicsSettings.currentRenderPipeline;
            if (pipeline == null)
                return "BuiltIn";

            Type type = pipeline.GetType();
            string typeNamespace = type.Namespace ?? string.Empty;
            string typeName = type.Name ?? string.Empty;

            if (typeNamespace.Contains("Universal") || typeName.Contains("Universal"))
                return "URP";

            if (typeNamespace.Contains("HighDefinition") ||
                typeName.Contains("HDRenderPipeline") ||
                typeName.Contains("HDRP"))
            {
                return "HDRP";
            }

            return string.IsNullOrEmpty(typeName) ? "Unknown" : typeName;
        }

        private static string GetRenderPipelineVersion()
        {
            RenderPipelineAsset pipeline = GraphicsSettings.currentRenderPipeline;
            if (pipeline == null)
                return string.Empty;

            string typeNamespace = pipeline.GetType().Namespace ?? string.Empty;
            string packageName = null;

            if (typeNamespace.Contains("HighDefinition"))
                packageName = "com.unity.render-pipelines.high-definition";
            else if (typeNamespace.Contains("Universal"))
                packageName = "com.unity.render-pipelines.universal";

            if (string.IsNullOrEmpty(packageName))
                return string.Empty;

            UnityEditor.PackageManager.PackageInfo packageInfo =
                UnityEditor.PackageManager.PackageInfo.FindForAssetPath("Packages/" + packageName);
            return packageInfo != null ? packageInfo.version : string.Empty;
        }

        private static Transform FindHeadCandidate(IEnumerable<Transform> transforms)
        {
            Transform[] allTransforms = transforms.ToArray();
            string[] preferredNames = { "CC_Base_Head", "Head", "head" };

            foreach (string preferredName in preferredNames)
            {
                Transform match = allTransforms.FirstOrDefault(
                    transform => string.Equals(transform.name, preferredName, StringComparison.Ordinal));
                if (match != null)
                    return match;
            }

            return allTransforms.FirstOrDefault(transform =>
                transform.name.IndexOf("head", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private static bool IsLikelyCharacterCreatorCharacter(IEnumerable<Transform> transforms)
        {
            bool hasHead = false;
            bool hasHip = false;

            foreach (Transform transform in transforms)
            {
                hasHead |= string.Equals(transform.name, "CC_Base_Head", StringComparison.OrdinalIgnoreCase);
                hasHip |= string.Equals(transform.name, "CC_Base_Hip", StringComparison.OrdinalIgnoreCase);
            }

            return hasHead && hasHip;
        }

        private static void AddMaterialIfPropertyExists(
            Material material,
            IEnumerable<string> propertyNames,
            List<string> destination)
        {
            string[] matches = propertyNames.Where(material.HasProperty).ToArray();
            if (matches.Length == 0)
                return;

            string shaderName = material.shader != null ? material.shader.name : "Missing Shader";
            destination.Add($"{material.name}  [{shaderName}]  {string.Join(", ", matches)}");
        }

        private static bool ContainsAny(string value, IEnumerable<string> terms)
        {
            return terms.Any(term => value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private static string GetTransformPath(Transform transform, Transform root)
        {
            var names = new List<string>();
            Transform current = transform;

            while (current != null)
            {
                names.Add(current.name);
                if (current == root)
                    break;

                current = current.parent;
            }

            names.Reverse();
            return string.Join("/", names);
        }

        private static string DisplayValue(string value)
        {
            return string.IsNullOrEmpty(value) ? "Not found" : value;
        }
    }
}
