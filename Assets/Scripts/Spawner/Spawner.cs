using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    private float width;
    private float height;
    private Vector2 screenPos;
    private Transform currentCharacter;
    // private Touch touch;
    private Ray camRay;
    [SerializeField] private Transform character;
    private void Awake() {
        width = Screen.width / 2f;
        height = Screen.height / 2f;
    }

    void OnGUI() {
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);
    
        GUI.Label(new Rect(20, 20, width, height * 0.25f), Input.touchCount.ToString());
    }

    
    private void Update() {
        if (Input.touchCount > 0) {
            // touch = Input.GetTouch(0);
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    camRay = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(camRay, out hit)) {
                        Debug.Log("found char");
                        Controller bodyPart = hit.transform.gameObject.GetComponentInParent<Controller>();
                        if (bodyPart != null) {
                            currentCharacter = bodyPart.transform;
                        }
                    }
                    else {
                        Debug.Log("creating char");
                        currentCharacter = Instantiate(character, camRay.GetPoint(30), Random.rotation);
                    }
                }

                if (touch.phase == TouchPhase.Moved) {
                    camRay = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
                    // touch = Input.GetTouch(0);
                    if (currentCharacter != null) {
                        currentCharacter.GetComponent<Controller>().Move(camRay.GetPoint(30));
                    }
                }
            }
            
        }
    }
}
