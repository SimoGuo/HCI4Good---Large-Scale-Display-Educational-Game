using Character;
using PlayerCharacter.States;
using UnityEngine;


public class PlayerStateMachine {
    public PlayerState currentPlayerState { get; set; }

    public void Init(PlayerState startingState) {
        currentPlayerState = startingState;
        currentPlayerState.EnterState();
    }

    public void ChangeState(PlayerState state) {
        if (state == currentPlayerState) return;
        state.ExitState();
        currentPlayerState = state;
        state.EnterState();
    }
}
