// Assets/Editor/FbxClipRenamerWindow.cs
// Batch-rename FBX-embedded AnimationClips via ModelImporter.clipAnimations.
// Works on the current Project selection (FBX files or folders).
// Style and UX are similar to your AnimatorSelectionTools window.
//
// Usage:
// - Select one or more FBX assets and/or folders in the Project window.
// - Open the window: Ride/VH/FBX Clip Renamer
// - Choose a Rename Mode and options.
// - Click "Preview" to see what would change (Console).
// - Click "Apply Rename" to write new names into the .fbx.meta (import settings).
// - Optional: "Revert To Importer Defaults" clears explicit clipAnimations to defaults.
//
// Notes:
// - Changes are stored in the FBX .meta (ModelImporter settings) and survive reimports.
// - The FBX file itself is not modified.
// - Commit meta files if you use version control.

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FbxClipRenamerWindow : EditorWindow
{
    private enum RenameMode
    {
        PrefixFromTake,     // Take "Ver1@Standing01" -> prefix "Ver1", base "Standing01" -> "Ver1_Standing01"
        CustomPrefix,       // Use user-specified prefix + "_" + base
        ReplaceAtWithUnderscore, // Replace '@' with '_' inside current clip name only
        KeepBaseName        // Keep current clip name (useful if you only want to promote defaults to explicit)
    }

    [Serializable]
    private class Options
    {
        public RenameMode Mode = RenameMode.PrefixFromTake;
        public string CustomPrefix = "VerX";
        public string Joiner = "_";
        public bool PromoteDefaultsWhenEmpty = true; // if importer has no clipAnimations, copy defaultClipAnimations and rename those
        public bool SkipIfNameAlreadyMatches = true;
        public bool RecurseSelectedFolders = true;
        public bool SortPreviewByAsset = true;
    }

    private Options m_Options = new Options();

    // Cached selection scan
    private List<string> m_FbxPaths = new List<string>();
    private int m_TotalClipCount;
    private int m_AfterRenameDifferentCount;

    [MenuItem("Ride/VH/FBX Clip Renamer")]
    private static void Open() { GetWindow<FbxClipRenamerWindow>("FBX Clip Renamer"); }

    private void OnFocus()
    {
        RefreshSelectionSummary();
    }

    private void OnSelectionChange()
    {
        RefreshSelectionSummary();
        Repaint();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Selection", EditorStyles.boldLabel);
        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
        {
            EditorGUILayout.LabelField($"FBX Assets Found: {m_FbxPaths.Count}");
            EditorGUILayout.LabelField($"Total Clips (current import view): {m_TotalClipCount}");
            EditorGUILayout.LabelField($"Preview: Renamed (different): {m_AfterRenameDifferentCount}");
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Rename Options", EditorStyles.boldLabel);
        m_Options.Mode = (RenameMode)EditorGUILayout.EnumPopup("Rename Mode", m_Options.Mode);

        if (m_Options.Mode == RenameMode.CustomPrefix)
        {
            m_Options.CustomPrefix = EditorGUILayout.TextField("Custom Prefix", m_Options.CustomPrefix);
        }

        if (m_Options.Mode == RenameMode.PrefixFromTake || m_Options.Mode == RenameMode.CustomPrefix)
        {
            m_Options.Joiner = EditorGUILayout.TextField("Joiner", m_Options.Joiner);
            if (string.IsNullOrEmpty(m_Options.Joiner)) m_Options.Joiner = "_";
        }

        // Inline preview label (first selected asset's first clip)
        {
            var sample = InlineSamplePreview(); // recompute each OnGUI so it reacts to option changes
            if (!string.IsNullOrEmpty(sample))
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    EditorGUILayout.LabelField("Example rename (from first selected FBX):", EditorStyles.miniBoldLabel);
                    EditorGUILayout.LabelField(sample, EditorStyles.miniLabel);
                }
            }
            else
            {
                // Keep layout consistent; show an empty spacer when nothing selected
                GUILayout.Space(4);
            }
        }

        m_Options.PromoteDefaultsWhenEmpty = EditorGUILayout.ToggleLeft("Promote defaults to explicit clips (when none are set)", m_Options.PromoteDefaultsWhenEmpty);
        m_Options.SkipIfNameAlreadyMatches = EditorGUILayout.ToggleLeft("Skip clips whose name already matches target", m_Options.SkipIfNameAlreadyMatches);
        m_Options.RecurseSelectedFolders = EditorGUILayout.ToggleLeft("Recurse into selected folders", m_Options.RecurseSelectedFolders);
        m_Options.SortPreviewByAsset = EditorGUILayout.ToggleLeft("Sort preview by asset path", m_Options.SortPreviewByAsset);

        EditorGUILayout.Space();

        using (new EditorGUI.DisabledScope(m_FbxPaths.Count == 0))
        {
            if (GUILayout.Button("Preview"))
            {
                DoPreview();
            }

            if (GUILayout.Button("Apply Rename"))
            {
                DoApplyRename();
                RefreshSelectionSummary();
            }

            if (GUILayout.Button("Revert To Importer Defaults (Selected)"))
            {
                if (EditorUtility.DisplayDialog("Revert To Defaults",
                    "This will clear explicit clipAnimations so the importer uses its defaults. Continue?",
                    "Revert", "Cancel"))
                {
                    RevertToDefaults();
                    RefreshSelectionSummary();
                }
            }
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Refresh Selection Summary"))
        {
            RefreshSelectionSummary();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("How it works", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "• Operates entirely on the ModelImporter (stored in the .fbx.meta).\n" +
            "• FBX file data is unchanged.\n" +
            "• Commit .meta files to share new names with your team.",
            MessageType.Info);
    }

    private void RefreshSelectionSummary()
    {
        m_FbxPaths.Clear();
        m_TotalClipCount = 0;
        m_AfterRenameDifferentCount = 0;

        var guids = Selection.assetGUIDs;
        if (guids == null || guids.Length == 0)
            return;

        var set = new HashSet<string>(StringComparer.Ordinal);

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(path)) continue;

            if (Directory.Exists(path))
            {
                if (!m_Options.RecurseSelectedFolders) continue;
                var folderGuids = AssetDatabase.FindAssets("t:Model", new[] { path });
                foreach (var fg in folderGuids)
                {
                    var fpath = AssetDatabase.GUIDToAssetPath(fg);
                    if (IsFbx(fpath) && set.Add(fpath))
                        m_FbxPaths.Add(fpath);
                }
            }
            else
            {
                if (IsFbx(path) && set.Add(path))
                    m_FbxPaths.Add(path);
            }
        }

        // Count clips and estimate different-after-rename
        foreach (var p in m_FbxPaths)
        {
            var importer = AssetImporter.GetAtPath(p) as ModelImporter;
            if (importer == null) continue;

            var clips = importer.clipAnimations;
            //bool usedDefaults = false;
            if (clips == null || clips.Length == 0)
            {
                clips = importer.defaultClipAnimations;
                //usedDefaults = true;
            }

            m_TotalClipCount += clips.Length;

            foreach (var c in clips)
            {
                var target = BuildTargetName(c, m_Options);
                if (!string.Equals(c.name, target, StringComparison.Ordinal))
                    m_AfterRenameDifferentCount++;
            }
        }

        if (m_Options.SortPreviewByAsset)
            m_FbxPaths.Sort(StringComparer.Ordinal);
    }

    private void DoPreview()
    {
        if (m_FbxPaths.Count == 0)
        {
            Debug.LogWarning("FBX Clip Renamer: Nothing selected.");
            return;
        }

        int diff = 0;
        foreach (var path in m_FbxPaths)
        {
            var importer = AssetImporter.GetAtPath(path) as ModelImporter;
            if (importer == null) continue;

            var clips = importer.clipAnimations;
            //bool usingDefaults = false;
            if (clips == null || clips.Length == 0)
            {
                clips = importer.defaultClipAnimations;
                //usingDefaults = true;
            }

            if (clips == null || clips.Length == 0) continue;

            bool any = false;
            for (int i = 0; i < clips.Length; i++)
            {
                var c = clips[i];
                var target = BuildTargetName(importer, c, m_Options);

                if (!string.Equals(c.name, target, StringComparison.Ordinal))
                {
                    if (!any)
                    {
                        //Debug.Log($"[Preview] {path}");
                        any = true;
                    }
                    Debug.Log($"   {c.name}  ->  {target}   (take: {c.takeName})");
                    diff++;
                }
            }
        }

        if (diff == 0)
            Debug.Log("FBX Clip Renamer: Preview found no differences.");
    }

    private void DoApplyRename()
    {
        if (m_FbxPaths.Count == 0)
        {
            Debug.LogWarning("FBX Clip Renamer: Nothing selected.");
            return;
        }

        int changedFiles = 0;
        int changedClips = 0;

        AssetDatabase.StartAssetEditing();
        try
        {
            foreach (var path in m_FbxPaths)
            {
                var importer = AssetImporter.GetAtPath(path) as ModelImporter;
                if (importer == null) continue;

                var clips = importer.clipAnimations;
                //bool usingDefaults = false;
                if (clips == null || clips.Length == 0)
                {
                    clips = importer.defaultClipAnimations;
                    //usingDefaults = true;

                    if ((clips == null || clips.Length == 0) && !m_Options.PromoteDefaultsWhenEmpty)
                        continue; // nothing to rename and not promoting
                }

                if (clips == null || clips.Length == 0)
                    continue;

                bool fileChanged = false;

                for (int i = 0; i < clips.Length; i++)
                {
                    var c = clips[i];
                    string target = BuildTargetName(importer, c, m_Options);

                    if (m_Options.SkipIfNameAlreadyMatches && string.Equals(c.name, target, StringComparison.Ordinal))
                        continue;

                    if (!string.Equals(c.name, target, StringComparison.Ordinal))
                    {
                        c.name = target;
                        clips[i] = c;
                        fileChanged = true;
                        changedClips++;
                    }
                }

                if (fileChanged)
                {
                    // If we started from defaults, we must promote them to explicit to persist our edits.
                    //if (usingDefaults || importer.clipAnimations == null || importer.clipAnimations.Length == 0)
                    importer.clipAnimations = clips;

                    importer.SaveAndReimport();
                    changedFiles++;
                    Debug.Log($"Renamed {path}");
                }
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
        }

        Debug.Log($"FBX Clip Renamer: Done. Files changed: {changedFiles}, Clips renamed: {changedClips}");
    }

    private void RevertToDefaults()
    {
        if (m_FbxPaths.Count == 0) return;

        int changedFiles = 0;

        AssetDatabase.StartAssetEditing();
        try
        {
            foreach (var path in m_FbxPaths)
            {
                var importer = AssetImporter.GetAtPath(path) as ModelImporter;
                if (importer == null) continue;

                if (importer.clipAnimations != null && importer.clipAnimations.Length > 0)
                {
                    importer.clipAnimations = Array.Empty<ModelImporterClipAnimation>();
                    importer.SaveAndReimport();
                    changedFiles++;
                    Debug.Log($"Reverted to defaults: {path}");
                }
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
        }

        Debug.Log($"FBX Clip Renamer: Reverted {changedFiles} file(s) to importer defaults.");
    }

    private string InlineSamplePreview()
    {
        if (m_FbxPaths == null || m_FbxPaths.Count == 0) return string.Empty;

        var path = m_FbxPaths[0];
        var importer = AssetImporter.GetAtPath(path) as ModelImporter;
        if (importer == null) return string.Empty;

        var clips = importer.clipAnimations;
        if (clips == null || clips.Length == 0)
            clips = importer.defaultClipAnimations;

        if (clips == null || clips.Length == 0) return string.Empty;

        // Use the first clip as the sample
        var c = clips[0];

        // Current name (fallback to base name derived from take if empty)
        var current = string.IsNullOrEmpty(c.name) ? ExtractBaseName(c.name, c.takeName) : c.name;

        // Target per current options
        var target = BuildTargetName(importer, c, m_Options);

        if (string.IsNullOrEmpty(current) || string.IsNullOrEmpty(target)) return string.Empty;

        return current + " -> " + target;
    }

    // ---------- Naming logic ----------

    private static string BuildTargetName(ModelImporter importer, ModelImporterClipAnimation clip, Options opt)
    {
        // Try explicit clip take first
        string take = clip.takeName;

        if (string.IsNullOrEmpty(take) && importer != null)
        {
            var defaults = importer.defaultClipAnimations;
            if (defaults != null && defaults.Length > 0)
            {
                ModelImporterClipAnimation best = null;

                // 1) tolerant frame-range match
                foreach (var d in defaults)
                {
                    if (FramesMatch(clip, d)) { best = d; break; }
                }

                // 2) fallback: duration match
                if (best == null)
                {
                    foreach (var d in defaults)
                    {
                        if (DurationMatch(clip, d)) { best = d; break; }
                    }
                }

                if (best != null)
                {
                    take = best.takeName;
                }
            }
        }

        if (string.IsNullOrEmpty(ExtractPrefixFromTake(take)))
        {
            var fb = ExtractPrefixFromAssetPath(importer);      // e.g., "Ver1_RunPack" or "Ver1"
            if (!string.IsNullOrEmpty(fb))
            {
                // Synthesize a "take-like" string so the core logic can extract the prefix
                // Base name will still come from clip.name (your ExtractBaseName prefers clip.name)
                take = fb + "@";
            }
        }

        return BuildTargetNameCore(clip, opt, take);
    }

    private static string BuildTargetName(ModelImporterClipAnimation clip, Options opt)
    {
        return BuildTargetNameCore(clip, opt, clip.takeName);
    }

    private static string BuildTargetNameCore(ModelImporterClipAnimation clip, Options opt, string effectiveTakeName)
    {
        switch (opt.Mode)
        {
            case RenameMode.PrefixFromTake:
            {
                var prefix  = ExtractPrefixFromTake(effectiveTakeName);
                var baseName = ExtractBaseName(clip.name, effectiveTakeName);
                if (string.IsNullOrEmpty(prefix)) return baseName;
                return prefix + opt.Joiner + baseName;
            }

            case RenameMode.CustomPrefix:
            {
                var baseName = ExtractBaseName(clip.name, effectiveTakeName);
                var p = opt.CustomPrefix ?? string.Empty;
                if (p.Length == 0) return baseName;
                return p + opt.Joiner + baseName;
            }

            case RenameMode.ReplaceAtWithUnderscore:
            {
                // (Optional enhancement you asked about)
                // Keep current behavior: literal '@' -> '_'
                var current = string.IsNullOrEmpty(clip.name) ? "Clip" : clip.name;
                return current.Replace('@', '_');
            }

            case RenameMode.KeepBaseName:
            default:
            {
                return string.IsNullOrEmpty(clip.name) ? ExtractBaseName(clip.name, effectiveTakeName) : clip.name;
            }
        }
    }

    // "Ver1@Standing01" -> "Ver1"
    private static string ExtractPrefixFromTake(string takeName)
    {
        if (string.IsNullOrEmpty(takeName)) return string.Empty;
        int at = takeName.IndexOf('@');
        if (at > 0) return takeName.Substring(0, at);
        return string.Empty;
    }

    // Prefer existing clip.name if present; else right side of takeName; else whole takeName; else "Clip"
    private static string ExtractBaseName(string currentClipName, string takeName)
    {
        if (!string.IsNullOrEmpty(currentClipName))
            return currentClipName;

        if (!string.IsNullOrEmpty(takeName))
        {
            int at = takeName.IndexOf('@');
            if (at >= 0 && at + 1 < takeName.Length)
                return takeName.Substring(at + 1);
            return takeName;
        }

        return "Clip";
    }

    private static bool IsFbx(string path)
    {
        var ext = Path.GetExtension(path);
        return string.Equals(ext, ".fbx", StringComparison.OrdinalIgnoreCase);
    }

    private static bool RoughlyEqual(double a, double b, double eps = 1e-3) => Math.Abs(a - b) <= eps;
    private static bool FramesMatch(ModelImporterClipAnimation a, ModelImporterClipAnimation b)
        => RoughlyEqual(a.firstFrame, b.firstFrame, 1e-3) && RoughlyEqual(a.lastFrame, b.lastFrame, 1e-3);

    private static bool DurationMatch(ModelImporterClipAnimation a, ModelImporterClipAnimation b)
    {
        var ad = a.lastFrame - a.firstFrame;
        var bd = b.lastFrame - b.firstFrame;
        return RoughlyEqual(ad, bd, 1e-3);
    }

    private static string ExtractPrefixFromAssetPath(ModelImporter importer)
    {
        if (importer == null) return string.Empty;
        var file = Path.GetFileNameWithoutExtension(importer.assetPath);
        return string.IsNullOrEmpty(file) ? string.Empty : file;
    }
}
