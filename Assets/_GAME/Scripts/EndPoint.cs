using UnityEngine;

public class EndPoint : MonoBehaviour
{
    Player player;
    GameObject glow;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        glow = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        glow.SetActive(true);
        player.atEnd = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        glow.SetActive(false);
        player.atEnd = false;
    }
}
