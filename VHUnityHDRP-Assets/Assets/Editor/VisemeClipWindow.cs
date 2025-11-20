// VisemeClipWindow.cs
// EditorWindow to preview and bake single-frame viseme AnimationClips using a recipe system.
// Usage: Ride > Visemes
// - Select the character root in the Hierarchy.
// - Optionally set the Face Mesh Filter (defaults to "CC_Base_Body").
// - Pick a Viseme, Preview, then Generate Clip.
//
// Notes:
// - Recipes use weights in 0..1; the baker converts to Unity's 0..100 blendshape weights.
// - Only shapes that exist on the target mesh are keyed in the clip.
// - Add more viseme recipes in BuildRecipe() switch-case.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class VisemeClipWindow : EditorWindow
{
    public enum RigType
    {
        CC,
        Rocketbox
    }

    private enum VisemeType
    {
        PBM,
        ShCh,
        W,
        open,
        tBack,
        tRoof,
        tTeeth,
        FV,
        wide,
        face_neutral,
        _045_blink_lf,
        _045_blink_rt,
    }

    private static readonly Dictionary<string, float> Recipe_CC_PBM = new()
    {
        { "B_M_P", 1.0f },
    };

    // ShCh ("shhh") recipe:
    // - Pucker dominant for pursed, rounded lips
    // - Funnel slight to tighten aperture
    // - Press/Tighten add firmness
    // - Close + small Jaw_Up bring teeth together
    // - Contract narrows horizontally
    // - Chin up to support lower lip
    private static readonly Dictionary<string, float> Recipe_CC_ShCh = new()
    {
        { "Ch_J", 0.75f },
    };

    private static readonly Dictionary<string, float> Recipe_CC_W = new()
    {
        { "W_OO", 1.0f },
    };

    private static readonly Dictionary<string, float> Recipe_CC_open = new()
    {
        { "Oh", 1.0f },
    };

    private static readonly Dictionary<string, float> Recipe_CC_tBack = new()
    {
    };

    private static readonly Dictionary<string, float> Recipe_CC_tRoof = new()
    {
    };

    private static readonly Dictionary<string, float> Recipe_CC_tTeeth = new()
    {
    };

    // FV recipe:
    // - Lips and teeth pressed together for “F” or “V” sounds.
    // - Your rig already provides an F_V blendshape, which encapsulates this.
    // - Single shape with moderate intensity (adjust 0.6f if needed).
    private static readonly Dictionary<string, float> Recipe_CC_FV = new()
    {
        { "F_V", 1.0f }
    };

    private static readonly Dictionary<string, float> Recipe_CC_wide = new()
    {
        { "EE", 1.0f },
    };

    // Face_Neutral recipe:
    // - Intended to bring the mouth, lips, jaw, and chin back to a relaxed rest state.
    // - We explicitly zero key mouth shapes to ensure a neutral expression.
    // - Adjust or add any others based on your rig’s available blendshapes.
    private static readonly Dictionary<string, float> Recipe_CC_FaceNeutral = new()
    {
        { "None", 1.0f },
    };

    private static readonly Dictionary<string, float> Recipe_CC_045_blink_lf = new()
    {
        { "Eye_Blink_L", 1.0f },
    };

    private static readonly Dictionary<string, float> Recipe_CC_045_blink_rt = new()
    {
        { "Eye_Blink_R", 1.0f },
    };

    // ------------------------------------------------------------------------------

    private static readonly Dictionary<string, float> Recipe_RB_PBM = new()
    {
        { "blendShape1.AA_VI_01_PP", 1.0f },    // PBM / PP blend
    };

    private static readonly Dictionary<string, float> Recipe_RB_ShCh = new()
    {
        { "blendShape1.AA_VI_06_CH", 1.0f },    // CH shape approximates Sh/Ch
    };

    private static readonly Dictionary<string, float> Recipe_RB_W = new()
    {
        { "blendShape1.AA_VI_02_FF", 1.0f },    // closest to rounded "W" vowel
    };

    private static readonly Dictionary<string, float> Recipe_RB_open = new()
    {
        { "blendShape1.AA_VI_10_aa", 1.0f },    // "ah" / wide open
    };

    private static readonly Dictionary<string, float> Recipe_RB_tBack = new()
    {
    };

    private static readonly Dictionary<string, float> Recipe_RB_tRoof = new()
    {
    };

    private static readonly Dictionary<string, float> Recipe_RB_tTeeth = new()
    {
    };

    private static readonly Dictionary<string, float> Recipe_RB_FV = new()
    {
        { "blendShape1.AA_VI_02_FF", 1.0f },    // FF = F/V lower lip to teeth
    };

    private static readonly Dictionary<string, float> Recipe_RB_wide = new()
    {
        { "blendShape1.AA_VI_11_E", 1.0f },     // "E" vowel, lateral widening
    };

    private static readonly Dictionary<string, float> Recipe_RB_FaceNeutral = new()
    {
        { "blendShape1._Neutral", 1.0f },       // neutral rest pose
    };

    private static readonly Dictionary<string, float> Recipe_RB_045_blink_lf = new()
    {
        { "blendShape1.AK_09_EyeBlinkLeft", 1.0f },
    };

    private static readonly Dictionary<string, float> Recipe_RB_045_blink_rt = new()
    {
        { "blendShape1.AK_10_EyeBlinkRight", 1.0f },
    };


    private GameObject m_characterRoot;
    private RigType m_rigType = RigType.CC;
    private VisemeType m_selectedViseme = VisemeType.face_neutral;
    private string m_outputFileName = "Viseme.anim";
    private bool m_foldoutAdvanced = true;
    private readonly List<string> m_unionBlendshapeNames = new();  // Cache: distinct union of blendshape names across all SMRs

    private string m_outputFolder = "Assets/Art/Animations/CharacterCreatorFace";

    private Dictionary<string, float> m_editableRecipe;
    private Vector2 m_recipeScroll;
    private VisemeType m_lastVisemeInit = (VisemeType)(-1);
    private RigType m_lastRigInit = (RigType)(-1);

    private bool m_includeWeights = false; // optional toggle; names are primary

    private bool m_foldoutBlendshapeBrowser = false;
    private Vector2 m_blendshapeScroll;
    private string m_browserSearch = "";      // quick filter

    private SkinnedMeshRenderer m_cleanupSmr; // target SMR for duplicate cleanup

    private RuntimeAnimatorController m_baseController;
    private bool m_assignToCharacterAnimator = true;


    [MenuItem("Ride/Visemes/Viseme Clip Generator")]
    public static void ShowWindow()
    {
        var win = GetWindow<VisemeClipWindow>("Visemes");
        win.minSize = new Vector2(420, 280);
        win.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Viseme Composer (Single-Frame)", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        var newRoot = (GameObject)EditorGUILayout.ObjectField("Character Root", m_characterRoot, typeof(GameObject), true);
        if (newRoot != m_characterRoot)
        {
            if (m_characterRoot != null)
            {
                m_outputFolder = (m_outputFolder ?? "").Replace("\\", "/").TrimEnd('/');
                if (m_outputFolder.EndsWith(m_characterRoot.name))
                    m_outputFolder = m_outputFolder.Substring(0, m_outputFolder.Length - m_characterRoot.name.Length).TrimEnd('/');
            }

            m_characterRoot = newRoot;

            if (m_characterRoot != null)
            {
                m_outputFolder = (m_outputFolder ?? "").Replace("\\", "/").TrimEnd('/');
                m_outputFolder = $"{m_outputFolder}/{m_characterRoot.name}";
            }
        }

        EditorGUILayout.LabelField("Rig Type", EditorStyles.boldLabel);
        int newRigIndex = GUILayout.Toolbar((int)m_rigType, new[] { "CC (Character Creator)", "Rocketbox" });
        if (newRigIndex != (int)m_rigType)
        {
            m_rigType = (RigType)newRigIndex;
            ClearPreview();
            m_editableRecipe = null;
            m_lastRigInit = (RigType)(-1);
            ApplyPreview();
        }

        EditorGUILayout.Space();
        var selectedViseme = (VisemeType)EditorGUILayout.EnumPopup("Viseme", m_selectedViseme);
        if (selectedViseme != m_selectedViseme)
        {
            m_selectedViseme = selectedViseme;
            ClearPreview();
            ApplyPreview();

            string cleanName = GetVisemeClipName(m_selectedViseme);
            m_outputFileName = $"{cleanName}.anim";
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear Preview", GUILayout.Height(28)))
            ClearPreview();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        m_outputFileName = EditorGUILayout.TextField("Output File Name", m_outputFileName);
        m_outputFolder = EditorGUILayout.TextField("Output Folder", m_outputFolder);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Single-Frame Clip", GUILayout.Height(32)))
            GenerateClip();
        if (GUILayout.Button("Generate All", GUILayout.Height(32)))
            GenerateAllClips();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("Animator Override", EditorStyles.boldLabel);

        m_baseController = (RuntimeAnimatorController)EditorGUILayout.ObjectField("Base Controller", m_baseController, typeof(RuntimeAnimatorController), false);
        m_assignToCharacterAnimator = EditorGUILayout.Toggle("Assign To Character", m_assignToCharacterAnimator);

        GUI.enabled = (m_baseController != null && m_characterRoot != null);
        if (GUILayout.Button("Create Override Controller", GUILayout.Height(28)))
            CreateVisemeOverrideController();

        GUI.enabled = true;

        EditorGUILayout.Space(8);
        m_foldoutAdvanced = EditorGUILayout.Foldout(m_foldoutAdvanced, "Advanced");
        if (m_foldoutAdvanced)
        {
            EnsureEditableRecipe();

            // Buttons row
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset To Defaults", GUILayout.Height(22)))
            {
                ResetEditableRecipeToBase();
                ApplyRecipeToAll(m_editableRecipe);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(4);

            // Sliders list
            m_recipeScroll = EditorGUILayout.BeginScrollView(m_recipeScroll);
            if (m_editableRecipe != null && m_editableRecipe.Count > 0)
            {
                var keys = new List<string>(m_editableRecipe.Keys);
                for (int i = 0; i < keys.Count; i++)
                {
                    string shape = keys[i];
                    float val = m_editableRecipe[shape];

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(shape, GUILayout.Width(220));

                    EditorGUI.BeginChangeCheck();
                    float newVal = EditorGUILayout.Slider(val, 0f, 1f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        m_editableRecipe[shape] = newVal;
                        ApplySingleShapeToAll(shape, newVal); // apply to every SMR that has this shape
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                EditorGUILayout.LabelField("No shapes in this recipe (not implemented yet).");
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("Blendshape Tools", EditorStyles.boldLabel);

            GUI.enabled = m_characterRoot != null;
            m_includeWeights = EditorGUILayout.Toggle("Include Current Weights", m_includeWeights);
            if (GUILayout.Button("Export Blendshape Names From Character"))
                ExportBlendshapesFromCharacter();
            GUI.enabled = true;

            m_foldoutBlendshapeBrowser = EditorGUILayout.Foldout(m_foldoutBlendshapeBrowser, "Blendshape Browser");
            if (m_foldoutBlendshapeBrowser)
            {
                if (m_characterRoot == null)
                {
                    EditorGUILayout.HelpBox("Assign a Character Root.", MessageType.Info);
                }
                else
                {
                    var smrs = GetAllSmrs();
                    if (smrs.Length == 0)
                    {
                        EditorGUILayout.HelpBox("No SkinnedMeshRenderers with blendshapes found under Character Root.", MessageType.Info);
                    }
                    else
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Search", GUILayout.Width(52));
                        string newFilter = EditorGUILayout.TextField(m_browserSearch);
                        EditorGUILayout.EndHorizontal();
                        if (!string.Equals(newFilter, m_browserSearch, StringComparison.Ordinal)) m_browserSearch = newFilter;

                        RebuildUnionBlendshapeNames();

                        // One-time heads-up: Animator can override in Edit mode.
                        var anim = m_characterRoot.GetComponentInChildren<Animator>(true);
                        //if (anim != null && anim.enabled)
                        //{
                        //    EditorGUILayout.HelpBox("Animator is enabled and may override blendshapes in Edit mode.", MessageType.Warning);
                        //}

                        m_blendshapeScroll = EditorGUILayout.BeginScrollView(m_blendshapeScroll, GUILayout.MinHeight(320));
                        string needle = m_browserSearch?.Trim();
                        bool useFilter = !string.IsNullOrEmpty(needle);
                        for (int i = 0; i < m_unionBlendshapeNames.Count; i++)
                        {
                            string shape = m_unionBlendshapeNames[i];
                            if (useFilter && shape.IndexOf(needle, StringComparison.OrdinalIgnoreCase) < 0)
                                continue;

                            // Estimate current % by first SMR that has it (display only; writes go to ALL).
                            float w = 0f;
                            foreach (var smr in smrs)
                            {
                                int idx = smr.sharedMesh.GetBlendShapeIndex(shape);
                                if (idx >= 0) { w = smr.GetBlendShapeWeight(idx); break; }
                            }

                            EditorGUILayout.BeginHorizontal();

                            // Toggle button: jumps between 100 and 0
                            bool isOn = w >= 99.5f;
                            if (GUILayout.Button(isOn ? "0" : "100", GUILayout.Width(46)))
                            {
                                float target01 = isOn ? 0f : 1f;
                                ApplySingleShapeToAll(shape, target01);
                                w = target01 * 100; // update local for the slider that follows
                            }

                            // Name + slider (0..100)
                            EditorGUILayout.LabelField(shape, GUILayout.Width(220));
                            EditorGUI.BeginChangeCheck();
                            float newW = EditorGUILayout.Slider(w, 0f, 100f);
                            if (EditorGUI.EndChangeCheck())
                                ApplySingleShapeToAll(shape, Mathf.Clamp01(newW / 100f));

                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndScrollView();

                        if (GUILayout.Button("Clear All (All SMRs)"))
                            ClearAllBlendshapes();
                    }
                }
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Diagnose Eye/Nose Shapes"))
                DiagnoseAllShapes();
            GUI.enabled = true;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Duplicate Cleanup (Single SMR)", EditorStyles.boldLabel);

            m_cleanupSmr = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Target SMR", m_cleanupSmr, typeof(SkinnedMeshRenderer), true);

            bool hasCleanupTarget = m_cleanupSmr != null && m_cleanupSmr.sharedMesh != null;
            GUI.enabled = hasCleanupTarget;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Inspect Duplicates"))
                InspectDuplicateBlendshapes();

            if (GUILayout.Button("Export Cleaned Mesh"))
                ExportCleanedMeshFromDuplicates();
            EditorGUILayout.EndHorizontal();
        }
    }

    private void ApplyPreview()
    {
        if (m_characterRoot == null)
        {
            Debug.LogError("Preview failed: assign Character Root first.");
            return;
        }

        var recipe = GetActiveRecipe();
        if (recipe == null || recipe.Count == 0)
        {
            Debug.LogWarning("Preview: no shapes in the current recipe.");
            return;
        }

        var smrs = GetAllSmrs();
        if (smrs.Length == 0)
        {
            Debug.LogError("Preview failed: no SkinnedMeshRenderers with blendshapes under Character Root.");
            return;
        }

        var anim = m_characterRoot.GetComponentInChildren<Animator>(true);
        if (anim != null && anim.enabled)
            Debug.LogWarning("Animator is enabled and may override blendshapes in Edit mode.");

        int applied = ApplyRecipeToAll(recipe);

        Debug.Log($"Preview applied: {applied} blendshape weights across {smrs.Length} SMR(s).");
    }

    private void ClearPreview()
    {
        if (m_characterRoot == null)
        {
            Debug.LogError("Clear failed: assign Character Root first.");
            return;
        }
        int cleared = ClearAllBlendshapes();

        Debug.Log($"Preview cleared: {cleared} weights set to 0 across all SMR(s).");
    }

    private AnimationClip GenerateSingleVisemeClip(RigType rigType, VisemeType viseme, Dictionary<string, float> recipeOverride = null)
    {
        if (m_characterRoot == null)
        {
            Debug.LogError("Generate failed: assign Character Root first.");
            return null;
        }

        var smrs = GetAllSmrs();
        if (smrs.Length == 0)
        {
            Debug.LogError("Generate failed: no SkinnedMeshRenderers with blendshapes under Character Root.");
            return null;
        }

        var recipe = recipeOverride ?? BuildRecipe(rigType, viseme);
        if (recipe == null)
        {
            Debug.LogWarning($"Generate: recipe not implemented for {viseme}. Skipping.");
            return null;
        }

        var clip = new AnimationClip { name = GetVisemeClipName(viseme) };

        foreach (var kvp in recipe)
        {
            string shape = kvp.Key;
            float weight100 = Mathf.Clamp01(kvp.Value) * 100f;

            foreach (var smr in smrs)
            {
                int idx = smr.sharedMesh.GetBlendShapeIndex(shape);
                if (idx < 0) continue;

                string path = AnimationUtility.CalculateTransformPath(smr.transform, m_characterRoot.transform);
                var binding = new EditorCurveBinding
                {
                    path = path,
                    type = typeof(SkinnedMeshRenderer),
                    propertyName = "blendShape." + shape
                };

                var curve = new AnimationCurve();
                curve.AddKey(new Keyframe(0f, weight100));     // t=0
                curve.AddKey(new Keyframe(1f/30f, weight100)); // tiny non-zero length to avoid “zero-length” oddities

                // optionally add a toggle to generate a 0 weight frame 0
                //curve.AddKey(new Keyframe(0f, 0f));            // start at 0
                //curve.AddKey(new Keyframe(1f/30f, weight100)); // reach target quickly (one frame)

                AnimationUtility.SetEditorCurve(clip, binding, curve);
            }
        }

        return clip;
    }

    private void GenerateClip()
    {
        var clip = GenerateSingleVisemeClip(m_rigType, m_selectedViseme, GetActiveRecipe());
        if (clip != null)
        {
            string suggestedName = string.IsNullOrWhiteSpace(m_outputFileName)
                ? GetVisemeClipName(m_selectedViseme) + ".anim"
                : m_outputFileName.Trim();

            string defaultDir = GetDefaultSaveDir();
            string savePath = EditorUtility.SaveFilePanelInProject(
                "Save Viseme Clip",
                suggestedName,
                "anim",
                "Choose a location for the viseme AnimationClip",
                defaultDir);

            if (string.IsNullOrEmpty(savePath))
            {
                Debug.Log("Save canceled.");
                return;
            }

            CreateOrReplaceAsset(clip, savePath);
            AssetDatabase.SaveAssets();
            EditorGUIUtility.PingObject(clip);

            Debug.Log($"Created viseme clip: {clip.name}");
        }
    }

    private void GenerateAllClips()
    {
        if (m_characterRoot == null)
        {
            Debug.LogError("GenerateAll failed: assign Character Root first.");
            return;
        }

        string basePath = GetDefaultSaveDir();
        int count = 0;

        foreach (VisemeType viseme in Enum.GetValues(typeof(VisemeType)))
        {
            var defaultRecipe = BuildRecipe(m_rigType, viseme);
            if (defaultRecipe == null)
            {
                Debug.LogWarning($"Skipping {viseme}: no default recipe defined.");
                continue;
            }

            var clip = GenerateSingleVisemeClip(m_rigType, viseme, defaultRecipe);
            if (clip != null)
            {
                string outPath = Path.Combine(basePath, $"{clip.name}.anim");
                CreateOrReplaceAsset(clip, outPath);
                count++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Generated {count} viseme clips in: {basePath}");
    }

    private string GetDefaultSaveDir()
    {
        string folder = (m_outputFolder ?? "").Replace("\\", "/").TrimEnd('/');
        if (string.IsNullOrWhiteSpace(folder))
            folder = "Assets/Visemes";

        if (!folder.StartsWith("Assets/", StringComparison.Ordinal) && folder != "Assets")  // Ensure it starts with Assets/
        {
            Debug.LogWarning($"Output folder \"{folder}\" is not inside Assets/. Forcing into Assets/Visemes.");
            folder = "Assets/Visemes";
        }

        EnsureFolderExists(folder);  // Make sure the folder exists
        return folder;
    }

    private static void EnsureFolderExists(string folder)
    {
        if (AssetDatabase.IsValidFolder(folder)) return;

        if (folder.StartsWith("Assets/"))
        {
            string parent = "Assets";
            string rest = folder.Substring("Assets/".Length);
            string[] parts = rest.Split('/');
            foreach (string p in parts)
            {
                string candidate = parent + "/" + p;
                if (!AssetDatabase.IsValidFolder(candidate))
                {
                    AssetDatabase.CreateFolder(parent, p);
                }
                parent = candidate;
            }
        }
        else if (folder == "Assets")
        {
            // ok
        }
        else
        {
            AssetDatabase.CreateFolder("Assets", "Visemes");
        }
    }

    private static void CreateOrReplaceAsset(AnimationClip clip, string assetPath)
    {
        // Ensure directory exists
        var fullDir = Path.GetDirectoryName(assetPath);
        if (!string.IsNullOrEmpty(fullDir))
            Directory.CreateDirectory(fullDir);

        // Delete existing to guarantee a clean slate
        if (AssetDatabase.LoadAssetAtPath<AnimationClip>(assetPath) != null)
            AssetDatabase.DeleteAsset(assetPath);

        AssetDatabase.CreateAsset(clip, assetPath);
    }

    // Central recipe builder. Return null for not-yet-implemented visemes.
    private static Dictionary<string, float> BuildRecipe(RigType rigType, VisemeType visemeType)
    {
        switch (rigType)
        {
            case RigType.CC:
            {
                switch (visemeType)
                {
                    case VisemeType.PBM: return Recipe_CC_PBM;
                    case VisemeType.ShCh: return Recipe_CC_ShCh;
                    case VisemeType.W: return Recipe_CC_W;
                    case VisemeType.open: return Recipe_CC_open;
                    case VisemeType.tBack: return Recipe_CC_tBack;
                    case VisemeType.tRoof: return Recipe_CC_tRoof;
                    case VisemeType.tTeeth: return Recipe_CC_tTeeth;
                    case VisemeType.FV: return Recipe_CC_FV;
                    case VisemeType.wide: return Recipe_CC_wide;
                    case VisemeType.face_neutral: return Recipe_CC_FaceNeutral;
                    case VisemeType._045_blink_lf: return Recipe_CC_045_blink_lf;
                    case VisemeType._045_blink_rt: return Recipe_CC_045_blink_rt;
                    default: return null;
                }
            }
            case RigType.Rocketbox:
            {
                switch (visemeType)
                {
                    case VisemeType.PBM: return Recipe_RB_PBM;
                    case VisemeType.ShCh: return Recipe_RB_ShCh;
                    case VisemeType.W: return Recipe_RB_W;
                    case VisemeType.open: return Recipe_RB_open;
                    case VisemeType.tBack: return Recipe_RB_tBack;
                    case VisemeType.tRoof: return Recipe_RB_tRoof;
                    case VisemeType.tTeeth: return Recipe_RB_tTeeth;
                    case VisemeType.FV: return Recipe_RB_FV;
                    case VisemeType.wide: return Recipe_RB_wide;
                    case VisemeType.face_neutral: return Recipe_RB_FaceNeutral;
                    case VisemeType._045_blink_lf: return Recipe_RB_045_blink_lf;
                    case VisemeType._045_blink_rt: return Recipe_RB_045_blink_rt;
                    default: return null;
                }
            }
            default:
                return null;
        }
    }

    // ensure we have an editable copy of the current viseme's base recipe
    private void EnsureEditableRecipe()
    {
        if (m_editableRecipe == null || m_lastVisemeInit != m_selectedViseme || m_lastRigInit != m_rigType)
        {
            var baseRecipe = BuildRecipe(m_rigType, m_selectedViseme);
            m_editableRecipe = baseRecipe != null
                ? new Dictionary<string, float>(baseRecipe)
                : new Dictionary<string, float>();
            m_lastVisemeInit = m_selectedViseme;
            m_lastRigInit = m_rigType;
        }
    }

    // reset editable recipe back to the base recipe for the current viseme
    private void ResetEditableRecipeToBase()
    {
        var baseRecipe = BuildRecipe(m_rigType, m_selectedViseme);
        m_editableRecipe = baseRecipe != null
            ? new Dictionary<string, float>(baseRecipe)
            : new Dictionary<string, float>();

        m_lastVisemeInit = m_selectedViseme;
        m_lastRigInit = m_rigType;
    }

    // provide a single point to retrieve the active recipe (edited if available)
    private Dictionary<string, float> GetActiveRecipe()
    {
        EnsureEditableRecipe();
        return m_editableRecipe;
    }

    private void ExportBlendshapesFromCharacter()
    {
        if (m_characterRoot == null)
        {
            EditorUtility.DisplayDialog("Missing Root", "Assign a Character Root first.", "OK");
            return;
        }

        string saveFolder = GetDefaultSaveDir();
        EnsureFolderExists(saveFolder);

        var smrs = m_characterRoot.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        if (smrs == null || smrs.Length == 0)
        {
            EditorUtility.DisplayDialog("No SkinnedMeshRenderers", "No SkinnedMeshRenderer components found under the Character Root.", "OK");
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine("Blendshape Name Report");
        sb.AppendLine($"Character Root: {m_characterRoot.name}");
        sb.AppendLine();

        foreach (var smr in smrs)
        {
            if (smr == null || smr.sharedMesh == null) continue;

            string path = AnimationUtility.CalculateTransformPath(smr.transform, m_characterRoot.transform);
            sb.AppendLine("Renderer: " + smr.name);
            sb.AppendLine("Path: " + (string.IsNullOrEmpty(path) ? "(root)" : path));
            sb.AppendLine("Mesh: " + smr.sharedMesh.name);
            sb.AppendLine("BlendShapeCount: " + smr.sharedMesh.blendShapeCount);

            for (int i = 0; i < smr.sharedMesh.blendShapeCount; i++)
            {
                string shapeName = smr.sharedMesh.GetBlendShapeName(i);
                if (m_includeWeights)
                {
                    float w = 0f;
                    try { w = smr.GetBlendShapeWeight(i); } catch { w = 0f; }
                    sb.AppendLine("  - " + shapeName + "    weight=" + w.ToString("0.##"));
                }
                else
                {
                    sb.AppendLine("  - " + shapeName);
                }
            }
            sb.AppendLine();
        }

        string fileName = $"Blendshapes_{m_characterRoot.name}.txt";
        string assetPath = AssetDatabase.GenerateUniqueAssetPath(saveFolder.TrimEnd('/') + "/" + fileName);
        string fullPath = Path.GetFullPath(assetPath);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        File.WriteAllText(fullPath, sb.ToString(), Encoding.UTF8);

        AssetDatabase.ImportAsset(assetPath);
        AssetDatabase.SaveAssets();

        Debug.Log("Blendshape report written to: " + assetPath);
        EditorUtility.RevealInFinder(fullPath);
    }

    // Return all SkinnedMeshRenderers under the character that have blendshapes.
    private SkinnedMeshRenderer[] GetAllSmrs()
    {
        if (m_characterRoot == null) return Array.Empty<SkinnedMeshRenderer>();
        var smrs = m_characterRoot.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        var list = new List<SkinnedMeshRenderer>(smrs.Length);
        foreach (var smr in smrs)
        {
            if (smr != null && smr.sharedMesh != null && smr.sharedMesh.blendShapeCount > 0)
                list.Add(smr);
        }
        return list.ToArray();
    }

    // Build/update the distinct union of blendshape names across all SMRs.
    private void RebuildUnionBlendshapeNames()
    {
        m_unionBlendshapeNames.Clear();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var smr in GetAllSmrs())
        {
            var mesh = smr.sharedMesh;
            for (int i = 0; i < mesh.blendShapeCount; i++)
            {
                string name = mesh.GetBlendShapeName(i);
                if (seen.Add(name)) m_unionBlendshapeNames.Add(name);
            }
        }
        m_unionBlendshapeNames.Sort(StringComparer.OrdinalIgnoreCase);
    }

    // Apply a single shape value (0..1) to ALL SMRs that contain it.
    private void ApplySingleShapeToAll(string shapeName, float weight01)
    {
        var smrs = GetAllSmrs();
        foreach (var smr in smrs)
        {
            int idx = smr.sharedMesh.GetBlendShapeIndex(shapeName);
            if (idx < 0) continue;
            smr.SetBlendShapeWeight(idx, Mathf.Clamp01(weight01) * 100f);
            Undo.RecordObject(smr, "Apply Blendshape");
            PrefabUtility.RecordPrefabInstancePropertyModifications(smr);
            EditorUtility.SetDirty(smr);
        }
        EditorApplication.QueuePlayerLoopUpdate();
        SceneView.RepaintAll();
    }

    // Apply a whole recipe (0..1 values) to ALL SMRs (only where present).
    private int ApplyRecipeToAll(Dictionary<string, float> recipe)
    {
        if (recipe == null || recipe.Count == 0) return 0;
        int applied = 0;
        var smrs = GetAllSmrs();
        foreach (var smr in smrs)
        {
            foreach (var kv in recipe)
            {
                int idx = smr.sharedMesh.GetBlendShapeIndex(kv.Key);
                if (idx < 0) continue;
                smr.SetBlendShapeWeight(idx, Mathf.Clamp01(kv.Value) * 100f);
                applied++;
            }
            Undo.RecordObject(smr, "Apply Viseme Preview (All SMRs)");
            PrefabUtility.RecordPrefabInstancePropertyModifications(smr);
            EditorUtility.SetDirty(smr);
        }
        EditorApplication.QueuePlayerLoopUpdate();
        SceneView.RepaintAll();
        return applied;
    }

    // Clear all blendshapes on ALL SMRs (set to 0).
    private int ClearAllBlendshapes()
    {
        int cleared = 0;
        var smrs = GetAllSmrs();
        foreach (var smr in smrs)
        {
            var mesh = smr.sharedMesh;
            for (int i = 0; i < mesh.blendShapeCount; i++)
            {
                smr.SetBlendShapeWeight(i, 0f);
                cleared++;
            }
            Undo.RecordObject(smr, "Clear Blendshapes (All SMRs)");
            PrefabUtility.RecordPrefabInstancePropertyModifications(smr);
            EditorUtility.SetDirty(smr);
        }
        EditorApplication.QueuePlayerLoopUpdate();
        SceneView.RepaintAll();
        return cleared;
    }

    private struct ShapeStats
    {
        public int frames;
        public int movedVerts;
        public float maxDelta; // max magnitude across x/y/z
    }

    private static ShapeStats GetShapeStats(SkinnedMeshRenderer smr, int shapeIndex)
    {
        var mesh = smr.sharedMesh;
        var stats = new ShapeStats();
        if (mesh == null || shapeIndex < 0) return stats;

        int frames = mesh.GetBlendShapeFrameCount(shapeIndex);
        stats.frames = frames;
        if (frames <= 0) return stats;

        // Use the last frame (highest weight) as representative
        int vertexCount = mesh.vertexCount;
        var dPos = new Vector3[vertexCount];
        var dNor = new Vector3[vertexCount];
        var dTan = new Vector3[vertexCount];

        mesh.GetBlendShapeFrameVertices(shapeIndex, frames - 1, dPos, dNor, dTan);

        int moved = 0;
        float maxMag = 0f;
        for (int i = 0; i < vertexCount; i++)
        {
            float m = dPos[i].sqrMagnitude;
            if (m > 0f) { moved++; if (m > maxMag) maxMag = m; }
        }

        stats.movedVerts = moved;
        stats.maxDelta = Mathf.Sqrt(maxMag);
        return stats;
    }

    private void DiagnoseAllShapes()
    {
        if (m_characterRoot == null)
        {
            Debug.LogWarning("Assign Character Root first.");
            return;
        }

        var smrs = GetAllSmrs();
        if (smrs.Length == 0)
        {
            Debug.LogWarning("No SMRs with blendshapes under Character Root.");
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine("### Blendshape Delta Diagnostics");
        sb.AppendLine($"CharacterRoot: {m_characterRoot.name}");
        sb.AppendLine($"SMR Count: {smrs.Length}");
        sb.AppendLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine("Format: ShapeName | Renderer | Frames | MovedVerts | MaxDelta");
        sb.AppendLine("-------------------------------------------------------------");

        foreach (string shape in m_unionBlendshapeNames)
        {
            foreach (var smr in smrs)
            {
                int idx = smr.sharedMesh.GetBlendShapeIndex(shape);
                if (idx < 0) continue;

                var stats = GetShapeStats(smr, idx);
                sb.AppendLine($"{shape}|{smr.name}|{stats.frames}|{stats.movedVerts}|{stats.maxDelta:0.######}");
            }
        }

        // Write to file (project root for simplicity)
        string folder = Application.dataPath;
        string file = Path.Combine(folder, $"BlendshapeDiagnostics_{m_characterRoot.name}_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        try
        {
            File.WriteAllText(file, sb.ToString());
            Debug.Log($"Blendshape diagnostics written to:\n{file}");
            EditorUtility.RevealInFinder(file);
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to write diagnostics file: " + ex.Message);
        }
    }

    private void InspectDuplicateBlendshapes()
    {
        if (m_cleanupSmr == null || m_cleanupSmr.sharedMesh == null)
        {
            Debug.LogWarning("Duplicate inspect: assign a Target SMR with a sharedMesh.");
            return;
        }

        var smr = m_cleanupSmr;
        var mesh = smr.sharedMesh;

        int count = mesh.blendShapeCount;
        if (count == 0)
        {
            Debug.LogWarning("Duplicate inspect: mesh has no blendshapes. SMR: " + smr.name);
            return;
        }

        var nameToIndices = new Dictionary<string, List<int>>(StringComparer.Ordinal);
        for (int i = 0; i < count; i++)
        {
            string name = mesh.GetBlendShapeName(i) ?? string.Empty;
            if (!nameToIndices.TryGetValue(name, out var list))
            {
                list = new List<int>();
                nameToIndices[name] = list;
            }
            list.Add(i);
        }

        var sb = new StringBuilder();
        sb.AppendLine("Duplicate blendshape inspection for SMR: " + smr.name);
        sb.AppendLine("Mesh: " + mesh.name);
        sb.AppendLine("BlendShapeCount: " + count);
        sb.AppendLine();

        int duplicateGroups = 0;

        foreach (var kv in nameToIndices)
        {
            string shapeName = kv.Key;
            var indices = kv.Value;
            if (indices.Count <= 1) continue;

            duplicateGroups++;

            sb.AppendLine("Name: \"" + shapeName + "\" has " + indices.Count + " entries:");

            int bestIndex = -1;
            ShapeStats bestStats = new ShapeStats();

            for (int i = 0; i < indices.Count; i++)
            {
                int idx = indices[i];
                var stats = GetShapeStats(smr, idx);
                sb.AppendLine("  index " + idx +
                              " | frames=" + stats.frames +
                              " | movedVerts=" + stats.movedVerts +
                              " | maxDelta=" + stats.maxDelta);

                if (bestIndex < 0)
                {
                    bestIndex = idx;
                    bestStats = stats;
                }
                else
                {
                    bool newIsBetter =
                        stats.movedVerts > bestStats.movedVerts ||
                        (stats.movedVerts == bestStats.movedVerts && stats.maxDelta > bestStats.maxDelta);

                    if (newIsBetter)
                    {
                        bestIndex = idx;
                        bestStats = stats;
                    }
                }
            }

            sb.AppendLine("  -> Recommended keep index: " + bestIndex +
                          " (movedVerts=" + bestStats.movedVerts +
                          ", maxDelta=" + bestStats.maxDelta + ")");
            sb.AppendLine();
        }

        if (duplicateGroups == 0)
        {
            Debug.Log("Duplicate inspect: no duplicate blendshape names found on SMR " + smr.name +
                      " (mesh " + mesh.name + ").");
        }
        else
        {
            Debug.Log(sb.ToString());
        }
    }

    private void ExportCleanedMeshFromDuplicates()
    {
        if (m_cleanupSmr == null || m_cleanupSmr.sharedMesh == null)
        {
            Debug.LogWarning("Export cleaned mesh: assign a Target SMR with a sharedMesh.");
            return;
        }

        var smr = m_cleanupSmr;
        var src = smr.sharedMesh;
        int shapeCount = src.blendShapeCount;
        if (shapeCount == 0)
        {
            Debug.LogWarning("Export cleaned mesh: source mesh has no blendshapes. SMR: " + smr.name);
            return;
        }

        // Decide which index to keep for each name.
        var nameToBestIndex = new Dictionary<string, int>(StringComparer.Ordinal);
        var nameToBestStats = new Dictionary<string, ShapeStats>(StringComparer.Ordinal);

        for (int i = 0; i < shapeCount; i++)
        {
            string name = src.GetBlendShapeName(i) ?? string.Empty;
            var stats = GetShapeStats(smr, i);

            if (!nameToBestIndex.TryGetValue(name, out int bestIndex))
            {
                nameToBestIndex[name] = i;
                nameToBestStats[name] = stats;
            }
            else
            {
                var bestStats = nameToBestStats[name];
                bool newIsBetter =
                    stats.movedVerts > bestStats.movedVerts ||
                    (stats.movedVerts == bestStats.movedVerts && stats.maxDelta > bestStats.maxDelta);

                if (newIsBetter)
                {
                    nameToBestIndex[name] = i;
                    nameToBestStats[name] = stats;
                }
            }
        }

        // Build a new mesh that copies geometry but only the chosen blendshapes.
        Mesh dst = new Mesh();
        dst.name = src.name + "_Cleaned";

        dst.indexFormat = src.indexFormat;

        dst.vertices = src.vertices;
        dst.normals = src.normals;
        dst.tangents = src.tangents;
        dst.colors = src.colors;

        dst.uv = src.uv;
        dst.uv2 = src.uv2;
        dst.uv3 = src.uv3;
        dst.uv4 = src.uv4;
        dst.uv5 = src.uv5;
        dst.uv6 = src.uv6;
        dst.uv7 = src.uv7;
        dst.uv8 = src.uv8;

        dst.bindposes = src.bindposes;
        dst.boneWeights = src.boneWeights;
        dst.bounds = src.bounds;

        dst.subMeshCount = src.subMeshCount;
        for (int sm = 0; sm < src.subMeshCount; sm++)
        {
            dst.SetTriangles(src.GetTriangles(sm), sm);
        }

        int vertexCount = src.vertexCount;
        var dPos = new Vector3[vertexCount];
        var dNor = new Vector3[vertexCount];
        var dTan = new Vector3[vertexCount];

        // Preserve original order as much as possible: walk source indices and add if this
        // index is the chosen one for its name.
        var addedNames = new HashSet<string>(StringComparer.Ordinal);
        for (int i = 0; i < shapeCount; i++)
        {
            string name = src.GetBlendShapeName(i) ?? string.Empty;
            if (!nameToBestIndex.TryGetValue(name, out int bestIndex)) continue;
            if (bestIndex != i) continue;         // not the chosen one
            if (!addedNames.Add(name)) continue;  // already added

            int frames = src.GetBlendShapeFrameCount(i);
            for (int f = 0; f < frames; f++)
            {
                float w = src.GetBlendShapeFrameWeight(i, f);
                src.GetBlendShapeFrameVertices(i, f, dPos, dNor, dTan);
                dst.AddBlendShapeFrame(name, w, dPos, dNor, dTan);
            }
        }

        // Save as a new asset next to the source mesh (or in Assets as a fallback).
        string srcPath = AssetDatabase.GetAssetPath(src);
        string folder = string.IsNullOrEmpty(srcPath)
            ? "Assets"
            : Path.GetDirectoryName(srcPath);

        string assetName = dst.name + ".asset";
        string assetPath = AssetDatabase.GenerateUniqueAssetPath(
            folder.TrimEnd('/', '\\') + "/" + assetName);

        AssetDatabase.CreateAsset(dst, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // Assign to the SMR.
        Undo.RecordObject(smr, "Assign cleaned mesh");
        smr.sharedMesh = dst;
        PrefabUtility.RecordPrefabInstancePropertyModifications(smr);
        EditorUtility.SetDirty(smr);

        Debug.Log("Export cleaned mesh: created and assigned " + assetPath +
                  " to SMR " + smr.name + " (was mesh " + src.name + ").");
    }

    private void CreateVisemeOverrideController()
    {
        if (m_characterRoot == null)
        {
            Debug.LogError("Override creation failed: assign Character Root.");
            return;
        }

        if (m_baseController == null)
        {
            Debug.LogError("Override creation failed: assign Base Animator Controller.");
            return;
        }

        string folder = GetDefaultSaveDir();
        EnsureFolderExists(folder);

        // Create override controller
        var overrideCtrl = new AnimatorOverrideController();
        overrideCtrl.runtimeAnimatorController = m_baseController;

        // Gather all clips inside the controller
        var originalClips = overrideCtrl.runtimeAnimatorController.animationClips;
        var overridePairs = new List<KeyValuePair<AnimationClip, AnimationClip>>();

        foreach (var origClip in originalClips)
        {
            if (origClip == null) continue;

            // If clip name starts with a digit, prefix "_" so it matches enum naming.
            string origNameModified = origClip.name;
            if (!string.IsNullOrEmpty(origNameModified) && char.IsDigit(origNameModified[0]))
                origNameModified = $"_{origNameModified}";

            // We assume the clip name matches a VisemeType enum.
            // If not, keep the original clip.
            if (Enum.TryParse(origNameModified, out VisemeType viseme))
            {
                string cleanName = GetVisemeClipName(viseme);

                string path = Path.Combine(m_outputFolder, $"{cleanName}.anim").Replace("\\", "/");

                var replacement = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
                if (replacement != null)
                {
                    overridePairs.Add(new KeyValuePair<AnimationClip, AnimationClip>(origClip, replacement));
                }
                else
                {
                    Debug.LogWarning($"Override: Viseme clip not found: {path}. Using original.");
                    overridePairs.Add(new KeyValuePair<AnimationClip, AnimationClip>(origClip, origClip));
                }
            }
            else
            {
                // Names don't match: pass through unchanged
                overridePairs.Add(new KeyValuePair<AnimationClip, AnimationClip>(origClip, origClip));
            }
        }

        overrideCtrl.ApplyOverrides(overridePairs);

        // Save it
        string saveName = $"{m_characterRoot.name}AnimatorControllerOverride.overrideController";
        string savePath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(folder, saveName));

        AssetDatabase.CreateAsset(overrideCtrl, savePath);
        AssetDatabase.SaveAssets();

        Debug.Log($"Created AnimatorOverrideController:\n{savePath}");

        // Optional: assign it directly to the character Animator
        if (m_assignToCharacterAnimator)
        {
            var anim = m_characterRoot.GetComponentInChildren<Animator>(true);
            if (anim != null)
            {
                Undo.RecordObject(anim, "Assign Animator Override");
                anim.runtimeAnimatorController = overrideCtrl;
                EditorUtility.SetDirty(anim);
                Debug.Log("Assigned override to character Animator.");
            }
            else
            {
                Debug.LogWarning("No Animator found on character to assign override.");
            }
        }

        EditorGUIUtility.PingObject(overrideCtrl);
    }

    public void RunForCharacter(GameObject root,
        RigType rigType,
        RuntimeAnimatorController baseController,
        string outputFolder,
        bool assignToAnimator)
    {
        m_characterRoot = root;
        m_rigType = rigType;
        m_baseController = baseController;
        m_outputFolder = outputFolder;
        m_assignToCharacterAnimator = assignToAnimator;

        GenerateAllClips();
        CreateVisemeOverrideController();
    }

    private static string GetVisemeClipName(VisemeType viseme)
    {
        string name = viseme.ToString();
        if (name.StartsWith("_"))
            name = name.Substring(1);
        return name;
    }
}
