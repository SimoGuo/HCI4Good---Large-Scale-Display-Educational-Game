using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static event Action<Dialog> ShowDialogEvent;

    public static void CallShowDialogEvent(Dialog dialog)
    {
        ShowDialogEvent?.Invoke(dialog);
    }
}
