using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] string title;
    [SerializeField] UIManager manager;
    [SerializeField] Sprite completeSprite;

    private void Start()
    {
        if (PlayerPrefs.GetInt(title) == 1)
        {
            Image image = gameObject.GetComponent<Image>();
            if (completeSprite != null && image != null)
                image.sprite = completeSprite;
        }
    }

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
