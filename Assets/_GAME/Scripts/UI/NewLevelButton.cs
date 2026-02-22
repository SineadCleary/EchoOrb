using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI inputText;

    public void New()
    {
        // replace invalid filename chars with "_"
        string title = string.Join("_", inputText.text.Split(Path.GetInvalidFileNameChars()));
        LevelLoader.currentLevel = new LevelData(title);
        SceneManager.LoadScene(2);
    }
}
