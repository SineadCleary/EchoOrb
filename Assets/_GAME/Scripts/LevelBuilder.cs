using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] SO_Database database;
    [SerializeField] Tilemap tilemap;

    void Start()
    {
        if (LevelLoader.currentLevel == null) return;
        Build(LevelLoader.currentLevel);
        LevelLoader.currentLevel = null;
    }

    public void Build(LevelData data)
    {
        // Items
        foreach (var obj in data.items)
        {
            GameObject prefab = database.GetGamePrefab(obj.prefabID);
            Vector3 position = new Vector3(obj.x, obj.y, 0);
            Instantiate(prefab, position, Quaternion.identity);
        }

        // Tiles
        foreach (var tile in data.tiles)
        {
            TileBase tileBase = database.GetTile(tile.tileID);
            
            Vector3 worldPos = new Vector3(tile.x, tile.y, 0);
            Vector3Int cellPos = tilemap.WorldToCell(worldPos);

            tilemap.SetTile(cellPos, tileBase);
        }
    }
}

public static class LevelLoader
{
    public static LevelData currentLevel;
}
