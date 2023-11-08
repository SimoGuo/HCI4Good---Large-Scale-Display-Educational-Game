using UnityEngine;

public class CancelButtonScript : MonoBehaviour
{
    public GameObject popupPanel;

    public void OnCancelButtonClick()
    {
        popupPanel.SetActive(false);
    }
}
