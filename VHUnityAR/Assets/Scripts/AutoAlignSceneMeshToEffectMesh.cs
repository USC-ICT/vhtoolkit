using UnityEngine;

public class AutoAlignSceneMeshToEffectMesh : MonoBehaviour
{
    private const string ROOM_ROOT_PREFIX = "Room -";
    private const string SCENE_MESH_VOLUME_NAME = "MeshVolume";
    public Transform m_effectMeshRoom = null;
    public Transform m_meshVolume = null;
    public bool m_aligned = false;
    void Start()
    {
        StartCoroutine(WaitAndAlign());
    }

    System.Collections.IEnumerator WaitAndAlign()
    {
        while (m_effectMeshRoom == null)
        {
            m_effectMeshRoom = FindTransform(ROOM_ROOT_PREFIX, true);
            if (m_effectMeshRoom == null)
                yield return null;
        }

        while (m_meshVolume == null)
        {
            m_meshVolume = FindTransform(SCENE_MESH_VOLUME_NAME, false);
            if (m_meshVolume == null)
                yield return null;
        }

        if (!m_aligned && m_effectMeshRoom != null && m_meshVolume != null)
        {
            ApplyAlignment(m_effectMeshRoom, m_meshVolume);
            m_aligned = true;
        }
    }

    private static Transform FindTransform(string name, bool prefixMatch)
    {
        foreach (GameObject candidate in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            bool matches = prefixMatch
                ? candidate.name.StartsWith(name, System.StringComparison.Ordinal)
                : candidate.name == name;
            if (matches)
                return candidate.transform;
        }

        return null;
    }

    private void ApplyAlignment(Transform effectRoot, Transform roomRoot)
    {
        Quaternion wRot = effectRoot.rotation * Quaternion.Inverse(roomRoot.rotation);
        Vector3    wPos = effectRoot.position - (wRot * roomRoot.position);

        transform.SetPositionAndRotation(wPos, wRot);
        transform.localScale = Vector3.one;

        if (roomRoot.parent != transform)
            roomRoot.SetParent(transform, true);

        roomRoot.localScale = Vector3.one;
    }
}
