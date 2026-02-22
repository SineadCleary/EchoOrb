using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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
            card.GetComponentInChildren<TextMeshProUGUI>().text = title;

            // File path
            LevelCardButton[] buttons = card.GetComponentsInChildren<LevelCardButton>();
            foreach (LevelCardButton button in buttons)
            {
                button.filepath = Path.GetFullPath(files[i]);
            }
        }
    }
}
