using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using VHAssets;

namespace Ride.VH
{
    public class CCCharacterSetupWindow : EditorWindow
    {
        [Serializable]
        private class CharacterInfo
        {
            public string Guid;
            public string Name;
            public string AssetPath;      // FBX path
            public string Folder;         // Folder containing FBX
            public string JsonPath;       // <fbxname>.json
            public string FbmFolder;      // <fbxname>.fbm
            public string TexturesFolder; // "textures"

            public bool HasJson;
            public bool HasFbmFolder;
            public bool HasTexturesFolder;

            public string PrefabFolder;   // Folder/Prefabs
            public string PrefabPath;     // Folder/Prefabs/<fbxname>.prefab
            public string VhPrefabPath;   // Folder/Prefabs/<fbxname>_VH.prefab

            public bool HasPrefab;        // Imported by CC window
            public bool HasVhPrefab;      // Fully configured by this window

            public bool FolderStructureValid => HasJson && HasFbmFolder && HasTexturesFolder;
        }


        private const string ExpectedReallusionMenu = "Reallusion/Import Characters";

        private static readonly List<FacialAnimationPlayer_BlendShape.VisemeBlendShapeMapping> DefaultCCFacialRecipe = new()
        {
            // This maps VISIME KEY -> one or more BLENDSHAPE NAMES.
            new (FacialAnimationPlayer.FaceShape.FV) { BlendShapes = new List<FacialAnimationPlayer_BlendShape.BlendShapeEntry> {
                new ("F_V") { WeightMultiplier = 1f }, } },
            new (FacialAnimationPlayer.FaceShape.open) { BlendShapes = new List<FacialAnimationPlayer_BlendShape.BlendShapeEntry> {
                new ("Oh") { WeightMultiplier = 1f }, } },
            new (FacialAnimationPlayer.FaceShape.PBM) { BlendShapes = new List<FacialAnimationPlayer_BlendShape.BlendShapeEntry> {
                new ("B_M_P") { WeightMultiplier = 1f }, } },
            new (FacialAnimationPlayer.FaceShape.ShCh) { BlendShapes = new List<FacialAnimationPlayer_BlendShape.BlendShapeEntry> {
                new ("Ch_J") { WeightMultiplier = 0.75f }, } },
            new (FacialAnimationPlayer.FaceShape.tBack) { BlendShapes = new List<FacialAnimationPlayer_BlendShape.BlendShapeEntry> {
                /* empty */ } },
            new (FacialAnimationPlayer.FaceShape.tRoof) { BlendShapes = new List<FacialAnimationPlayer_BlendShape.BlendShapeEntry> {
                /* empty */ } },
            new (FacialAnimationPlayer.FaceShape.tTeeth) { BlendShapes = new List<FacialAnimationPlayer_BlendShape.BlendShapeEntry> {
                /* empty */ } },
            new (FacialAnimationPlayer.FaceShape.W) { BlendShapes = new List<FacialAnimationPlayer_BlendShape.BlendShapeEntry> {
                new ("W_OO") { WeightMultiplier = 1f }, } },
            new (FacialAnimationPlayer.FaceShape.wide) { BlendShapes = new List<FacialAnimationPlayer_BlendShape.BlendShapeEntry> {
                new ("EE") { WeightMultiplier = 1f }, } },
            new (FacialAnimationPlayer.FaceShape.face_neutral) { BlendShapes = new List<FacialAnimationPlayer_BlendShape.BlendShapeEntry> {
                new ("None") { WeightMultiplier = 1f }, } },
        };

        /// <summary>
        /// a short, human-readable summary of the CC recipe for UI display.
        /// Keep this small; it is meant for an Editor help box.
        /// </summary>
        private const string DefaultCCFacialRecipeSummary =
            "CC BlendShape Recipe:\n" +
            "- FV -> F_V\n" +
            "- open -> Oh\n" +
            "- PBM -> B_M_P\n" +
            "- ShCh -> Ch_J\n" +
            "- tBack -> <empty>\n" +
            "- tRoof -> <empty>\n" +
            "- tTeeth -> <empty>\n" +
            "- W -> W_OO\n" +
            "- wide -> EE\n" +
            "- face_neutral -> None";

        /// <summary>
        /// Hard-coded CC recipe used by the setup window.
        /// Starter version based on the inspector screenshot.
        /// </summary>
        public static readonly List<EyelidController_BlendShapes.BlendShapeMapping> DefaultCCEyelidRecipe = new()
        {
            new (EyelidController_BlendShapes.EyelidSideLeft) { BlendShapes = new List<EyelidController_BlendShapes.BlendShapeEntry> {
                new EyelidController_BlendShapes.BlendShapeEntry("Eye_Blink_L") { WeightMultiplier = 1f }, } },
            new (EyelidController_BlendShapes.EyelidSideRight) { BlendShapes = new List<EyelidController_BlendShapes.BlendShapeEntry> {
                new EyelidController_BlendShapes.BlendShapeEntry("Eye_Blink_R") { WeightMultiplier = 1f }, } },
        };

        /// <summary> Short human-readable recipe summary for editor UI.</summary>
        private const string DefaultCCEyelidRecipeSummary =
            "CC Eyelid BlendShape Recipe:\n" +
            "- 045_blink_lf -> Eye_Blink_L\n" +
            "- 045_blink_rt -> Eye_Blink_R";


        private List<CharacterInfo> m_Characters = new();
        private Vector2 m_ListScroll;
        private Vector2 m_DetailScroll;
        private int m_SelectedIndex = -1;


        // Task toggles
        [SerializeField] private bool m_SetUpdateWhenOffscreen = false;
        [SerializeField] private bool m_AdjustNormalMapStrength = true;
        [SerializeField] private float m_NormalMapStrength = 0.3f;
        [SerializeField] private bool m_AdjustLensesSettings = true;
        [SerializeField] private bool m_AdjustJoints = false;
        [SerializeField] private Vector3 m_ClavicleOffset = new Vector3(0f, -0.02f, 0f);

        // VH component toggles (defaults as requested)
        [SerializeField] private RuntimeAnimatorController m_animatorController;
        [SerializeField] private bool m_AddVHCharacterProfile = true;
        [SerializeField] private UnityEngine.Object m_nvbgSystemScript;
        [SerializeField] private bool m_AddMecanimCharacter = true;
        [SerializeField] private string m_mecanimStartingPosture = "PSA_IdleStandingUpright01";
        [SerializeField] private bool m_AddGazeControllerIK = true;
        [SerializeField] private float m_gazeHeadWeight = 0.5f;
        [SerializeField] private float m_gazeBodyWeight = 0.2f;
        [SerializeField] private float m_gazeEyeWeight = 1.0f;
        [SerializeField] private bool m_AddFacialAnimationPlayer = true;
        [SerializeField]
        [Tooltip("If enabled, applies the hard-coded CC blendshape recipe to FacialAnimationPlayer_BlendShape during prefab creation.")]
        private bool m_ApplyFacialBlendShapeCCRecipe = true;
        [SerializeField] private bool m_showFacialCCRecipeSummary;
        [SerializeField] private bool m_AddHeadController = true;
        [SerializeField] private string m_headControllerNeckName = "CC_Base_Head";
        [SerializeField] private bool m_headControllerFlipTransform = true;
        [SerializeField] private bool m_AddBlinkController = true;
        [SerializeField] private bool m_AddEyelidController = true;
        [SerializeField]
        [Tooltip("If enabled, applies the hard-coded CC blendshape recipe to EyelidController_BlendShape during prefab creation.")]
        private bool m_ApplyEyelidBlendShapeCCRecipe = true;
        [SerializeField] private bool m_showEyelidCCRecipeSummary;
        [SerializeField] private bool m_AddSaccadeController = true;
        [SerializeField] private string m_saccadeLeftEyeName = "CC_Base_L_Eye";
        [SerializeField] private bool m_saccadeLeftEyeInverted = false;
        [SerializeField] private string m_saccadeRightEyeName = "CC_Base_R_Eye";
        [SerializeField] private bool m_saccadeRightEyeInverted = false;

        [SerializeField] private bool m_AddGenericMaleAdultGestureMap = true;
        [SerializeField] private bool m_AddBmlEventHandler = true;
        [SerializeField] private bool m_AddAudioSourceNode = true;

        [SerializeField] private bool m_AddAnimatorMessenger = false;
        [SerializeField] private bool m_AddListeningController = false;
        [SerializeField] private bool m_AddMirroringController = false;


        [MenuItem("Ride/CC Character Setup")]
        public static void ShowWindow()
        {
            var window = GetWindow<CCCharacterSetupWindow>("CC Character Setup");
            window.minSize = new Vector2(600f, 400f);
            window.RefreshCharacterList();
        }

        private void OnEnable()
        {
            if (m_Characters == null)
                m_Characters = new List<CharacterInfo>();

            if (m_Characters.Count == 0)
                RefreshCharacterList();

            EnsureDefaultAnimatorController();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                // LEFT: list
                using (new EditorGUILayout.VerticalScope())
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField("CC5 Characters In Project", EditorStyles.boldLabel);
                        if (GUILayout.Button("Refresh"))
                            RefreshCharacterList();
                    }

                    DrawCharacterListSection();
                }

                DrawVerticalSeparator();

                // RIGHT: details
                using (new EditorGUILayout.VerticalScope())
                {
                    EditorGUILayout.LabelField("Selected Character", EditorStyles.boldLabel);
                    DrawSelectedCharacterSection();
                }
            }
        }

        private void DrawCharacterListSection()
        {
            using (new EditorGUILayout.VerticalScope("box"))
            {
                if (m_Characters == null || m_Characters.Count == 0)
                {
                    EditorGUILayout.HelpBox("No CC5-style FBX assets were found.\n\nClick Refresh after exporting a character from Character Creator 5.", MessageType.Info);
                    return;
                }

                using (var scroll = new EditorGUILayout.ScrollViewScope(m_ListScroll))
                {
                    m_ListScroll = scroll.scrollPosition;

                    for (int i = 0; i < m_Characters.Count; i++)
                    {
                        var info = m_Characters[i];
                        bool isSelected = (i == m_SelectedIndex);

                        using (new EditorGUILayout.HorizontalScope())
                        {
                            Color prevColor = GUI.color;
                            GUI.color = GetCharacterItemColor(info);

                            var style = isSelected ? EditorStyles.miniButtonMid : EditorStyles.miniButton;
                            if (GUILayout.Button(info.Name, style))
                                m_SelectedIndex = i;

                            GUI.color = prevColor;
                        }
                    }
                }
            }
        }

        private void DrawSelectedCharacterSection()
        {
            if (m_SelectedIndex < 0 || m_SelectedIndex >= m_Characters.Count)
            {
                EditorGUILayout.HelpBox("Select a CC5 character from the list above.", MessageType.Info);
                return;
            }

            var info = m_Characters[m_SelectedIndex];

            using (var scroll = new EditorGUILayout.ScrollViewScope(m_DetailScroll))
            {
                m_DetailScroll = scroll.scrollPosition;

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Label("FBX Path:", GUILayout.Width(180));
                    DrawClickableAssetLabel(info.AssetPath, true);
                }
                EditorGUILayout.Space();

                // Folder structure report
                EditorGUILayout.LabelField("Folder Structure", EditorStyles.boldLabel);
                DrawPathStatus("FBX", info.AssetPath, File.Exists(info.AssetPath));
                DrawPathStatus("JSON", info.JsonPath, info.HasJson);
                DrawPathStatus("FBM Folder", info.FbmFolder, info.HasFbmFolder);
                DrawPathStatus("Textures Folder", info.TexturesFolder, info.HasTexturesFolder);
                bool hasPrefabFolder = AssetDatabase.IsValidFolder(info.PrefabFolder);
                DrawPathStatus("Prefabs Folder", info.PrefabFolder, hasPrefabFolder);
                bool hasPrefab = File.Exists(info.PrefabPath);
                DrawPathStatus("Prefab", info.PrefabPath, hasPrefab);
                bool hasVhPrefab = File.Exists(info.VhPrefabPath);
                DrawPathStatus("VH Prefab", info.VhPrefabPath, hasVhPrefab);

                if (!info.FolderStructureValid)
                    EditorGUILayout.HelpBox("Folder structure does not match expected CC5 export:\n- <fbxname>.fbx\n- <fbxname>.json\n- <fbxname>.fbm (folder)\n- textures (folder)", MessageType.Warning);
                else
                    EditorGUILayout.HelpBox("Folder structure looks valid.", MessageType.Info);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                // Reallusion importer button
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Label("Step 1: Import Character Prefab via Reallusion Importer", EditorStyles.boldLabel);
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Open Importer Tab", GUILayout.Width(160f)))
                        EditorApplication.ExecuteMenuItem(ExpectedReallusionMenu);
                }

                EditorGUILayout.HelpBox($"Use Reallusion -> Import Characters to create the main prefab.\n\nAfter import, this tool expects the prefab at:\n{info.PrefabPath}", MessageType.None);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                // Task checkboxes
                EditorGUILayout.LabelField("Step 2: Configuration Tasks", EditorStyles.boldLabel);

                using (new EditorGUI.DisabledGroupScope(!info.HasPrefab))
                {
                    EditorGUILayout.LabelField("Mesh and Materials", EditorStyles.miniBoldLabel);
                    m_SetUpdateWhenOffscreen = EditorGUILayout.ToggleLeft("Set all SkinnedMeshRenderers: updateWhenOffscreen = true", m_SetUpdateWhenOffscreen);

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        m_AdjustNormalMapStrength = EditorGUILayout.ToggleLeft("Adjust Surface Input Normal Map strength on materials", m_AdjustNormalMapStrength);
                        GUILayout.FlexibleSpace();
                        using (new EditorGUI.DisabledGroupScope(!m_AdjustNormalMapStrength))
                            m_NormalMapStrength = EditorGUILayout.FloatField("Normal Strength", m_NormalMapStrength, GUILayout.Width(220f));
                    }

                    m_AdjustLensesSettings = EditorGUILayout.ToggleLeft("Adjust \"lenses\" objects (Shadow Off, Preserve Specular = 0, Alpha Clipping = 1)", m_AdjustLensesSettings);

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Skeleton Adjustments", EditorStyles.miniBoldLabel);
                    m_AdjustJoints = EditorGUILayout.ToggleLeft("Apply hard-coded clavicle offset", m_AdjustJoints);
                    using (new EditorGUI.DisabledGroupScope(!m_AdjustJoints))
                        m_ClavicleOffset = EditorGUILayout.Vector3Field("Clavicle Offset (local)", m_ClavicleOffset);

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("VH Components To Add To Root", EditorStyles.miniBoldLabel);

                    using (new EditorGUILayout.VerticalScope("box"))
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            m_animatorController = (RuntimeAnimatorController)EditorGUILayout.ObjectField("Animator Controller", m_animatorController, typeof(RuntimeAnimatorController), false);
                            if (GUILayout.Button("Male", GUILayout.Width(80f))) m_animatorController = FindAssetByName<RuntimeAnimatorController>("CCMaleAnimatorController");
                            if (GUILayout.Button("Female", GUILayout.Width(80f))) m_animatorController = FindAssetByName<RuntimeAnimatorController>("CCFemaleAnimatorController");
                        }

                        if (m_animatorController == null)
                            EditorGUILayout.HelpBox("No Animator Controller selected. Use Male/Female or drag one in.", MessageType.Info);

                        EditorGUILayout.Space();

                        m_AddVHCharacterProfile = EditorGUILayout.ToggleLeft("VHCharacterProfile", m_AddVHCharacterProfile);

                        GUI.enabled = false;
                        m_nvbgSystemScript = EditorGUILayout.ObjectField("NVBG System (script)", m_nvbgSystemScript, typeof(UnityEngine.Object), false);

                        using (new EditorGUILayout.HorizontalScope())
                        {
                            if (GUILayout.Button("Male", GUILayout.Width(80f))) m_nvbgSystemScript = FindAssetByName<UnityEngine.Object>("NonverbalBehaviorGeneratorSystemCCMale");
                            if (GUILayout.Button("Female", GUILayout.Width(80f))) m_nvbgSystemScript = FindAssetByName<UnityEngine.Object>("NonverbalBehaviorGeneratorSystemCCFemale");
                            GUILayout.FlexibleSpace();
                        }

                        if (m_nvbgSystemScript == null)
                            EditorGUILayout.HelpBox("No NVBG script selected. Use Male/Female or drag the script in.", MessageType.Info);
                        GUI.enabled = true;

                        EditorGUILayout.Space();

                        m_AddMecanimCharacter = EditorGUILayout.ToggleLeft("MecanimCharacter", m_AddMecanimCharacter);
                        using (new EditorGUI.DisabledGroupScope(!m_AddMecanimCharacter))
                        {
                            EditorGUI.indentLevel += 2;
                            using (new EditorGUILayout.HorizontalScope())
                            {
                                m_mecanimStartingPosture = EditorGUILayout.TextField("Starting Posture", m_mecanimStartingPosture);
                                if (GUILayout.Button("Male", GUILayout.Width(80f))) m_mecanimStartingPosture = "PSA_IdleStandingUpright01";
                                if (GUILayout.Button("Female", GUILayout.Width(80f))) m_mecanimStartingPosture = "CC_Fml_IdleStandingUpright01";
                            }
                            EditorGUI.indentLevel -= 2;
                        }

                        using (new EditorGUILayout.HorizontalScope())
                        {
                            m_AddGazeControllerIK = EditorGUILayout.ToggleLeft("GazeController_IK", m_AddGazeControllerIK);
                            m_gazeHeadWeight = EditorGUILayout.Slider("Head Weight", m_gazeHeadWeight, 0.0f, 1.0f);
                            m_gazeBodyWeight = EditorGUILayout.Slider("Body Weight", m_gazeBodyWeight, 0.0f, 1.0f);
                            m_gazeEyeWeight = EditorGUILayout.Slider("Eye Weight", m_gazeEyeWeight, 0.0f, 1.0f);
                        }

                        m_AddFacialAnimationPlayer = EditorGUILayout.ToggleLeft("FacialAnimationPlayer (BlendShape)", m_AddFacialAnimationPlayer);
                        using (new EditorGUI.DisabledGroupScope(!m_AddFacialAnimationPlayer))
                        {
                            EditorGUI.indentLevel += 2;
                            m_ApplyFacialBlendShapeCCRecipe = EditorGUILayout.ToggleLeft(new GUIContent("Apply Facial CC BlendShape Recipe", "Applies a hard-coded mapping recipe suitable for CC characters."), m_ApplyFacialBlendShapeCCRecipe);
                            if (m_ApplyFacialBlendShapeCCRecipe)
                            {
                                using (new EditorGUILayout.HorizontalScope())
                                {
                                    GUILayout.Space(EditorGUI.indentLevel * 15f);
                                    m_showFacialCCRecipeSummary = EditorGUILayout.BeginFoldoutHeaderGroup(m_showFacialCCRecipeSummary, "CC Facial BlendShape Recipe");
                                    if (m_showFacialCCRecipeSummary)
                                        EditorGUILayout.HelpBox(DefaultCCFacialRecipeSummary, MessageType.Info);
                                    EditorGUILayout.EndFoldoutHeaderGroup();
                                }
                            }
                            EditorGUI.indentLevel -= 2;
                        }
                        EditorGUILayout.Space();

                        m_AddHeadController = EditorGUILayout.ToggleLeft("HeadController", m_AddHeadController);
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            EditorGUI.indentLevel += 2;
                            m_headControllerNeckName = EditorGUILayout.TextField("Neck Transform", m_headControllerNeckName);
                            m_headControllerFlipTransform = EditorGUILayout.Toggle("Flip Transform?", m_headControllerFlipTransform);
                            EditorGUI.indentLevel -= 2;
                        }
                        EditorGUILayout.Space();

                        m_AddBlinkController = EditorGUILayout.ToggleLeft("BlinkValueController", m_AddBlinkController);
                        m_AddEyelidController = EditorGUILayout.ToggleLeft("EyelidController (BlendShape)", m_AddEyelidController);
                        using (new EditorGUI.DisabledGroupScope(!m_AddEyelidController))
                        {
                            EditorGUI.indentLevel += 2;
                            m_ApplyEyelidBlendShapeCCRecipe = EditorGUILayout.ToggleLeft(new GUIContent("Apply Eyelid CC BlendShape Recipe", "Applies a hard-coded mapping recipe suitable for CC characters."), m_ApplyEyelidBlendShapeCCRecipe);
                            if (m_ApplyEyelidBlendShapeCCRecipe)
                            {
                                using (new EditorGUILayout.HorizontalScope())
                                {
                                    GUILayout.Space(EditorGUI.indentLevel * 15f);
                                    m_showEyelidCCRecipeSummary = EditorGUILayout.BeginFoldoutHeaderGroup(m_showEyelidCCRecipeSummary, "CC Eyelid BlendShape Recipe");
                                    if (m_showEyelidCCRecipeSummary)
                                        EditorGUILayout.HelpBox(DefaultCCEyelidRecipeSummary, MessageType.Info);
                                    EditorGUILayout.EndFoldoutHeaderGroup();
                                }
                            }
                            EditorGUI.indentLevel -= 2;
                        }
                        EditorGUILayout.Space();
                        m_AddSaccadeController = EditorGUILayout.ToggleLeft("SaccadeController", m_AddSaccadeController);
                        EditorGUI.indentLevel += 2;
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            EditorGUILayout.LabelField("Left Eye", EditorStyles.miniBoldLabel, GUILayout.Width(80));
                            m_saccadeLeftEyeName = EditorGUILayout.TextField("Transform Name", m_saccadeLeftEyeName);
                            m_saccadeLeftEyeInverted = EditorGUILayout.Toggle("Is Inverted?", m_saccadeLeftEyeInverted);
                        }
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            EditorGUILayout.LabelField("Right Eye", EditorStyles.miniBoldLabel, GUILayout.Width(80));
                            m_saccadeRightEyeName = EditorGUILayout.TextField("Transform Name", m_saccadeRightEyeName);
                            m_saccadeRightEyeInverted = EditorGUILayout.Toggle("Is Inverted?", m_saccadeRightEyeInverted);
                        }
                        EditorGUI.indentLevel -= 2;
                        EditorGUILayout.Space();

                        m_AddGenericMaleAdultGestureMap = EditorGUILayout.ToggleLeft("GenericMaleAdultGestureMap", m_AddGenericMaleAdultGestureMap);
                        m_AddBmlEventHandler = EditorGUILayout.ToggleLeft("BmlEventHandler (BML Event Handler)", m_AddBmlEventHandler);
                        m_AddAudioSourceNode = EditorGUILayout.ToggleLeft("Audio Source (SoundNode)", m_AddAudioSourceNode);

                        EditorGUILayout.Space();
                        EditorGUILayout.LabelField("Optional Components", EditorStyles.miniBoldLabel);
                        m_AddAnimatorMessenger = EditorGUILayout.ToggleLeft("AnimatorMessenger", m_AddAnimatorMessenger);
                        m_AddListeningController = EditorGUILayout.ToggleLeft("ListeningController", m_AddListeningController);
                        m_AddMirroringController = EditorGUILayout.ToggleLeft("MirroringController", m_AddMirroringController);
                    }

                    EditorGUILayout.Space();

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("Configure And Create VH Prefab", GUILayout.Width(260f), GUILayout.Height(28f)))
                            ConfigureSelectedCharacter(info);

                        if (GUILayout.Button("Take Screenshot", GUILayout.Width(140f), GUILayout.Height(28f)))
                            TakeScreenshotForSelectedCharacter(info);
                    }
                }
            }
        }

        private static void DrawPathStatus(string label, string path, bool exists)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label(label, GUILayout.Width(110f));
                Color prev = GUI.color;
                GUI.color = exists ? Color.green : Color.red;
                GUILayout.Label(exists ? "Found" : "Missing", GUILayout.Width(70f));
                GUI.color = prev;
                DrawClickableAssetLabel(path, exists);
            }
        }

        private static void DrawClickableAssetLabel(string path, bool exists)
        {
            if (exists && !string.IsNullOrEmpty(path))
            {
                var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
                if (asset != null)
                {
                    if (GUILayout.Button(path, EditorStyles.linkLabel))
                    {
                        Selection.activeObject = asset;
                        EditorGUIUtility.PingObject(asset);
                    }
                }
                else
                {
                    GUILayout.Label(path);
                }
            }
            else
            {
                GUILayout.Label(path);
            }
        }

        private void RefreshCharacterList()
        {
            // Remember currently selected character (by GUID) before clearing
            string selectedGuid = null;
            if (m_Characters != null && m_SelectedIndex >= 0 && m_SelectedIndex < m_Characters.Count)
                selectedGuid = m_Characters[m_SelectedIndex].Guid;

            m_Characters.Clear();
            m_SelectedIndex = -1;

            // Find all Model assets (includes FBX)
            string[] guids = AssetDatabase.FindAssets("t:Model", new[] { "Assets" });

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (TryBuildCharacterInfo(guid, path, out CharacterInfo info))
                    m_Characters.Add(info);
            }

            m_Characters = m_Characters.OrderBy(c => c.Name, StringComparer.OrdinalIgnoreCase).ToList();

            // Restore selection if the same character still exists
            if (!string.IsNullOrEmpty(selectedGuid))
            {
                for (int i = 0; i < m_Characters.Count; i++)
                {
                    if (string.Equals(m_Characters[i].Guid, selectedGuid, StringComparison.Ordinal))
                    {
                        m_SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private bool TryBuildCharacterInfo(string guid, string assetPath, out CharacterInfo info)
        {
            info = null;

            if (string.IsNullOrEmpty(assetPath))
                return false;

            if (!assetPath.EndsWith(".fbx", StringComparison.OrdinalIgnoreCase))
                return false;

            string folder = Path.GetDirectoryName(assetPath);
            string name = Path.GetFileNameWithoutExtension(assetPath);

            // Heuristic: treat as a CC-style character if there is a matching JSON next to the FBX.
            string jsonPath = Path.Combine(folder, name + ".json").Replace("\\", "/");
            bool hasJson = File.Exists(jsonPath);
            if (!hasJson)
                return false;

            string fbmFolder = Path.Combine(folder, name + ".fbm").Replace("\\", "/");
            bool hasFbm = AssetDatabase.IsValidFolder(fbmFolder);

            string texturesFolder = Path.Combine(folder, "textures").Replace("\\", "/");
            bool hasTextures = AssetDatabase.IsValidFolder(texturesFolder);
            if (!hasTextures)
                return false;

            string prefabFolder = Path.Combine(folder, "Prefabs").Replace("\\", "/");
            string prefabPath = Path.Combine(prefabFolder, name + ".prefab").Replace("\\", "/");
            string vhPrefabPath = Path.Combine(prefabFolder, name + "_VH.prefab").Replace("\\", "/");

            bool hasPrefab = File.Exists(prefabPath);
            bool hasVhPrefab = File.Exists(vhPrefabPath);

            info = new CharacterInfo
            {
                Guid = guid,
                Name = name,
                AssetPath = assetPath,
                Folder = folder,
                JsonPath = jsonPath,
                FbmFolder = fbmFolder,
                TexturesFolder = texturesFolder,
                HasJson = hasJson,
                HasFbmFolder = hasFbm,
                HasTexturesFolder = hasTextures,
                PrefabFolder = prefabFolder,
                PrefabPath = prefabPath,
                VhPrefabPath = vhPrefabPath,
                HasPrefab = hasPrefab,
                HasVhPrefab = hasVhPrefab
            };

            return true;
        }

        private void ConfigureSelectedCharacter(CharacterInfo info)
        {
            if (info == null)
            {
                EditorUtility.DisplayDialog("No Character Selected", "Please select a character first.", "OK");
                return;
            }

            if (!File.Exists(info.PrefabPath))
            {
                EditorUtility.DisplayDialog("Prefab Not Found", $"Expected prefab at:\n\n{info.PrefabPath}\n\nRun Reallusion -> Import Characters for this FBX, then try again.", "OK");
                return;
            }

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(info.PrefabPath);
            if (prefab == null)
            {
                EditorUtility.DisplayDialog("Prefab Load Failed", $"Could not load prefab:\n{info.PrefabPath}", "OK");
                return;
            }

            string vhPrefabPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(info.PrefabFolder, info.Name + "_VH.prefab").Replace("\\", "/"));

            GameObject instance = null;

            try
            {
                instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                if (instance == null)
                {
                    EditorUtility.DisplayDialog("Instantiate Failed", $"Failed to instantiate prefab:\n{info.PrefabPath}", "OK");
                    return;
                }

                ApplyConfigurationToInstance(instance);

                PrefabUtility.SaveAsPrefabAsset(instance, vhPrefabPath, out bool success);

                if (success)
                {
                    Debug.Log($"Created VH variant prefab: {vhPrefabPath}");
                    var created = AssetDatabase.LoadAssetAtPath<GameObject>(vhPrefabPath);
                    Selection.activeObject = created;
                    EditorGUIUtility.PingObject(created);
                    EditorUtility.DisplayDialog("VH Prefab Created", $"New VH variant prefab created:\n{vhPrefabPath}", "OK");
                }
                else
                {
                    EditorUtility.DisplayDialog("Save Failed", $"SaveAsPrefabAsset reported failure for:\n{vhPrefabPath}", "OK");
                }
            }
            finally
            {
                if (instance != null)
                    DestroyImmediate(instance);
            }

            RefreshCharacterList();
        }

        private void ApplyConfigurationToInstance(GameObject root)
        {
            if (root == null)
                return;

            if (m_SetUpdateWhenOffscreen)
                SetSMRUpdateWhenOffscreen(root, true);

            if (m_AdjustNormalMapStrength)
                AdjustNormalMapStrengthOnMaterials(root, m_NormalMapStrength);

            if (m_AdjustLensesSettings)
                AdjustLensesSettings(root);

            if (m_AdjustJoints)
                ApplyClavicleOffset(root, m_ClavicleOffset);

            AddAndConfigureVHComponents(root);
        }

        private void SetSMRUpdateWhenOffscreen(GameObject root, bool value)
        {
            var smrs = root.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            foreach (var smr in smrs)
                smr.updateWhenOffscreen = value;
        }

        private void AdjustNormalMapStrengthOnMaterials(GameObject root, float strength)
        {
            var renderers = root.GetComponentsInChildren<Renderer>(true);
            foreach (var rend in renderers)
            {
                if (rend.sharedMaterials == null)
                    continue;

                foreach (var mat in rend.sharedMaterials)
                {
                    if (mat == null)
                        continue;

                    // Try a few common normal strength property names.
                    //SetFloatIfHasProperty(mat, "_BumpScale", strength);
                    SetFloatIfHasProperty(mat, "_NormalScale", strength);
                    //SetFloatIfHasProperty(mat, "_NORMAL_SCALE", strength);
                }
            }
        }

        private void SetFloatIfHasProperty(Material mat, string propName, float value)
        {
            if (mat.HasProperty(propName))
            {
                mat.SetFloat(propName, value);
                EditorUtility.SetDirty(mat);
            }
        }

        private void AdjustLensesSettings(GameObject root)
        {
            var transforms = root.GetComponentsInChildren<Transform>(true);
            foreach (var t in transforms)
            {
                if (!string.Equals(t.name, "Lenses", StringComparison.OrdinalIgnoreCase))
                    continue;

                var rend = t.GetComponent<Renderer>();
                if (rend == null)
                    continue;

                rend.shadowCastingMode = ShadowCastingMode.Off;

                if (rend.sharedMaterials == null)
                    continue;

                foreach (var mat in rend.sharedMaterials)
                {
                    if (mat == null)
                        continue;

                    SetFloatIfHasProperty(mat, "_EnableBlendModePreserveSpecularLighting", 0);
                    SetFloatIfHasProperty(mat, "_AlphaCutoffEnable", 1);
                }
            }
        }

        private void ApplyClavicleOffset(GameObject root, Vector3 offset)
        {
            var transforms = root.GetComponentsInChildren<Transform>(true);
            foreach (var t in transforms)
            {
                if (string.Equals(t.name, "L_Clavicle", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(t.name, "R_Clavicle", StringComparison.OrdinalIgnoreCase))
                {
                    t.localPosition += offset;
                }
            }
        }

        private void AddAndConfigureVHComponents(GameObject root)
        {
            ConfigureAnimatorController(root);

            if (m_AddVHCharacterProfile)
                ConfigureVHCharacterProfile(root);

            if (m_AddMecanimCharacter)
                ConfigureMecanimCharacter(root);

            if (m_AddGazeControllerIK)
                ConfigureGazeControllerIK(root);

            if (m_AddFacialAnimationPlayer)
                ConfigureFacialAnimationBlendShape(root);

            if (m_AddHeadController)
                ConfigureHeadController(root);

            if (m_AddBlinkController)
                AddComponentByType<BlinkValueProvider>(root);

            if (m_AddEyelidController)
                ConfigureEyelidController(root);

            if (m_AddSaccadeController)
                ConfigureSaccadeController(root);

            if (m_AddGenericMaleAdultGestureMap)
                AddComponentByType<GenericMaleAdultGestureMap>(root);

            if (m_AddBmlEventHandler)
            {
                var bml = AddComponentByType<BMLEventHandler>(root);
                ConfigureBmlEventHandler(bml);
            }

            if (m_AddAudioSourceNode)
                ConfigureAudioSourceNode(root);

            if (m_AddAnimatorMessenger)
                AddComponentByType<AnimatorMessenger>(root);

            if (m_AddListeningController)
                AddComponentByType<ListeningController>(root);

            if (m_AddMirroringController)
                AddComponentByType<MirroringController>(root);
        }

        private static T AddComponentByType<T>(GameObject go) where T : Component
        {
            var existing = go.GetComponent<T>();
            if (existing != null)
                return existing;

            return go.AddComponent<T>();
        }

        private void ConfigureAnimatorController(GameObject root)
        {
            var animator = root.GetComponentInChildren<Animator>(true);
            if (animator == null)
            {
                Debug.LogWarning($"No Animator found under '{root.name}'. Cannot assign Animator Controller.");
                return;
            }

            animator.runtimeAnimatorController = m_animatorController;
            EditorUtility.SetDirty(animator);
        }

        private void ConfigureVHCharacterProfile(GameObject root)
        {
            var profile = AddComponentByType<VHCharacterProfile>(root);

            // Add NVBG component from selected script (if any).
            if (m_nvbgSystemScript != null)
            {
#if false
                Type t = m_nvbgSystemScript.GetClass();
                if (t == null)
                {
                    Debug.LogWarning("NVBG script has no class (GetClass returned null).");
                }
                else if (!typeof(Component).IsAssignableFrom(t))
                {
                    Debug.LogWarning($"NVBG type '{t.FullName}' is not a Component.");
                }
                else
                {
                    var existing = root.GetComponent(t);
                    Component nvbgComponent = existing != null ? existing : root.AddComponent(t);

                    // VHCharacterProfile.NVBG is strongly typed; assign only if compatible.
                    if (nvbgComponent is NonverbalBehaviorGeneratorSystem nvbgSystem)
                    {
                        profile.NVBG = nvbgSystem;
                        EditorUtility.SetDirty(profile);
                    }
                    else
                    {
                        Debug.LogWarning(
                            $"NVBG component '{t.FullName}' is not a NonverbalBehaviorGeneratorSystem; cannot assign to VHCharacterProfile.NVBG.");
                    }
                }
#endif
            }
        }

        private void ConfigureMecanimCharacter(GameObject root)
        {
            var mecanim = AddComponentByType<MecanimCharacter>(root);

            var so = new SerializedObject(mecanim);
            var postureProp = so.FindProperty("m_StartingPosture");
            if (postureProp != null)
                postureProp.stringValue = m_mecanimStartingPosture;

            so.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(mecanim);
        }

        private void ConfigureGazeControllerIK(GameObject root)
        {
            var gaze = AddComponentByType<GazeController_IK>(root);
            gaze.HeadGazeWeight = Mathf.Clamp01(m_gazeHeadWeight);
            gaze.BodyGazeWeight = Mathf.Clamp01(m_gazeBodyWeight);
            gaze.EyeGazeWeight = Mathf.Clamp01(m_gazeEyeWeight);

            EditorUtility.SetDirty(gaze);
        }

        private void ConfigureFacialAnimationBlendShape(GameObject root)
        {
            var player = AddComponentByType<FacialAnimationPlayer_BlendShape>(root);

            if (m_ApplyFacialBlendShapeCCRecipe)
                player.EditorSetBlendShapeMapping(DefaultCCFacialRecipe);

            EditorUtility.SetDirty(player);
        }

        private void ConfigureHeadController(GameObject root)
        {
            var head = AddComponentByType<HeadController>(root);

            var so = new SerializedObject(head);

            var neckProp = so.FindProperty("m_NeckTransformName");
            if (neckProp != null)
                neckProp.stringValue = m_headControllerNeckName;

            var flipProp = so.FindProperty("m_flipTransform");
            if (flipProp != null)
                flipProp.boolValue = m_headControllerFlipTransform;

            var nodAmpProp = so.FindProperty("m_NodAmplifier");
            if (nodAmpProp != null)
                nodAmpProp.floatValue = 10.0f;

            so.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(head);
        }

        private void ConfigureEyelidController(GameObject root)
        {
            var eyelid = AddComponentByType<EyelidController_BlendShapes>(root);

            if (m_ApplyEyelidBlendShapeCCRecipe)
                eyelid.EditorSetBlendShapeMapping(DefaultCCEyelidRecipe);
        }

        private void ConfigureSaccadeController(GameObject root)
        {
            var saccade = AddComponentByType<SaccadeController>(root);

            var so = new SerializedObject(saccade);

            var eyesProp = so.FindProperty("m_EyeTransformNames");
            if (eyesProp == null || !eyesProp.isArray)
            {
                Debug.LogWarning("SaccadeController: could not find serialized array 'm_EyeTransformNames'.");
                return;
            }

            eyesProp.arraySize = 2;

            // Left eye
            var left = eyesProp.GetArrayElementAtIndex(0);
            var leftName = left.FindPropertyRelative("transformName");
            var leftInv = left.FindPropertyRelative("isInverted");
            if (leftName != null) leftName.stringValue = m_saccadeLeftEyeName;
            if (leftInv != null) leftInv.boolValue = m_saccadeLeftEyeInverted;

            // Right eye
            var right = eyesProp.GetArrayElementAtIndex(1);
            var rightName = right.FindPropertyRelative("transformName");
            var rightInv = right.FindPropertyRelative("isInverted");
            if (rightName != null) rightName.stringValue = m_saccadeRightEyeName;
            if (rightInv != null) rightInv.boolValue = m_saccadeRightEyeInverted;

            var magProp = so.FindProperty("m_MagnitudeScaler");
            if (magProp != null)
                magProp.floatValue = 0.6f;

            var modeProp = so.FindProperty("m_SaccadeMode");
            if (modeProp != null)
                modeProp.intValue = (int)CharacterDefines.SaccadeType.Listen;

            so.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(saccade);
        }

        private static void ConfigureBmlEventHandler(BMLEventHandler bmlHandler)
        {
            bmlHandler.m_TrimBMLTimingWhenParsing = false;


            // scene setup
            // - set Character Controller to MecanimManager in scene
            // - set Cutscene Prefab to Cutscene01 in scene
        }

        private static void ConfigureAudioSourceNode(GameObject root)
        {
            // Reuse if already present
            Transform existing = root.transform.Find("SoundNode");
            GameObject node;

            if (existing != null)
            {
                node = existing.gameObject;
            }
            else
            {
                node = new GameObject("SoundNode");
                node.transform.SetParent(root.transform, false);
            }

            // Position at head height (quick & dirty)
            node.transform.SetLocalPositionAndRotation(new Vector3(0f, 1.7f, 0f), Quaternion.identity);
            node.transform.localScale = Vector3.one;

            // Ensure AudioSource
            var audio = node.GetComponent<AudioSource>();
            if (audio == null)
                audio = node.AddComponent<AudioSource>();

            audio.playOnAwake = false;
            audio.spatialBlend = 1.0f;  // 3D
            audio.rolloffMode = AudioRolloffMode.Linear;
            audio.minDistance = 1.0f;
            audio.maxDistance = 100.0f;

            EditorUtility.SetDirty(node);
            EditorUtility.SetDirty(audio);
        }

        private static Color GetCharacterItemColor(CharacterInfo info)
        {
            // 1) Yellow = missing files (folder structure not fully valid)
            if (!info.FolderStructureValid) return new Color(1.0f, 0.85f, 0.3f);  // Warm yellow, not too neon

            // 2) Grey = not yet imported by CC window (no prefab in Prefabs folder)
            if (!info.HasPrefab) return new Color(0.6f, 0.6f, 0.6f);  // Neutral grey

            // 3) Blue = imported by CC, but not configured with our window yet
            if (info.HasPrefab && !info.HasVhPrefab) return new Color(0.42f, 0.65f, 1.0f);   // soft Unity-style blue

            // 4) Green = fully configured (base prefab + VH variant present)
            //return new Color(0.15f, 0.9f, 0.5f);  // Slightly brighter / more cyan-tinted green
            return new Color(0.3f, 0.8f, 0.3f);  // medium green
        }

        private static void DrawVerticalSeparator()
        {
            Rect rect = GUILayoutUtility.GetRect(1f, 1f, GUILayout.ExpandHeight(true));
            if (Event.current.type == EventType.Repaint)
                EditorGUI.DrawRect(rect, new Color(0.3f, 0.3f, 0.3f, 1f));  // A subtle dark line
        }

        private static T FindAssetByName<T>(string exactName) where T : UnityEngine.Object
        {
            if (string.IsNullOrEmpty(exactName))
                return null;

            // Search by name, then verify exact match.
            string filter = exactName + " t:" + typeof(T).Name;
            string[] guids = AssetDatabase.FindAssets(filter, new[] { "Assets" });

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (asset != null && string.Equals(asset.name, exactName, StringComparison.Ordinal))
                    return asset;
            }

            // Fallback: search by type only (in case name query didn’t hit), then exact match
            guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { "Assets" });
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (asset != null && string.Equals(asset.name, exactName, StringComparison.Ordinal))
                    return asset;
            }

            return null;
        }

        private void EnsureDefaultAnimatorController()
        {
            if (m_animatorController != null)
                return;

            m_animatorController = FindAssetByName<RuntimeAnimatorController>("CCMaleAnimatorController");
        }

        private void TakeScreenshotForSelectedCharacter(CharacterInfo info)
        {
            if (info == null)
                return;

            // Use VH prefab if it exists; otherwise, fall back to base prefab.
            string prefabPath = File.Exists(info.VhPrefabPath) ? info.VhPrefabPath : info.PrefabPath;

            if (!File.Exists(prefabPath))
            {
                EditorUtility.DisplayDialog("Prefab Not Found", $"Expected prefab at:\n\n{prefabPath}\n\nCreate the prefab first, then try again.", "OK");
                return;
            }

            string previewScenePath = FindScenePathByName("RL_PreviewScene");
            if (string.IsNullOrEmpty(previewScenePath))
            {
                EditorUtility.DisplayDialog("Preview Scene Not Found", "Could not find a scene asset named 'RL_PreviewScene' anywhere under Assets.\n\nCreate it or rename your preview scene to match.", "OK");
                return;
            }

            string prefabFolder = info.PrefabFolder;
            if (!AssetDatabase.IsValidFolder(prefabFolder))
            {
                EditorUtility.DisplayDialog("Prefab Folder Not Found", $"Prefab folder does not exist:\n\n{prefabFolder}", "OK");
                return;
            }

            string pngName = Path.GetFileNameWithoutExtension(prefabPath) + ".png";
            string outputAssetPath = Path.Combine(prefabFolder, pngName).Replace("\\", "/");
            string outputFullPath = GetFullPathFromAssetPath(outputAssetPath);

            var prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefabAsset == null)
            {
                EditorUtility.DisplayDialog("Load Failed", $"Could not load prefab:\n{prefabPath}", "OK");
                return;
            }

            var originalSetup = EditorSceneManager.GetSceneManagerSetup();

            try
            {
                // Load preview scene (single mode). We will restore original setup afterward.
                EditorSceneManager.OpenScene(previewScenePath, OpenSceneMode.Single);

                // Find a camera in the scene (prefer a Camera named "Main Camera", else any camera).
                Camera cam = FindPreferredCamera();
                if (cam == null)
                {
                    EditorUtility.DisplayDialog("Camera Not Found", "No Camera was found in RL_PreviewScene.\n\nAdd a Camera (e.g., 'Main Camera') and try again.", "OK");
                    return;
                }

                // Move camera relative to its current orientation.
                cam.transform.position += (cam.transform.forward * 2.5f) + (cam.transform.up * 0.5f) + (cam.transform.right * -0.1f);

                // Instantiate prefab into the loaded preview scene at origin.
                Scene scene = SceneManager.GetActiveScene();
                var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset, scene);
                if (instance == null)
                {
                    EditorUtility.DisplayDialog("Instantiate Failed", $"Failed to instantiate:\n{prefabPath}", "OK");
                    return;
                }

                instance.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

                // Force one evaluation tick for Animator if present (edit mode safe).
                var animator = instance.GetComponentInChildren<Animator>(true);
                if (animator != null)
                    animator.Update(0f);

                // Render and save.
                TakeCameraScreenshotToPng(cam, outputFullPath, 1024, 1024);

                AssetDatabase.Refresh();

                Debug.Log($"Screenshot saved: {outputAssetPath}");
                EditorUtility.DisplayDialog("Screenshot Saved", $"Saved:\n{outputAssetPath}", "OK");
            }
            finally
            {
                // Restore whatever scenes the user had open before, without saving preview scene changes.
                EditorSceneManager.RestoreSceneManagerSetup(originalSetup);
            }
        }

        private static string FindScenePathByName(string sceneNameNoExtension)
        {
            if (string.IsNullOrEmpty(sceneNameNoExtension))
                return null;

            // Search all scene assets and match by filename.
            string[] guids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets" });
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                string file = Path.GetFileNameWithoutExtension(path);
                if (string.Equals(file, sceneNameNoExtension, StringComparison.Ordinal))
                    return path;
            }

            return null;
        }

        private static Camera FindPreferredCamera()
        {
            var cams = FindObjectsByType<Camera>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if (cams == null || cams.Length == 0)
                return null;

            // Prefer Main Camera tag if present, else a camera named "Main Camera", else first.
            foreach (var c in cams)
            {
                if (c != null && c.CompareTag("MainCamera"))
                    return c;
            }

            foreach (var c in cams)
            {
                if (c != null && string.Equals(c.name, "Main Camera", StringComparison.Ordinal))
                    return c;
            }

            return cams[0];
        }

        private static void TakeCameraScreenshotToPng(Camera cam, string outputFullPath, int width, int height)
        {
            if (cam == null)
                return;

            Directory.CreateDirectory(Path.GetDirectoryName(outputFullPath));

            var prevRt = RenderTexture.active;
            var prevTarget = cam.targetTexture;

            var rt = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
            var tex = new Texture2D(width, height, TextureFormat.ARGB32, false);

            try
            {
                cam.targetTexture = rt;
                cam.Render();

                RenderTexture.active = rt;
                tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
                tex.Apply();

                byte[] png = tex.EncodeToPNG();
                File.WriteAllBytes(outputFullPath, png);
            }
            finally
            {
                cam.targetTexture = prevTarget;
                RenderTexture.active = prevRt;

                DestroyImmediate(rt);
                DestroyImmediate(tex);
            }
        }

        private static string GetFullPathFromAssetPath(string assetPath)
        {
            // assetPath like "Assets/..."
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            return Path.GetFullPath(Path.Combine(projectRoot, assetPath));
        }
    }
}
