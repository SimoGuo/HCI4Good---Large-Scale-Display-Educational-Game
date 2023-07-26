using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.States
{
    public class PlayerBuffState : PlayerState
    {
        public PlayerBuffState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
        }
        public override void EnterState()
        {
            base.EnterState();
            player.PlayerBuffInstance.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
            player.PlayerBuffInstance.ExitState();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            player.PlayerBuffInstance.FrameUpdate();

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            player.PlayerBuffInstance.PhysicsUpdate();
        }
    }
}
