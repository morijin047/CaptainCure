using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class TrackPlayer : Action
{
    public Transform player;
    public override TaskStatus OnUpdate()
    {
        Vector3 direction = player.transform.position - Zombie.Instance.transform.position;
        Zombie.Instance.transform.rotation = Quaternion.Slerp(Zombie.Instance.transform.rotation, 
            Quaternion.LookRotation(direction), 5 * Time.deltaTime);
        return TaskStatus.Success;
    }
}
