using UnityEngine;

namespace PlayerCharacter.States {
    public class PlayerAttackState : PlayerState {
        public PlayerAttackState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
        }

        public override void EnterState() {
            base.EnterState();
            player.PlayerAttackInstance.EnterState();
        }

        public override void ExitState() {
            base.ExitState();
            player.PlayerAttackInstance.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            player.PlayerAttackInstance.FrameUpdate();
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();
            player.PlayerAttackInstance.PhysicsUpdate();
        }
        
    }
}