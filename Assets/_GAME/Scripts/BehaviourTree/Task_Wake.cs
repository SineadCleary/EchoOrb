using UnityEngine;
using BehaviourTree;

public class Task_Wake : Node
{
    private Animator animator;
    private float delay = 0.5f;
    private float timeStarted;
    private bool timerStarted;

    public Task_Wake(Transform transform)
    {
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (!timerStarted)
        {
            timeStarted = Time.time;
            timerStarted = true;
            if (animator != null)
            {
                animator.SetBool("sleep", false);
                animator.SetBool("idle", true);
            }
        }

        if (Time.time - timeStarted >= delay)
        {
            SetData("awake", true);
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }
}