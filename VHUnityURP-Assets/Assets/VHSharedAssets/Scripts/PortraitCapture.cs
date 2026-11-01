using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Ride;
using VHAssets;

namespace VH
{
    /// <summary>
    /// Captures head-shot portrait PNGs of every character in a lineup scene, for the study
    /// builder's character picker and other user interface use.
    ///
    /// HOW TO USE (Play mode):
    ///   1. Open the lineup scene and press Play. This script bootstraps itself in any scene
    ///      whose name contains "Lineup", so no scene edit is needed.
    ///   2. Set the Game view to a high fixed resolution (e.g. 1920x1080) for crisp output.
    ///   3. Press F9. For each character in DebugMenu_Animations' list, the script hides the
    ///      other characters, walks the lineup to bring this one into the viewing spot, frames
    ///      the face, applies the smile, waits for the image to settle, captures, restores
    ///      everything, and writes {project}/Portraits/{character}.png .
    ///   4. Copy the PNGs into the study builder's portrait folder, renamed to the builder's
    ///      character ids. Files are named after the Animator, which is not the same thing.
    ///
    /// The script is render-pipeline agnostic and is kept identical across the URP and HDRP
    /// asset projects, so portraits can be taken in whichever pipeline looks best for a given
    /// character and combined afterwards.
    ///
    /// WHY LINEUP NAVIGATION rather than teleporting each character to a fixed mark: the
    /// lineup light is fixed in the world and characters are meant to move into it, so
    /// teleporting bypasses the scene's lighting design and most shots come out dark. Walking
    /// the lineup also guarantees the file name and the framing come from the same Animator,
    /// which is what keeps portraits attached to the right identity. The debug menu's manual
    /// camera controls are deliberately left untouched.
    ///
    /// WHY PLAY MODE rather than an offline Editor capture: the idle pose, post-processing and
    /// the facial expression are all runtime behaviour. An Editor-offline path would render
    /// unposed characters without post-processing.
    ///
    /// SMILE: the two character families reach the expression differently, so both are covered.
    /// Character Creator characters have their expression blendshapes driven directly, by name,
    /// because their animator declares a happy parameter with no animation behind it. Everyone
    /// else is asked to play the expression through their own facial animator. See ApplySmile.
    /// </summary>
    public class PortraitCapture : MonoBehaviour
    {
        [Header("Trigger")]
        public KeyCode m_captureKey = KeyCode.F9;
        [Tooltip("Start capturing automatically a few seconds after Play (for unattended runs).")]
        public bool m_autoCapture = false;
        public float m_autoCaptureDelaySeconds = 5f;

        [Header("Output")]
        [Tooltip("Folder created next to Assets/. PNGs are named after each character's Animator GameObject.")]
        public string m_outputFolder = "Portraits";
        [Tooltip("Output PNG is square, this many pixels per side.")]
        public int m_outputSize = 512;

        [Header("Stage")]
        [Tooltip("Hide all other characters while capturing one (clean background, no occlusion).")]
        public bool m_isolateCharacters = true;
        [Tooltip("Scene objects hidden for the duration of the capture, by name. The staging set is " +
                 "built for full-body viewing and intrudes on a head-and-shoulders crop.")]
        public List<string> m_hideDuringCapture = new List<string> { "Stage" };

        [Header("Head framing")]
        [Tooltip("Camera distance in front of the face, in meters. Raise it to leave more space " +
                 "around the face.")]
        public float m_headDistance = 0.62f;
        [Tooltip("How far below the eye line the camera aims, in meters. This is what lifts the " +
                 "eyes above the middle of the frame, so the face is not centred like a mugshot. " +
                 "Raise it to move faces up in frame.")]
        public float m_aimBelowEyeLine = 0.032f;
        [Tooltip("Camera height offset relative to the aim point, in meters. 0 keeps the camera level.")]
        public float m_headHeightOffset = 0f;
        [Tooltip("Extra aim offset above the aim point, in meters (negative aims lower). Tilts the " +
                 "camera, so prefer the offset above for repositioning the face.")]
        public float m_lookAtHeightOffset = 0f;
        [Tooltip("Where the eye line sits between the head bone and the top of the head, used only " +
                 "for rigs without eye bones or hips. 0 = at the head bone, 1 = at the crown.")]
        [Range(0f, 1f)] public float m_eyeLineFraction = 0.5f;
        [Tooltip("For rigs with no eye bones: how far the eye line sits above the head bone, as a " +
                 "fraction of the hips-to-head span. Scales with body size and ignores headwear. " +
                 "This estimates where the eyes actually are; use the aim offset above to change " +
                 "how the face sits in frame.")]
        [Range(0f, 0.5f)] public float m_eyeLineHeadRatio = 0.14f;

        [Header("Framing (2D crop)")]
        [Tooltip("1 = full Game-view height as the square crop; smaller zooms in further.")]
        [Range(0.3f, 1f)] public float m_cropZoom = 0.75f;
        [Tooltip("Vertical crop offset as a fraction of view height; positive moves the crop up. " +
                 "0 centres the crop on the aim point, which is already the eye line.")]
        [Range(-0.3f, 0.3f)] public float m_cropOffsetY = 0f;

        [Header("Timing")]
        [Tooltip("Seconds to wait after the lineup slide settles (cloth/hair/idle).")]
        public float m_settleSeconds = 1.0f;
        [Tooltip("Seconds to wait AFTER the camera is placed, so temporal effects (anti-aliasing, auto-exposure, motion blur history) converge — too short gives blurry or dark captures.")]
        public float m_cameraSettleSeconds = 1.5f;

        [Header("Cut-out geometry")]
        [Tooltip("Some characters carry their hair, eyelashes and other cut-out geometry on a " +
                 "material set to transparent blending with depth writing off. Overlapping hair " +
                 "cards then blend with whatever is behind them instead of reading as solid hair, " +
                 "and the cards pick up a broad specular sheen. This switches those materials to " +
                 "alpha clipping for the capture.")]
        public bool m_repairCutoutMaterials = true;
        [Tooltip("Material name fragments treated as cut-out geometry.")]
        public List<string> m_cutoutMaterialNames = new List<string> { "_opacity" };
        [Tooltip("Alpha below which a cut-out pixel is discarded.")]
        [Range(0f, 1f)] public float m_cutoutAlphaCutoff = 0.5f;
        [Tooltip("Smoothness applied to cut-out materials. Hair lit as a smooth surface reads as shiny.")]
        [Range(0f, 1f)] public float m_cutoutSmoothness = 0.05f;

        [Header("Smile")]
        [Tooltip("Give each character a pleasant expression for the portrait.")]
        public bool m_applySmile = true;
        [Tooltip("Articulation of the expression, scaling every blendshape weight and the value " +
                 "passed to the facial animator. Matches the strength the applications use for a " +
                 "slight smile.")]
        [Range(0f, 1f)] public float m_smileStrength = 0.6f;
        [Tooltip("The happy expression for Character Creator characters, as blendshape names and " +
                 "their relative weights. These rigs declare a happy animator parameter but have " +
                 "no animation behind it, so the shapes must be driven directly. Both Character " +
                 "Creator naming generations are listed; a character carries one set or the other, " +
                 "never both, so the absent names simply find no match.")]
        public List<SmileBlendShape> m_smileBlendShapes = new List<SmileBlendShape>
        {
            new SmileBlendShape("Mouth_SharpCorner_Pull_L", 0.6f),
            new SmileBlendShape("Mouth_SharpCorner_Pull_R", 0.6f),
            new SmileBlendShape("Mouth_Corner_Pull_L",      0.4f),
            new SmileBlendShape("Mouth_Corner_Pull_R",      0.4f),
            new SmileBlendShape("Mouth_Smile_Sharp_L",      0.6f),
            new SmileBlendShape("Mouth_Smile_Sharp_R",      0.6f),
            new SmileBlendShape("Mouth_Smile_L",            0.4f),
            new SmileBlendShape("Mouth_Smile_R",            0.4f),
        };
        [Tooltip("The character's own name for the happy expression, passed to its facial animator. " +
                 "Must be the enum-style name: a controller may also declare an underscore-less " +
                 "variant that nothing is wired to.")]
        public string m_smileExpression = "_112_happy";

        /// <summary>One blendshape's contribution to an expression.</summary>
        [Serializable]
        public class SmileBlendShape
        {
            [Tooltip("Blendshape name on the character's face mesh.")]
            public string Name;
            [Tooltip("Relative weight within the expression.")]
            [Range(0f, 1f)] public float Weight = 1f;

            public SmileBlendShape(string name, float weight)
            {
                Name = name;
                Weight = weight;
            }
        }

        bool m_busy;

        // Bootstraps without any scene modification: auto-attaches in lineup scenes only.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Bootstrap()
        {
            if (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Lineup"))
                return;
            if (FindAnyObjectByType<PortraitCapture>() != null)
                return;
            var go = new GameObject("PortraitCapture");
            go.AddComponent<PortraitCapture>();
            Debug.Log("[PortraitCapture] Ready — press F9 to capture portraits of the full lineup.");
        }

        IEnumerator Start()
        {
            if (!m_autoCapture)
                yield break;
            yield return new WaitForSeconds(m_autoCaptureDelaySeconds);
            if (!m_busy)
                StartCoroutine(CaptureAllCoroutine());
        }

        void Update()
        {
            if (Input.GetKeyDown(m_captureKey) && !m_busy)
                StartCoroutine(CaptureAllCoroutine());
        }

        IEnumerator CaptureAllCoroutine()
        {
            m_busy = true;

            var menu = FindAnyObjectByType<DebugMenu_Animations>();
            if (menu == null || menu.Characters == null || menu.Characters.Length == 0)
            {
                Debug.LogError("[PortraitCapture] No DebugMenu_Animations (or empty character list) in scene — aborting.");
                m_busy = false;
                yield break;
            }

            var camera = FindAnyObjectByType<Camera>(FindObjectsInactive.Exclude);
            if (camera == null)
            {
                Debug.LogError("[PortraitCapture] No camera in scene — aborting.");
                m_busy = false;
                yield break;
            }

            // Hide the debug menu chrome for clean captures; remember camera pose.
            var debugMenu = Systems.Get<DebugMenu>();
            if (debugMenu != null)
                debugMenu.ShowMenu(false);
            camera.transform.GetPositionAndRotation(out var camPos, out var camRot);

            // Staging geometry is sized for a full-body lineup and cuts across a head crop.
            var hidden = HideStagingObjects();

            string outDir = Path.Combine(Application.dataPath, "..", m_outputFolder);
            Directory.CreateDirectory(outDir);

            var all = menu.Characters.Where(a => a != null).ToArray();
            int captured = 0;

            // Rewind the lineup to the first character, then walk it forward. Navigation
            // (not teleportation) is what brings each character into the scene's fixed,
            // proven-lit viewing spot — exactly like manual use.
            int guard = menu.CharacterCount + 2;
            while (menu.CurrentCharacterIndex > 0 && guard-- > 0)
                menu.PreviousCharacter();
            yield return WaitForSettle(menu);

            for (int i = 0; i < menu.CharacterCount; i++)
            {
                // Identity-safe: file name AND framing both come from this same Animator.
                var animator = menu.CurrentCharacter;
                if (animator != null && animator.gameObject.activeSelf)
                {
                    var others = m_isolateCharacters
                        ? all.Where(a => a != animator && a.gameObject.activeSelf).ToList()
                        : new List<Animator>();
                    foreach (var o in others) o.gameObject.SetActive(false);

                    if (m_repairCutoutMaterials)
                    {
                        int repaired = RepairCutoutMaterials(animator);
                        if (repaired > 0)
                            Debug.Log($"[PortraitCapture] {animator.name}: switched {repaired} " +
                                      "cut-out material(s) to alpha clipping.");
                    }

                    if (m_applySmile)
                        ApplySmile(animator, true);

                    yield return WaitForSettle(menu);

                    // Frame the face from the character's head bone, wherever it stands.
                    FrameHead(camera, animator);

                    // Let temporal effects converge on the new view before shooting.
                    yield return new WaitForSeconds(m_cameraSettleSeconds);

                    yield return new WaitForEndOfFrame();
                    var path = Path.Combine(outDir, Sanitize(animator.name) + ".png");
                    CaptureToFile(path);
                    captured++;
                    Debug.Log($"[PortraitCapture] {captured}/{menu.CharacterCount}  {animator.name}  ->  {Path.GetFullPath(path)}");

                    if (m_applySmile)
                        ApplySmile(animator, false);
                    foreach (var o in others) o.gameObject.SetActive(true);
                }

                if (i < menu.CharacterCount - 1)
                {
                    menu.NextCharacter();
                    yield return WaitForSettle(menu);
                }
            }

            camera.transform.SetPositionAndRotation(camPos, camRot);
            foreach (var go in hidden) go.SetActive(true);
            if (debugMenu != null)
                debugMenu.ShowMenu(true);

            Debug.Log($"[PortraitCapture] Done — {captured} portraits in {Path.GetFullPath(outDir)}");
            m_busy = false;
        }

        /// <summary>
        /// Deactivates the staging objects named in <see cref="m_hideDuringCapture"/>.
        /// </summary>
        /// <returns>The objects that were deactivated, so the caller can restore them.</returns>
        /// <remarks>
        /// Only objects that are currently active are returned, so restoring never switches on
        /// something that was already off before the capture began.
        /// </remarks>
        List<GameObject> HideStagingObjects()
        {
            var hidden = new List<GameObject>();
            if (m_hideDuringCapture == null)
                return hidden;

#if UNITY_6000_5_OR_NEWER
            foreach (var t in FindObjectsByType<Transform>(FindObjectsInactive.Exclude))
#else
            foreach (var t in FindObjectsByType<Transform>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
#endif
            {
                if (t == null || !m_hideDuringCapture.Contains(t.name)) continue;
                if (!t.gameObject.activeSelf) continue;

                t.gameObject.SetActive(false);
                hidden.Add(t.gameObject);
                Debug.Log($"[PortraitCapture] Hid staging object '{t.name}' for the capture.");
            }

            return hidden;
        }

        IEnumerator WaitForSettle(DebugMenu_Animations menu)
        {
            float guard = 5f;
            while (!menu.IsSwapSettled && (guard -= Time.deltaTime) > 0f)
                yield return null;
            yield return new WaitForSeconds(m_settleSeconds);
        }

        /// <summary>
        /// Places the camera in front of the character's face, centred on the eye line.
        /// </summary>
        /// <remarks>
        /// The eye line is taken from the rig's eye bones where it has them, since that is exactly
        /// the height a portrait should be centred on. Rigs without eye bones derive it from the
        /// head bone and the top of the character's own bounds, because the head bone pivots near
        /// the base of the skull and the distance from there to the crown scales with head size.
        /// A fixed offset instead frames correctly only on the proportions it was tuned against,
        /// which leaves taller or smaller-headed characters sitting low with dead space above them.
        /// </remarks>
        void FrameHead(Camera camera, Animator animator)
        {
            string method;
            Vector3 eyeLine = ResolveEyeLine(animator, out method);

            // Aim below the eye line rather than at it. Aiming at the eyes puts them in the middle
            // of the frame; a portrait wants them above it, with the extra room under the chin.
            // Applied to every rig so both eye-line methods frame alike.
            Vector3 aim = eyeLine - Vector3.up * m_aimBelowEyeLine;

            var forward = animator.transform.forward;
            var pos = aim + forward * m_headDistance + Vector3.up * m_headHeightOffset;
            var lookAt = aim + Vector3.up * m_lookAtHeightOffset;
            camera.transform.SetPositionAndRotation(pos, Quaternion.LookRotation(lookAt - pos, Vector3.up));

            Debug.Log($"[PortraitCapture] {animator.name}: eye line from {method} " +
                      $"(y={eyeLine.y:F3}), aiming at y={aim.y:F3}.");
        }

        /// <summary>Returns the world position of the character's eye line.</summary>
        /// <param name="method">How the position was arrived at, for diagnostics.</param>
        Vector3 ResolveEyeLine(Animator animator, out string method)
        {
            if (animator.isHuman)
            {
                var leftEye = animator.GetBoneTransform(HumanBodyBones.LeftEye);
                var rightEye = animator.GetBoneTransform(HumanBodyBones.RightEye);
                if (leftEye != null && rightEye != null)
                {
                    method = "eye bones";
                    return (leftEye.position + rightEye.position) * 0.5f;
                }

                var head = animator.GetBoneTransform(HumanBodyBones.Head);
                if (head != null)
                {
                    // Rigs without eye bones are measured against the skeleton rather than the
                    // renderer bounds: the hips-to-head span scales with the character and, unlike
                    // the top of the bounds, is unaffected by a hat, helmet or tall hair.
                    var hips = animator.GetBoneTransform(HumanBodyBones.Hips);
                    if (hips != null)
                    {
                        method = "head bone + hips-to-head span";
                        float span = Mathf.Abs(head.position.y - hips.position.y);
                        return head.position + Vector3.up * (span * m_eyeLineHeadRatio);
                    }

                    method = "head bone + bounds crown";
                    float crown = GetRendererBounds(animator).max.y;
                    float headToCrown = Mathf.Max(0f, crown - head.position.y);
                    return head.position + Vector3.up * (headToCrown * m_eyeLineFraction);
                }
            }

            method = "renderer bounds";
            var bounds = GetRendererBounds(animator);
            return new Vector3(bounds.center.x, bounds.max.y - 0.15f, bounds.center.z);
        }

        static Bounds GetRendererBounds(Animator animator)
        {
            var renderers = animator.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
                return new Bounds(animator.transform.position + Vector3.up * 1.6f, Vector3.one * 0.3f);
            var b = renderers[0].bounds;
            foreach (var r in renderers)
                b.Encapsulate(r.bounds);
            return b;
        }

        void CaptureToFile(string path)
        {
            // Full Game-view capture (includes post-processing), then center-square
            // crop and downscale. Output quality scales with the Game view resolution.
            var shot = ScreenCapture.CaptureScreenshotAsTexture();
            try
            {
                int side = Mathf.RoundToInt(Mathf.Min(shot.width, shot.height) * m_cropZoom);
                int x = (shot.width - side) / 2;
                int y = (shot.height - side) / 2 + Mathf.RoundToInt(shot.height * m_cropOffsetY);
                x = Mathf.Clamp(x, 0, shot.width - side);
                y = Mathf.Clamp(y, 0, shot.height - side);

                var cropped = new Texture2D(side, side, TextureFormat.RGBA32, false);
                cropped.SetPixels(shot.GetPixels(x, y, side, side));
                cropped.Apply();

                var final = ScaleTexture(cropped, m_outputSize);
                File.WriteAllBytes(path, final.EncodeToPNG());

                Destroy(cropped);
                Destroy(final);
            }
            finally
            {
                Destroy(shot);
            }
        }

        static Texture2D ScaleTexture(Texture2D src, int size)
        {
            var rt = RenderTexture.GetTemporary(size, size, 0, RenderTextureFormat.ARGB32);
            var prev = RenderTexture.active;
            Graphics.Blit(src, rt);
            RenderTexture.active = rt;
            var dst = new Texture2D(size, size, TextureFormat.RGBA32, false);
            dst.ReadPixels(new Rect(0, 0, size, size), 0, 0);
            dst.Apply();
            RenderTexture.active = prev;
            RenderTexture.ReleaseTemporary(rt);
            return dst;
        }

        /// <summary>
        /// Switches a character's cut-out materials from transparent blending to alpha clipping.
        /// </summary>
        /// <remarks>
        /// Blended cut-out geometry with depth writing disabled cannot sort correctly: hair cards
        /// are drawn in object order rather than depth order, so overlapping cards blend with the
        /// background and with each other and lose their colour. Alpha clipping makes each pixel
        /// either present or absent, which restores both the colour and the depth sorting.
        /// Materials are edited through the renderer's instances, never the shared assets, because
        /// these are imported sub-assets of the model.
        /// </remarks>
        /// <returns>How many materials were switched.</returns>
        int RepairCutoutMaterials(Animator animator)
        {
            int repaired = 0;

            foreach (var renderer in animator.GetComponentsInChildren<Renderer>(true))
            {
                var materials = renderer.materials;
                bool touched = false;

                foreach (var material in materials)
                {
                    if (material == null)
                        continue;
                    if (!m_cutoutMaterialNames.Any(fragment => !string.IsNullOrEmpty(fragment) &&
                            material.name.IndexOf(fragment, StringComparison.OrdinalIgnoreCase) >= 0))
                        continue;
                    // Already opaque: nothing to correct.
                    if (material.HasProperty("_Surface") && material.GetFloat("_Surface") == 0f)
                        continue;

                    material.SetFloat("_Surface", 0f);
                    material.SetFloat("_Blend", 0f);
                    material.SetFloat("_AlphaClip", 1f);
                    material.SetFloat("_ZWrite", 1f);
                    material.SetFloat("_Cutoff", m_cutoutAlphaCutoff);
                    material.SetFloat("_Smoothness", m_cutoutSmoothness);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                    repaired++;
                    touched = true;
                }

                if (touched)
                    renderer.materials = materials;
            }

            return repaired;
        }

        /// <summary>
        /// Puts a pleasant expression on the character for the portrait.
        /// </summary>
        /// <remarks>
        /// The two character sources reach the expression by different routes, so the blendshapes
        /// are tried first: a Character Creator rig also declares a happy animator parameter, but
        /// with no animation behind it, so driving the parameter would report success and leave the
        /// face neutral. Characters whose expression is a real animation have none of these shapes,
        /// so they fall through to the parameter.
        /// </remarks>
        void ApplySmile(Animator animator, bool on)
        {
            int driven = DriveSmileBlendShapes(animator, on);
            if (driven > 0)
            {
                if (on)
                    Debug.Log($"[PortraitCapture] {animator.name}: smiling via {driven} blendshape(s).");
                return;
            }

            // Otherwise ask the character to play the expression itself. This routes through the
            // character's facial animator, which is the supported path for both character
            // families and the only one that reaches an expression built as character art.
            var character = animator.GetComponentInParent<MecanimCharacter>();
            if (character != null)
            {
                character.PlayViseme(m_smileExpression, on ? m_smileStrength : 0f);
                if (on)
                    Debug.Log($"[PortraitCapture] {animator.name}: smiling via " +
                              $"PlayViseme(\"{m_smileExpression}\").");
                return;
            }

            if (on)
                Debug.LogWarning($"[PortraitCapture] {animator.name}: no smile blendshape matched and " +
                                 "no MecanimCharacter to play the expression, so this portrait will " +
                                 "be neutral.");
        }

        /// <summary>
        /// Applies the configured expression shapes to every mesh under the character.
        /// </summary>
        /// <returns>How many shapes were driven, zero when the character has none of them.</returns>
        int DriveSmileBlendShapes(Animator animator, bool on)
        {
            int driven = 0;

            foreach (var smr in animator.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var mesh = smr != null ? smr.sharedMesh : null;
                if (mesh == null)
                    continue;

                for (int i = 0; i < mesh.blendShapeCount; i++)
                {
                    // Exporters may qualify a shape with its mesh name, so compare on the
                    // trailing segment rather than the whole string.
                    string shapeName = mesh.GetBlendShapeName(i);
                    int dot = shapeName.LastIndexOf('.');
                    if (dot >= 0)
                        shapeName = shapeName.Substring(dot + 1);

                    var entry = m_smileBlendShapes.FirstOrDefault(
                        e => e != null && string.Equals(e.Name, shapeName, StringComparison.OrdinalIgnoreCase));
                    if (entry == null)
                        continue;

                    smr.SetBlendShapeWeight(i, on ? entry.Weight * m_smileStrength * 100f : 0f);
                    driven++;
                }
            }

            return driven;
        }

        static string Sanitize(string s)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                s = s.Replace(c, '_');
            return s;
        }
    }
}
