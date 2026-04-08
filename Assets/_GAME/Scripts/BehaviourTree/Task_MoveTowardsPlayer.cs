using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class Task_MoveTowardsPlayer : Node
{
    private float speed;
    private float stopDistance;
    private Transform transform;
    private Transform player;
    private Animator animator;

    private float repathTimer = 0f;
    private float repathRate = 0.25f;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    public Task_MoveTowardsPlayer(float speed, float stopDistancem, Transform transform, Transform player)
    {
        this.speed = speed;
        this.stopDistance = stopDistancem;
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
        this.player = player;
    }

    public override NodeState Evaluate()
    {
        if (player == null)
            return NodeState.FAILURE;

        repathTimer -= Time.deltaTime;
        if (repathTimer < 0f)
        {
            repathTimer = repathRate;
            SetTargetPosition(player.position);
        }

        if (Vector3.Distance(transform.position, player.position) <= stopDistance)
        {
            animator?.SetBool("isMoving", false);
            state = NodeState.SUCCESS;
            return state;
        }

        HandleMovement();

        state = NodeState.RUNNING;
        return state;
    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.05f)
            {
                if (animator != null)
                {
                    animator.SetBool("isMoving", true);

                    // Flip sprite
                    Vector3 moveDir = (targetPosition - transform.position).normalized;
                    if (moveDir.x > 0) transform.localScale = new Vector3(-1, 1, 1);
                    else if (moveDir.x < 0) transform.localScale = new Vector3(1, 1, 1);
                }
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    speed * Time.deltaTime
                );
            }
            else
            {
                // reached node
                transform.position = targetPosition;
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    // reached final node
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving()
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", false);
        }
        pathVectorList = null;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(transform.position, targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
}
