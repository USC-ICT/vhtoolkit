using UnityEngine;
using VHAssets;

/// <summary>
/// Owns the placement-scoped portions of the AR experience. The demo controller
/// can exist from startup, while the visible character, UI, and lighting content
/// stays hidden until the user places the experience in the real world.
/// </summary>
public class PlacedExperienceRoot : MonoBehaviour
{
    [Header("Placement Content")]
    [SerializeField] private Transform m_charactersRoot;
    [SerializeField] private GameObject m_uiRoot;
    [SerializeField] private GameObject m_lightingRoot;

    public Transform CharactersRoot => m_charactersRoot;
    public bool IsPlaced { get; private set; }

    private void Awake()
    {
        if (!m_charactersRoot)
            m_charactersRoot = transform.Find("Characters");
        if (!m_uiRoot)
        {
            var ui = transform.Find("UI");
            if (ui) m_uiRoot = ui.gameObject;
        }
        if (!m_lightingRoot)
        {
            var lighting = transform.Find("Lighting");
            if (lighting) m_lightingRoot = lighting.gameObject;
        }

        HidePlacementContent();
    }

    public void ShowPlacementContent()
    {
        IsPlaced = true;

        if (m_uiRoot) m_uiRoot.SetActive(true);
        if (m_lightingRoot) m_lightingRoot.SetActive(true);

        SetCharacterActiveState(true);
    }

    public void HidePlacementContent()
    {
        IsPlaced = false;

        SetCharacterActiveState(false);

        if (m_uiRoot) m_uiRoot.SetActive(false);
        if (m_lightingRoot) m_lightingRoot.SetActive(false);
    }

    private void SetCharacterActiveState(bool active)
    {
        if (!m_charactersRoot)
            return;

        foreach (var character in m_charactersRoot.GetComponentsInChildren<MecanimCharacter>(true))
        {
            if (character)
                character.gameObject.SetActive(active);
        }
    }
}
