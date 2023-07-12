namespace PlayerCharacter.States {
    public class PlayerWalkState : PlayerState {
        public PlayerWalkState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
            
        }
        public override void EnterState() {
            base.EnterState();
        }

        public override void ExitState() {
            base.ExitState();
        }

        public override void FrameUpdate() {
            base.FrameUpdate();
            player.anim.Play("walk");
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();
        }

    }
}