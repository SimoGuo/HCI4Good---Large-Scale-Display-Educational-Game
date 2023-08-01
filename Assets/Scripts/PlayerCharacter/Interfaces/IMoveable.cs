using UnityEngine;

namespace PlayerCharacter.Interfaces {
    public interface IMoveable {
        public Vector3 TargetFinger { set; get; }
        Rigidbody rb { get; set; }
        private void Move() { }
    }
}
