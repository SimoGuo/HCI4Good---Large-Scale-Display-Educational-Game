using UnityEngine;
using UnityEngine.UI;

public class HidePopupMessage : MonoBehaviour
{
    public Button proceed;

    public GameObject popupMessage;

    void Start()
    {
        proceed.onClick.AddListener(HidePopupMessageOnClick);
    }

    void HidePopupMessageOnClick()
    {
        if (popupMessage != null)
        {
            popupMessage.SetActive(false);
        }
    }
}
