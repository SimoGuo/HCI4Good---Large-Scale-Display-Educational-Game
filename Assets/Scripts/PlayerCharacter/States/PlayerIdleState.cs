using UnityEngine;

namespace PlayerCharacter.States {
    public class PlayerIdleState : PlayerState {
        public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
        }
        public override void EnterState() {
            base.EnterState();
        }

        public override void ExitState() {
            base.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            player.anim.Play("idle");
            
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();
        }
        
    }
}