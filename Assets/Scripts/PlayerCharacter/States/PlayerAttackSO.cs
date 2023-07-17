using System.Collections;
using System.Collections.Generic;
using PlayerCharacter;
using UnityEngine;

public class PlayerAttackSO : ScriptableObject {
    protected Animator anim;
    protected Player player;
    protected GameObject gameObject;
    
    public virtual void Initialize(GameObject gameObject, Player player) {
        this.gameObject = gameObject;
        this.player = player;
        anim = gameObject.GetComponent<Animator>();
        if (anim == null) {
            Debug.LogWarning("Animator is Null");
        }
    }
    
    public virtual void EnterState(){}

    public virtual void FrameUpdate() {
        // anim.Play("attack");
    }

    public virtual void PhysicsUpdate() {
        
    }
    public virtual void ExitState(){}
}
