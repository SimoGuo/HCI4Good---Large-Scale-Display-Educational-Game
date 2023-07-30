using System.Collections.Generic;
using PlayerCharacter;
using UnityEngine;

namespace PlayerInput {
    public class InputManager : MonoBehaviour {

        [SerializeField] private Transform[] players;
        [SerializeField] private bool kb = true;
        private Transform _closestPlayer;
        private List<TouchLocation> _touchLocations = new List<TouchLocation>();
    

        private void FixedUpdate() {
            if (kb) {
                if (UnityEngine.Input.GetMouseButton(0)) {
                    // Debug.Log("here");
                    Ray r = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                    if (Physics.Raycast(r, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) {
                        // Debug.Log(hit.point);
                        Transform closestPlayer = GetClosestPlayer(hit.point);
                        if (closestPlayer != null) {
                            closestPlayer.GetComponent<Player>().NeedsPlayerFinger = false;
                            closestPlayer.GetComponent<Player>().TargetFinger = hit.point;
                        }
                    }
                }
            }
            else {
                foreach (Touch touch in UnityEngine.Input.touches) {
                    if (touch.phase == TouchPhase.Began) {
                        Debug.Log("began");
                        _closestPlayer = GetClosestPlayer(touch);
                        if (_closestPlayer != null) {
                            _touchLocations.Add(new TouchLocation(touch.fingerId, _closestPlayer));
                        }
                    }
                    if (touch.phase == TouchPhase.Moved) {
                        Debug.Log("moved");
                        Ray r = Camera.main.ScreenPointToRay(touch.position);
                        if (Physics.Raycast(r, out RaycastHit hit)) {
                            TouchLocation thisTouch = _touchLocations.Find(i => i.fingerID == touch.fingerId);
                            thisTouch.player.GetComponent<Player>().TargetFinger = hit.point;
                        }
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        Debug.Log("ended");
                        TouchLocation thisTouch = _touchLocations.Find(i => i.fingerID == touch.fingerId);
                        thisTouch.player.GetComponent<Player>().NeedsPlayerFinger = true;
                        _touchLocations.RemoveAt(_touchLocations.IndexOf(thisTouch));
                    }
                }    
            }
        
        }

        Transform GetClosestPlayer(Vector3 mousePos) {
            for (int i = 0; i < players.Length; i++) {
                if (Vector3.Distance(mousePos, players[i].position) <= 5) {
                    return players[i];
                }
            }
            
            return null;
        }
    
        Transform GetClosestPlayer(Touch touch) {
            Ray r = Camera.main.ScreenPointToRay(touch.position);
            for (int i = 0; i < players.Length; i++) {
                if (Physics.Raycast(r, out RaycastHit hit)) {
                    if (Vector3.Distance(hit.point, players[i].position) <= 5 && players[i].GetComponent<Player>().NeedsPlayerFinger) {
                        players[i].GetComponent<Player>().NeedsPlayerFinger = false;
                        return players[i];
                    }
                }
            }

            return null;
        }
    }
}
