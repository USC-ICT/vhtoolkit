using System;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;
using VH;

public sealed class BatchFixLoadingSetupWindow : EditorWindow
{
    [SerializeField] private bool m_warnIfMissing = true;
    [SerializeField] private bool m_resetFillQuadLocalScale = false;

    [MenuItem("Tools/Batch Fix Loading Setup (Hardcoded)")]
    public static void ShowWindow()
    {
        BatchFixLoadingSetupWindow window = GetWindow<BatchFixLoadingSetupWindow>("Fix Loading Setup");
        window.minSize = new Vector2(460, 180);
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Applies hard-coded hookups for each selected root GameObject.", EditorStyles.wordWrappedLabel);
        EditorGUILayout.Space();

        m_warnIfMissing = EditorGUILayout.ToggleLeft("Warn if something is missing", m_warnIfMissing);
        m_resetFillQuadLocalScale = EditorGUILayout.ToggleLeft("Reset FillQuad localScale to (1,1,1)", m_resetFillQuadLocalScale);

        using (new EditorGUI.DisabledScope(Selection.gameObjects == null || Selection.gameObjects.Length == 0))
        {
            if (GUILayout.Button($"Apply To Selection ({Selection.gameObjects.Length})"))
                Apply();
        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox(
            "For each selected GameObject:\n" +
            "1) LoadingProgressText: m_text = child \"LoadingText\" (TextMeshPro), m_fillQuad = child \"LoadingBarFill\" (Transform)\n" +
            "2) SkinnedMeshAlphaController: Renderer Root Override = child \"LoadingPlaceholder\"\n" +
            "3) RideCatalogAsset: Placeholder Object = child \"LoadingPlaceholder\"",
            MessageType.Info);
    }

    private void Apply()
    {
        GameObject[] roots = Selection.gameObjects;
        if (roots == null || roots.Length == 0)
            return;

        Undo.IncrementCurrentGroup();
        int undoGroup = Undo.GetCurrentGroup();

        int modifiedRoots = 0;

        foreach (GameObject root in roots)
        {
            if (root == null)
                continue;

            Transform tLoadingText = FindChildByName(root.transform, "LoadingText");
            Transform tLoadingBarFill = FindChildByName(root.transform, "LoadingBarFill");
            Transform tLoadingPlaceholder = FindChildByName(root.transform, "LoadingPlaceholder");

            bool modifiedThisRoot = false;

            // 1) LoadingProgressText
            LoadingProgressText lpt = root.GetComponent<LoadingProgressText>();
            if (lpt == null)
            {
                Warn(root, "Missing LoadingProgressText component.");
            }
            else
            {
                TextMeshPro tmp = null;
                if (tLoadingText != null)
                {
                    tmp = tLoadingText.GetComponent<TextMeshPro>();
                    if (tmp == null)
                        Warn(root, "Child \"LoadingText\" exists, but has no TextMeshPro component.");
                }
                else
                {
                    Warn(root, "Could not find child named \"LoadingText\".");
                }

                if (tLoadingBarFill == null)
                    Warn(root, "Could not find child named \"LoadingBarFill\".");

                Undo.RecordObject(lpt, "Batch Fix LoadingProgressText");

                bool changed = false;

                // Set m_text (TextMeshPro)
                if (GetPrivateFieldValue<TextMeshPro>(lpt, "m_text") != tmp)
                {
                    SetPrivateFieldValue(lpt, "m_text", tmp);
                    changed = true;
                }

                // Set m_fillQuad (Transform)
                if (GetPrivateFieldValue<Transform>(lpt, "m_fillQuad") != tLoadingBarFill)
                {
                    SetPrivateFieldValue(lpt, "m_fillQuad", tLoadingBarFill);
                    changed = true;
                }

                if (changed)
                {
                    EditorUtility.SetDirty(lpt);
                    modifiedThisRoot = true;
                }

                if (m_resetFillQuadLocalScale && tLoadingBarFill != null)
                {
                    Undo.RecordObject(tLoadingBarFill, "Reset FillQuad Scale");
                    tLoadingBarFill.localScale = Vector3.one;
                }
            }

            // 2) SkinnedMeshAlphaController.Renderer Root Override = LoadingPlaceholder
            if (TryAssignOnComponent(
                root,
                componentTypeName: "SkinnedMeshAlphaController",
                fieldName: "m_rendererRootOverride", // adjust if your actual backing field name differs
                value: tLoadingPlaceholder != null ? (UnityEngine.Object)tLoadingPlaceholder.gameObject : null,
                warnContext: "SkinnedMeshAlphaController",
                onModified: () => modifiedThisRoot = true))
            {
                // success (or at least component existed and we attempted assignment)
            }

            // 3) RideCatalogAsset.Placeholder Object = LoadingPlaceholder
            if (TryAssignOnComponent(
                root,
                componentTypeName: "RideCatalogAsset",
                fieldName: "m_placeholderObject", // adjust if your actual backing field name differs
                value: tLoadingPlaceholder != null ? (UnityEngine.Object)tLoadingPlaceholder.gameObject : null,
                warnContext: "RideCatalogAsset",
                onModified: () => modifiedThisRoot = true))
            {
                // success
            }

            if (modifiedThisRoot)
                modifiedRoots++;
        }

        Undo.CollapseUndoOperations(undoGroup);

        Debug.Log($"Batch Fix Loading Setup: modified {modifiedRoots} root object(s).");
    }

    private void Warn(GameObject context, string message)
    {
        if (m_warnIfMissing)
            Debug.LogWarning($"[{context.name}] {message}", context);
    }

    private static Transform FindChildByName(Transform root, string name)
    {
        if (root == null || string.IsNullOrEmpty(name))
            return null;

        Transform[] all = root.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < all.Length; i++)
        {
            if (string.Equals(all[i].name, name, StringComparison.Ordinal))
                return all[i];
        }

        return null;
    }

    private static T GetPrivateFieldValue<T>(object obj, string fieldName) where T : class
    {
        FieldInfo fi = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        return fi != null ? fi.GetValue(obj) as T : null;
    }

    private static void SetPrivateFieldValue(object obj, string fieldName, object value)
    {
        FieldInfo fi = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        fi?.SetValue(obj, value);
    }

    private bool TryAssignOnComponent(
        GameObject root,
        string componentTypeName,
        string fieldName,
        UnityEngine.Object value,
        string warnContext,
        Action onModified)
    {
        Component c = root.GetComponent(componentTypeName);
        if (c == null)
        {
            Warn(root, $"Missing {warnContext} component.");
            return false;
        }

        // Find LoadingPlaceholder if needed
        if (value == null)
            Warn(root, "Could not find child named \"LoadingPlaceholder\".");

        // Assign private [SerializeField] backing field by name.
        FieldInfo fi = c.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        if (fi == null)
        {
            Warn(root, $"{warnContext}: could not find field \"{fieldName}\" on type {c.GetType().Name}.");
            return true; // component exists; we attempted.
        }

        if (!typeof(UnityEngine.Object).IsAssignableFrom(fi.FieldType))
        {
            Warn(root, $"{warnContext}: field \"{fieldName}\" is not a UnityEngine.Object reference.");
            return true;
        }

        Undo.RecordObject(c, $"Batch Fix {warnContext}");

        UnityEngine.Object current = fi.GetValue(c) as UnityEngine.Object;
        if (current != value)
        {
            fi.SetValue(c, value);
            EditorUtility.SetDirty(c);
            onModified?.Invoke();
        }

        return true;
    }
}
