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
using System;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Rendering;
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
#endif

namespace Reallusion.Runtime
{
    [ExecuteInEditMode]
    public class CameraProxy : MonoBehaviour
    {
        #region Bindable properties to be animated
        // Bindable animatable proxy properties (Transforms will always be animated by the clip directly)
        public float ProxyFocalLength;
        public float ProxyDofEnable;
        public float ProxyDofFocus;
        public float ProxyDofRange;
        public float ProxyDofFarBlur;
        public float ProxyDofNearBlur;
        public float ProxyDofFarTransition;
        public float ProxyDofNearTransition;
        public float ProxyDofMinBlendDist;
        public float ProxyFieldOfView;
        public float ProxyActive;
        #endregion Bindable properties to be animated

        #region Variables
        // undo system is losing this data -- force serialization
        [SerializeField] public RuntimePipeline runtimePipeline = RuntimePipeline.None;  // allows CustomEditor to detect pipeline
        [SerializeField] public Camera CameraComponent;

        public AnimationClip CachedAnimation;
        private bool initialSetupComplete = false;
        public bool InitialSetupComplete { get { return initialSetupComplete; } }

        // initial values set by the importer
        // reveal these in the custom inspector along with an apply initial settings function        
        public Vector3 initialPosition = Vector3.zero;
        public Quaternion initialRotation = Quaternion.identity;
        public Vector3 initialScale = Vector3.one;

        //public bool initialActive = false;
        public bool initialAnimateFov = false;
        public float initialFocalLength = 0f;
        public float initialFieldOfView = 0f;

        public bool initialDofEnable = false;
        public float initialDofFocus = 0f;
        public float initialDofRange = 0f;
        public float initialDofFarBlur = 0f;
        public float initialDofNearBlur = 0f;
        public float initialDofFarTransition = 0f;
        public float initialDofNearTransition = 0f;
        public float initialDofMinBlendDist = 0f;
        public Vector2 initalSensorSize = Vector2.zero;
        public float initialAperture = 0f;

        // which properties actually change and require update from proxy values     
        public bool dof_delta = false;
        public bool fov_delta = false;
        public bool animateDof = false;
        public bool animateFov = false;
        public bool overrideAperture = false;
        public bool overrideFieldOfView = false;

        public bool MultiplySensorSize;
        public float MultiplySensorScale = 1f;
        public float Aperture = 16f;
                
        public float fovOverrideValue = 27f;    // value to force fov to

        // build in user option
        public bool BuiltinQuickVolume = true;

        // urp scaling for extra 'Focal Length' property in the volume override
        public float FocusScale = 1.0f;

        #endregion Variables

        #region Pipeline specific functions   

// HDRP Spcific
#if HDRP_17_RUNTIME
        // no additional camera data needed
#elif HDRP_12_RUNTIME
        HDAdditionalCameraData HDCameraData = null;
#elif HDRP_RUNTIME
        public Volume volume;
        public VolumeProfile profile;
        public DepthOfField hdrpDof;
        HDAdditionalCameraData HDCameraData = null;
#elif URP_RUNTIME
        // URP Specific
        public UniversalAdditionalCameraData URPCameraData = null;
        public Volume volume;
        public VolumeProfile profile;        
        public DepthOfField urpDof;
#elif POST_PROCESSING
        // Built-in (Post Processing) settings
        public PostProcessVolume volume;
        public DepthOfField ppDof;
#endif

        bool cachedComponents = false;
        public bool CacheCameraComponents()
        {
            cachedComponents = false;
            CameraComponent = GetComponent<Camera>();

            // HDRP Specific
#if HDRP_17_RUNTIME
            runtimePipeline = RuntimePipeline.HighDefinition;
            cachedComponents = true;
#elif HDRP_12_RUNTIME
            runtimePipeline = RuntimePipeline.HighDefinition;
            HDCameraData = GetComponent<HDAdditionalCameraData>();
            if (HDCameraData == null)
            {
                //Debug.LogWarning("Cannot find HDAdditionalCameraData component");
                cachedComponents = false;

            }

            if (HDCameraData != null && CameraComponent != null)
            {
                //Debug.LogWarning("FOUND HDAdditionalCameraData component");
                cachedComponents = true;
            }
#elif HDRP_RUNTIME
            runtimePipeline = RuntimePipeline.HighDefinition;
            HDCameraData = GetComponent<HDAdditionalCameraData>();
            if (HDCameraData == null)
            {
                //Debug.LogWarning("Cannot find HDAdditionalCameraData component");
                cachedComponents = false;
            }

            if (HDCameraData != null && CameraComponent != null)
            {
                //Debug.LogWarning("FOUND HDAdditionalCameraData component");
                cachedComponents = true;
            }
            Volume[] volumes = FindObjectsOfType<Volume>();
            foreach (Volume vol in volumes)
            {
                if (vol.isGlobal)
                {
                    if (vol.sharedProfile != null)
                    {
                        if (initialDofEnable)
                        {
                            if (vol.sharedProfile.TryGet<DepthOfField>(out hdrpDof))
                            {
                                volume = vol;
                                profile = vol.sharedProfile;
                                break;
                            }
                        }
                    }
                }
            }
            cachedComponents = true;

#elif URP_RUNTIME
            // URP Specific
#if UNITY_EDITOR
            //Selection.activeGameObject = this.gameObject;
#endif
            runtimePipeline = RuntimePipeline.Uninversal;  
            URPCameraData = GetComponent<UniversalAdditionalCameraData>();
#if UNITY_2021_3_OR_NEWER
            Volume[] volumes = FindObjectsByType<Volume>(FindObjectsSortMode.None);
#else
            Volume[] volumes = FindObjectsOfType<Volume>();
#endif
            foreach (Volume vol in volumes)
            {
                if (vol.isGlobal)
                {
                    if (vol.sharedProfile != null)
                    {
                        if (initialDofEnable)
                        {
                            if (vol.sharedProfile.TryGet<DepthOfField>(out urpDof))
                            {
                                volume = vol;
                                profile = vol.sharedProfile;
                                break;
                            }
                            else
                            {
#if UNITY_EDITOR
                                try
                                {
                                    urpDof = VolumeProfileFactory.CreateVolumeComponent<DepthOfField>(profile: profile,
                                                                                                  overrides: true,
                                                                                                  saveAsset: true);
                                }
                                catch
                                {

                                }
#else
                                try
                                {
                                    urpDof = profile.Add<DepthOfField>(true);
                                }
                                catch
                                {

                                }
#endif
                            }
                        }
                        else
                        {
                            volume = vol;
                            profile = vol.profile;
                            break;
                        }
                    }
                }
            }

            if (initialDofEnable && urpDof != null)
            {
                urpDof.SetAllOverridesTo(true);
                DepthOfFieldModeParameter mode = new DepthOfFieldModeParameter(DepthOfFieldMode.Bokeh, true);
                urpDof.mode = mode;
            }

            cachedComponents = true;

            /*
            if (URPCameraData == null)
            {
                Debug.LogWarning("Cannot find UniversalAdditionalCameraData component");
                cachedComponents = false;
            }
            
            bool dofPass = initialDofEnable ? urpDof != null : true;
            if (URPCameraData != null && CameraComponent != null && dofPass)
            {
                Debug.LogWarning("FOUND Universal RP components");
                cachedComponents = true;
            }
            else
            {
                Debug.LogWarning("CameraComponent: " + (CameraComponent == null ? "NULL  " : "NOT NULL  ") + "URPCameraData: " + (URPCameraData == null ? "NULL  " : "NOT NULL  ")+ "urpDof: " + (urpDof == null ? "NULL  " : "NOT NULL  "));
            }
            */
#elif POST_PROCESSING
            // Built-in (Post Processing) settings
            runtimePipeline = RuntimePipeline.Builtin;

            // USER OPTIONS
            // find the global post processing volume then find/add the DOF settings
            // find/add the PostProcessLayer component on the camera
            // adjust the PostProcessLayer component so that its volumeLayer property 
            // matches the layer of the global volume
            //
            // this wil work best if the user has their own post process volume in its own dedicated layer

            // OR
            // find/add the PostProcessLayer component on the camera
            // get/set its volumelayer (fallback to layer of the camera) (NB post processing should really be done in its own unique layer)
            // make a PostProcessManager.instance.QuickVolume with a newly created DepthOfField object in the volumelayer
            //
            // The quickvolume method will obey any layer changes made by the user and will be less intrusive

            // find a global volume: this will have already been created by the importer or by the user
#if UNITY_2021_3_OR_NEWER
            PostProcessVolume[] volumes = GameObject.FindObjectsByType<PostProcessVolume>(FindObjectsSortMode.None);
#else
            PostProcessVolume[] volumes = GameObject.FindObjectsOfType<PostProcessVolume>();
#endif
            foreach (PostProcessVolume vol in volumes)
            {
                if (vol.isGlobal)
                {
                    volume = vol;
                    break;
                }
            }

            // get the layer
            string layer = string.Empty;
            int layerId = 0;

            if (volume != null)
            {
                layerId = volume.gameObject.layer;
            }
            else
            {
                layerId = gameObject.layer;
            }

            layer = LayerMask.LayerToName(layerId);
            LayerMask mask = LayerMask.GetMask(layer);
                        
            PostProcessLayer postProcessLayer = null;
            if (initialDofEnable)
            {
                if (BuiltinQuickVolume)
                {
                    ppDof = ScriptableObject.CreateInstance<DepthOfField>();
                    ppDof.enabled.Override(true);
                    ppDof.focusDistance.Override(1f);
                    ppDof.aperture.Override(1f);
                    postProcessLayer = GetComponent<PostProcessLayer>();
                    if (postProcessLayer == null)
                    {
                        postProcessLayer = gameObject.AddComponent<PostProcessLayer>();
                    }
                    if (postProcessLayer.volumeTrigger == null)
                        postProcessLayer.volumeTrigger = this.gameObject.transform;
                    postProcessLayer.volumeLayer = mask;

                    volume = PostProcessManager.instance.QuickVolume(layerId, 100f, ppDof);
                    volume.runInEditMode = true;
                }
                else
                {
                    if (volume.profile != null)
                    {
                        if (!volume.profile.TryGetSettings<DepthOfField>(out ppDof))
                        {
                            ppDof = volume.profile.AddSettings<DepthOfField>();
                        }

                        postProcessLayer = GetComponent<PostProcessLayer>();
                        if (postProcessLayer == null)
                        {
                            postProcessLayer = gameObject.AddComponent<PostProcessLayer>();
                        }
                        if (postProcessLayer.volumeTrigger == null)
                            postProcessLayer.volumeTrigger = this.gameObject.transform;
                        postProcessLayer.volumeLayer = mask;
                    }
                    else { Debug.LogWarning("The Global Post Processing Volume MUST have a profile assigned"); }
                }
            }

            if (CameraComponent == null)
            {
                //Debug.LogWarning("Cannot find Camera component");
                cachedComponents = false;
            }
            bool dofPass = initialDofEnable ? ppDof != null : true;            
            if (CameraComponent != null && dofPass)
            {
                //Debug.LogWarning("FOUND Camera component");
                cachedComponents = true;
            }
#else
            runtimePipeline = RuntimePipeline.Builtin;
            if (CameraComponent != null)
            {
                //Debug.LogWarning("FOUND Camera component");
                cachedComponents = true;
            }
#endif
            return cachedComponents;
        }

        public void InitialVolumeProperties()
        {
#if HDRP_RUNTIME
            // HDRP volume settings - managed by the camera
#elif URP_RUNTIME
            // URP volume settings - require reference to the DoF settings in the volume
            if (initialDofEnable && urpDof != null)
            {
                urpDof.focusDistance.value = initialDofFocus;
                urpDof.aperture.value = initialAperture;
            }
#elif POST_PROCESSING
            // Built-in (Post Processing) volume settings - require reference to the DoF settings in the volume
            if (initialDofEnable && ppDof != null)
            {
                ppDof.focusDistance.value = initialDofFocus;
                ppDof.aperture.value = initialAperture;
            }
#endif
            initialSetupComplete = true;
        }

#endregion Pipeline specific functions

        #region Scaling and conversion factors
        // ...
        #endregion Scaling and conversion factors

        #region Initial Setup by Importer
        // call SetupLight from  importer by reflection to set initial values with a frame delay to allow any init to complete
        // relevant to HDRP which adds a HDAdditionalCameraData component to the Camera GameObject
        // this requires a delayed application of settings
#if UNITY_EDITOR
        public void SetupCamera(string jsonDataString, AnimationClip clip)
        {
            //Debug.LogWarning("SetupCamera - clip is null: " + (clip == null).ToString());
            JsonCameraData settingsObject = null;
            try
            {
                settingsObject = JsonConvert.DeserializeObject<JsonCameraData>(jsonDataString);
            }
            catch (Exception ex)
            {
                Debug.Log("Failed to deserialize the setup object: " + ex.Message);
                return;
            }

            if (settingsObject == null) { Debug.LogWarning("Settings Object is null!"); return; }

            initialPosition = settingsObject.GetPosition();
            initialRotation = settingsObject.GetRotation();
            initialScale = settingsObject.GetScale();
            initialAnimateFov = settingsObject.fov_delta;
            initialFocalLength = settingsObject.FocalLength;
            initialFieldOfView = settingsObject.Fov;
            initialDofEnable = settingsObject.DofEnable;
            initialDofFocus = settingsObject.DofFocus;
            initialDofRange = settingsObject.DofRange;
            initialDofFarBlur = settingsObject.DofFarBlur;
            initialDofNearBlur = settingsObject.DofNearBlur;
            initialDofFarTransition = settingsObject.DofFarTransition;
            initialDofNearTransition = settingsObject.DofNearTransition;
            initialDofMinBlendDist = settingsObject.DofMinBlendDist;

            initalSensorSize = new Vector2(settingsObject.Width, settingsObject.Height);

            float alpha = ((initialDofRange + initialDofFarTransition + initialDofNearTransition) / 16f) * 0.01f;
            float beta = 1 / ((initialDofFarBlur + initialDofNearBlur) / 2);
            initialAperture = alpha * beta;
            Aperture = initialAperture;

            // which properties actually change and require update from proxy values     
            dof_delta = settingsObject.dof_delta;
            fov_delta = settingsObject.fov_delta;
            animateFov = initialAnimateFov;
            animateDof = initialDofEnable;
            if (clip != null) { CachedAnimation = clip; }
        }
#endif
#endregion Initial Setup by Importer

        #region Start and Update
        void Start()
        {
            CacheCameraComponents();
        }

        void Update()
        {
            if (!initialSetupComplete)
                InitialVolumeProperties();

            if (!cachedComponents)
                CacheCameraComponents();

            if (cachedComponents)
                UpdateCamera();
        }

        void UpdateCamera()
        {
            if (CameraComponent == null) return;

            if (animateFov)
            {
                if (overrideFieldOfView)
                    CameraComponent.fieldOfView = fovOverrideValue;
                else
                    CameraComponent.fieldOfView = ProxyFieldOfView;
            }

            if (animateDof)
            {
#if HDRP_17_RUNTIME
                CameraComponent.focusDistance = ProxyDofFocus * 0.01f;
                if (!overrideAperture)
                    CameraComponent.aperture = CalculateAperture();
                else
                    CameraComponent.aperture = Aperture;
#elif HDRP_12_RUNTIME
                if (HDCameraData != null)
                {                    
                    HDCameraData.physicalParameters.focusDistance = ProxyDofFocus * 0.01f;
                    if (!overrideAperture)
                        HDCameraData.physicalParameters.aperture = CalculateAperture();
                    else
                        HDCameraData.physicalParameters.aperture = Aperture;
                }
#elif HDRP_RUNTIME
                if (HDCameraData != null)
                {
                    if (!overrideAperture)
                        HDCameraData.physicalParameters.aperture = CalculateAperture();
                    else
                        HDCameraData.physicalParameters.aperture = Aperture;
                }
                if (hdrpDof != null)
                {
                    hdrpDof.focusDistance.value = ProxyDofFocus * 0.01f;
                }
#elif URP_RUNTIME
                if (initialDofEnable && urpDof != null)
                {
                    urpDof.focusDistance.value = ProxyDofFocus * 0.01f;
                    if (!overrideAperture)
                        urpDof.aperture.value = CalculateAperture();
                    else
                        urpDof.aperture.value = Aperture;

                    urpDof.focalLength.value = ProxyDofFocus * CalculateAperture() * FocusScale * 0.01f;
                }
#elif POST_PROCESSING
                if (initialDofEnable && ppDof != null)
                {
                    ppDof.focusDistance.value = ProxyDofFocus * 0.01f;
                    if (!overrideAperture)
                        ppDof.aperture.value = CalculateAperture();
                    else
                        ppDof.aperture.value = Aperture;

                    ppDof.focalLength.value = ProxyDofFocus * Aperture * FocusScale * 0.01f;
                }
#elif UNITY_2021_3_OR_NEWER
                CameraComponent.focusDistance = ProxyDofFocus * 0.01f;
                if (!overrideAperture)
                    CameraComponent.aperture = CalculateAperture();
                else
                    CameraComponent.aperture = Aperture;            
#endif
            }
        }

        public void UpdateSensorSize()
        {
            CameraComponent.sensorSize = initalSensorSize * MultiplySensorScale;
        }

        public void UpdateAperture()
        {
#if HDRP_RUNTIME

#if HDRP_17_RUNTIME
            if (CameraComponent == null)
            {
                CameraComponent.aperture = Aperture;
            }
#else
            if (HDCameraData != null)
            {     
                HDCameraData.physicalParameters.aperture = Aperture;
            }
#endif
#elif URP_RUNTIME
            if (initialDofEnable && urpDof != null)
            {
                urpDof.aperture.value = Aperture;
            }
#elif POST_PROCESSING
            if (initialDofEnable && ppDof != null)
            {
                ppDof.aperture.value = Aperture;
            }
#endif
        }

        public void UpdateFov()
        {
            if (CameraComponent == null) return;
            CameraComponent.fieldOfView = fovOverrideValue;
        }

        public float CalculateAperture()
        {
            float alpha = ((ProxyDofRange + ProxyDofFarTransition + ProxyDofNearTransition) / 16f) * 0.01f;
            float beta = 1 / ((ProxyDofFarBlur + ProxyDofNearBlur) / 2);
            return alpha * beta;
        }

        // Cleanup
#if HDRP_RUNTIME
        // HDRP Spcific        
#elif URP_RUNTIME
        // URP Specific        
#elif POST_PROCESSING
        // Builtin Specific
        private void OnEnable()
        {
            CacheCameraComponents();   
        }

        void OnDisable()
        {
            if (volume != null && BuiltinQuickVolume)
            {
#if UNITY_EDITOR
                PostProcessVolume.DestroyImmediate(volume, true);
#else
                RuntimeUtilities.DestroyVolume(volume, true);
#endif
            }
        }

        void OnDestroy()
        {
            if (volume != null && BuiltinQuickVolume)
            {
#if UNITY_EDITOR
                PostProcessVolume.DestroyImmediate(volume, true);
#else
                RuntimeUtilities.DestroyVolume(volume, true);
#endif
            }
        }
#endif

        #endregion Start and Update

        #region Class data
#if UNITY_EDITOR
        public class JsonCameraData
        {
            public const string linkIdStr = "link_id";
            public const string nameStr = "name";
            public const string locStr = "loc";
            public const string rotStr = "rot";
            public const string scaStr = "sca";
            public const string fovStr = "fov";
            public const string fitStr = "fit";
            public const string widthStr = "width";
            public const string heightStr = "height";
            public const string focalStr = "focal_length";
            public const string farStr = "far_clip";
            public const string nearStr = "near_clip";
            public const string posStr = "pos";
            public const string dofEnaStr = "dof_enable";
            public const string dofWeightStr = "dof_weight";
            public const string dofDecayStr = "dof_decay";
            public const string dofFocusStr = "dof_focus";
            public const string dofRanStr = "dof_range";
            public const string dofFarBlStr = "dof_far_blur";
            public const string dofNearBlStr = "dof_near_blur";
            public const string dofFarTranStr = "dof_far_transition";
            public const string dofNearTranStr = "dof_near_transition";
            public const string dofMinBlendStr = "dof_min_blend_distance";
            public const string framesStr = "frame_count";

            [JsonProperty(linkIdStr)]
            public string LinkId { get; set; }
            [JsonProperty(nameStr)]
            public string Name { get; set; }
            [JsonProperty(locStr)]
            public float[] loc { get; set; }
            [JsonIgnore]
            public Vector3 Pos { get { return this.GetPosition(); } }
            [JsonProperty(rotStr)]
            public float[] rot { get; set; }
            [JsonIgnore]
            public Quaternion Rot { get { return this.GetRotation(); } }
            [JsonProperty(scaStr)]
            public float[] sca { get; set; }
            [JsonIgnore]
            public Vector3 Scale { get { return this.GetScale(); } }
            [JsonProperty(fovStr)]
            public float Fov { get; set; }
            [JsonProperty(fitStr)]
            public string Fit { get; set; }
            [JsonProperty(widthStr)]
            public float Width { get; set; }
            [JsonProperty(heightStr)]
            public float Height { get; set; }
            [JsonProperty(focalStr)]
            public float FocalLength { get; set; }
            [JsonProperty(farStr)]
            public float FarClip { get; set; }
            [JsonProperty(nearStr)]
            public float NearClip { get; set; }
            [JsonProperty(posStr)]
            public float[] pos { get; set; }
            [JsonIgnore]
            public Vector3 PivotPosition { get { return this.GetPivot(); } }
            [JsonProperty(dofEnaStr)]
            public bool DofEnable { get; set; }
            [JsonProperty(dofWeightStr)]
            public float DofWeight { get; set; }
            [JsonProperty(dofDecayStr)]
            public float DofDecay { get; set; }
            [JsonProperty(dofFocusStr)]
            public float DofFocus { get; set; }
            [JsonProperty(dofRanStr)]
            public float DofRange { get; set; }
            [JsonProperty(dofFarBlStr)]
            public float DofFarBlur { get; set; }
            [JsonProperty(dofNearBlStr)]
            public float DofNearBlur { get; set; }
            [JsonProperty(dofFarTranStr)]
            public float DofFarTransition { get; set; }
            [JsonProperty(dofNearTranStr)]
            public float DofNearTransition { get; set; }
            [JsonProperty(dofMinBlendStr)]
            public float DofMinBlendDist { get; set; }
            [JsonProperty(framesStr)]
            public int FrameCount { get; set; }


            // Animated properties (determined by the importer - repackaged here to ease use of <CameraProxy> setup
            public const string dofDltStr = "dof_delta";
            public const string fovDltStr = "fov_delta";

            [JsonProperty(dofDltStr)]
            public bool dof_delta { get; set; }
            [JsonProperty(fovDltStr)]
            public bool fov_delta { get; set; }

            public JsonCameraData()
            {
                this.LinkId = string.Empty;
                this.Name = string.Empty;
                this.loc = new float[0];
                this.rot = new float[0];
                this.sca = new float[0];
                this.Fov = 0f;
                this.Fit = string.Empty;
                this.Width = 0f;
                this.Height = 0f;
                this.FocalLength = 0f;
                this.FarClip = 0f;
                this.NearClip = 0f;
                this.pos = new float[0];
                this.DofEnable = false;
                this.DofWeight = 0f;
                this.DofDecay = 0f;
                this.DofFocus = 0f;
                this.DofRange = 0f;
                this.DofFarBlur = 0f;
                this.DofNearBlur = 0f;
                this.DofFarTransition = 0f;
                this.DofNearTransition = 0f;
                this.DofMinBlendDist = 0f;
                this.FrameCount = 0;

                this.dof_delta = false;
                this.fov_delta = false;
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

            public Vector3 GetPivot()
            {
                return new Vector3(-pos[0], pos[2], -pos[1]) * 0.01f;
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
