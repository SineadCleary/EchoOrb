using UnityEngine;
using System.Collections.Generic;
using BehaviourTree;

public class BT_SimpleCreature : BTree
{
    protected override Node SetupTree()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new Check_RageMode(gameManager),
                new Task_MoveTowardsPlayer(5f, 0.5f, transform, player),
            }),
            new Task_Wander(5f, transform, 1f, 8f),
        });

        return root;
    }
}
