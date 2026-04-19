using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BoundsLine : MonoBehaviour
{
    LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        SetLine();
    }

    public void SetLine()
    {
        Vector2 origin = GridManager.origin;
        int width = GridManager.width;
        int height = GridManager.height;

        lr.SetPosition(0, new Vector3(origin.x, origin.y));
        lr.SetPosition(1, new Vector3(origin.x + width, origin.y));
        lr.SetPosition(2, new Vector3(origin.x + width, origin.y + height));
        lr.SetPosition(3, new Vector3(origin.x, origin.y + height));
    }

}
