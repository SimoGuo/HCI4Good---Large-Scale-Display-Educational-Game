using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using PlayerCharacter.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Renderer = Maze.Renderer;
using Random = UnityEngine.Random;

public class EnemyMeleeUnit : MonoBehaviour, IDamageable
{
    private NavMeshAgent agent;

    // Player position
    [SerializeField] private LayerMask groundLayer, playerLayer;

    [SerializeField]
    private GameManager gameManager;
    // Patrolling
    private Vector3 walkPoint;
    private bool walkPointSet;

    // States
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange;

    // Variables for testing
    private float distance;
    [field: SerializeField] public float maxHealth { get; set; } = 100;
    public float currentHealth { get; set; }
    private Renderer _maze;
    private ParticleSystem _particles;
    private Animator _animator;

    [SerializeField] private Transform player;
    [SerializeField] private Transform healthBar;

    [SerializeField]
    private Transform canvas;

    private ScoreManager _scoreManager;
    private healthBar _myHealthBar;

    private void Start()
    {
        // Initialize components and variables
        agent = GetComponent<NavMeshAgent>();
        _maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<Renderer>();
        _particles = GetComponentInChildren<ParticleSystem>();
        //canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        _scoreManager = canvas.GetComponentInChildren<ScoreManager>();
        _myHealthBar = Instantiate(healthBar, canvas).GetComponent<healthBar>();
        _myHealthBar.SetMaxHealth(maxHealth);
        walkPoint = transform.position;
        walkPointSet = true;

        _animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        // Detect nearby players
        Collider[] players = Physics.OverlapSphere(transform.position, sightRange, playerLayer);
        player = players.Length > 0 ? players[0].transform : null;

        if (player != null)
        {
            // Check if the player is within attack range
            playerInAttackRange = Vector3.Distance(transform.position, player.position) < attackRange;
            playerInSightRange = true;
        }
        else
        {
            playerInAttackRange = false;
            playerInSightRange = false;
        }

        if (!playerInSightRange && !playerInAttackRange)
        {
            // If no player is in sight or attack range, patrol
            Patrolling();
        }

        if (playerInSightRange && !playerInAttackRange && player != null)
        {
            // If the player is in sight but not in attack range, chase the player
            walkPointSet = false;
            ChasePlayer();
        }

        if (playerInAttackRange && player != null)
        {
            // If the player is in attack range, attack the player
            walkPointSet = false;
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (agent.remainingDistance < .5f)
        {
            // Generate a new patrol point
            walkPoint = _maze.GetNodeCenter(Random.Range(0, _maze.Width), Random.Range(0, _maze.Height));
            walkPointSet = true;
        }
        else
        {
            walkPointSet = false;
        }

        if (walkPointSet)
        {
            // Set the patrol point as the destination
            agent.SetDestination(walkPoint);
        }
    }

    private void ChasePlayer()
    {
        // Set the player's position as the destination to chase
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Rotate to face the player when in attack range
        transform.LookAt(player);
    }

    public void Damage(float amount)
    {
        // Reduce the enemy's health and trigger damage effects
        currentHealth -= amount;
        _particles.Play();
        _myHealthBar.TakeDamage(amount);

        // Check if the enemy is killed
        if (currentHealth <= 0)
            Kill();
    }

    public void Kill()
    {
        // Handle enemy death, update score, and clean up
        gameManager.EnemyDied(transform);
        _scoreManager.EnemyDied();
        Destroy(_myHealthBar.transform.gameObject);
        Destroy(gameObject);
    }
}
