using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class BT_MischeviousCreature : BTree
{
    [SerializeField] float speed = 5f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new Check_Value("hasOrb"), // if has orb
                new Task_SetAnimationBool("hasOrb", true, transform),
                new Check_CanFindHolder(false), // empty holder
                new Task_GoTo(speed, transform),
                new Task_PlaceOrb(transform),
            }),
            new Sequence(new List<Node>
            {
                new Inverter(
                    new Check_Value("hasOrb")), // if not has orb
                new Task_SetAnimationBool("hasOrb", false, transform),
                new Check_CanFindHolder(true), // occupied holder
                new Task_GoTo(speed, transform),
                new Task_TakeOrb(transform),
            }),
            new Task_Wander(speed, transform, 1, 5),
        });

        return root;
    }
}
