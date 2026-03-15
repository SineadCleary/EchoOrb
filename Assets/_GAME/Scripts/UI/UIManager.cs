using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Toggle levelValid;

    public void StartGame()
    {
        SceneManager.LoadScene(3);
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
