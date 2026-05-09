/* 
 * Copyright (C) 2021 Victor Soupday
 * This file is part of CC_Unity_Tools <https://github.com/soupday/CC_Unity_Tools>
 * 
 * CC_Unity_Tools is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * CC_Unity_Tools is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with CC_Unity_Tools.  If not, see <https://www.gnu.org/licenses/>.
 */

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System;

namespace Reallusion.Runtime
{
    [CustomEditor(typeof(BoneDriver))]
    public class BoneDriverEditor : Editor
    {
        private BoneDriver boneDriver;

        const float LABEL_WIDTH = 80f;
        const float SPACER = 20f;
        const float LINE_SPACER = 5f;
        const float BUTTON_WIDTH = 190f;

        private void OnEnable()
        {
            boneDriver = (BoneDriver)target;
        }

        public override void OnInspectorGUI()
        {
            OnBoneDriverGUI();
            base.OnInspectorGUI();
        }

        private Styles textStyle;

        public class Styles
        {
            public GUIStyle titleStyle;
            public GUIStyle toggleStyle;
            public GUIStyle toggleStyleActive;
            public GUIStyle toggleStyleInactive;
            public GUIStyle labelStyle;
            public GUIStyle helpStyle;

            public Color activeCol = Color.cyan * 0.85f;
            public Color inactiveCol = Color.cyan * 0.65f;

            public Styles()
            {
                titleStyle = new GUIStyle(GUI.skin.label);
                titleStyle.fontSize = 16;
                titleStyle.fontStyle = FontStyle.BoldAndItalic;

                toggleStyle = new GUIStyle(GUI.skin.toggle);
                toggleStyle.fontSize = 12;
                toggleStyle.fontStyle = FontStyle.BoldAndItalic;

                toggleStyleActive = new GUIStyle(GUI.skin.toggle);
                toggleStyleActive.fontSize = 12;
                toggleStyleActive.fontStyle = FontStyle.Italic;
                toggleStyleActive.onNormal.textColor = activeCol;
                toggleStyleActive.onHover.textColor = activeCol;
                toggleStyleActive.normal.textColor = activeCol;
                toggleStyleActive.hover.textColor = activeCol;

                toggleStyleInactive = new GUIStyle(GUI.skin.toggle);
                toggleStyleInactive.fontSize = 12;
                toggleStyleInactive.fontStyle = FontStyle.Italic;
                toggleStyleInactive.onNormal.textColor = inactiveCol;
                toggleStyleInactive.onHover.textColor = inactiveCol;
                toggleStyleInactive.normal.textColor = inactiveCol;
                toggleStyleInactive.hover.textColor = inactiveCol;

                labelStyle = new GUIStyle(GUI.skin.label);
                labelStyle.fontSize = 12;

                helpStyle = new GUIStyle(GUI.skin.GetStyle("HelpBox"));
                helpStyle.fontSize = 12;
            }
        }

        private void OnBoneDriverGUI()
        {
            if (textStyle == null) textStyle = new Styles();
            EditorGUI.BeginChangeCheck();
            GUILayout.BeginVertical();

            bool animMode = AnimationMode.InAnimationMode();
            bool canSave = HasOverrides();

            GUILayout.Label("Save Changes to Prefab", textStyle.titleStyle);
            GUILayout.Space(LINE_SPACER);

            string anim = "Animation mode is active. ALL changes will be saved to the prefab when clicking below. (TOGGLE the PREVIEW mode button in the TimeLine window to disable animation mode)";
            string save = "Save the changes made to the BoneDriver component.";
            string noChange = "The BoneDriver component has no unsaved changes.";

            string saveMsg = canSave ? (animMode ? anim : save) : noChange;
            bool saveAll = canSave & animMode;

            EditorGUILayout.HelpBox(saveMsg, saveAll ? MessageType.Warning : MessageType.Info, true);

            EditorGUI.BeginDisabledGroup(!canSave);

            string saveBoneDriver = "Save BoneDriver changes to Prefab";
            string saveALL = "Save ALL changes to Prefab";

            if (GUILayout.Button(saveAll ? saveALL : saveBoneDriver))
            {
                SaveToPrefab();
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(LINE_SPACER);

            // Rebuild
            GUILayout.Label("Rebuild & Reset the BoneDriver", textStyle.titleStyle);
            GUILayout.Space(LINE_SPACER);
            EditorGUILayout.HelpBox("Rebuild the data needed for the BoneDriver in case of any problems. This will reset the BoneDriver to its original settings.", MessageType.Info, true);
            if (GUILayout.Button("Rebuild Bone Driver"))
            {
                boneDriver.RebuildSetup();
                SaveToPrefab();
            }
            GUILayout.Space(LINE_SPACER);

            //Expression Driver
            GUILayout.Label("Expression Driver", textStyle.titleStyle);
            GUILayout.Space(LINE_SPACER);
            EditorGUILayout.HelpBox("The expression can be used to drive the face bones directly (instead of with an animation track)", MessageType.Info, true);
            boneDriver.bones = GUILayout.Toggle(boneDriver.bones, "Expressions Drive Face Bones", textStyle.toggleStyle);

            GUILayout.Space(LINE_SPACER);
            EditorGUI.BeginDisabledGroup(!boneDriver.bones);
            GUILayout.Label("Drive individual bones", textStyle.titleStyle);
            GUILayout.Space(LINE_SPACER);

            bool driveIndividual = boneDriver.glossary.ExpressionsByBone.FindAll(x => x.BoneMatch != BoneDriver.BoneMatch.None).Count == boneDriver.glossary.ExpressionsByBone.Count;
            if (!driveIndividual)
            {
                EditorGUILayout.HelpBox("In order to enable/disable the driving of individual bones, the BoneDriver must be rebuilt either by using the rebuild bonedirver button (above) or by rebuilding the character in the main CCiC tools window.", MessageType.Warning, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Enable or Disable the driving by expression of individual bones", MessageType.Info, true);
            }

            GUILayout.Space(LINE_SPACER);

            GUILayout.BeginHorizontal();
            GUILayout.Space(SPACER);

            GUILayout.BeginVertical();
            if (HasBoneMatch(BoneDriver.BoneMatch.Eye))
                boneDriver.driveEyes = GUILayout.Toggle(boneDriver.driveEyes, "Drive Eyes", boneDriver.driveEyes ? textStyle.toggleStyleActive : textStyle.toggleStyleInactive);
            if (HasBoneMatch(BoneDriver.BoneMatch.Jaw))
                boneDriver.driveJaw = GUILayout.Toggle(boneDriver.driveJaw, "Drive Jaw", boneDriver.driveJaw ? textStyle.toggleStyleActive : textStyle.toggleStyleInactive);
            if (HasBoneMatch(BoneDriver.BoneMatch.Neck))
                boneDriver.driveNeck = GUILayout.Toggle(boneDriver.driveNeck, "Drive Neck", boneDriver.driveNeck ? textStyle.toggleStyleActive : textStyle.toggleStyleInactive);
            if (HasBoneMatch(BoneDriver.BoneMatch.Teeth))
                boneDriver.driveTeeth = GUILayout.Toggle(boneDriver.driveTeeth, "Drive Teeth", boneDriver.driveTeeth ? textStyle.toggleStyleActive : textStyle.toggleStyleInactive);
            if (HasBoneMatch(BoneDriver.BoneMatch.Tongue))
                boneDriver.driveTongue = GUILayout.Toggle(boneDriver.driveTongue, "Drive Tongue", boneDriver.driveTongue ? textStyle.toggleStyleActive : textStyle.toggleStyleInactive);
            if (HasBoneMatch(BoneDriver.BoneMatch.Head))
                boneDriver.driveHead = GUILayout.Toggle(boneDriver.driveHead, "Drive Head", boneDriver.driveHead ? textStyle.toggleStyleActive : textStyle.toggleStyleInactive);

            EditorGUI.EndDisabledGroup();
            GUILayout.Space(LINE_SPACER);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.Space(LINE_SPACER);
            EditorGUILayout.HelpBox("The expression can be copied in real time to any objects with the same blendshapes instead of needing to have animation tracks for each blendshape on each object.", MessageType.Info, true);
            boneDriver.expressions = GUILayout.Toggle(boneDriver.expressions, "Expressions are copied to all face parts", textStyle.toggleStyle);
            GUILayout.Space(LINE_SPACER);
            EditorGUILayout.HelpBox("The expression can be used to drive constraint blend shapes (those beginning with 'C_' in CC5 characters).", MessageType.Info, true);
            boneDriver.constraint = GUILayout.Toggle(boneDriver.constraint, "Expressions control constraint blend shapes.", textStyle.toggleStyle);
            GUILayout.Space(LINE_SPACER);

            // Viseme Amplification
            GUILayout.Label("Viseme Amplification", textStyle.titleStyle);
            GUILayout.Space(LINE_SPACER);
            EditorGUILayout.HelpBox("Expression Visemes can be amplified in real time by adjusting the power slider (lower will have more effect) and scale slider (a direct multiply of the power adjusted viseme value)", MessageType.Info, true);
            string ampTip = "(Viseme Value ^ Power) * Scale";
            boneDriver.amplify = GUILayout.Toggle(boneDriver.amplify, new GUIContent("Amplify Vismes", ampTip), textStyle.toggleStyle);
            GUILayout.Space(LINE_SPACER);
            EditorGUI.BeginDisabledGroup(!boneDriver.amplify);
            GUILayout.BeginHorizontal();
            GUILayout.Space(SPACER);
            GUILayout.Label(new GUIContent("Power", ampTip), textStyle.labelStyle, GUILayout.Width(LABEL_WIDTH));
            boneDriver.visemePower = EditorGUILayout.Slider(boneDriver.visemePower, 0.1f, 2.0f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(SPACER);
            GUILayout.Label(new GUIContent("Scale", ampTip), textStyle.labelStyle, GUILayout.Width(LABEL_WIDTH));
            boneDriver.visemeScale = EditorGUILayout.Slider(boneDriver.visemeScale, 0f, 2f);
            GUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(SPACER);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(boneDriver);
            }
            GUILayout.EndVertical();
        }

        bool HasBoneMatch(BoneDriver.BoneMatch boneMatch)
        {
            return boneDriver.glossary.ExpressionsByBone.FindAll(x => x.BoneMatch == boneMatch).Count > 0;
        }

        public void SaveToPrefab()
        {
            try
            {
                GameObject prefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(boneDriver.gameObject);

                if (prefabRoot && PrefabUtility.GetPrefabInstanceStatus(prefabRoot) != PrefabInstanceStatus.NotAPrefab)
                {
                    if (AnimationMode.InAnimationMode())
                    {
                        // Apply ALL overrides to the prefab
                        PrefabUtility.ApplyPrefabInstance(prefabRoot, InteractionMode.UserAction);
                        Debug.Log("ALL changes have been saved to the character prefab.");
                    }
                    else
                    {
                        // Apply on BoneDriver overrides to the prefab
                        List<ObjectOverride> overrides = PrefabUtility.GetObjectOverrides(prefabRoot);
                        foreach (var ovr in overrides)
                        {
                            if (ovr.instanceObject.GetType() == typeof(BoneDriver))
                            {
                                ovr.Apply();
                                Debug.Log("BoneDriver changes have been saved to the character prefab.");
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Failed to save the BoneDriver changes to the prefab: {e.Message}");
            }
        }

        bool HasOverrides()
        {
            try
            {
                GameObject prefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(boneDriver.gameObject);

                if (prefabRoot != null && PrefabUtility.GetPrefabInstanceStatus(prefabRoot) != PrefabInstanceStatus.NotAPrefab)
                {
                    // report only BoneDriver overrides on the prefab
                    List<ObjectOverride> overrides = PrefabUtility.GetObjectOverrides(prefabRoot);
                    foreach (var ovr in overrides)
                    {
                        if (ovr.instanceObject.GetType() == typeof(BoneDriver))
                        {
                            return true;
                        }
                    }

                    // report ANY overrides on the prefab
                    //return PrefabUtility.HasPrefabInstanceAnyOverrides(prefabRoot, false);  
                }
            }
            catch
            {

            }
            return false;
        }
    }
}
