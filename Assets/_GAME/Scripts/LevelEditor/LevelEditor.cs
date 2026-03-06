using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] SO_Placeable currentPlaceableItem;
    public bool gridSnapping = true;
    [SerializeField] GameObject objectGroup;
    public enum Tool { PLACE, SELECT, ERASE, EYEDROP, MOVE }
    [SerializeField] Button[] buttons;
    private Tool currentTool = Tool.PLACE;
    
    GameObject selectedObject;
    GameObject objectPreview;

    CameraMovement mainCamera;

    Vector2 mousePosScreen;
    Vector2 mousePosWorld;
    bool pointerOnUI;
    bool pointerOnObject;
    bool drawing;

    void Start()
    {
        mainCamera = Camera.main.GetComponent<CameraMovement>();
        SetPreview(currentPlaceableItem.editorPrefab);
    }

    void Update()
    {
        // Mouse position
        mousePosScreen = Mouse.current.position.ReadValue();
        mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        if (gridSnapping) mousePosWorld = new Vector2(Mathf.RoundToInt(mousePosWorld.x), Mathf.RoundToInt(mousePosWorld.y));
        
        pointerOnUI = EventSystem.current.IsPointerOverGameObject();
        pointerOnObject = Physics2D.OverlapPoint(mousePosWorld);

        // Object Preview
        if (currentTool == Tool.PLACE)
        {
            objectPreview.SetActive(true);
            objectPreview.transform.position = new Vector3(mousePosWorld.x, mousePosWorld.y, -1);
        }
        else objectPreview.SetActive(false);

        // Clear current selection
        if (currentTool != Tool.SELECT)
        {
            if (selectedObject != null)
            {
                selectedObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                selectedObject = null;
            }
        }
        
        // Draw
        if (drawing) Draw();
    }

    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        if (pointerOnUI) return;

        if (currentTool == Tool.MOVE)
        {
            if (context.started)
                mainCamera.StartDrag();
            if (context.canceled)
                mainCamera.StopDrag();
        }

        if (context.started)
        {
            drawing = true;
            return;
        }
        if (context.canceled) 
        {
            drawing = false;
            return;
        }
        // context.performed
        switch (currentTool)
        {
            case Tool.MOVE:
                if (context.started)
                    mainCamera.StartDrag();
                if (context.canceled)
                    mainCamera.StopDrag();
                break;

            case Tool.SELECT:
                if (!context.performed) return;
                TrySelectObject();
                break;

            case Tool.EYEDROP:
                if (!context.performed) return;
                TrySelectObject();
                if (selectedObject != null)
                {
                    SetCurrentTool(Tool.PLACE);
                    Placeable placeable = selectedObject.GetComponent<Placeable>();
                    if (placeable != null)
                    {
                        SwapPlaceableItem(placeable);
                    }
                }
                break;

            default:
                break;
        }
    }

    void Draw()
    {
        switch (currentTool)
        {
            case Tool.PLACE:
                if (pointerOnObject) return;
                Instantiate(currentPlaceableItem.editorPrefab, mousePosWorld, Quaternion.identity, objectGroup.transform);
                break;

            case Tool.ERASE:
                TrySelectObject();
                if (selectedObject != null)
                {
                    Destroy(selectedObject);
                }
                break;

            default: break;
        }
    }

    void TrySelectObject()
    {
        Collider2D hit = Physics2D.OverlapPoint(mousePosWorld);

        // Clear previous selection
        if (selectedObject != null)
        {
            selectedObject.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1);
            selectedObject = null;
        }

        if (hit == null) return;

        selectedObject = hit.gameObject;

        // selection highlight
        if (currentTool == Tool.SELECT)
        {
            SpriteRenderer sr = selectedObject.GetComponent<SpriteRenderer>();
            sr.color = Color.yellow;
        }
    }

    public void SetCurrentTool(Tool tool)
    {
        currentTool = tool;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i != (int)tool;
        }
    }

    public void SwapPlaceableItem(Placeable placeable)
    {
        currentPlaceableItem = placeable.data;
        SetPreview(currentPlaceableItem.editorPrefab);
    }

    void SetPreview(GameObject prefab)
    {
        if (objectPreview != null)
        {
            Destroy(objectPreview);
        }
        objectPreview = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        objectPreview.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.35f);
        objectPreview.GetComponent<Collider2D>().enabled = false;
    }
}
