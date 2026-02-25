using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public GridManager grid;

    public List<Node> FindPath(Node start, Node target)
    {
        //  Скидання даних нод перед кожним пошуком
        foreach (Node n in grid.grid)
        {
            n.gCost = int.MaxValue;
            n.hCost = 0;
            n.parent = null;
        }

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        start.gCost = 0;
        start.hCost = Heuristic(start, target);

        openSet.Add(start);

        while (openSet.Count > 0)
        {
            Node current = openSet[0];

            foreach (Node n in openSet)
            {
                if (n.fCost < current.fCost ||
                   (n.fCost == current.fCost && n.hCost < current.hCost))
                {
                    current = n;
                }
            }

            openSet.Remove(current);
            closedSet.Add(current);

            if (current == target)
                return RetracePath(start, target);

            foreach (Node neighbor in GetNeighbors(current))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                int newCost = current.gCost + 10;

                if (newCost < neighbor.gCost)
                {
                    neighbor.gCost = newCost;
                    neighbor.hCost = Heuristic(neighbor, target);
                    neighbor.parent = current;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null;
    }

    List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        Vector2Int[] directions = {
            Vector2Int.right,
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.down
        };

        foreach (Vector2Int dir in directions)
        {
            int x = node.x + dir.x;
            int y = node.y + dir.y;

            if (x >= 0 && y >= 0 &&
                x < grid.width && y < grid.height)
            {
                neighbors.Add(grid.grid[x, y]);
            }
        }

        return neighbors;
    }

    int Heuristic(Node a, Node b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    List<Node> RetracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node current = end;

        while (current != start)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }
}