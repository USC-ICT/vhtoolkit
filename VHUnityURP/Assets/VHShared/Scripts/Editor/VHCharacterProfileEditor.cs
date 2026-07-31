using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom inspector for VHCharacterProfile. For the Safety and Domain sections it shows the
/// resolved shared text (read-only) when Source = Shared Default, the editable field only when
/// Source = Custom, and a note when None - so designers always see what text is actually in effect.
/// A foldout previews the fully composed system prompt the character will send.
/// </summary>
[CustomEditor(typeof(VHCharacterProfile))]
[CanEditMultipleObjects]
public class VHCharacterProfileEditor : Editor
{
    static bool s_showComposed;
    // Foldout state per section label (Domain / Safety). Default false = collapsed.
    static readonly Dictionary<string, bool> s_sharedExpanded = new Dictionary<string, bool>();

    static GUIStyle s_sharedTextStyle;
    static GUIStyle SharedTextStyle
    {
        get
        {
            if (s_sharedTextStyle == null)
                s_sharedTextStyle = new GUIStyle(GUI.skin.textArea) { fontSize = 12, wordWrap = true };
            return s_sharedTextStyle;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty prop = serializedObject.GetIterator();
        bool enterChildren = true;
        while (prop.NextVisible(enterChildren))
        {
            enterChildren = false;
            switch (prop.name)
            {
                case "m_Script":
                    using (new EditorGUI.DisabledScope(true))
                        EditorGUILayout.PropertyField(prop);
                    break;

                // Drawn as part of the source section below; skip the standalone field.
                case "m_domainPrompt":
                case "m_safetyPrompt":
                    break;

                case "m_domainSource":
                    DrawSourceSection(prop, serializedObject.FindProperty("m_domainPrompt"),
                                      VHPrompts.DemoDomain, "Domain");
                    break;

                case "m_safetySource":
                    DrawSourceSection(prop, serializedObject.FindProperty("m_safetyPrompt"),
                                      VHPrompts.BaseSafety, "Safety");
                    break;

                default:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
            }
        }

        DrawComposedPreview();

        serializedObject.ApplyModifiedProperties();
    }

    static void DrawSourceSection(SerializedProperty sourceProp, SerializedProperty textProp,
                                  string sharedText, string label)
    {
        EditorGUILayout.PropertyField(sourceProp, new GUIContent(label + " Source"));

        // enumValueIndex maps 1:1 to PromptPartSource (SharedDefault=0, Custom=1, None=2).
        var source = (PromptPartSource)sourceProp.enumValueIndex;
        EditorGUI.indentLevel++;
        switch (source)
        {
            case PromptPartSource.Custom:
                if (textProp != null)
                    EditorGUILayout.PropertyField(textProp, new GUIContent(label + " Prompt"));
                break;

            case PromptPartSource.SharedDefault:
                s_sharedExpanded.TryGetValue(label, out bool expanded);
                expanded = EditorGUILayout.Foldout(expanded,
                    "Shared " + label.ToLower() + " text (from VHPrompts)", true);
                s_sharedExpanded[label] = expanded;
                if (expanded)
                {
                    float h = SharedTextStyle.CalcHeight(
                        new GUIContent(sharedText), EditorGUIUtility.currentViewWidth - 40f);
                    EditorGUILayout.SelectableLabel(sharedText, SharedTextStyle, GUILayout.Height(h));
                }
                break;

            default: // None
                EditorGUILayout.HelpBox("No " + label.ToLower() + " section will be included.",
                                        MessageType.Warning);
                break;
        }
        EditorGUI.indentLevel--;
    }

    void DrawComposedPreview()
    {
        EditorGUILayout.Space();
        s_showComposed = EditorGUILayout.Foldout(s_showComposed, "Composed system prompt (preview)", true);
        if (!s_showComposed) return;

        var profile = target as VHCharacterProfile;
        if (profile == null) return;

        string composed = profile.llmPrompt; // composes when Compose From Parts is ON, else legacy
        EditorGUILayout.HelpBox(
            string.IsNullOrEmpty(composed) ? "(empty)" : composed, MessageType.None);
        if (targets.Length > 1)
            EditorGUILayout.LabelField("(preview shown for the first selected character)",
                                       EditorStyles.miniLabel);
    }
}
