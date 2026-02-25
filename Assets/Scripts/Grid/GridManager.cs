using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap groundTilemap;

    public int width;
    public int height;

    public Node[,] grid;

    void Awake()
    {
        grid = new Node[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Node(x, y, true);
            }
    }

    public Vector3 CellToWorld(int x, int y)
    {
        return groundTilemap.CellToWorld(new Vector3Int(x, y, 0))
               + groundTilemap.cellSize / 2;
    }

    public Node GetNodeFromWorld(Vector3 worldPos)
    {
        Vector3Int cell = groundTilemap.WorldToCell(worldPos);

        if (cell.x < 0 || cell.y < 0 ||
            cell.x >= width || cell.y >= height)
            return null;

        return grid[cell.x, cell.y];
    }
}