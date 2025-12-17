using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Holder : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Player player; 
    public bool powered { get; private set; }
    public bool near;
    [SerializeField] Sprite sprite_holder;
    [SerializeField] Sprite sprite_holder_orb;
    [SerializeField] Sprite sprite_holder_hilight;
    [SerializeField] Sprite sprite_holder_orb_hilight;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
    }

    protected void Activate()
    {
        if (!powered) return;
        OnActivate();
    }

    protected abstract void OnActivate();
}
