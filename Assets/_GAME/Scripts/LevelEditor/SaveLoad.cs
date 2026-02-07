using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    string path;
    [SerializeField] Transform objectGroup;
    //[SerializeField] GameObject temp;
    [SerializeField] ObjectDatabase database;

    private void Awake()
    {
        path = Application.persistentDataPath + "/levelData.json";
    }

    public void Save()
    {
        LevelData levelData = new LevelData();

        foreach (Transform item in objectGroup)
        {
            PlaceableObject placeable = item.GetComponent<PlaceableObject>();

            Vector3 pos = item.position;

            TileData data = new TileData
            {
                //id = database.GetID(item.gameObject),
                id = placeable.id,
                x = pos.x,
                y = pos.y,
            };

            levelData.tiles.Add(data);
        }

        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(path, json);
        Debug.Log("Saved level to: " + path);
    }

    public void Load()
    {
        if (!File.Exists(path))
        {
            Debug.LogError("File " + path + " does not exist");
            return;
        }

        ClearAll();

        string json = File.ReadAllText(path);
        LevelData levelData = JsonUtility.FromJson<LevelData>(json);

        foreach (TileData tile in levelData.tiles)
        {
            GameObject prefab = database.GetPrefab(tile.id);
            Vector3 pos = new Vector3(tile.x, tile.y, 0);
            Instantiate(prefab, pos, Quaternion.identity, objectGroup);
        }

        Debug.Log("File loaded");
    }

    public void ClearAll()
    {
        foreach(Transform item in objectGroup)
        {
            Destroy(item.gameObject);
        }
    }

}
