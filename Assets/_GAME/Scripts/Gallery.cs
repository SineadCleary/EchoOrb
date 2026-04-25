using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            string filename = Path.GetFileName(files[i]);
            LevelData levelData = SaveLoad.LoadLevelDataFromJSON(Path.Combine(path, filename));
            GameObject card = Instantiate(levelCard, cardContainer);
            card.GetComponent<LevelCardSetup>().Setup(filename, files[i], levelData);
        }
    }
}
