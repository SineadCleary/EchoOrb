using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

public class BT_MischeviousCreature : BTree
{
    [SerializeField] float speed = 5f;
    [SerializeField] CreatureHealth health;
    [SerializeField] HasOrb hasOrb;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new Check_HasOrb(hasOrb), // if has orb
                new Check_CanFindHolder(false, hasOrb), // empty holder
                new Task_GoTo(speed, transform, hasOrb),
                new Task_PlaceOrb(transform, health, hasOrb),
            }),
            new Sequence(new List<Node>
            {
                new Inverter(
                    new Check_HasOrb(hasOrb)), // if not has orb
                new Check_CanFindHolder(true, hasOrb), // occupied holder
                new Task_GoTo(speed, transform, hasOrb),
                new Task_TakeOrb(transform, health, hasOrb),
            }),
            new Task_Wander(speed, transform, 1, 5),
        });

        return root;
    }
}