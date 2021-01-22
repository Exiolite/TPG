using System;
using System.Collections;
using Systems.Audio;
using Systems.Stats;
using Core;
using UnityEngine;
using UnityEngine.AI;

namespace Systems.Actor
{
    public abstract class ActorBehaviour : MonoBehaviour
    {
        [Header("Vision")]
        [SerializeField] private float lookRadius;
        [SerializeField] private float attackRadius;
        
        [Header("Combat")] 
        [SerializeField] private float damage;
        [SerializeField] private float attackDelay;
        [SerializeField] private bool attackBeforeDelay;
        [SerializeField] private int memoryAboutPlayer;
        
        [Header("Stats")]
        [SerializeField] private StatParameter health;
        
        [Header("Sounds")]
        [SerializeField] private AudioClipAndVolume moveSound;
        [SerializeField] private AudioClipAndVolume attackSound;
        [SerializeField] private AudioClipAndVolume getDamageSound;

        [Header("Visual")]
        [SerializeField] private GameObject hitParticles;
        private ParticleSystem _hitParticles;
        [SerializeField] private GameObject deathParticles;
        private ParticleSystem _deathParticles;

        private AudioSource _audioSource;
        private NavMeshAgent _navMeshAgent;

        private float _distanceToPlayer;
        
        private bool _isAttacking;
        private bool _isPlayerSpotted;
        private bool _isDead;

        
        
        protected abstract void Move(ref NavMeshAgent navMeshAgent);
        protected abstract void Attack(float damage);
        
        
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            
            _hitParticles = Instantiate(hitParticles, transform).GetComponent<ParticleSystem>();
            _deathParticles = Instantiate(deathParticles, transform).GetComponent<ParticleSystem>();
            _hitParticles.GetComponent<ParticleSystemRenderer>().material = GetComponent<MeshRenderer>().material;
            _hitParticles.GetComponent<ParticleSystemRenderer>().trailMaterial = GetComponent<MeshRenderer>().material;
            _deathParticles.GetComponent<ParticleSystemRenderer>().material = GetComponent<MeshRenderer>().material;
            _deathParticles.GetComponent<ParticleSystemRenderer>().trailMaterial = GetComponent<MeshRenderer>().material;
        }
        
        private void Update()
        {
            UpdateBehaviour();
        }
        
        private void UpdateBehaviour()
        {
            if (_isDead) return;
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
            if (attackBeforeDelay)
            {
                _isAttacking = true;
                Attack(-damage);
                yield return new WaitForSeconds(attackDelay);
                _isAttacking = false;
            }
            else
            {
                _isAttacking = true;
                yield return new WaitForSeconds(attackDelay);
                Attack(-damage);
                _isAttacking = false;
            }
        }

        private IEnumerator RememberAboutPlayer()
        {
            if (_isPlayerSpotted) yield break;
            _isPlayerSpotted = true;
            yield return new WaitForSeconds(memoryAboutPlayer);
            _isPlayerSpotted = false;
        }


        
        private void OnTriggerEnter(Collider other)
        {
            if (!GameManager.instance.playerBehaviour.PlayerAnimator.IsAttacking) return;
            if (!other.CompareTag("Weapon")) return;
            ApplyDamageToActor(-GameManager.instance.playerBehaviour.Equipment.Weapon.GetWeaponData().damage);
        }
        
        private void ApplyDamageToActor(float value)
        {
            health.ChangeStat(value);
            getDamageSound.PlaySound(ref _audioSource);
            if (hitParticles != null) _hitParticles.Play();
            if (!health.CheckStat()) return;
            if (deathParticles != null) _deathParticles.Play();
            _navMeshAgent.ResetPath();
            StartCoroutine(DeathCoroutine());
        }
        
        private IEnumerator DeathCoroutine()
        {
            _isDead = true;
            yield return new WaitForSeconds(0.8f);
            Destroy(gameObject);
        }
        
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}