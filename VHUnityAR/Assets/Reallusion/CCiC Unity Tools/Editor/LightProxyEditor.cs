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
    [CustomEditor(typeof(LightProxy))]
    public class LightProxyEditor : Editor
    {
        public class Styles
        {
            public GUIStyle sectionLabel;

            public Styles()
            {
                sectionLabel = new GUIStyle(GUI.skin.label);
                sectionLabel.fontSize = 14;
                sectionLabel.fontStyle = FontStyle.BoldAndItalic;
                sectionLabel.normal.textColor = Color.white;
            }
        }

        [SerializeField] private LightProxy proxy;
        public Styles styles;
        private bool doneInit = false;

        void DoInit()
        {
            doneInit = true;
        }

        private void OnEnable()
        {
            proxy = target as LightProxy;
        }

        public override void OnInspectorGUI()
        {
            if (proxy == null)
            {
                proxy = target as LightProxy;
                return;
            }

            // undo system is losing data
            if (proxy.runtimePipeline == LightProxy.RuntimePipeline.None)
            {
                proxy.CacheLightComponents();
            }

            if (styles == null) styles = new Styles();

            if (!doneInit) DoInit();
            
            GUILayout.BeginVertical();
            
            PipelineGUI();

            InfoPaneGUI();

            if (proxy.color_delta)            
                ColorControlsGUI();

            if (proxy.mult_delta)
                IntensityControlsGUI();

            if (proxy.range_delta)
                RangeControlsGUI();

            if (proxy.LightComponent != null && proxy.LightComponent.type == LightType.Spot)
            {
                if (proxy.angle_delta)
                    SpotlightControlsGUI();
            }

            GUILayout.EndVertical();
        }

        public void InfoPaneGUI()
        {
            EditorGUILayout.HelpBox("The controls here are made available when a light property is animated, and allow you to 'fine tune' any values that look to be mismatched with the source iClone scene.\n\nThe value override is a straight multiplier applied to the animated value.", MessageType.Info);
        }

        public void PipelineGUI()
        {
            string pipeline = string.Empty;
            switch (proxy.runtimePipeline)
            {
                case LightProxy.RuntimePipeline.None:
                    {
                        pipeline = "Unknown Pipeline";
                        break;
                    }
                case LightProxy.RuntimePipeline.Builtin:
                    {
                        pipeline = "Built-in Pipeline";
                        break;
                    }
                case LightProxy.RuntimePipeline.Uninversal:
                    {
                        pipeline = "Uninversal Render Pipeline";
                        break;
                    }
                case LightProxy.RuntimePipeline.HighDefinition:
                    {
                        pipeline = "High Definition Render Pipeline";
                        break;
                    }
            }
            GUILayout.Label(pipeline, styles.sectionLabel);

            GUILayout.Space(4f);
        }

        private void ColorControlsGUI()
        {
            // COLOR CONTROL
            GUILayout.Label("Color Control", styles.sectionLabel);
            GUILayout.Space(4f);

            EditorGUI.BeginDisabledGroup(!proxy.color_delta);

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            proxy.UpdateColors = GUILayout.Toggle(proxy.UpdateColors, "Animate Light Color");
            GUILayout.EndHorizontal();

            GUILayout.Space(2f);

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            proxy.MultiplyColor = GUILayout.Toggle(proxy.MultiplyColor, "Multiply by color (optional)");
            GUILayout.Space(4f);
            proxy.ColorMultiplier = EditorGUILayout.ColorField(proxy.ColorMultiplier, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup();

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("TreeEditor.Refresh").image, "Reset light color to initial value"), GUILayout.Width(EditorGUIUtility.singleLineHeight), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                proxy.MultiplyColor = false;
                proxy.SetInitialLightColor();
            }
            GUILayout.Space(2f);
            GUILayout.Label("Reset light color");
            GUILayout.EndHorizontal();
            // END OF COLOR CONTROL
        }

        private void IntensityControlsGUI()
        {
            // INTENSITY CONTROL
            GUILayout.Space(4f);
            GUILayout.Label("Intensity Control", styles.sectionLabel);
            GUILayout.Space(4f);

            EditorGUI.BeginDisabledGroup(!proxy.mult_delta);

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            proxy.UpdateIntensity = GUILayout.Toggle(proxy.UpdateIntensity, "Animate Intensity", GUI.skin.toggle);
            GUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup();

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            proxy.MultiplyIntensity = GUILayout.Toggle(proxy.MultiplyIntensity, "");
            GUILayout.Space(2f);
            GUILayout.Label("Adjust Intensity (mult.)");
            EditorGUI.BeginDisabledGroup(!proxy.MultiplyIntensity);
            EditorGUI.BeginChangeCheck();
            proxy.IntensityMultiplier = EditorGUILayout.Slider(proxy.IntensityMultiplier, 0f, 20f);
            if (EditorGUI.EndChangeCheck())
            {
                proxy.GUIAdjustIntensity();
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("TreeEditor.Refresh").image, "Reset light intensity to initial value"), GUILayout.Width(EditorGUIUtility.singleLineHeight), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                proxy.MultiplyIntensity = false;
                proxy.IntensityMultiplier = 1f;
                proxy.SetInitialLightIntensity();
            }
            GUILayout.Space(2f);
            GUILayout.Label("Reset light intensity");
            GUILayout.EndHorizontal();
            // END OF INTENSITY CONTROL
        }

        private void RangeControlsGUI()
        { 
            // RANGE CONTROL
            GUILayout.Space(4f);
            GUILayout.Label("Range Control", styles.sectionLabel);
            GUILayout.Space(4f);

            EditorGUI.BeginDisabledGroup(!proxy.range_delta); // !range_delta - start

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            proxy.UpdateRange = GUILayout.Toggle(proxy.UpdateRange, "Animate Range", GUI.skin.toggle);
            GUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup(); // !range_delta - end

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            proxy.MultiplyRange = GUILayout.Toggle(proxy.MultiplyRange, "");
            GUILayout.Space(4f);
            GUILayout.Label("Adjust Range (mult.)");
            EditorGUI.BeginDisabledGroup(!proxy.MultiplyRange); // !MultiplyRange - start
            EditorGUI.BeginChangeCheck();
            proxy.RangeMultiplier = EditorGUILayout.Slider(proxy.RangeMultiplier, 0f, 20f);
            if (EditorGUI.EndChangeCheck())
            {
                proxy.GUIAdjustRange();
            }
            EditorGUI.EndDisabledGroup(); // !MultiplyRange - end
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("TreeEditor.Refresh").image, "Reset light range to initial value"), GUILayout.Width(EditorGUIUtility.singleLineHeight), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                proxy.MultiplyRange = false;
                proxy.RangeMultiplier = 1f;
                proxy.SetInitialLightRange();
            }
            GUILayout.Space(2f);
            GUILayout.Label("Reset light range");
            GUILayout.EndHorizontal();
            // END OF RANGE CONTROL
        }

        private void SpotlightControlsGUI()
        {
            // SPOTLIGHT CONTROL
            GUILayout.Space(4f);
            GUILayout.Label("Spotlight Angle Control", styles.sectionLabel);
            GUILayout.Space(4f);

            EditorGUI.BeginDisabledGroup(proxy.LightComponent.type != LightType.Spot); // != LightType.Spot - start

            EditorGUI.BeginDisabledGroup(!proxy.angle_delta); // !angle_delta - start

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            proxy.UpdateAngles = GUILayout.Toggle(proxy.UpdateAngles, "Animate Spotlight Angles", GUI.skin.toggle);
            GUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup();  // !angle_delta - end

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);

            GUILayout.BeginVertical();

            GUILayout.FlexibleSpace();
            proxy.MultiplyAngles = GUILayout.Toggle(proxy.MultiplyAngles, "");
            GUILayout.FlexibleSpace();

            GUILayout.EndVertical();

            GUILayout.Space(4f);

            GUILayout.BeginVertical();
            EditorGUI.BeginDisabledGroup(!proxy.MultiplyAngles); // !MultiplyAngles - start
            GUILayout.BeginHorizontal();
            GUILayout.Label("Outer Angle (mult.)");
            EditorGUI.BeginChangeCheck();
            proxy.OuterAngleMultiplier = EditorGUILayout.Slider(proxy.OuterAngleMultiplier, 0f, 2f);
            if (EditorGUI.EndChangeCheck())
            {
                proxy.GUIAdjustSpotlightAngles();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Inner Angle (mult.)");
            EditorGUI.BeginChangeCheck();
            proxy.InnerAngleMultiplier = EditorGUILayout.Slider(proxy.InnerAngleMultiplier, 0f, 2f);
            if (EditorGUI.EndChangeCheck())
            {
                proxy.GUIAdjustSpotlightAngles();
            }
            GUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();  // !MultiplyAngles
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup(); // != LightType.Spot

            GUILayout.Space(4f);

            GUILayout.BeginHorizontal();
            GUILayout.Space(12f);
            if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("TreeEditor.Refresh").image, "Reset spotlight angles to initial values"), GUILayout.Width(EditorGUIUtility.singleLineHeight), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                proxy.MultiplyAngles = false;
                proxy.LightComponent.spotAngle = 1;
                proxy.LightComponent.innerSpotAngle = 1;
                proxy.OuterAngleMultiplier = 1;
                proxy.InnerAngleMultiplier = 1;
                proxy.SetInitialSpotlightAngles();
            }
            GUILayout.Space(2f);
            GUILayout.Label("Reset spotlight angles");
            GUILayout.EndHorizontal();
            // END OF SPOTLIGHT CONTROL
        }
    }
}
