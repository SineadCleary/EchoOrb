using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostWall : MonoBehaviour
{
    TilemapCollider2D myCollider; 
    Tilemap tilemap;
    public bool visible;
    Color c_invisible;
    Color c_normal;

    private void Start()
    {
        myCollider = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<Tilemap>();
        c_normal = tilemap.color;
        c_invisible = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0.5f);

        SetVisible(visible);
    }

    public void SetVisible(bool visible)
    {
        this.visible = visible;
        myCollider.enabled = visible;
        tilemap.color = visible ? c_normal : c_invisible;
    }
}
