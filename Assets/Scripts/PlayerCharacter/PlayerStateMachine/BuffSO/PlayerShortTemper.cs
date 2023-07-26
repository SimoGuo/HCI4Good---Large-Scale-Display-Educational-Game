using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.BuffSO
{
    [CreateAssetMenu(fileName = "Short Temper", menuName = "Player Logic/Buff Logic/Short Temper")]
    public class PlayerShortTemper : PlayerBuffSO
    {
        public override void Initialize(GameObject gameObject, Player player)
        {
            base.Initialize(gameObject, player);
        }

        public override void EnterState()
        {
            base.EnterState();
            anim.Play("short temper");
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
