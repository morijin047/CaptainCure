using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class StartTimerToWander : Conditional
{
    public Zombie zombieAi;
    float timeToWander = 0.0f;
    public override TaskStatus OnUpdate()
    {
        this.timeToWander += Time.deltaTime;
        Debug.Log(this.timeToWander);
        if (this.timeToWander < zombieAi.TimeToWanderAfterLosingSightOfPlayer)
        {
            return TaskStatus.Success;
        } else
        {
            this.timeToWander = 0.0f;
            zombieAi.SeePlayer = false;
            return TaskStatus.Failure;
        }
    }
}
