using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class GoToBreakable : Action
{
    public Zombie zombieAi;
    float stopDistance = 5.0f;
    public override TaskStatus OnUpdate()
    {
        if (zombieAi.DetectBreakable)
        {
            Vector3 breakablePosition = zombieAi.DetectedBreakable.transform.position;
            float distanceToBreakable = Vector3.Distance(zombieAi.transform.position, breakablePosition);

            zombieAi.GoTo(breakablePosition);
            if (!zombieAi.DetectedBreakable.IsBreakable)
            {
                return TaskStatus.Failure;
            }
            else if (distanceToBreakable > stopDistance)
            {
                //return TaskStatus.Running;
            }
            else if (distanceToBreakable <= stopDistance)
            {
                // make undetectebale by others
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
}
