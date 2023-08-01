using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;
using Renderer = Maze.Renderer;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        [SerializeField] private Renderer maze;
        [SerializeField] private EnemySpawner spawner;
        [SerializeField] private NavMeshSurface surface;
        [SerializeField] private List<Transform> enemies;
        [SerializeField] private Scene endScene;
        private bool _enemyDied;
        private void Start() {
            maze.Build();
            enemies = spawner.Spawn();
            surface.BuildNavMesh();
        }
        
        public void EnemyDied(Transform enemy) {
            enemies.Remove(enemy);
            if (enemies.Count == 0) {
                SceneManager.LoadScene("End");
            }
        }
    }
}