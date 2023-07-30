using System;
using Unity.AI.Navigation;
using UnityEngine;

namespace Maze {
    public class Renderer : MonoBehaviour {
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 10;
        private float horizontal;
        private float vertical;
        [SerializeField] private Transform wall;
        [SerializeField] private float wallGap;
        [SerializeField] private Transform center;

        public NavMeshSurface surface;

        public Vector3 GetNodeCenter(int i, int j) {
            if (i >= width || j >= height || i < 0 || j < 0) return Vector3.zero;
        
            horizontal = (wallGap + wall.localScale.x) * Mathf.Cos(30 * Mathf.Deg2Rad);
            vertical = (wallGap + wall.localScale.x) + ((wallGap + wall.localScale.x) * Mathf.Sin(30 * Mathf.Deg2Rad));
                
            Vector3 pos = new Vector3((i - width / 2) * horizontal * 2, transform.position.y, (j - height / 2) * vertical); // center of node
                
            if (j % 2 == 0) {
                pos -= new Vector3(horizontal, 0, 0);
            }

            return pos;
        }
    
        void Render(Generator.NodeState[,] maze) {
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    Generator.NodeState node = maze[i, j];

                    string name = i.ToString() + j.ToString();
                    Vector3 pos = GetNodeCenter(i, j);                

                    if (node.HasFlag(Generator.NodeState.UpLeft)) {
                        Transform upLeft = Instantiate(wall, transform);
                    
                        upLeft.eulerAngles = new Vector3(upLeft.transform.rotation.eulerAngles.x, -30, 0);
                        upLeft.position = pos + new Vector3(-horizontal / 2, 0, vertical / 2);
                        upLeft.name = "upLeft" + name;
                    
                    }
                    // //
                    if (node.HasFlag(Generator.NodeState.Left)) {
                        Transform left = Instantiate(wall, transform);
                    
                        left.eulerAngles = new Vector3(left.transform.rotation.eulerAngles.x, 90, 0);
                        left.position = pos + new Vector3(-horizontal, 0, 0);
                        left.name = "left" + name;
                    }
                    // //
                    if (node.HasFlag(Generator.NodeState.DownLeft)) {
                        Transform downLeft = Instantiate(wall, transform);
                    
                        downLeft.eulerAngles = new Vector3(downLeft.transform.rotation.eulerAngles.x, 30, 0);
                        downLeft.position = pos + new Vector3(-horizontal / 2, 0, -vertical / 2);
                        downLeft.name = "downLeft" + name;
                    }


                    if (j == 0) {
                        if (node.HasFlag(Generator.NodeState.DownRight)) {
                            Transform downRight = Instantiate(wall, transform);
                        
                            downRight.eulerAngles = new Vector3(downRight.transform.rotation.eulerAngles.x, -30, 0);
                            downRight.position = pos + new Vector3(horizontal / 2, 0, -vertical / 2);
                            downRight.name = "downRight" + name;
                        }
                    }

                    if (i == width - 1) {
                        if (node.HasFlag(Generator.NodeState.Right)) {
                            Transform left = Instantiate(wall, transform);
                        

                            left.eulerAngles = new Vector3(left.transform.rotation.eulerAngles.x, 90, 0);
                            left.position = pos + new Vector3(horizontal, 0, 0);
                            left.name = "right" + name;
                        }

                        if (j != height - 1 && i == width - 1) {
                            if (node.HasFlag(Generator.NodeState.UpRight)) {
                                Transform upRight = Instantiate(wall, transform);
                            

                                upRight.eulerAngles = new Vector3(upRight.transform.rotation.eulerAngles.x, 30, 0);
                                upRight.position = pos + new Vector3(horizontal / 2, 0, vertical / 2);
                                upRight.name = "upRight" + name;
                            }
                                            
                            if (node.HasFlag(Generator.NodeState.DownRight)) {
                                Transform downRight = Instantiate(wall, transform);
                            

                                downRight.eulerAngles = new Vector3(downRight.transform.rotation.eulerAngles.x, -30, 0);
                                downRight.position = pos + new Vector3(horizontal / 2, 0, -vertical / 2);
                                downRight.name = "downRight" + name;
                            }
                        }
                    }

                

                    if (j == height - 1) {
                        if (node.HasFlag(Generator.NodeState.UpRight)) {
                            Transform upRight = Instantiate(wall, transform);
                        

                            upRight.eulerAngles = new Vector3(upRight.transform.rotation.eulerAngles.x, 30, 0);
                            upRight.position = pos + new Vector3(horizontal / 2, 0, vertical / 2);
                            upRight.name = "upRight" + name;
                        }

                        if (i == width - 1) {
                            if (node.HasFlag(Generator.NodeState.DownRight)) {
                                Transform downRight = Instantiate(wall, transform);
                            

                                downRight.eulerAngles = new Vector3(downRight.transform.rotation.eulerAngles.x, -30, 0);
                                downRight.position = pos + new Vector3(horizontal / 2, 0, -vertical / 2);
                                downRight.name = "downRight" + name;
                            } 
                        }
                    }
                }
            }    
        }

        private void Start() {
            foreach (Transform t in transform) {
                Destroy(t.gameObject);
            }

            Render(Generator.Generate(width, height));
            surface.BuildNavMesh();
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
}
