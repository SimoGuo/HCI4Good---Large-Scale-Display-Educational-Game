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

public class EnemyMeleeUnit: MonoBehaviour, IDamageable
{
    private NavMeshAgent agent;
    
    //Player position
   // [SerializeField] private Transform player;
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
    
    private void Start() {

        agent = GetComponent<NavMeshAgent>();
        _maze = GameObject.FindGameObjectWithTag("Maze").GetComponent<Renderer>();
        _particles = GetComponentInChildren<ParticleSystem>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
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
    private void Update() {
        // RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        // Vector2 viewport = Camera.main.WorldToScreenPoint(transform.position);
        // // Vector2 canvasNormalized = new Vector2((viewport.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * .5f),
        // //     (viewport.x * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * .5f));
        // _myHealthBar.GetComponent<RectTransform>().anchorMin = viewport;
        // _myHealthBar.GetComponent<RectTransform>().anchorMax = viewport;

        _myHealthBar.GetComponent<RectTransform>().position = transform.position + Vector3.up * 3;
        _myHealthBar.GetComponent<RectTransform>().LookAt(new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z));
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
        _myHealthBar.TakeDamage(amount);
        if (currentHealth <= 0) Kill();
    }

    public void Kill() {
        gameManager.EnemyDied(transform);
        _scoreManager.EnemyDied();
        Destroy(_myHealthBar.transform.gameObject);
        Destroy(gameObject);
    }
}
