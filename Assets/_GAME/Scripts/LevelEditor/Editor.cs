using UnityEngine;
using UnityEngine.InputSystem;

public class Editor : MonoBehaviour
{
    Vector2 mousePos;
    Vector2 objectPos;
    [SerializeField] GameObject newItem;
    [SerializeField] bool gridSnapping = true;
    [SerializeField] GameObject levelObjects;

    void Start()
    {
        
    }

    void Update()
    {
        mousePos = Mouse.current.position.ReadValue();
        objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (gridSnapping)
        {
            objectPos = new Vector2(Mathf.RoundToInt(objectPos.x), Mathf.RoundToInt(objectPos.y));
        }
    }

    void OnLeftMouse()
    {
        Instantiate(newItem, objectPos, Quaternion.identity, levelObjects.transform);
    }
}
