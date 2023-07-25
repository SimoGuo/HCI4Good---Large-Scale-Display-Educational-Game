using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase {
    public class PlayerIdleSO : ScriptableObject {
        protected Player Player;
        protected GameObject GameObject;
    
        public virtual void Initialize(GameObject gameObject, Player player) {
            this.GameObject = gameObject;
            this.Player = player;
        }
    
        public virtual void EnterState(){ }
        public virtual void FrameUpdate() { }
        public virtual void PhysicsUpdate(){ }
        public virtual void ExitState(){ }
    }
}