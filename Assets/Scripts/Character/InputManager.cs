using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField] private Transform players;

    private void FixedUpdate() {
        foreach (Touch touch in Input.touches) {
            Ray r = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(r, out RaycastHit hit, Mathf.Infinity, ~LayerMask.NameToLayer("Ignore Raycast"))) {
                foreach (Transform player in players) {
                    
                    if (Vector3.Distance(player.position, hit.point) < 5) {
                        if (player.GetComponent<PlayerTouchController>().needsTarget) {
                            
                            player.GetComponent<PlayerTouchController>().target = hit.point;
                            player.GetComponent<PlayerTouchController>().needsTarget = false;
                        }
                        if (touch.phase == TouchPhase.Ended) {
                            player.GetComponent<PlayerTouchController>().needsTarget = true;
                        }
                    }
                    
                }
            }

            
        }
    }
}
