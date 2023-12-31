using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.AttackSO {
    [CreateAssetMenu(fileName = "Melee Attack", menuName = "Player Logic/Attack Logic/Melee")]
    public class PlayerMeleeAttack : PlayerAttackSO {
        [Range(0, 3)]
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private float damageAmount;
        [SerializeField] private float cooldown;
        private float _lastAttacked = -9999f;
        public override void Initialize(GameObject gameObject, Player player) {
            base.Initialize(gameObject, player);
            player.attackZone.radius = attackRange;
            player.attackZone.isTrigger = true;
        }

        public override void EnterState() {
            base.EnterState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            if (Player.InAttackRange && (Time.time > _lastAttacked + cooldown)) {
                _lastAttacked = Time.time;
                Player.TargetedEnemy.Damage(10000);
            }
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();
        }

        public override void ExitState() {
            base.ExitState();
        }
    }
}