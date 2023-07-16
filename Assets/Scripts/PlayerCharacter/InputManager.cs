using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using PlayerCharacter;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField] private Transform[] players;
    
    private List<TouchLocation> TouchLocations = new List<TouchLocation>();
    

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
                    thisTouch.player.GetComponent<Player>().target = hit.point;
                }
            }
            if (touch.phase == TouchPhase.Ended) {
                Debug.Log("ended");
                TouchLocation thisTouch = TouchLocations.Find(i => i.fingerID == touch.fingerId);
                thisTouch.player.GetComponent<Player>().needsTarget = true;
                TouchLocations.RemoveAt(TouchLocations.IndexOf(thisTouch));
            }
        }
    }

    Transform GetClosestPlayer(Touch touch) {
        Ray r = Camera.main.ScreenPointToRay(touch.position);
        for (int i = 0; i < players.Length; i++) {
            if (Physics.Raycast(r, out RaycastHit hit)) {
                if (Vector3.Distance(hit.point, players[i].position) <= 5 && players[i].GetComponent<Player>().needsTarget) {
                    players[i].GetComponent<Player>().needsTarget = false;
                    return players[i];
                }
            }
        }

        return null;
    }
}