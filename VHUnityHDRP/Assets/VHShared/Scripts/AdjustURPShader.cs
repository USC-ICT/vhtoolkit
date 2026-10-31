using UnityEngine;

public class AdjustURPShader : MonoBehaviour
{
    public void InitializeLoadedAsset()
    {
        FixShaders();
    }

    /// <summary>
    /// Rebinds bundle-loaded URP/Lit materials to the player's local shader reference.
    /// Assigning <see cref="Material.shader"/> resets the material's render queue to the
    /// shader default, so the raw override is restored afterward to preserve custom queues
    /// as well as -1 (use the shader default).
    /// https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Material-renderQueue.html
    /// </summary>
    private void FixShaders()
    {
        var replacementShader = Shader.Find("Universal Render Pipeline/Lit");
        if (replacementShader == null)
            return;

        var skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(true);
        foreach (var renderer in skinnedRenderers)
            foreach (var mat in renderer.sharedMaterials)
                if (mat != null && mat.shader != null && mat.shader.name == "Universal Render Pipeline/Lit")
                {
                    int renderQueueOverride = mat.rawRenderQueue;
                    mat.shader = replacementShader;
                    mat.renderQueue = renderQueueOverride;
                }

        var meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
        foreach (var renderer in meshRenderers)
            foreach (var mat in renderer.sharedMaterials)
                if (mat != null && mat.shader != null && mat.shader.name == "Universal Render Pipeline/Lit")
                {
                    int renderQueueOverride = mat.rawRenderQueue;
                    mat.shader = replacementShader;
                    mat.renderQueue = renderQueueOverride;
                }
    }
}
