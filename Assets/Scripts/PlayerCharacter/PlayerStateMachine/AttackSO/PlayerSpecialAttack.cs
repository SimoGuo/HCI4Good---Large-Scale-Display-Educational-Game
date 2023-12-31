using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.AttackSO
{
    [CreateAssetMenu(fileName = "Special Attack", menuName = "Player Logic/Attack Logic/SpecialAttack")]
    public class PlayerSpecialAttack : PlayerAttackSO
    {
        [Range(0, 3)]
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private float damageAmount;
        [SerializeField] private float cooldown;
        private float _lastAttacked = -9999f;
        public override void Initialize(GameObject gameObject, Player Player)
        {
            base.Initialize(gameObject, Player);
            Player.attackZone.radius = attackRange;
            Player.attackZone.isTrigger = true;
        }

        public override void EnterState()
        {
            base.EnterState();
            anim.Play("special attack");
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            if (Player.InAttackRange && (Time.time > _lastAttacked + cooldown))
            {
                _lastAttacked = Time.time;
                Player.TargetedEnemy.Damage(damageAmount);
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
    }
}
