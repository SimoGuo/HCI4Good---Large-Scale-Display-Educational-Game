using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase {
    public class PlayerAttackSO : ScriptableObject {
        protected Player Player;
        protected GameObject GameObject;
    protected Animator anim;
        public virtual void Initialize(GameObject gameObject, Player player) {
            this.GameObject = gameObject;
            this.Player = player;
            anim = gameObject.GetComponent<Animator>();
        }
    
        public virtual void EnterState(){}

        public virtual void FrameUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void ExitState(){}
    }
}
