using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class PlayerIsNear : Conditional
{
    public Zombie zombieAi;
    public Transform player;
    public override TaskStatus OnUpdate()
    {
        float d = Vector3.Distance(zombieAi.transform.position, player.position);
        if (Vector3.Distance(zombieAi.transform.position, player.position) <= zombieAi.DistanceToAttack)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
