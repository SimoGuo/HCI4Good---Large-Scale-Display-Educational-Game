using UnityEngine;

namespace PlayerCharacter.Interfaces {
    public interface IMoveable {
        public Vector3 Target { set; get; }
        Rigidbody rb { get; set; }
        private void Move() { }
    }
}
