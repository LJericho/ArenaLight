using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float speed = 3f;
    public Pathfinder pathfinder;
    public GridManager grid;

    public void MoveTo(Vector3 targetWorld)
    {
        Node start = grid.GetNodeFromWorld(transform.position);
        Node end = grid.GetNodeFromWorld(targetWorld);

        if (start == null || end == null) return;

        List<Node> path = pathfinder.FindPath(start, end);
        if (path != null)
            StartCoroutine(Move(path));
    }

    IEnumerator Move(List<Node> path)
    {
        foreach (Node node in path)
        {
            Vector3 target = grid.CellToWorld(node.x, node.y);
            while (Vector3.Distance(transform.position, target) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    speed * Time.deltaTime
                );
                yield return null;
            }
        }
    }
}