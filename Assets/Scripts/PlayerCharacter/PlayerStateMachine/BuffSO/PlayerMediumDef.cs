using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.BuffSO
{
    [CreateAssetMenu(fileName = "Medium Def", menuName = "Player Logic/Buff Logic/Medium Def")]
    public class PlayerMediumDef : PlayerBuffSO
    {
        public override void Initialize(GameObject gameObject, Player player)
        {
            base.Initialize(gameObject, player);
        }

        public override void EnterState()
        {
            base.EnterState();
            anim.Play("medium def");
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
