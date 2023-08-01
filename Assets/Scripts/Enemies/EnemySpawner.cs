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
        [field: SerializeField] public List<Transform> Enemies { private set; get; }

        public void Spawn() {
            for (int i = 0; i < noOfEnemies; i++) {
                Transform e = Instantiate(enemy,
                    maze.GetNodeCenter(Random.Range(0, maze.Width), Random.Range(0, maze.Height)) + Vector3.up,
                    Quaternion.identity);
                e.GetComponentInChildren<UnityEngine.Renderer>().material.color = Random.ColorHSV();
                Enemies.Add(e);
            }
        }
    }
}