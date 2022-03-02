using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;

public class SeePlayer : Conditional
{
    public Zombie zombieAi;
    public Transform player; //singleton later

    public override TaskStatus OnUpdate()
    {
        //Aim to where player is
        Vector3 distance = player.position - zombieAi.transform.position;
        float angle = Vector3.Angle(distance, zombieAi.transform.forward);
        //Debug.Log(distance.magnitude);
        if (angle < zombieAi.AngleOfVision / 2 && distance.magnitude < zombieAi.GetDectectionDistance() )
        {
            //zombieAi.SetAngleOfVisionToTotal();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;

        //return distanceAwarenessMethod(); //doesn't care the field of view (like hearing something on distance)
    }

    private TaskStatus distanceAwarenessMethod()
    {
        Vector3 directionToPlayer = player.position - Zombie.Instance.transform.position;
        Debug.DrawLine(Zombie.Instance.transform.position, directionToPlayer, Color.red);
        bool canSeeWall = false;
        RaycastHit rch;
        if (Physics.Raycast(Zombie.Instance.transform.position, directionToPlayer, out rch))
        {
            //Debug.Log("I see: " + rch.collider.name);
            if (rch.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                canSeeWall = true;
            }
        }
        if (directionToPlayer.magnitude < Zombie.Instance.GetDectectionDistance())
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
