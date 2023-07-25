using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using Projectile;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.AttackSO {
    [CreateAssetMenu(fileName = "Projectile Attack", menuName = "Player Logic/Attack Logic/Projectile")]
    public class ProjectileAttack : PlayerAttackSO {
        [SerializeField] private Transform projectile;
        [SerializeField] private float speed;
        [SerializeField] private float damageAmount;
        [SerializeField] private float attackRange;
        [SerializeField] private float coolDown;
        private float _lastAttacked = -999f;
        
        public override void Initialize(GameObject gameObject, Player player) {
            base.Initialize(gameObject, player);
            player.attackZone.radius = attackRange;
            player.attackZone.isTrigger = true;
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            if (Player.InAttackRange && Time.time > (_lastAttacked + coolDown)) {
                _lastAttacked = Time.time;
                Transform handle = Instantiate(projectile, GameObject.transform.position, Quaternion.identity);
                if (Player.TargetedEnemy != null) {
                    handle.LookAt(Player.TargetedEnemy.transform);
                }
                handle.GetComponent<Arrow>().Direction =
                    (Player.TargetedEnemy.transform.position - GameObject.transform.position).normalized;
                handle.GetComponent<Arrow>().DamageAmount = damageAmount;
                handle.GetComponent<Arrow>().ArrowSpeed = speed;
            }
        }
    }
}