using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class HearSomething : Conditional
{
    public Zombie zombieAi;
    public override TaskStatus OnUpdate()
    {
        if (zombieAi.TimeHeardSound + 3 >= Time.time  && zombieAi.heardSound)
        {
            zombieAi.InChaseMode = false;
            zombieAi.SeePlayer = false;
            zombieAi.ResetAngleOfVisionToNormal();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
