using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [Header("Grid setup")]
    // When we have different level sizes move these to LevelBuilder, and add origin
    [SerializeField] private int width = 58;
    [SerializeField] private int height = 31;
    [Header("Tilemaps")]
    // Ghost wall tilemaps
    [SerializeField] private Tilemap wallsTilemap;


    private void Awake()
    {
        new Pathfinding(width, height);
    }

    //private void Start()
    //{
    //    SetupGrid();
    //}

    public void SetupGrid()
    {
        MyGrid<PathNode> grid = Pathfinding.Instance.GetGrid();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode node = grid.GetGridObject(x, y);

                Vector3 worldPos = grid.GetWorldPosition(x, y);
                Vector3Int cell = wallsTilemap.WorldToCell(worldPos);

                bool hasWall = wallsTilemap.HasTile(cell);

                node.isWalkable = !hasWall;
            }
        }
    }
}