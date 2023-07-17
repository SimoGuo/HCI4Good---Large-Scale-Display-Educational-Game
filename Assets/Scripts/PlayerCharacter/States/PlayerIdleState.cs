using UnityEngine;

namespace PlayerCharacter.States {
    public class PlayerIdleState : PlayerState {
        public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
        }
        public override void EnterState() {
            base.EnterState();
            player.PlayerIdleInstance.EnterState();
        }

        public override void ExitState() {
            base.ExitState();
            player.PlayerIdleInstance.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            // player.Anim.Play("idle");
            player.PlayerIdleInstance.FrameUpdate();
            
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();
            player.PlayerIdleInstance.PhysicsUpdate();
        }
        
    }
}