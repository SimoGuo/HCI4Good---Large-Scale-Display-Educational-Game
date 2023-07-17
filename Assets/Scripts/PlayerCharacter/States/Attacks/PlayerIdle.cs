using PlayerCharacter;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Idle", menuName = "Player Logic/Idle Logic/Idle")]
public class PlayerIdle : PlayerIdleSO {
    public override void Initialize(GameObject gameObject, Player player) {
        base.Initialize(gameObject, player);
    }

    public override void EnterState() {
        base.EnterState();
    }

    public override void FrameUpdate() {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public override void ExitState() {
        base.ExitState();
    }
}