using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    private float width;
    private float height;
    private Vector2 screenPos;
    private Touch[] touches;
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
        touches = Input.touches;
    }

    private void Update() {
        touches = Input.touches;
    }

    private void FixedUpdate() {
        
        if (touches.Length == 0) {
            fingerToCharacter.Clear();
        }
        
        foreach (Touch touch in touches) {
            if (touch.phase == TouchPhase.Began) {
                camRay = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));

                if (Physics.Raycast(camRay, out RaycastHit hit) && hit.collider.CompareTag("Player")) {
                    fingerToCharacter.TryAdd(touch.fingerId, hit.transform.GetComponentInParent<Controller>().transform);
                    continue;
                }

                if (!fingerToCharacter.ContainsKey(touch.fingerId)) {
                    fingerToCharacter.Add(touch.fingerId, Instantiate(character, camRay.GetPoint((30)), Random.rotation));
                }
            }
        }
        
        
        foreach (KeyValuePair<int, Transform> i in fingerToCharacter) {
            foreach (Touch touch in touches) {
                if (touch.fingerId == i.Key) {
                    if (touch.phase == TouchPhase.Moved) {
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
