using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class DistanceToPlayer : Conditional
{
    public Zombie zombieAi;
    public Transform player;
    public override TaskStatus OnUpdate()
    {
        Vector3 distanceToPlayer = player.transform.position - zombieAi.transform.position;
        if(distanceToPlayer.magnitude < zombieAi.GetDectectionDistance())
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
