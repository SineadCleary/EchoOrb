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

    public Task_GoTo(float speed, UnityEngine.Transform transform)
    {
        this.speed = speed;
        this.transform = transform;
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (!hasValidPath)
        {
            object data = GetData("holderPosition");
            if (data is Vector3 targetPos)
            {
                SetTargetPosition(targetPos);
                if (!hasValidPath) return NodeState.FAILURE; // pathfinding failed
            }
            else
            {
                ClearData("holder");
                ClearData("holderPosition");
                return NodeState.FAILURE;
            }
        }

        HandleMovement();

        return hasValidPath
        ? NodeState.RUNNING
        : NodeState.SUCCESS;
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
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
