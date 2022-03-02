using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class Stunned : Conditional
{
    public Zombie zombieAi;
    public override TaskStatus OnUpdate()
    {
        return zombieAi.IsStunned ? TaskStatus.Success : TaskStatus.Failure;
    }
}
