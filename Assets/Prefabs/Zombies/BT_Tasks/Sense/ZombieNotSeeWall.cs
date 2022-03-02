using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class ZombieNotSeeWall : Conditional
{
    public Zombie zombieAi;
    public Transform player;
    public override TaskStatus OnUpdate()
    {
        Vector3 directionToPlayer = player.position - zombieAi.transform.position;
        bool canSeeWall = false;
        if (zombieAi.InChaseMode)
        {
            RaycastHit rch;
            if (Physics.Raycast(zombieAi.transform.position, directionToPlayer, out rch))
            {
                //Debug.Log("I see: " + rch.collider.name);
                if (rch.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                //rch.collider.bounds.center;
                {
                    canSeeWall = true;
                }
            }
        }
        else
        {
            RaycastHit[] rchs = Physics.RaycastAll(zombieAi.transform.position, directionToPlayer.normalized, directionToPlayer.magnitude);
            foreach (RaycastHit rch in rchs)
            {
                if (rch.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    canSeeWall = true;
                }
            }
        }
        if (!canSeeWall)
        {
            zombieAi.SeePlayer = true;
            zombieAi.SetAngleOfVisionToTotal();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
