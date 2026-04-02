using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class Check_CanFindHolder : Node
{
    bool occupied;

    public Check_CanFindHolder(bool occupied)
    {
        this.occupied = occupied;
    }

    public override NodeState Evaluate()
    {
        // If already have target holder SUCCESS
        if (GetData("holder") != null)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        // Else search for new target
        Holder[] allHolders = GameObject.FindObjectsByType<Holder>(FindObjectsSortMode.None);
        List<Holder> holders = new List<Holder>();

        // Don't go straight back to the last holder
        Holder lastHolder = (Holder)GetData("lastHolder");

        foreach(Holder holder in allHolders)
        {
            if (holder.powered == occupied && (lastHolder == null || holder != lastHolder))
            {
                holders.Add(holder);
            }
        }

        if (holders.Count <= 0)
        {
            state = NodeState.FAILURE;
            return state;
        }

        int randomIndex = Random.Range(0, holders.Count);
        SetData("holder", holders[randomIndex]);
        SetData("holderPosition", holders[randomIndex].transform.position);

        ClearData("lastHolder");

        state = GetData("holder") == null || GetData("holderPosition") == null ? NodeState.FAILURE : NodeState.SUCCESS;
        return state;
    }
}
