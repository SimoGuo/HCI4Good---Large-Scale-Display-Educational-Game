using UnityEngine;

public class PlayerTouchController : MonoBehaviour {
    public bool needsTarget { set; get; } = true;
    public Vector3 target { set; get; }
    [SerializeField] private float speed = 5;
    private Rigidbody rb;
    private Animator anim;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate() {
        // Debug.Log(needsTarget);
        if (needsTarget) {
            rb.velocity = Vector3.zero;
            return;
        }
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
        if (Vector3.Distance(transform.position, target) < .5f) {
            rb.velocity = Vector3.zero;
        }
        else {
            rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }

        if (rb.velocity.magnitude >= 1f) {
            anim.Play("walk");
        }
        else {
            anim.Play("attack");
        }
    }
}
