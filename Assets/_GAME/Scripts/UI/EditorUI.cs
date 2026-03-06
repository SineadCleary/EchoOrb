using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditorUI : MonoBehaviour
{
    [SerializeField] LevelEditor editor;
    [SerializeField] SaveLoad saveLoad;

    // Placeables
    public void SwapItem(GameObject item)
    {
        Placeable placeableData = item.GetComponent<Placeable>();
        if (placeableData != null)
        {
            editor.SwapPlaceableItem(placeableData);
            editor.SetCurrentTool(LevelEditor.Tool.PLACE);
        }
    }

    // Grid Snapping
    public void ToggleGridSnapping()
    {
        editor.gridSnapping = !editor.gridSnapping;
    }

    // Tools
    public void SelectTool()
    {
        editor.SetCurrentTool(LevelEditor.Tool.SELECT);
    }

    public void PlaceTool()
    {
        editor.SetCurrentTool(LevelEditor.Tool.PLACE);
    }
    
    public void EraseTool()
    {
        editor.SetCurrentTool(LevelEditor.Tool.ERASE);
    }

    public void EyedropTool()
    {
        editor.SetCurrentTool(LevelEditor.Tool.EYEDROP);
    }

    public void MoveTool()
    {
        editor.SetCurrentTool(LevelEditor.Tool.MOVE);
    }

    // Toggle element
    public void ToggleElement(GameObject element)
    {
        if (element.activeInHierarchy) element.SetActive(false);
        else element.SetActive(true);
    }

    // Save/load
    public void SaveButton(GameObject savePanel)
    {
        if (!saveLoad.isValidLevel())
        {

        }
        else saveLoad.Save();
        ToggleElement(savePanel);
    }

    public void SaveAndQuit()
    {
        if (!saveLoad.isValidLevel())
        {

        }
        else saveLoad.Save();
        SceneManager.LoadScene(4);
    }

    public void EnterPlayMode()
    {
        if (!saveLoad.isValidLevel())
        {

        }
        else
        {
            LevelLoader.currentLevel = saveLoad.EditorObjectsToLevelData();
            SceneManager.LoadScene(3);
        }
    }

    public void ClearAll()
    {
        saveLoad.ClearAll();
    }
}
