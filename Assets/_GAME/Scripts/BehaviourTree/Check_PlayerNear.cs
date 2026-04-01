using UnityEngine;
using BehaviourTree;

public class Check_PlayerNear : Node
{
    private Transform myTransform;
    private Transform playerTransform;
    private float detectionRadius;

    public Check_PlayerNear(Transform myTransform, Transform playerTransform, float radius)
    {
        this.myTransform = myTransform;
        this.playerTransform = playerTransform;
        detectionRadius = radius;
    }

    public override NodeState Evaluate()
    {
        if (playerTransform == null || myTransform == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        float distance = Vector2.Distance(myTransform.position, playerTransform.position);

        if (distance <= detectionRadius) state = NodeState.SUCCESS;
        else state = NodeState.FAILURE;
        
        return state;
    }
}
