using System;
using Enemies;
using Unity.AI.Navigation;
using UnityEngine;
using Renderer = Maze.Renderer;

namespace DefaultNamespace {
    public class GameSetup : MonoBehaviour {
        [SerializeField] private Renderer maze;
        [SerializeField] private EnemySpawner spawner;
        [SerializeField] private NavMeshSurface surface;
        private void Start() {
            maze.Build();
            spawner.Spawn();
            surface.BuildNavMesh();
        }
    }
}