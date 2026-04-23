using BehaviourTree;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task_GoTo : Node
{
    private float speed;
    private UnityEngine.Transform transform;
    private bool hasValidPath;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    private Animator animator;
    private HasOrb hasOrb;

    private Vector3 currentTarget;
    private bool reachedTarget;

    public Task_GoTo(float speed, UnityEngine.Transform transform, HasOrb hasOrb)
    {
        this.speed = speed;
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
        this.hasOrb = hasOrb;
    }

    public override NodeState Evaluate()
    {
        if (hasOrb.targetHolder == null)
            return NodeState.FAILURE;

        Holder holder = hasOrb.targetHolder;

        Vector3 newTarget = holder.transform.position;

        if (!hasValidPath || newTarget != currentTarget)
        {
            reachedTarget = false;
            currentTarget = newTarget;

            SetTargetPosition(currentTarget);

            if (!hasValidPath)
                return NodeState.FAILURE;

        }

        if (reachedTarget)
        {
            return NodeState.SUCCESS;
        }

        HandleMovement();

        if (reachedTarget)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
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
                    // Flip sprite
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
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList == null || pathVectorList.Count == 0)
        {
            hasValidPath = false;
            return;
        }

        if (pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }

        hasValidPath = true;
    }

    private void StopMoving()
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", false);
        }
        pathVectorList = null;
        hasValidPath = false;
        reachedTarget = true;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
