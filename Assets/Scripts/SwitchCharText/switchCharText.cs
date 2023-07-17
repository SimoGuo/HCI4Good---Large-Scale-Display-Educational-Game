using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class switchCharText : MonoBehaviour
{
    [SerializeField]private RawImage image;
    [SerializeField]private TextMeshProUGUI text;

    private float switchInterval = 5f;

    private bool isImageVisible = true;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(switchImageText());
    }
    
    private IEnumerator switchImageText()
    {
        while (true)
        {
            if(isImageVisible)
            {
                image.gameObject.SetActive(true);
                text.gameObject.SetActive(false);
            }
            else
            {
                image.gameObject.SetActive(false);
                text.gameObject.SetActive(true);
            }

            isImageVisible = !isImageVisible;

            yield return new WaitForSeconds(switchInterval);
        }
    }
}
