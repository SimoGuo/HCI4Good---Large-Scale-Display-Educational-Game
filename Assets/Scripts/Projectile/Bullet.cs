using PlayerCharacter.Interfaces;
using UnityEngine;

namespace Projectile {
    public class Bullet : MonoBehaviour {
        //[SerializeField] private Transform bullet;
        private Rigidbody _rb;
        public float BulletSpeed { private get; set; }
        public float DamageAmount { private get; set; }
        void Start() {
            _rb = GetComponent<Rigidbody>();
            _rb.AddForce(transform.forward * BulletSpeed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.collider.CompareTag("Enemy")) {
                collision.transform.GetComponent<IDamageable>().Damage(DamageAmount);
                // Destroy(gameObject);
            }
        }
    }
}