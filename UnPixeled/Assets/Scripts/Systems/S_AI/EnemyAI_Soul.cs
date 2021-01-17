//Copyright Ex/IO 2020

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Systems.S_HealthStats;
using Core.Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyAI_Soul : MonoBehaviour
{
    [Header("AI Settings")] [SerializeField]
    public float lookRadius;

    [SerializeField] public float runRadius;
    [SerializeField] public float attackRadius;
    [SerializeField] public float chaseRadius;

    public enum AiType
    {
        melee,
        range
    }

    public AiType ai_Type;

    [Header("Parameters")] [SerializeField]
    public float damage;

    [SerializeField] public float attackDelay;
    private float lastAttacked = -9999;

    [SerializeField] public float shootForce;
    [SerializeField] public GameObject projectile;
    [SerializeField] public Transform pfPojectile;

    private NavMeshAgent _navMeshAgent;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }


    private void Awake()
    {
        EventManager.playerSpottedByEnemy.AddListener(PlayerSpottedByEmeny);
    }


    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (!GetComponent<HealthStatsActor>().dead)
            ActorActions();
    }


    private void PlayerSpottedByEmeny(Transform _nearbyEnemy)
    {
        if (_navMeshAgent != null)
        {
            float distance = Vector3.Distance(transform.position, _nearbyEnemy.position);
            if (distance <= chaseRadius)
            {
                _navMeshAgent.SetDestination(GameManager.instance.playerBehaviour.transform.position);
            }
        }
    }

    private void ActorActions()
    {
        if (GameManager.instance.playerBehaviour != null)
        {
            float distance =
                Vector3.Distance(GameManager.instance.playerBehaviour.transform.position,
                    transform.position); // Count distance

            if (distance <= lookRadius)
            {
                RaycastHit hit;
                Ray rayToPlayer = new Ray(transform.position,
                    GameManager.instance.playerBehaviour.transform.position - transform.position);
                Physics.Raycast(rayToPlayer, out hit);
                if (hit.collider != null)
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        ActorMove(distance);
                        ActorAttack(distance);
                        EventManager.playerSpottedByEnemy.Invoke(transform);
                    }
            }
        }
    }

    private void ActorMove(float distance)
    {
        if (distance <= runRadius)
            switch (ai_Type)
            {
                case AiType.melee:

                    _navMeshAgent.SetDestination(GameManager.instance.playerBehaviour.transform.position);

                    break;


                case AiType.range:

                    Transform startTransform = transform;
                    transform.rotation =
                        Quaternion.LookRotation(transform.position -
                                                GameManager.instance.playerBehaviour.transform.position);
                    Vector3 runTo = transform.position + transform.forward * distance;

                    NavMeshHit hit;
                    NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));

                    transform.position = startTransform.position;
                    transform.rotation = startTransform.rotation;

                    _navMeshAgent.SetDestination(hit.position);
                    break;
            }
    }

    private void ActorAttack(float distance)
    {
        if (Time.time > lastAttacked + attackDelay)
            switch (ai_Type)
            {
                case AiType.melee:
                    if (distance <= attackRadius)
                    {
                        EventStats.changePlayerHealth.Invoke(-damage);
                        lastAttacked = Time.time;
                    }

                    break;

                case AiType.range:
                    if (distance <= attackRadius)
                    {
                        EventAudio.rangeSoulShoot.Invoke(GetComponent<AudioSource>());
                        transform.rotation =
                            Quaternion.LookRotation(transform.position -
                                                    GameManager.instance.playerBehaviour.transform.position);
                        GameObject bullet = Instantiate(projectile, pfPojectile.transform.position,
                            pfPojectile.transform.rotation);
                        bullet.GetComponent<ProjectileBehaviour>().damage = damage;
                        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * -shootForce);
                        lastAttacked = Time.time;
                    }

                    break;
            }
    }
}