using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUI : MonoBehaviour
{
    private GameObject panel;
    public TextMeshProUGUI dialogText;

    private void OnEnable()
    {
        panel = transform.Find("dlgPanel").gameObject;
        EventHandler.ShowDialogEvent += ShowDialog;
    }

    private void OnDisable()
    {
        EventHandler.ShowDialogEvent -= ShowDialog;
    }

    private void ShowDialog(Dialog _dialogStruct)
    {
        if (_dialogStruct.dialog != null)
        {
            panel.SetActive(true);
            GameObject.Find("Canvas").transform.Find("DialogPanel").gameObject.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
        dialogText.text = _dialogStruct.dialog;
    }
}
