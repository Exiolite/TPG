using Systems.Projectiles;
using Core;
using UnityEngine;
using UnityEngine.AI;

namespace Systems.Actor.Actors
{
    public class RangeActorBehaviour : ActorBehaviour
    {
        [SerializeField] private ProjectileBehaviour projectileBehaviour;
        
        protected override void Move(ref NavMeshAgent navMeshAgent)
        {
            Transform startTransform = navMeshAgent.transform;
            
            transform.rotation = Quaternion.LookRotation(transform.position - GameManager.instance.playerBehaviour.transform.position);
            
            Vector3 runTo = transform.position + transform.forward * 5;

            NavMeshHit hit;
            NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));

            transform.position = startTransform.position;
            transform.rotation = startTransform.rotation;

            navMeshAgent.SetDestination(hit.position);
        }

        protected override void Attack(float damage)
        {
            projectileBehaviour.Damage = damage;
            Instantiate(projectileBehaviour);
        }
    }
}