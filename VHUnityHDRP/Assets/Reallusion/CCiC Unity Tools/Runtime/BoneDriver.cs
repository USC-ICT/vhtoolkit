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

//#define BENCHMARK

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Reallusion.Runtime
{
    [ExecuteInEditMode]
    public class BoneDriver : MonoBehaviour
    {
        #region Vars
        [SerializeField][HideInInspector] private SkinnedMeshRenderer baseBodySmr;
        [SerializeField] public ExpressionGlossary glossary;
        [SerializeField][HideInInspector] private List<UpdateConstraint> constraintList;
        [SerializeField][HideInInspector] private List<UpdateConstraint> limitList;
        [SerializeField][HideInInspector] private List<int> constraintTargets;
        [SerializeField][HideInInspector] private List<SkinnedMeshRenderer> blendShapeTargets;
        [SerializeField][HideInInspector] private int blendShapeCount = 0;
        [SerializeField][HideInInspector] private BlendShapeIndex[] atlas;
        [SerializeField][HideInInspector] private string glossaryString;
        [SerializeField][HideInInspector] private string constraintString;
        [SerializeField][HideInInspector] private bool bonesSetup;
        [SerializeField][HideInInspector] private bool expressionsSetup;
        [SerializeField][HideInInspector] private bool constraintSetup;
        [SerializeField][HideInInspector] public bool bones;
        [SerializeField][HideInInspector] public bool expressions;
        [SerializeField][HideInInspector] public bool constraint;
        [SerializeField][HideInInspector] public bool amplify = false;
        [SerializeField][HideInInspector] public float visemePower = 1f;
        [SerializeField][HideInInspector] public float visemeScale = 1f;
        [SerializeField][HideInInspector] private VisemeByRenderer[] visemesByRenderer;
        [SerializeField][HideInInspector] public bool driveEyes = true;
        [SerializeField][HideInInspector] public bool driveJaw = true;
        [SerializeField][HideInInspector] public bool driveNeck = true;
        [SerializeField][HideInInspector] public bool driveTongue = true;
        [SerializeField][HideInInspector] public bool driveTeeth = true;
        [SerializeField][HideInInspector] public bool driveHead = true;

        #endregion Vars

        #region Setup
        public void SetupFromJson(string glossarySetupString, string constraintSetupString, bool bonesEnable, bool expressionsEnable, bool constraintEnable, bool driveHeadBone)
        {
            glossaryString = glossarySetupString;
            constraintString = constraintSetupString;
            bonesSetup = bonesEnable;
            expressionsSetup = expressionsEnable;
            constraintSetup = constraintEnable;

            // ui toggles
            bones = bonesEnable;
            expressions = expressionsEnable;
            constraint = constraintEnable;

            driveEyes = true;
            driveJaw = true;
            driveNeck = true;
            driveTongue = true;
            driveTeeth = true;
            driveHead = driveHeadBone;

            try
            {
                glossary = JsonConvert.DeserializeObject<ExpressionGlossary>(glossarySetupString);
                constraintList = new List<UpdateConstraint>();
                limitList = new List<UpdateConstraint>();
                if (constraintEnable)
                {
                    //Debug.Log("Deserializing Constraints");
                    List<UpdateConstraint> fullList = JsonConvert.DeserializeObject<List<UpdateConstraint>>(constraintSetupString);

                    constraintList = fullList.FindAll(x => x.UpdateMode == UpdateMode.Add);
                    limitList = fullList.FindAll(x => x.UpdateMode == UpdateMode.Limit);
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Failed to deserialize the setup object: " + ex.Message);
                return;
            }

            CacheConstraintTargets();
            CacheVisemesByRenderer();
            CacheTransformData();
            CacheBlendShapeTargetObjects();
        }

        public void SetupFlags(bool bonesEnable, bool expressionsEnable, bool constraintEnable)
        {
            bonesSetup = bonesEnable;
            expressionsSetup = expressionsEnable;
            constraintSetup = constraintEnable;

            // ui toggles
            bones = bonesEnable;
            expressions = expressionsEnable;
            constraint = constraintEnable;

            CacheConstraintTargets();
            CacheVisemesByRenderer();
            CacheTransformData();
            CacheBlendShapeTargetObjects();
        }

        public void RebuildSetup()
        {
            //Debug.Log("Rebuilding BoneDriver setup");
            amplify = false;
            visemePower = 1f;
            visemeScale = 1f;
            bones = bonesSetup;
            expressions = expressionsSetup;
            SetupFromJson(glossaryString, constraintString, bonesSetup, expressionsSetup, constraintSetup, driveHead);
        }

        public void TestLogGlossary()
        {
            foreach (ExpressionByBone ebb in glossary.ExpressionsByBone)
            {
                Debug.Log($"BoneName {ebb.BoneName}");
                foreach (Expression e in ebb.Expressions)
                {
                    Debug.Log($"    {e.ExpressionName} idx: {e.BlendShapeIndex}");
                }
            }
        }

        void CacheTransformData()
        {
            if (glossary == null)
            {
                Debug.LogWarning("No glossary object found for BoneDriver");
                return;
            }

            foreach (ExpressionByBone ebb in glossary.ExpressionsByBone)
            {
                /*
                GameObject go = GameObject.Find(ebb.BoneName);
                if (go != null)
                    ebb.BoneTransform = go.transform;
                */
                ebb.BoneTransform = FindTransformInThisHierarchy(ebb.BoneName);
                ebb.BoneMatch = GetBoneMatch(ebb.BoneName);
                ebb.RefPosition = ebb.GetRefPosition();
                ebb.RefRotation = ebb.GetRefRotaion();

                foreach (Expression e in ebb.Expressions)
                {
                    e.Translate = e.GetTranslate();
                    e.Rotation = e.GetRotaion();
                }
            }
        }

        Transform FindTransformInThisHierarchy(string name)
        {
            // this script is implicitly placed on "*":
            //
            //  Character Root
            //         |-> CC_Base_Body *

            Transform root = this.transform.parent;
            Transform[] transforms = root.gameObject.GetComponentsInChildren<Transform>();
            try
            {
                Transform match = transforms.FirstOrDefault(t => t.name == name);
                if (match != null)
                    return match;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Unable to find Transform {name} in hierarchy: {e.Message}");
                return null;
            }
            Debug.LogWarning($"Unable to find Transform {name} in hierarchy.");
            return null;
        }

        BoneMatch GetBoneMatch(string boneName)
        {
            if (boneName.ToLower().Contains("jaw") || boneName.ToLower().Contains("mouth")) return BoneMatch.Jaw;
            if (boneName.ToLower().Contains("eye")) return BoneMatch.Eye;
            //if (boneName.ToLower().Contains("neck")) return BoneMatch.Neck;
            if (boneName.ToLower().Contains("tongue")) return BoneMatch.Tongue;
            if (boneName.ToLower().Contains("teeth")) return BoneMatch.Teeth;
            if (boneName.ToLower().Contains("head") || boneName.ToLower().Contains("neck")) return BoneMatch.Head;
            return BoneMatch.None;
        }

        public string[] RetrieveBoneArray()
        {
            List<string> bones = new List<string>();
            if (glossary == null) return bones.ToArray();

            try
            {
                foreach (ExpressionByBone ebb in glossary.ExpressionsByBone)
                {
                    bones.Add(ebb.BoneName);
                }
                return bones.ToArray();
            }
            catch (Exception e) { Debug.LogWarning($"Cannot retrive list of bones driven by expressions: {e.Message}"); return bones.ToArray(); }
        }

        public Dictionary<string, List<string>> RetrieveBoneDictionary()
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            if (glossary == null) return dict;

            try
            {
                foreach (ExpressionByBone ebb in glossary.ExpressionsByBone)
                {
                    string bone = ebb.BoneName;
                    List<string> blendShapes = new List<string>();
                    foreach (Expression exp in ebb.Expressions)
                    {
                        blendShapes.Add(exp.ExpressionName);
                    }
                    dict.Add(bone, blendShapes);
                }
                return dict;
            }
            catch
            {

            }

            return dict;
        }

        void CacheBlendShapeTargetObjects()
        {
            baseBodySmr = GetComponent<SkinnedMeshRenderer>();
            blendShapeCount = baseBodySmr.sharedMesh.blendShapeCount;

            var smrs = this.transform.parent.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

            blendShapeTargets = new List<SkinnedMeshRenderer>();
            foreach (var s in smrs)
            {
                if (s.sharedMesh.blendShapeCount > 0 && s.name != this.gameObject.name && s.name != "CC_Base_Tongue")
                    blendShapeTargets.Add(s);
            }
            /*
            foreach (var s in blendShapeTargets)
            {
                Debug.Log($"SMR: {s.name} Count: {s.sharedMesh.blendShapeCount}");
            }
            */
            atlas = new BlendShapeIndex[blendShapeCount];

            for (int i = 0; i < blendShapeCount; i++)
            {
                BlendShapeIndex b = new BlendShapeIndex(i);
                string name = baseBodySmr.sharedMesh.GetBlendShapeName(i);
                List<BlendTarget> targets = new List<BlendTarget>();
                foreach (var sharedMeshRenderer in blendShapeTargets)
                {
                    int shapeIndex = sharedMeshRenderer.sharedMesh.GetBlendShapeIndex(name);
                    if (shapeIndex != -1)
                    {
                        targets.Add(new BlendTarget(sharedMeshRenderer, shapeIndex));
                    }
                }
                b.blendTargets = targets.ToArray();
                atlas[i] = b;
            }
        }

        void CacheVisemesByRenderer()
        {
            string[] rendererNames = new string[] { name };//{ "CC_Base_Body", "CC_Base_Tongue" };

            string[] visemesTotal = new string[]
            {
                "Open",
                "V_Open",
                "Explosive",
                "V_Explosive",
                "Dental_Lip",
                "V_Dental_Lip",
                "Tight-O",
                "V_Tight_O",
                "Tight",
                "V_Tight",
                "Wide",
                "V_Wide",
                "Affricate",
                "V_Affricate",
                "Lip_Open",
                "V_Lip_Open",
                "Tongue_up",
                "V_Tongue_up",
                "Tongue_Raise",
                "V_Tongue_Raise",
                "Tongue_Out",
                "V_Tongue_Out",
                "Tongue_Narrow",
                "V_Tongue_Narrow",
                "Tongue_Lower",
                "V_Tongue_Lower",
                "Tongue_Curl-U",
                "V_Tongue_Curl_U",
                "Tongue_Curl-D",
                "V_Tongue_Curl_D",
                "EE",
                "Er",
                "Ih",
                "IH",
                "Ah",
                "Oh",
                "W_OO",
                "S_Z",
                "Ch_J",
                "F_V",
                "Th",
                "TH",
                "T_L_D_N",
                "B_M_P",
                "K_G_H_NG",
                "AE",
                "R",
            };

            List<VisemeByRenderer> vbr = new List<VisemeByRenderer>();
            foreach (var rendererName in rendererNames)
            {
                List<int> indicies = new List<int>();
                Transform t = FindTransformInThisHierarchy(rendererName);
                if (t != null)
                {
                    SkinnedMeshRenderer s = t.gameObject.GetComponent<SkinnedMeshRenderer>();
                    if (s != null)
                    {
                        for (int i = 0; i < s.sharedMesh.blendShapeCount; i++)
                        {
                            if (visemesTotal.Contains(s.sharedMesh.GetBlendShapeName(i)))
                            {
                                indicies.Add(i);
                            }
                        }
                    }
                    vbr.Add(new VisemeByRenderer(s, indicies.ToArray()));
                }
            }
            visemesByRenderer = vbr.ToArray();
        }

        void CacheConstraintTargets()
        {
            constraintTargets = new List<int>();

            if (constraintList != null)
            {
                foreach (UpdateConstraint constraint in constraintList)
                {
                    if (!constraintTargets.Contains(constraint.TargetIndex))
                        constraintTargets.Add(constraint.TargetIndex);
                }
            }
        }

        #endregion Setup

        #region Update
        void Start()
        {
            if (baseBodySmr == null) baseBodySmr = GetComponent<SkinnedMeshRenderer>();
            try
            {
                if (glossary.ExpressionsByBone.FindAll(p => p.BoneTransform == null).Count() > 0)
                {
                    RebuildSetup();
                }
            }
            catch { Debug.LogWarning("Error rebuilding BoneDriver setup"); }

            if (visemesByRenderer == null) RebuildSetup();

#if BENCHMARK
            testCount = 0;
            total = 0f;
#endif
        }

        private void Update()
        {
            if (baseBodySmr == null) baseBodySmr = GetComponent<SkinnedMeshRenderer>();
#if BENCHMARK
            animatorStart = Time.realtimeSinceStartup;
#endif
        }

        private void LateUpdate()
        {
            if (baseBodySmr == null) return;

            bool isVisible = false;
            if (Camera.main) isVisible = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), baseBodySmr.bounds);
#if UNITY_EDITOR
            if (SceneView.lastActiveSceneView) isVisible |= GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(SceneView.lastActiveSceneView.camera), baseBodySmr.bounds);
#endif
            isVisible &= baseBodySmr.isVisible;
            if (!isVisible) return;

#if BENCHMARK
            animatorEnd = Time.realtimeSinceStartup;
            float elapsed = animatorEnd - animatorStart;
            if (testCount++ < 1000)
            {
                total += elapsed;
            }
            else
            {
                Debug.Log(total);
                testCount = 0;
                total = 0f;
            }

            /*
            0.2143044
            0.2630916
            */

            //Benchmark();
            Benchmark2();
#else
            if (amplify) LateUpdateVisemeAmplifier();
            if (constraint) LateUpdateConstraintDriver();
            if (bones) LateUpdateBoneDriver();
            if (expressions) LateUpdateExpressionTranspose();
#endif
        }

        void LateUpdateVisemeAmplifier()
        {
            if (visemesByRenderer == null) return;

            foreach (VisemeByRenderer v in visemesByRenderer)
            {
                for (int i = 0; i < v.visemeIndicies.Length; i++)
                {
                    float current = v.smr.GetBlendShapeWeight(v.visemeIndicies[i]);
                    if (v.lastSetValues[i] == current)
                    {
#if UNITY_EDITOR

#endif
                        continue;
                    }

                    float amplified = Amplified(current);
                    v.lastSetValues[i] = amplified;
                    v.smr.SetBlendShapeWeight(v.visemeIndicies[i], amplified);
                }
            }
        }

        float Amplified(float current)
        {
            float normalized = current / 100f;
            float abs = Mathf.Abs(normalized);
            float scaled = Mathf.Pow(abs, visemePower) * visemeScale * Mathf.Sign(current);
            return Mathf.Clamp(scaled * 100f, -150f, 150f);
        }

        void LateUpdateConstraintDriver()
        {
            foreach (int i in constraintTargets)
            {
                baseBodySmr.SetBlendShapeWeight(i, 100f);
            }

            foreach (UpdateConstraint u in constraintList)
            {
                switch (u.CurveMode)
                {
                    case CurveMode.Direct:
                        {
                            float src = baseBodySmr.GetBlendShapeWeight(u.SourceIndex) / 100f;
                            float tgt = baseBodySmr.GetBlendShapeWeight(u.TargetIndex);
                            tgt = (tgt * src);
                            baseBodySmr.SetBlendShapeWeight(u.TargetIndex, tgt);
                            break;
                        }
                    case CurveMode.Proportional:
                        {
                            float src = baseBodySmr.GetBlendShapeWeight(u.SourceIndex) / 100f;
                            float tgt = baseBodySmr.GetBlendShapeWeight(u.TargetIndex);
                            tgt = (tgt * src);
                            //tgt = tgt * src * u.Gradient;
                            baseBodySmr.SetBlendShapeWeight(u.TargetIndex, 0f);
                            break;
                        }
                    case CurveMode.Sawtooth:
                        {
                            float src = baseBodySmr.GetBlendShapeWeight(u.SourceIndex) / 100f;
                            float tgt = baseBodySmr.GetBlendShapeWeight(u.TargetIndex);
                            tgt = tgt * GetY(src);
                            baseBodySmr.SetBlendShapeWeight(u.TargetIndex, tgt);
                            break;
                        }
                    case CurveMode.None:
                        {
                            break;
                        }
                }
            }

            foreach (UpdateConstraint u in limitList)
            {
                float src = baseBodySmr.GetBlendShapeWeight(u.SourceIndex);
                float tgt = baseBodySmr.GetBlendShapeWeight(u.TargetIndex);
                tgt = Mathf.Clamp(tgt, -src, src);
                baseBodySmr.SetBlendShapeWeight(u.TargetIndex, tgt);
            }

        }

        float GetY(float x)
        {
            return Mathf.Clamp(1f - (2f * Mathf.Abs(x - 0.5f)), 0f, 1f);
        }


        void LateUpdateBoneDriver()
        {
            if (glossary == null) return;

            foreach (ExpressionByBone ebb in glossary.ExpressionsByBone)
            {
                bool drive = false;
                if (ebb.BoneTransform != null)
                {
                    switch (ebb.BoneMatch)
                    {
                        case BoneMatch.None:
                            {
                                drive = true;
                                break;
                            }
                        case BoneMatch.Eye:
                            {
                                drive = driveEyes;
                                break;
                            }
                        case BoneMatch.Jaw:
                            {
                                drive = driveJaw;
                                break;
                            }
                        case BoneMatch.Neck:
                            {
                                drive = driveNeck;
                                break;
                            }
                        case BoneMatch.Tongue:
                            {
                                drive = driveTongue;
                                break;
                            }
                        case BoneMatch.Teeth:
                            {
                                drive = driveTeeth;
                                break;
                            }
                        case BoneMatch.Head:
                            {
                                drive = driveHead;
                                break;
                            }
                        default:
                            {
                                drive = true;
                                break;
                            }
                    }
                }


                if (drive)
                {
                    Vector3 positionDelta = ebb.RefPosition;
                    Quaternion rotationDelta = ebb.RefRotation;

                    foreach (Expression e in ebb.Expressions)
                    {
                        if (e.isConstraint) continue;

                        float weight = baseBodySmr.GetBlendShapeWeight(e.BlendShapeIndex);
                        if (weight != 0)
                        {
                            positionDelta += (e.Translate * (weight / 100));
                            rotationDelta = rotationDelta * Scale(e.Rotation, (weight / 100));
                        }
                    }

                    ebb.BoneTransform.localPosition = positionDelta;
                    ebb.BoneTransform.localRotation = rotationDelta;
                }
            }
        }


        public static Quaternion Scale(Quaternion q, float scalar)
        {
            return Quaternion.Lerp(Quaternion.identity, q, scalar);
        }

        void LateUpdateExpressionTranspose()
        {
            foreach (BlendShapeIndex b in atlas)
            {
                foreach (var targets in b.blendTargets)
                {
                    targets.smr.SetBlendShapeWeight(targets.idx, baseBodySmr.GetBlendShapeWeight(b.idx));
                }
            }
        }
        #endregion Update

        #region Test
#if BENCHMARK
        int testCount = 0;
        float total = 0f;
        float animatorStart = 0f;
        float animatorEnd = 0f;
        
        void Benchmark()
        {
            float start = Time.realtimeSinceStartup;
            foreach (BlendShapeIndex b in atlas)
            {
                foreach (var targets in b.blendTargets)
                {
                    float weight = baseBodySmr.GetBlendShapeWeight(b.idx);
                    if (weight != b.lastValue)
                    {
                        b.lastValue = weight;
                        targets.smr.SetBlendShapeWeight(targets.idx, weight);
                    }
                }
            }
            float end = Time.realtimeSinceStartup;
            float elapsed = end - start;
            total += elapsed;
            if (testCount++ < 1000)
            {
                total += elapsed;
            }
            else
            {
                Debug.Log(total); // 0.0004048347
                testCount = 0;
                total = 0f;
            }
        }

        void Benchmark2()
        {
            float start = Time.realtimeSinceStartup;
            foreach (BlendShapeIndex b in atlas)
            {
                foreach (var targets in b.blendTargets)
                {
                    targets.smr.SetBlendShapeWeight(targets.idx, baseBodySmr.GetBlendShapeWeight(b.idx));
                }
            }
            float end = Time.realtimeSinceStartup;
            float elapsed = end - start;
            total += elapsed;
            if (testCount++ < 1000)
            {
                total += elapsed;
            }
            else
            {
                Debug.Log(total); // 0.0002827644
                testCount = 0;
                total = 0f;
            }
        }
#endif
        #endregion Test

        #region Class Data
        [Serializable]
        public class BlendShapeIndex
        {
            public int idx;
            public float lastValue;
            public BlendTarget[] blendTargets;

            public BlendShapeIndex(int i)
            {
                idx = i;
                lastValue = -1000f;
                blendTargets = new BlendTarget[0];
            }
        }

        [Serializable]
        public class BlendTarget
        {
            public SkinnedMeshRenderer smr;
            public int idx;

            public BlendTarget(SkinnedMeshRenderer r, int i)
            {
                smr = r;
                idx = i;
            }
        }

        // to be deserialized from JSON
        [Serializable]
        public class ExpressionGlossary
        {
            public List<ExpressionByBone> ExpressionsByBone;

            public ExpressionGlossary()
            {
                ExpressionsByBone = new List<ExpressionByBone>();
            }
        }

        [Serializable]
        public class ExpressionByBone
        {
            public string BoneName;
            [JsonIgnore]
            public Transform BoneTransform;
            [JsonIgnore]
            public BoneMatch BoneMatch;

            // bind pose data from skeleton avatar.humanDescription.skeleton
            public float[] RefPositionArr;
            public float[] RefRotationArr;

            [JsonIgnore]
            public Vector3 RefPosition;
            [JsonIgnore]
            public Quaternion RefRotation;

            public List<Expression> Expressions;

            public ExpressionByBone(string name, Vector3 position, Quaternion rotation)
            {
                Expressions = new List<Expression>();
                BoneName = name;
                RefPositionArr = new float[] { position.x, position.y, position.z };
                RefRotationArr = new float[] { rotation.x, rotation.y, rotation.z, rotation.w };
            }

            public Vector3 GetRefPosition()
            {
                return new Vector3(RefPositionArr[0], RefPositionArr[1], RefPositionArr[2]);
            }

            public Quaternion GetRefRotaion()
            {
                return new Quaternion(RefRotationArr[0], RefRotationArr[1], RefRotationArr[2], RefRotationArr[3]);
            }
        }

        [Serializable]
        public class Expression
        {
            public string ExpressionName;
            public int BlendShapeIndex;
            public bool isViseme;
            public bool isConstraint;

            public float[] TranslateArr;
            public float[] RotationArr;

            [JsonIgnore]
            public Vector3 Translate;
            [JsonIgnore]
            public Quaternion Rotation;

            public Expression(string name, int index, bool viseme, Vector3 translate, Vector4 rotation)
            {
                ExpressionName = name;
                BlendShapeIndex = index;
                isViseme = viseme;
                TranslateArr = new float[] { translate.x, translate.y, translate.z };
                RotationArr = new float[] { rotation.x, rotation.y, rotation.z, rotation.w };
                //isConstraint = name.ToLower().StartsWith("c_");
            }

            public Vector3 GetTranslate()
            {
                return new Vector3(TranslateArr[0], TranslateArr[1], TranslateArr[2]);
            }

            public Quaternion GetRotaion()
            {
                return new Quaternion(RotationArr[0], RotationArr[1], RotationArr[2], RotationArr[3]);
            }
        }

        [Serializable]
        public enum BoneMatch
        {
            None,  //  when none (i.e. created before this was introduced and the bonedriver has yet to be rebuilt), ignore the driver flags and drive the bone  
            Eye,
            Jaw,
            Neck,
            Tongue,
            Teeth,
            Head,
        }

        [Serializable]
        public class VisemeByRenderer
        {
            public SkinnedMeshRenderer smr;
            public int[] visemeIndicies;
            public float[] lastSetValues;

            public VisemeByRenderer(SkinnedMeshRenderer smr, int[] visemeIndicies)
            {
                this.smr = smr;
                this.visemeIndicies = visemeIndicies;
                this.lastSetValues = new float[visemeIndicies.Length];
            }
        }

        [Serializable]
        public class JsonConstraint
        {
            public string ConstraintName;
            public string[] SourceChannels;
            public string TargetChannel;
            public string CurveMode;
            public List<float[]> Curve;
            public string Mode;

            public JsonConstraint(string constraintName)
            {
                ConstraintName = constraintName;
            }
        }

        [Serializable]
        public enum UpdateMode
        {
            None = 0,
            Add = 1,
            Limit = 2,
        }

        public enum CurveMode
        {
            None = 0,
            Direct = 1,
            Proportional = 2,
            Sawtooth = 3,
        }

        [Serializable]
        public class UpdateConstraint
        {
            public int SourceIndex;
            public int TargetIndex;
            public UpdateMode UpdateMode;
            public CurveMode CurveMode;
            public float Gradient;
        }

        #endregion Class Data
    }
}