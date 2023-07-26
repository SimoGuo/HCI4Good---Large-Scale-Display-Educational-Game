using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog Data", menuName = "Dialog/Dialog Data")]

public class DialogSO : ScriptableObject
{
    [SerializeField] private List<Dialog> dialogList;

    /*[HideInInspector]*/ public List<Dialog> dialogCacheList;

    public void InitDialog()
    {
        dialogCacheList.Clear();
        foreach (var item in dialogList)
        {
            dialogCacheList.Add(new Dialog() { isNeedReset = item.isNeedReset, interlocutor1 = item.interlocutor1, interlocutor2= item.interlocutor2, dialog = item.dialog });
        }
    }
}
