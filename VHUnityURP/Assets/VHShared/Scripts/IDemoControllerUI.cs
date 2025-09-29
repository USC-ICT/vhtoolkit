using UnityEngine;

public interface IDemoControllerUI
{
    string InputFieldText { get; set; }
    bool IsInputFieldFocused { get; }
    void SubmitInputTextField();
    void PopulateResponseUI(string writer, string response);
    void UpdateResponseScroll(string text);
    void SetAsrButtonColor(Color color);
    void InitializeCanvasCamera(); //AR only, desktop skips implementation
}
