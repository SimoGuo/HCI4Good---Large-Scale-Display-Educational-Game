using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase {
    public class PlayerAttackSO : ScriptableObject {
        protected Player player;
        protected GameObject gameObject;
        protected Animator anim;
        public virtual void Initialize(GameObject gameObject, Player player) {
            this.gameObject = gameObject;
            this.player = player;
            anim = gameObject.GetComponent<Animator>();
        }
    
        public virtual void EnterState(){}

        public virtual void FrameUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void ExitState(){}
    }
}
