using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
public class GoToThrowable : Action
{
    public Zombie zombieAi;
    float stopDistance = 2.0f;

    public override TaskStatus OnUpdate()
    {
        if (zombieAi.HasDetectedThrowable())
        {
            Vector3 throwablePosition = zombieAi.DetectedThrowable.transform.position;
            float distanceToThrowable = Vector3.Distance(zombieAi.transform.position, throwablePosition);

            zombieAi.GoTo(throwablePosition);

            if (!zombieAi.DetectedThrowable.IsGrabbable)
            {
                return TaskStatus.Failure;
            }

            if (distanceToThrowable > stopDistance)
            {
                //return TaskStatus.Running;
            }

            else if (distanceToThrowable <= stopDistance)
            {
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
}
