using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    string path;
    [SerializeField] Transform objectGroup;
    [SerializeField] SO_Database database;

    private void Awake()
    {
        path = Application.persistentDataPath + "/levelData.json";
    }

    // editor objects -> LevelData
    // used for loading game level from editor
    public LevelData EditorObjectsToLevelData()
    {
        LevelData levelData = new LevelData();

        foreach (Placeable placeable in objectGroup.GetComponentsInChildren<Placeable>())
        {
            placeable.AddToLevelData(levelData);
        }

        return levelData;
    }

    // JSON -> LevelData
    // used for loading game level from gallery
    public LevelData LoadLevelDataFromJSON(string filepath)
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
    public void Load()
    {
        if (!File.Exists(path))
        {
            Debug.LogError("File " + path + " does not exist");
            return;
        }

        ClearAll();

        LevelData levelData = LoadLevelDataFromJSON(path);

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

        Debug.Log("File loaded");
    }

    // Play the current level in editor
    public void PlayLevel()
    {
        //LevelLoader.LoadFromEditor(EditorObjectsToLevelData());
        LevelLoader.currentLevel = EditorObjectsToLevelData();
        SceneManager.LoadScene(3);
    }

    public void ClearAll()
    {
        foreach(Transform item in objectGroup)
        {
            Destroy(item.gameObject);
        }
    }

}
