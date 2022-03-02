using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class SawAndNotSee : Conditional
{
    public Zombie zombieAi;
    public Transform player;
    public override TaskStatus OnUpdate()
    {
        zombieAi.zombieAgent.speed = zombieAi.InitialMaxSpeed;
        zombieAi.zombieAgent.acceleration = zombieAi.InitialAcceleration;
        Vector3 directionToPlayer = player.position - zombieAi.transform.position;
        bool canSeeWall = false;
        RaycastHit [] rchs = Physics.RaycastAll(zombieAi.transform.position, directionToPlayer, directionToPlayer.magnitude);
        foreach (RaycastHit rch in rchs)
        {
            if (rch.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                canSeeWall = true;
            }
        }

        if (zombieAi.SeePlayer && canSeeWall)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
