using UnityEngine;

public class EnablePlayButton : MonoBehaviour
{
    [SerializeField] SaveLoad saveLoad;
    [SerializeField] UIManager uiManager;
    [SerializeField] LevelEditor editor;
    [SerializeField] GameObject message;
    [SerializeField] GameObject startPointButton;
    [SerializeField] GameObject endPointButton;

    public void TryEnterPlayMode()
    {
        bool hasStartPoint = editor.startPoint != null;
        bool hasEndPoint = editor.endPoints >= 1;
        if (hasStartPoint && hasEndPoint)
        {
            LevelLoader.currentLevel = saveLoad.EditorObjectsToLevelData();
            uiManager.StartCustomGame();
        }
        else
        {
            message.SetActive(true);
            startPointButton.SetActive(!hasStartPoint);
            endPointButton.SetActive(!hasEndPoint);
        }
    }
}
