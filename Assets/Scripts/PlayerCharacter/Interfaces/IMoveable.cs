using UnityEngine;

namespace PlayerCharacter.Interfaces {
    public interface IMoveable {
        Vector3 Target { set; get; }
        Rigidbody rb { get; set; }
        void Move();
    }
}
