using System.IO;
using UnityEngine;
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
            editor.currentTool = LevelEditor.Tool.PLACE;
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
        editor.currentTool = LevelEditor.Tool.SELECT;
    }

    public void PlaceTool()
    {
        editor.currentTool = LevelEditor.Tool.PLACE;
    }
    
    public void EraseTool()
    {
        editor.currentTool = LevelEditor.Tool.ERASE;
    }

    public void EyedropTool()
    {
        editor.currentTool = LevelEditor.Tool.EYEDROP;
    }

    public void MoveTool()
    {
        editor.currentTool = LevelEditor.Tool.MOVE;
    }

    // Save/load
    public void SaveButton()
    {
        saveLoad.Save();
    }

    public void EnterPlayMode()
    {
        LevelLoader.currentLevel = saveLoad.EditorObjectsToLevelData();
        SceneManager.LoadScene(3);
    }

    public void ClearAll()
    {
        saveLoad.ClearAll();
    }
}
