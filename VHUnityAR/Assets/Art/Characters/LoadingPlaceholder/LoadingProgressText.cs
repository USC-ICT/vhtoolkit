using UnityEngine;

namespace VH
{
    public sealed class LoadingProgressText : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshPro m_text;
        [SerializeField] private Transform m_fillQuad;

        [Range(0f, 1f)]
        [SerializeField] private float m_progress01 = 0f;

        private Vector3 m_fillBaseScale;
        private float m_fillBasePosX;
        private float m_fillBaseHalfWidth;


        private void Awake()
        {
            if (m_fillQuad != null)
            {
                m_fillBaseScale = m_fillQuad.localScale;
                m_fillBasePosX = m_fillQuad.localPosition.x;

                // Most common case: Quad is 1 unit wide, so scale.x == width in local units.
                // If your quad mesh is not 1 unit wide, see the "More robust" note below.
                m_fillBaseHalfWidth = m_fillBaseScale.x * 0.5f;
            }

            Apply(m_progress01);
        }

        public void SetProgress01(float progress01)
        {
            m_progress01 = Mathf.Clamp01(progress01);
            Apply(m_progress01);
        }

        // called via SendMessage from RideCatalogAsset during load
        public void UpdateLoadedAssetProgress(float progress01)
        {
            SetProgress01(progress01);
        }

        private void Apply(float progress01)
        {
            int pct = Mathf.RoundToInt(progress01 * 100f);

            SetText($"Loading... {pct}%");

            if (m_fillQuad == null)
                return;

            progress01 = Mathf.Clamp01(progress01);

            // Scale X to progress (full width = current width at 1.0)
            Vector3 s = m_fillBaseScale;
            s.x = m_fillBaseScale.x * progress01;
            m_fillQuad.localScale = s;

            // Shift so LEFT edge stays fixed at the original left edge
            float x = m_fillBasePosX - (m_fillBaseHalfWidth * (1f - progress01));
            Vector3 p = m_fillQuad.localPosition;
            p.x = x;
            m_fillQuad.localPosition = p;

            m_fillQuad.gameObject.SetActive(progress01 > 0.001f);
        }

        private void SetText(string s)
        {
            if (m_text != null)
                m_text.text = s;
        }
    }
}
