using UnityEngine;

namespace PlayerCharacter.States {
    public class PlayerAttackState : PlayerState {
        public PlayerAttackState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
        }

        public override void EnterState() {
            base.EnterState();
        }

        public override void ExitState() {
            base.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            player.anim.Play("attack");
            
            // attacking code
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();
        }
        
    }
}