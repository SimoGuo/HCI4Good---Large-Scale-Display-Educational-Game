using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.AttackSO
{
    [CreateAssetMenu(fileName = "Ranged Attack", menuName = "Player Logic/Attack Logic/Ranged")]
    public class PlayerRangedAttack : PlayerAttackSO
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float cooldown;

        private float _lastAttacked = -9999f;

        public override void Initialize(GameObject gameObject, Player player)
        {
            base.Initialize(gameObject, Player);
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            if (Time.time > _lastAttacked + cooldown)
            {
                _lastAttacked = Time.time;
                LaunchProjectile();
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        private void LaunchProjectile()
        {
            if (projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, Player.transform.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Player.transform.right * projectileSpeed;
                }
            }
        }
    }
}
