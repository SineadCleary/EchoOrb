using BehaviourTree;
using UnityEngine;

public class Task_Shoot : Node
{
    private Transform transform;
    private Animator animator;
    private Transform player;
    private GameObject bulletPrefab;
    private float cooldown;

    private float cooldownTimer;

    public Task_Shoot(Transform transform, Transform player, GameObject bulletPrefab, float cooldown)
    {
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
        this.player = player;
        this.bulletPrefab = bulletPrefab;
        this.cooldown = cooldown;
    }

    public override NodeState Evaluate()
    {
        cooldownTimer -= Time.deltaTime;
        animator.SetBool("isMoving", false);

        if (cooldownTimer <= 0)
        {
            animator.SetTrigger("shoot");
            Shoot();
            cooldownTimer = cooldown;
        }

        state = NodeState.SUCCESS;
        return state;
    }

    private void Shoot()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        GameObject bullet = Object.Instantiate(bulletPrefab, transform.position + (Vector3)(dir * 1.1f), Quaternion.identity);
        bullet.GetComponent<Bullet>().SetMoveDirection(dir);
    }
}
