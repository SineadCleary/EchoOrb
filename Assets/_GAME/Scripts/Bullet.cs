using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float moveSpeed = 5f;
    Vector2 moveDirection;

    void Start()
    {
        moveDirection = transform.up.normalized;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        myRigidbody.linearVelocity = moveDirection * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // hit player
        }
        //else if (collision.CompareTag("Enemy"))
        //{
        //    // hit enemy
        //}
        //Destroy(gameObject);
        Destroy(gameObject);
    }
}
