﻿using System.Collections;
using Systems.Audio;
using Systems.Stats;
using Core;
using UnityEngine;
using UnityEngine.AI;

namespace Systems.Actor
{
    public abstract class ActorBehaviour : MonoBehaviour
    {
        [SerializeField] private StatParameter health;

        [Header("Combat")] 
        [SerializeField] private float damage;
        [SerializeField] private float attackDelay;
        [SerializeField] private int memoryAboutPlayer;
        
        [Header("Vision")]
        [SerializeField] private float lookRadius;
        [SerializeField] private float attackRadius;
        
        [Header("Sounds")]
        [SerializeField] private AudioClipAndVolume moveSound;
        [SerializeField] private AudioClipAndVolume attackSound;
        [SerializeField] private AudioClipAndVolume getDamageSound;

        private AudioSource _audioSource;
        private NavMeshAgent _navMeshAgent;
        
        private float _distanceToPlayer;
        
        private bool _isAttacking;
        private bool _isPlayerSpotted;
        

        public void ApplyDamageToActor(int value)
        {
            health.ChangeStat(value);
            getDamageSound.PlaySound(ref _audioSource);
            if (health.CheckStat()) Destroy(gameObject);
        }
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            UpdateBehaviour();
        }
        

        private void UpdateBehaviour()
        {
            if (CalculateDistance() < lookRadius || _isPlayerSpotted)
            {
                Move(ref _navMeshAgent);
                StartCoroutine(RememberAboutPlayer());
                moveSound.PlaySound(ref _audioSource);
            }
            if (CalculateDistance() < attackRadius)
            {
                StartCoroutine(AttackCoroutine());
                attackSound.PlaySound(ref _audioSource);
            }
        }

        private float CalculateDistance()
        {
            return Vector3.Distance(GameManager.instance.playerBehaviour.transform.position, transform.position);
        }

        private IEnumerator AttackCoroutine()
        {
            if (_isAttacking) yield break;
            _isAttacking = true;
            yield return new WaitForSeconds(attackDelay);
            Attack(-damage);
            _isAttacking = false;
        }

        private IEnumerator RememberAboutPlayer()
        {
            if (_isPlayerSpotted) yield break;
            _isPlayerSpotted = true;
            yield return new WaitForSeconds(memoryAboutPlayer);
            _isPlayerSpotted = false;
        }
        
        
        
        protected abstract void Move(ref NavMeshAgent navMeshAgent);
        protected abstract void Attack(float damage);


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }
    }
}