using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] SO_PlaceableItem currentPlaceableItem;
    public bool gridSnapping = true;
    [SerializeField] GameObject objectGroup;

    public enum Tool { PLACE, SELECT, ERASE, EYEDROP }
    public Tool currentTool = Tool.PLACE;
    
    GameObject selectedObject;
    GameObject objectPreview;

    Vector2 mousePosScreen;
    Vector2 mousePosWorld;
    bool pointerOnUI;
    bool pointerOnObject;

    void Start()
    {
        SetPreview(currentPlaceableItem.prefab);
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
        
    }

    void OnLeftMouse()
    {
        if (pointerOnUI) return;
        if (currentTool == Tool.PLACE)
        {
            if (pointerOnObject) return;
            Instantiate(currentPlaceableItem.prefab, mousePosWorld, Quaternion.identity, objectGroup.transform);
        }
        else if (currentTool == Tool.SELECT)
        {
            TrySelectObject();
        }
        else if (currentTool == Tool.ERASE)
        {
            TrySelectObject();
            if (selectedObject != null)
            {
                Destroy(selectedObject);
            }
        }
        else if (currentTool == Tool.EYEDROP)
        {
            TrySelectObject();
            if (selectedObject != null)
            {
                currentTool = Tool.PLACE;
                Item item = selectedObject.GetComponent<Item>();
                if (item != null)
                {
                    SwapPlaceableItem(item);
                }
            }
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


    public void SwapPlaceableItem(Item item)
    {
        currentPlaceableItem = item.data;
        SetPreview(currentPlaceableItem.prefab);
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
