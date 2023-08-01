using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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

    [SerializeField]
    private GameManager gameManager;
    //Patrolling
    private Vector3 walkPoint;
    private bool walkPointSet;

    //States
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange;

    //Variables for testing
    private float distance;
    // player = GameObject.Find("PlayerCharacter").transform;
    [field: SerializeField] public float maxHealth { get; set; } = 100;
    public float currentHealth { get; set; }
    private Renderer _maze;
    private ParticleSystem _particles;
    private Animator _animator;

    private void Start() {
        _maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<Renderer>();
        _particles = GetComponentInChildren<ParticleSystem>();
        walkPoint = transform.position;
        walkPointSet = true;
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, sightRange, playerLayer);
        player = players.Length > 0 ? players[0].transform : null;
        if (player != null) {
            playerInAttackRange = Vector3.Distance(transform.position, player.position) < attackRange;
            playerInSightRange = true;
        }
        else {
            playerInAttackRange = false;
            playerInSightRange = false;
            
        }
        if (!playerInSightRange && !playerInAttackRange)
        {
            _animator.SetBool("Attack", false);
            // walkPointSet = false;
            // Debug.Log("here1");
            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange && player != null)
        {
            _animator.SetBool("Attack", false);
            walkPointSet = false;
            // Debug.Log("here2");
            ChasePlayer();
        }
        if (playerInAttackRange && player != null)
        {
            _animator.SetBool("Attack", true);
            walkPointSet = false;
            // Debug.Log("here3");
            AttackPlayer();
        }
        
    }

    private void Patrolling() {

        if (agent.remainingDistance < .5f) {
            walkPoint = _maze.GetNodeCenter(Random.Range(0, _maze.Width), Random.Range(0, _maze.Height));
            walkPointSet = true;
        }
        else {
            walkPointSet = false;
        }

        if (walkPointSet) {
            agent.SetDestination(walkPoint);
        }
    }

    private void ChasePlayer() {
        // agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);
    }

    public void Damage(float amount) {
        currentHealth -= amount;
        _particles.Play();
        if (currentHealth <= 0) Kill();
    }

    public void Kill() {
        gameManager.EnemyDied(transform);
        Destroy(gameObject);
    }
}
