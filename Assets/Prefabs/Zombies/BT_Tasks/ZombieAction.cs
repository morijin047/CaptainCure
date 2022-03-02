using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class ZombieAction : Action
{
    protected Zombie zombieAi;
    
    public override void OnAwake()
    {
        base.OnAwake();
        zombieAi = gameObject.GetComponent<Zombie>();
    }
}
