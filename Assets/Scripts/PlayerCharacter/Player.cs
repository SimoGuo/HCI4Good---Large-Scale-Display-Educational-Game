using System;
using PlayerCharacter.Interfaces;
using PlayerCharacter.States;
using UnityEngine;

namespace PlayerCharacter {
    
    public class Player : MonoBehaviour, IMoveable, IDamageable {
        public bool needsTarget { set; get; } = true;
        public Vector3 target { set; get; }
        public Rigidbody rb { get; set; }
        public Animator anim;
        private PlayerStateMachine stateMachine;
        private PlayerAttackState attackState;
        private PlayerWalkState walkState;
        private PlayerIdleState idleState;
        public PlayerMeleeCombat attackZone;
        
        
        [SerializeField] private float speed = 5;
        [SerializeField] private float maxSpeed = 10;
        [SerializeField] private float stoppingDistance = .5f;
        

        private void Awake() {
            stateMachine = new PlayerStateMachine();
            idleState = new PlayerIdleState(this, stateMachine);
            walkState = new PlayerWalkState(this, stateMachine);
            attackState = new PlayerAttackState(this, stateMachine);
            anim = GetComponent<Animator>();
            
        }

        private void Start() {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            anim = GetComponent<Animator>();
            stateMachine.Init(idleState);
        }
       public bool _hasTarget;
        /*public bool HasTarget()
        {
            if(_hasTarget = attackZone.detectedColliders.Count>0){
                return true;
            }
            else return false;
        }*/
        
        
        
        

        private void FixedUpdate() {
            stateMachine.currentPlayerState.PhysicsUpdate();
            Move();
        }
        //public int radar = attackZone.detectedColliders.Count;
        

        private void Update() {
            
            int radar = attackZone.detectedColliders.Count;
            stateMachine.currentPlayerState.FrameUpdate();
            if (rb.velocity.magnitude == 0 && radar == 0) {
                stateMachine.ChangeState(idleState);
            }

            else if (radar>0) {
                stateMachine.ChangeState(attackState);
            }
            else {
                stateMachine.ChangeState(walkState);
            }
            
            
            
        }
       

        

        public void Move() {
            if (needsTarget) return;
        
            transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
            if (Vector3.Distance(transform.position, target) < stoppingDistance) {
                rb.velocity = Vector3.zero;
            }
            else {
                if (rb.velocity.magnitude <= maxSpeed) {
                    rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
                }
            }

        }

        public void Damage(float amount) {
            throw new NotImplementedException();
        }

        public void Kill() {
            throw new NotImplementedException();
        }

        public float maxHealth { get; set; }
        public float currentHealth { get; set; }
    }
}
