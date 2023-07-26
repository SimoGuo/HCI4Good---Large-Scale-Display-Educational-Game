using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.BuffSO
{
    [CreateAssetMenu(fileName = "No Mana Power", menuName = "Player Logic/Buff Logic/No Mana Power")]
    public class PlayerNoManaPower : PlayerBuffSO
    {
        public override void Initialize(GameObject gameObject, Player player)
        {
            base.Initialize(gameObject, player);
        }

        public override void EnterState()
        {
            base.EnterState();
            anim.Play("no mana power");
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
