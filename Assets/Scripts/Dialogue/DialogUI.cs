using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    public GameObject panel;
    public Text dialogText;

    private void OnEnable()
    {
        EventHandler.ShowDialogEvent += ShowDialog;
    }

    private void OnDisable()
    {
        EventHandler.ShowDialogEvent -= ShowDialog;
    }

    private void ShowDialog(string dialog)
    {
        if (dialog != string.Empty)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
        dialogText.text = dialog;
    }
}
