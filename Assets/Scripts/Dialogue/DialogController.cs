/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public DialogSO[] characterDialogs;

    private DialogSO characterDialog;

    private Stack<string> characterDialogStack = new Stack<string>();

    public bool isTalking;

    private int characterNumber = 0;

    private void Awake()
    {
        characterDialog = characterDialogs[characterNumber];
        FillDialogStack();
    }

    public void FillDialogStack()
    {
        for (int i = characterDialog.dialogList.Count-1 ; i>=0 ; i--)
        {
            characterDialogStack.Push(characterDialog.dialogList[i]);
        }
    }

    public void ShowDialog()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogRoutine(characterDialogStack));
        }
    }

    private IEnumerator DialogRoutine(Stack<string> _data)
    {
        isTalking = true;

        if (_data.TryPop(out string result))
        {
            EventHandler.CallShowDialogEvent(result);
            yield return null;
        }
        else
        {
            EventHandler.CallShowDialogEvent(string.Empty);
            //FillDialogStack();
        }
        isTalking = false;
    }
}
*/
