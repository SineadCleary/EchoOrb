using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class Check_CanFindHolder : Node
{
    bool occupied;
    private Holder[] allHolders;
    HasOrb hasOrb;

    public Check_CanFindHolder(bool occupied, HasOrb hasOrb)
    {
        this.occupied = occupied;
        this.hasOrb = hasOrb;
    }

    public override NodeState Evaluate()
    {
        if (allHolders == null || allHolders.Length == 0)
        {
            allHolders = GameObject.FindObjectsByType<Holder>(FindObjectsSortMode.None);
        }

        // If already have target holder SUCCESS
        Holder currentHolder = hasOrb.targetHolder;
        if (currentHolder != null)
        {
            if (currentHolder.powered == occupied)
            {
                return NodeState.SUCCESS;
            }
            else
            { 
                hasOrb.targetHolder = null;
            }
        }

        // Else search for new target
        List<Holder> holders = new List<Holder>();

        // Don't go straight back to the last holder
        Holder lastHolder = hasOrb.lastHolder;

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
        hasOrb.targetHolder = holders[randomIndex];

        return NodeState.SUCCESS;
    }
}
