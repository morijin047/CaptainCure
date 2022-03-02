using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class PlayerNotTooFar : Conditional
{
    public Zombie zombieAi;
    public Transform player;
    public float margin = 2.0f;
    public override TaskStatus OnUpdate()
    {
        Vector3 distanceFromPlayer = player.position - zombieAi.transform.position;
        if (distanceFromPlayer.magnitude > zombieAi.GetDectectionDistance() + margin)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}
