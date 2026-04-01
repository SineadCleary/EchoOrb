using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowClick : MonoBehaviour
{
    private const float speed = 5f;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    private void Update()
    {
        HandleMovement();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SetTargetPosition(GetMouseWorldPosition());
        }
    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.05f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    speed * Time.deltaTime
                );
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(mouseScreen);
        world.z = 0f;
        return world;
    }

}