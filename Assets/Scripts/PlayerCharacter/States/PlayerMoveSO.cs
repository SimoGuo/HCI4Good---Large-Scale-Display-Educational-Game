using System.Collections;
using System.Collections.Generic;
using PlayerCharacter;
using UnityEngine;

public class PlayerMoveSO : ScriptableObject {
    public float Speed{ get; protected set; }
    public float MaxSpeed { get; protected set; }
    public float StoppingDistance { get; protected set; }
    private Animator Anim;
    private Player Player;
    private GameObject GameObject;
    
    public virtual void Initialize(GameObject gameObject, Player player) {
        this.GameObject = gameObject;
        this.Player = player;
        Anim = gameObject.GetComponent<Animator>();
        if (Anim == null) {
            Debug.LogWarning("Animator is Null");
        }
    }
    
    public virtual void EnterState(){}

    public virtual void FrameUpdate() {
        // Anim.Play("walk");
    }
    public virtual void PhysicsUpdate(){}
    public virtual void ExitState(){}
}
