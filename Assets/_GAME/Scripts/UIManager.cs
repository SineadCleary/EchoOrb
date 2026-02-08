using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject openPanel;
    [SerializeField] GameObject closePanel;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartEditor()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenInstructions()
    {
        if (openPanel == null || closePanel == null) return;
        openPanel.SetActive(true);
        closePanel.SetActive(false);
    }
}
