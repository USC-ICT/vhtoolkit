using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using VHAssets;

namespace Ride.Examples
{
    /// <summary>
    /// Editor utility that brings the demo characters onto the current gaze and eyelid setup.
    ///
    /// It does three things:
    /// <list type="bullet">
    ///   <item><description>
    ///   Places the gaze targets used to convey "thinking" beneath the camera, so that a glance
    ///   reads as a small deviation from looking at the viewer rather than a turn away from them.
    ///   </description></item>
    ///   <item><description>
    ///   Ensures every character has a thinking controller.
    ///   </description></item>
    ///   <item><description>
    ///   Replaces the legacy blink component with the current eyelid stack. The legacy component
    ///   drives blinks through the same facial animation parameters used for visemes, so the
    ///   eyelids are disturbed on every viseme while a character speaks.
    ///   </description></item>
    /// </list>
    ///
    /// The gaze target representing the viewer is left untouched; it already sits on the camera.
    /// </summary>
    public static class ArThinkingGazeTargetSetup
    {
        private const string LogPrefix = "[ArThinkingGazeTargetSetup]";
        private const string DemoObjectsPrefabPath = "Assets/Prefabs/DemoObjects.prefab";
        private const string CharactersRootName = "Characters";
        private const string CameraAnchorName = "CenterEyeAnchor";
        private const string GazeTargetsRootName = "GazeTargets";

        /// <summary>
        /// Lateral distance of a thinking target from the camera, in metres. Kept small so that a
        /// glance is a slight shift of the eyes and head rather than a whole-body reorientation.
        /// </summary>
        private const float LateralOffset = 0.20f;

        /// <summary>Vertical distance of a thinking target from the camera, in metres.</summary>
        private const float VerticalOffset = 0.20f;

        /// <summary>
        /// Distance behind the camera, in metres. Placing the targets behind the viewer keeps the
        /// glance directed past them into the distance rather than at a point in mid-air.
        /// </summary>
        private const float DepthOffset = -0.20f;

        private static readonly string[] EyelidParameters = { "045_blink_lf", "045_blink_rt" };

        /// <summary>The head controller's own default, and the only value this tool will replace.</summary>
        private const string DefaultNeckBoneName = "JtSkullA";

        /// <summary>Neck bone names to try, in order, when a character's configured name is absent.</summary>
        private static readonly string[] NeckBoneCandidates = { "JtSkullA", "JtHead", "CC_Base_Head", "JtNeck" };

        /// <summary>
        /// Describes one thinking target as a signed offset from the camera.
        /// </summary>
        private readonly struct TargetLayout
        {
            public readonly string Name;
            public readonly float LateralSign;
            public readonly float VerticalSign;

            public TargetLayout(string name, float lateralSign, float verticalSign)
            {
                Name = name;
                LateralSign = lateralSign;
                VerticalSign = verticalSign;
            }
        }

        private static readonly TargetLayout[] ThinkingTargets =
        {
            new TargetLayout("GazeTargetUpLeft",    -1f,  1f),
            new TargetLayout("GazeTargetUpRight",    1f,  1f),
            new TargetLayout("GazeTargetDownLeft",  -1f, -1f),
            new TargetLayout("GazeTargetDownRight",  1f, -1f),
        };

        [MenuItem("Ride/AR/Set Up Thinking Gaze Targets")]
        public static void SetUpThinkingGazeTargets()
        {
            ConfigureCharacters();
            int placedUnderCamera = PlaceThinkingTargetsUnderCamera();

            Debug.Log($"{LogPrefix} Done. Targets under '{CameraAnchorName}': {placedUnderCamera}.");
        }

        /// <summary>
        /// Applies the per-character changes in the demo prefab: removes character-parented
        /// thinking targets, ensures a thinking controller, and migrates the eyelid stack.
        /// </summary>
        private static void ConfigureCharacters()
        {
            GameObject prefabRoot = PrefabUtility.LoadPrefabContents(DemoObjectsPrefabPath);
            if (prefabRoot == null)
            {
                Debug.LogError($"{LogPrefix} Could not load prefab at '{DemoObjectsPrefabPath}'.");
                return;
            }

            try
            {
                Transform charactersRoot = FindDescendant(prefabRoot.transform, CharactersRootName);
                if (charactersRoot == null)
                {
                    Debug.LogError(
                        $"{LogPrefix} Could not find a '{CharactersRootName}' transform in " +
                        $"'{DemoObjectsPrefabPath}'.");
                    return;
                }

                int targetsRemoved = 0;
                int controllersAdded = 0;
                int blinkMigrated = 0;
                int eyelidAdded = 0;
                int tuningApplied = 0;

                foreach (MecanimCharacter character in charactersRoot.GetComponentsInChildren<MecanimCharacter>(true))
                {
                    if (!character.TryGetComponent(out ThinkingController _))
                    {
                        character.gameObject.AddComponent<ThinkingController>();
                        controllersAdded++;
                    }

                    foreach (TargetLayout layout in ThinkingTargets)
                    {
                        Transform existing = character.transform.Find(layout.Name);
                        if (existing == null)
                            continue;

                        Object.DestroyImmediate(existing.gameObject);
                        targetsRemoved++;
                    }

                    if (MigrateBlinkComponent(character.gameObject))
                        blinkMigrated++;

                    if (EnsureEyelidController(character.gameObject))
                        eyelidAdded++;

                    tuningApplied += ApplyCharacterTuning(character.gameObject);
                }

                Debug.Log(
                    $"{LogPrefix} Characters updated. Thinking targets removed: {targetsRemoved}. " +
                    $"ThinkingControllers added: {controllersAdded}. " +
                    $"Blink components migrated: {blinkMigrated}. " +
                    $"Eyelid controllers added: {eyelidAdded}. " +
                    $"Tuning values corrected: {tuningApplied}.");

                PrefabUtility.SaveAsPrefabAsset(prefabRoot, DemoObjectsPrefabPath);
            }
            finally
            {
                PrefabUtility.UnloadPrefabContents(prefabRoot);
            }
        }

        /// <summary>
        /// Replaces a legacy blink component with the current one, preserving its timing settings.
        /// </summary>
        /// <returns>True if a component was replaced.</returns>
        private static bool MigrateBlinkComponent(GameObject characterObject)
        {
            BlinkController existing = characterObject.GetComponent<BlinkController>();
            if (existing is BlinkValueProvider)
                return false;

            float minInterval = 4f;
            float maxInterval = 8f;
            float blinkLength = 0.2f;

            if (existing != null)
            {
                var previous = new SerializedObject(existing);
                minInterval = previous.FindProperty("m_MinBlinkInterval").floatValue;
                maxInterval = previous.FindProperty("m_MaxBlinkInterval").floatValue;
                blinkLength = previous.FindProperty("m_BlinkLength").floatValue;
                Object.DestroyImmediate(existing);
            }

            var provider = characterObject.AddComponent<BlinkValueProvider>();
            var so = new SerializedObject(provider);
            so.FindProperty("m_MinBlinkInterval").floatValue = minInterval;
            so.FindProperty("m_MaxBlinkInterval").floatValue = maxInterval;
            so.FindProperty("m_BlinkLength").floatValue = blinkLength;
            so.FindProperty("m_IsBlinkingOn").boolValue = true;
            so.FindProperty("m_BlinkMode").enumValueIndex = 1;
            so.FindProperty("m_BlinkBlendMax").floatValue = 1f;
            SetStringArray(so.FindProperty("m_EyeLidControllerParams"), EyelidParameters);
            SetStringArray(so.FindProperty("m_EyeLidBlendShapes"), EyelidParameters);
            so.ApplyModifiedPropertiesWithoutUndo();

            return true;
        }

        /// <summary>
        /// Adds the animator-driven eyelid controller when a character does not already have one.
        /// Its blink and gaze references are left unassigned; the component resolves them at runtime.
        /// </summary>
        /// <returns>True if a component was added.</returns>
        private static bool EnsureEyelidController(GameObject characterObject)
        {
            if (characterObject.GetComponent<EyelidController>() != null)
                return false;

            var eyelids = characterObject.AddComponent<EyelidController_Animator>();
            var so = new SerializedObject(eyelids);
            so.FindProperty("m_enableSoftEyes").boolValue = true;
            so.FindProperty("m_downwardLidAmount").floatValue = 0.35f;
            so.FindProperty("m_straightLidAmount").floatValue = 0.05f;
            so.FindProperty("m_upwardLidAmount").floatValue = 0f;
            so.FindProperty("m_enableBlink").boolValue = true;
            so.FindProperty("m_blinkStrength").floatValue = 1f;
            so.FindProperty("m_smoothingTime").floatValue = 0.02f;
            so.FindProperty("m_lidBlendMax").floatValue = 1f;
            SetStringArray(so.FindProperty("m_eyelidParams"), EyelidParameters);
            so.ApplyModifiedPropertiesWithoutUndo();

            return true;
        }

        /// <summary>
        /// Brings a character's behavior tuning onto the values shared by the other VHToolkit
        /// projects. Gaze weights are intentionally excluded; those are tuned per project.
        /// </summary>
        /// <returns>The number of values that were changed.</returns>
        private static int ApplyCharacterTuning(GameObject characterObject)
        {
            int changed = 0;

            changed += ApplyValue<HeadController>(characterObject, "m_NodAmplifier", 30f);
            changed += ApplyValue<ListeningController>(characterObject, "m_nodCooldown", 0.5f);
            changed += ApplyValue<FacialAnimationPlayer_Animator>(characterObject, "m_EasingEquation", 2f);
            changed += ApplyValue<BMLEventHandler>(characterObject, "m_TrimBMLTimingWhenParsing", 0f);

            // Thinking gaze weights are raised well above the shared defaults. On a fixed viewpoint
            // a character that relaxes its gaze while thinking still faces the viewer, but in a
            // head-mounted display the viewer may stand to one side, where the same relaxation
            // reads as the character turning away before it glances.
            // Eyes above the head weight so glances read lively; body well below so the torso
            // stays reserved. Per-channel speed values cannot express this: the gaze system uses
            // one shared position transition, so channel character comes from these weights.
            changed += ApplyValue<ThinkingController>(characterObject, "m_headGazeWeight", 0.45f);
            changed += ApplyValue<ThinkingController>(characterObject, "m_eyeGazeWeight", 0.35f);
            changed += ApplyValue<ThinkingController>(characterObject, "m_bodyGazeWeight", 0.07f);

            // Half the shared retarget speed. The glance targets sit close to the camera here, so
            // the default speed covers that short distance almost instantly and reads as a twitch.
            changed += ApplyValue<ThinkingController>(characterObject, "MinimumGazeSpeed", 35f);
            changed += ApplyValue<ThinkingController>(characterObject, "MaximumGazeSpeed", 45f);
            changed += ApplyValue<ThinkingController>(characterObject, "m_eyeGazeSpeed", 40f);
            changed += ApplyValue<ThinkingController>(characterObject, "m_bodyGazeSpeed", 40f);

            // Consistent glance tempo: every glance takes a similar time regardless of how far
            // apart the chosen targets are, with a small random range for variety. With
            // speed-based glances the duration varies with both the target pair (adjacent vs
            // diagonal) and the rolled speed, which reads as erratic.
            changed += ApplyValue<ThinkingController>(characterObject, "m_minGlanceDuration", 0.35f);
            changed += ApplyValue<ThinkingController>(characterObject, "m_maxGlanceDuration", 0.65f);

            changed += ApplyNeckBone(characterObject);

            return changed;
        }

        /// <summary>
        /// Points the head controller at a neck bone that exists on this character's rig. The
        /// component's default name suits one rig family; on others the lookup fails and nodding
        /// is lost.
        /// </summary>
        /// <returns>1 when the name was corrected, otherwise 0.</returns>
        private static int ApplyNeckBone(GameObject characterObject)
        {
            HeadController head = characterObject.GetComponent<HeadController>();
            if (head == null)
                return 0;

            var serialized = new SerializedObject(head);
            SerializedProperty property = serialized.FindProperty("m_NeckTransformName");
            if (property == null)
                return 0;

            // Only replace the component's default. Any other value was set deliberately and is
            // trusted, because a character whose art loads at runtime has none of its bones
            // present here - searching the hierarchy would find nothing, or worse, find a bone
            // belonging to a loading placeholder and overwrite a correct name with it.
            if (property.stringValue != DefaultNeckBoneName)
                return 0;

            // Leave it alone when the configured bone is present.
            if (FindDescendant(characterObject.transform, property.stringValue) != null)
                return 0;

            foreach (string candidate in NeckBoneCandidates)
            {
                if (FindDescendant(characterObject.transform, candidate) == null)
                    continue;

                Debug.Log(
                    $"{LogPrefix} '{characterObject.name}' has no bone named " +
                    $"'{property.stringValue}'; using '{candidate}'.");
                property.stringValue = candidate;
                serialized.ApplyModifiedPropertiesWithoutUndo();
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Sets a serialized value on a component when it is present and not already correct.
        /// </summary>
        /// <returns>1 when a value was changed, otherwise 0.</returns>
        private static int ApplyValue<T>(GameObject characterObject, string propertyPath, float value)
            where T : Component
        {
            T component = characterObject.GetComponent<T>();
            if (component == null)
                return 0;

            var serialized = new SerializedObject(component);
            SerializedProperty property = serialized.FindProperty(propertyPath);
            if (property == null)
            {
                Debug.LogWarning(
                    $"{LogPrefix} '{typeof(T).Name}' on '{characterObject.name}' has no " +
                    $"'{propertyPath}'; skipped.");
                return 0;
            }

            switch (property.propertyType)
            {
                case SerializedPropertyType.Float:
                    if (Mathf.Approximately(property.floatValue, value))
                        return 0;
                    property.floatValue = value;
                    break;

                case SerializedPropertyType.Integer:
                    if (property.intValue == (int)value)
                        return 0;
                    property.intValue = (int)value;
                    break;

                case SerializedPropertyType.Enum:
                    if (property.enumValueIndex == (int)value)
                        return 0;
                    property.enumValueIndex = (int)value;
                    break;

                case SerializedPropertyType.Boolean:
                    bool target = !Mathf.Approximately(value, 0f);
                    if (property.boolValue == target)
                        return 0;
                    property.boolValue = target;
                    break;

                default:
                    Debug.LogWarning(
                        $"{LogPrefix} '{propertyPath}' on '{typeof(T).Name}' is " +
                        $"{property.propertyType}; skipped.");
                    return 0;
            }

            serialized.ApplyModifiedPropertiesWithoutUndo();
            return 1;
        }

        private static void SetStringArray(SerializedProperty property, string[] values)
        {
            if (property == null)
                return;

            property.arraySize = values.Length;
            for (int i = 0; i < values.Length; i++)
                property.GetArrayElementAtIndex(i).stringValue = values[i];
        }

        /// <summary>
        /// Creates or repositions the thinking gaze targets beneath the camera's gaze target root
        /// in the active scene.
        /// </summary>
        /// <returns>The number of targets that were created or repositioned.</returns>
        private static int PlaceThinkingTargetsUnderCamera()
        {
            Scene scene = SceneManager.GetActiveScene();
            if (!scene.IsValid() || !scene.isLoaded)
            {
                Debug.LogWarning($"{LogPrefix} No loaded scene. Open the AR scene and run this again.");
                return 0;
            }

            Transform cameraAnchor = FindInScene(scene, CameraAnchorName);
            if (cameraAnchor == null)
            {
                Debug.LogError(
                    $"{LogPrefix} Could not find '{CameraAnchorName}' in scene '{scene.name}'. " +
                    $"Open the AR scene and run this again.");
                return 0;
            }

            Transform gazeTargetsRoot = cameraAnchor.Find(GazeTargetsRootName);
            if (gazeTargetsRoot == null)
            {
                var created = new GameObject(GazeTargetsRootName);
                gazeTargetsRoot = created.transform;
                gazeTargetsRoot.SetParent(cameraAnchor, false);
            }

            int placed = 0;
            foreach (TargetLayout layout in ThinkingTargets)
            {
                Transform target = gazeTargetsRoot.Find(layout.Name);
                if (target == null)
                {
                    var created = new GameObject(layout.Name);
                    target = created.transform;
                    target.SetParent(gazeTargetsRoot, false);
                }

                target.localPosition = new Vector3(
                    layout.LateralSign * LateralOffset,
                    layout.VerticalSign * VerticalOffset,
                    DepthOffset);
                target.localRotation = Quaternion.identity;
                target.localScale = Vector3.one;
                placed++;
            }

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            Debug.Log($"{LogPrefix} Saved scene '{scene.name}'.");

            return placed;
        }

        private static Transform FindInScene(Scene scene, string name)
        {
            foreach (GameObject root in scene.GetRootGameObjects())
            {
                Transform found = FindDescendant(root.transform, name);
                if (found != null)
                    return found;
            }

            return null;
        }

        private static Transform FindDescendant(Transform root, string name)
        {
            if (root.name == name)
                return root;

            foreach (Transform child in root)
            {
                Transform found = FindDescendant(child, name);
                if (found != null)
                    return found;
            }

            return null;
        }
    }
}
