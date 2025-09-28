using UnityEditor;
using UnityEngine;
using VHAssets;

public class VhComponentSetupEditor : EditorWindow
{
    private GameObject m_targetObject;
    private string m_leftEyeName = string.Empty;
    private string m_rightEyeName = string.Empty;
    private string m_neckTransformName = string.Empty;


    [MenuItem("Ride/VH Component Setup")]
    public static void ShowWindow()
    {
        GetWindow<VhComponentSetupEditor>("Add VH Components");
    }

    private void OnGUI()
    {
        GUILayout.Label("Drag a scene GameObject here", EditorStyles.boldLabel);
        m_targetObject = (GameObject)EditorGUILayout.ObjectField("Target GameObject", m_targetObject, typeof(GameObject), true);

        GUILayout.Space(10);

        GUILayout.Label("Saccade Controller Settings", EditorStyles.boldLabel);
        m_leftEyeName = EditorGUILayout.TextField("Left Eye", m_leftEyeName);
        m_rightEyeName = EditorGUILayout.TextField("Right Eye", m_rightEyeName);

        GUILayout.Space(10);
        GUILayout.Label("Head Controller Settings", EditorStyles.boldLabel);
        m_neckTransformName = EditorGUILayout.TextField("Neck Transform", m_neckTransformName);



        if (m_targetObject == null || m_leftEyeName == string.Empty || m_leftEyeName == string.Empty || m_neckTransformName == string.Empty)
        {
            EditorGUILayout.HelpBox
                ("Assign a GameObject from the scene.\n" +
                "All name fields are required", MessageType.Info);
            return;
        }


        if (PrefabUtility.IsPartOfPrefabAsset(m_targetObject))
        {
            EditorGUILayout.HelpBox("Please drag a GameObject from the scene (not a prefab asset).", MessageType.Error);
        }
        else if (GUILayout.Button("Add Components"))
        {
            AddComponentsToGameObject(m_targetObject);
        }

    }

    private void AddComponentsToGameObject(GameObject go)
    {
        Undo.RegisterCompleteObjectUndo(go, "Add Components");

        AddIfMissing<MecanimCharacter>(go);
        AddIfMissing<GazeController_IK>(go);
        AddSaccadeController(go);
        AddHeadController(go);
        //AddIfMissing<SaccadeController>(go);
        //AddIfMissing<HeadController>(go);
        AddIfMissing<BlinkController>(go);
        AddIfMissing<ListeningController>(go);
        AddIfMissing<MirroringController>(go);

        Debug.Log("Components added to scene GameObject.");
    }

    private void AddIfMissing<T>(GameObject go) where T : Component
    {
        if (go.GetComponent<T>() == null)
        {
            go.AddComponent<T>();
        }
    }

    private void AddSaccadeController(GameObject go)
    {
        var existing = go.GetComponent<SaccadeController>();
        if (existing != null)
            return;

        var saccade = go.AddComponent<SaccadeController>();

        SerializedObject so = new SerializedObject(saccade);
        SerializedProperty arrayProp = so.FindProperty("m_EyeTransformNames");

        if (arrayProp != null && arrayProp.isArray)
        {
            so.Update();

            arrayProp.arraySize = 2;

            SerializedProperty element0 = arrayProp.GetArrayElementAtIndex(0);
            SetEyeTransformData(element0, m_leftEyeName, false);

            SerializedProperty element1 = arrayProp.GetArrayElementAtIndex(1);
            SetEyeTransformData(element1, m_rightEyeName, true);

            so.ApplyModifiedProperties();
        }
        else
        {
            Debug.LogWarning("Could not find 'm_EyeTransformNames' property in SaccadeController.");
        }
    }

    private void SetEyeTransformData(SerializedProperty property, string name, bool isRight)
    {
        SerializedProperty transformName = property.FindPropertyRelative("transformName");
        SerializedProperty isRightEye = property.FindPropertyRelative("isRightEye");

        if (transformName != null) transformName.stringValue = name;
        if (isRightEye != null) isRightEye.boolValue = isRight;
    }

    private void AddHeadController(GameObject go)
    {
        var existing = go.GetComponent<HeadController>();
        if (existing != null)
            return;

        var headController = go.AddComponent<HeadController>();
        SerializedObject so = new SerializedObject(headController);
        SerializedProperty neckProp = so.FindProperty("m_NeckTransformName");

        if (neckProp != null)
        {
            so.Update();
            neckProp.stringValue = m_neckTransformName;
            so.ApplyModifiedProperties();
        }
        else
        {
            Debug.LogWarning("Could not find 'm_NeckTransformName' in HeadController.");
        }
    }
}
