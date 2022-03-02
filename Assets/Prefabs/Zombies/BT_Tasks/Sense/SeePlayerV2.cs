using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class SeePlayerV2 : Conditional
{
    public Zombie zombieAi;
    public Transform player;
    public override TaskStatus OnUpdate()
    {
        if (zombieAi.AngleToPlayer() < zombieAi.AngleOfVision / 2 && zombieAi.DistanceToPlayer() < zombieAi.GetDectectionDistance()
            && !zombieAi.CanSeeWall())
        {
            zombieAi.SeePlayer = true;
            zombieAi.SetAngleOfVisionToTotal();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;

        //return distanceAwarenessMethod(); //doesn't care the field of view (like hearing something on distance)
    }

}
