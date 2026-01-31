using UnityEngine;

public class EditorUI : MonoBehaviour
{
    [SerializeField] LevelEditor editor;

    public void SwapItem(GameObject item)
    {
        editor.SwapPlaceableItem(item);
    }

    public void ToggleGridSnapping()
    {
        editor.gridSnapping = !editor.gridSnapping;
    }

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
}
