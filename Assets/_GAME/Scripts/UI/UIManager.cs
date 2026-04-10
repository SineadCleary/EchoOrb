using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Toggle levelValid;

    private void Start()
    {
        if (levelValid != null)
        {
            levelValid.isOn = LevelLoader.currentLevel.complete;
        }
    }

    public void StartCustomGame()
    {
        if (LevelLoader.currentLevel == null) return;
        SceneManager.LoadScene(3);
    }

    public void StartBaseGame()
    {
        if (LevelLoader.currentLevel == null) return;
        SceneManager.LoadScene(5);
    }

    public void StartEditor()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        LevelLoader.currentLevel = null;
        SceneManager.LoadScene(0);
    }

    public void OpenGallery()
    {
        LevelLoader.currentLevel = null;
        SceneManager.LoadScene(2);
    }

    public void OpenLevelSelection()
    {
        LevelLoader.currentLevel = null;
        SceneManager.LoadScene(4);
    }

    public void SaveCurrentLevel()
    {
        bool valid = false;
        if (levelValid != null && levelValid.gameObject.activeInHierarchy && levelValid.isOn) valid = true;
        SaveLoad.Save(valid);
    }

    public void SaveCurrentLevelAndQuit()
    {
        SaveCurrentLevel();
        OpenGallery();
    }
}
