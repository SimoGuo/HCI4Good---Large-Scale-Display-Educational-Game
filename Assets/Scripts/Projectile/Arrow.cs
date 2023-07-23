using PlayerCharacter.Interfaces;
using UnityEngine;

namespace Projectile {
    public class Arrow : MonoBehaviour {
        private Rigidbody _rb;
        public float ArrowSpeed { private get; set; }
        public float DamageAmount { private get; set; }
        void Start() {
            Debug.Log("speed: " + ArrowSpeed + "damage: " + DamageAmount);
            _rb = GetComponent<Rigidbody>();
            _rb.AddForce(transform.forward * ArrowSpeed, ForceMode.Force);
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.collider.CompareTag("Enemy")) {
                collision.transform.GetComponent<IDamageable>().Damage(DamageAmount);
                Destroy(gameObject);
            }
        }
    }
}