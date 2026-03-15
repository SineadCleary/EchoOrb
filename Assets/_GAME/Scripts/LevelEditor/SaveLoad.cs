using System;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    static string folderPath;
    [SerializeField] Transform objectGroup;
    [SerializeField] SO_Database database;
    [SerializeField] Camera mainCamera;

    private void Start()
    {
        if (LevelLoader.currentLevel != null)
        {
            Load(LevelLoader.currentLevel);
            string title = LevelLoader.currentLevel.title;

            folderPath = Path.Combine(Application.persistentDataPath, "Levels");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        }
    }

    // editor objects -> LevelData
    // used for saving, and entering play mode
    public LevelData EditorObjectsToLevelData()
    {
        // Get current levelData
        LevelData levelData = LevelLoader.currentLevel;

        // Clear and rebuild lists
        levelData.items.Clear();
        levelData.tiles.Clear();
        foreach (Placeable placeable in objectGroup.GetComponentsInChildren<Placeable>())
        {
            placeable.AddToLevelData(levelData);
        }

        // Update camera
        levelData.cameraPos = mainCamera.transform.position;
        levelData.cameraZoom = mainCamera.orthographicSize;

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
    public static void Save(bool valid = false)
    {
        if (LevelLoader.currentLevel == null)
        {
            Debug.LogWarning("Cannot save. LevelLoader.currentLevel is null");
            return;
        }
        // Get level data 
        LevelData levelData = LevelLoader.currentLevel;
        // Only update date on save
        levelData.date = DateTime.Now.ToString("d");
        levelData.complete = valid;
        // Write to JSON
        string json = JsonUtility.ToJson(levelData, true);
        string path = Path.Combine(folderPath, levelData.title + ".json");
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

        // Setup Camera
        mainCamera.transform.position = levelData.cameraPos;
        mainCamera.orthographicSize = levelData.cameraZoom;
    }

    // Clear all editor placeables
    public void ClearAll()
    {
        foreach(Transform item in objectGroup)
        {
            Destroy(item.gameObject);
        }
    }

    // Validation
    public bool isValidLevel()
    {
        return true;
    }
}
