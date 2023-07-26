using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.BuffSO
{
    [CreateAssetMenu(fileName = "Highest Strength", menuName = "Player Logic/Buff Logic/Highest Strength")]
    public class PlayerHighestStrength : PlayerBuffSO
    {
        public override void Initialize(GameObject gameObject, Player player)
        {
            base.Initialize(gameObject, player);
        }

        public override void EnterState()
        {
            base.EnterState();
            anim.Play("highest strength");
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
