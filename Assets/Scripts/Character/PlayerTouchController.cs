using UnityEngine;

public class PlayerTouchController : MonoBehaviour {
    public bool needsTarget { set; get; } = true;
    public Vector3 target { set; get; }
    [SerializeField] private float speed = 5;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float stoppingDistance = .5f;
    private Rigidbody rb;
    private Animator anim;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate() {
        if (rb.velocity.magnitude >= 1f) {
            Debug.Log("walking");
            anim.Play("walk");
        }
        else {
            Debug.Log("attacking");
            anim.Play("Armature|attack");
        }
        if (needsTarget) {
            // rb.velocity = Vector3.zero;
            return;
        }
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
        if (Vector3.Distance(transform.position, target) < stoppingDistance) {
            rb.velocity = Vector3.zero;
        }
        else {
            if (rb.velocity.magnitude <= maxSpeed) {
                rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
            }
        }

        
    }
}
