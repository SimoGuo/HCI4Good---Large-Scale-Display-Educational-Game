using UnityEngine;

public class PlayerTouchController : MonoBehaviour {
    [SerializeField] private float speed = 5;
    private Rigidbody rb;
    private Animator anim;
    public Vector3 target { set; private get; }
    public bool needsTarget { set; get; } = true;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate() {
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
        if (Vector3.Distance(transform.position, target) < .1f) {
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
