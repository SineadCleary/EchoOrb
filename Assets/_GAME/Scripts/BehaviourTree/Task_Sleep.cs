using UnityEngine;
using BehaviourTree;

public class Task_Sleep : Node
{
    private Animator animator;

    public Task_Sleep(Transform transform)
    {
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("Sleep");
        if (animator != null)
        {
            animator.SetBool("idle", false);
            animator.SetBool("sleep", true);
        }

        state = NodeState.RUNNING;
        return state;
    }
}