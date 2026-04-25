using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Rigidbody2D myRigidbody;
    SpriteRenderer mySpriteRenderer;
    Animator myAnimator;
    Vector2 moveDirection;
    public bool atEnd = false;

    GameManager gameManager;
    [SerializeField] Sprite death;

    public List<Holder> nearHolders;

    public UnityEvent activateEvent;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        myAnimator.SetBool("IsMoving", myRigidbody.linearVelocity.sqrMagnitude > 0.01f);
    }

    private void FixedUpdate()
    {
        myRigidbody.linearVelocity = moveDirection * moveSpeed;
    }

    public void Hurt()
    {
        myAnimator.SetTrigger("Hurt");
    }

    public void Die()
    {
        mySpriteRenderer.sprite = death;
    }

    // Input
    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>().normalized;

        // Flip sprite
        if (moveDirection.x > 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDirection.x < 0) transform.localScale = new Vector3(1, 1, 1);
    }

    void OnActivate()
    {
        myAnimator.SetTrigger("Activate");
        AudioManager.instance.PlayActivateSound();
        activateEvent.Invoke();
    }

    void OnPut()
    {
        if (atEnd)
        {
            gameManager.Win();
        }

        // Near a holder
        if (nearHolders.Count == 0) return;

        foreach (var holder in nearHolders)
        {
            // Holder powered
            if (holder.powered)
            {
                holder.SetPowered(false);
                gameManager.AddOrb();
            }
            // Holder not powered
            else
            {
                // Has an orb
                if (gameManager.numOrbs <= 0) continue;

                holder.SetPowered(true);
                gameManager.RemoveOrb();
            }
        }
    }

    void OnPause()
    {
        gameManager.PauseGame();
    }
}
