using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardSetup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] GameObject status;
    [SerializeField] Button playButton;
    [SerializeField] TextMeshProUGUI authorText;
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] GameObject publishButton;
    [SerializeField] LevelCardButton[] levelCardButtons;

    public void Setup(string filename, string file, LevelData levelData)
    {
        // Title
        string title = levelData.title;
        gameObject.name = title + "_card";
        titleText.text = title;

        // Status
        if (!levelData.complete)
        {
            status.SetActive(true);
            playButton.interactable = false;
        }

        // Author
        authorText.text = "by " + levelData.author;

        // Date
        dateText.text = levelData.date;

#if UNITY_EDITOR
        // publish button
        publishButton.SetActive(true);
#endif

        // File path
        foreach (LevelCardButton button in levelCardButtons)
        {
            button.filepath = Path.GetFullPath(file);
        }
    }
}
