using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        collision.GetComponent<Player>().enabled = false;
        collision.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        collision.transform.position = transform.position;
    }
}
