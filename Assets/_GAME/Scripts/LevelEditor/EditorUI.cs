using UnityEngine;

public class EditorUI : MonoBehaviour
{
    [SerializeField] LevelEditor editor;

    public void SwapItem(GameObject item)
    {
        Placeable placeableData = item.GetComponent<Placeable>();
        if (placeableData != null)
        {
            editor.SwapPlaceableItem(placeableData);
            editor.currentTool = LevelEditor.Tool.PLACE;
        }
    }

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
}
