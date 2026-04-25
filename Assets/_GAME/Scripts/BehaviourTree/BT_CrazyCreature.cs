using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class BT_CrazyCreature : BTree
{
    [SerializeField] float shootRadius = 5f;
    [SerializeField] float seePlayerRadius = 9f;
    [SerializeField] float speed = 5f;
    [SerializeField] float shootCooldown = 1f;
    [SerializeField] GameObject bullet;

    protected override Node SetupTree()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new Check_PlayerNear(transform, player, shootRadius),
                new Task_Shoot(transform, player, bullet, shootCooldown),
            }),
            new Sequence(new List<Node>
            {
                new Check_PlayerNear(transform, player, seePlayerRadius),
                new Task_MoveTowardsPlayer(speed, 3f, transform, player),
            }),
            new Task_Wander(speed, transform, 1, 8),
        });

        return root;
    }
}
