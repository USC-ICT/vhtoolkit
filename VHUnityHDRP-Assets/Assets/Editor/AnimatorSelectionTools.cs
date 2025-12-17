// Assets/Editor/AnimatorSelectionTools.cs
// Operates on Animator states selected in the Animator window.
// Automatically infers the AnimatorController and its layer from the selection.
// Actions:
//   1) Arrange Selected States In Grid
//   2) Add Back-To-Idle Transitions (from each selected state to a named idle)

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimatorSelectionTools : EditorWindow
{
    // User supplies only the idle state name. Everything else is inferred.
    private string idleStateName = "Standing01_Idle";

    // Layout parameters.
    private int columns = 6;
    private Vector2 startPos = new Vector2(0, 300);
    private Vector2 spacing = new Vector2(220, 50);

    // Exit Transition settings
    [SerializeField] private float m_exitTime = 1.0f;
    [SerializeField] private float m_blendDuration = 0.25f;
    [SerializeField] private bool m_useFixedDuration = true;


    [MenuItem("Ride/VH/Animator Selection Tools")]
    private static void Open() { GetWindow<AnimatorSelectionTools>("Animator Selection Tools"); }

    private void OnGUI()
    {
        // Idle name + helper button to pull from current Animator selection.
        using (new EditorGUILayout.HorizontalScope())
        {
            idleStateName = EditorGUILayout.TextField("Idle State Name", idleStateName);

            // Use the first selected AnimatorState (if any) to populate the field.
            var firstSelectedState = GetSelectedStates().FirstOrDefault();
            using (new EditorGUI.DisabledScope(firstSelectedState == null))
            {
                if (GUILayout.Button("Use Selected", GUILayout.Width(110)))
                {
                    if (firstSelectedState != null)
                        idleStateName = firstSelectedState.name;
                }
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Layout", EditorStyles.boldLabel);
        columns = Mathf.Max(1, EditorGUILayout.IntField("Columns", columns));
        startPos = EditorGUILayout.Vector2Field("Start Position", startPos);
        spacing = EditorGUILayout.Vector2Field("Spacing", spacing);

        // Show what we can infer from the current selection
        InferControllerAndLayer(out var controller, out var layerIndex, out var infoMsg);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Inference", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(infoMsg, MessageType.Info);

        bool ready = (controller != null && layerIndex >= 0);
        using (new EditorGUI.DisabledScope(!ready))
        {
            if (GUILayout.Button("Arrange Selected States In Grid"))
                ArrangeSelectedStates(controller, layerIndex);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Back-to-Idle Transition Settings", EditorStyles.boldLabel);

            m_exitTime = EditorGUILayout.Slider("Exit Time", m_exitTime, 0.0f, 1.0f);

            string durationLabel = m_useFixedDuration
                ? "Blend Duration (sec)"
                : "Blend Duration (normalized)";
            m_blendDuration = EditorGUILayout.FloatField(durationLabel, m_blendDuration);
            if (m_blendDuration < 0f)
                m_blendDuration = 0f;

            m_useFixedDuration = EditorGUILayout.Toggle("Use Fixed Duration", m_useFixedDuration);

            EditorGUILayout.HelpBox("Recommended for short gestures: ExitTime=1.0, Duration=0.15, FixedDuration=true", MessageType.Info);

            EditorGUILayout.Space();

            if (GUILayout.Button("Add Back-To-Idle Transitions For Selected"))
                AddBackTransitionsForSelected(controller, layerIndex);

            if (GUILayout.Button("Update Existing Back-To-Idle Transitions"))
                UpdateBackTransitionsForSelected(controller, layerIndex);

            if (GUILayout.Button("Clear Transitions For Selected"))
                ClearTransitionsForSelected(controller, layerIndex);
        }
    }

    // ---------- Core actions ----------

    private void ArrangeSelectedStates(AnimatorController controller, int layerIndex)
    {
        var root = controller.layers[layerIndex].stateMachine;

        var selectedStates = GetSelectedStates().ToArray();
        if (selectedStates.Length == 0)
        {
            EditorUtility.DisplayDialog("Animator Selection Tools", "Select one or more states in the Animator window.", "OK");
            return;
        }

        Undo.RegisterCompleteObjectUndo(controller, "Arrange Animator States");

        var idle = FindStateByName(root, idleStateName);
        var selectedSet = new HashSet<AnimatorState>(selectedStates);

        int row = 0, col = 0;

        if (idle != null && selectedSet.Contains(idle))
        {
            var owner = FindOwningStateMachine(root, idle);
            if (owner != null)
                SetStatePositionSerialized(owner, idle, startPos + new Vector2(0, 0));
        }

        var selectedStatesSorted = selectedStates
            .Where(s => s != null && (idle == null || s != idle))
            .OrderBy(s => s.name, System.StringComparer.Ordinal);
        foreach (var st in selectedStatesSorted)
        {
            var owner = FindOwningStateMachine(root, st);
            if (owner == null)
                continue;

            var pos = startPos + new Vector2((col + 1) * spacing.x, row * spacing.y);
            SetStatePositionSerialized(owner, st, pos);

            col++;
            if (col >= columns)
            {
                col = 0;
                row++;
            }
        }

        EditorUtility.SetDirty(controller);
        AssetDatabase.SaveAssets();
    }

    private void AddBackTransitionsForSelected(AnimatorController controller, int layerIndex)
    {
        var root = controller.layers[layerIndex].stateMachine;

        var selectedStates = GetSelectedStates().ToArray();
        if (selectedStates.Length == 0)
        {
            EditorUtility.DisplayDialog("Animator Selection Tools", "Select one or more states in the Animator window.", "OK");
            return;
        }

        var idle = FindStateByName(root, idleStateName);
        if (idle == null)
        {
            EditorUtility.DisplayDialog("Animator Selection Tools", "Idle state not found: " + idleStateName, "OK");
            return;
        }

        Undo.RegisterCompleteObjectUndo(controller, "Add Back-To-Idle Transitions");

        foreach (var st in selectedStates)
        {
            if (st == null || st == idle)
                continue;

            // If the state already has any outgoing transitions, assume it is authored to return to idle.
            if (st.transitions != null && st.transitions.Length > 0)
                continue;

            // If a transition to idle exists but is invalid, repair it; otherwise create a new one.
            var toIdle = st.transitions.FirstOrDefault(t => t != null && t.destinationState == idle);
            if (toIdle == null)
                toIdle = st.AddTransition(idle);

            // Configure the transition using UI settings
            toIdle.hasExitTime = true;
            toIdle.exitTime = Mathf.Clamp01(m_exitTime);
            toIdle.hasFixedDuration = m_useFixedDuration;
            toIdle.duration = Mathf.Max(0f, m_blendDuration);
            toIdle.offset = 0.0f;
            toIdle.interruptionSource = TransitionInterruptionSource.None;
            toIdle.orderedInterruption = false;
            toIdle.canTransitionToSelf = false;

            // Remove any leftover conditions (idle transition should be unconditional)
            foreach (var c in toIdle.conditions.ToArray())
                toIdle.RemoveCondition(c);

            // mark the transition asset dirty so settings persist.
            EditorUtility.SetDirty(toIdle);
        }

        EditorUtility.SetDirty(controller);
        AssetDatabase.SaveAssets();
    }

    private void UpdateBackTransitionsForSelected(AnimatorController controller, int layerIndex)
    {
        var root = controller.layers[layerIndex].stateMachine;

        var selectedStates = GetSelectedStates().ToArray();
        if (selectedStates.Length == 0)
        {
            EditorUtility.DisplayDialog("Animator Selection Tools", "Select one or more states in the Animator window.", "OK");
            return;
        }

        Undo.RegisterCompleteObjectUndo(controller, "Update Back-To-Idle Transitions");

        foreach (var st in selectedStates)
        {
            if (st == null)
                continue;

            var transitions = st.transitions;
            if (transitions == null || transitions.Length == 0)
                continue;

            foreach (var toIdle in transitions)
            {
                if (toIdle == null)
                    continue;

                // Apply updated user settings
                toIdle.hasExitTime = true;
                toIdle.exitTime = Mathf.Clamp01(m_exitTime);
                toIdle.hasFixedDuration = m_useFixedDuration;
                toIdle.duration = Mathf.Max(0f, m_blendDuration);
                toIdle.offset = 0.0f;
                toIdle.interruptionSource = TransitionInterruptionSource.None;
                toIdle.orderedInterruption = false;
                toIdle.canTransitionToSelf = false;

                // Always remove conditions for idle-return transitions
                foreach (var c in toIdle.conditions.ToArray())
                    toIdle.RemoveCondition(c);

                EditorUtility.SetDirty(toIdle);
            }
        }

        EditorUtility.SetDirty(controller);
        AssetDatabase.SaveAssets();
    }

    private void ClearTransitionsForSelected(AnimatorController controller, int layerIndex)
    {
        var root = controller.layers[layerIndex].stateMachine;

        var selectedStates = GetSelectedStates().ToArray();
        if (selectedStates.Length == 0)
        {
            EditorUtility.DisplayDialog("Animator Selection Tools", "Select one or more states in the Animator window.", "OK");
            return;
        }

        Undo.RegisterCompleteObjectUndo(controller, "Clear Transitions");

        foreach (var st in selectedStates)
        {
            if (st == null)
                continue;

            for (int i = st.transitions.Length - 1; i >= 0; i--)
                st.RemoveTransition(st.transitions[i]);
        }

        EditorUtility.SetDirty(controller);
        AssetDatabase.SaveAssets();
    }

    // Infers the controller and the most likely layer from the current selection.
    private void InferControllerAndLayer(out AnimatorController controller, out int layerIndex, out string info)
    {
        controller = null;
        layerIndex = -1;
        info = "No AnimatorState selected.";

        var states = GetSelectedStates().ToArray();
        if (states.Length == 0)
            return;

        // 1) All selected states must belong to the same controller asset.
        // AnimatorState is a sub-asset of the controller. We can resolve by asset path.
        var byPath = new Dictionary<string, List<AnimatorState>>();
        foreach (var s in states)
        {
            var path = AssetDatabase.GetAssetPath(s);
            if (string.IsNullOrEmpty(path))
            {
                info = "Selected states are not asset-backed AnimatorStates.";
                return;
            }

            if (!byPath.TryGetValue(path, out var list))
            {
                list = new List<AnimatorState>();
                byPath[path] = list;
            }

            list.Add(s);
        }

        if (byPath.Count != 1)
        {
            info = "Selection spans multiple controller assets. Select states from a single controller.";
            return;
        }

        var controllerPath = byPath.Keys.First();
        controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
        if (controller == null)
        {
            info = "Could not load AnimatorController at path: " + controllerPath;
            return;
        }

        // 2) Determine which layer owns the majority of selected states.
        var rootMachines = controller.layers.Select(l => l.stateMachine).ToArray();
        int bestLayer = -1;
        int bestCount = -1;

        for (int i = 0; i < rootMachines.Length; i++)
        {
            int count = 0;
            foreach (var s in states)
            {
                if (FindOwningStateMachine(rootMachines[i], s) != null)
                    count++;
            }

            if (count > bestCount)
            {
                bestCount = count;
                bestLayer = i;
            }
        }

        if (bestLayer < 0 || bestCount == 0)
        {
            info = "Could not match selection to any layer in controller: " + controller.name;
            controller = null;
            return;
        }

        layerIndex = bestLayer;

        // 3) Build a friendly info line.
        var stateNames = string.Join(", ", states.Select(s => s.name).Take(3));
        if (states.Length > 3) stateNames += ", ...";
        info = $"Controller: {controller.name}   Layer: {controller.layers[layerIndex].name}"
             + $"   Selected: {states.Length} states ({stateNames})";
    }

    // ---------- Helpers ----------

    private static IEnumerable<AnimatorState> GetSelectedStates()
    {
        // Animator window selection yields AnimatorState instances.
        return Selection.objects.OfType<AnimatorState>().Distinct();
    }

    private static AnimatorState FindStateByName(AnimatorStateMachine root, string name)
    {
        if (root == null || string.IsNullOrEmpty(name))
            return null;

        // Search direct states
        foreach (var child in root.states)
        {
            if (child.state != null && child.state.name == name)
                return child.state;
        }

        // Search sub state machines
        foreach (var sm in root.stateMachines)
        {
            var found = FindStateByName(sm.stateMachine, name);
            if (found != null) return found;
        }

        return null;
    }

    private static AnimatorStateMachine FindOwningStateMachine(AnimatorStateMachine root, AnimatorState target)
    {
        if (root == null || target == null)
            return null;

        foreach (var child in root.states)
        {
            if (child.state == target)
                return root;
        }

        foreach (var sm in root.stateMachines)
        {
            var owner = FindOwningStateMachine(sm.stateMachine, target);
            if (owner != null)
                return owner;
        }

        return null;
    }

    // Sets a state's position by editing serialized data (works across Unity versions).
    private static void SetStatePositionSerialized(AnimatorStateMachine owner, AnimatorState state, Vector2 pos2D)
    {
        if (owner == null || state == null)
            return;

        var so = new SerializedObject(owner);
        var childStates = so.FindProperty("m_ChildStates");
        if (childStates == null || !childStates.isArray)
            return;

        Vector3 pos3 = (Vector3)pos2D;

        for (int i = 0; i < childStates.arraySize; i++)
        {
            var element = childStates.GetArrayElementAtIndex(i);
            var stateProp = element.FindPropertyRelative("m_State");
            if (stateProp != null && stateProp.objectReferenceValue == state)
            {
                var posProp = element.FindPropertyRelative("m_Position");
                if (posProp != null)
                    posProp.vector3Value = pos3;

                so.ApplyModifiedProperties();
                return;
            }
        }
    }
}
