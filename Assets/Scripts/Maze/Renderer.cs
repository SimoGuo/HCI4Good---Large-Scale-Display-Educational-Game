using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Renderer : MonoBehaviour {
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    private float horizontal;
    private float vertical;
    [SerializeField] private Transform wall;
    [SerializeField] private Transform center;

    public NavMeshSurface surface;
    
    void Render(Generator.NodeState[,] maze) {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Generator.NodeState node = maze[i, j];

                string name = i.ToString() + j.ToString();
                horizontal = wall.localScale.x * Mathf.Cos(30 * Mathf.Deg2Rad);
                vertical = wall.localScale.x + (wall.localScale.x * Mathf.Sin(30 * Mathf.Deg2Rad));
                
                Vector3 pos = new Vector3((i - width / 2) * horizontal * 2, transform.position.y, (j - height / 2) * vertical); // center of node
                
                if (j % 2 == 0) {
                    pos -= new Vector3(horizontal, 0, 0);
                }
                
                Instantiate(center, transform);
                center.position = pos;
                center.name = name;

                if (node.HasFlag(Generator.NodeState.UpLeft)) {
                    Transform upLeft = Instantiate(wall, transform);
                    upLeft.eulerAngles = new Vector3(0, -30, 0);
                    upLeft.position = pos + new Vector3(-horizontal / 2, 0, vertical / 2);
                    upLeft.name = "upLeft" + name;
                    
                }
                // //
                if (node.HasFlag(Generator.NodeState.Left)) {
                    Transform left = Instantiate(wall, transform);
                    left.eulerAngles = new Vector3(0, 90, 0);
                    left.position = pos + new Vector3(-horizontal, 0, 0);
                    left.name = "left" + name;
                }
                // //
                if (node.HasFlag(Generator.NodeState.DownLeft)) {
                    Transform downLeft = Instantiate(wall, transform);
                    downLeft.eulerAngles = new Vector3(0, 30, 0);
                    downLeft.position = pos + new Vector3(-horizontal / 2, 0, -vertical / 2);
                    downLeft.name = "downLeft" + name;
                }


                if (j == 0) {
                    if (node.HasFlag(Generator.NodeState.DownRight)) {
                        Transform downRight = Instantiate(wall, transform);
                        downRight.eulerAngles = new Vector3(0, -30, 0);
                        downRight.position = pos + new Vector3(horizontal / 2, 0, -vertical / 2);
                        downRight.name = "downRight" + name;
                    }
                }

                if (i == width - 1) {
                    if (node.HasFlag(Generator.NodeState.Right)) {
                        Transform left = Instantiate(wall, transform);
                        left.eulerAngles = new Vector3(0, 90, 0);
                        left.position = pos + new Vector3(horizontal, 0, 0);
                        left.name = "right" + name;
                    }

                    if (j != height - 1 && i == width - 1) {
                        if (node.HasFlag(Generator.NodeState.UpRight)) {
                            Transform upRight = Instantiate(wall, transform);
                            upRight.eulerAngles = new Vector3(0, 30, 0);
                            upRight.position = pos + new Vector3(horizontal / 2, 0, vertical / 2);
                            upRight.name = "upRight" + name;
                        }
                                            
                        if (node.HasFlag(Generator.NodeState.DownRight)) {
                            Transform downRight = Instantiate(wall, transform);
                            downRight.eulerAngles = new Vector3(0, -30, 0);
                            downRight.position = pos + new Vector3(horizontal / 2, 0, -vertical / 2);
                            downRight.name = "downRight" + name;
                        }
                    }
                }

                

                if (j == height - 1) {
                    if (node.HasFlag(Generator.NodeState.UpRight)) {
                        Transform upRight = Instantiate(wall, transform);
                        upRight.eulerAngles = new Vector3(0, 30, 0);
                        upRight.position = pos + new Vector3(horizontal / 2, 0, vertical / 2);
                        upRight.name = "upRight" + name;
                    }

                    if (i == width - 1) {
                        if (node.HasFlag(Generator.NodeState.DownRight)) {
                            Transform downRight = Instantiate(wall, transform);
                            downRight.eulerAngles = new Vector3(0, -30, 0);
                            downRight.position = pos + new Vector3(horizontal / 2, 0, -vertical / 2);
                            downRight.name = "downRight" + name;
                        } 
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

            surface.BuildNavMesh();
        }
        
    }
}
