using UnityEngine;
using Ride;

namespace VH
{
    /// <summary>
    /// Controls the alpha of all <see cref="SkinnedMeshRenderer"/> components under a chosen root
    /// using a perceptually weighted curve. Intended for placeholder / fade-in assets using
    /// transparent materials (HDRP or URP).
    ///
    /// This component supports two common initialization paths:
    /// - Non-loadable objects: initializes in <see cref="Start"/>.
    /// - Loadable objects (e.g., driven by <see cref="RideCatalogAsset"/>): initializes when
    ///   <c>InitializeLoadedAsset()</c> is invoked via SendMessage after instantiation.
    ///
    /// While an asset is loading, <see cref="RideCatalogAsset"/> may call
    /// <c>UpdateLoadedAssetProgress(float)</c> via SendMessage. This component uses that
    /// normalized progress value [0..1] to drive alpha (fade in).
    /// </summary>
    public class SkinnedMeshAlphaController : MonoBehaviour
    {
        [Tooltip("Optional override root used to find all SkinnedMeshRenderers in this hierarchy (including inactive children). " +
                 "If null, the GameObject this component is attached to is used as the search root.")]
        [SerializeField] private GameObject m_rendererRootOverride;

        [Tooltip("Public alpha input in range [0..1]. This value is remapped before applying.")]
        [Range(0f, 1f)]
        [SerializeField] private float m_alpha01 = 1.0f;

        [SerializeField] private AnimationCurve m_alphaCurve = new AnimationCurve(
            new Keyframe(0.0f, 0.0f),
            new Keyframe(0.1f, 0.03f),
            new Keyframe(0.2f, 0.08f),
            new Keyframe(0.3f, 0.12f),
            new Keyframe(0.4f, 0.18f),
            new Keyframe(0.5f, 0.22f),
            new Keyframe(0.6f, 0.28f),
            new Keyframe(0.7f, 0.30f),
            new Keyframe(0.8f, 0.55f),
            new Keyframe(0.9f, 0.75f),
            new Keyframe(1.0f, 1.0f)
        );

        private static readonly int s_baseColorId = Shader.PropertyToID("_BaseColor");

        private SkinnedMeshRenderer[] m_smrs;
        private MaterialPropertyBlock m_mpb;


        private void Start()
        {
            var root = m_rendererRootOverride == null ? transform : m_rendererRootOverride.transform;
            m_smrs = root.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            m_mpb = new MaterialPropertyBlock();
        }

        /// <summary>
        /// Sets the alpha using a normalized [0..1] value.
        /// The value is perceptually remapped before being applied.
        /// </summary>
        public void SetAlpha01(float alpha01)
        {
            m_alpha01 = Mathf.Clamp01(alpha01);
            ApplyAlpha(m_alpha01);
        }

        // called via SendMessage from RideCatalogAsset during load
        public void UpdateLoadedAssetProgress(float progress01)
        {
            SetAlpha01(progress01);
        }

        private void ApplyAlpha(float alpha01)
        {
            if (m_smrs == null || m_smrs.Length == 0)
                return;

            float weightedAlpha = EvaluateAlpha(alpha01);

            foreach (var smr in m_smrs)
            {
                if (smr == null)
                    continue;

                smr.GetPropertyBlock(m_mpb);

                var baseColor = Color.white;
                var mat = smr.sharedMaterial;
                if (mat != null && mat.HasProperty(s_baseColorId))
                    baseColor = mat.GetColor(s_baseColorId);

                baseColor.a = weightedAlpha;

                m_mpb.SetColor(s_baseColorId, baseColor);
                smr.SetPropertyBlock(m_mpb);
            }
        }

        private float EvaluateAlpha(float alpha01)
        {
            alpha01 = Mathf.Clamp01(alpha01);

            if (m_alphaCurve == null || m_alphaCurve.length == 0)
                return alpha01;

            float a = m_alphaCurve.Evaluate(alpha01);
            return Mathf.Clamp01(a);
        }
    }
}
