using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.BuffSO
{
    [CreateAssetMenu(fileName = "Battle Cry", menuName = "Player Logic/Buff Logic/Battle Cry")]
    public class PlayerBattleCry : PlayerBuffSO
    {
        public override void Initialize(GameObject gameObject, Player player)
        {
            base.Initialize(gameObject, player);
        }

        public override void EnterState()
        {
            base.EnterState();
            anim.Play("buff");
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
