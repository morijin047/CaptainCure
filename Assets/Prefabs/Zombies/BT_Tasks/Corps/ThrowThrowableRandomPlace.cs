using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class ThrowThrowableRandomPlace : Action
{
    public Zombie zombieAi;
    public override TaskStatus OnUpdate()
    {
        if (!zombieAi.DetectedThrowable.IsGrabbable)
        {
            return TaskStatus.Failure;
        }
        else
        {
            zombieAi.PlayThrowAnimToRandomPlace();
            return TaskStatus.Success;
        }
    }
}
