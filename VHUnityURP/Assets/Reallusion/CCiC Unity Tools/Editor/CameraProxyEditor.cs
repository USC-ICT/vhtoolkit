/*
 *  This file is distributed as part of CC_Unity_Tools <https://github.com/soupday/CC_Unity_Tools>
 * 
 *  MIT No Attribution (https://github.com/aws/mit-0)
 *
 *  Copyright 2025 Victor Soupday
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a copy of this
 *  software and associated documentation files (the "Software"), to deal in the Software
 *  without restriction, including without limitation the rights to use, copy, modify,
 *  merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
 *  permit persons to whom the Software is furnished to do so.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 *  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 *  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 *  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
 *  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 *  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Reallusion.Runtime
{
    [CustomEditor(typeof(CameraProxy))]
    public class CameraProxyEditor : Editor
    {
        public class Styles
        {
            public GUIStyle sectionLabel;

            public Styles()
            {                
                sectionLabel = new GUIStyle(GUI.skin.label);
                sectionLabel.fontSize = 14;
                sectionLabel.fontStyle= FontStyle.BoldAndItalic;
                sectionLabel.normal.textColor = Color.white;
            }
        }

        [SerializeField] private CameraProxy proxy;
        public Styles styles;
        private bool doneInit = false;

        void DoInit()
        {
            doneInit = true;
        }

        private void OnEnable()
        {
            proxy = target as CameraProxy;
        }

        public override void OnInspectorGUI()
        {
            if (proxy != null)
            {
                proxy = target as CameraProxy;
                return;
            }

            // undo system is losing data
            if (proxy.runtimePipeline == CameraProxy.RuntimePipeline.None)
            {
                proxy.CacheCameraComponents();
            }

            if (styles == null) styles = new Styles();

            if (!doneInit) DoInit();

            GUILayout.BeginVertical();

            PipelineGUI();

            InfoPaneGUI();

            if(proxy.dof_delta) 
                DephthOfFieldGUI();

            if(proxy.fov_delta)
                FieldOfViewGUI();

            GUILayout.EndVertical();
        }

        public void PipelineGUI()
        {
            string pipeline = string.Empty;
            switch (proxy.runtimePipeline)
            {
                case CameraProxy.RuntimePipeline.None:
                    {
                        pipeline = "Unknown Pipeline";
                        break;
                    }
                case CameraProxy.RuntimePipeline.Builtin:
                    {
                        pipeline = "Built-in Pipeline";
                        break;
                    }
                case CameraProxy.RuntimePipeline.Uninversal:
                    {
                        pipeline = "Uninversal Render Pipeline";
                        break;
                    }
                case CameraProxy.RuntimePipeline.HighDefinition:
                    {
                        pipeline = "High Definition Render Pipeline";
                        break;
                    }
            }

            GUILayout.Label(pipeline, styles.sectionLabel);

            GUILayout.Space(4f);
        }

        public void InfoPaneGUI()
        {
            EditorGUILayout.HelpBox("The controls here are made available when a camera property is animated, and allow you to 'fine tune' any values that look to be mismatched with the source iClone scene.\n\nThe value override is a straight multiplier applied to the animated value.", MessageType.Info);
        }

        public void DephthOfFieldGUI()
        {
            // Depth of Field settings
            proxy.animateDof = GUILayout.Toggle(proxy.animateDof, new GUIContent("Animate Depth of Field", "Animate the Depth of field settings in override on the global volume in the scene."));

            GUILayout.BeginHorizontal();

            GUILayout.Space(20f);

            GUILayout.BeginVertical();

            GUILayout.Space(4f);

            EditorGUI.BeginDisabledGroup(!proxy.animateDof);
            GUILayout.Label("Depth of field blur settings", styles.sectionLabel);

            GUILayout.Space(4f);

            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent("Sensor scale mult", "Multiplier to apply to the base sensor size imported from CC/iClone - A larger sensor increases the blur effect."), GUILayout.Width(120f));
            EditorGUI.BeginChangeCheck();
            proxy.MultiplySensorScale = GUILayout.HorizontalSlider(proxy.MultiplySensorScale, 0.1f, 4f);
            if (EditorGUI.EndChangeCheck())
            {
                proxy.UpdateSensorSize();
            }
            EditorGUI.BeginChangeCheck();
            string newMult = GUILayout.TextField(proxy.MultiplySensorScale.ToString("0.00"), GUILayout.Width(35f));
            if (EditorGUI.EndChangeCheck())
            {
                if (float.TryParse(newMult, out float result))
                {
                    proxy.MultiplySensorScale = result;
                    proxy.UpdateSensorSize();
                }
            }
            GUILayout.Space(3f);
            GUILayout.EndHorizontal();

            if (proxy.runtimePipeline == CameraProxy.RuntimePipeline.Uninversal)
            {
                GUILayout.Space(4f);

                GUILayout.BeginHorizontal();

                GUILayout.Label(new GUIContent("Focus scale mult", "(URP only) Multiplier to scale the Focal Length of the Depth of Field Override in the global volume."), GUILayout.Width(120f));
                proxy.FocusScale = GUILayout.HorizontalSlider(proxy.FocusScale, 0.1f, 100f);

                EditorGUI.BeginChangeCheck();
                string newFscl = GUILayout.TextField(proxy.FocusScale.ToString("0.00"), GUILayout.Width(35f));
                if (EditorGUI.EndChangeCheck())
                {
                    if (float.TryParse(newFscl, out float result))
                    {
                        proxy.FocusScale = result;
                    }
                }

                GUILayout.Space(3f);
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(4f);

            GUILayout.BeginHorizontal();
            proxy.overrideAperture = GUILayout.Toggle(proxy.overrideAperture, new GUIContent("Override Aperture", ""));
            GUILayout.EndHorizontal();

            GUILayout.Space(4f);

            GUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(!proxy.overrideAperture);
            GUILayout.Label(new GUIContent("Aperture (F-Stop)", "Aperture of the camera, low vaules give a shallower depth of field."), GUILayout.Width(120f));
            EditorGUI.BeginChangeCheck();
            proxy.Aperture = GUILayout.HorizontalSlider(proxy.Aperture, 0.1f, 32f);
            if (EditorGUI.EndChangeCheck())
            {
                //proxy.UpdateAperture();
            }

            if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("TreeEditor.Refresh").image, "Reset aperture to initial value"), GUILayout.Width(EditorGUIUtility.singleLineHeight), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                proxy.Aperture = proxy.initialAperture;
            }

            EditorGUI.BeginChangeCheck();
            string newAp = GUILayout.TextField(proxy.Aperture.ToString("0.00"), GUILayout.Width(35f));
            if (EditorGUI.EndChangeCheck())
            {
                if (float.TryParse(newAp, out float result))
                {
                    proxy.Aperture = result;
                    //proxy.UpdateAperture();
                }
            }

            GUILayout.Space(3f);
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            // End of Depth of Field settings
        }

        public void FieldOfViewGUI()
        {
            // Field of View settings
            GUILayout.Space(4f);

            proxy.animateFov = GUILayout.Toggle(proxy.animateFov, new GUIContent("Animate Field of View", "Animate the field of view setting of the camera."));

            GUILayout.BeginHorizontal();

            GUILayout.Space(20f);

            GUILayout.BeginVertical();

            GUILayout.Space(4f);

            EditorGUI.BeginDisabledGroup(!proxy.animateFov);
            GUILayout.Label("Field of view settings", styles.sectionLabel);

            GUILayout.Space(4f);

            GUILayout.BeginHorizontal();
            proxy.overrideFieldOfView = GUILayout.Toggle(proxy.overrideFieldOfView, new GUIContent("Override field of view", ""));
            GUILayout.EndHorizontal();

            GUILayout.Space(4f);

            GUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(!proxy.overrideFieldOfView);
            GUILayout.Label(new GUIContent("Field of view", "Value to force the camera field of view to - this will override any animated fov values."), GUILayout.Width(120f));
            EditorGUI.BeginChangeCheck();
            proxy.fovOverrideValue = GUILayout.HorizontalSlider(proxy.fovOverrideValue, 0.1f, 179f);
            if (EditorGUI.EndChangeCheck())
            {
                proxy.UpdateFov();
            }

            if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("TreeEditor.Refresh").image, "Reset aperture to initial value"), GUILayout.Width(EditorGUIUtility.singleLineHeight), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                proxy.fovOverrideValue = proxy.initialFieldOfView;
                proxy.UpdateFov();
            }

            EditorGUI.BeginChangeCheck();
            string newFovMult = GUILayout.TextField(proxy.fovOverrideValue.ToString("0.00"), GUILayout.Width(35f));
            if (EditorGUI.EndChangeCheck())
            {
                if (float.TryParse(newFovMult, out float result))
                {
                    proxy.fovOverrideValue = Mathf.Clamp(result, 0.1f, 179f);
                    proxy.UpdateFov();
                }
            }
            GUILayout.Space(3f);
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup();
        }

#if HDRP_RUNTIME
        public void HDRPSpecificGUI()
        {

        }
#endif
    }
}
