using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    // Width and height must be odd numbers
    public static int width = 61;
    public static int height = 41;
    public static Vector3 origin = new Vector3(-(width/2f), -(height/2f));

    [SerializeField] private List<Tilemap> nonWalkableTilemaps;

    private void Awake()
    {
        new Pathfinding(width, height);
    }

    public void SetupGrid()
    {
        MyGrid<PathNode> grid = Pathfinding.Instance.GetGrid();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode node = grid.GetGridObject(x, y);

                Vector3 worldPos = grid.GetWorldPosition(x, y);
                Vector3Int cell = nonWalkableTilemaps[0].WorldToCell(worldPos);

                bool walkable = true;
                foreach (var tilemap in nonWalkableTilemaps)
                {
                    if (tilemap.HasTile(cell))
                    {
                        walkable = false;
                        break;
                    }
                }

                node.isWalkable = walkable;
            }
        }
    }
}