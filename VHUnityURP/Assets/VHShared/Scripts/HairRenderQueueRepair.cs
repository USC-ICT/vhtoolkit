using UnityEngine;

namespace Ride.Examples
{
    /// <summary>
    /// Restores the render queue each multi-pass hair material belonging to a loaded character
    /// needs for its surface type.
    /// </summary>
    /// <remarks>
    /// Hair on these characters is drawn by a pair of materials: a first pass that establishes
    /// the strands and a second pass that blends their soft edges over it. The order matters, so
    /// each pass declares its own render queue - the first in the alpha-tested range and the
    /// second in the transparent range.
    ///
    /// A material loaded from an asset bundle can arrive with both passes reporting the base
    /// opaque queue instead of the queues its shader declares. Both passes then draw together in
    /// the opaque range, the second pass no longer composites over the first, and hair, brows,
    /// beards and eyelashes render with hard aliased edges instead of soft ones. Character skin
    /// is unaffected, which makes the result look like a texture or shader problem rather than an
    /// ordering one.
    ///
    /// Both passes use the same shader, and that shader declares the transparent queue, so the
    /// shader's queue is only right for the second pass. The first pass is an opaque, alpha-clipped
    /// surface and belongs in the alpha-tested range: drawn in the transparent range, it no longer
    /// writes the depth that depth of field reads, and the hair is blurred as if it were the
    /// background behind it. The queue is therefore derived from each material's own surface type
    /// rather than from its shader.
    /// </remarks>
    public static class HairRenderQueueRepair
    {
        /// <summary>Marks a material as using the render queue declared by its shader.</summary>
        private const int UseShaderRenderQueue = -1;

        /// <summary>Shader name prefix identifying the multi-pass hair shaders.</summary>
        private const string HairShaderPrefix = "Shader Graphs/RL5_HairShader";

        /// <summary>Value of the shader's surface-type property that selects opaque rendering.</summary>
        private const float SurfaceOpaque = 0f;

        /// <summary>HDRP's queue for opaque, alpha-tested surfaces that do not receive decals.</summary>
        private const int OpaqueAlphaTestQueue = 2450;

        /// <summary>HDRP's queue for opaque, alpha-tested surfaces that receive decals.</summary>
        private const int OpaqueDecalAlphaTestQueue = 2475;

        /// <summary>
        /// Restores the surface-type render queues on the hair materials under the given object.
        /// </summary>
        /// <param name="root">Root of the loaded character. Nothing happens when null.</param>
        /// <returns>The number of materials whose render queue was restored.</returns>
        /// <remarks>
        /// Safe to call more than once for the same character; materials already in the right
        /// queue are skipped.
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
                    if (material == null || material.shader == null)
                        continue;

                    if (!material.shader.name.StartsWith(HairShaderPrefix))
                        continue;

                    bool opaque = material.HasProperty("_SurfaceType") && material.GetFloat("_SurfaceType") == SurfaceOpaque;
                    int expectedQueue = opaque ? GetOpaqueAlphaTestQueue(material) : material.shader.renderQueue;
                    if (material.renderQueue == expectedQueue)
                        continue;

                    material.renderQueue = opaque ? expectedQueue : UseShaderRenderQueue;
                    repaired++;
                }
            }

            if (repaired > 0)
                Debug.Log($"{nameof(HairRenderQueueRepair)}: restored render queues on {repaired} hair material(s) for '{root.name}'.");

            return repaired;
        }

        /// <summary>
        /// Returns the alpha-tested queue HDRP assigns an opaque material, which depends on
        /// whether the material receives decals.
        /// </summary>
        /// <param name="material">An opaque hair material.</param>
        /// <returns>The opaque alpha-tested render queue for the material.</returns>
        private static int GetOpaqueAlphaTestQueue(Material material)
        {
            bool receivesDecals = !material.HasProperty("_SupportDecals") || material.GetFloat("_SupportDecals") != 0f;
            return receivesDecals ? OpaqueDecalAlphaTestQueue : OpaqueAlphaTestQueue;
        }
    }
}
