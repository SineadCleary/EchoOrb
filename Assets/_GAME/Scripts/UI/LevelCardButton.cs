using System.IO;
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

    public void Delete()
    {
        if (string.IsNullOrEmpty(filepath)) return;
        File.Delete(filepath);
        Debug.Log("Deleted " + filepath);
        Destroy(this.transform.parent.gameObject);
    }

    public void ShowDeletePanel()
    {
        GameObject panel = this.transform.GetChild(0).gameObject;
        panel.SetActive(!panel.activeInHierarchy);
    }
}
