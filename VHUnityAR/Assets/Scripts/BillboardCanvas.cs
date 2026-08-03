using UnityEngine;

public class BillboardCanvas : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) return;
        }

        // Calculate direction from the camera to the canvas
        Vector3 direction = transform.position - mainCamera.transform.position;
        direction.y = 0; // Ignore the vertical component to limit up/down rotation

        if (direction.sqrMagnitude > 0.001f) // Avoid zero-length direction vectors
        {
            // Correct the rotation to face the camera
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
