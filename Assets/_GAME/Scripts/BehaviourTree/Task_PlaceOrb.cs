using UnityEngine;
using BehaviourTree;

public class Task_PlaceOrb : Node
{
    private CreatureHealth health;
    HasOrb hasOrb;
    public Task_PlaceOrb(Transform transfomrm, CreatureHealth health, HasOrb hasOrb) {
        this.health = health;
        this.hasOrb = hasOrb;
    }

    public override NodeState Evaluate()
    {
        Holder holder = hasOrb.targetHolder;

        if (holder == null || holder.powered || !hasOrb.GetHasOrb())
        {
            // invalidate target and fail
            hasOrb.targetHolder = null;
            hasOrb.lastHolder = null;
            state = NodeState.FAILURE;
            return state;
        }

        Debug.Log("Place");
        hasOrb.SetHasOrb(false);
        holder.SetPowered(true);
        health.dropsOrb = false;

        hasOrb.lastHolder = hasOrb.targetHolder;
        hasOrb.targetHolder = null;

        state = NodeState.SUCCESS;
        return state;
    }
}

public class Task_TakeOrb : Node
{
    private CreatureHealth health;
    HasOrb hasOrb;
    public Task_TakeOrb(Transform transform, CreatureHealth health, HasOrb hasOrb)
    {
        this.health = health;
        this.hasOrb = hasOrb;
    }

    public override NodeState Evaluate()
    {
        Holder holder = hasOrb.targetHolder;

        if (holder == null || !holder.powered || hasOrb.GetHasOrb())
        {
            // invalidate target and fail
            hasOrb.targetHolder = null;
            hasOrb.lastHolder = null;
            state = NodeState.FAILURE;
            return state;
        }

        Debug.Log("Take");
        hasOrb.SetHasOrb(true);
        holder.SetPowered(false);
        health.dropsOrb = true;

        hasOrb.lastHolder = hasOrb.targetHolder;
        hasOrb.targetHolder = null;

        state = NodeState.SUCCESS;
        return state;
    }
}
