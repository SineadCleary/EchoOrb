using UnityEngine;
using BehaviourTree;

public class Task_PlaceOrb : Node
{
    private Animator animator;
    private CreatureHealth health;
    public Task_PlaceOrb(Transform transfomrm, CreatureHealth health) {
        animator = transfomrm.GetComponent<Animator>();
        this.health = health;
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
        health.dropsOrb = false;

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
    private CreatureHealth health;
    public Task_TakeOrb(Transform transform, CreatureHealth health)
    {
        animator = transform.GetComponent<Animator>();
        this.health = health;
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
        health.dropsOrb = true;

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
