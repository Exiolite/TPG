//Copyright Ex/IO 2020
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyAI_Soul : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] public float lookRadius;
    [SerializeField] public float runRadius;
    [SerializeField] public float attackRadius;
    [SerializeField] public float chaseRadius;
    public enum AI_Type { melee, range }
    public AI_Type ai_Type;

    [Header("Parameters")]
    [SerializeField] public float damage;
    [SerializeField] public float attackDelay;
    float lastAttacked = -9999;

    [SerializeField] public float shootForce;
    [SerializeField] public GameObject projectile;
    [SerializeField] public Transform pfPojectile;

    NavMeshAgent navMeshAgent;



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }



    private void Awake()
    {
        EventGame.playerSpottedByEnemy.AddListener(PlayerSpottedByEmeny);
    }



    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }



    void Update()
    {
        if (!GetComponent<HealthStats_actor>().dead)
            ActorActions();
    }





    private void PlayerSpottedByEmeny(Transform _nearbyEnemy)
    {
        if (navMeshAgent != null)
        {
            float distance = Vector3.Distance(transform.position, _nearbyEnemy.position);
            if (distance <= chaseRadius)
            {
                navMeshAgent.SetDestination(GameManager.instance.playerManager.transform.position);
            }
        }
    }

    private void ActorActions()
    {
        if (GameManager.instance.playerManager != null)
        {
            float distance = Vector3.Distance(GameManager.instance.playerManager.transform.position, transform.position); // Count distance

            if (distance <= lookRadius)
            {
                RaycastHit hit;
                Ray rayToPlayer = new Ray(transform.position, GameManager.instance.playerManager.transform.position - transform.position);
                Physics.Raycast(rayToPlayer, out hit);
                if (hit.collider != null)
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        ActorMove(distance);
                        ActorAttack(distance);
                        EventGame.playerSpottedByEnemy.Invoke(transform);
                    }
            }
        }
    }

    private void ActorMove(float distance)
    {
        if (distance <= runRadius)
            switch (ai_Type)
            {
                case AI_Type.melee:

                    navMeshAgent.SetDestination(GameManager.instance.playerManager.transform.position);

                    break;



                case AI_Type.range:

                    Transform startTransform = transform;
                    transform.rotation = Quaternion.LookRotation(transform.position - GameManager.instance.playerManager.transform.position);
                    Vector3 runTo = transform.position + transform.forward * distance;

                    NavMeshHit hit;
                    NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));

                    transform.position = startTransform.position;
                    transform.rotation = startTransform.rotation;

                    navMeshAgent.SetDestination(hit.position);
                    break;
            }
    }

    private void ActorAttack(float distance)
    {
        if (Time.time > lastAttacked + attackDelay)
            switch (ai_Type)
            {
                case AI_Type.melee:
                    if (distance <= attackRadius)
                    {
                        EventGame.playerStatAction.Invoke("remove", "health", damage);
                        lastAttacked = Time.time;
                    }
                    break;

                case AI_Type.range:
                    if (distance <= attackRadius)
                    {
                        EventAudio.rangeSoulShoot.Invoke(GetComponent<AudioSource>());
                        transform.rotation = Quaternion.LookRotation(transform.position - GameManager.instance.playerManager.transform.position);
                        GameObject bullet = Instantiate(projectile, pfPojectile.transform.position, pfPojectile.transform.rotation);
                        bullet.GetComponent<Projectile>().damage = damage;
                        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * -shootForce);
                        lastAttacked = Time.time;
                    }
                    break;
            }
    }
}