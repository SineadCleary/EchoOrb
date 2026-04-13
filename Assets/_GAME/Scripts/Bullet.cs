using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D myRigidbody;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int damage = 10;
    Vector2 moveDirection;
    float destroyTime = 20f;
    float destroyTimeCounter = 0f;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (moveDirection == Vector2.zero) moveDirection = transform.up.normalized;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        destroyTimeCounter += Time.deltaTime;
        if (destroyTimeCounter >= destroyTime)
        {
            Destroy(gameObject);
        }
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
        else if (collision.CompareTag("Creature"))
        {
            CreatureHealth creature = collision.gameObject.GetComponent<CreatureHealth>();
            if (creature == null)
            {
                Debug.LogError("Creature: " + collision.name + " does not have attached CreatureHealth.");
                return;
            }
            creature.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }
}
