using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;

public class ChasePlayer : Action
{
    NavMeshAgent zombieAgent;
    public Zombie zombieAi;
    public Transform target;
    public float accelerationIncrementRate = .001f;
    public float maxSpeedValue = 7;
    public override void OnAwake()
    {
        zombieAgent = zombieAi.zombieAgent;
    }
    public override TaskStatus OnUpdate()
    {
        zombieAi.InChaseMode = true;
        Vector3 direction = target.transform.position - zombieAi.transform.position;

        if (Vector3.Distance(zombieAi.transform.position, target.position) > zombieAi.DistanceToAttack)
        {
            zombieAi.GoTo(target.position);
            //zombieAi.zombieAgent.speed = maxSpeedValue;
            //zombieAi.zombieAgent.acceleration += accelerationIncrementRate;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
