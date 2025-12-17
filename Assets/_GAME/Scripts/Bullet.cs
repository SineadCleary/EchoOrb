using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D myRigidbody;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int damage = 10;
    Vector2 moveDirection;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
            gameManager.AddHealth(-damage);
        }
        else if (collision.CompareTag("Enemy"))
        {
            // hit enemy
            collision.gameObject.GetComponent<SimpleEnemyMovement>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
