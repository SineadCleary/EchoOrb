using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Rigidbody2D myRigidbody;
    Vector2 moveDirection;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();   
    }

    private void FixedUpdate()
    {
        myRigidbody.linearVelocity = moveDirection * moveSpeed;
    }

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>().normalized;
    }
}
