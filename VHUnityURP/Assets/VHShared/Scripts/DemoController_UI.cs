using Ride.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the UI for the desktop demo, including input fields, scrollable response display, and ASR button interaction.
/// </summary>
public class DemoController_UI : RideBaseMinimal, IDemoControllerUI
{
    [Header("Controller")]
    [SerializeField] private DemoControllerBase m_controller;

    [Header("UI")]
    [SerializeField] private TMP_InputField m_inputField;
    [SerializeField] private ScrollRect m_responseScroll;
    [SerializeField] private Image m_asrButton;

    public string InputFieldText
    {
        get => m_inputField ? m_inputField.text : string.Empty;
        set { if (m_inputField) m_inputField.text = value; }
    }

    public bool IsInputFieldFocused => m_inputField && m_inputField.isFocused;

    public void InitializeCanvasCamera() { /*Only implemented for AR*/ }

    /// <summary>
    /// Sets the background color of the ASR (speech recognition) button.
    /// </summary>
    /// <param name="color">Color to apply to the ASR button.</param>
    public void SetAsrButtonColor(Color color)
    {
        if (!m_asrButton)
        {
            Debug.LogWarning("SetAsrButtonColor: Missing ASR button Image.");
            return;
        }

        m_asrButton.color = color;

        var btn = m_asrButton.GetComponent<Button>();
        if (btn) btn.interactable = (color != Color.gray);
    }


    /// <summary>
    /// Submits the text from the input field, updates the UI, and sends it to the LLM via the controller.
    /// </summary>
    public void SubmitInputTextField()
    {
        if (string.IsNullOrEmpty(InputFieldText) || m_controller == null)
            return;

        PopulateResponseUI("You", InputFieldText);
        m_controller.AskLLMQuestion(InputFieldText);
        InputFieldText = string.Empty;
    }

    /// <summary>
    /// Adds a formatted user or VH response to the UI and updates the scroll view.
    /// </summary>
    /// <param name="writer">The entity that wrote the response ("You" or system agent).</param>
    /// <param name="response">The response text to display.</param>
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
    /// <param name="text">The text to append to the response view.</param>
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
            Debug.LogWarning("UpdateResponseScroll: TextMeshProUGUI not found under ScrollRect.");
            return;
        }

        label.text += text;

        Canvas.ForceUpdateCanvases();
        m_responseScroll.verticalNormalizedPosition = 0f;
    }
}
