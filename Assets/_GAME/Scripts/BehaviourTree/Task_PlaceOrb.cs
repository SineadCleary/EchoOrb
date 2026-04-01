using UnityEngine;
using BehaviourTree;

public class Task_PlaceOrb : Node
{
    public Task_PlaceOrb() { }

    public override NodeState Evaluate()
    {
        Holder holder = (Holder)GetData("targetHolder");
        if (holder == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if (holder.powered)
        {
            state = NodeState.FAILURE;
            return state;
        }

        holder.SetPowered(true);

        SetData("hasOrb", false);

        // remove if want to check previous holder ??
        //ClearData("targetHolder");

        state = NodeState.SUCCESS;
        return state;
    }
}

public class Task_TakeOrb : Node
{
    public Task_TakeOrb() { }

    public override NodeState Evaluate()
    {
        Holder holder = (Holder)GetData("targetHolder");
        if (holder == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if (!holder.powered)
        {
            state = NodeState.FAILURE;
            return state;
        }

        holder.SetPowered(false);

        SetData("hasOrb", true);

        state = NodeState.SUCCESS;
        return state;
    }
}
