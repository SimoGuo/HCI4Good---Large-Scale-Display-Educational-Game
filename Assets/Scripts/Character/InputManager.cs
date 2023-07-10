using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField] private Transform[] players;
    
    public List<TouchLocation> TouchLocations = new List<TouchLocation>();

    private void Start() {
        
    }

    private void FixedUpdate() {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                Debug.Log("began");
                Transform closestPlayer = GetClosestPlayer(touch);
                if (closestPlayer != null) {
                    
                    TouchLocations.Add(new TouchLocation(touch.fingerId, closestPlayer));
                }
            }
            if (touch.phase == TouchPhase.Moved) {
                Debug.Log("moved");
                Ray r = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(r, out RaycastHit hit)) {
                    TouchLocation thisTouch = TouchLocations.Find(i => i.fingerID == touch.fingerId);
                    thisTouch.player.GetComponent<PlayerTouchController>().target = hit.point;
                }
            }
            if (touch.phase == TouchPhase.Ended) {
                Debug.Log("ended");
                TouchLocation thisTouch = TouchLocations.Find(i => i.fingerID == touch.fingerId);
                thisTouch.player.GetComponent<PlayerTouchController>().needsTarget = true;
                TouchLocations.RemoveAt(TouchLocations.IndexOf(thisTouch));
            }
        }
    }

    Transform GetClosestPlayer(Touch touch) {
        Ray r = Camera.main.ScreenPointToRay(touch.position);
        for (int i = 0; i < players.Length; i++) {
            if (Physics.Raycast(r, out RaycastHit hit)) {
                if (Vector3.Distance(hit.point, players[i].position) <= 5 && players[i].GetComponent<PlayerTouchController>().needsTarget) {
                    players[i].GetComponent<PlayerTouchController>().needsTarget = false;
                    return players[i];
                }
            }
        }

        return null;
    }
}
