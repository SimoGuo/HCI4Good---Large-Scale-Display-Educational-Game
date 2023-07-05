using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine.AI;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rendererv2 : MonoBehaviour {
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    private float horizontal;
    private float vertical;
    [SerializeField] private Transform wall;
    [SerializeField] private Transform center;
    private Transform current;
    private Transform neighbour;
    public NavMeshSurface surface;
    
    IEnumerator Render() {
        foreach (Generatorv2.NodeState[,] maze in Generatorv2.Generate(width, height)) {
            foreach (Transform t in transform) {
                if (t.name != "current") {
                    Destroy(t.gameObject);
                }
            }

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    Generatorv2.NodeState node = maze[i, j];

                    string name = i.ToString() + j.ToString();
                    horizontal = wall.localScale.x * Mathf.Cos(30 * Mathf.Deg2Rad);
                    vertical = wall.localScale.x + (wall.localScale.x * Mathf.Sin(30 * Mathf.Deg2Rad));

                    Vector3 pos = new Vector3((i - width / 2) * horizontal * 2, transform.position.y,
                        (j - height / 2) * vertical); // center of node

                    if (j % 2 == 0) {
                        pos -= new Vector3(horizontal, 0, 0);
                    }

                    if (node.HasFlag(Generatorv2.NodeState.Current)) {
                        current.position = pos;
                    }
                    if (node.HasFlag(Generatorv2.NodeState.Neighbour)) {
                        neighbour.position = pos;
                    }

                    if (node.HasFlag(Generatorv2.NodeState.UpLeft)) {
                        Transform upLeft = Instantiate(wall, transform);

                        upLeft.eulerAngles = new Vector3(0, -30, 0);
                        upLeft.position = pos + new Vector3(-horizontal / 2, 0, vertical / 2);
                        upLeft.name = "upLeft" + name;

                    }

                    // //
                    if (node.HasFlag(Generatorv2.NodeState.Left)) {
                        Transform left = Instantiate(wall, transform);

                        left.eulerAngles = new Vector3(0, 90, 0);
                        left.position = pos + new Vector3(-horizontal, 0, 0);
                        left.name = "left" + name;
                    }

                    // //
                    if (node.HasFlag(Generatorv2.NodeState.DownLeft)) {
                        Transform downLeft = Instantiate(wall, transform);

                        downLeft.eulerAngles = new Vector3(0, 30, 0);
                        downLeft.position = pos + new Vector3(-horizontal / 2, 0, -vertical / 2);
                        downLeft.name = "downLeft" + name;
                    }


                    if (j == 0) {
                        if (node.HasFlag(Generatorv2.NodeState.DownRight)) {
                            Transform downRight = Instantiate(wall, transform);

                            downRight.eulerAngles = new Vector3(0, -30, 0);
                            downRight.position = pos + new Vector3(horizontal / 2, 0, -vertical / 2);
                            downRight.name = "downRight" + name;
                        }
                    }

                    if (i == width - 1) {
                        if (node.HasFlag(Generatorv2.NodeState.Right)) {
                            Transform left = Instantiate(wall, transform);


                            left.eulerAngles = new Vector3(0, 90, 0);
                            left.position = pos + new Vector3(horizontal, 0, 0);
                            left.name = "right" + name;
                        }

                        if (j != height - 1 && i == width - 1) {
                            if (node.HasFlag(Generatorv2.NodeState.UpRight)) {
                                Transform upRight = Instantiate(wall, transform);


                                upRight.eulerAngles = new Vector3(0, 30, 0);
                                upRight.position = pos + new Vector3(horizontal / 2, 0, vertical / 2);
                                upRight.name = "upRight" + name;
                            }

                            if (node.HasFlag(Generatorv2.NodeState.DownRight)) {
                                Transform downRight = Instantiate(wall, transform);


                                downRight.eulerAngles = new Vector3(0, -30, 0);
                                downRight.position = pos + new Vector3(horizontal / 2, 0, -vertical / 2);
                                downRight.name = "downRight" + name;
                            }
                        }
                    }



                    if (j == height - 1) {
                        if (node.HasFlag(Generatorv2.NodeState.UpRight)) {
                            Transform upRight = Instantiate(wall, transform);


                            upRight.eulerAngles = new Vector3(0, 30, 0);
                            upRight.position = pos + new Vector3(horizontal / 2, 0, vertical / 2);
                            upRight.name = "upRight" + name;
                        }

                        if (i == width - 1) {
                            if (node.HasFlag(Generatorv2.NodeState.DownRight)) {
                                Transform downRight = Instantiate(wall, transform);


                                downRight.eulerAngles = new Vector3(0, -30, 0);
                                downRight.position = pos + new Vector3(horizontal / 2, 0, -vertical / 2);
                                downRight.name = "downRight" + name;
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(.1f);
        }
    }
    
    private void Start() {
        surface = GetComponent<NavMeshSurface>();
        current = Instantiate(center);
        neighbour = Instantiate(center);
        current.name = "current";
        neighbour.name = "current";
        neighbour.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StopCoroutine(Render());
            foreach (Transform t in transform) {
                Destroy(t.gameObject);
            }
            StartCoroutine(Render());
        }

    }
}
