using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    private float width;
    private float height;
    private Vector2 screenPos;
    
    private void Awake() {
        width = Screen.width / 2f;
        height = Screen.height / 2f;
    }

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);

        GUI.Label(new Rect(20, 20, width, height * 0.25f),
            "x = " + screenPos.x.ToString("f2") +
            ", y = " + screenPos.y.ToString("f2"));
    }

    
    private void Update() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved) {
                
                screenPos.x = touch.position.x;
                screenPos.y = touch.position.y;

                transform.position = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0f)).GetPoint(20);

            }
        }
    }
}
