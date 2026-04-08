using UnityEngine;

public class TrapBlock : MonoBehaviour
{
    bool debug = true;
    bool found = false;

    Rigidbody2D myRigidbody;
    int layerMask;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] int rayLength = 10;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        layerMask = LayerMask.GetMask("Player", "Default", "Placeable");
    }

    private void FixedUpdate()
    {
        if (debug)
        {
            Debug.DrawRay(transform.position, Vector2.left * rayLength, Color.red);
            Debug.DrawRay(transform.position, Vector2.right * rayLength, Color.red);
            Debug.DrawRay(transform.position, Vector2.up * rayLength, Color.red);
            Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
        }

        RaycastHit2D leftRay = Physics2D.Raycast(transform.position, Vector2.left, rayLength, layerMask);
        RaycastHit2D rightRay = Physics2D.Raycast(transform.position, Vector2.right, rayLength, layerMask);
        RaycastHit2D upRay = Physics2D.Raycast(transform.position, Vector2.up, rayLength, layerMask);
        RaycastHit2D downRay = Physics2D.Raycast(transform.position, Vector2.down, rayLength, layerMask);

        if (leftRay.collider != null && !found)
        {
            // move left
            if (leftRay.collider.CompareTag("Player"))
            {
                found = true;
                myRigidbody.linearVelocity = new Vector2(-1 * moveSpeed, 0);
            }
        } 
        if (rightRay.collider != null && !found)
        {
            if (rightRay.collider.CompareTag("Player"))
            {
                // move right
                found = true;
                myRigidbody.linearVelocity = new Vector2(1 * moveSpeed, 0);
            }
        }
        if (upRay.collider != null && !found)
        {
            if (upRay.collider.CompareTag("Player"))
            {
                // move up
                found = true;
                myRigidbody.linearVelocity = new Vector2(0, 1 * moveSpeed);
            }
        }
        if (downRay.collider != null && !found)
        {
            if (downRay.collider.CompareTag("Player"))
            {
                // move down
                found = true;
                myRigidbody.linearVelocity = new Vector2(0, -1 * moveSpeed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ADWASDA");
        found = false;
        myRigidbody.linearVelocity = Vector2.zero;
        // round position
    }
}
