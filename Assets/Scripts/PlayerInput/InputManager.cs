using System.Collections.Generic;
using PlayerCharacter;
using UnityEngine;

namespace PlayerInput {
    public class InputManager : MonoBehaviour {

        [SerializeField] private Transform[] players;
        [SerializeField] private bool kb = true;
        [SerializeField] private Transform closestPlayer;
        private List<TouchLocation> TouchLocations = new List<TouchLocation>();
    

        private void FixedUpdate() {
            if (kb) {
                if (UnityEngine.Input.GetMouseButton(0)) {
                    // Debug.Log("here");
                    Ray r = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                    if (Physics.Raycast(r, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) {
                        Debug.Log(hit.point);
                        Transform closestPlayer = GetClosestPlayer(hit.point);
                        if (closestPlayer != null) {
                            closestPlayer.GetComponent<Player>().NeedsTarget = false;
                            closestPlayer.GetComponent<Player>().Target = hit.point;
                        }
                    }
                }
            }
            else {
                foreach (Touch touch in UnityEngine.Input.touches) {
                    if (touch.phase == TouchPhase.Began) {
                        Debug.Log("began");
                        closestPlayer = GetClosestPlayer(touch);
                        if (closestPlayer != null) {
                            TouchLocations.Add(new TouchLocation(touch.fingerId, closestPlayer));
                        }
                    }
                    if (touch.phase == TouchPhase.Moved) {
                        Debug.Log("moved");
                        Ray r = Camera.main.ScreenPointToRay(touch.position);
                        if (Physics.Raycast(r, out RaycastHit hit)) {
                            TouchLocation thisTouch = TouchLocations.Find(i => i.fingerID == touch.fingerId);
                            thisTouch.player.GetComponent<Player>().Target = hit.point;
                        }
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        Debug.Log("ended");
                        TouchLocation thisTouch = TouchLocations.Find(i => i.fingerID == touch.fingerId);
                        thisTouch.player.GetComponent<Player>().NeedsTarget = true;
                        TouchLocations.RemoveAt(TouchLocations.IndexOf(thisTouch));
                    }
                }    
            }
        
        }

        Transform GetClosestPlayer(Vector3 mousePos) {
            for (int i = 0; i < players.Length; i++) {
                if (Vector3.Distance(mousePos, players[i].position) <= 5) {
                    return players[i];
                }
                else {
                    Debug.Log("too far");
                }
            }
            
            return null;
        }
    
        Transform GetClosestPlayer(Touch touch) {
            Ray r = Camera.main.ScreenPointToRay(touch.position);
            for (int i = 0; i < players.Length; i++) {
                if (Physics.Raycast(r, out RaycastHit hit)) {
                    if (Vector3.Distance(hit.point, players[i].position) <= 5 && players[i].GetComponent<Player>().NeedsTarget) {
                        players[i].GetComponent<Player>().NeedsTarget = false;
                        return players[i];
                    }
                }
            }

            return null;
        }
    }
}
