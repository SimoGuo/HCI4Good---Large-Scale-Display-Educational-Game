using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.MovementSO {
    [CreateAssetMenu(fileName = "Player Walk", menuName = "Player Logic/Move Logic/Walk")]
    public class PlayerWalk : PlayerMoveSO {
        [SerializeField] private float speed = 5;
        [SerializeField] private float maxSpeed = 10;
        [SerializeField] private float stoppingDistance = .5f;

        public override void Initialize(GameObject gameObject, Player player) {
            base.Initialize(gameObject, player);
            Speed = speed;
            MaxSpeed = maxSpeed;
            StoppingDistance = stoppingDistance;
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
}
