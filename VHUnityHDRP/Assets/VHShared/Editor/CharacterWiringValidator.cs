// Assets/VHShared/Editor/CharacterWiringValidator.cs
//
// Validates that bundle-loaded characters have the scene references they need, and can
// repair them by copying a working character's wiring.
//
// Why this exists: a prefab cannot reference a scene object, so a character's controller
// callback and its BMLEventHandler references can only live as prefab-instance overrides in
// the scene. Adding a character to the characters prefab therefore produces one that looks
// complete but has those fields empty, and duplicating an existing placeholder does not help
// because the references were never stored in the prefab to begin with.
//
// The failure is silent and misleading. Load completion is driven by the character's
// OnAssetLoaded event calling back into the controller - there is no code-side success
// handler - so an unwired event means the bundle downloads, the character appears, progress
// reaches 100%, and the load never completes. Nothing reports a missing reference.
//
// This runs automatically on entering Play mode so the problem names itself, and is also
// available from the Ride menu for an on-demand report or repair.

using System.Collections.Generic;
using System.Linq;
using Ride;
using Ride.Examples;
using UnityEditor;
using UnityEngine;
using VHAssets;

namespace Ride.Examples.EditorTools
{
    [InitializeOnLoad]
    public static class CharacterWiringValidator
    {
        private const string MenuValidate = "Ride/VH/Validate Character Wiring";
        private const string MenuFix = "Ride/VH/Fix Character Wiring";

        /// <summary>Describes one character's wiring, and what is missing from it.</summary>
        private class Report
        {
            public Transform character;
            public string catalogLabel;
            public bool hasCatalogAsset;
            public bool eventTargetMissing;
            public bool eventMethodMissing;
            public string eventArgument;
            public bool argumentMismatch;
            public bool controllerMissing;
            public bool cutsceneMissing;

            public bool IsBroken => hasCatalogAsset &&
                (eventTargetMissing || eventMethodMissing || argumentMismatch
                 || controllerMissing || cutsceneMissing);
        }

        static CharacterWiringValidator()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange change)
        {
            if (change != PlayModeStateChange.ExitingEditMode) return;

            var reports = Inspect();
            var broken = reports.Where(r => r.IsBroken).ToList();
            if (broken.Count == 0) return;

            foreach (var r in broken)
                Debug.LogError($"[CharacterWiring] '{r.character.name}' is not wired and will hang at " +
                               $"\"Finalizing...\" if selected: {Describe(r)}. " +
                               $"Run {MenuFix} or see CHARACTER-WIRING.md.");
        }

        [MenuItem(MenuValidate)]
        private static void Validate()
        {
            var reports = Inspect();
            if (reports.Count == 0)
            {
                Debug.LogWarning("[CharacterWiring] No characters found. Open the scene with the character controller first.");
                return;
            }

            var lines = new List<string> { "[CharacterWiring] report:" };
            foreach (var r in reports)
                lines.Add($"   {r.character.name.PadRight(34)}label={(string.IsNullOrEmpty(r.catalogLabel) ? "-" : r.catalogLabel).PadRight(30)}" +
                          (r.IsBroken ? "BROKEN: " + Describe(r) : "ok"));

            int broken = reports.Count(r => r.IsBroken);
            lines.Add($"   {broken} of {reports.Count} not wired.");

            if (broken > 0) Debug.LogError(string.Join("\n", lines));
            else Debug.Log(string.Join("\n", lines));
        }

        [MenuItem(MenuFix)]
        private static void Fix()
        {
            var reports = Inspect();
            var broken = reports.Where(r => r.IsBroken).ToList();
            if (broken.Count == 0)
            {
                Debug.Log("[CharacterWiring] Nothing to fix - every character is wired.");
                return;
            }

            var reference = reports.FirstOrDefault(r => r.hasCatalogAsset && !r.IsBroken);
            if (reference == null)
            {
                Debug.LogError("[CharacterWiring] No correctly wired character to copy from. " +
                               "Wire one by hand first - see CHARACTER-WIRING.md for the field values.");
                return;
            }

            if (!EditorUtility.DisplayDialog("Fix character wiring",
                    $"Copy the wiring from '{reference.character.name}' to {broken.Count} character(s)?\n\n" +
                    "This writes scene references only. Each character keeps its own name as the " +
                    "SelectCharacter argument.",
                    "Fix", "Cancel"))
                return;

            var refCatalog = reference.character.GetComponent<RideCatalogAsset>();
            var refCall = new SerializedObject(refCatalog)
                .FindProperty("m_onAssetLoaded.m_PersistentCalls.m_Calls").GetArrayElementAtIndex(0);

            var target = refCall.FindPropertyRelative("m_Target").objectReferenceValue;
            string typeName = refCall.FindPropertyRelative("m_TargetAssemblyTypeName").stringValue;
            string method = refCall.FindPropertyRelative("m_MethodName").stringValue;
            int mode = refCall.FindPropertyRelative("m_Mode").intValue;
            int callState = refCall.FindPropertyRelative("m_CallState").intValue;

            var refBml = reference.character.GetComponentInChildren<BMLEventHandler>(true);
            var refController = refBml != null ? refBml.m_CharacterController : null;
            var refCutscene = refBml != null ? refBml.m_CutscenePrefab : null;

            int fixedCount = 0;
            foreach (var r in broken)
            {
                var catalog = r.character.GetComponent<RideCatalogAsset>();
                if (catalog != null)
                {
                    var so = new SerializedObject(catalog);
                    var calls = so.FindProperty("m_onAssetLoaded.m_PersistentCalls.m_Calls");
                    if (calls != null && calls.isArray)
                    {
                        if (calls.arraySize == 0) calls.InsertArrayElementAtIndex(0);
                        var call = calls.GetArrayElementAtIndex(0);
                        call.FindPropertyRelative("m_Target").objectReferenceValue = target;
                        call.FindPropertyRelative("m_TargetAssemblyTypeName").stringValue = typeName;
                        call.FindPropertyRelative("m_MethodName").stringValue = method;
                        call.FindPropertyRelative("m_Mode").intValue = mode;
                        call.FindPropertyRelative("m_CallState").intValue = callState;
                        // The argument must be this character's own name - SelectCharacter
                        // matches on the GameObject name with an exact string compare.
                        call.FindPropertyRelative("m_Arguments.m_StringArgument").stringValue = r.character.name;
                        so.ApplyModifiedProperties();
                    }
                }

                var bml = r.character.GetComponentInChildren<BMLEventHandler>(true);
                if (bml != null)
                {
                    if (bml.m_CharacterController == null) bml.m_CharacterController = refController;
                    if (bml.m_CutscenePrefab == null) bml.m_CutscenePrefab = refCutscene;
                    EditorUtility.SetDirty(bml);
                }

                fixedCount++;
            }

            EditorSceneManagerSave();
            Debug.Log($"[CharacterWiring] Wired {fixedCount} character(s) from '{reference.character.name}'. Scene saved.");
        }

        private static void EditorSceneManagerSave()
        {
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
        }

        private static string Describe(Report r)
        {
            var missing = new List<string>();
            if (r.eventTargetMissing) missing.Add("OnAssetLoaded target");
            if (r.eventMethodMissing) missing.Add("OnAssetLoaded method");
            if (r.argumentMismatch) missing.Add($"OnAssetLoaded argument (is '{r.eventArgument}', expected '{r.character.name}')");
            if (r.controllerMissing) missing.Add("BMLEventHandler character controller");
            if (r.cutsceneMissing) missing.Add("BMLEventHandler cutscene prefab");
            return string.Join(", ", missing);
        }

        /// <summary>Inspects every character under the open scene's controller.</summary>
        private static List<Report> Inspect()
        {
            var reports = new List<Report>();

            var controller = Object.FindAnyObjectByType<DemoControllerBase>();
            if (controller == null || controller.CharactersParent == null) return reports;

            foreach (Transform category in controller.CharactersParent)
            {
                foreach (Transform child in category)
                {
                    if (child.GetComponent<MecanimCharacter>() == null) continue;

                    var report = new Report { character = child };
                    var catalog = child.GetComponent<RideCatalogAsset>();
                    report.hasCatalogAsset = catalog != null;

                    if (catalog != null)
                    {
                        var so = new SerializedObject(catalog);

                        var labels = so.FindProperty("labelsToLoad");
                        if (labels != null && labels.isArray && labels.arraySize > 0)
                            report.catalogLabel = labels.GetArrayElementAtIndex(0).stringValue;

                        var calls = so.FindProperty("m_onAssetLoaded.m_PersistentCalls.m_Calls");
                        if (calls == null || !calls.isArray || calls.arraySize == 0)
                        {
                            report.eventTargetMissing = true;
                            report.eventMethodMissing = true;
                        }
                        else
                        {
                            var call = calls.GetArrayElementAtIndex(0);
                            report.eventTargetMissing = call.FindPropertyRelative("m_Target").objectReferenceValue == null;
                            report.eventMethodMissing = string.IsNullOrEmpty(call.FindPropertyRelative("m_MethodName").stringValue);
                            report.eventArgument = call.FindPropertyRelative("m_Arguments.m_StringArgument").stringValue;
                            report.argumentMismatch = report.eventArgument != child.name;
                        }
                    }

                    var bml = child.GetComponentInChildren<BMLEventHandler>(true);
                    if (bml != null)
                    {
                        report.controllerMissing = bml.m_CharacterController == null;
                        report.cutsceneMissing = bml.m_CutscenePrefab == null;
                    }

                    reports.Add(report);
                }
            }

            return reports;
        }
    }
}
