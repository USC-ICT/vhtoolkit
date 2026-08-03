using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    public Button button_Ellie;
    public Button button_John;
    public Button button_Kevin;
    public Button button_Ariana;
    public Button button_Aaron;

    public void SetInteractable(bool interactable)
    {
        SetButtonInteractable(button_Ellie, interactable);
        SetButtonInteractable(button_John, interactable);
        SetButtonInteractable(button_Kevin, interactable);
        SetButtonInteractable(button_Ariana, interactable);
        SetButtonInteractable(button_Aaron, interactable);
    }

    private static void SetButtonInteractable(Button button, bool interactable)
    {
        if (button)
            button.interactable = interactable;
    }
}
