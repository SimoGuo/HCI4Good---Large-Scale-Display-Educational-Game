using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.AttackSO {
    [CreateAssetMenu(fileName = "Projectile Attack", menuName = "Player Logic/Attack Logic/Projectile")]
    public class ProjectileAttack : PlayerAttackSO {
        [SerializeField] private Transform arrow;
        [SerializeField] private float arrowSpeed;
        [SerializeField] private float arrowPower;

        [SerializeField] private float attackRange;
        
        public override void Initialize(GameObject gameObject, Player player) {
            base.Initialize(gameObject, player);
        }

        public override void FrameUpdate() {
            
        }
    }
}