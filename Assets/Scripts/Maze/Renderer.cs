using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Renderer : MonoBehaviour {
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;

    [SerializeField] private Transform wall;
    
    void Render(Generator.NodeState[,] maze) {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Generator.NodeState node = maze[i, j];
                Vector3 pos = new Vector3((i - width / 2) * wall.localScale.x, 0, (j - height / 2) * wall.localScale.x);


                if (node.HasFlag(Generator.NodeState.Up)) {
                    Transform up = Instantiate(wall, transform);
                    up.position = pos + new Vector3(0, 0, wall.localScale.x / 2);
                    
                }
                
                if (node.HasFlag(Generator.NodeState.Left)) {
                    Transform left = Instantiate(wall, transform);
                    left.eulerAngles = new Vector3(0, 90, 0);
                    left.position = pos + new Vector3(-wall.localScale.x / 2, 0, 0);
                    
                }

                if (i == width - 1) {
                    if (node.HasFlag(Generator.NodeState.Right)) {
                        Transform right = Instantiate(wall, transform);
                        right.eulerAngles = new Vector3(0, 90, 0);
                        right.position = pos + new Vector3(wall.localScale.x / 2, 0, 0);
                    }
                }

                if (j == 0) {
                    if (node.HasFlag(Generator.NodeState.Down)) {
                        Transform down = Instantiate(wall, transform);
                        down.position = pos + new Vector3(0, 0, -wall.localScale.x / 2);
                    }
                }
            }
        }    
    }
    
    private void Start() {
        
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (Transform t in transform) {
                Destroy(t.gameObject);
            }
            Render(Generator.Generate(width, height));
        }
    }
}
