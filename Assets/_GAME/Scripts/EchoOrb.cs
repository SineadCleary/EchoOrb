using UnityEngine;
using UnityEngine.Audio;

public class EchoOrb : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] AudioClip pickupSound;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.AddOrb();
            collision.GetComponent<AudioSource>().PlayOneShot(pickupSound);
            Destroy(gameObject);
        }
    }
}
