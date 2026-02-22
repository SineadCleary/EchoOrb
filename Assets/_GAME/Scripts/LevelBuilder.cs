using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] SO_Database database;
    [Header("Tilemaps")]
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap wallTilemap;
    [SerializeField] Tilemap greenWallATilemap;
    [SerializeField] Tilemap greenWallBTilemap;
    [SerializeField] Tilemap purpleWallATilemap;
    [SerializeField] Tilemap purpleWallBTilemap;

    private Dictionary<MyTilemap, Tilemap> tilemapDictionary;

    void Awake()
    {
        tilemapDictionary = new Dictionary<MyTilemap, Tilemap>
        {
            { MyTilemap.Wall, wallTilemap },
            { MyTilemap.GreenWall_A, greenWallATilemap },
            { MyTilemap.GreenWall_B, greenWallBTilemap },
            { MyTilemap.PurpleWall_A, purpleWallATilemap },
            { MyTilemap.PurpleWall_B, purpleWallBTilemap }
        };
    }

    void Start()
    {
        if (LevelLoader.currentLevel == null) return;
        Build(LevelLoader.currentLevel);
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
            
            MyTilemap myTilemap = database.GetTilemap(tile.tileID);
            if (!tilemapDictionary.TryGetValue(myTilemap, out Tilemap tilemap))
            {
                Debug.LogError("No Tilemap assigned for id: " + tile.tileID);
                continue;
            }

            Vector3 worldPos = new Vector3(tile.x, tile.y, 0);
            if (tilemap == null) Debug.Log(tileBase.name);
            Vector3Int cellPos = tilemap.WorldToCell(worldPos);

            tilemap.SetTile(cellPos, tileBase);
        }
    }
}

public static class LevelLoader
{
    public static LevelData currentLevel;
}
