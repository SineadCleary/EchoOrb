using UnityEngine;

[RequireComponent(typeof(UIManager))]
public class EditorUI : MonoBehaviour
{
    [SerializeField] LevelEditor editor;
    [SerializeField] SaveLoad saveLoad; 
    UIManager uiManager;

    private void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

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
        saveLoad.EditorObjectsToLevelData();
        SaveLoad.Save();
        ToggleElement(savePanel);
    }

    public void SaveAndQuit()
    {
        saveLoad.EditorObjectsToLevelData();
        SaveLoad.Save();
        uiManager.OpenGallery();
    }

    public void EnterPlayMode()
    {
        LevelLoader.currentLevel = saveLoad.EditorObjectsToLevelData();
        uiManager.StartCustomGame();
    }

    public void ClearAll()
    {
        saveLoad.ClearAll();
    }

    //public void Undo()
    //{
    //    editor.Undo();
    //}
}
