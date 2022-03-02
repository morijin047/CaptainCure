using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Attack : Action
{
    public Zombie zombieAi;
    NavMeshAgent zombieAgent;

    public override void OnAwake()
    {
        zombieAgent = zombieAi.zombieAgent;
    }

    public override TaskStatus OnUpdate()
    {
        zombieAi.zombieAgent.speed = zombieAi.InitialMaxSpeed;
        zombieAi.zombieAgent.acceleration = zombieAi.InitialAcceleration;
        zombieAi.PlayAttackAnim();
        return TaskStatus.Success;
    }
}
