using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoController : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI textButton;

    private bool isImageVisible = true;

    void Start()
    {
        // Hide the text part by default
        text.gameObject.SetActive(false);
    }

    // Method to be called when the button for this character is clicked
    public void ToggleImageAndText()
    {
        isImageVisible = !isImageVisible;
        image.gameObject.SetActive(isImageVisible);
        text.gameObject.SetActive(!isImageVisible);
        if(isImageVisible)
        {
            textButton.text = "SHOW";
        }
        else
        {
            textButton.text = "HIDE";
        }
    }
}
