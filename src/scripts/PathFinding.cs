using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class PathFinding {
    private Tile[,] _board;
    private readonly Vector2I[] _directions = {
        new Vector2I(1, -1),
        new Vector2I(1, 0),
        new Vector2I(1, 1),
        new Vector2I(0, 1),
        new Vector2I(-1, 0),
        new Vector2I(0, -1)
    };

    public PathFinding(Tile[,] board) {
        _board = board;
    }

    // Return a List of Tile including both the start and end tile, going from start to end
    public Tile[] FindPath(Tile start, Tile end) {
        PriorityQueue<Tile, int> queue = new PriorityQueue<Tile, int>();
        queue.Enqueue(start, GetManhattanDistance(start, end));

        Dictionary<Tile, int> lowestPathCostTo = new Dictionary<Tile, int>();
        lowestPathCostTo.Add(start, 0);

        // Key - tile we want to reach
        // Value - tile used to reach the key tile with the lowest path cost
        Dictionary<Tile, Tile> optimalTileFrom = new Dictionary<Tile, Tile>();
        optimalTileFrom.Add(start, start);

        while (queue.Count > 0) {
            Tile current = queue.Dequeue();

            if (current.Equals(end)) {
                LinkedList<Tile> path = new LinkedList<Tile>();
                while (current != start) {
                    path.AddFirst(current);
                    current = optimalTileFrom.GetValueOrDefault(current, start);
                }
                path.AddFirst(start);
                return path.ToArray();
            }

            List<Tile> neighbors = FindNeighbors(current);
            int pathCost = lowestPathCostTo.GetValueOrDefault(current, 0) + 1;
            foreach (Tile neighbor in neighbors) {
                if (!lowestPathCostTo.ContainsKey(neighbor) ||
                        pathCost < lowestPathCostTo.GetValueOrDefault(neighbor, 0)) {
                    lowestPathCostTo.Add(neighbor, pathCost);
                    optimalTileFrom.Add(neighbor, current);
                    queue.Enqueue(neighbor, pathCost + GetManhattanDistance(neighbor, end));
                }
            }
        }
        return null;
    }

    // Can optimize by precomputing all neighbors for a tile ahead of time
    // Optimize if pathfinding performance becomes an issue
    private List<Tile> FindNeighbors(Tile current) {
        List<Tile> neighbors = new List<Tile>();
        foreach (Vector2I direction in _directions) {
            Vector2I coordinates = current.GetCoordinates() + direction;
            if (IsValidCoordinates(coordinates)) {
                neighbors.Add(_board[coordinates.X, coordinates.Y]);
            }
        }
        return neighbors;
    }

    private bool IsValidCoordinates(Vector2I coordinates) {
        return coordinates.X >= 0 && coordinates.X < _board.GetLength(0) &&
            coordinates.Y >= 0 && coordinates.Y < _board.GetLength(1);
    }

    private int GetManhattanDistance(Tile start, Tile end) {
        Vector2I difference = start.GetCoordinates() - end.GetCoordinates();
        return Math.Abs(difference.X) + Math.Abs(difference.Y);
    }

}