using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI orbsText;
    [SerializeField] TextMeshProUGUI healthText;
    public int numOrbs {  get; private set; }
    public int playerHealth { get; private set; } = 100;


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
