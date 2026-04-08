using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI orbsText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] AudioClip playerHurtSound;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    AudioSource audioSource;
    GameObject player;
    public int numOrbs {  get; private set; }
    public int playerHealth { get; private set; } = 100;

    private void Start()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public int AddOrb()
    {
        numOrbs++;
        orbsText.text = "Orbs: " + numOrbs;
        return numOrbs;
    }

    public int RemoveOrb()
    {
        numOrbs--;
        orbsText.text = "Orbs: " + numOrbs;
        return numOrbs;
    }

    public int AddHealth(int healthPoints)
    {
        // Minus - Damage
        if (healthPoints < 0)
        {
            player.GetComponent<Player>().Hurt();
            audioSource.PlayOneShot(playerHurtSound);
        }
        playerHealth += healthPoints;
        if (playerHealth > 100) playerHealth = 100;
        else if (playerHealth <= 0)
        {
            playerHealth = 0;
            PlayerDeath();
            player.GetComponent<Player>().Die();
        }
        healthText.text = "Health: " + playerHealth;
        return playerHealth;
    }

    public void Win()
    {
        // Player
        if (player != null)
        {
            player.GetComponent<Player>().enabled = false;
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        }

        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    void PlayerDeath()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
