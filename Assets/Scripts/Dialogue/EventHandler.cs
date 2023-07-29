using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static event Action<string> ShowDialogEvent;

    public static void CallShowDialogEvent(string dialog)
    {
        ShowDialogEvent?.Invoke(dialog);
    }
}
