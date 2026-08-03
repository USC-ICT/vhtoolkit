using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

public class TextureLink : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string url = "https://vhtoolkit.ict.usc.edu";

#if !UNITY_EDITOR && UNITY_WEBGL
    // Link to the .jslib function
    [DllImport("__Internal")]
    private static extern void OpenWindow(string url);
#endif

    public void OnPointerClick(PointerEventData eventData)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            OpenWindow(url);
#else
        Application.OpenURL(url);
#endif
    }
}
