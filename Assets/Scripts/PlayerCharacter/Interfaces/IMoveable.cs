using UnityEngine;

namespace PlayerCharacter.Interfaces {
    public interface IMoveable {
        public bool needsTarget { set; get; }
        public Vector3 target { set; get; }
        Rigidbody rb { get; set; }
        void Move();
    }
}
