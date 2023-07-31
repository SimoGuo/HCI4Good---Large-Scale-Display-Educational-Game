using System;
using Unity.AI.Navigation;
using UnityEngine;

namespace Maze {
    public class Renderer : MonoBehaviour {
        [field: SerializeField] public int Width { private set; get; } = 10;
        [field: SerializeField] public int Height { private set; get; } = 10;
        private float horizontal;
        private float vertical;
        [SerializeField] private Transform wall;
        [SerializeField] private float wallGap;
        [SerializeField] private Transform center;

        public Vector3 GetNodeCenter(int i, int j) {
            if (i >= Width || j >= Height || i < 0 || j < 0) return Vector3.zero;
        
            horizontal = (wallGap + wall.localScale.x) * Mathf.Cos(30 * Mathf.Deg2Rad);
            vertical = (wallGap + wall.localScale.x) + ((wallGap + wall.localScale.x) * Mathf.Sin(30 * Mathf.Deg2Rad));
                
            Vector3 pos = new Vector3((i - Width / 2) * horizontal * 2, transform.position.y, (j - Height / 2) * vertical); // center of node
                
            if (j % 2 == 0) {
                pos -= new Vector3(horizontal, 0, 0);
            }

            return pos;
        }
    
        void Render(Generator.NodeState[,] maze) {
            for (int i = 0; i < Width; i++) {
                for (int j = 0; j < Height; j++) {
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

                    if (i == Width - 1) {
                        if (node.HasFlag(Generator.NodeState.Right)) {
                            Transform left = Instantiate(wall, transform);
                        

                            left.eulerAngles = new Vector3(left.transform.rotation.eulerAngles.x, 90, 0);
                            left.position = pos + new Vector3(horizontal, 0, 0);
                            left.name = "right" + name;
                        }

                        if (j != Height - 1 && i == Width - 1) {
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

                

                    if (j == Height - 1) {
                        if (node.HasFlag(Generator.NodeState.UpRight)) {
                            Transform upRight = Instantiate(wall, transform);
                        

                            upRight.eulerAngles = new Vector3(upRight.transform.rotation.eulerAngles.x, 30, 0);
                            upRight.position = pos + new Vector3(horizontal / 2, 0, vertical / 2);
                            upRight.name = "upRight" + name;
                        }

                        if (i == Width - 1) {
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
            // foreach (Transform t in transform) {
            //     Destroy(t.gameObject);
            // }
            //
            // Render(Generator.Generate(Width, Height));
            // surface.BuildNavMesh();
        }

        public void Build() {
            foreach (Transform t in transform) {
                Destroy(t.gameObject);
            }

            Render(Generator.Generate(Width, Height));
        }
    }
}
