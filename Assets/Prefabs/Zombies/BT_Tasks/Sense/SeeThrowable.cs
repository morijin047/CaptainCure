using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class SeeThrowable : Conditional
{
    public Zombie zombieAi;
    public Transform player;

    public override TaskStatus OnUpdate()
    {
        if (zombieAi.DetectThrowable)
        {
            //zombieAi.NotInChaseMode(); 
            //zombieAi.NotSeePlayer();
            //zombieAi.HearNothing();
            //zombieAi.DetectNothing();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
