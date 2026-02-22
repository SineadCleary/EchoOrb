using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    string path;
    [SerializeField] Transform objectGroup;
    [SerializeField] SO_Database database;

    private void Start()
    {
        if (LevelLoader.currentLevel != null)
        {
            Load(LevelLoader.currentLevel);
            string title = LevelLoader.currentLevel.title;

            string folderPath = Path.Combine(Application.persistentDataPath, "Levels");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            path = Path.Combine(folderPath, title + ".json");
        }
    }

    // editor objects -> LevelData
    // used for loading game level from editor
    public LevelData EditorObjectsToLevelData()
    {
        LevelData levelData = new LevelData(LevelLoader.currentLevel.title);

        foreach (Placeable placeable in objectGroup.GetComponentsInChildren<Placeable>())
        {
            placeable.AddToLevelData(levelData);
        }

        return levelData;
    }

    // JSON -> LevelData
    // used for loading game level from gallery
    public static LevelData LoadLevelDataFromJSON(string filepath)
    {
        if (!File.Exists(filepath))
        {
            Debug.LogError("File " + filepath + " does not exist");
            return null;
        }

        string json = File.ReadAllText(filepath);
        return JsonUtility.FromJson<LevelData>(json);
    }

    // LevelData -> JSON
    public void Save() 
    {
        string json = JsonUtility.ToJson(EditorObjectsToLevelData(), true);
        File.WriteAllText(path, json);
        Debug.Log("Saved level to: " + path);
    }

    // LevelData -> editor objects
    public void Load(LevelData levelData)
    {
        if (levelData == null)
        {
            Debug.LogError("LevelData is null");
            return;
        }

        ClearAll();

        // Load Tiles
        foreach (TileData tile in levelData.tiles)
        {
            GameObject prefab = database.GetEditorPrefab(tile.tileID);
            Vector3 pos = new Vector3(tile.x, tile.y, 0);
            Instantiate(prefab, pos, Quaternion.identity, objectGroup);
        }

        // Load Items
        foreach (ItemData item in levelData.items)
        {
            GameObject prefab = database.GetEditorPrefab(item.prefabID);
            Vector3 pos = new Vector3(item.x, item.y, 0);
            Instantiate(prefab, pos, Quaternion.identity, objectGroup);
        }
    }

    // Clear all editor placeables
    public void ClearAll()
    {
        foreach(Transform item in objectGroup)
        {
            Destroy(item.gameObject);
        }
    }

}
