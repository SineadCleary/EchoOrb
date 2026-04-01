using BehaviourTree;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task_Wander : Node
{
    private UnityEngine.Transform transform;

    private float speed;

    private float waitTime; // in seconds
    private float minWait;
    private float maxWait;
    private float waitCounter = 0f;
    private bool waiting = true;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    public Task_Wander(float speed, UnityEngine.Transform transform, float minWait, float maxWait)
    {
        this.speed = speed;
        this.transform = transform;
        this.minWait = minWait;
        this.maxWait = maxWait;
        waitTime = Random.Range(minWait, maxWait);
    }

    public override NodeState Evaluate()
    {
        if (waiting)
        {
            //Debug.Log("waiting");
            waitCounter += Time.deltaTime;
            if (waitCounter > waitTime)
            {
                //Debug.Log("done: " + waitTime);
                waiting = false;
                SetRandomTarget();
            }
        }
        else
        {
            //Debug.Log("moving");
            HandleMovement();
        }

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
                //Vector3 moveDir = (targetPosition - transform.position).normalized; // used for animation
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
        pathVectorList = null;
        waitTime = Random.Range(minWait, maxWait);
        waitCounter = 0;
        waiting = true;
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    private void SetRandomTarget()
    {
        Pathfinding pathfinding = Pathfinding.Instance;
        MyGrid<PathNode> grid = pathfinding.GetGrid();

        const int maxAttempts = 50; // avoid infinite loops
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            // 1
            //int x = (int)transform.position.x + Random.Range(-5, 6);
            //int y = (int)transform.position.y + Random.Range(-5, 6);

            //x = Mathf.Clamp(x, (int)grid.GetOrigin().x, grid.GetWidth() - 1);
            //y = Mathf.Clamp(y, (int)grid.GetOrigin().y, grid.GetHeight() - 1);

            // 2
            //int x = Random.Range((int)grid.GetOrigin().x, grid.GetWidth());
            //int y = Random.Range((int)grid.GetOrigin().y, grid.GetHeight());

            int x, y;
            Vector3 randomOffset = Random.insideUnitCircle * 4;
            randomOffset.z = 0;
            grid.GetGridPosition(transform.position + randomOffset, out x, out y);

            PathNode node = grid.GetGridObject(x, y);

            // check if node is walkable
            if (node != null && node.isWalkable)
            {
                Vector3 worldPos = grid.GetWorldPosition(x, y);

                // try to find a path from current position
                List<Vector3> testPath = pathfinding.FindPath(transform.position, worldPos);

                if (testPath != null && testPath.Count > 0)
                {
                    // valid target found
                    SetTargetWorldPosition(worldPos);
                    //Debug.Log(worldPos);
                    //Debug.DrawRay(transform.position, worldPos, Color.red, 4f);
                    return;
                }
            }
            attempts++;
        }
        Debug.LogWarning("Wander task: failed to find a valid target after " + maxAttempts + " attempts.");
    }

    public void SetTargetWorldPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetCurrentPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
}
