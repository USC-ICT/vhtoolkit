using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using VHAssets;

/// <summary>
/// For VITA male characters, a mapping of 'generic' events to specific state names in an animator controller.
/// </summary>
public class GenericMaleAdultGestureMap : GestureMapDefinition
{
    
    void Awake()
    {
        gestureMapName = "GenericMaleAdultAvgGesture";

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_SingleTapSmLf01", "YOU", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_SingleTapSmRt01", "YOU", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PointSmLf01", "YOU", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PointSmRt01", "YOU", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_You02", "YOU", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Me01", "ME", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        // gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Me01", "ME", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_RollSmLf02", "ME", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Me02", "ME", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_AllTapSmLf01", "WE", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_AllTapSmRt01", "WE", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Open01", "WE", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PleaPalmsUp01", "WE", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PleaPalmsUp01", "WE", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Us01", "WE", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowLf04", "LEFT", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowLf05", "LEFT", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));

        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowRt03", "RIGHT", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ThrowAwayRt01", "RIGHT", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_RollSmLf03", "LEFT", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_RollSmRt03", "RIGHT", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_AllTapSmLf01", "HERE", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_AllTapSmRt01", "HERE", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_SingleTapSmLf01", "HERE", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_SingleTapSmRt01", "HERE", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Here01", "HERE", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_HereBt01", "HERE", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_HereBt02", "HERE", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ScootSmLf01", "ASIDE", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ScootSmRt01", "ASIDE", "DEICTIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ChopBt01", "ASIDE", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ScratchHead01", "ASIDE", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PleaPalmsUp03", "ASIDE", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Something01", "ASIDE", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_AllTapSmLf01", "SURROUND", "DEICTIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_TroublingYou01", "SURROUND", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Us01", "SURROUND", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PleaPalmsUp01", "SURROUND", "DEICTIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ScootSmLf01", "NEGATION", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ScootSmRt01", "NEGATION", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_DiscardMedLf01", "NEGATION", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_AllTapSmLf01", "NEGATION", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_AllTapSmRt01", "NEGATION", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PalmDownRt01", "NEGATION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PalmDownLf01", "NEGATION", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_NegateRt01", "NEGATION", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PointUpRt01", "INTENSIFIER_POSITIVE", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_HereBt01", "INTENSIFIER_POSITIVE", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ChopBt01", "INTENSIFIER_POSITIVE", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowLf04", "INTENSIFIER_POSITIVE", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_DiscardMedLf01", "INTENSIFIER_NEGATIVE", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PalmDownRt01", "INTENSIFIER_NEGATIVE", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ThrowAwayRt01", "INTENSIFIER_NEGATIVE", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_HeadShakeNo01", "INTENSIFIER_NEGATIVE", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_CenterSmLf01", "LOGICAL_OPPOSITION", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Interject01", "LOGICAL_OPPOSITION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PalmDownRt01", "LOGICAL_OPPOSITION", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PleaPalmsUp02", "LOGICAL_OPPOSITION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_LookAround01", "LOGICAL_ADDITION", "METAPHORIC", "", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_HereBt02", "LOGICAL_ADDITION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ChopBt01", "LOGICAL_ADDITION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_DblBeatLowBt01", "LOGICAL_ADDITION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_EmptyMedBt02", "QUESTION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_EmptyMedBt01", "QUESTION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_EmptyMedLf01", "QUESTION", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ShrugSm01", "QUESTION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ShrugSm02", "QUESTION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ShrugSm03", "QUESTION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PleaPalmsUp02", "QUESTION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_WhatSmall", "QUESTION", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowBt04", "QUESTION", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ShrugSm03", "UNCERTAINTY", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ScratchHead01", "UNCERTAINTY", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_WhatMedium", "UNCERTAINTY", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowLf05", "INDIFFERENCE", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ShrugLg01", "INDIFFERENCE", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowBt01", "INDIFFERENCE", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatHandMidBt01", "INCLUSIVITY", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_HereBt01", "INCLUSIVITY", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_TroublingYou01", "INCLUSIVITY", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_RollSmBt01", "INCLUSIVITY", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PleaPalmsUp02", "QUANTITY_ALL", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_WhatMedium", "QUANTITY_ALL", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Something01", "QUANTITY_ALL", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_EmptyMedBt01", "QUANTITY_ALL", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PleaPalmsUp02", "QUANTITY_APPROXIMATION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_WhatMedium", "QUANTITY_APPROXIMATION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Something01", "QUANTITY_APPROXIMATION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_EmptyMedBt01", "QUANTITY_APPROXIMATION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_DustOffLeg01", "EMPTY", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_HeadShakeNo01", "EMPTY", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PalmDownLf01", "EMPTY", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PalmDownRt01", "EMPTY", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_FrontMedLf01", "APPROXIMATION", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatHandMidBt01", "APPROXIMATION", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ScratchFace01", "APPROXIMATION", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowRt01", "APPROXIMATION", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_IndicateSmRt01", "CONTRAST", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PleaPalmsUp02", "CONTRAST", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Contrast01", "CONTRAST", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ContrastBt01", "CONTRAST", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));

        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PleaPalmsUp02", "COMPARATIVE", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Contrast01", "COMPARATIVE", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ContrastBt01", "COMPARATIVE", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_Open01", "COMPARATIVE_POSITIVE", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_PalmDownLf01", "COMPARATIVE _NEGATIVE", "METAPHORIC", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_WhatMedium", "COMPARATIVE_BIGGER", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_HereBt01", "COMPARATIVE_SMALLER", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_SingleTapSmRt01", "COMPARATIVE_SMALLER", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_DeepBreath01", "STOP", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_InterjectBt01", "STOP", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_LeanForward01", "STOP", "METAPHORIC", "BOTH_HANDS", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_AllTapSmLf01", "OBLIGATION", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_ThrowAwayRt01", "OBLIGATION", "METAPHORIC", "RIGHT_HAND", "", "IdleSittingUpright01"));


        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_RollSmBt03", "GREETING", "EMBLEM", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_WhatSmall", "GREETING", "EMBLEM", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_WhatSmall", "GREETING", "EMBLEM", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_You02", "GREETING", "EMBLEM", "RIGHT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_EmptySmBt01", "RHYTHM", "BEAT", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowBt01", "RHYTHM", "BEAT", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowBt02", "RHYTHM", "BEAT", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowBt03", "RHYTHM", "BEAT", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowBt04", "RHYTHM", "BEAT", "BOTH_HANDS", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowLf01", "RHYTHM", "BEAT", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowLf02", "RHYTHM", "BEAT", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowLf03", "RHYTHM", "BEAT", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowLf04", "RHYTHM", "BEAT", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowLf05", "RHYTHM", "BEAT", "LEFT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowRt01", "RHYTHM", "BEAT", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowRt02", "RHYTHM", "BEAT", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowRt03", "RHYTHM", "BEAT", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowRt04", "RHYTHM", "BEAT", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowRt05", "RHYTHM", "BEAT", "RIGHT_HAND", "", "IdleSittingUpright01"));
        //gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_BeatLowRt06", "RHYTHM", "BEAT", "RIGHT_HAND", "", "IdleSittingUpright01"));

        gestureMaps.Add(new SmartbodyGestureMap("IdleSittingUpright01_CoughMedLf01", "COUGH", "", "", "", "IdleSittingUpright01"));
    }

}
