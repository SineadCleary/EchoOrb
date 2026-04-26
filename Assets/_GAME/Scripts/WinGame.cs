using UnityEngine;

public class WinGame : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerPrefs.SetInt(LevelLoader.currentLevel.title, 1);
    }
}
