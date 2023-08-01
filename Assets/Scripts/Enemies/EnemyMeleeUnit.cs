using System;
using System.Collections;
using System.Collections.Generic;
using PlayerCharacter.Interfaces;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using Renderer = Maze.Renderer;
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
    [SerializeField] private bool playerInSightRange, playerInAttackRange;

    //Variables for testing
    private Vector3 distanceToWalkPoint;
    private float distance;
    // player = GameObject.Find("PlayerCharacter").transform;
    public float maxHealth { get; set; } = 100;
    [field: SerializeField] public float currentHealth { get; set; }
    private Renderer _maze;

    private Animator _animator;

    private void Start() {
        _maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<Renderer>();
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        //Check for player in sight and attack range
        // foreach (GameObject t in GameObject.FindGameObjectsWithTag("Player")) {
        //     if (Vector3.Distance())
        // }
        
        player = Physics.OverlapSphere(transform.position, sightRange, playerLayer)?[0]?.transform;
        if (player != null) {
            playerInAttackRange = Vector3.Distance(transform.position, player.position) < attackRange;
            playerInSightRange = true;
        }
        else {
            playerInAttackRange = false;
            playerInSightRange = false;
        }
        Debug.Log(walkPoint);
        if (!playerInSightRange && !playerInAttackRange)
        {
            _animator.SetBool("Attack", false);
            // walkPointSet = false;
            Debug.Log("here1");
            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange && player != null)
        {
            _animator.SetBool("Attack", false);
            walkPointSet = true;
            Debug.Log("here2");
            ChasePlayer();
        }
        if (playerInAttackRange && player != null)
        {
            _animator.SetBool("Attack", true);
            walkPointSet = true;
            Debug.Log("here3");
            AttackPlayer();
        }
        
    }

    private void Patrolling() {

        if (Vector3.Distance(transform.position, walkPoint) < .5f) {
            walkPoint = _maze.GetNodeCenter(Random.Range(0, _maze.Width), Random.Range(0, _maze.Height));
            walkPointSet = false;
        }
        else {
            walkPointSet = true;
        }

        if (!walkPointSet) {
            agent.SetDestination(walkPoint);
        }
    }

    private void ChasePlayer() {
        // agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make the enemy stop before attacking
        // agent.SetDestination(transform.position);
        // agent.isStopped = true;
        transform.LookAt(player);
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
