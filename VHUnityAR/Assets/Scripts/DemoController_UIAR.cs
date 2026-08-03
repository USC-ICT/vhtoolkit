using System.Collections.Generic;
using Ride.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the UI for the demo (AR): input field, scrollable transcript, ASR button, and canvas camera binding.
/// </summary>
public class DemoController_UIAR : RideBaseMinimal, IDemoControllerUI
{
    [Header("Controller")]
    [SerializeField] private DemoControllerBase m_controller;  // works with DemoControllerAR or Desktop via base

    [Header("UI")]
    [SerializeField] private TMP_InputField m_inputField;
    [SerializeField] private ScrollRect m_responseScroll;
    [SerializeField] private Image m_asrButton;
    [SerializeField] private List<Canvas> m_canvases = new();

    private GameObject UiRoot
    {
        get
        {
            if (m_canvases.Count == 0 || !m_canvases[0])
                return null;

            Transform current = m_canvases[0].transform;
            while (current != null)
            {
                if (current.name == "UI")
                    return current.gameObject;

                current = current.parent;
            }

            return null;
        }
    }

    public string InputFieldText
    {
        get => m_inputField ? m_inputField.text : string.Empty;
        set { if (m_inputField) m_inputField.text = value; }
    }

    public bool IsInputFieldFocused => m_inputField && m_inputField.isFocused;

    /// <summary>
    /// AR-only: bind world-space canvases to CenterEyeAnchor camera (fallback to Camera.main).
    /// </summary>
    public void InitializeCanvasCamera()
    {
        var eyeAnchor = GameObject.Find("CenterEyeAnchor");
        Camera cam = eyeAnchor ? eyeAnchor.GetComponent<Camera>() : Camera.main;
        if (!cam)
            return;

        foreach (var canvas in m_canvases)
            canvas.worldCamera = cam;
    }

    public void ShowPlacementUI()
    {
        if (UiRoot)
            UiRoot.SetActive(true);
    }

    public void HidePlacementUI()
    {
        if (UiRoot)
            UiRoot.SetActive(false);
    }

    /// <summary>
    /// Sets the background color of the ASR (speech recognition) button.
    /// </summary>
    public void SetAsrButtonColor(Color color)
    {
        if (!m_asrButton)
            return;
        m_asrButton.color = color;
    }

    public void SetAsrButtonText(string text)
    {
        if (!m_asrButton)
            return;
        m_asrButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    /// <summary>
    /// Not used in AR; needed per the IDemoControllerUI.
    /// </summary>
    /// <param name="color">Color to apply to the next character button.</param>
    public void SetNextCharacterButtonColor(Color color)
    {

    }

    /// <summary>
    /// Submits the text from the input field, updates the UI, and sends it to the LLM via the controller.
    /// </summary>
    public void SubmitInputTextField()
    {
        if (string.IsNullOrEmpty(InputFieldText) || m_controller == null)
            return;

        PopulateResponseUI("You", InputFieldText);
        m_controller.AskNLPQuestion(InputFieldText);
        InputFieldText = string.Empty;
    }

    /// <summary>
    /// Adds a formatted user or VH response to the UI and updates the scroll view.
    /// </summary>
    public void PopulateResponseUI(string writer, string response)
    {
        string line = writer == "You"
            ? $"User: {response}\n\n"
            : $"<color=yellow>VH: {response}</color>\n\n";

        UpdateResponseScroll(line);
    }

    /// <summary>
    /// Updates the response scroll view with new text and scrolls to the bottom.
    /// </summary>
    public void UpdateResponseScroll(string text)
    {
        if (!m_responseScroll)
        {
            Debug.LogWarning("UpdateResponseScroll: ScrollRect reference missing.");
            return;
        }

        var label = m_responseScroll.GetComponentInChildren<TextMeshProUGUI>();
        if (!label)
        {
            Debug.LogWarning("UpdateResponseScroll: Could not find TextMeshProUGUI under ScrollRect.");
            return;
        }

        label.text += text;

        // Ensure scroll view updates correctly.
        Canvas.ForceUpdateCanvases();
        m_responseScroll.verticalNormalizedPosition = 0f;
    }
}
