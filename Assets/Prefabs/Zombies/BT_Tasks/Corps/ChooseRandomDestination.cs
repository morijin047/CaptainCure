using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class ChooseRandomDestination : Action
{
    NavMeshAgent zombieAgent;

    public override void OnAwake()
    {
        zombieAgent = Zombie.Instance.GetComponent<NavMeshAgent>();
    }
    public override TaskStatus OnUpdate()
    {
        Vector3 randomPlace = new Vector3(Random.Range(-300, 300), 0, Random.Range(-300, 300));
        zombieAgent.SetDestination(randomPlace);
        return TaskStatus.Success;
    }
}
