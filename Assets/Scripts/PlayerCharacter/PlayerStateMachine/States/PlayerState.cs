using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.States {
    public class PlayerState {
        protected Player player;
        private global::PlayerCharacter.PlayerStateMachine.PlayerStateMachine playerStateMachine;

        protected PlayerState(Player player, global::PlayerCharacter.PlayerStateMachine.PlayerStateMachine playerStateMachine) {
            this.player = player;
            this.playerStateMachine = playerStateMachine;
        }

        public virtual void EnterState(){}
        public virtual void FrameUpdate(){}
        public virtual void PhysicsUpdate(){}
        public virtual void ExitState(){}
    }
}