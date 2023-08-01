using System;
using System.Collections.Generic;
using UnityEngine;
using Renderer = Maze.Renderer;
using Random = UnityEngine.Random;

namespace Enemies {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private Renderer maze;
        [SerializeField] private Transform enemy;
        [SerializeField] private int noOfEnemies;

        public List<Transform> Spawn() {
            List<Transform> enemies = new List<Transform>();
            for (int i = 0; i < noOfEnemies; i++) {
                Transform e = Instantiate(enemy,
                    maze.GetNodeCenter(Random.Range(maze.Width / 4, 3 * (maze.Width / 4)), Random.Range(0, maze.Height)) + Vector3.up,
                    Quaternion.identity);
                UnityEngine.Renderer[] renderers = e.GetComponentsInChildren<UnityEngine.Renderer>();
                foreach (UnityEngine.Renderer renderer in renderers) {
                    foreach (Material mat in renderer.materials) {
                        mat.color = Random.ColorHSV();
                    }
                }
                enemies.Add(e);
            }

            return enemies;
        }
    }
}