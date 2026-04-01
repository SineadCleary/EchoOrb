using UnityEngine;
using BehaviourTree;

public class Task_Wake : Node
{
    private Animator animator;

    public Task_Wake(Transform transform)
    {
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (animator != null)
        {
            animator.SetBool("sleep", false);
            animator.SetBool("idle", true);
        }

        SetData("awake", true);

        state = NodeState.RUNNING;
        return state;
    }
}