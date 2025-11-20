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
using UnityEngine;
using System.Reflection;
using System;

#if UNITY_EDITOR
using UnityEditor;
#if PLASTIC_NEWTONSOFT_AVAILABLE
using Unity.Plastic.Newtonsoft.Json;
#else
using Newtonsoft.Json;  // com.unity.collab-proxy (plastic scm) versions prior to 1.14.12
#endif
#endif
using UnityEngine.Rendering;
#if HDRP_RUNTIME
using UnityEngine.Rendering.HighDefinition;
#elif URP_RUNTIME
using UnityEngine.Rendering.Universal;
#elif POST_PROCESSING
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;
#endif

namespace Reallusion.Runtime
{
    [ExecuteInEditMode]
    public class LightProxy : MonoBehaviour
    {
        #region Bindable properties to be animated
        // Bindable animatable proxy properties (Transforms will always be animated by the clip directly)
        public float ProxyActive;
        public float ProxyColor_r;
        public float ProxyColor_g;
        public float ProxyColor_b;
        public float ProxyMultiplier;
        public float ProxyRange;
        public float ProxyAngle;
        public float ProxyFalloff;
        public float ProxyAttenuation;
        public float ProxyDarkness;
        #endregion Bindable properties to be animated

        #region Variables
        // undo system is losing this data -- force serialization
        [SerializeField] public RuntimePipeline runtimePipeline = RuntimePipeline.None;  // allows CustomEditor to detect pipeline
        [SerializeField] public Light LightComponent;

        // initial values set by the importer
        // reveal these in the custom inspector along with an apply initial settings function
        public string initialType = string.Empty;
        public Vector3 initialPosition = Vector3.zero;
        public Quaternion initialRotation = Quaternion.identity;
        public Vector3 initialScale = Vector3.one;
        public bool initialActive = false;
        public float initialColorR = 0f;
        public float initialColorG = 0f;
        public float initialColorB = 0f;
        public float initialMultiplier = 0f;
        public float initialRange = 0f;
        public float initialAngle = 0f;
        public float initialFalloff = 0f;
        public float initialAttenuation = 0f;

        // which properties actually change and require update from proxy values     
        public bool pos_delta = false, rot_delta = false, scale_delta = false, active_delta = false, color_delta = false, mult_delta = false, range_delta = false, angle_delta = false, fall_delta = false, att_delta = false, dark_delta = false;

        // InspectorGUI Control 
        public bool UpdateColors = false;
        public bool MultiplyColor = false;
        public Color ColorMultiplier = Color.white;

        public bool UpdateIntensity = false;
        public bool MultiplyIntensity = false;
        public float IntensityMultiplier = 1.0f;        

        public bool UpdateRange = false;
        public bool MultiplyRange = false;
        public float RangeMultiplier = 1.0f;

        public bool UpdateAngles = false;
        public bool MultiplyAngles = false;
        public float OuterAngleMultiplier = 1.0f;
        public float InnerAngleMultiplier = 1.0f;

        #endregion Variables

        #region Scaling and conversion factors
#if HDRP_17_RUNTIME
        public const float HDRP_INTENSITY_SCALE = 3000f;
#else
        public const float HDRP_INTENSITY_SCALE = 25000f;
#endif
        public const float URP_INTENSITY_SCALE = 1f;
        public const float PP_INTENSITY_SCALE = 0.12f;
        public const float BASE_INTENSITY_SCALE = 0.12f;
        public const float RANGE_SCALE = 0.01f;

        #endregion Scaling and conversion factors

        #region Pipeline specific functions   
#if HDRP_RUNTIME
        HDAdditionalLightData HDLightData = null;
#elif URP_RUNTIME
        // URP Specific
#elif POST_PROCESSING
        // Built-in (Post Processing) settings
#endif

        bool cachedComponents = false;
        public bool CacheLightComponents()
        {
            cachedComponents = false;
            LightComponent = GetComponent<Light>();
#if HDRP_RUNTIME
            // HDRP Specific
            runtimePipeline = RuntimePipeline.HighDefinition;
            HDLightData = GetComponent<HDAdditionalLightData>();
            if (HDLightData == null)
            {
                //Debug.LogWarning("Cannot find HDAdditionalLightData component");
                cachedComponents = false;
            }

            if (HDLightData != null && LightComponent != null)
            {
                //Debug.LogWarning("FOUND HDAdditionalLightData component");
                cachedComponents = true;
            }
#elif URP_RUNTIME
            // URP Specific
            runtimePipeline = RuntimePipeline.Uninversal;  
            if (LightComponent != null)
            {
                //Debug.LogWarning("FOUND Light component");
                cachedComponents = true;
            }
#elif POST_PROCESSING
            // Built-in (Post Processing) settings
            runtimePipeline = RuntimePipeline.Builtin;
            if ( LightComponent != null )
            {
                cachedComponents = true;
            }
#else 
            runtimePipeline = RuntimePipeline.Builtin;
            if ( LightComponent != null )
            {
                cachedComponents = true;
            }
#endif
            return cachedComponents;
        }

        // Color Settings
        public void SetLightColor()
        {
            Color setScolor = new Color(ProxyColor_r, ProxyColor_g, ProxyColor_b);
            LightComponent.color = MultiplyColor ? setScolor * ColorMultiplier : setScolor;
        }

        public void SetInitialLightColor()
        {
            LightComponent.color = new Color(initialColorR, initialColorG, initialColorB);
        }
        // Color Settings - End

        // Intensity Settings
        public void SetLightIntensity()
        {

#if HDRP_17_RUNTIME
            float value = MultiplyIntensity ? ProxyMultiplier * HDRP_INTENSITY_SCALE * IntensityMultiplier : ProxyMultiplier * HDRP_INTENSITY_SCALE;
            LightComponent.intensity = value;
#elif HDRP_RUNTIME
            // HDRP Specific
            if (HDLightData == null) HDLightData = GetComponent<HDAdditionalLightData>();

            if (HDLightData == null) { Debug.LogWarning("InitialIntensity - Cannot find HDAdditionalLightData component"); return; }

            float value = MultiplyIntensity ? ProxyMultiplier * HDRP_INTENSITY_SCALE * IntensityMultiplier : ProxyMultiplier * HDRP_INTENSITY_SCALE;
            HDLightData.intensity = value;
#elif URP_RUNTIME
            // URP Specific
            float value = MultiplyIntensity ? ProxyMultiplier * URP_INTENSITY_SCALE * IntensityMultiplier : ProxyMultiplier * URP_INTENSITY_SCALE;
            LightComponent.intensity = value;
#elif POST_PROCESSING
            // Built-in (Post Processing) settings
            float value = MultiplyIntensity ? ProxyMultiplier * PP_INTENSITY_SCALE * IntensityMultiplier : ProxyMultiplier * PP_INTENSITY_SCALE;
            LightComponent.intensity = value;
#else
            float value = MultiplyIntensity ? ProxyMultiplier * BASE_INTENSITY_SCALE * IntensityMultiplier : ProxyMultiplier * BASE_INTENSITY_SCALE;
            LightComponent.intensity = value;
#endif
        }

        public void SetInitialLightIntensity()
        {
#if HDRP_17_RUNTIME
            LightComponent.intensity = initialMultiplier * HDRP_INTENSITY_SCALE;
#elif HDRP_RUNTIME
            // HDRP Specific
            if (HDLightData == null) HDLightData = GetComponent<HDAdditionalLightData>();

            if (HDLightData == null) { Debug.LogWarning("InitialIntensity - Cannot find HDAdditionalLightData component"); return; }
            HDLightData.intensity = initialMultiplier * HDRP_INTENSITY_SCALE;
#elif URP_RUNTIME
            // URP Specific
            LightComponent.intensity = initialMultiplier * URP_INTENSITY_SCALE;
#elif POST_PROCESSING
            // Built-in (Post Processing) settings            
            LightComponent.intensity = initialMultiplier * PP_INTENSITY_SCALE;
#else
            LightComponent.intensity = initialMultiplier * BASE_INTENSITY_SCALE;
#endif
        }

        public void GUIAdjustIntensity()
        {
            if (mult_delta)
            {
                SetLightIntensity();
            }
            else
            {
#if HDRP_17_RUNTIME
                LightComponent.intensity = initialMultiplier * IntensityMultiplier * HDRP_INTENSITY_SCALE;
#elif HDRP_RUNTIME
                // HDRP Specific
                if (HDLightData == null) HDLightData = GetComponent<HDAdditionalLightData>();

                if (HDLightData == null) { Debug.LogWarning("InitialIntensity - Cannot find HDAdditionalLightData component"); return; }

                HDLightData.intensity = initialMultiplier * IntensityMultiplier * HDRP_INTENSITY_SCALE;
#elif URP_RUNTIME
                // URP Specific
                LightComponent.intensity = initialMultiplier * IntensityMultiplier * URP_INTENSITY_SCALE;
#elif POST_PROCESSING
                // Built-in (Post Processing) settings
                LightComponent.intensity = initialMultiplier * IntensityMultiplier * PP_INTENSITY_SCALE;
#else
                LightComponent.intensity = initialMultiplier * IntensityMultiplier * BASE_INTENSITY_SCALE;
#endif
            }
        }
        // Intensity Settings - End

        // Range Settings
        public void SetLightRange()
        {
            LightComponent.range = MultiplyRange ? ProxyRange * RANGE_SCALE * RangeMultiplier : ProxyRange * RANGE_SCALE;
        }

        public void SetInitialLightRange()
        {
            LightComponent.range = initialRange * RANGE_SCALE;
        }

        public void GUIAdjustRange()
        {
            if (range_delta)
            {
                SetLightRange();
            }
            else
            {
                LightComponent.range = initialRange * RANGE_SCALE * RangeMultiplier;
            }
        }
        // Range Settings - End

        // Spotlight Settings
        public void SetSpotlightAngles()
        {
            LightComponent.spotAngle = MultiplyAngles ? ProxyAngle * OuterAngleMultiplier : ProxyAngle;
            LightComponent.innerSpotAngle = MultiplyAngles ? GetInnerAngle(ProxyFalloff, ProxyAttenuation) * InnerAngleMultiplier : GetInnerAngle(ProxyFalloff, ProxyAttenuation);
        }

        public void SetInitialSpotlightAngles()
        {
            LightComponent.spotAngle = initialAngle;
            LightComponent.innerSpotAngle = GetInnerAngle(initialFalloff, initialAttenuation);
        }

        public void GUIAdjustSpotlightAngles()
        {
            if (angle_delta)
            {
                SetSpotlightAngles();
            }
            else
            {
                LightComponent.spotAngle = initialAngle * OuterAngleMultiplier;
                LightComponent.innerSpotAngle = GetInnerAngle(initialFalloff, initialAttenuation) * InnerAngleMultiplier;
            }
        }

        public float GetInnerAngle(float fall, float att)
        {
            return (fall + att) / 2;
        }
        // Spotlight Settings - End

        #endregion Pipeline specific functions

        #region Initial Setup by Importer
        // call SetupLight from  importer by reflection to set initial values with a frame delay to allow any init to complete
        // most relevant to HDRP which adds a HDAdditionalLightData component to the Light GameObject
        // this requires a delayed application of settings.
#if UNITY_EDITOR
        public void SetupLight(string jsonDataString)
        {
            JsonLightData settingsObject = null;
            try
            {
                settingsObject = JsonConvert.DeserializeObject<JsonLightData>(jsonDataString);
            }
            catch (Exception ex)
            {
                Debug.Log("Failed to deserialize the setup object: " + ex.Message);
                return;
            }

            if (settingsObject == null) { Debug.LogWarning("Settings Object is null!"); return; }

            initialType = settingsObject.Type;
            initialPosition = settingsObject.GetPosition();
            initialRotation = settingsObject.GetRotation();
            initialScale = settingsObject.GetScale();
            initialActive = settingsObject.Active;
            initialColorR = settingsObject.GetColor().r;
            initialColorG = settingsObject.GetColor().g;
            initialColorB = settingsObject.GetColor().b;
            initialMultiplier = settingsObject.Multiplier;
            initialRange = settingsObject.Range;
            initialAngle = settingsObject.Angle;
            initialFalloff = settingsObject.Falloff;
            initialAttenuation = settingsObject.Attenuation;

            // which properties actually change and require update from proxy values     
            pos_delta = settingsObject.pos_delta;
            rot_delta = settingsObject.rot_delta;
            scale_delta = settingsObject.scale_delta;
            active_delta = settingsObject.active_delta;
            color_delta = settingsObject.color_delta;
            mult_delta = settingsObject.mult_delta;
            range_delta = settingsObject.range_delta;
            angle_delta = settingsObject.angle_delta;
            fall_delta = settingsObject.fall_delta;
            att_delta = settingsObject.att_delta;
            dark_delta = settingsObject.dark_delta;

            UpdateColors = color_delta;
            UpdateIntensity = mult_delta;
            UpdateRange = range_delta;
            UpdateAngles = angle_delta || fall_delta || att_delta;
        }
#endif
#endregion Initial Setup by Importer

        #region Start and Update
        void Start()
        {
            CacheLightComponents();
        }

        void Update()
        {
            if (!cachedComponents)
                CacheLightComponents();

            if (cachedComponents)
                UpdateLight();
        }

        void UpdateLight()
        {
            if (LightComponent == null) return;

            if (UpdateColors)
            {
                Color setScolor = new Color(ProxyColor_r, ProxyColor_g, ProxyColor_b);
                LightComponent.color = MultiplyColor ? setScolor * ColorMultiplier : setScolor;
            }

            if (UpdateIntensity)
            {
                SetLightIntensity();
            }

            if (UpdateRange)
            {
                LightComponent.range = MultiplyRange ? ProxyRange * RANGE_SCALE * RangeMultiplier : ProxyRange * RANGE_SCALE;
            }

            if (UpdateAngles)
            {
                LightComponent.spotAngle = ProxyAngle;
                LightComponent.innerSpotAngle = GetInnerAngle(ProxyFalloff, ProxyAttenuation);
            }            
        }
        #endregion Start and Update

        #region Class data
#if UNITY_EDITOR
        public class JsonLightData
        {
            // partial extract of on disk json corresponding to an individual light
            public const string linkIdStr = "link_id";
            public const string nameStr = "name";
            public const string locStr = "loc";
            public const string rotStr = "rot";
            public const string scaStr = "sca";
            public const string activestr = "active";
            public const string colorStr = "color";
            public const string multStr = "multiplier";
            public const string typeStr = "type";
            public const string rangeStr = "range";
            public const string angleStr = "angle";
            public const string falloffStr = "falloff";
            public const string attStr = "attenuation";
            public const string inSqStr = "inverse_square";
            public const string traStr = "transmission";
            public const string istubeStr = "is_tube";
            public const string tubeLenStr = "tube_length";
            public const string tubeRadStr = "tube_radius";
            public const string tubeRadSoftStr = "tube_soft_radius";
            public const string isRectStr = "is_rectangle";
            public const string rectStr = "rect";
            public const string shadowStr = "cast_shadow";
            public const string darkStr = "darkness";
            public const string framesStr = "frame_count";

            [JsonProperty(linkIdStr)]
            public string LinkId { get; set; }
            [JsonProperty(nameStr)]
            public string Name { get; set; }
            [JsonProperty(locStr)]
            public float[] loc { get; set; }
            public Vector3 Pos { get { return this.GetPosition(); } }
            [JsonProperty(rotStr)]
            public float[] rot { get; set; }
            public Quaternion Rot { get { return this.GetRotation(); } }
            [JsonProperty(scaStr)]
            public float[] sca { get; set; }
            public Vector3 Scale { get { return this.GetScale(); } }
            [JsonProperty(activestr)]
            public bool Active { get; set; }
            [JsonProperty(colorStr)]
            public float[] color { get; set; }
            public Color Color { get { return this.GetColor(); } }
            [JsonProperty(multStr)]
            public float Multiplier { get; set; }
            [JsonProperty(typeStr)]
            public string Type { get; set; }
            [JsonProperty(rangeStr)]
            public float Range { get; set; }
            [JsonProperty(angleStr)]
            public float Angle { get; set; }
            [JsonProperty(falloffStr)]
            public float Falloff { get; set; }
            [JsonProperty(attStr)]
            public float Attenuation { get; set; }
            [JsonProperty(inSqStr)]
            public bool InverseSquare { get; set; }
            [JsonProperty(traStr)]
            public bool Transmission { get; set; }
            [JsonProperty(istubeStr)]
            public bool IsTube { get; set; }
            [JsonProperty(tubeLenStr)]
            public float TubeLength { get; set; }
            [JsonProperty(tubeRadStr)]
            public float TubeRadius { get; set; }
            [JsonProperty(tubeRadSoftStr)]
            public float TubeSoftRadius { get; set; }
            [JsonProperty(isRectStr)]
            public bool IsRect { get; set; }
            [JsonProperty(rectStr)]
            public float[] rect { get; set; }
            [JsonProperty(shadowStr)]
            public bool CastShadow { get; set; }
            [JsonProperty(darkStr)]
            public float Darkness { get; set; }
            [JsonProperty(framesStr)]
            public int FrameCount { get; set; }


            // Animated properties (determined by the importer - repackaged here to ease use of <LightProxy> setup
            public const string posDltStr = "pos_delta";
            public const string rotDltStr = "rot_delta";
            public const string scaDltStr = "sca_delta";
            public const string actDltStr = "act_delta";
            public const string colDltStr = "col_delta";
            public const string mulDltStr = "mul_delta";
            public const string ranDltStr = "ran_delta";
            public const string angDltStr = "ang_delta";
            public const string falDltStr = "fal_delta";
            public const string attDltStr = "att_delta";
            public const string darDltStr = "dar_delta";

            [JsonProperty(posDltStr)]
            public bool pos_delta { get; set; }
            [JsonProperty(rotDltStr)]
            public bool rot_delta { get; set; }
            [JsonProperty(scaDltStr)]
            public bool scale_delta { get; set; }
            [JsonProperty(actDltStr)]
            public bool active_delta { get; set; }
            [JsonProperty(colDltStr)]
            public bool color_delta { get; set; }
            [JsonProperty(mulDltStr)]
            public bool mult_delta { get; set; }
            [JsonProperty(ranDltStr)]
            public bool range_delta { get; set; }
            [JsonProperty(angDltStr)]
            public bool angle_delta { get; set; }
            [JsonProperty(falDltStr)]
            public bool fall_delta { get; set; }
            [JsonProperty(attDltStr)]
            public bool att_delta { get; set; }
            [JsonProperty(darDltStr)]
            public bool dark_delta { get; set; }

            public JsonLightData()
            {
                this.LinkId = string.Empty;
                this.Name = string.Empty;
                this.loc = new float[0];
                this.rot = new float[0];
                this.sca = new float[0];
                this.Active = false;
                this.color = new float[0];
                this.Multiplier = 0f;
                this.Type = string.Empty;
                this.Range = 0f;
                this.Angle = 0f;
                this.Falloff = 0f;
                this.Attenuation = 0f;
                this.InverseSquare = false;
                this.Transmission = false;
                this.IsTube = false;
                this.TubeLength = 0f;
                this.TubeRadius = 0f;
                this.TubeSoftRadius = 0f;
                this.IsRect = false;
                this.rect = new float[0];
                this.CastShadow = false;
                this.Darkness = 0f;
                this.FrameCount = 0;

                this.pos_delta = false;
                this.rot_delta = false;
                this.scale_delta = false;
                this.active_delta = false;
                this.color_delta = false;
                this.mult_delta = false;
                this.range_delta = false;
                this.angle_delta = false;
                this.fall_delta = false;
                this.att_delta = false;
                this.dark_delta = false;
            }

            public Vector3 GetPosition()
            {
                return new Vector3(-loc[0], loc[2], -loc[1]) * 0.01f;
            }

            public Quaternion GetRotation()
            {
                Quaternion unCorrected = new Quaternion(rot[0], -rot[2], rot[1], rot[3]);
                Quaternion cameraCorrection = Quaternion.Euler(90f, -180f, 0f);
                Quaternion corrected = unCorrected * cameraCorrection;
                return corrected;
            }

            public Vector3 GetScale()
            {
                return new Vector3(sca[0], sca[1], sca[2]);
            }

            public Color GetColor()
            {
                return new Color(color[0], color[1], color[2]);
            }
        }
#endif
        [Serializable]
        public enum RuntimePipeline
        {
            None = 0,
            Builtin = 1,
            Uninversal = 2,
            HighDefinition = 3
        }
        #endregion Class data
    }
}
