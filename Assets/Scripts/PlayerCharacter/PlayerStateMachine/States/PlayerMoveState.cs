namespace PlayerCharacter.PlayerStateMachine.States {
    public class PlayerMoveState : PlayerState {
        public PlayerMoveState(Player player, global::PlayerCharacter.PlayerStateMachine.PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
            
        }
        public override void EnterState() {
            base.EnterState();
            player.PlayerMoveInstance.EnterState();
        }

        public override void ExitState() {
            base.ExitState();
            player.PlayerMoveInstance.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            player.PlayerMoveInstance.FrameUpdate();
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();
            player.PlayerMoveInstance.PhysicsUpdate();
        }

    }
}