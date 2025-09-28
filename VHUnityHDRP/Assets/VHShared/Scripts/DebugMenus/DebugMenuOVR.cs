using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Ride.Examples
{
#if false
        public class DebugMenuOVR : RideMonoBehaviour
        {
#if !UNITY_WEBGL
            [Header("Default Animator")]
            [SerializeField] RuntimeAnimatorController m_animator;
            [SerializeField] Avatar m_avatar;

            [Header("CC Animator")]
            [SerializeField] RuntimeAnimatorController m_ccAnimator;
            //[SerializeField] RuntimeAnimatorController m_ccFemaleAnimator;
            [SerializeField] Avatar m_ccAvatar;
            //[SerializeField] Avatar m_ccFemaleAvatar;

            //[Header("Camera Position")]
            //[SerializeField] Vector3 m_bodyPosition = new();
            //[SerializeField] Vector3 m_bodyRotation = new();
            //[SerializeField] Vector3 m_facePosition = new();
            //[SerializeField] Vector3 m_faceRotation = new();

            [Header("Viseme Mapping")]
            //[SerializeField] List<OvrMappingToCC> m_ovrMappings = new();

            private string m_defaultIdleAnim = "SubtleIdleLoop";
            private string[] m_visemeShapes = new string[]
            {
                "Sil","PP","FF","TH","DD","kk","CH","SS","nn","RR","aa","E","I","O","U",
            };

            private List<float> m_cachedBlendValue = new();

            //private float Merged_Open_Mouth;
            //private float V_Open;
            //private float V_Explosive;
            //private float V_Dental_Lip;
            //private float V_Tight_O;
            //private float V_Tight;
            //private float V_Wide;
            //private float V_Affricate;
            //private float V_Lip_Open;

            #region Debug menu variables
            DebugMenu m_debugMenu;
            DebugMenus m_debugMenusBase;
            DemoController m_controller;
            #endregion

            protected override void Start()
            {
                base.Start();
                m_debugMenu = Globals.api.GetSystem<DebugMenu>();
                m_controller = FindAnyObjectByType<DemoController>();
                m_debugMenusBase = FindAnyObjectByType<DebugMenus>();
            }

            public void OnGuiOvrLipsync()
            {
#if false
                m_debugMenusBase.OnGUICharacterConfig();
                var character = m_controller.CurrentCharacter;
                var ovrComp = character.GetComponentInChildren<OVRLipSyncContextMorphTarget>();
                if (ovrComp == null)
                {
                    m_debugMenu.DrawGUILabel("<b>Please select a different character.</b>");
                    return;
                }

                m_debugMenu.DrawGUILabel("<b>Reset:</b>");
                if (m_debugMenu.DrawGUIButton("Reset"))
                {
                    character.GetComponent<Animator>().runtimeAnimatorController = m_animator;
                    character.GetComponent<Animator>().avatar = m_avatar;

                    var ovrContext = character.GetComponent<OVRLipSyncContext>();
                    var overMorphTarget = character.GetComponent<OVRLipSyncContextMorphTarget>();

                    ovrContext.gameObject.SetActive(true);
                    overMorphTarget.gameObject.SetActive(true);
                }

                m_debugMenu.DrawGUILabel("<b>Visemse:</b>");
                DrawVisemeSelection();
#endif
            }

            private int visemeIdx = 0;
            private void DrawVisemeSelection()
            {
                int selectedIndex = m_debugMenu.DrawGUISelectionGrid(visemeIdx, m_visemeShapes, 4);
                bool didSelectionChange = (selectedIndex != visemeIdx);

                visemeIdx = selectedIndex;
                DrawConfiguration(didSelectionChange);
                PlayViseme();
            }

            private void PlayViseme()
            {
#if false
                string visemeName = m_visemeShapes[visemeIdx];
                OvrMappingToCC mappingScript = m_ovrMappings.Where(x => x.name == visemeName).FirstOrDefault();
                if (mappingScript == null) { Debug.LogWarning($"DebugMenuOVR.cs:::Failed to find {visemeName} in the ovrMappings param"); return; }

                var character = m_controller.CurrentCharacter;
                character.GetComponent<Animator>().runtimeAnimatorController = m_ccAnimator;
                character.GetComponent<Animator>().avatar = m_ccAvatar;

                var ovrContext = character.GetComponentInChildren<OVRLipSyncContext>();
                var overMorphTarget = character.GetComponentInChildren<OVRLipSyncContextMorphTarget>();

                ovrContext.enabled = false;
                overMorphTarget.enabled = false;

                //foreach (var map in mappingScript.m_blendshapeEntries)
                //{
                //    int blendshapeIdx = overMorphTarget.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(map.blendshape);
                //    overMorphTarget.skinnedMeshRenderer.SetBlendShapeWeight(blendshapeIdx, map.value);
                //}
                for (int i = 0; i < mappingScript.m_blendshapeEntries.Count(); ++i)
                {
                    var mapEntry = mappingScript.m_blendshapeEntries[i];
                    int blendshapeIdx = overMorphTarget.skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(mapEntry.blendshape);
                    overMorphTarget.skinnedMeshRenderer.SetBlendShapeWeight(blendshapeIdx, m_cachedBlendValue[i]);
                }
#endif
            }

            private void DrawConfiguration(bool didSelectionChange)
            {
                string visemeName = m_visemeShapes[visemeIdx];
                OvrMappingToCC mappingScript = m_ovrMappings.Where(x => x.name == visemeName).FirstOrDefault();
                if (mappingScript == null) { Debug.LogWarning($"DebugMenuOVR.cs:::Failed to find {visemeName} in the ovrMappings param"); return; }

                if (didSelectionChange || m_cachedBlendValue.Count == 0)
                {
                    //reset cached blendshape values
                    m_cachedBlendValue.Clear();
                    foreach (var param in mappingScript.m_blendshapeEntries)
                    {
                        m_cachedBlendValue.Add(param.value);
                    }
                }

                for (int i = 0; i < mappingScript.m_blendshapeEntries.Count(); ++i)
                {
                    m_debugMenu.DrawGUILabel(mappingScript.m_blendshapeEntries[i].blendshape);
                    m_cachedBlendValue[i] = m_debugMenu.DrawGUIHorizontalSlider(m_cachedBlendValue[i], 0, 100f);
                }

                m_debugMenu.DrawGUILabel("<b>Save Current Params</b>");
                if (m_debugMenu.DrawGUIButton("Save"))
                {
                    for (int i = 0; i < m_cachedBlendValue.Count(); ++i)
                    {
                        mappingScript.m_blendshapeEntries[i].value = m_cachedBlendValue[i];
                    }
                }
            }
        }

    }
#endif
#endif
}
