using UnityEngine;

namespace PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase {
    public class PlayerMoveSO : ScriptableObject {
        public float Speed{ get; protected set; }
        public float MaxSpeed { get; protected set; }
        public float StoppingDistance { get; protected set; }
        
        private Player Player;
        private GameObject GameObject;
    
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
