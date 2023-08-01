using System;
using PlayerCharacter.Interfaces;
using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using PlayerCharacter.PlayerStateMachine.States;
using TMPro;
using UnityEngine;

namespace PlayerCharacter {
    
    
    public class Player : MonoBehaviour, IDamageable, IMoveable {
        public bool NeedsPlayerFinger { set; get; } = true;
        public Vector3 TargetFinger { set; get; }
        public Rigidbody rb { get; set; }
        private Animator _anim;
        public PlayerStateMachine.PlayerStateMachine _stateMachine;
        private PlayerAttackState _attackState;
        private PlayerMoveState _moveState;
        private PlayerIdleState _idleState;
        private bool _isMoving;

        [SerializeField] public SphereCollider attackZone;
        [field: SerializeField] public bool InAttackRange { get; private set; }
        [field: SerializeField] public EnemyMeleeUnit TargetedEnemy { get; private set; }
        [SerializeField] private PlayerIdleSO playerIdleBase;
        [SerializeField] private PlayerMoveSO playerMoveBase;
        [SerializeField] private PlayerAttackSO playerAttackBase;
 
        public float maxHealth { get; set; }
        public float currentHealth { get; set; }

        public PlayerIdleSO PlayerIdleInstance { get; private set; }
        public PlayerMoveSO PlayerMoveInstance { get; private set; }
        public PlayerAttackSO PlayerAttackInstance { get; private set; }

        private void Awake() {
            PlayerIdleInstance = Instantiate(playerIdleBase);
            PlayerMoveInstance = Instantiate(playerMoveBase);
            PlayerAttackInstance = Instantiate(playerAttackBase);
            
            _stateMachine = new PlayerStateMachine.PlayerStateMachine();
            
            _idleState = new PlayerIdleState(this, _stateMachine);
            _moveState = new PlayerMoveState(this, _stateMachine);
            _attackState = new PlayerAttackState(this, _stateMachine);

        }

        private void Start() {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            _anim = GetComponent<Animator>();
            
            PlayerIdleInstance.Initialize(gameObject, this);
            PlayerMoveInstance.Initialize(gameObject, this);
            PlayerAttackInstance.Initialize(gameObject, this);

            _stateMachine.Init(_idleState);
        }

        private void FixedUpdate() {
            Move();
            _stateMachine.CurrentPlayerState.PhysicsUpdate();
            
        }

        private void Update() {
            // if (_stateMachine == null) Debug.Log("statemachine null");
            // if (_stateMachine.CurrentPlayerState == null) Debug.Log("currentState null");
            // Debug.Log(NeedsPlayerFinger + " " + TargetFinger);
            _stateMachine.CurrentPlayerState.FrameUpdate();
            // Debug.Log(rb.velocity.magnitude);
            _anim.SetBool("Moving", _isMoving);
            _anim.SetBool("Attack", InAttackRange);
            if (TargetedEnemy == null) {
                InAttackRange = false;
            }

            if (!_isMoving && !InAttackRange) {
                TargetedEnemy = null;
                _stateMachine.ChangeState(_idleState);
            }

            if (!_isMoving && InAttackRange) {
                _stateMachine.ChangeState(_attackState);
            }

            if (_isMoving) {
                _stateMachine.ChangeState(_moveState);
            }
        }

        private void Move() {
            if (NeedsPlayerFinger) {
                rb.velocity = Vector3.zero;
                _isMoving = false;
                return;
            }

            if (_isMoving) {
                transform.LookAt(new Vector3(TargetFinger.x, transform.position.y, TargetFinger.z));
            }
            else {
                if (TargetedEnemy != null) {
                    
                    transform.LookAt(new Vector3(TargetedEnemy.transform.position.x, transform.position.y, TargetedEnemy.transform.position.z));
                }
            }
            if (Vector3.Distance(transform.position, new Vector3(TargetFinger.x, transform.position.y, TargetFinger.z)) <= PlayerMoveInstance.StoppingDistance) {
                rb.velocity = Vector3.zero;
                _isMoving = false;
            }
            else {
                _isMoving = true;
                if (rb.velocity.magnitude <= PlayerMoveInstance.MaxSpeed) {
                    rb.AddForce(transform.forward * PlayerMoveInstance.Speed, ForceMode.Acceleration);
                }
            }

        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Enemy")) {
                InAttackRange = true;
                TargetedEnemy = other.GetComponent<EnemyMeleeUnit>();
            }
        }

        private void OnTriggerStay(Collider other) {
            if (other.CompareTag("Enemy")) {
                InAttackRange = true;
                if (TargetedEnemy == null) {
                    TargetedEnemy = other.GetComponent<EnemyMeleeUnit>();
                }
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Enemy")) {
                InAttackRange = false;
                TargetedEnemy = null;
            }
        }

        public void Damage(float amount) {
            throw new NotImplementedException();
        }

        public void Kill() {
            throw new NotImplementedException();
        }
        
    }
}
