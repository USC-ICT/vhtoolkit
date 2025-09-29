using UnityEngine;

public class AdjustURPShader : MonoBehaviour
{
    public void InitializeLoadedAsset()
    {
        FixShaders();
    }

    private void FixShaders()
    {
        var skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(true);
        foreach (var renderer in skinnedRenderers)
            foreach (var mat in renderer.sharedMaterials)
                if (mat != null && mat.shader != null && mat.shader.name == "Universal Render Pipeline/Lit")
                    mat.shader = Shader.Find("Universal Render Pipeline/Lit");

        var meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
        foreach (var renderer in meshRenderers)
            foreach (var mat in renderer.sharedMaterials)
                if (mat != null && mat.shader != null && mat.shader.name == "Universal Render Pipeline/Lit")
                    mat.shader = Shader.Find("Universal Render Pipeline/Lit");
    }
}
