using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LevelEditor : MonoBehaviour
{
    Vector2 mousePosScreen;
    Vector2 mousePosWorld;
    [SerializeField] GameObject currentPlaceableItem;
    public bool gridSnapping = true;
    [SerializeField] GameObject levelObjects;
    GameObject objectPreview;
    bool pointerOnUI;
    bool pointerOnObject;
    public enum Tool { PLACE, SELECT, ERASE, EYEDROP }
    public Tool currentTool = Tool.PLACE;
    GameObject selectedObject;

    void Start()
    {
        SetPreview(currentPlaceableItem);
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
            //if (pointerOnObject) objectPreview.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.35f);
            //else objectPreview.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.35f);
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
            Instantiate(currentPlaceableItem, mousePosWorld, Quaternion.identity, levelObjects.transform);
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
                //currentPlaceableItem = selectedObject.gameObject;
                //SetPreview(currentPlaceableItem);
                SwapPlaceableItem(selectedObject);
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


    public void SwapPlaceableItem(GameObject item)
    {
        currentPlaceableItem = item;
        SetPreview(item);
    }

    void SetPreview(GameObject item)
    {
        if (objectPreview != null)
        {
            Destroy(objectPreview);
        }
        objectPreview = Instantiate(item, Vector3.zero, Quaternion.identity);
        objectPreview.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.35f);
        objectPreview.GetComponent<Collider2D>().enabled = false;
    }
}
