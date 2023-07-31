using System;
using System.Collections;
using System.Collections.Generic;
using PlayerCharacter.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMeleeUnit: MonoBehaviour, IDamageable
{
    private NavMeshAgent agent;
    
    //Player position
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask groundLayer, playerLayer;

    //Patrolling
    private Vector3 walkPoint;
    private bool walkPointSet;
    [SerializeField] private float walkPointRange;

    //Attacking
    [SerializeField] private float timeBetweenAttacks;
    private bool alreadyAttacked;

    //States
    [SerializeField] private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    //Variables for testing
    private Vector3 distanceToWalkPoint;
    private float distance;
    // player = GameObject.Find("PlayerCharacter").transform;
    public float maxHealth { get; set; } = 100;
    [field: SerializeField] public float currentHealth { get; set; }

    private SphereCollider _attackCollider;
    private Animator _animator;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _attackCollider = GetComponent<SphereCollider>();
        _attackCollider.radius = sightRange;
        _attackCollider.isTrigger = true;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        //Check for player in sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            _animator.SetBool("Attack", false);
            Debug.Log("here1");
            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange && player != null)
        {
            _animator.SetBool("Attack", false);
            Debug.Log("here2");
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange && player != null)
        {
            _animator.SetBool("Attack", true);
            Debug.Log("here3");
            AttackPlayer();
        }
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Player")) {
            player = other.transform;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.collider.CompareTag("Player")) {
            player = null;
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet) {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        distanceToWalkPoint = transform.position - walkPoint;
        distance = distanceToWalkPoint.magnitude;

        //Walkpoint reached (Will not work if NavAgent stopping distance > 1)
        if (distanceToWalkPoint.magnitude < 1) {
        walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Caculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3 (transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make the enemy stop before attacking
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void Damage(float amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) Kill();
    }

    public void Kill() {
        Destroy(gameObject);
    }
}
