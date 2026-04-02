using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class BT_MischeviousCreature : BTree
{
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new Check_Value("hasOrb"), // if has orb
                new Check_CanFindHolder(false), // empty holder
                new Task_GoTo(5f, transform),
                new Task_PlaceOrb(),
            }),
            new Sequence(new List<Node>
            {
                new Inverter(new Check_Value("hasOrb")), // if not has orb
                new Check_CanFindHolder(true), // occupied holder
                new Task_GoTo(5f, transform),
                new Task_TakeOrb(),
            }),
            new Task_Wander(5f, transform, 1, 5),
        });

        return root;
    }
}
