using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class NotSawBefore : Conditional
{
    public Zombie zombieAi;
    public override TaskStatus OnUpdate()
    {
        if (!zombieAi.SeePlayer)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
