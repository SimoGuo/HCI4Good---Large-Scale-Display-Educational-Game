 using System;
 using System.Collections;
using System.Collections.Generic;
 using UnityEditor;

 // this is just a regular c# class no monobehaviour
public static class Generator {
    [Flags]
    public enum NodeState : byte {
        Left      = 1 << 0, // 00000001
        UpLeft    = 1 << 1, // 00000010
        UpRight   = 1 << 2, // 00000100
        Right     = 1 << 3, // 00001000
        DownLeft  = 1 << 4, // 00010000
        DownRight = 1 << 5, // 00100000
        Visited   = 1 << 7  // 10000000
    }

    public struct Position {
        public int x;
        public int y;
    }

    public struct Neighbour {
        public Position Position;
        public NodeState SharedWall;
    }

    private static NodeState GetOppositeWall(NodeState node) {
        switch (node) {
            case NodeState.Right: return NodeState.Left;            
            case NodeState.Left: return NodeState.Right;            
            case NodeState.UpLeft: return NodeState.DownLeft;            
            case NodeState.DownLeft: return NodeState.UpLeft;
            default: return NodeState.UpLeft;
        }
    }

    private static NodeState[,] RecursiveBacktracker(NodeState[,] maze, int width, int height) {

        Random rng = new Random();
        Stack<Position> positions = new Stack<Position>();
        
        // random start
        // Position pos = new Position { x = rng.Next(0, width), y = rng.Next(0, height) };
        Position pos = new Position { x = 0, y = 0 };
        maze[pos.x, pos.y] |= NodeState.Visited;
        positions.Push(pos);

        while (positions.Count > 0) {
            Position current = positions.Pop();

            List<Neighbour> neighbours = GetUnvisitedNeighbours(current, maze, width, height);

            if (neighbours.Count > 0) {
                // push because this node still has unvisited neighbours
                positions.Push(current);
                Neighbour randNeighbour = neighbours[rng.Next(0, neighbours.Count)];
                
                // remove walls
                maze[current.x, current.y] &= ~randNeighbour.SharedWall;
                maze[randNeighbour.Position.x, randNeighbour.Position.y] &= ~GetOppositeWall(randNeighbour.SharedWall);

                maze[randNeighbour.Position.x, randNeighbour.Position.y] |= NodeState.Visited;
                
                positions.Push(randNeighbour.Position);
            }
        }
        
        
        
        return maze;
    }

    private static List<Neighbour> GetUnvisitedNeighbours(Position p, NodeState[,] maze, int width, int height) {
        List<Neighbour> list = new List<Neighbour>();
        // if left wall unvisited
        if (p.x > 0) {
            if (!maze[p.x - 1, p.y].HasFlag(NodeState.Visited)) {
                list.Add(new Neighbour {
                    Position = new Position { x = p.x - 1, y = p.y },
                    SharedWall = NodeState.Left
                });
            }
        }
        
        if (p.y > 0) {
            if (!maze[p.x, p.y - 1].HasFlag(NodeState.Visited)) {
                list.Add(new Neighbour {
                    Position = new Position { x = p.x, y = p.y - 1 },
                    SharedWall = NodeState.DownLeft
                });
            }
        }
        
        if (p.x < width - 1) {
            if (!maze[p.x + 1, p.y].HasFlag(NodeState.Visited)) {
                list.Add(new Neighbour {
                    Position = new Position { x = p.x + 1, y = p.y },
                    SharedWall = NodeState.Right
                });
            }
        }
        
        if (p.y < height - 1) {
            if (!maze[p.x, p.y + 1].HasFlag(NodeState.Visited)) {
                list.Add(new Neighbour {
                    Position = new Position { x = p.x, y = p.y + 1 },
                    SharedWall = NodeState.UpLeft 
                });
            }
        }
        
        return list;
    }
    
    
    public static NodeState[,] Generate(int width, int height) {
        NodeState[,] maze = new NodeState[width, height];
        NodeState start = NodeState.DownLeft | NodeState.Left | NodeState.Right | NodeState.UpLeft | NodeState.DownRight | NodeState.UpRight;

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                maze[i, j] = start;
            }
        }

        // return RecursiveBacktracker(maze, width, height);
        return maze;
    }
    
}
