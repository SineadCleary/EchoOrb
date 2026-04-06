using UnityEngine;
using BehaviourTree;

public class Task_PlaceOrb : Node
{
    private Animator animator;
    public Task_PlaceOrb(Transform transfomrm) {
        animator = transfomrm.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Holder holder = (Holder)GetData("holder");

        object hasOrbObj = GetData("hasOrb");
        bool hasOrb = hasOrbObj != null && (bool)hasOrbObj;

        if (holder == null || holder.powered || !hasOrb)
        {
            // invalidate target and fail
            ClearData("holder");
            ClearData("holderPosition");
            ClearData("lastHolder");
            state = NodeState.FAILURE;
            return state;
        }

        Debug.Log("Place");
        SetData("hasOrb", false);
        holder.SetPowered(true);

        if (animator != null)
        {
            animator.SetBool("hasOrb", false);
        }

        SetData("lastHolder", holder);
        ClearData("holder");
        ClearData("holderPosition");

        state = NodeState.SUCCESS;
        return state;
    }
}

public class Task_TakeOrb : Node
{
    private Animator animator;
    public Task_TakeOrb(Transform transform)
    {
        animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Holder holder = (Holder)GetData("holder");

        object hasOrbObj = GetData("hasOrb");
        bool hasOrb = hasOrbObj != null && (bool)hasOrbObj;

        if (holder == null || !holder.powered || hasOrb)
        {
            // invalidate target and fail
            ClearData("holder");
            ClearData("holderPosition");
            ClearData("lastHolder");
            state = NodeState.FAILURE;
            return state;
        }

        Debug.Log("Take");
        SetData("hasOrb", true);
        holder.SetPowered(false);

        if (animator != null)
        {
            animator.SetBool("hasOrb", true);
        }

        SetData("lastHolder", holder);
        ClearData("holder");
        ClearData("holderPosition");

        state = NodeState.SUCCESS;
        return state;
    }
}
