using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;

public class GoToSoundSource : Action
{
    public Zombie zombieAi;

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(zombieAi.transform.position, zombieAi.GetSoundHeard()) > 5.0f)
        {
            zombieAi.GoTo(zombieAi.GetSoundHeard());
            return TaskStatus.Running;
        } else if (Vector3.Distance(zombieAi.transform.position, zombieAi.GetSoundHeard()) <= 5.0f)
        {
            zombieAi.heardSound = false;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
