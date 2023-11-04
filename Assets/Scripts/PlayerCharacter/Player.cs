using System;
using PlayerCharacter.Interfaces;
using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using PlayerCharacter.PlayerStateMachine.States;
using TMPro;
using UnityEngine;

namespace PlayerCharacter {

    public class Player : MonoBehaviour, IDamageable, IMoveable {
        // Public properties
        public bool NeedsPlayerFinger { set; get; } = true;
        public Vector3 TargetFinger { set; get; }
        public Rigidbody rb { get; set; }
        private Animator _anim;
        public PlayerStateMachine.PlayerStateMachine _stateMachine;
        private PlayerAttackState _attackState;
        private PlayerMoveState _moveState;
        private PlayerIdleState _idleState;
        private bool _isMoving;

        // Serialized fields
        [SerializeField] public SphereCollider attackZone;
        [field: SerializeField] public bool InAttackRange { get; private set; }
        [field: SerializeField] public EnemyMeleeUnit TargetedEnemy { get; private set; }
        [SerializeField] private PlayerIdleSO playerIdleBase;
        [SerializeField] private PlayerMoveSO playerMoveBase;
        [SerializeField] private PlayerAttackSO playerAttackBase;

        // Health properties
        public float maxHealth { get; set; }
        public float currentHealth { get; set; }

        // Player State Objects
        public PlayerIdleSO PlayerIdleInstance { get; private set; }
        public PlayerMoveSO PlayerMoveInstance { get; private set; }
        public PlayerAttackSO PlayerAttackInstance { get; private set; }

        private void Awake() {
            // Initialize Player State Objects
            PlayerIdleInstance = Instantiate(playerIdleBase);
            PlayerMoveInstance = Instantiate(playerMoveBase);
            PlayerAttackInstance = Instantiate(playerAttackBase);

            // Initialize the Player State Machine
            _stateMachine = new PlayerStateMachine.PlayerStateMachine();

            // Initialize State objects
            _idleState = new PlayerIdleState(this, _stateMachine);
            _moveState = new PlayerMoveState(this, _stateMachine);
            _attackState = new PlayerAttackState(this, _stateMachine);

            // Set initial state to Idle
            _stateMachine.Init(_idleState);
        }

        private void Start() {
            // Initialize Rigidbody and Animator components
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            _anim = GetComponent<Animator>();

            // Initialize Player State Objects
            PlayerIdleInstance.Initialize(gameObject, this);
            PlayerMoveInstance.Initialize(gameObject, this);
            PlayerAttackInstance.Initialize(gameObject, this);

            // Set initial state to Idle
            _stateMachine.Init(_idleState);
        }
        private void FixedUpdate()
        {
            Move();
            _stateMachine.CurrentPlayerState.PhysicsUpdate();

        }
        private void Update() {
            // Update the Animator flags based on player's state
            _anim.SetBool("Moving", _isMoving);
            _anim.SetBool("Attack", InAttackRange);

            // Check if there is a TargetedEnemy
            if (TargetedEnemy == null) {
                InAttackRange = false;
            } else if (!_isMoving && !InAttackRange) {
                // If not moving and not in attack range, change to Attack state
                TargetedEnemy = null;
                _stateMachine.ChangeState(_idleState);
                _stateMachine.ChangeState(_attackState);
            }

            if (_isMoving) {
                // If the player is moving, change to Move state
                _stateMachine.ChangeState(_moveState);
            }
        }

        private void Move() {
            if (NeedsPlayerFinger) {
                // If player doesn't need to move, reset velocity and set not moving
                rb.velocity = Vector3.zero;
                _isMoving = false;
                return;
            }

            if (_isMoving) {
                // Rotate the player to face the target finger
                transform.LookAt(new Vector3(TargetFinger.x, transform.position.y, TargetFinger.z));
            } else {
                if (TargetedEnemy != null) {
                    // If there's a targeted enemy, rotate to face the enemy
                    transform.LookAt(new Vector3(TargetedEnemy.transform.position.x, transform.position.y, TargetedEnemy.transform.position.z));
                }
            }

            // Check if the player is close enough to the target
            if (Vector3.Distance(transform.position, new Vector3(TargetFinger.x, transform.position.y, TargetFinger.z)) <= PlayerMoveInstance.StoppingDistance) {
                rb.velocity = Vector3.zero;
                _isMoving = false;
            } else {
                // Player is moving, apply force in the forward direction
                _isMoving = true;
                if (rb.velocity.magnitude <= PlayerMoveInstance.MaxSpeed) {
                    rb.AddForce(transform.forward * PlayerMoveInstance.Speed, ForceMode.Acceleration);
                }
            }
        }

        private void OnTriggerEnter(Collider other) {
            // Handle entering the attack zone and target an enemy
            if (other.CompareTag("Enemy")) {
                InAttackRange = true;
                TargetedEnemy = other.GetComponent<EnemyMeleeUnit>();
            }
        }

        private void OnTriggerStay(Collider other) {
            // Handle staying in the attack zone and targeting an enemy
            if (other.CompareTag("Enemy")) {
                InAttackRange = true;
                if (TargetedEnemy == null) {
                    TargetedEnemy = other.GetComponent<EnemyMeleeUnit>();
                }
            }
        }

        private void OnTriggerExit(Collider other) {
            // Handle exiting the attack zone and reset targeting
            if (other.CompareTag("Enemy")) {
                InAttackRange = false;
                TargetedEnemy = null;
            }
        }

        public void Damage(float amount) {
            // Handle damage to the player (not implemented)
            throw new NotImplementedException();
        }

        public void Kill() {
            // Handle the player's death (not implemented)
            throw new NotImplementedException();
        }
    }
}
