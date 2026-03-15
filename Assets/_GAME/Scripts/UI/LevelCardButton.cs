using System.IO;
using UnityEngine;

public class LevelCardButton : MonoBehaviour
{
    public string filepath;
    UIManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    public void Play()
    {
        if (string.IsNullOrEmpty(filepath)) return;
        LevelLoader.currentLevel = SaveLoad.LoadLevelDataFromJSON(filepath);
        manager.StartGame();
    }

    public void Edit()
    {
        if (string.IsNullOrEmpty(filepath)) return;
        LevelLoader.currentLevel = SaveLoad.LoadLevelDataFromJSON(filepath);
        manager.StartEditor();
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
