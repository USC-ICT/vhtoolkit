using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Ride.VH
{
    /// <summary>
    /// Editor window that compares two Humanoid avatar definitions as imported by Unity.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This tool inspects the humanoid mapping data stored in <see cref="ModelImporter.humanDescription"/> for two assets
    /// (typically FBX files) and renders a structured diff of:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Skeleton bone transforms (path, position, rotation, scale).</description></item>
    /// <item><description>Human bone mapping (HumanBodyBones name -> skeleton bone name).</description></item>
    /// <item><description>Human bone limit settings (min/max/center/useDefault).</description></item>
    /// <item><description>Humanoid tuning values (arm/leg stretch, twists, feet spacing, translation DoF).</description></item>
    /// </list>
    /// <para>
    /// The window can also copy Avatar A import settings to Avatar B (import settings only).
    /// This is intended for debugging and content pipeline triage, not runtime use.
    /// </para>
    /// <para>
    /// Implementation detail: this tool reads import settings via <see cref="AssetImporter.GetAtPath(string)"/> and therefore
    /// operates on the asset database state. It does not require instantiating a character in-scene.
    /// </para>
    /// </remarks>
    public sealed class AvatarDefinitionDiffWindow : EditorWindow
    {
        private UnityEngine.Object _aAsset;
        private UnityEngine.Object _bAsset;

        private Snapshot _a;
        private Snapshot _b;

        private Vector2 _scroll;
        private bool _showSummary = true;
        private bool _showWarnings = true;
        private bool _showBoneMappingDiffs = true;
        private bool _showBoneLimitsDiffs = true;
        private bool _showTuningDiffs = true;
        private bool _showSkeletonDiffs = true;
        private bool _showCCSpecifics = true;

        private bool _showOnlyDiffs = false;
        private int _summaryMaxItems = 25;

        private bool _showHumanMapBody = true;
        private bool _showHumanMapHead = true;
        private bool _showHumanMapLeftHand = true;
        private bool _showHumanMapRightHand = true;
        private bool _showHumanMapOther = false;

        private const float FloatEpsilon = 0.0005f;
        private const float AngleEpsilonDeg = 0.05f;
        private const float PosEpsilon = 0.0005f;
        private const float ScaleEpsilon = 0.0005f;


        [MenuItem("Ride/Avatars/Humanoid Avatar Diff")]
        public static void ShowWindow()
        {
            var w = GetWindow<AvatarDefinitionDiffWindow>("Humanoid Avatar Diff");
            w.minSize = new Vector2(900f, 500f);
            w.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Pick two FBX assets (or any asset imported by ModelImporter).", EditorStyles.boldLabel);

                using (new EditorGUILayout.HorizontalScope())
                {
                    _aAsset = EditorGUILayout.ObjectField("Avatar A", _aAsset, typeof(UnityEngine.Object), false);
                    _bAsset = EditorGUILayout.ObjectField("Avatar B", _bAsset, typeof(UnityEngine.Object), false);
                }

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Load / Refresh", GUILayout.Height(24)))
                    {
                        _a = Snapshot.TryCreate(_aAsset);
                        _b = Snapshot.TryCreate(_bAsset);
                    }

                    _showOnlyDiffs = GUILayout.Toggle(_showOnlyDiffs, "Show Only diffs", GUILayout.Height(24));

                    GUI.enabled = _a.IsValid && _b.IsValid;
                    if (GUILayout.Button("Copy A -> B (import settings)", GUILayout.Height(24)))
                        CopyAToB();
                    GUI.enabled = true;
                }

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Collapse All Sections"))
                    {
                        _showSummary = false;
                        _showWarnings = false;
                        _showBoneMappingDiffs = false;
                        _showBoneLimitsDiffs = false;
                        _showTuningDiffs = false;
                        _showSkeletonDiffs = false;
                        _showCCSpecifics = false;
                    }

                    if (GUILayout.Button("Expand All Sections"))
                    {
                        _showSummary = true;
                        _showWarnings = true;
                        _showBoneMappingDiffs = true;
                        _showBoneLimitsDiffs = true;
                        _showTuningDiffs = true;
                        _showSkeletonDiffs = true;
                        _showCCSpecifics = true;
                    }
                }
            }

            EditorGUILayout.Space();

            if (!_a.IsValid || !_b.IsValid)
            {
                EditorGUILayout.HelpBox("Select two assets and click Load / Refresh.", MessageType.Info);
                return;
            }

            using (var scrollViewScope = new EditorGUILayout.ScrollViewScope(_scroll))
            {
                _scroll = scrollViewScope.scrollPosition;

                DrawHeader();
                DrawCompactSummary();
                DrawWarnings();
                DrawHumanBoneMappingDiff();
                DrawHumanBoneLimitsDiff();
                DrawTuningDiff();
                DrawSkeletonPoseDiff();
                DrawCCSpecificIssues();
            }
        }

        private void DrawCompactSummary()
        {
            _showSummary = EditorGUILayout.Foldout(_showSummary, "Compact Summary (largest differences first)", true);
            if (!_showSummary) return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Score is magnitude relative to the diff threshold. Higher usually means more likely to matter.", EditorStyles.miniLabel);

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Label("Top", GUILayout.Width(28f));
                    _summaryMaxItems = Mathf.Clamp(EditorGUILayout.IntField(_summaryMaxItems, GUILayout.Width(50f)), 5, 200);
                }

                var items = new List<SummaryItem>(256);
                AddFloat(items, "Tuning", "armStretch", _a.ArmStretch, _b.ArmStretch, FloatEpsilon);
                AddFloat(items, "Tuning", "legStretch", _a.LegStretch, _b.LegStretch, FloatEpsilon);
                AddFloat(items, "Tuning", "upperArmTwist", _a.UpperArmTwist, _b.UpperArmTwist, FloatEpsilon);
                AddFloat(items, "Tuning", "lowerArmTwist", _a.LowerArmTwist, _b.LowerArmTwist, FloatEpsilon);
                AddFloat(items, "Tuning", "upperLegTwist", _a.UpperLegTwist, _b.UpperLegTwist, FloatEpsilon);
                AddFloat(items, "Tuning", "lowerLegTwist", _a.LowerLegTwist, _b.LowerLegTwist, FloatEpsilon);
                AddFloat(items, "Tuning", "feetSpacing", _a.FeetSpacing, _b.FeetSpacing, FloatEpsilon);
                AddBool(items, "Tuning", "hasTranslationDoF", _a.HasTranslationDoF, _b.HasTranslationDoF);

                // Human mapping.
                {
                    var keys = new HashSet<string>(_a.HumanBones.Keys);
                    keys.UnionWith(_b.HumanBones.Keys);
                    foreach (string humanName in keys)
                    {
                        _a.HumanBones.TryGetValue(humanName, out HumanBone aInfo);
                        _b.HumanBones.TryGetValue(humanName, out HumanBone bInfo);

                        if (string.Equals(aInfo.boneName, bInfo.boneName, StringComparison.Ordinal))
                            continue;

                        items.Add(new SummaryItem(
                            "Mapping",
                            humanName,
                            string.IsNullOrEmpty(aInfo.boneName) ? "(none)" : aInfo.boneName,
                            string.IsNullOrEmpty(bInfo.boneName) ? "(none)" : bInfo.boneName,
                            "DIFF",
                            999f));
                    }
                }

                // Human limits.
                {
                    var keys = new HashSet<string>(_a.HumanBones.Keys);
                    keys.UnionWith(_b.HumanBones.Keys);
                    foreach (string humanName in keys)
                    {
                        _a.HumanBones.TryGetValue(humanName, out HumanBone aInfo);
                        _b.HumanBones.TryGetValue(humanName, out HumanBone bInfo);

                        AddV3(items, "Limits", humanName + ".min", aInfo.limit.min, bInfo.limit.min, FloatEpsilon);
                        AddV3(items, "Limits", humanName + ".max", aInfo.limit.max, bInfo.limit.max, FloatEpsilon);
                        AddV3(items, "Limits", humanName + ".center", aInfo.limit.center, bInfo.limit.center, FloatEpsilon);
                        AddBool(items, "Limits", humanName + ".useDefault", aInfo.limit.useDefaultValues, bInfo.limit.useDefaultValues);
                    }
                }

                // Skeleton pose.
                {
                    var keys = new HashSet<string>(_a.Skeleton.Keys);
                    keys.UnionWith(_b.Skeleton.Keys);

                    foreach (string path in keys)
                    {
                        _a.Skeleton.TryGetValue(path, out SkeletonBone aInfo);
                        _b.Skeleton.TryGetValue(path, out SkeletonBone bInfo);

                        float posMag = (aInfo.position - bInfo.position).magnitude;
                        float sclMag = (aInfo.scale - bInfo.scale).magnitude;
                        float angDeg = Quaternion.Angle(aInfo.rotation, bInfo.rotation);

                        if (posMag > PosEpsilon)
                            items.Add(new SummaryItem("Skeleton", path + ".pos", FormatV3(aInfo.position), FormatV3(bInfo.position),
                                "d=" + posMag.ToString("0.#####", CultureInfo.InvariantCulture), posMag / PosEpsilon));

                        if (angDeg > AngleEpsilonDeg)
                            items.Add(new SummaryItem("Skeleton", path + ".rot", FormatEulerDeg(aInfo.rotation), FormatEulerDeg(bInfo.rotation),
                                "d=" + angDeg.ToString("0.###", CultureInfo.InvariantCulture) + " deg", angDeg / AngleEpsilonDeg));

                        if (sclMag > ScaleEpsilon)
                            items.Add(new SummaryItem("Skeleton", path + ".scl", FormatV3(aInfo.scale), FormatV3(bInfo.scale),
                                "d=" + sclMag.ToString("0.#####", CultureInfo.InvariantCulture), sclMag / ScaleEpsilon));
                    }
                }

                if (_showOnlyDiffs)
                    items = items.Where(i => i.Score > 0f).ToList();

                items.Sort((x, y) => y.Score.CompareTo(x.Score));

                int count = Math.Min(_summaryMaxItems, items.Count);
                if (count == 0)
                {
                    EditorGUILayout.LabelField("No differences found.");
                    return;
                }

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("Category", EditorStyles.miniBoldLabel, GUILayout.Width(80f));
                    EditorGUILayout.LabelField("Field", EditorStyles.miniBoldLabel, GUILayout.Width(260f));
                    EditorGUILayout.LabelField("A", EditorStyles.miniBoldLabel, GUILayout.Width(260f));
                    EditorGUILayout.LabelField("B", EditorStyles.miniBoldLabel, GUILayout.Width(260f));
                    EditorGUILayout.LabelField("Delta", EditorStyles.miniBoldLabel, GUILayout.Width(110f));
                }

                for (int i = 0; i < count; i++)
                {
                    SummaryItem it = items[i];
                    Color c = Styles.GetSeverityColor(it.Score);

                    using (new EditorGUILayout.HorizontalScope())
                    using (new GUIColorScope(c))
                    {
                        EditorGUILayout.LabelField(it.Category, GUILayout.Width(80f));
                        EditorGUILayout.LabelField(it.Name, GUILayout.Width(260f));
                        EditorGUILayout.LabelField(it.A, GUILayout.Width(260f));
                        EditorGUILayout.LabelField(it.B, GUILayout.Width(260f));
                        EditorGUILayout.LabelField(it.Delta, GUILayout.Width(110f));
                    }
                }
            }
        }

        private static void AddFloat(List<SummaryItem> items, string cat, string name, float a, float b, float eps)
        {
            float d = Math.Abs(a - b);
            if (d <= eps)
                return;

            items.Add(new SummaryItem(
                cat,
                name,
                a.ToString("0.#####", CultureInfo.InvariantCulture),
                b.ToString("0.#####", CultureInfo.InvariantCulture),
                "d=" + d.ToString("0.#####", CultureInfo.InvariantCulture),
                d / eps));
        }

        private static void AddBool(List<SummaryItem> items, string cat, string name, bool a, bool b)
        {
            if (a == b) return;
            items.Add(new SummaryItem(cat, name, a ? "true" : "false", b ? "true" : "false", "DIFF", 999f));
        }

        private static void AddV3(List<SummaryItem> items, string cat, string name, Vector3 a, Vector3 b, float eps)
        {
            Vector3 d = a - b;
            float mag = d.magnitude;
            if (mag <= eps)
                return;
            items.Add(new SummaryItem(cat, name, FormatV3(a), FormatV3(b), "d=" + mag.ToString("0.#####", CultureInfo.InvariantCulture), mag / eps));
        }

        private readonly struct SummaryItem
        {
            public readonly string Category;
            public readonly string Name;
            public readonly string A;
            public readonly string B;
            public readonly string Delta;
            public readonly float Score;

            public SummaryItem(string category, string name, string a, string b, string delta, float score)
            {
                Category = category ?? string.Empty;
                Name = name ?? string.Empty;
                A = a ?? string.Empty;
                B = b ?? string.Empty;
                Delta = delta ?? string.Empty;
                Score = score;
            }
        }

        private readonly struct DiagnosisItem
        {
            public readonly string Title;
            public readonly string Details;
            public readonly string SuggestedAction;
            public readonly float Severity;

            public DiagnosisItem(string title, string details, string suggestedAction, float severity)
            {
                Title = title ?? string.Empty;
                Details = details ?? string.Empty;
                SuggestedAction = suggestedAction ?? string.Empty;
                Severity = severity;
            }
        }

        private void DrawHeader()
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("A", EditorStyles.boldLabel);
                EditorGUILayout.LabelField(_a.AssetPath);
                EditorGUILayout.LabelField(_a.SummaryLine);
                EditorGUILayout.LabelField(_a.TPoseSummaryLine);

                EditorGUILayout.Space(6f);

                EditorGUILayout.LabelField("B", EditorStyles.boldLabel);
                EditorGUILayout.LabelField(_b.AssetPath);
                EditorGUILayout.LabelField(_b.SummaryLine);
                EditorGUILayout.LabelField(_b.TPoseSummaryLine);
            }
        }

        private void DrawWarnings()
        {
            _showWarnings = EditorGUILayout.Foldout(_showWarnings, "Warnings / Checks", true);
            if (!_showWarnings) return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                var warnings = new List<string>();
                warnings.AddRange(_a.GetSanityWarnings("A"));
                warnings.AddRange(_b.GetSanityWarnings("B"));

                if (warnings.Count == 0)
                {
                    EditorGUILayout.LabelField("No obvious issues detected.");
                    return;
                }

                foreach (string w in warnings)
                    EditorGUILayout.LabelField("- " + w, EditorStyles.wordWrappedLabel);
            }
        }

        private void DrawCCSpecificIssues()
        {
            _showCCSpecifics = EditorGUILayout.Foldout(_showCCSpecifics, "CC / CC5 specific issues", true);
            if (!_showCCSpecifics) return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField(
                    "Heuristics only. This section looks for common CC / CC5 mismatch patterns (UpperChest presence, shoulder shifts, twist mapping differences).",
                    EditorStyles.wordWrappedMiniLabel);

                List<DiagnosisItem> items = CCDiagnoser.Diagnose(_a, _b);

                if (items.Count == 0)
                {
                    EditorGUILayout.LabelField("No common CC / CC5 mismatch patterns detected.");
                    return;
                }

                foreach (DiagnosisItem it in items.OrderByDescending(i => i.Severity))
                {
                    Color c = Styles.GetSeverityColor(it.Severity);
                    using (new GUIColorScope(c))
                        EditorGUILayout.LabelField("- " + it.Title, EditorStyles.wordWrappedLabel);

                    if (!string.IsNullOrEmpty(it.Details))
                        EditorGUILayout.LabelField("  " + it.Details, EditorStyles.wordWrappedMiniLabel);

                    if (!string.IsNullOrEmpty(it.SuggestedAction))
                        EditorGUILayout.LabelField("  Suggest: " + it.SuggestedAction, EditorStyles.wordWrappedMiniLabel);

                    EditorGUILayout.Space(2f);
                }
            }
        }

        private void DrawTuningDiff()
        {
            _showTuningDiffs = EditorGUILayout.Foldout(_showTuningDiffs, "Humanoid Tuning (Muscles & Settings)", true);
            if (!_showTuningDiffs) return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                DrawDiffRow("armStretch", _a.ArmStretch, _b.ArmStretch, FloatEpsilon);
                DrawDiffRow("legStretch", _a.LegStretch, _b.LegStretch, FloatEpsilon);
                DrawDiffRow("upperArmTwist", _a.UpperArmTwist, _b.UpperArmTwist, FloatEpsilon);
                DrawDiffRow("lowerArmTwist", _a.LowerArmTwist, _b.LowerArmTwist, FloatEpsilon);
                DrawDiffRow("upperLegTwist", _a.UpperLegTwist, _b.UpperLegTwist, FloatEpsilon);
                DrawDiffRow("lowerLegTwist", _a.LowerLegTwist, _b.LowerLegTwist, FloatEpsilon);
                DrawDiffRow("feetSpacing", _a.FeetSpacing, _b.FeetSpacing, FloatEpsilon);
                DrawDiffRow("hasTranslationDoF", _a.HasTranslationDoF, _b.HasTranslationDoF);
            }
        }

        private void DrawHumanBoneMappingDiff()
        {
            _showBoneMappingDiffs = EditorGUILayout.Foldout(_showBoneMappingDiffs, "Human Bone Mapping", true);
            if (!_showBoneMappingDiffs) return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Collapse All", GUILayout.Width(110f)))
                    {
                        _showHumanMapBody = false;
                        _showHumanMapHead = false;
                        _showHumanMapLeftHand = false;
                        _showHumanMapRightHand = false;
                        _showHumanMapOther = false;
                    }

                    if (GUILayout.Button("Expand All", GUILayout.Width(110f)))
                    {
                        _showHumanMapBody = true;
                        _showHumanMapHead = true;
                        _showHumanMapLeftHand = true;
                        _showHumanMapRightHand = true;
                        _showHumanMapOther = true;
                    }
                }

                EditorGUILayout.Space(4f);

                var remaining = new HashSet<string>(_a.HumanBones.Keys);
                remaining.UnionWith(_b.HumanBones.Keys);

                DrawHumanBoneMappingGroup("Body", ref _showHumanMapBody, s_HumanMapBodyOrder, remaining);
                DrawHumanBoneMappingGroup("Head", ref _showHumanMapHead, s_HumanMapHeadOrder, remaining);
                DrawHumanBoneMappingGroup("Left Hand", ref _showHumanMapLeftHand, s_HumanMapLeftHandOrder, remaining);
                DrawHumanBoneMappingGroup("Right Hand", ref _showHumanMapRightHand, s_HumanMapRightHandOrder, remaining);

                if (remaining.Count > 0)
                    DrawHumanBoneMappingGroup("Other", ref _showHumanMapOther, remaining.OrderBy(s => s).ToArray(), remaining);
            }
        }

        // Order is intended to mirror Unity's Avatar Configuration tabs.
        private static readonly string[] s_HumanMapBodyOrder =
        {
            // Body.
            "Hips",
            "Spine",
            "Chest",
            "UpperChest",
            "",

            // Left Arm.
            "LeftShoulder",
            "LeftUpperArm",
            "LeftLowerArm",
            "LeftHand",
            "",

            // Right Arm.
            "RightShoulder",
            "RightUpperArm",
            "RightLowerArm",
            "RightHand",
            "",

            // Left Leg.
            "LeftUpperLeg",
            "LeftLowerLeg",
            "LeftFoot",
            "LeftToes",
            "",

            // Right Leg.
            "RightUpperLeg",
            "RightLowerLeg",
            "RightFoot",
            "RightToes",
        };

        private static readonly string[] s_HumanMapHeadOrder =
        {
            "Neck",
            "Head",
            "LeftEye",
            "RightEye",
            "Jaw",
        };

        private static readonly string[] s_HumanMapLeftHandOrder =
        {
            "Left Thumb Proximal",
            "Left Thumb Intermediate",
            "Left Thumb Distal",

            "Left Index Proximal",
            "Left Index Intermediate",
            "Left Index Distal",

            "Left Middle Proximal",
            "Left Middle Intermediate",
            "Left Middle Distal",

            "Left Ring Proximal",
            "Left Ring Intermediate",
            "Left Ring Distal",

            "Left Little Proximal",
            "Left Little Intermediate",
            "Left Little Distal",
        };

        private static readonly string[] s_HumanMapRightHandOrder =
        {
            "Right Thumb Proximal",
            "Right Thumb Intermediate",
            "Right Thumb Distal",

            "Right Index Proximal",
            "Right Index Intermediate",
            "Right Index Distal",

            "Right Middle Proximal",
            "Right Middle Intermediate",
            "Right Middle Distal",

            "Right Ring Proximal",
            "Right Ring Intermediate",
            "Right Ring Distal",

            "Right Little Proximal",
            "Right Little Intermediate",
            "Right Little Distal",
        };

        private void DrawHumanBoneMappingGroup(string title, ref bool foldout, IReadOnlyList<string> orderedHumanNames, HashSet<string> remaining)
        {
            EditorGUILayout.Space(2f);
            foldout = EditorGUILayout.Foldout(foldout, title, true);
            if (!foldout)
                return;

            EditorGUI.indentLevel++;
            try
            {
                for (int i = 0; i < orderedHumanNames.Count; i++)
                {
                    string humanName = orderedHumanNames[i];

                    if (string.IsNullOrEmpty(humanName)) { EditorGUILayout.Space(6f); continue; }

                    bool isKnown = remaining.Contains(humanName);

                    // Always consume the key so it doesn't show up again in "Other".
                    if (isKnown)
                        remaining.Remove(humanName);

                    // If neither avatar even contains this entry, only show it when not filtered.
                    if (!isKnown && _showOnlyDiffs)
                        continue;

                    _a.HumanBones.TryGetValue(humanName, out HumanBone aInfo);
                    _b.HumanBones.TryGetValue(humanName, out HumanBone bInfo);

                    bool differs =
                        !string.Equals(aInfo.boneName, bInfo.boneName, StringComparison.Ordinal) ||
                        !string.Equals(aInfo.humanName, bInfo.humanName, StringComparison.Ordinal);

                    if (_showOnlyDiffs && !differs)
                        continue;

                    DrawHumanBoneMappingRow(humanName, aInfo, bInfo, differs);
                }
            }
            finally
            {
                EditorGUI.indentLevel--;
            }
        }

        private void DrawHumanBoneMappingRow(string humanName, HumanBone aInfo, HumanBone bInfo, bool differs)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUIStyle left = differs ? Styles.DiffLeft : EditorStyles.label;
                GUIStyle right = differs ? Styles.DiffRight : EditorStyles.label;

                bool isRequired = IsRequiredHumanoidBone(humanName);
                Color prev = GUI.color;
                if (!isRequired)
                    GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 0.7f);

                EditorGUILayout.LabelField(humanName, GUILayout.Width(220f));
                GUI.color = prev;

                EditorGUILayout.LabelField(string.IsNullOrEmpty(aInfo.boneName) ? "(none)" : aInfo.boneName, left, GUILayout.Width(300f));
                EditorGUILayout.LabelField(string.IsNullOrEmpty(bInfo.boneName) ? "(none)" : bInfo.boneName, right, GUILayout.Width(300f));
            }
        }

        /// <summary>
        /// Draws a compact side-by-side comparison of <see cref="HumanBone.limit"/> values
        /// between two avatars. Each row represents a single bone, showing its
        /// min/max/center values and whether it uses default limits.
        ///
        /// Note: These limits do <b>not</b> correspond 1:1 with the "Muscles & Settings"
        /// tab in Unity’s Avatar Configuration window. Unity’s UI displays derived
        /// muscle limits, while this view shows the raw limit data actually stored
        /// inside the <see cref="Avatar.humanDescription"/> definition. This section
        /// is retained here for completeness, since these are the values Unity serializes.
        /// </summary>
        private void DrawHumanBoneLimitsDiff()
        {
            _showBoneLimitsDiffs = EditorGUILayout.Foldout(_showBoneLimitsDiffs, "Human Bone Limits", true);
            if (!_showBoneLimitsDiffs) return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.HelpBox(
                    "These are the raw HumanBone.limit values stored in the Avatar definition. " +
                    "They do NOT correspond 1:1 with the 'Muscles & Settings' tab in Unity’s Avatar Configuration window, " +
                    "which displays derived muscle limits. This section shows the serialized data Unity stores internally.",
                    MessageType.Info);

                var keys = new HashSet<string>(_a.HumanBones.Keys);
                keys.UnionWith(_b.HumanBones.Keys);

                foreach (string humanName in keys.OrderBy(s => s))
                {
                    _a.HumanBones.TryGetValue(humanName, out HumanBone aInfo);
                    _b.HumanBones.TryGetValue(humanName, out HumanBone bInfo);

                    bool differs =
                        !Approximately(aInfo.limit.min, bInfo.limit.min, FloatEpsilon) ||
                        !Approximately(aInfo.limit.max, bInfo.limit.max, FloatEpsilon) ||
                        !Approximately(aInfo.limit.center, bInfo.limit.center, FloatEpsilon) ||
                        aInfo.limit.useDefaultValues != bInfo.limit.useDefaultValues;

                    if (_showOnlyDiffs && !differs)
                        continue;

                    //EditorGUILayout.Space(4f);
                    //EditorGUILayout.LabelField(humanName, EditorStyles.boldLabel);

                    //DrawLimitLine("useDefault", aInfo.limit.useDefaultValues.ToString(), bInfo.limit.useDefaultValues.ToString(), aInfo.limit.useDefaultValues != bInfo.limit.useDefaultValues);

                    float minMag = (aInfo.limit.min - bInfo.limit.min).magnitude;
                    float maxMag = (aInfo.limit.max - bInfo.limit.max).magnitude;
                    float cenMag = (aInfo.limit.center - bInfo.limit.center).magnitude;

                    //DrawLimitLine("min", FormatV3(aInfo.limit.min), FormatV3(bInfo.limit.min),
                    //    "d=" + minMag.ToString("0.#####", CultureInfo.InvariantCulture),
                    //    minMag / FloatEpsilon, minMag > FloatEpsilon);

                    //DrawLimitLine("max", FormatV3(aInfo.limit.max), FormatV3(bInfo.limit.max),
                    //    "d=" + maxMag.ToString("0.#####", CultureInfo.InvariantCulture),
                    //    maxMag / FloatEpsilon, maxMag > FloatEpsilon);

                    //DrawLimitLine("center", FormatV3(aInfo.limit.center), FormatV3(bInfo.limit.center),
                    //    "d=" + cenMag.ToString("0.#####", CultureInfo.InvariantCulture),
                    //    cenMag / FloatEpsilon, cenMag > FloatEpsilon);

                    string aStr = FormatLimit(aInfo.limit);
                    string bStr = FormatLimit(bInfo.limit);
                    string diffStr = GetLimitDiff(aInfo.limit, bInfo.limit);

                    //EditorGUILayout.LabelField(humanName.PadRight(12), aStr, EditorStyles.miniLabel);
                    //EditorGUILayout.LabelField("".PadRight(12), bStr, EditorStyles.miniLabel);
                    //if (!string.IsNullOrEmpty(diffStr))
                    //    EditorGUILayout.LabelField("".PadRight(12), "-> " + diffStr, EditorStyles.miniLabel);

                    DrawLimitLine(humanName, aStr, bStr, diffStr, 
                        string.IsNullOrEmpty(diffStr) ? 0f : 999f,
                        !string.IsNullOrEmpty(diffStr));
                }
            }
        }

        private static string FormatLimit(HumanLimit limit) => $"min {FormatV3(limit.min)} / max {FormatV3(limit.max)} / c {FormatV3(limit.center)} / def ({limit.useDefaultValues})";

        private static string GetLimitDiff(HumanLimit a, HumanLimit b)
        {
            var diffs = new List<string>();

            if (a.useDefaultValues != b.useDefaultValues) diffs.Add("useDefault differs");
            if (!Approximately(a.min, b.min, FloatEpsilon)) diffs.Add("min differs");
            if (!Approximately(a.max, b.max, FloatEpsilon)) diffs.Add("max differs");
            if (!Approximately(a.center, b.center, FloatEpsilon)) diffs.Add("center differs");
            return string.Join(", ", diffs);
        }

        private void DrawSkeletonPoseDiff()
        {
            _showSkeletonDiffs = EditorGUILayout.Foldout(_showSkeletonDiffs, "Skeleton Pose (bind pose from importer)", true);
            if (!_showSkeletonDiffs) return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("This often reveals T-pose edits or bone rotation fixes.", EditorStyles.miniLabel);

                var keys = new HashSet<string>(_a.Skeleton.Keys);
                keys.UnionWith(_b.Skeleton.Keys);

                foreach (string path in keys.OrderBy(s => s))
                {
                    _a.Skeleton.TryGetValue(path, out SkeletonBone aInfo);
                    _b.Skeleton.TryGetValue(path, out SkeletonBone bInfo);

                    bool differs =
                        !Approximately(aInfo.position, bInfo.position, PosEpsilon) ||
                        !Approximately(aInfo.scale, bInfo.scale, ScaleEpsilon) ||
                        !ApproximatelyAngle(aInfo.rotation, bInfo.rotation, AngleEpsilonDeg);

                    if (_showOnlyDiffs && !differs)
                        continue;

                    using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                    {
                        //EditorGUILayout.LabelField(path, EditorStyles.boldLabel);
                        _a.SkeletonPath.TryGetValue(path, out string aPath);
                        _b.SkeletonPath.TryGetValue(path, out string bPath);
                        string fullPath = string.IsNullOrEmpty(aPath) ? bPath : aPath;
                        string pathLabel = path;
                        if (!string.IsNullOrEmpty(fullPath))
                            pathLabel += $" ({fullPath})";
                        EditorGUILayout.LabelField(pathLabel, EditorStyles.boldLabel);

                        //DrawLimitLine("pos", FormatV3(aInfo.Position), FormatV3(bInfo.Position), !Approximately(aInfo.Position, bInfo.Position, PosEpsilon));
                        //DrawLimitLine("rot", FormatQ(aInfo.Rotation), FormatQ(bInfo.Rotation), !ApproximatelyAngle(aInfo.Rotation, bInfo.Rotation, AngleEpsilonDeg));
                        //DrawLimitLine("scl", FormatV3(aInfo.Scale), FormatV3(bInfo.Scale), !Approximately(aInfo.Scale, bInfo.Scale, ScaleEpsilon));

                        float posMag = (aInfo.position - bInfo.position).magnitude;
                        float sclMag = (aInfo.scale - bInfo.scale).magnitude;
                        float angDeg = Quaternion.Angle(aInfo.rotation, bInfo.rotation);

                        DrawLimitLine("pos", FormatV3(aInfo.position), FormatV3(bInfo.position),
                            "d=" + posMag.ToString("0.#####", CultureInfo.InvariantCulture),
                            posMag / PosEpsilon, posMag > PosEpsilon);

                        DrawLimitLine("rot", FormatEulerDeg(aInfo.rotation), FormatEulerDeg(bInfo.rotation),
                            "d=" + angDeg.ToString("0.###", CultureInfo.InvariantCulture) + " deg",
                            angDeg / AngleEpsilonDeg, angDeg > AngleEpsilonDeg);

                        DrawLimitLine("scl", FormatV3(aInfo.scale), FormatV3(bInfo.scale),
                            "d=" + sclMag.ToString("0.#####", CultureInfo.InvariantCulture),
                            sclMag / ScaleEpsilon, sclMag > ScaleEpsilon);
                    }
                }
            }
        }

        private void DrawDiffRow(string name, float a, float b, float eps)
        {
            float d = Math.Abs(a - b);
            bool differs = d > eps;
            if (_showOnlyDiffs && !differs)
                return;

            using (new EditorGUILayout.HorizontalScope())
            {
                GUIStyle left = differs ? Styles.DiffLeft : EditorStyles.label;
                GUIStyle right = differs ? Styles.DiffRight : EditorStyles.label;
                Color c = differs ? Styles.GetSeverityColor(d / eps) : GUI.contentColor;

                EditorGUILayout.LabelField(name, GUILayout.Width(220f));
                using (new GUIColorScope(c))
                {
                    EditorGUILayout.LabelField(a.ToString("0.#####", CultureInfo.InvariantCulture), left, GUILayout.Width(260f));
                    EditorGUILayout.LabelField(b.ToString("0.#####", CultureInfo.InvariantCulture), right, GUILayout.Width(260f));
                    EditorGUILayout.LabelField("d=" + d.ToString("0.#####", CultureInfo.InvariantCulture), GUILayout.Width(140f));
                }
            }
        }

        private void DrawDiffRow(string name, bool a, bool b)
        {
            bool differs = a != b;
            if (_showOnlyDiffs && !differs)
                return;

            using (new EditorGUILayout.HorizontalScope())
            {
                GUIStyle left = differs ? Styles.DiffLeft : EditorStyles.label;
                GUIStyle right = differs ? Styles.DiffRight : EditorStyles.label;
                Color c = differs ? Styles.GetSeverityColor(999f) : GUI.contentColor;

                EditorGUILayout.LabelField(name, GUILayout.Width(220f));
                using (new GUIColorScope(c))
                {
                    EditorGUILayout.LabelField(a ? "true" : "false", left, GUILayout.Width(260f));
                    EditorGUILayout.LabelField(b ? "true" : "false", right, GUILayout.Width(260f));
                    EditorGUILayout.LabelField("DIFF", GUILayout.Width(140f));
                }
            }
        }

        private void DrawLimitLine(string label, string a, string b, string delta, float score, bool differs)
        {
            if (_showOnlyDiffs && !differs)
                return;

            using (new EditorGUILayout.HorizontalScope())
            {
                GUIStyle left = differs ? Styles.DiffLeft : EditorStyles.label;
                GUIStyle right = differs ? Styles.DiffRight : EditorStyles.label;
                Color c = differs ? Styles.GetSeverityColor(score) : GUI.contentColor;

                EditorGUILayout.LabelField(label, GUILayout.Width(80f));
                using (new GUIColorScope(c))
                {
                    EditorGUILayout.LabelField(a, left, GUILayout.Width(320f));
                    EditorGUILayout.LabelField(b, right, GUILayout.Width(320f));
                    EditorGUILayout.LabelField(delta ?? string.Empty, GUILayout.Width(140f));
                }
            }
        }

        private void CopyAToB()
        {
            if (!_a.IsValid || !_b.IsValid)
                return;

            ModelImporter bImporter = _b.Importer;
            if (bImporter == null)
                return;

            Undo.RecordObject(bImporter, "Copy Humanoid Avatar Definition");

            HumanDescription hd = bImporter.humanDescription;
            hd = _a.HumanDescription;

            bImporter.humanDescription = hd;

            // Reimport to apply.
            AssetDatabase.ImportAsset(_b.AssetPath, ImportAssetOptions.ForceUpdate);

            // Refresh snapshots.
            _a = Snapshot.TryCreate(_aAsset);
            _b = Snapshot.TryCreate(_bAsset);
        }

        /// <summary>
        /// Returns true if the specified humanoid bone is considered required
        /// by Unity for a valid humanoid avatar configuration.
        /// 
        /// This matches Unity's internal AvatarSetupTool behavior.
        /// Everything not in this list is treated as optional.
        /// </summary>
        private static bool IsRequiredHumanoidBone(HumanBodyBones bone)
        {
            switch (bone)
            {
                case HumanBodyBones.Hips:
                case HumanBodyBones.Spine:
                case HumanBodyBones.Head:

                case HumanBodyBones.LeftUpperLeg:
                case HumanBodyBones.LeftLowerLeg:
                case HumanBodyBones.LeftFoot:

                case HumanBodyBones.RightUpperLeg:
                case HumanBodyBones.RightLowerLeg:
                case HumanBodyBones.RightFoot:

                case HumanBodyBones.LeftUpperArm:
                case HumanBodyBones.LeftLowerArm:
                case HumanBodyBones.LeftHand:

                case HumanBodyBones.RightUpperArm:
                case HumanBodyBones.RightLowerArm:
                case HumanBodyBones.RightHand:
                    return true;

                default:
                    return false;
            }
        }

        private static bool IsRequiredHumanoidBone(string boneName)
        {
            switch (boneName)
            {
                case "Hips":
                case "Spine":
                case "Head":

                case "LeftUpperLeg":
                case "LeftLowerLeg":
                case "LeftFoot":

                case "RightUpperLeg":
                case "RightLowerLeg":
                case "RightFoot":

                case "LeftUpperArm":
                case "LeftLowerArm":
                case "LeftHand":

                case "RightUpperArm":
                case "RightLowerArm":
                case "RightHand":
                    return true;

                default:
                    return false;
            }
        }

        private static bool Approximately(Vector3 a, Vector3 b, float eps) => Math.Abs(a.x - b.x) <= eps && Math.Abs(a.y - b.y) <= eps && Math.Abs(a.z - b.z) <= eps;
        private static bool ApproximatelyAngle(Quaternion a, Quaternion b, float epsDeg) => Quaternion.Angle(a, b) <= epsDeg;

        private static string FormatV3(Vector3 v) => $"({v.x:0.#####}, {v.y:0.#####}, {v.z:0.#####})";
        private static string FormatEulerDeg(Quaternion q)
        {
            Vector3 e = q.eulerAngles;
            e = ToSignedEulerDegrees(e);
            return $"({e.x:0.###}, {e.y:0.###}, {e.z:0.###}) deg";
        }

        private static Vector3 ToSignedEulerDegrees(Vector3 e)
        {
            // Unity returns 0..360. Convert to -180..180 for readability.
            if (e.x > 180f) e.x -= 360f;
            if (e.y > 180f) e.y -= 360f;
            if (e.z > 180f) e.z -= 360f;
            return e;
        }

        private sealed class GUIColorScope : IDisposable
        {
            private readonly Color _prev;

            public GUIColorScope(Color c)
            {
                _prev = GUI.contentColor;
                GUI.contentColor = c;
            }

            public void Dispose()
            {
                GUI.contentColor = _prev;
            }
        }

        private static class Styles
        {
            public static readonly GUIStyle DiffLeft;
            public static readonly GUIStyle DiffRight;

            static Styles()
            {
                DiffLeft = new GUIStyle(EditorStyles.label);
                DiffRight = new GUIStyle(EditorStyles.label);

                // Subtle highlight.
                //DiffLeft.normal.textColor = new Color(1f, 0.65f, 0.2f, 1f);
                //DiffRight.normal.textColor = new Color(0.8f, 0.6f, 1f, 1f);

                Color hotColor = new Color(1f, 0.3f, 0.3f, 1f);
                DiffLeft.normal.textColor = hotColor;
                DiffRight.normal.textColor = hotColor;
            }

            public static Color GetSeverityColor(float score)
            {
                // score is roughly "how many epsilons" the difference is.
                // 1 = barely different, 5 = noticeable, 20+ = huge.
                float t = Mathf.Clamp01((score - 1f) / 10f);

                //Color baseColor = new Color(1f, 1f, 1f, 1f);
                //Color warnColor = new Color(1f, 0.6f, 0.15f, 1f);
                //Color hotColor = new Color(1f, 0.3f, 0.3f, 1f);
                Color baseColor = new Color(0.3f, 1, 0.3f, 1f);
                Color warnColor = new Color(1f, 1f, 0.3f, 1f);
                Color hotColor = new Color(1f, 0.3f, 0.3f, 1f);

                if (score >= 20f)
                    return Color.Lerp(warnColor, hotColor, Mathf.Clamp01((score - 20f) / 40f));

                return Color.Lerp(baseColor, warnColor, t);
            }
        }

        /// <summary>
        /// Captured import-time Humanoid data for one asset, normalized into dictionaries for fast comparison.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A <see cref="Snapshot"/> is built from a Unity asset (usually an FBX) by reading its <see cref="ModelImporter"/>
        /// and extracting <see cref="ModelImporter.humanDescription"/>.
        /// </para>
        /// <para>
        /// This is editor-only data: it reflects import settings and imported skeleton, not runtime pose.
        /// </para>
        /// <para>
        /// The dictionaries are keyed for quick diffing:
        /// </para>
        /// <list type="bullet">
        /// <item><description><see cref="HumanBones"/> is keyed by human bone name (e.g., "LeftUpperArm").</description></item>
        /// <item><description><see cref="Skeleton"/> is keyed by skeleton path/name.</description></item>
        /// </list>
        /// </remarks>
        private readonly struct Snapshot
        {
            public readonly bool IsValid;
            public readonly string AssetPath;
            public readonly ModelImporter Importer;
            public readonly HumanDescription HumanDescription;

            public readonly float ArmStretch;
            public readonly float LegStretch;
            public readonly float UpperArmTwist;
            public readonly float LowerArmTwist;
            public readonly float UpperLegTwist;
            public readonly float LowerLegTwist;
            public readonly float FeetSpacing;
            public readonly bool HasTranslationDoF;

            public readonly Dictionary<string, HumanBone> HumanBones;    // key: human bone name
            public readonly Dictionary<string, SkeletonBone> Skeleton;   // key: skeleton object name
            public readonly Dictionary<string, string> SkeletonPath;   // key: skeleton object name to full path

            public readonly string SummaryLine;

            public readonly bool TPoseHasError;
            public readonly float TPoseMaxErrorDeg;
            public readonly int TPoseFailCount;
            public readonly string TPoseWorstBone;
            public readonly string[] TPoseFailingBonesTop;
            public readonly string TPoseSummaryLine;

            private Snapshot(
                bool isValid,
                string assetPath,
                ModelImporter importer,
                HumanDescription hd,
                Dictionary<string, HumanBone> humanBones,
                Dictionary<string, SkeletonBone> skeleton,
                Dictionary<string, string> skeletonPaths,
                string summaryLine,
                bool tPoseHasError,
                float tPoseMaxErrorDeg,
                int tPoseFailCount,
                string tPoseWorstBone,
                string[] tPoseFailingBonesTop,
                string tPoseSummaryLine)
            {
                IsValid = isValid;
                AssetPath = assetPath ?? string.Empty;
                Importer = importer;
                HumanDescription = hd;

                ArmStretch = hd.armStretch;
                LegStretch = hd.legStretch;
                UpperArmTwist = hd.upperArmTwist;
                LowerArmTwist = hd.lowerArmTwist;
                UpperLegTwist = hd.upperLegTwist;
                LowerLegTwist = hd.lowerLegTwist;
                FeetSpacing = hd.feetSpacing;
                HasTranslationDoF = hd.hasTranslationDoF;

                HumanBones = humanBones ?? new Dictionary<string, HumanBone>();
                Skeleton = skeleton ?? new Dictionary<string, SkeletonBone>();
                SkeletonPath = skeletonPaths ?? new Dictionary<string, string>();

                SummaryLine = summaryLine ?? string.Empty;

                TPoseHasError = tPoseHasError;
                TPoseMaxErrorDeg = tPoseMaxErrorDeg;
                TPoseFailCount = tPoseFailCount;
                TPoseWorstBone = tPoseWorstBone ?? string.Empty;
                TPoseFailingBonesTop = tPoseFailingBonesTop ?? Array.Empty<string>();
                TPoseSummaryLine = tPoseSummaryLine ?? string.Empty;
            }

            public static Snapshot TryCreate(UnityEngine.Object obj)
            {
                if (obj == null)
                    return default;

                string path = AssetDatabase.GetAssetPath(obj);
                if (string.IsNullOrEmpty(path))
                    return default;

                AssetImporter ai = AssetImporter.GetAtPath(path);
                if (ai is not ModelImporter mi)
                    return default;

                if (mi.animationType != ModelImporterAnimationType.Human)
                {
                    string s = "Not Humanoid (animationType is " + mi.animationType + ")";
                    return new Snapshot(false, path, mi, default, null, null, null, s, false, 0f, 0, string.Empty, Array.Empty<string>(), string.Empty);
                }

                HumanDescription hd = mi.humanDescription;

                var humanBones = new Dictionary<string, HumanBone>(StringComparer.Ordinal);
                if (hd.human != null)
                {
                    foreach (HumanBone hb in hd.human)
                    {
                        if (string.IsNullOrEmpty(hb.humanName))
                            continue;

                        humanBones[hb.humanName] = hb;
                    }
                }

                // SkeletonBone.name is not guaranteed to be a full hierarchy path, but in practice
                // it is stable enough to detect pose edits across the same rig.
                var skeleton = new Dictionary<string, SkeletonBone>(StringComparer.Ordinal);
                if (hd.skeleton != null)
                {
                    foreach (SkeletonBone sb in hd.skeleton)
                    {
                        string key = sb.name ?? string.Empty;
                        if (key.Length == 0)
                            continue;

                        // If duplicates exist, keep the first and ignore the rest.
                        if (!skeleton.ContainsKey(key))
                            skeleton[key] = sb;
                    }
                }

                var skeletonPaths = BuildSkeletonNameToFullPathMap(hd.skeleton);

                TPoseEvaluator.Evaluate(path, humanBones,
                    out bool tPoseHasError,
                    out float tPoseMaxErrorDeg,
                    out int tPoseFailCount,
                    out string tPoseWorstBone,
                    out string[] tPoseFailingBonesTop);

                string tPoseSummaryLine = tPoseHasError
                    ? ("T-Pose: NOT OK (max=" + tPoseMaxErrorDeg.ToString("0.###", CultureInfo.InvariantCulture) +
                       " deg, bones=" + tPoseFailCount.ToString(CultureInfo.InvariantCulture) +
                       ", worst=" + tPoseWorstBone + ")")
                    : "T-Pose: OK";

                string summary = BuildSummary(mi, hd, humanBones, skeleton);

                return new Snapshot(
                    true, path, mi, hd, humanBones, skeleton, skeletonPaths, summary,
                    tPoseHasError, tPoseMaxErrorDeg, tPoseFailCount, tPoseWorstBone, tPoseFailingBonesTop, tPoseSummaryLine);
            }

            private static string BuildSummary(ModelImporter mi, HumanDescription hd, Dictionary<string, HumanBone> humanBones, Dictionary<string, SkeletonBone> skeleton)
            {
                StringBuilder sb = new StringBuilder(256);
                sb.Append("Humanoid. ");
                sb.Append("HumanBones=").Append(humanBones.Count).Append(", ");
                sb.Append("SkeletonBones=").Append(skeleton.Count).Append(". ");
                sb.Append("Twist UA/LA/UL/LL=");
                sb.Append(hd.upperArmTwist.ToString("0.###", CultureInfo.InvariantCulture)).Append("/");
                sb.Append(hd.lowerArmTwist.ToString("0.###", CultureInfo.InvariantCulture)).Append("/");
                sb.Append(hd.upperLegTwist.ToString("0.###", CultureInfo.InvariantCulture)).Append("/");
                sb.Append(hd.lowerLegTwist.ToString("0.###", CultureInfo.InvariantCulture));
                sb.Append(". Stretch A/L=");
                sb.Append(hd.armStretch.ToString("0.###", CultureInfo.InvariantCulture)).Append("/");
                sb.Append(hd.legStretch.ToString("0.###", CultureInfo.InvariantCulture));
                sb.Append(". FeetSpacing=");
                sb.Append(hd.feetSpacing.ToString("0.###", CultureInfo.InvariantCulture));
                sb.Append(". TranslationDoF=");
                sb.Append(hd.hasTranslationDoF ? "true" : "false");
                return sb.ToString();
            }

            public IEnumerable<string> GetSanityWarnings(string label)
            {
                if (!IsValid)
                {
                    yield return label + ": Snapshot invalid.";
                    yield break;
                }

                // Basic expected bones for a typical humanoid.
                string[] important = new[]
                {
                    "Hips","Spine","Chest","Neck","Head",
                    "LeftUpperArm","LeftLowerArm","LeftHand",
                    "RightUpperArm","RightLowerArm","RightHand",
                    "LeftUpperLeg","LeftLowerLeg","LeftFoot",
                    "RightUpperLeg","RightLowerLeg","RightFoot"
                };

                foreach (string h in important)
                {
                    if (!HumanBones.TryGetValue(h, out HumanBone info) || string.IsNullOrEmpty(info.boneName))
                        yield return label + ": Missing mapping for " + h;
                }

                // Heuristic: twist values outside 0..1 are unusual.
                if (UpperArmTwist < 0f || UpperArmTwist > 1f) yield return label + ": upperArmTwist is outside [0..1].";
                if (LowerArmTwist < 0f || LowerArmTwist > 1f) yield return label + ": lowerArmTwist is outside [0..1].";
                if (UpperLegTwist < 0f || UpperLegTwist > 1f) yield return label + ": upperLegTwist is outside [0..1].";
                if (LowerLegTwist < 0f || LowerLegTwist > 1f) yield return label + ": lowerLegTwist is outside [0..1].";

                // Heuristic: missing skeleton often implies importer didn't populate.
                if (Skeleton.Count < 10)
                    yield return label + ": Skeleton list looks very small; pose diffs may be unreliable.";

                if (TPoseHasError)
                {
                    yield return label + ": Character is not in T pose (max=" +
                        TPoseMaxErrorDeg.ToString("0.###", CultureInfo.InvariantCulture) +
                        " deg, bones=" + TPoseFailCount.ToString(CultureInfo.InvariantCulture) +
                        ", worst=" + TPoseWorstBone + ").";

                    // Similar to Unity: show failing bone names (top list).
                    for (int i = 0; i < TPoseFailingBonesTop.Length; i++)
                        yield return label + ":   " + TPoseFailingBonesTop[i];
                }
            }

            private static Dictionary<string, string> BuildSkeletonNameToFullPathMap(SkeletonBone [] skeleton)
            {
                // Build name -> parentName map (via reflection on internal field).
                var parentByName = new Dictionary<string, string>(StringComparer.Ordinal);
                foreach (var skeletonBone in skeleton)
                {
                    string name = skeletonBone.name;
                    if (string.IsNullOrEmpty(name))
                        continue;

                    string parent = TryGetSkeletonBoneParentName(skeletonBone);
                    parentByName[name] = parent ?? string.Empty;
                }

                // Find skeleton roots (bones with no parent, or parent not in skeleton list).
                var roots = new List<string>(4);
                foreach (var kvp in parentByName)
                {
                    string parent = kvp.Value;
                    if (string.IsNullOrEmpty(parent) || !parentByName.ContainsKey(parent))
                        roots.Add(kvp.Key);
                }

                // If there's exactly one root, strip it from all paths (except the root itself).
                string stripRoot = roots.Count == 1 ? roots[0] : null;

                // Resolve full path for each bone.
                var result = new Dictionary<string, string>(StringComparer.Ordinal);
                foreach (string name in parentByName.Keys)
                    result[name] = ResolveSkeletonPath(name, parentByName, stripRoot);

                return result;
            }

            private static string ResolveSkeletonPath(string name, Dictionary<string, string> parentByName, string stripRoot)
            {
                // Walk parent links up to a root, building Root/.../Leaf.
                // Detect cycles / missing parents gracefully.
                var parts = new List<string>(16);
                var visited = new HashSet<string>(StringComparer.Ordinal);

                string cur = name;
                while (!string.IsNullOrEmpty(cur))
                {
                    if (!visited.Add(cur))
                    {
                        // Cycle detected. Fall back to just the name.
                        return name;
                    }

                    parts.Add(cur);

                    if (!parentByName.TryGetValue(cur, out string parent) || string.IsNullOrEmpty(parent))
                        break;

                    // If parent isn't in the skeleton list, still include it once and stop.
                    if (!parentByName.ContainsKey(parent))
                    {
                        //parts.Add(parent);
                        break;
                    }

                    cur = parent;
                }

                parts.Reverse();

                // Strip the single common root if requested (but keep it for the root bone itself).
                if (!string.IsNullOrEmpty(stripRoot) &&
                    parts.Count > 1 &&
                    string.Equals(parts[0], stripRoot, StringComparison.Ordinal))
                {
                    parts.RemoveAt(0);
                }

                return string.Join("/", parts);
            }

            private static string TryGetSkeletonBoneParentName(SkeletonBone sb)
            {
                // Unity exposes m_ParentName as `internal string parentName;`
                // We access it via reflection (Editor-only; safe for this tool).
                var skeletonBoneParentNameField = typeof(SkeletonBone).GetField("parentName", BindingFlags.Instance | BindingFlags.NonPublic);
                if (skeletonBoneParentNameField == null)
                    return string.Empty;

                object boxed = sb;
                return skeletonBoneParentNameField.GetValue(boxed) as string ?? string.Empty;
            }
        }


        /// <summary>
        /// Utility that evaluates whether the imported Humanoid skeleton is in (or close to) Unity's expected T-Pose,
        /// and reports the worst offending bones.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This logic is intended to mimic the high-level behavior of Unity's Avatar configuration warnings
        /// (e.g., "Character is not in T Pose") by computing per-bone alignment errors against expected directions.
        /// </para>
        /// <para>
        /// It operates on imported skeleton data and human bone mapping rather than scene animation state.
        /// The output is designed for diagnostics and UI presentation.
        /// </para>
        /// </remarks>
        private static class TPoseEvaluator
        {
            private const float ErrorEpsilonDeg = 0.0001f;
            private const int MaxFailingBonesToReport = 24;


            /// <summary>
            /// Minimal wrapper that associates a human bone name with a resolved skeleton <see cref="Transform"/>.
            /// </summary>
            /// <remarks>
            /// Used internally by <see cref="TPoseEvaluator"/> to keep the evaluation code close to Unity-like semantics:
            /// "this HumanBodyBones slot maps to that Transform".
            /// </remarks>
            private sealed class BoneWrapper
            {
                public readonly string humanBoneName;
                public Transform bone;

                public BoneWrapper(string humanBoneName, Transform bone)
                {
                    this.humanBoneName = humanBoneName;
                    this.bone = bone;
                }
            }

            /// <summary>
            /// Expected pose data for a bone used during T-Pose evaluation.
            /// </summary>
            /// <remarks>
            /// <para>
            /// This describes the expected direction and comparison method for a bone when checking alignment.
            /// Some bones are evaluated in global space, others in local/relative space, depending on the intended constraint.
            /// </para>
            /// <para>
            /// This is a data container to keep the evaluation loop tight and avoid repeated conditional logic.
            /// </para>
            /// </remarks>
            private sealed class BonePoseData
            {
                public Vector3 direction = Vector3.zero;
                public bool compareInGlobalSpace = false;
                public float maxAngle;
                public int[] childIndices = null;
                public Vector3 planeNormal = Vector3.zero;

                public BonePoseData(Vector3 dir, bool globalSpace, float maxAngleDiff)
                {
                    direction = (dir == Vector3.zero ? dir : dir.normalized);
                    compareInGlobalSpace = globalSpace;
                    maxAngle = maxAngleDiff;
                }

                public BonePoseData(Vector3 dir, bool globalSpace, float maxAngleDiff, int[] children) : this(dir, globalSpace, maxAngleDiff)
                {
                    childIndices = children;
                }

                public BonePoseData(Vector3 dir, bool globalSpace, float maxAngleDiff, Vector3 planeNormal, int[] children) : this(dir, globalSpace, maxAngleDiff, children)
                {
                    this.planeNormal = planeNormal;
                }
            }

            // Copied/condensed from Unity AvatarSetupTool.sBonePoses
            private static readonly BonePoseData[] sBonePoses =
            {
                new(Vector3.up, true, 15),  // Hips,
                new(new Vector3(-0.05f, -1, 0),      true, 15),   // LeftUpperLeg,
                new(new Vector3(0.05f, -1, 0),       true, 15),   // RightUpperLeg,
                new(new Vector3(-0.05f, -1, -0.15f), true, 20),   // LeftLowerLeg,
                new(new Vector3(0.05f, -1, -0.15f),  true, 20),   // RightLowerLeg,
                new(new Vector3(-0.05f, 0, 1),       true, 20, Vector3.up, null),   // LeftFoot,
                new(new Vector3(0.05f, 0, 1),        true, 20, Vector3.up, null),   // RightFoot,
                new(Vector3.up, true, 30, new int[] {(int)HumanBodyBones.Chest, (int)HumanBodyBones.UpperChest, (int)HumanBodyBones.Neck, (int)HumanBodyBones.Head}), // Spine,
                new(Vector3.up, true, 30, new int[] {(int)HumanBodyBones.UpperChest, (int)HumanBodyBones.Neck, (int)HumanBodyBones.Head}), // Chest,
                new(Vector3.up, true, 30),  // Neck,
                null, // Head,
                new(-Vector3.right, true, 20),  // LeftShoulder,
                new(Vector3.right, true, 20),   // RightShoulder,
                new(-Vector3.right, true, 05),  // LeftUpperArm,
                new(Vector3.right, true, 05),   // RightUpperArm,
                new(-Vector3.right, true, 05),  // LeftLowerArm,
                new(Vector3.right, true, 05),   // RightLowerArm,
                new(-Vector3.right, false, 10, Vector3.forward, new int[] {(int)HumanBodyBones.LeftMiddleProximal}),  // LeftHand,
                new(Vector3.right, false, 10, Vector3.forward, new int[] {(int)HumanBodyBones.RightMiddleProximal}), // RightHand,
                null, // LeftToes,
                null, // RightToes,
                null, // LeftEye,
                null, // RightEye,
                null, // Jaw,
                new(new Vector3(-1, 0, 1), false, 10), // Left Thumb 1
                new(new Vector3(-1, 0, 1), false, 05),
                new(new Vector3(-1, 0, 1), false, 05),
                new(-Vector3.right, false, 10),  // Left Index
                new(-Vector3.right, false, 05),
                new(-Vector3.right, false, 05),
                new(-Vector3.right, false, 10),  // Left Middle
                new(-Vector3.right, false, 05),
                new(-Vector3.right, false, 05),
                new(-Vector3.right, false, 10),  // Left Ring
                new(-Vector3.right, false, 05),
                new(-Vector3.right, false, 05),
                new(-Vector3.right, false, 10),  // Left Little
                new(-Vector3.right, false, 05),
                new(-Vector3.right, false, 05),
                new(new Vector3(1, 0, 1), false, 10), // Right Thumb 1
                new(new Vector3(1, 0, 1), false, 05),
                new(new Vector3(1, 0, 1), false, 05),
                new(Vector3.right, false, 10),   // Right Index
                new(Vector3.right, false, 05),
                new(Vector3.right, false, 05),
                new(Vector3.right, false, 10),   // Right Middle
                new(Vector3.right, false, 05),
                new(Vector3.right, false, 05),
                new(Vector3.right, false, 10),   // Right Ring
                new(Vector3.right, false, 05),
                new(Vector3.right, false, 05),
                new(Vector3.right, false, 10),   // Right Little
                new(Vector3.right, false, 05),
                new(Vector3.right, false, 05),
                new(Vector3.up, true, 30, new int[] {(int)HumanBodyBones.Neck, (int)HumanBodyBones.Head}),  // UpperChest,
            };

            /// <summary>
            /// Represents a single failing bone entry from the T-Pose evaluation.
            /// </summary>
            /// <remarks>
            /// <para>
            /// <see cref="ErrorDeg"/> is the angular error in degrees between the current pose direction and the expected direction.
            /// </para>
            /// <para>
            /// The index is primarily for internal sorting / stable ordering (it can correspond to an internal evaluation order).
            /// </para>
            /// </remarks>
            private readonly struct Fail
            {
                public readonly int Index;
                public readonly string BoneName;
                public readonly string HumanName;
                public readonly float ErrorDeg;

                public Fail(int index, string boneName, string humanName, float errorDeg)
                {
                    Index = index;
                    BoneName = boneName ?? string.Empty;
                    HumanName = humanName ?? string.Empty;
                    ErrorDeg = errorDeg;
                }
            }

            public static void Evaluate(
                string assetPath,
                Dictionary<string, HumanBone> humanBones,
                out bool hasError,
                out float maxErrorDeg,
                out int failCount,
                out string worstBone,
                out string[] failingBonesTop)
            {
                hasError = false;
                maxErrorDeg = 0f;
                failCount = 0;
                worstBone = string.Empty;
                failingBonesTop = Array.Empty<string>();

                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (prefab == null)
                    return;

                GameObject instance = null;
                try
                {
                    instance = UnityEngine.Object.Instantiate(prefab);

                    ModelImporter mi = AssetImporter.GetAtPath(assetPath) as ModelImporter;
                    if (mi == null)
                        return;

                    HumanDescription hd = mi.humanDescription;

                    // Unity does this BEFORE evaluating: put the instance into the importer skeleton pose.
                    ApplySkeletonPoseToInstance(instance.transform, hd.skeleton);

                    // Unity resolves bones using GetModelBones + GetHumanBones (not first-by-name scan).
                    var modelBones = GetModelBones(instance.transform, false, null);

                    // Build mapping: humanName -> boneName (Unity uses this shape, not your HumanBoneInfo dict).
                    var existingMappings = new Dictionary<string, string>(StringComparer.Ordinal);
                    if (hd.human != null)
                    {
                        foreach (HumanBone hb in hd.human)
                        {
                            if (string.IsNullOrEmpty(hb.humanName))
                                continue;

                            existingMappings[hb.humanName] = hb.boneName ?? string.Empty;
                        }
                    }

                    var bones = GetHumanBones(existingMappings, modelBones);

                    Quaternion orientation = AvatarComputeOrientation(bones);

                    var fails = new List<Fail>(64);

                    int poseCount = Mathf.Min(sBonePoses.Length, bones.Length);
                    for (int i = 0; i < poseCount; i++)
                    {
                        float e = GetBoneAlignmentError(bones, orientation, i);
                        if (e > ErrorEpsilonDeg)
                        {
                            hasError = true;
                            failCount++;

                            string humanName = bones[i].humanBoneName ?? ((HumanBodyBones)i).ToString();
                            string boneName = bones[i].bone != null ? bones[i].bone.name : "(null)";
                            fails.Add(new Fail(i, boneName, humanName, e));

                            if (e > maxErrorDeg)
                            {
                                maxErrorDeg = e;
                                worstBone = boneName;
                            }
                        }
                    }

                    if (fails.Count > 0)
                    {
                        fails.Sort((a, b) => b.ErrorDeg.CompareTo(a.ErrorDeg));

                        int n = Mathf.Min(MaxFailingBonesToReport, fails.Count);
                        string[] top = new string[n];
                        for (int i = 0; i < n; i++)
                        {
                            Fail f = fails[i];
                            top[i] = f.BoneName + " [" + f.HumanName + "] (+" + f.ErrorDeg.ToString("0.###", CultureInfo.InvariantCulture) + " deg)";
                        }

                        failingBonesTop = top;
                    }
                }
                finally
                {
                    if (instance != null)
                        UnityEngine.Object.DestroyImmediate(instance);
                }
            }

            // --- Copied/condensed from AvatarSetupTool ---

            private static float GetBoneAlignmentError(BoneWrapper[] bones, Quaternion avatarOrientation, int boneIndex)
            {
                if (boneIndex < 0 || boneIndex >= sBonePoses.Length)
                    return 0f;

                BoneWrapper bone = bones[boneIndex];
                BonePoseData pose = sBonePoses[boneIndex];
                if (bone == null || bone.bone == null || pose == null)
                    return 0f;

                if (boneIndex == (int)HumanBodyBones.Hips)
                {
                    float angleX = Vector3.Angle(avatarOrientation * Vector3.right, Vector3.right);
                    float angleY = Vector3.Angle(avatarOrientation * Vector3.up, Vector3.up);
                    float angleZ = Vector3.Angle(avatarOrientation * Vector3.forward, Vector3.forward);
                    return Mathf.Max(0f, Mathf.Max(angleX, Mathf.Max(angleY, angleZ)) - pose.maxAngle);
                }

                Vector3 dir = GetBoneAlignmentDirection(bones, avatarOrientation, boneIndex);
                if (dir == Vector3.zero)
                    return 0f;

                Quaternion space = GetRotationSpace(bones, avatarOrientation, boneIndex);
                Vector3 goalDir = space * pose.direction;

                if (pose.planeNormal != Vector3.zero)
                    dir = Vector3.ProjectOnPlane(dir, space * pose.planeNormal);

                return Mathf.Max(0f, Vector3.Angle(dir, goalDir) - pose.maxAngle);
            }

            private static Quaternion GetRotationSpace(BoneWrapper[] bones, Quaternion avatarOrientation, int boneIndex)
            {
                Quaternion parentDelta = Quaternion.identity;
                BonePoseData pose = sBonePoses[boneIndex];
                if (!pose.compareInGlobalSpace)
                {
                    int parentIndex = HumanTrait.GetParentBone(boneIndex);
                    if (parentIndex > 0)
                    {
                        BonePoseData parentPose = sBonePoses[parentIndex];
                        if (bones[parentIndex].bone != null && parentPose != null)
                        {
                            Vector3 parentDir = GetBoneAlignmentDirection(bones, avatarOrientation, parentIndex);
                            if (parentDir != Vector3.zero)
                            {
                                Vector3 parentPoseDir = avatarOrientation * parentPose.direction;
                                parentDelta = Quaternion.FromToRotation(parentPoseDir, parentDir);
                            }
                        }
                    }
                }

                return parentDelta * avatarOrientation;
            }

            private static Vector3 GetBoneAlignmentDirection(BoneWrapper[] bones, Quaternion avatarOrientation, int boneIndex)
            {
                if (sBonePoses[boneIndex] == null)
                    return Vector3.zero;

                BoneWrapper bone = bones[boneIndex];
                if (bone == null || bone.bone == null)
                    return Vector3.zero;

                BonePoseData pose = sBonePoses[boneIndex];
                int childBoneIndex = -1;

                if (pose.childIndices != null)
                {
                    foreach (int i in pose.childIndices)
                    {
                        if (i >= 0 && i < bones.Length && bones[i] != null && bones[i].bone != null)
                        {
                            childBoneIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    childBoneIndex = GetHumanBoneChild(bones, boneIndex);
                }

                Vector3 dir;
                if (childBoneIndex >= 0 && childBoneIndex < bones.Length && bones[childBoneIndex] != null && bones[childBoneIndex].bone != null)
                {
                    dir = bones[childBoneIndex].bone.position - bone.bone.position;
                }
                else
                {
                    if (bone.bone.childCount != 1)
                        return Vector3.zero;

                    dir = Vector3.zero;
                    foreach (Transform child in bone.bone)
                    {
                        dir = child.position - bone.bone.position;
                        break;
                    }
                }

                return dir.normalized;
            }

            private static int GetHumanBoneChild(BoneWrapper[] bones, int boneIndex)
            {
                for (int i = 0; i < HumanTrait.BoneCount; i++)
                    if (HumanTrait.GetParentBone(i) == boneIndex)
                        return i;
                return -1;
            }

            private static Quaternion AvatarComputeOrientation(BoneWrapper[] bones)
            {
                Transform leftUpLeg = bones[(int)HumanBodyBones.LeftUpperLeg].bone;
                Transform rightUpLeg = bones[(int)HumanBodyBones.RightUpperLeg].bone;
                Transform leftArm = bones[(int)HumanBodyBones.LeftUpperArm].bone;
                Transform rightArm = bones[(int)HumanBodyBones.RightUpperArm].bone;

                if (leftUpLeg != null && rightUpLeg != null && leftArm != null && rightArm != null)
                    return AvatarComputeOrientation(leftUpLeg.position, rightUpLeg.position, leftArm.position, rightArm.position);

                return Quaternion.identity;
            }

            private static Quaternion AvatarComputeOrientation(Vector3 leftUpLeg, Vector3 rightUpLeg, Vector3 leftArm, Vector3 rightArm)
            {
                Vector3 legsRightDir = Vector3.Normalize(rightUpLeg - leftUpLeg);
                Vector3 armsRightDir = Vector3.Normalize(rightArm - leftArm);
                Vector3 torsoRightDir = Vector3.Normalize(legsRightDir + armsRightDir);

                bool sensibleOrientation =
                    Mathf.Abs(torsoRightDir.x * torsoRightDir.y) < 0.05f &&
                    Mathf.Abs(torsoRightDir.y * torsoRightDir.z) < 0.05f &&
                    Mathf.Abs(torsoRightDir.z * torsoRightDir.x) < 0.05f;

                Vector3 legsAvgPos = (leftUpLeg + rightUpLeg) * 0.5f;
                Vector3 armsAvgPos = (leftArm + rightArm) * 0.5f;
                Vector3 torsoUpDir = Vector3.Normalize(armsAvgPos - legsAvgPos);

                if (sensibleOrientation)
                {
                    int axisIndex = 0;
                    for (int i = 1; i < 3; i++)
                        if (Mathf.Abs(torsoUpDir[i]) > Mathf.Abs(torsoUpDir[axisIndex]))
                            axisIndex = i;

                    float sign = Mathf.Sign(torsoUpDir[axisIndex]);
                    torsoUpDir = Vector3.zero;
                    torsoUpDir[axisIndex] = sign;
                }

                Vector3 torsoForwardDir = Vector3.Cross(torsoRightDir, torsoUpDir);

                if (torsoForwardDir == Vector3.zero || torsoUpDir == Vector3.zero)
                    return Quaternion.identity;

                return Quaternion.LookRotation(torsoForwardDir, torsoUpDir);
            }

            private static void ApplySkeletonPoseToInstance(Transform root, SkeletonBone[] skeleton)
            {
                if (root == null || skeleton == null || skeleton.Length == 0)
                    return;

                ApplySkeletonPoseRecursive(root, skeleton, true);
            }

            private static void ApplySkeletonPoseRecursive(Transform t, SkeletonBone[] skeleton, bool isRoot)
            {
                if (t == null)
                    return;

                int idx = FindSkeletonBoneIndex(skeleton, t.name, isRoot);
                if (idx >= 0)
                {
                    SkeletonBone sb = skeleton[idx];
                    t.SetLocalPositionAndRotation(sb.position, sb.rotation);
                    t.localScale = sb.scale;
                }

                for (int i = 0; i < t.childCount; i++)
                    ApplySkeletonPoseRecursive(t.GetChild(i), skeleton, false);
            }

            // Matches Unity FindSkeletonBone behavior:
            // - root checks skeleton[0] only
            // - non-root scans skeleton[1..] so duplicate root names under children don't steal the root entry.
            private static int FindSkeletonBoneIndex(SkeletonBone[] skeleton, string name, bool isRoot)
            {
                if (skeleton == null || skeleton.Length == 0 || string.IsNullOrEmpty(name))
                    return -1;

                if (isRoot)
                {
                    if (skeleton[0].name == name)
                        return 0;
                    return -1;
                }

                for (int i = 1; i < skeleton.Length; i++)
                {
                    if (skeleton[i].name == name)
                        return i;
                }

                return -1;
            }

            private static Dictionary<Transform, bool> GetModelBones(Transform root, bool includeAll, BoneWrapper[] humanBones)
            {
                if (root == null)
                    return null;

                var bones = new Dictionary<Transform, bool>();
                var skinnedBones = new List<Transform>();

                if (!includeAll)
                {
                    var skinnedMeshRenderers = root.GetComponentsInChildren<SkinnedMeshRenderer>();

                    foreach (var rend in skinnedMeshRenderers)
                    {
                        var meshBones = rend.bones;
                        var meshBonesUsed = new bool[meshBones.Length];
                        var weights = rend.sharedMesh != null ? rend.sharedMesh.boneWeights : null;

                        if (weights != null)
                        {
                            foreach (var w in weights)
                            {
                                if (w.weight0 != 0) meshBonesUsed[w.boneIndex0] = true;
                                if (w.weight1 != 0) meshBonesUsed[w.boneIndex1] = true;
                                if (w.weight2 != 0) meshBonesUsed[w.boneIndex2] = true;
                                if (w.weight3 != 0) meshBonesUsed[w.boneIndex3] = true;
                            }
                        }

                        for (int i = 0; i < meshBones.Length; i++)
                        {
                            if (meshBonesUsed[i])
                            {
                                Transform b = meshBones[i];
                                if (b != null && !skinnedBones.Contains(b))
                                    skinnedBones.Add(b);
                            }
                        }
                    }

                    DetermineIsActualBone(root, bones, skinnedBones, false, humanBones);
                }

                if (bones.Count < HumanTrait.RequiredBoneCount)
                {
                    bones.Clear();
                    skinnedBones.Clear();
                    DetermineIsActualBone(root, bones, skinnedBones, true, humanBones);
                }

                return bones;
            }

            private static bool DetermineIsActualBone(
                Transform tr,
                Dictionary<Transform, bool> bones,
                List<Transform> skinnedBones,
                bool includeAll,
                BoneWrapper[] humanBones)
            {
                bool actualBone = includeAll;
                bool boneParent = false;
                bool boneChild = false;

                int childBones = 0;
                for (int i = 0; i < tr.childCount; i++)
                {
                    if (DetermineIsActualBone(tr.GetChild(i), bones, skinnedBones, includeAll, humanBones))
                        childBones++;
                }

                if (childBones > 0)
                    boneParent = true;
                if (childBones > 1)
                    actualBone = true;

                if (!actualBone)
                {
                    if (skinnedBones.Contains(tr))
                        actualBone = true;
                }

                if (!actualBone)
                {
                    var components = tr.GetComponents<Component>();
                    if (components.Length > 1)
                    {
                        foreach (var comp in components)
                        {
                            if ((comp is Renderer renderer) && comp is not SkinnedMeshRenderer)
                            {
                                Bounds bounds = renderer.bounds;
                                bounds.extents = bounds.size;

                                if (tr.childCount == 0 && tr.parent && bounds.Contains(tr.parent.position))
                                {
                                    if (tr.parent.GetComponent<Renderer>() != null)
                                        actualBone = true;
                                    else
                                        boneChild = true;
                                }
                                else if (bounds.Contains(tr.position))
                                {
                                    actualBone = true;
                                }
                            }
                        }
                    }
                }

                if (!actualBone && humanBones != null)
                {
                    foreach (var bw in humanBones)
                    {
                        if (bw != null && tr == bw.bone)
                        {
                            actualBone = true;
                            break;
                        }
                    }
                }

                if (actualBone)
                {
                    bones[tr] = true;
                }
                else if (boneParent)
                {
                    if (!bones.ContainsKey(tr))
                        bones[tr] = false;
                }
                else if (boneChild && tr.parent != null)
                {
                    bones[tr.parent] = true;
                }

                return bones.ContainsKey(tr);
            }

            private static BoneWrapper[] GetHumanBones(Dictionary<string, string> existingMappings, Dictionary<Transform, bool> actualBones)
            {
                var humanBoneNames = HumanTrait.BoneName;
                var bones = new BoneWrapper[humanBoneNames.Length];

                for (int i = 0; i < humanBoneNames.Length; i++)
                {
                    Transform bone = null;

                    string humanBoneName = humanBoneNames[i];
                    if (existingMappings != null && existingMappings.TryGetValue(humanBoneName, out string boneName))
                    {
                        // Unity picks the first transform whose .name matches boneName, but from actualBones.Keys
                        // (which is not the same as GetComponentsInChildren order, and matters for duplicate names).
                        foreach (Transform t in actualBones.Keys)
                        {
                            if (t != null && t.name == boneName)
                            {
                                bone = t;
                                break;
                            }
                        }
                    }

                    bones[i] = new BoneWrapper(humanBoneName, bone);
                }

                return bones;
            }
        }

        /// <summary>
        /// Heuristic analyzer that attempts to infer likely causes of humanoid mapping differences,
        /// with special attention to common Character Creator (CC / CC5) rig patterns.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This diagnoser does not perform a full semantic validation of the avatar.
        /// Instead, it applies lightweight pattern recognition over two <see cref="Snapshot"/> instances
        /// to detect systematic mapping differences that commonly occur when:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Comparing CC / CC5 exports against non-CC rigs.</description></item>
        /// <item><description>Mixing export presets (with or without UpperChest).</description></item>
        /// <item><description>Accidentally shifting Chest vs UpperChest assignments.</description></item>
        /// <item><description>Mapping clavicles vs shoulders inconsistently.</description></item>
        /// <item><description>Mapping twist bones differently (dedicated twist vs parent reuse).</description></item>
        /// </list>
        /// <para>
        /// The output is a list of <see cref="DiagnosisItem"/> entries ordered by relative severity.
        /// These are intended for UI presentation in the diff window, not for automated correction.
        /// </para>
        /// <para>
        /// Important: this class relies on name-based heuristics (string matching against bone names and skeleton entries).
        /// It does not assume a specific exporter version and may produce false positives if rigs use unconventional naming.
        /// The goal is to provide high-signal hints for content triage, not authoritative validation.
        /// </para>
        /// <para>
        /// This logic is editor-only and operates strictly on import-time data contained in <see cref="Snapshot"/>.
        /// It does not evaluate runtime pose or animation state.
        /// </para>
        /// </remarks>
        private static class CCDiagnoser
        {
            private static readonly string[] TwistHumanBones =
            {
                "LeftUpperArmTwist","LeftLowerArmTwist",
                "RightUpperArmTwist","RightLowerArmTwist",
                "LeftUpperLegTwist","LeftLowerLegTwist",
                "RightUpperLegTwist","RightLowerLegTwist"
            };

            public static List<DiagnosisItem> Diagnose(Snapshot a, Snapshot b)
            {
                var items = new List<DiagnosisItem>();
                if (!a.IsValid || !b.IsValid)
                    return items;

                // 0) Quick signature differences (useful when comparing CC vs non-CC assets).
                string aSig = GetRigSignature(a);
                string bSig = GetRigSignature(b);
                if (!string.Equals(aSig, bSig, StringComparison.Ordinal))
                {
                    items.Add(new DiagnosisItem(
                        "Rig signature differs (A looks like " + aSig + ", B looks like " + bSig + ")",
                        "This often correlates with different exporter presets and different expected humanoid mappings.",
                        "If this was meant to be the same rig family, re-export both with the same preset, or copy humanoid mapping from the known-good one.",
                        8f));
                }

                // 1) UpperChest presence / mapping mismatch.
                string aUpper = GetMappedBone(a, "UpperChest");
                string bUpper = GetMappedBone(b, "UpperChest");
                bool aHasUpper = !string.IsNullOrEmpty(aUpper) || HasSkeletonToken(a, "upperchest");
                bool bHasUpper = !string.IsNullOrEmpty(bUpper) || HasSkeletonToken(b, "upperchest");

                if (aHasUpper != bHasUpper)
                {
                    items.Add(new DiagnosisItem(
                        "UpperChest exists on one rig but not the other",
                        "A UpperChest=" + ToDisplay(aUpper) + ", B UpperChest=" + ToDisplay(bUpper) + ".",
                        "For CC / CC5: export both with the same spine option (UpperChest on/off). In Unity, ensure Chest/UpperChest are mapped consistently.",
                        12f));
                }
                else if (!string.Equals(aUpper, bUpper, StringComparison.Ordinal) && (string.IsNullOrEmpty(aUpper) || string.IsNullOrEmpty(bUpper)))
                {
                    items.Add(new DiagnosisItem(
                        "UpperChest is unmapped on one side",
                        "A UpperChest=" + ToDisplay(aUpper) + ", B UpperChest=" + ToDisplay(bUpper) + ".",
                        "Map UpperChest if the rig actually has it; otherwise leave it empty on both and map Chest instead.",
                        10f));
                }

                // 2) Shoulder mapping shifts (CC commonly uses clavicle naming; other rigs vary).
                DiagnoseShoulder(items, a, b, "LeftShoulder");
                DiagnoseShoulder(items, a, b, "RightShoulder");

                // 3) Twist mapping differences.
                foreach (string twistHuman in TwistHumanBones)
                    DiagnoseTwist(items, a, b, twistHuman);

                // 4) Chest / UpperChest swap (a common symptom of UpperChest presence).
                string aChest = GetMappedBone(a, "Chest");
                string bChest = GetMappedBone(b, "Chest");
                if (!string.IsNullOrEmpty(aChest) && !string.IsNullOrEmpty(bChest) && !string.Equals(aChest, bChest, StringComparison.Ordinal))
                {
                    bool looksLikeSwap =
                        (NameHasAny(aChest, "upperchest", "spine02", "spine_02", "spine2") &&
                         NameHasAny(bChest, "chest", "spine01", "spine_01", "spine1")) ||
                        (NameHasAny(bChest, "upperchest", "spine02", "spine_02", "spine2") &&
                         NameHasAny(aChest, "chest", "spine01", "spine_01", "spine1"));

                    if (looksLikeSwap)
                    {
                        items.Add(new DiagnosisItem(
                            "Chest mapping looks shifted (possible Chest vs UpperChest swap)",
                            "A Chest=" + ToDisplay(aChest) + ", B Chest=" + ToDisplay(bChest) + ".",
                            "If the rig has both Chest and UpperChest, verify both mappings. If it only has one, keep the mapping on Chest and leave UpperChest empty.",
                            9f));
                    }
                }

                return items;
            }

            private static void DiagnoseShoulder(List<DiagnosisItem> items, Snapshot a, Snapshot b, string human)
            {
                string aMap = GetMappedBone(a, human);
                string bMap = GetMappedBone(b, human);
                if (string.Equals(aMap, bMap, StringComparison.Ordinal))
                    return;

                bool aClav = NameHasAny(aMap, "clav", "clavicle");
                bool bClav = NameHasAny(bMap, "clav", "clavicle");
                bool aShoulder = NameHasAny(aMap, "shoulder");
                bool bShoulder = NameHasAny(bMap, "shoulder");

                if ((aClav != bClav) || (aShoulder != bShoulder) || string.IsNullOrEmpty(aMap) || string.IsNullOrEmpty(bMap))
                {
                    items.Add(new DiagnosisItem(
                        human + " mapping differs (possible clavicle/shoulder shift)",
                        "A " + human + "=" + ToDisplay(aMap) + ", B " + human + "=" + ToDisplay(bMap) + ".",
                        "In Unity Avatar config, map " + human + " to the clavicle/shoulder bone (not UpperArm). CC rigs typically use a clavicle bone here.",
                        8f));
                }
            }

            private static void DiagnoseTwist(List<DiagnosisItem> items, Snapshot a, Snapshot b, string twistHuman)
            {
                string aTwist = GetMappedBone(a, twistHuman);
                string bTwist = GetMappedBone(b, twistHuman);
                if (string.Equals(aTwist, bTwist, StringComparison.Ordinal))
                    return;

                string parentHuman = GetTwistParentHuman(twistHuman);
                string aParent = string.IsNullOrEmpty(parentHuman) ? string.Empty : GetMappedBone(a, parentHuman);
                string bParent = string.IsNullOrEmpty(parentHuman) ? string.Empty : GetMappedBone(b, parentHuman);

                bool aDedicated = NameHasAny(aTwist, "twist") && !string.IsNullOrEmpty(aTwist);
                bool bDedicated = NameHasAny(bTwist, "twist") && !string.IsNullOrEmpty(bTwist);

                bool aSameAsParent = !string.IsNullOrEmpty(aParent) && string.Equals(aTwist, aParent, StringComparison.Ordinal);
                bool bSameAsParent = !string.IsNullOrEmpty(bParent) && string.Equals(bTwist, bParent, StringComparison.Ordinal);

                bool meaningful =
                    (aDedicated != bDedicated) ||
                    (aSameAsParent != bSameAsParent) ||
                    (string.IsNullOrEmpty(aTwist) != string.IsNullOrEmpty(bTwist));

                if (!meaningful)
                    return;

                string details =
                    "A " + twistHuman + "=" + ToDisplay(aTwist) + ", B " + twistHuman + "=" + ToDisplay(bTwist) + ".";

                if (!string.IsNullOrEmpty(parentHuman))
                    details += " Parent " + parentHuman + " (A=" + ToDisplay(aParent) + ", B=" + ToDisplay(bParent) + ").";

                items.Add(new DiagnosisItem(
                    twistHuman + " mapping differs (twist chain mismatch)",
                    details,
                    "Prefer mapping twist bones to dedicated '*Twist' bones if the rig has them. If the rig has no twist bones, leave twist mappings empty and rely on twist sliders.",
                    9f));
            }

            private static string GetTwistParentHuman(string twistHuman)
            {
                switch (twistHuman)
                {
                    case "LeftUpperArmTwist": return "LeftUpperArm";
                    case "LeftLowerArmTwist": return "LeftLowerArm";
                    case "RightUpperArmTwist": return "RightUpperArm";
                    case "RightLowerArmTwist": return "RightLowerArm";
                    case "LeftUpperLegTwist": return "LeftUpperLeg";
                    case "LeftLowerLegTwist": return "LeftLowerLeg";
                    case "RightUpperLegTwist": return "RightUpperLeg";
                    case "RightLowerLegTwist": return "RightLowerLeg";
                    default: return string.Empty;
                }
            }

            private static string GetMappedBone(Snapshot s, string human)
            {
                if (s.HumanBones != null && s.HumanBones.TryGetValue(human, out HumanBone info))
                    return info.boneName ?? string.Empty;
                return string.Empty;
            }

            private static bool HasSkeletonToken(Snapshot s, string token)
            {
                if (s.Skeleton == null || s.Skeleton.Count == 0)
                    return false;

                foreach (string k in s.Skeleton.Keys)
                {
                    if (k == null) continue;
                    if (k.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0) return true;
                }

                return false;
            }

            private static bool NameHasAny(string name, params string[] tokens)
            {
                if (string.IsNullOrEmpty(name))
                    return false;

                foreach (var token in tokens)
                    if (name.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0)
                        return true;

                return false;
            }

            private static string ToDisplay(string s) => string.IsNullOrEmpty(s) ? "(none)" : s;

            private static string GetRigSignature(Snapshot s)
            {
                foreach (HumanBone hb in s.HumanBones.Values)
                {
                    string n = hb.boneName;
                    if (string.IsNullOrEmpty(n)) continue;

                    if (n.StartsWith("CC_Base_", StringComparison.OrdinalIgnoreCase) || n.IndexOf("CC_Base_", StringComparison.OrdinalIgnoreCase) >= 0) return "CC";
                    if (n.IndexOf("Bip01", StringComparison.OrdinalIgnoreCase) >= 0 || n.IndexOf("Bip_", StringComparison.OrdinalIgnoreCase) >= 0) return "Bip";
                    if (n.IndexOf("mixamorig", StringComparison.OrdinalIgnoreCase) >= 0) return "Mixamo";
                }

                foreach (string k in s.Skeleton.Keys)
                {
                    if (string.IsNullOrEmpty(k)) continue;

                    if (k.IndexOf("CC_Base_", StringComparison.OrdinalIgnoreCase) >= 0) return "CC";
                    if (k.IndexOf("Bip01", StringComparison.OrdinalIgnoreCase) >= 0 || k.IndexOf("Bip_", StringComparison.OrdinalIgnoreCase) >= 0) return "Bip";
                    if (k.IndexOf("mixamorig", StringComparison.OrdinalIgnoreCase) >= 0) return "Mixamo";
                }

                return "Unknown";
            }
        }
    }
}
