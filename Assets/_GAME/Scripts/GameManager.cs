using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI orbsText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] AudioClip playerHurtSound;
    AudioSource audioSource;
    public int numOrbs {  get; private set; }
    public int playerHealth { get; private set; } = 100;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (healthPoints < 0)
        {
            audioSource.PlayOneShot(playerHurtSound);
        }
        playerHealth += healthPoints;
        if (playerHealth > 100) playerHealth = 100;
        else if (playerHealth < 0)
        {
            playerHealth = 0;
            PlayerDeath();
        }
        healthText.text = "Health: " + playerHealth;
        return playerHealth;
    }

    void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
