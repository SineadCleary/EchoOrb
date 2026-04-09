using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostWall : MonoBehaviour
{
    TilemapCollider2D myCollider; 
    Tilemap tilemap;
    public bool visible;
    Color c_invisible;
    Color c_normal;
    MyGrid<PathNode> grid;
    BoundsInt bounds;

    private void Start()
    {
        myCollider = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<Tilemap>();
        grid = Pathfinding.Instance.GetGrid();
        c_normal = tilemap.color;
        c_invisible = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0.5f);
        bounds = tilemap.cellBounds;

        SetVisible(visible);
    }

    public void SetVisible(bool visible)
    {
        this.visible = visible;
        myCollider.enabled = visible;
        tilemap.color = visible ? c_normal : c_invisible;

        // For each tile in tilemap
        foreach (Vector3Int cellPos in bounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(cellPos)) continue;

            Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);

            grid.GetGridPosition(worldPos, out int x, out int y);
            PathNode node = grid.GetGridObject(x, y);

            if (node != null)
            {
                node.isWalkable = !visible; // if visible - not walkable
            }
        }
    }
}
