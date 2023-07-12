using UnityEngine;

namespace PlayerCharacter.States {
    public class PlayerState {
        protected Player player;
        protected PlayerStateMachine playerStateMachine;

        public PlayerState(Player player, PlayerStateMachine playerStateMachine) {
            this.player = player;
            this.playerStateMachine = playerStateMachine;
        }

        public virtual void EnterState(){}
        public virtual void FrameUpdate(){}
        public virtual void PhysicsUpdate(){}
        public virtual void ExitState(){}
    }
}