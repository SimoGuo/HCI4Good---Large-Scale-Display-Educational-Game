using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    private float width;
    private float height;
    private Dictionary<int, Transform> fingerToCharacter = new Dictionary<int, Transform>();
    private Transform currentCharacter;
    private Ray camRay;
    
    [SerializeField] private Transform character;
    private void Awake() {
        width = Screen.width / 2f;
        height = Screen.height / 2f;
    }
    
    private void Update() {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                camRay = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
                if (!fingerToCharacter.ContainsKey(touch.fingerId)) {
                    fingerToCharacter.Add(touch.fingerId, Instantiate(character, camRay.GetPoint((30)), Random.rotation));
                }
            }

        }
        
        
        // foreach (KeyValuePair<int, Transform> i in fingerToCharacter) {
        //     foreach (Touch touch in Input.touches) {
        //         if (touch.fingerId == i.Key) {
        //             if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
        //                 camRay = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
        //                 i.Value.GetComponent<Controller>().Move(camRay.GetPoint(30));
        //             }
        //             break;
        //         }
        //     }
        // }

        foreach (Touch touch in Input.touches) {
            if (fingerToCharacter.TryGetValue(touch.fingerId, out currentCharacter) && touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                camRay = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
                currentCharacter.GetComponent<Controller>().Move(camRay.GetPoint(30));
            }
        }
        
        if (Input.touchCount > 0) {
            fingerToCharacter = fingerToCharacter.Where(pair => GetTouch(pair.Key).phase != TouchPhase.Ended)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
        

    }

    Touch GetTouch(int finger) {
        foreach (Touch touch in Input.touches) {
            if (touch.fingerId == finger) return touch;
        }

        return Input.GetTouch(0);
    }
    
    void OnGUI() {
        GUI.skin.label.fontSize = (int)(Screen.width / 100.0f);
        GUI.Label(new Rect(20, 20, width, height * 0.25f), fingerToCharacter.Count.ToString());
    }
}
