using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "SO_Database", menuName = "Scriptable Objects/Database")]
public class SO_Database : ScriptableObject
{
    public SO_Placeable[] placeables;
    private Dictionary<int, SO_Placeable> dictionary;

    private void OnEnable()
    {
        dictionary = new Dictionary<int, SO_Placeable>();

        foreach (var placeable in placeables)
        {
            if (dictionary.ContainsKey(placeable.id))
            {
                Debug.LogError("Duplicate ID found in database: " + placeable.id);
                continue;
            }

            dictionary.Add(placeable.id, placeable);
        }
    }

    public GameObject GetEditorPrefab(int id)
    {
        // TryGetValue(): checks for id: returns true if found, false if not found
        // out: method assigns value 
        if (dictionary.TryGetValue(id, out var item)) 
        {
            return item.editorPrefab;
        }
        
        Debug.LogError("Dictionary missing id: " + id);
        return null;
    }

    public GameObject GetGamePrefab(int id) // items
    {
        if (dictionary.TryGetValue(id, out var item) && item.gamePrefab != null)
        {
            return item.gamePrefab;
        }

        Debug.LogError("No GamePrefab for id: " + id);
        return null;
    }

    public TileBase GetTile(int id) // tiles
    {
        if (dictionary.TryGetValue(id, out var item) && item.tile != null)
        {
            return item.tile;
        }

        Debug.LogError("No Tile for id: " + id);
        return null;
    }

    public MyTilemap GetTilemap(int id) // tiles
    {
        if (dictionary.TryGetValue(id, out var item))
        {
            return item.tilemap;
        }

        Debug.LogError("ID: " + id + " not found.");
        return MyTilemap.None;
    }
}
