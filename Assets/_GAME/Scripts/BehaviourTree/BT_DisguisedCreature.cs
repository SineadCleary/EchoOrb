using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class BT_DisguisedCreature : BTree
{
    [SerializeField] float radius = 2f;
    [SerializeField] float speed = 5f;

    protected override Node SetupTree()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new Check_Value("awake"),
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new Check_RageMode(gameManager),
                        new Task_MoveTowardsPlayer(speed, 0.5f, transform, player),
                    }),
                    new Task_Wander(speed, transform, 1f, 8f),
                }),
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
