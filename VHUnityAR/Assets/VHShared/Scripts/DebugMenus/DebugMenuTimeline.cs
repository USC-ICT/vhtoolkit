using Ride.Timeline;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ride.Examples
{
    /// <summary>
    /// Handles the Debug Menu interface for playing character-specific timelines.
    /// Displays a UI that allows selecting and playing different timelines.
    /// </summary>
    public class DebugMenuTimeline : RideMonoBehaviour
    {
        [Header("Timeline Parents")]
        [SerializeField] Transform m_timelines;

        private PlayableDirector m_currentTimeline;
        //[Header("CC Animators")]
        //[SerializeField] RuntimeAnimatorController m_animator;


        #region Debug Menu
        DebugMenu m_debugMenu;
        DemoController m_controller;
        DebugMenus m_debugMenusBase;
        Vector2 m_timelineScroll;
        #endregion


        /// <summary>
        /// Initializes references to the Debug Menu, Demo Controller, and Debug Menus Base.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_debugMenu = Globals.api.GetSystem<DebugMenu>();
            m_controller = FindAnyObjectByType<DemoController>();
            m_debugMenusBase = FindAnyObjectByType<DebugMenus>();
        }


        /// <summary>
        /// Handles the GUI drawing for the Timeline selection menu.
        /// Displays available timelines based on the selected character.
        /// </summary>
        public void OnGUITimeline()
        {
            //var character = m_controller.CurrentCharacter;

            m_debugMenusBase.OnGUICharacterConfig();

            // Create a button for each timeline with the name of the GameObject.
            using (var scrollPosition = new GUILayout.ScrollViewScope(m_timelineScroll, GUILayout.MaxHeight(200)))
            {
                m_timelineScroll = scrollPosition.scrollPosition;

                foreach (var director in m_timelines.GetComponentsInChildren<PlayableDirector>())
                {
                    if (m_debugMenu.Button(director.gameObject.name))
                    {
                        StopAllTimelines();
                        m_controller.CurrentCharacter.StopLipSyncPerformance();
                        m_controller.CurrentCharacter.StopAudio();

                        OverwriteCharacterName(director.gameObject.name, m_controller.CurrentCharacter.name);
                        director.Play();
                    }
                }
            }
        }

        public void OverwriteCharacterName(string directorName, string characterName)
        {
            PlayableDirector director = GetDirector(directorName);
            if (director == null) { return; }

            foreach (var output in director.playableAsset.outputs)
            {
                if (output.sourceObject is ControlTrack controlTrack)
                {
                    foreach (var clip in controlTrack.GetClips())
                    {
                        OverwriteCharacterName(clip.displayName, characterName);
                    }
                }

                else if (output.sourceObject is RideTimelineTrack track)
                {
                    foreach (var clip in track.GetClips())
                    {
                        Clip_vhPlayAudio audioClip = clip.asset as Clip_vhPlayAudio;
                        if (audioClip != null) { audioClip.m_behaviour.m_characterName = characterName; continue; }

                        Clip_vhBodyMovement bodyClip = clip.asset as Clip_vhBodyMovement;
                        if (bodyClip != null) { bodyClip.m_behaviour.m_characterName = characterName; continue; }

                        Clip_vhHeadMovement headClip = clip.asset as Clip_vhHeadMovement;
                        if (headClip != null) { headClip.m_behaviour.m_characterName = characterName; continue; }

                        Clip_vhFaceAnimation faceClip = clip.asset as Clip_vhFaceAnimation;
                        if (faceClip != null) { faceClip.m_behaviour.m_characterName = characterName; continue; }

                        foreach (var childTracks in clip.GetParentTrack().GetChildTracks())
                        {
                            foreach (var childClip in childTracks.GetClips())
                            {
                                Clip_vhPlayAudio audioClip_c = childClip.asset as Clip_vhPlayAudio;
                                if (audioClip_c != null) { audioClip_c.m_behaviour.m_characterName = characterName; continue; }

                                Clip_vhBodyMovement bodyClip_c = childClip.asset as Clip_vhBodyMovement;
                                if (bodyClip_c != null) { bodyClip_c.m_behaviour.m_characterName = characterName; continue; }

                                Clip_vhHeadMovement headClip_c = childClip.asset as Clip_vhHeadMovement;
                                if (headClip_c != null) { headClip_c.m_behaviour.m_characterName = characterName; continue; }

                                Clip_vhFaceAnimation faceClip_c = childClip.asset as Clip_vhFaceAnimation;
                                if (faceClip_c != null) { faceClip_c.m_behaviour.m_characterName = characterName; continue; }
                            }
                        }
                    }
                }
            }
        }

        public PlayableDirector GetDirector(string directorName)
        {
            List<PlayableDirector> directorList = FindObjectsByType<PlayableDirector>(FindObjectsSortMode.None).ToList();

            foreach (var director in directorList)
            {
                if (director.name == directorName)
                    return director;
            }

            Debug.LogWarning($"TimelineManager.cs::GetDirector() - Failed to find '{directorName}'");
            return null;
        }

        public void StopAllTimelines()
        {
            foreach (var director in GetAllDirectors())
            {
                director.Stop();
            }
        }

        public List<PlayableDirector> GetAllDirectors()
        {
            return FindObjectsByType<PlayableDirector>(FindObjectsSortMode.None).ToList();
        }


    }
}
