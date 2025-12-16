using UnityEngine;

public class GhostWall : MonoBehaviour
{
    BoxCollider2D myCollider;
    SpriteRenderer myRenderer;
    [SerializeField] bool visible;
    Color c_invisible;
    Color c_normal;

    private void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        c_normal = myRenderer.color;
        c_invisible = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 0.5f);

        SetVisible(visible);
    }

    public void Toggle()
    {
        if (visible)
        {
            SetVisible(false);
        }
        else
        {
            SetVisible(true);
        }
    }

    void SetVisible(bool visible)
    {
        // set visible
        if (visible)
        {
            this.visible = true;
            myCollider.enabled = true;
            // Swap sprite
            myRenderer.color = c_normal;


        }
        // set invisible
        else
        {
            this.visible = false;
            myCollider.enabled = false;
            // Swap sprite
            myRenderer.color = c_invisible;
        }
    }
}
