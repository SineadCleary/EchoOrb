using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI authorText;

    public void New()
    {
        // replace invalid filename chars with "_"
        string title = string.Join("_", titleText.text.Split(Path.GetInvalidFileNameChars()));
        string author = authorText.text;
        LevelLoader.currentLevel = new LevelData(title, author/*, new Vector3(0,0,-10), 5f*/);
        LevelLoader.currentLevel.cameraPos = new Vector3(0, 0, -10);
        LevelLoader.currentLevel.cameraZoom = 5;
        SceneManager.LoadScene(2);
    }
}
