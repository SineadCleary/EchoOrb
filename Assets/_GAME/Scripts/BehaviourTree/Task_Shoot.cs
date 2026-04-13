using BehaviourTree;
using UnityEngine;

public class Task_Shoot : Node
{
    private Transform transform;
    private Animator animator;
    private Transform player;
    private GameObject bulletPrefab;
    private AudioSource audioSource;
    private AudioClip shootSound;
    private float cooldown;
    LayerMask layerMask;

    private float cooldownTimer;

    public Task_Shoot(Transform transform, Transform player, GameObject bulletPrefab, AudioClip shootSound, float cooldown)
    {
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
        this.player = player;
        this.bulletPrefab = bulletPrefab;
        this.cooldown = cooldown;
        audioSource = transform.GetComponent<AudioSource>();
        this.shootSound = shootSound;
        layerMask = LayerMask.GetMask("Player", "NonWalkable", "Trap");
    }

    public override NodeState Evaluate()
    {
        cooldownTimer -= Time.deltaTime;
        animator.SetBool("isMoving", false);

        if (cooldownTimer <= 0)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, 20, layerMask);
            if (ray.collider == null || ray.collider.transform != player)
            {
                state = NodeState.FAILURE;
                return state;
            }
            animator.SetTrigger("shoot");
            Shoot();
            cooldownTimer = cooldown;
        }

        state = NodeState.SUCCESS;
        return state;
    }

    private void Shoot()
    {
        if ( audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        Vector2 dir = (player.position - transform.position).normalized;
        GameObject bullet = Object.Instantiate(bulletPrefab, transform.position + (Vector3)(dir * 1.1f), Quaternion.identity);
        bullet.GetComponent<Bullet>().SetMoveDirection(dir);
    }
}
