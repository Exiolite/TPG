using Systems.Stats;
using Core;
using UnityEngine.AI;

namespace Systems.Actor.Actors
{
    public class MeleeActorBehaviour : ActorBehaviour
    {
        protected override void Move(ref NavMeshAgent navMeshAgent)
        {
            navMeshAgent.SetDestination(GameManager.instance.playerBehaviour.transform.position);
        }

        protected override void Attack(float damage)
        {
            EventStats.ChangePlayerHealth.Invoke(damage);
        }
    }
}