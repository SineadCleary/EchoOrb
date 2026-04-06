using BehaviourTree;
using UnityEngine;

public class Task_SetAnimationBool : Node
{
    string param;
    bool value;
    Animator animator;

    public Task_SetAnimationBool(string param, bool value, Transform transform)
    {
        this.param = param;
        this.value = value;
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (animator != null)
        {
            animator.SetBool(param, value);
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
