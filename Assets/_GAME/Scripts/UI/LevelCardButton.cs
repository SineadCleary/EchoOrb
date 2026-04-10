using System.IO;
using UnityEditor;
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
        manager.StartCustomGame();
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

#if UNITY_EDITOR
    public void Publish()
    {
        if (string.IsNullOrEmpty(filepath)) return;

        string publishFolder = Path.Combine(Application.streamingAssetsPath, "Levels");

        if (!Directory.Exists(publishFolder))
            Directory.CreateDirectory(publishFolder);

        string fileName = Path.GetFileName(filepath);
        string destination = Path.Combine(publishFolder, fileName);

        File.Copy(filepath, destination, true);
        Debug.Log("Published level to: " + destination);
        AssetDatabase.Refresh();
    }
#endif
}
