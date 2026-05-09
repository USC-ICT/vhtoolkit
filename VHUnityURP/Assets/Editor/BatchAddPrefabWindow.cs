using UnityEditor;
using UnityEngine;

public sealed class BatchAddPrefabWindow : EditorWindow
{
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private bool m_matchParentLayer = true;
    [SerializeField] private bool m_resetLocalTransform = true;

    [MenuItem("Tools/Batch Add Prefab To Selection")]
    public static void ShowWindow()
    {
        BatchAddPrefabWindow window = GetWindow<BatchAddPrefabWindow>("Batch Add Prefab");
        window.minSize = new Vector2(360, 140);
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Instantiate a prefab under every selected GameObject.", EditorStyles.wordWrappedLabel);
        EditorGUILayout.Space();

        m_prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", m_prefab, typeof(GameObject), false);
        m_matchParentLayer = EditorGUILayout.ToggleLeft("Match parent layer", m_matchParentLayer);
        m_resetLocalTransform = EditorGUILayout.ToggleLeft("Reset local transform", m_resetLocalTransform);

        using (new EditorGUI.DisabledScope(m_prefab == null || Selection.gameObjects.Length == 0))
        {
            if (GUILayout.Button($"Add To Selection ({Selection.gameObjects.Length})"))
            {
                AddPrefabToSelection();
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Select your 20 parent GameObjects in the Hierarchy, pick a prefab above, then click Add.", MessageType.Info);
    }

    private void AddPrefabToSelection()
    {
        if (m_prefab == null)
            return;

        GameObject[] parents = Selection.gameObjects;
        Undo.IncrementCurrentGroup();
        int group = Undo.GetCurrentGroup();

        foreach (GameObject parent in parents)
        {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(m_prefab, parent.transform);
            Undo.RegisterCreatedObjectUndo(instance, "Batch Add Prefab");

            if (m_resetLocalTransform)
            {
                instance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                instance.transform.localScale = Vector3.one;
            }

            if (m_matchParentLayer)
                instance.layer = parent.layer;
        }

        Undo.CollapseUndoOperations(group);
    }
}
