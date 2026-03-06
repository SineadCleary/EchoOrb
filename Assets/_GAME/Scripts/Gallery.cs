using System.IO;
using TMPro;
using UnityEngine;

public class Gallery : MonoBehaviour
{
    [SerializeField] GameObject levelCard;
    [SerializeField] Transform cardContainer;

    void Start()
    {
        // Initialise gallery
        string path = Path.Combine(Application.persistentDataPath, "Levels");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string[] files = Directory.GetFiles(path, "*.json");
        for (int i=0; i<files.Length; i++)
        {
            GameObject card = Instantiate(levelCard, cardContainer);
            
            // Title
            string title = Path.GetFileNameWithoutExtension(files[i]);
            card.name = title + "_card";
            card.GetComponentsInChildren<TextMeshProUGUI>()[0].text = title;

            LevelData levelData = SaveLoad.LoadLevelDataFromJSON(Path.Combine(path, title + ".json"));

            // Status

            // Author
            card.GetComponentsInChildren<TextMeshProUGUI>()[2].text = "by " + levelData.author;

            // Date
            card.GetComponentsInChildren<TextMeshProUGUI>()[3].text = levelData.date;

            // File path
            LevelCardButton[] buttons = card.GetComponentsInChildren<LevelCardButton>();
            foreach (LevelCardButton button in buttons)
            {
                button.filepath = Path.GetFullPath(files[i]);
            }
        }
    }
}
