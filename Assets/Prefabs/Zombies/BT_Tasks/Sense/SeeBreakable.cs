using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class SeeBreakable : Conditional
{
    public Zombie zombieAi;

    public override TaskStatus OnUpdate()
    {
        if (zombieAi.DetectBreakable)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
