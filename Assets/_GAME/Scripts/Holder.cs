using UnityEngine;

public class Holder : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    PlayerMovement player;
    public bool powered { get; private set; }
    public bool near;
    [SerializeField] Sprite sprite_holder;
    [SerializeField] Sprite sprite_holder_orb;
    [SerializeField] Sprite sprite_holder_hilight;
    [SerializeField] Sprite sprite_holder_orb_hilight;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        SetPowered(powered);
    }

    private void OnEnable()
    {
        player.activateEvent.AddListener(Activate);
    }

    private void OnDisable()
    {
        player.activateEvent.RemoveListener(Activate);
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

    public void SetPowered(bool powered)
    {
        this.powered = powered;
        SetSprite();
        if (powered == false)
        {
            player.activateEvent.RemoveListener(Activate);
        }
        else
        {
            player.activateEvent.AddListener(Activate);
        }
    }

    void Activate()
    {
        Debug.Log("Activation");
    }
}
