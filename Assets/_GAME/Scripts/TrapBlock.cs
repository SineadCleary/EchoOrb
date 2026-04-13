using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapBlock : MonoBehaviour
{
    bool debug = true;
    Vector2 gridOffset = new Vector2(0.5f, 0.5f);

    bool moving = false;
    Vector2 destination;
    MyGrid<PathNode> grid;
    Transform player;
    GameManager gameManager;
    PathNode prevNode;

    Rigidbody2D myRigidbody;
    AudioSource audioSource;
    int layerMask;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] int rayLength = 10;
    [SerializeField] AudioClip blockSound;

    RaycastHit2D leftRay;
    RaycastHit2D rightRay;
    RaycastHit2D upRay;
    RaycastHit2D downRay;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        layerMask = LayerMask.GetMask("Player", "NonWalkable", "Trap"); // used for raycasts
        grid = Pathfinding.Instance.GetGrid();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        prevNode = grid.GetGridObject(transform.position);
        prevNode.isWalkable = false;
    }

    private void Update()
    {
        if (moving)
        {
            Crush();
        }
    }

    private void FixedUpdate()
    {
        Vector2 leftRayOrigin = (Vector2)transform.position + Vector2.left;
        Vector2 rightRayOrigin = (Vector2)transform.position + Vector2.right;
        Vector2 upRayOrigin = (Vector2)transform.position + Vector2.up;
        Vector2 downRayOrigin = (Vector2)transform.position + Vector2.down;

        if (debug)
        {
            Debug.DrawRay(leftRayOrigin, Vector2.left * rayLength, Color.red);
            Debug.DrawRay(rightRayOrigin, Vector2.right * rayLength, Color.red);
            Debug.DrawRay(upRayOrigin, Vector2.up * rayLength, Color.red);
            Debug.DrawRay(downRayOrigin, Vector2.down * rayLength, Color.red);
        }

        leftRay = Physics2D.Raycast(leftRayOrigin, Vector2.left, rayLength, layerMask);
        rightRay = Physics2D.Raycast(upRayOrigin, Vector2.right, rayLength, layerMask);
        upRay = Physics2D.Raycast(upRayOrigin, Vector2.up, rayLength, layerMask);
        downRay = Physics2D.Raycast(downRayOrigin, Vector2.down, rayLength, layerMask);

        if (leftRay.collider != null && !moving)
        {
            if (leftRay.collider.CompareTag("Player"))
            {
                moving = true;
                destination = GetDestination(Vector2Int.left);
            }
        }
        if (rightRay.collider != null && !moving)
        {
            if (rightRay.collider.CompareTag("Player"))
            {
                moving = true;
                destination = GetDestination(Vector2Int.right);
            }
        }
        if (upRay.collider != null && !moving)
        {
            if (upRay.collider.CompareTag("Player"))
            {
                moving = true;
                destination = GetDestination(Vector2Int.up);
            }
        }
        if (downRay.collider != null && !moving)
        {
            if (downRay.collider.CompareTag("Player"))
            {
                moving = true;
                destination = GetDestination(Vector2Int.down);
            }
        }

        if (moving)
            HandleMovement();
    }

    Vector2 GetDestination(Vector2Int direction)
    {
        grid.GetGridPosition(transform.position, out int startX, out int startY);
        PathNode startNode = grid.GetGridObject(startX, startY);
        if (startNode == null) return transform.position;

        // left/right
        if (direction.x != 0) 
        {
            for (int x = startX; x < grid.GetWidth() && x > 0; x += direction.x)
            {
                PathNode node = grid.GetGridObject(x + direction.x, startY);
                if (node != null && !node.isWalkable)
                {
                    Vector2 destination = grid.GetWorldPosition(node.x - direction.x, node.y);
                    destination += Vector2.one * 0.5f; // fixes grid offset issue

                    if (prevNode != null)
                    {
                        prevNode.isWalkable = true;
                    }

                    PathNode destNode = grid.GetGridObject(destination);
                    destNode.isWalkable = false;

                    prevNode = destNode;

                    return destination;
                }
            }
        }
        // up/down
        else
        {
            for (int y = startY; y < grid.GetHeight() && y > 0; y += direction.y)
            {
                PathNode node = grid.GetGridObject(startX, y + direction.y);
                if (node != null && !node.isWalkable)
                {
                    Vector2 destination = grid.GetWorldPosition(node.x, node.y - direction.y);
                    destination += gridOffset; // fixes grid offset issue

                    if (prevNode != null)
                    {
                        prevNode.isWalkable = true;
                    }

                    PathNode destNode = grid.GetGridObject(destination);
                    destNode.isWalkable = false;

                    prevNode = destNode;

                    return destination;
                }
            }
        }
        return transform.position;
    }

    void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            destination,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, destination) <= 0.05f)
        {
            // reached destination
            audioSource.PlayOneShot(blockSound);
            transform.position = destination;
            moving = false;
        }
    }

    void Crush() 
    { 
        if (Vector3.Distance(transform.position, destination) <= 1 && 
            Vector3.Distance(player.position, destination) <= 1) 
        {
            gameManager.AddHealth(-1000);
            moving = false;
        } 
    }
}
