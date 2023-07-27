using System;
using PlayerCharacter.Interfaces;
using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using PlayerCharacter.PlayerStateMachine.States;
using UnityEngine;

namespace PlayerCharacter {
    
    
    public class Player : MonoBehaviour, IDamageable, IMoveable {
        public bool NeedsTarget { set; get; } = true;
        public Vector3 Target { set; get; }
        public Rigidbody rb { get; set; }
        private Animator _anim;
        public PlayerStateMachine.PlayerStateMachine _stateMachine;
        private PlayerAttackState _attackState;
        private PlayerMoveState _moveState;
        private PlayerIdleState _idleState;
        private PlayerBuffState _buffState;

        [SerializeField] public SphereCollider attackZone;
        public bool InAttackRange { get; private set; }
        public EnemyMeleeUnit TargetedEnemy { get; private set; }

        [SerializeField] private PlayerIdleSO playerIdleBase;
        [SerializeField] private PlayerMoveSO playerMoveBase;
        [SerializeField] private PlayerAttackSO playerAttackBase;
        [SerializeField] private PlayerBuffSO playerBuffBase;


        public PlayerIdleSO PlayerIdleInstance { get; private set; }
        public PlayerMoveSO PlayerMoveInstance { get; private set; }
        public PlayerAttackSO PlayerAttackInstance { get; private set; }
        public PlayerBuffSO PlayerBuffInstance { get; private set; }


        private void Awake() {
            PlayerIdleInstance = Instantiate(playerIdleBase);
            PlayerMoveInstance = Instantiate(playerMoveBase);
            PlayerAttackInstance = Instantiate(playerAttackBase);
            
            _stateMachine = new PlayerStateMachine.PlayerStateMachine();
            
            _idleState = new PlayerIdleState(this, _stateMachine);
            _moveState = new PlayerMoveState(this, _stateMachine);
            _attackState = new PlayerAttackState(this, _stateMachine);
            _buffState = new PlayerBuffState(this, _stateMachine);

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
            if (_stateMachine == null) Debug.Log("statemachine null");
            if (_stateMachine.CurrentPlayerState == null) Debug.Log("currentState null");
            _stateMachine.CurrentPlayerState.FrameUpdate();
            Debug.Log(rb.velocity.magnitude);
            _anim.SetFloat("Speed", rb.velocity.magnitude);
            _anim.SetBool("Attack", InAttackRange);

            if (rb.velocity.magnitude <= 0.01f) {
                Debug.Log("idling");
                _stateMachine.ChangeState(_idleState);
            }
            else {
                Debug.Log("moving");
                _stateMachine.ChangeState(_moveState);
            }
            
            if (InAttackRange) {
                Debug.Log("in range");
                if (TargetedEnemy != null) {
                    _stateMachine.ChangeState(_attackState);
                }
                else {
                    InAttackRange = false;
                }
            }
        }

        public void Move() {
            if (NeedsTarget) {
                rb.velocity = Vector3.zero;
                return;
            };
            
            transform.LookAt(new Vector3(Target.x, transform.position.y, Target.z));
            if (Vector3.Distance(transform.position, Target) <= PlayerMoveInstance.StoppingDistance) {
                rb.velocity = Vector3.zero;
            }
            else {
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

        public float maxHealth { get; set; }
        public float currentHealth { get; set; }

        public void ChangePlayerAbilitie(PlayerMoveSO _playerMoveInstance)
        {
            PlayerMoveInstance = _playerMoveInstance;
        }

        public void ChangePlayerAbilitie(PlayerAttackSO _playerAttackInstance)
        {
            PlayerAttackInstance = _playerAttackInstance;
        }

        public void ChangePlayerAbilitie(PlayerBuffSO _playerBuffInstance)
        {
            PlayerBuffInstance = _playerBuffInstance;
        }
    }
}
