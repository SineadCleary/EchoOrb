using TMPro;
using UnityEngine;

public class NewLevelButton : MonoBehaviour
{
    [SerializeField] TMP_InputField titleField;
    [SerializeField] TMP_InputField authorField;
    [SerializeField] GameObject emptyFieldsMessage;
    [SerializeField] GameObject duplicateFieldsMessage;
    UIManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    public void New()
    {
        string title = titleField.text.Trim(); 
        string author = authorField.text.Trim();
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author))
        {
            duplicateFieldsMessage.SetActive(false);
            emptyFieldsMessage.SetActive(true);
            return;
        }
        if (SaveLoad.FileExists(SaveLoad.MakeFilename(title, author)))
        {
            emptyFieldsMessage.SetActive(false);
            duplicateFieldsMessage.SetActive(true);
            return;
        }
        LevelLoader.currentLevel = new LevelData(title, author);
        manager.StartEditor();
    }
}
