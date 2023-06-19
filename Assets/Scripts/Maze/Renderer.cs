using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Renderer : MonoBehaviour {
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    private float horizontal;
    private float vertical;
    [SerializeField] private Transform wall;
    [SerializeField] private Transform center;

    private Transform current;
    
    IEnumerator Render() {
        
        foreach (Generator.NodeState[,] maze in Generator.Generate(width, height)) {
            foreach (Transform t in transform) {
                if (t.name != "current") {
                    Destroy(t.gameObject);
                }
            }
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    Generator.NodeState node = maze[i, j];

                    string name = i.ToString() + j.ToString();
                    horizontal = wall.localScale.x * Mathf.Cos(30 * Mathf.Deg2Rad);
                    vertical = wall.localScale.x + (wall.localScale.x * Mathf.Sin(30 * Mathf.Deg2Rad));

                    Vector3 pos = new Vector3((i - width / 2) * horizontal * 2, 0,
                        (j - height / 2) * vertical); // center of node

                    if (j % 2 == 0) {
                        pos -= new Vector3(horizontal, 0, 0);
                    }

                    if (node.HasFlag(Generator.NodeState.Current)) {
                        current.position = pos;
                    }
                    

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


                    // if (i == width - 1) {
                    //     if (node.HasFlag(Generator.NodeState.Right)) {
                    //         Transform right = Instantiate(wall, transform);
                    //         right.eulerAngles = new Vector3(0, 90, 0);
                    //         right.position = pos + new Vector3(wall.localScale.x / 2, 0, 0);
                    //     }
                    // }
                    //
                    // if (j == 0) {
                    //     if (node.HasFlag(Generator.NodeState.DownLeft)) {
                    //         Transform down = Instantiate(wall, transform);
                    //         down.position = pos + new Vector3(0, 0, -wall.localScale.x / 2);
                    //     }
                    // }
                }
            }

            yield return new WaitForSeconds(.05f);
        }
    }
    
    private void Start() {
        current = Instantiate(center);
        current.name = "current";
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
