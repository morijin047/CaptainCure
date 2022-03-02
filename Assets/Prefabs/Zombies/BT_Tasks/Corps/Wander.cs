using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Wander : Action
{
    public Zombie zombieAi;
    NavMeshAgent zombieAgent;
    Animator zombieAnim;
    float maxSpeed;
    float range = 100;
    Vector3 randomPlace;
    public override void OnAwake()
    {
        //base.OnAwake();
        gameObject.GetComponent<Zombie>();

        zombieAgent = zombieAi.zombieAgent;
        zombieAnim = zombieAi.zombieAnim;
        maxSpeed = zombieAgent.speed;
        randomPlace = zombieAi.transform.position + new Vector3(Random.Range(-range, range), zombieAi.transform.position.y, Random.Range(-range, range));
    }
    public override TaskStatus OnUpdate()
    {
        zombieAi.zombieAgent.speed = zombieAi.InitialMaxSpeed;
        zombieAi.zombieAgent.acceleration = zombieAi.InitialAcceleration;
        zombieAi.InChaseMode = false;
        zombieAi.SeePlayer = false;
        //zombieAi.HearNothing();
        zombieAi.ResetAngleOfVisionToNormal();

        if (zombieAgent.enabled)
        {
            if (!zombieAgent.hasPath)
            {
                randomPlace = zombieAi.transform.position + new Vector3(Random.Range(-range, range), zombieAi.transform.position.y, Random.Range(-range, range));
                zombieAgent.SetDestination(randomPlace);
                return TaskStatus.Running;
            }

            zombieAgent.SetDestination(randomPlace);
            //if (zombieAgent.remainingDistance > zombieAgent.stoppingDistance && zombieAgent.pathPending)
            //{
            //    return TaskStatus.Running;
            //}
            if (zombieAgent.remainingDistance <= zombieAgent.stoppingDistance && !zombieAgent.pathPending)
            {
                randomPlace = zombieAi.transform.position + new Vector3(Random.Range(-range, range), zombieAi.transform.position.y, Random.Range(-range, range));
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;

    }
}