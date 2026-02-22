using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCardButton : MonoBehaviour
{
    public string filepath;
    public void Play()
    {
        if (string.IsNullOrEmpty(filepath)) return;
        LevelLoader.currentLevel = SaveLoad.LoadLevelDataFromJSON(filepath);
        SceneManager.LoadScene(3);
    }

    public void Edit()
    {
        if (string.IsNullOrEmpty(filepath)) return;
        LevelLoader.currentLevel = SaveLoad.LoadLevelDataFromJSON(filepath);
        SceneManager.LoadScene(2);
    }
}
