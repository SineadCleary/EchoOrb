using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class BT_DisguisedCreature : BTree
{
    [SerializeField] float radius = 2f;

    protected override Node SetupTree()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new Check_Value("awake"),
                new Task_Wander(5f, transform, 1f, 8f),
            }),
            new Sequence(new List<Node>
            {
                new Check_PlayerNear(transform, player, radius),
                new Task_Wake(transform),
            }),
            new Task_Sleep(transform),
        });

        return root;
    }
}
