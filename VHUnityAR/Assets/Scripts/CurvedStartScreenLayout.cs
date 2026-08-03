using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Lightweight wiring for the curved start screen prefab.
/// The curved canvas stack itself is authored in the prefab.
/// </summary>
public class CurvedStartScreenLayout : MonoBehaviour
{
    [SerializeField] private Canvas sourceCanvas;
    [SerializeField] private Button startButton;
    [SerializeField] private Transform curvedVisualRoot;
    [SerializeField] private bool flattenToWorldCanvas = true;

    public Canvas SourceCanvas => sourceCanvas;
    public Button StartButton => startButton;

    public void Configure(Camera worldCamera, UnityAction onStartPressed)
    {
        EnsureReferences();

        if (sourceCanvas)
        {
            sourceCanvas.renderMode = RenderMode.WorldSpace;
            sourceCanvas.worldCamera = worldCamera;
        }

        if (flattenToWorldCanvas)
            FlattenToWorldSpaceCanvas();

        if (startButton)
        {
            startButton.onClick.RemoveListener(onStartPressed);
            startButton.onClick.AddListener(onStartPressed);
        }
    }

    public void ClearButtonListener(UnityAction onStartPressed)
    {
        if (startButton)
            startButton.onClick.RemoveListener(onStartPressed);
    }

    private void EnsureReferences()
    {
        if (!sourceCanvas)
            sourceCanvas = GetComponentInChildren<Canvas>(true);

        if (!startButton && sourceCanvas)
            startButton = sourceCanvas.GetComponentInChildren<Button>(true);

        if (!curvedVisualRoot)
        {
            Transform curvedRoot = transform.Find("UI Cylinder");
            if (curvedRoot)
                curvedVisualRoot = curvedRoot;
        }
    }

    private void FlattenToWorldSpaceCanvas()
    {
        if (!sourceCanvas)
            return;

        Transform canvasTransform = sourceCanvas.transform;
        if (canvasTransform.parent == transform)
            return;

        Vector3 canvasScale = canvasTransform.localScale;
        canvasTransform.SetParent(transform, false);
        canvasTransform.localPosition = Vector3.zero;
        canvasTransform.localRotation = Quaternion.identity;
        canvasTransform.localScale = canvasScale;

        if (curvedVisualRoot)
            curvedVisualRoot.gameObject.SetActive(false);
    }
}
