using PlayerCharacter.Interfaces;
using UnityEngine;

namespace Projectile {
    public class Arrow : MonoBehaviour {
        private Rigidbody _rb;
        public float ArrowSpeed { private get; set; }
        public float DamageAmount { private get; set; }
        public Vector3 Direction { private get; set; }
        //Adjust this value to determine when the
        // arrow should despawn(currently set to 5 seconds)
        public float despawnTime = 5.0f; 
        
        private void Awake() {
            _rb = GetComponent<Rigidbody>();
            _rb.AddForce(Direction * ArrowSpeed, ForceMode.Impulse);
        }

        private void Start() {
            // Schedule the arrow to despawn after the specified time
            Destroy(gameObject, despawnTime);
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.collider.CompareTag("Enemy")) {
                collision.transform.GetComponent<IDamageable>().Damage(DamageAmount);
            }
        }
    }
}
