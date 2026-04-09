using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Holder : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    protected Player player;
    protected GameManager gameManager;

    public bool powered { get; private set; }
    public bool near;
    [SerializeField] Sprite sprite_holder;
    [SerializeField] Sprite sprite_holder_orb;
    [SerializeField] Sprite sprite_holder_hilight;
    [SerializeField] Sprite sprite_holder_orb_hilight;
    [SerializeField] AudioClip placeOrbSound;
    [SerializeField] AudioClip takeOrbSound;


    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        SetSprite();
    }

    protected virtual void OnEnable()
    {
        player.activateEvent.AddListener(Activate);
    }

    protected virtual void OnDisable()
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
        if (powered)
        {
            audioSource.PlayOneShot(placeOrbSound);
        }
        else audioSource.PlayOneShot(takeOrbSound);
    }

    protected void Activate()
    {
        if (!powered) return;
        OnActivate();
    }

    protected abstract void OnActivate();
}
