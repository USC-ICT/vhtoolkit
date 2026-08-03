using UnityEngine;
using VHAssets;

public class AssignCharacterControllerOnAwake : MonoBehaviour
{
    void Awake()
    {
        MecanimManager mecanimManager;
        BMLEventHandler bmlEventHandler;

        mecanimManager = GameObject.FindAnyObjectByType<MecanimManager>();
        if (mecanimManager != null)
        {
            bmlEventHandler = gameObject.GetComponent<BMLEventHandler>();
            if (bmlEventHandler != null)
                bmlEventHandler.m_CharacterController = mecanimManager;
            else
                Debug.LogError("[AssignCharacterControllerOnAwake] bmlEventHandler is null");
        }
        else
            Debug.LogError("[AssignCharacterControllerOnAwake] mecanimManager is null");
    }
}
