using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    private float width;
    private float height;
    private Dictionary<Touch, Transform> fingerToCharacter = new Dictionary<Touch, Transform>();
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
                
                if (!fingerToCharacter.ContainsKey(touch)) {
                    fingerToCharacter.Add(touch, Instantiate(character, camRay.GetPoint((30)), Random.rotation));
                }
            }

        }
        
        
        foreach (KeyValuePair<Touch, Transform> i in fingerToCharacter) {
            foreach (Touch touch in Input.touches) {
                if (touch.fingerId == i.Key.fingerId) {
                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                        camRay = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
                        i.Value.GetComponent<Controller>().Move(camRay.GetPoint(30));
                    }
                    break;
                }
            }
        }

    }

    void OnGUI() {
        GUI.skin.label.fontSize = (int)(Screen.width / 100.0f);
        string fingers = "";

        
        GUI.Label(new Rect(20, 20, width, height * 0.25f), fingerToCharacter.Count.ToString());
    }
}
