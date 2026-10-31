using UnityEngine;

namespace Ride.Examples
{
    /// <summary>
    /// Converts a loaded character's cut-out materials from transparent blending to alpha
    /// clipping, so hair reads as solid geometry.
    /// </summary>
    /// <remarks>
    /// Some characters place their hair, eyebrows and eyelashes on a single material shared with
    /// the body texture, using that texture's alpha channel to carve the shapes out. When such a
    /// material is authored as transparent, two things follow. Blended geometry is sorted per
    /// object rather than per triangle, so overlapping hair cards composite in an arbitrary order;
    /// and with depth writing disabled they also fail to occlude one another. The partially
    /// transparent pixels along every card edge then blend with whatever lies behind the head,
    /// which tints the hair towards the background colour and drains it of its own colour. The
    /// effect reads as a texture or shader fault rather than a blending one, and it is strongest
    /// against a saturated background.
    ///
    /// Alpha clipping resolves both problems: each pixel is either fully drawn or discarded, so
    /// there are no partially transparent edges to blend and ordinary depth testing sorts the
    /// cards. The cost is a harder silhouette, which is the accepted trade-off for cut-out hair.
    ///
    /// Only materials whose name marks them as cut-out geometry are altered, so genuinely
    /// transparent materials such as glass are left as authored.
    /// </remarks>
    public static class CutoutHairRepair
    {
        /// <summary>Name fragment identifying a material that carves its shapes out of an alpha channel.</summary>
        private const string CutoutMaterialMarker = "_opacity";

        /// <summary>Value of the shader's surface-type property that selects opaque rendering.</summary>
        private const float SurfaceOpaque = 0f;

        /// <summary>Alpha below which a clipped pixel is discarded.</summary>
        private const float AlphaCutoff = 0.5f;

        /// <summary>
        /// Switches the cut-out materials under the given object to alpha clipping.
        /// </summary>
        /// <param name="root">Root of the loaded character. Nothing happens when null.</param>
        /// <returns>The number of materials that were switched.</returns>
        /// <remarks>
        /// Safe to call more than once for the same character; materials already rendering as
        /// opaque are skipped.
        /// </remarks>
        public static int Apply(GameObject root)
        {
            if (root == null)
                return 0;

            int repaired = 0;

            // Shared materials are used deliberately - assigning through Renderer.materials would
            // clone every material and leave the character with per-instance copies.
            foreach (var renderer in root.GetComponentsInChildren<Renderer>(true))
            {
                foreach (var material in renderer.sharedMaterials)
                {
                    if (material == null)
                        continue;

                    if (material.name.IndexOf(CutoutMaterialMarker, System.StringComparison.OrdinalIgnoreCase) < 0)
                        continue;

                    if (!material.HasProperty("_Surface") || material.GetFloat("_Surface") == SurfaceOpaque)
                        continue;

                    material.SetFloat("_Surface", SurfaceOpaque);
                    material.SetFloat("_Blend", 0f);
                    material.SetFloat("_AlphaClip", 1f);
                    material.SetFloat("_ZWrite", 1f);
                    material.SetFloat("_Cutoff", AlphaCutoff);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                    repaired++;
                }
            }

            if (repaired > 0)
                Debug.Log($"{nameof(CutoutHairRepair)}: switched {repaired} cut-out material(s) to alpha clipping for '{root.name}'.");

            return repaired;
        }
    }
}
