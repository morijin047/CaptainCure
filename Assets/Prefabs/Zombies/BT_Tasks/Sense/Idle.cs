using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class Idle : Action
{
    public override TaskStatus OnUpdate()
    {
        //zombieAnim.SetBool("Idle");
        return TaskStatus.Failure;
    }
}
