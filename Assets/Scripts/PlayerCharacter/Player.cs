/*using System;
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
        public character enemy;

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
            DealDamageToCharacter(20f);
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

        private void DealDamageToCharacter(float damageAmount)
        {
            // Find the character component attached to the GameObject
            character characterComponent = GetComponent<character>();

            // Check if the character component is found
            if (characterComponent != null)
            {
                // Call the TakeDamage method to deal damage
                characterComponent.TakeDamage(damageAmount);
            }
        }

            private void Move() {
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
                enemy = other.GetComponent<character>();
                enemy.TakeDamage(20f);
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
*/


using System;
using System.Diagnostics;
using PlayerCharacter.Interfaces;
using PlayerCharacter.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

namespace PlayerCharacter
{
    public class Player : MonoBehaviour, IDamageable
    {
        public bool NeedsTarget { set; get; } = true;
        public Vector3 Target { set; get; }
        public Rigidbody rb { get; set; }
        public Animator Anim { get; private set; }
        private PlayerStateMachine _stateMachine;
        private PlayerAttackState _attackState;
        private PlayerMoveState _moveState;
        private PlayerIdleState _idleState;
        [SerializeField] public SphereCollider attackZone;
        public bool InAttackRange { get; private set; }
        public EnemyMeleeUnit TargetedEnemy { get; private set; }
        public character enemy;

        [SerializeField] private PlayerIdleSO playerIdleBase;
        [SerializeField] private PlayerMoveSO playerMoveBase;
        [SerializeField] private PlayerAttackSO playerAttackBase;

        public PlayerIdleSO PlayerIdleInstance { get; private set; }
        public PlayerMoveSO PlayerMoveInstance { get; private set; }
        public PlayerAttackSO PlayerAttackInstance { get; private set; }

        private bool isAttacking = false;
        private bool canAttack = true;
        [SerializeField] private float attackCooldownDuration = 1.5f;

        private void Awake()
        {
            PlayerIdleInstance = Instantiate(playerIdleBase);
            PlayerMoveInstance = Instantiate(playerMoveBase);
            PlayerAttackInstance = Instantiate(playerAttackBase);

            _stateMachine = new PlayerStateMachine();

            _idleState = new PlayerIdleState(this, _stateMachine);
            _moveState = new PlayerMoveState(this, _stateMachine);
            _attackState = new PlayerAttackState(this, _stateMachine);
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            Anim = GetComponent<Animator>();

            PlayerIdleInstance.Initialize(gameObject, this);
            PlayerMoveInstance.Initialize(gameObject, this);
            PlayerAttackInstance.Initialize(gameObject, this);

            _stateMachine.Init(_idleState);
        }

        private void FixedUpdate()
        {
            Move();
            _stateMachine.CurrentPlayerState.PhysicsUpdate();
        }

        private void Update()
        {
            _stateMachine.CurrentPlayerState.FrameUpdate();
            Anim.SetFloat("Speed", rb.velocity.magnitude);
            Anim.SetBool("Attack", InAttackRange);

            // Only attack if not already attacking and not on cooldown
            if (!isAttacking && canAttack && InAttackRange)
            {
               /* StartCoroutine(AttackRoutine());*/
            }

            if (rb.velocity.magnitude <= 0.01f)
            {
                UnityEngine.Debug.Log("idling");
                _stateMachine.ChangeState(_idleState);
            }
            else
            {
                UnityEngine.Debug.Log("moving");
                _stateMachine.ChangeState(_moveState);
            }
        }

        private IEnumerator AttackRoutine(Collider other)
        {
            isAttacking = true;
            Anim.SetTrigger("Attack");
            yield return new WaitForSeconds(PlayerAttackInstance.AttackDuration);
            DealDamageToCharacter(0.5f, other);
            canAttack = false;
            yield return new WaitForSeconds(attackCooldownDuration);
            canAttack = true;
            isAttacking = false;
        }

        private void DealDamageToCharacter(float damageAmount, Collider other)
        {
            character characterComponent = other.GetComponent<character>();
            if (characterComponent != null)
            {
                characterComponent.TakeDamage(damageAmount);
                if(characterComponent.currentHealth <= 0) {
                    InAttackRange = false;
                    TargetedEnemy = null;
                }
            }
        }

        private void Move()
        {
            if (NeedsTarget)
            {
                rb.velocity = Vector3.zero;
                return;
            };

            transform.LookAt(new Vector3(Target.x, transform.position.y, Target.z));
            if (Vector3.Distance(transform.position, Target) <= PlayerMoveInstance.StoppingDistance)
            {
                rb.velocity = Vector3.zero;
            }
            else
            {
                if (rb.velocity.magnitude <= PlayerMoveInstance.MaxSpeed)
                {
                    rb.AddForce(transform.forward * PlayerMoveInstance.Speed, ForceMode.Acceleration);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                InAttackRange = true;
                TargetedEnemy = other.GetComponent<EnemyMeleeUnit>();
                StartCoroutine(AttackRoutine(other));
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                InAttackRange = true;
                TargetedEnemy = other.GetComponent<EnemyMeleeUnit>();
                StartCoroutine(AttackRoutine(other));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                InAttackRange = false;
                TargetedEnemy = null;
            }
        }

        public void Damage(float amount)
        {
            throw new NotImplementedException();
        }

        public void Kill()
        {
            throw new NotImplementedException();
        }

        public float maxHealth { get; set; }
        public float currentHealth { get; set; }
    }
}
