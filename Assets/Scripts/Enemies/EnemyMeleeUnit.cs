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


    private Animator _anim;
    private void Awake()
    {
        //Get the player position through name (Script is for 1 player only, will change to more later)
        // commented out because we can set the player from the editor instead of relying on name
        agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    private void Start() {
        maxHealth = 100000;
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
            _anim.SetBool("Attack", false);
            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            _anim.SetBool("Attack", false);
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            _anim.SetBool("Attack", true);
            AttackPlayer();
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

    public float maxHealth { get; set; } = 100;
    [field: SerializeField] public float currentHealth { get; set; }
}
