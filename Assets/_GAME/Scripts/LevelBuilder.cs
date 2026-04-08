using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] SO_Database database;
    [SerializeField] GameObject player;
    [SerializeField] GameManager gameManager;
    [SerializeField] GridManager gridManager;
    [Header("Tilemaps")]
    [SerializeField] GameObject groundTilemap;
    [SerializeField] GameObject wallTilemap;
    [SerializeField] GameObject greenWallATilemap;
    [SerializeField] GameObject greenWallBTilemap;
    [SerializeField] GameObject purpleWallATilemap;
    [SerializeField] GameObject purpleWallBTilemap;
    [SerializeField] GameObject redWallATilemap;
    [SerializeField] GameObject redWallBTilemap;
    [SerializeField] GameObject yellowWallATilemap;
    [SerializeField] GameObject yellowWallBTilemap;

    private Dictionary<MyTilemap, GameObject> tilemapDictionary;
    private List<Cannon> cannons = new List<Cannon>();
    private List<CannonHolder> cannonHolders = new List<CannonHolder>();

    GameObject startPoint;
    Vector3 startPosition;

    void Awake()
    {
        tilemapDictionary = new Dictionary<MyTilemap, GameObject>
        {
            { MyTilemap.Ground, groundTilemap },
            { MyTilemap.Wall, wallTilemap },
            { MyTilemap.GreenWall_A, greenWallATilemap },
            { MyTilemap.GreenWall_B, greenWallBTilemap },
            { MyTilemap.PurpleWall_A, purpleWallATilemap },
            { MyTilemap.PurpleWall_B, purpleWallBTilemap },
            { MyTilemap.RedWall_A, redWallATilemap },
            { MyTilemap.RedWall_B, redWallBTilemap },
            { MyTilemap.YellowWall_A, yellowWallATilemap },
            { MyTilemap.YellowWall_B, yellowWallBTilemap },
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
            GameObject item = Instantiate(prefab, position, Quaternion.identity);
            
            // Holders
            // Ghost wall holders
            if (item.GetComponent<GhostWallHolder>() != null)
            {
                var holder = item.GetComponent<GhostWallHolder>();
                foreach (var type in holder.myTilemaps)
                {
                    if (tilemapDictionary.TryGetValue(type, out GameObject wallmap))
                    {
                        var wall = wallmap.GetComponent<GhostWall>();
                        if (wall != null)
                            holder.walls.Add(wall);
                    }
                }
            }
            // Cannon Holders
            else if (item.GetComponent<CannonHolder>() != null)
            {
                var cannonHolder = item.GetComponent<CannonHolder>();
                cannonHolders.Add(cannonHolder);
            }
            // Cannons
            else if (item.GetComponent<Cannon>() != null)
            {
                var cannon = item.GetComponent<Cannon>();
                cannons.Add(cannon);
            }
        }

        // Set cannon holder cannon lists
        foreach (CannonHolder cannonHolder in cannonHolders)
        {
            cannonHolder.cannons = cannons;
        }

        // Tiles
        foreach (var tile in data.tiles)
        {
            TileBase tileBase = database.GetTile(tile.tileID);
            
            MyTilemap myTilemap = database.GetTilemap(tile.tileID);
            if (!tilemapDictionary.TryGetValue(myTilemap, out GameObject wallmap))
            {
                Debug.LogError("No Tilemap assigned for id: " + tile.tileID);
                continue;
            }

            Vector3 worldPos = new Vector3(tile.x, tile.y, 0);
            if (wallmap == null) Debug.Log(tileBase.name);
            
            Tilemap tilemap = wallmap.GetComponent<Tilemap>();
            Vector3Int cellPos = tilemap.WorldToCell(worldPos);

            tilemap.SetTile(cellPos, tileBase);
        }

        // Setup grid
        gridManager.SetupGrid();

        // Player
        startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        if (startPoint != null) startPosition = startPoint.transform.position;
        else startPosition = Vector3.zero;
        player.transform.position = startPosition;
    }
}

public static class LevelLoader
{
    public static LevelData currentLevel;
}
