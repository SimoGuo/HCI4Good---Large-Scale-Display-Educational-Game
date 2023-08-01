using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public static DialogController Instance;

    [SerializeField] private DialogSO[] characterDialogs;

    public DialogSO characterDialog;

    private Stack<Dialog> characterDialogStack = new Stack<Dialog>();

    [SerializeField] private int characterNumber;

    public bool isTalking;

    public GameObject dlgCanvas;

    public GameObject imgTap;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        characterDialog = characterDialogs[characterNumber - 1];
        FillDialogStack();
        ShowDialog();
    }

    public void FillDialogStack()
    {
        characterDialog.InitDialog();
        for (int i = characterDialog.dialogCacheList.Count - 1; i >= 0; i--)
        {
            characterDialogStack.Push(characterDialog.dialogCacheList[i]);
        }
    }

    public void ShowDialog()
    {
        if (!isTalking)
        {
            if (characterDialogStack.TryPeek(out Dialog result))
            {
                if (result.isNeedReset)
                {
                    if (result.interlocutor2 == Interlocutor.Null)
                    {
                        imgTap.SetActive(true);
                    }
                    dlgCanvas.transform.Find("dlgPanel").gameObject.SetActive(false);
                    GameObject.Find("Canvas").transform.Find("DialogPanel").gameObject.SetActive(false);
                    result.isNeedReset = false;
                    EnterGameAnimation.Instance.InitPlayerAndEnemy((int)result.interlocutor1, (int)result.interlocutor2);
                    return;

                }
            }
            StartCoroutine(DialogRoutine(characterDialogStack));
        }
    }

    private IEnumerator DialogRoutine(Stack<Dialog> _data)
    {
        isTalking = true;

        if (_data.TryPop(out Dialog result))
        {
            EventHandler.CallShowDialogEvent(result);
            yield return null;
        }
        else
        {
            EventHandler.CallShowDialogEvent(new Dialog());
        }
        isTalking = false;
    }
}
