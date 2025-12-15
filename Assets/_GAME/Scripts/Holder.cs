using UnityEngine;

public class Holder : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool powered;
    public bool near;
    [SerializeField] Sprite sprite_holder;
    [SerializeField] Sprite sprite_holder_orb;
    [SerializeField] Sprite sprite_holder_hilight;
    [SerializeField] Sprite sprite_holder_orb_hilight;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    public void SetSprite()
    {
        if (powered && near)
        {
            spriteRenderer.sprite = sprite_holder_orb_hilight;
        }
        else if (powered && !near)
        {
            spriteRenderer.sprite = sprite_holder_orb;
        }
        else if (!powered && near)
        {
            spriteRenderer.sprite = sprite_holder_hilight;
        }
        else if (!powered && !near)
        {
            spriteRenderer.sprite = sprite_holder;
        }
    }
}
