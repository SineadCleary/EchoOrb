using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "SO_Database", menuName = "Scriptable Objects/Database")]
public class SO_Database : ScriptableObject
{
    public SO_Placeable[] items;

    public GameObject GetEditorPrefab(int id)
    {
        foreach (var item in items)
        {
            if (item.id == id) return item.editorPrefab;
        }

        Debug.LogError("Missing object id: " + id);
        return null;
    }

    public GameObject GetGamePrefab(int id)
    {
        foreach (var item in items)
        {
            if (item.id == id && item.gamePrefab != null) return item.gamePrefab;
        }

        Debug.LogError("Missing object id: " + id);
        return null;
    }

    public TileBase GetTile(int id)
    {
        foreach (var tile in items)
        {
            if (tile.id == id && tile.tile != null) return tile.tile;
        }

        Debug.LogError("Missing tile id: " + id);
        return null;
    }
}
