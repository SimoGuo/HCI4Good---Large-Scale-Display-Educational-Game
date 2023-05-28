using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    private float width;
    private float height;
    private Vector2 screenPos;

    private Dictionary<int, Transform> fingerToCharacter = new Dictionary<int, Transform>();
    private Transform currentCharacter;
    private List<int> fingerIDs;
    // private Touch touch;
    private Ray camRay;
    [SerializeField] private Transform character;
    private void Awake() {
        width = Screen.width / 2f;
        height = Screen.height / 2f;
        fingerIDs = new List<int>();
    }

    void MoveCharacter(Touch touch, Transform currentCharacter) {
        // if (touch.phase == TouchPhase.Began) {
        //     
        // }
        if (touch.phase == TouchPhase.Moved) {
            currentCharacter.GetComponent<Controller>().Move(Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0)).GetPoint(30));
        }
    }

    private void Update() {

        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    fingerToCharacter.TryAdd(touch.fingerId, Instantiate(character, Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0)).GetPoint(30), Random.rotation));
                }
            }
        }
        
        foreach (KeyValuePair<int, Transform> i in fingerToCharacter) {
            foreach (Touch touch in Input.touches) {
                if (touch.fingerId == i.Key) {
                    MoveCharacter(touch, i.Value);
                }
            }
        }
        
        // foreach (KeyValuePair<int, Transform> i in fingerToCharacter) {
        //     if (Input.touchCount == 0) break;
        //     Touch touch = Input.touches[0];
        //     for (int j = 0; j < Input.touchCount; j++) {
        //         if (Input.touches[j].fingerId == i.Key) {
        //             touch = Input.touches[j];
        //             break;
        //         }
        //     }
        //     MoveCharacter(touch, i.Value);
        // }
    }

    void OnGUI() {
        GUI.skin.label.fontSize = (int)(Screen.width / 100.0f);
        string fingers = "";

        
        GUI.Label(new Rect(20, 20, width, height * 0.25f), fingers);
    }
}
