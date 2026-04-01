using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Rigidbody2D myRigidbody;
    Vector2 moveDirection;
    

    GameManager gameManager;
    AudioSource myAudioSource;
    [SerializeField] AudioClip activateSound;

    public Holder nearHolder;

    public UnityEvent activateEvent;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        myRigidbody.linearVelocity = moveDirection * moveSpeed;
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
        myAudioSource.PlayOneShot(activateSound);
        activateEvent.Invoke();
    }

    void OnPut()
    {
        // Near a holder
        if (nearHolder == null) return;

        // Holder powered
        if (nearHolder.powered)
        {
            nearHolder.SetPowered(false);
            gameManager.AddOrb();
        }
        // Holder not powered
        else
        {
            // Has an orb
            if (gameManager.numOrbs <= 0) return;

            nearHolder.SetPowered(true);
            gameManager.RemoveOrb();
        }
    }
}
