using UnityEngine;

public class Enrage : MonoBehaviour
{
    GameManager gameManager;
    Player player;
    Animator animator;
    [SerializeField] float attackDistance = 1f;
    [SerializeField] int attackDamage = 20;
    [SerializeField] float attackCooldown = 1.1f;
    float timer = 0f;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameManager.rageMode)
        {
            animator.SetBool("rage", true);
            if (Vector2.Distance(player.transform.position, transform.position) <= attackDistance && timer <= 0)
            {
                Attack();
            }
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
            }
        }
        else
            animator.SetBool("rage", false);
    }

    void Attack()
    {
        gameManager.AddHealth(-attackDamage);
        timer = attackCooldown;
    }
}
