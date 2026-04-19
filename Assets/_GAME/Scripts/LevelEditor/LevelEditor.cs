using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] SO_Placeable currentPlaceableItem;
    public bool gridSnapping = true;
    public GameObject objectGroup;
    public enum Tool { PLACE, SELECT, ERASE, EYEDROP, MOVE }
    [SerializeField] Button[] buttons;
    private Tool currentTool = Tool.PLACE;
    
    GameObject selectedObject;
    GameObject objectPreview;

    [SerializeField] LayerMask objectMask;
    [SerializeField] LayerMask floorMask;

    [SerializeField] SO_Database database;

    CameraMovement mainCamera;
    [SerializeField] AudioSource UIAudioSource;

    Vector2 mousePosScreen;
    Vector2 mousePosWorld;
    bool pointerOnUI;
    bool pointerOnObject;
    bool pointerOnFloor;
    bool drawing;
    int rotation = 0;

    public GameObject startPoint;
    public int endPoints;

    Vector3 origin;
    int width, height;

    void Start()
    {
        mainCamera = Camera.main.GetComponent<CameraMovement>();
        SetPreview(currentPlaceableItem.editorPrefab);
        origin = GridManager.origin;
        width = GridManager.width;
        height = GridManager.height;
    }

    void Update()
    {
        // Mouse position
        mousePosScreen = Mouse.current.position.ReadValue();
        mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        if (gridSnapping) mousePosWorld = new Vector2(Mathf.RoundToInt(mousePosWorld.x), Mathf.RoundToInt(mousePosWorld.y));
        
        pointerOnUI = EventSystem.current.IsPointerOverGameObject();
        pointerOnObject = Physics2D.OverlapPoint(mousePosWorld, objectMask);
        pointerOnFloor = Physics2D.OverlapPoint(mousePosWorld, floorMask);

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

    public void OnRotateLeft(InputAction.CallbackContext context)
    {
        if (!context.performed || currentTool != Tool.PLACE || currentPlaceableItem.tile != null) return;
        Rotate(1);
    }

    public void OnRotateRight(InputAction.CallbackContext context)
    {
        if (!context.performed || currentTool != Tool.PLACE || currentPlaceableItem.tile != null) return;
        Rotate(-1);
    }

    private void Rotate(int direction)
    {
        rotation = (rotation + 90 * direction + 360) % 360;
        objectPreview.transform.rotation = Quaternion.Euler(0, 0, rotation);
        UIAudioSource.Play();
    }

    public void ResetRotation()
    {
        rotation = 0;
        objectPreview.transform.rotation = Quaternion.identity;
    }

    public void Toggle(InputAction.CallbackContext context)
    {
        if (!context.performed || currentTool != Tool.PLACE) return;
        if (currentPlaceableItem.altId == 0) return;
        currentPlaceableItem = database.GetPlaceable(currentPlaceableItem.altId);
        SetPreview(currentPlaceableItem.editorPrefab);
        UIAudioSource.Play();
    }

    void Draw()
    {
        switch (currentTool)
        {
            case Tool.PLACE:
                // Place floor
                if (currentPlaceableItem.id == 11)
                {
                    if (pointerOnFloor) return;
                    Instantiate(currentPlaceableItem.editorPrefab, mousePosWorld, Quaternion.identity, objectGroup.transform);
                    return;
                }

                // Place other tiles and items
                if (pointerOnObject) return;

                if (mousePosWorld.x < origin.x || 
                    mousePosWorld.y < origin.y ||
                    mousePosWorld.x > origin.x + width ||
                    mousePosWorld.y > origin.y + height)
                {
                    Debug.Log("Out of bounds");
                    return;
                }

                GameObject obj = Instantiate(currentPlaceableItem.editorPrefab, mousePosWorld, Quaternion.Euler(0f, 0f, rotation), objectGroup.transform);

                // start/end points
                if (currentPlaceableItem.id == 101) endPoints++;
                if (currentPlaceableItem.id == 100)
                {
                    if (startPoint != null) Destroy(startPoint);
                    startPoint = obj;
                }
                break;

            case Tool.ERASE:
                TrySelectObject();
                if (selectedObject == null) return;

                Item item = selectedObject.GetComponent<Item>();
                if (item != null && item.data.id == 101) endPoints--;
                Destroy(selectedObject);
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
        ResetRotation();
    }

    void SetPreview(GameObject prefab)
    {
        if (objectPreview != null)
        {
            Destroy(objectPreview);
        }
        objectPreview = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        SpriteRenderer sr = objectPreview.GetComponent<SpriteRenderer>();
        float transparency = sr.color.a;
        sr.color = new Color(1, 1, 1, transparency * 0.35f);
        objectPreview.GetComponent<Collider2D>().enabled = false;
    }

    public void ClearAll()
    {
        endPoints = 0;
        foreach (Transform item in objectGroup.transform) Destroy(item.gameObject);
    }
}
