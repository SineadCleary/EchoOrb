using UnityEngine;

public class SimpleEnemyAttack : MonoBehaviour
{
    GameManager gameManager;
    Player player;
    [SerializeField] float attackDistance = 1.1f;
    [SerializeField] int attackDamage = 20;
    [SerializeField] float attackCooldown = 0.5f;
    float timer = 0f;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= attackDistance && timer <= 0)
        {
            Attack();
        }
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    void Attack()
    {
        gameManager.AddHealth(-attackDamage);
        timer = attackCooldown;
    }
}
