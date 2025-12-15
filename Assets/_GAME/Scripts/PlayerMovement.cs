using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Rigidbody2D myRigidbody;
    Vector2 moveDirection;

    [SerializeField] GameManager gameManager;
    public Holder nearHolder;

    public UnityEvent activateEvent;

    private void Awake()
    {
        if (activateEvent == null) activateEvent = new UnityEvent();
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        myRigidbody.linearVelocity = moveDirection * moveSpeed;
    }

    // Input
    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>().normalized;
    }

    void OnActivate()
    {
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
            if (gameManager.NumOrbs <= 0) return;

            nearHolder.SetPowered(true);
            gameManager.RemoveOrb();
        }
    }
}
