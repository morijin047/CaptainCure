using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class BreakBreakable : Action
{
    public Zombie zombieAi;
    public override TaskStatus OnUpdate()
    {
        if (!zombieAi.DetectedBreakable.IsBreakable)
        {
            return TaskStatus.Failure;
        }
        else
        {
            zombieAi.PlayBreakAnim();
            return TaskStatus.Success;
        }

    }
}
