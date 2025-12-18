using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

// Used YouTube tutorial for Raycasts: https://www.youtube.com/watch?v=xDg2pxqJHq4 
public class SimpleEnemyMovement : MonoBehaviour
{
    GameObject player;
    Rigidbody2D myRigidbody;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float viewDistance = 50f;
    [SerializeField] LayerMask myLayerMask;
    bool seeTarget = false;
    [SerializeField] int health = 40;
    [SerializeField] GameObject orb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float direction = player.transform.position.x - transform.position.x;
        if (direction > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, viewDistance, myLayerMask);
        if(ray.collider != null)
        {
            seeTarget = ray.collider.CompareTag("Player");
        }
        if (seeTarget)
        {
            Vector2 movePos = Vector2.MoveTowards(myRigidbody.position, player.transform.position, moveSpeed * Time.fixedDeltaTime);
            myRigidbody.MovePosition(movePos);
        }

        // Debug
        if (seeTarget)
        {
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        }
    }

    public void TakeDamage(int healthPoints)
    {
        health -= healthPoints;
        if (health < 0)
        {
            // Death
            Instantiate(orb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
