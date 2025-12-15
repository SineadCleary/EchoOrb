using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Rigidbody2D myRigidbody;
    Vector2 moveDirection;

    [SerializeField] GameManager gameManager;
    public Holder nearHolder;


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
        if (gameManager.NumOrbs >= 1)
        {
            Debug.Log("activate");
        }
        else
        {
            Debug.Log("no orbs");
        }
    }

    void OnPut()
    {
        // Near a holder
        if (nearHolder == null) return;

        // Holder powered
        if (nearHolder.powered)
        {
            nearHolder.powered = false;
            nearHolder.SetSprite();
            gameManager.AddOrb();
        }
        // Holder not powered
        else
        {
            // Has an orb
            if (gameManager.NumOrbs <= 0) return;
            
            nearHolder.powered = true;
            nearHolder.SetSprite();
            gameManager.RemoveOrb();
        }
    }
}
