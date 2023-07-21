using PlayerCharacter.PlayerStateMachine.States;

namespace PlayerCharacter.PlayerStateMachine {
    public class PlayerStateMachine {
        public PlayerState CurrentPlayerState { get; private set; }

        public void Init(PlayerState startingState) {
            CurrentPlayerState = startingState;
            CurrentPlayerState.EnterState();
        }

        public void ChangeState(PlayerState state) {
            if (state == CurrentPlayerState) return;
            state.ExitState();
            CurrentPlayerState = state;
            state.EnterState();
        }
    }
}
