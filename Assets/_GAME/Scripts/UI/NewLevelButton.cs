using TMPro;
using System.IO;
using UnityEngine;

public class NewLevelButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI authorText;
    UIManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    public void New()
    {
        // replace invalid filename chars with "_"
        string title = string.Join("_", titleText.text.Split(Path.GetInvalidFileNameChars()));
        string author = authorText.text;
        LevelLoader.currentLevel = new LevelData(title, author);
        manager.StartEditor();
    }
}
