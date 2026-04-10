using System.IO;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] UIManager manager;

    public void Play()
    {
        if (string.IsNullOrEmpty(levelName))
        {
            Debug.LogError("Level name not assigned");
            return;
        }

        string filepath = Path.Combine(Application.streamingAssetsPath, "Levels", levelName + ".json");
        if (File.Exists(filepath))
        {
            LevelLoader.currentLevel = SaveLoad.LoadLevelDataFromJSON(filepath);
            manager.StartBaseGame();
        }
        else
        {
            Debug.LogError("File does not exist: " + filepath);
        }
    }
}
