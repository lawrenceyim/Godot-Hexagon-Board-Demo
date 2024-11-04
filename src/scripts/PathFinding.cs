using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class PathFinding {
    private TileData[,] _board;
    private readonly (int, int)[] _directions = {
        (1, -1),
        (1, 0),
        (1, 1),
        (0, 1),
        (-1, 0),
        (0, -1)
    };

    public PathFinding(TileData[,] board) {
        _board = board;
    }

    // Return a List of TileData including both the start and end TileData, going from start to end
    public TileData[] FindPath(TileData start, TileData end) {
        PriorityQueue<TileData, int> queue = new PriorityQueue<TileData, int>();
        queue.Enqueue(start, GetManhattanDistance(start.GetCoordinates(), end.GetCoordinates()));

        Dictionary<TileData, int> lowestPathCostTo = new Dictionary<TileData, int>();
        lowestPathCostTo.Add(start, 0);

        // Key - TileData we want to reach
        // Value - TileData used to reach the key TileData with the lowest path cost
        Dictionary<TileData, TileData> optimalTileDataFrom = new Dictionary<TileData, TileData>();
        optimalTileDataFrom.Add(start, start);

        while (queue.Count > 0) {
            TileData current = queue.Dequeue();

            if (current.Equals(end)) {
                LinkedList<TileData> path = new LinkedList<TileData>();
                while (current != start) {
                    path.AddFirst(current);
                    current = optimalTileDataFrom.GetValueOrDefault(current, start);
                }
                path.AddFirst(start);
                return path.ToArray();
            }

            List<TileData> neighbors = FindNeighbors(current);
            int pathCost = lowestPathCostTo.GetValueOrDefault(current, 0) + 1;
            foreach (TileData neighbor in neighbors) {
                if (!lowestPathCostTo.ContainsKey(neighbor) ||
                        pathCost < lowestPathCostTo.GetValueOrDefault(neighbor, 0)) {
                    lowestPathCostTo.Add(neighbor, pathCost);
                    optimalTileDataFrom.Add(neighbor, current);
                    queue.Enqueue(neighbor, pathCost + GetManhattanDistance(neighbor.GetCoordinates(), end.GetCoordinates()));
                }
            }
        }
        return null;
    }

    // Can optimize by precomputing all neighbors for a TileData ahead of time
    // Optimize if pathfinding performance becomes an issue
    private List<TileData> FindNeighbors(TileData current) {
        List<TileData> neighbors = new List<TileData>();
        foreach ((int, int) direction in _directions) {
            (int, int) coordinates = current.GetCoordinates();
            coordinates.Item1 += direction.Item1;
            coordinates.Item2 += direction.Item2;
            if (IsValidCoordinates(coordinates)) {
                neighbors.Add(_board[coordinates.Item1, coordinates.Item2]);
            }
        }
        return neighbors;
    }

    private bool IsValidCoordinates((int, int) coordinates) {
        return coordinates.Item1 >= 0 && coordinates.Item1 < _board.GetLength(0) &&
            coordinates.Item2 >= 0 && coordinates.Item2 < _board.GetLength(1);
    }

    private int GetManhattanDistance((int, int) start, (int, int) end) {
        return Math.Abs(start.Item1 - end.Item1) + Math.Abs(start.Item2 + end.Item2);
    }
}