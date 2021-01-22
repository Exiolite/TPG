using System;
using Core;
using UnityEngine;

namespace Systems.Projectiles
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private bool isAutoTrack;
        
        private Transform _transform;

        private static readonly Vector3 PlayerHeightCorrection = new Vector3(0,1.5f, 0);
        private Vector3 _playerPosition;

        private float _projectileSpeed;
        private float _damage;
        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }


        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Start()
        {
            _playerPosition = GameManager.instance.playerBehaviour.transform.position;
            transform.LookAt(_playerPosition + PlayerHeightCorrection);
        }

        private void Update()
        {
            if (isAutoTrack)
            {
                _playerPosition = GameManager.instance.playerBehaviour.transform.position;
                transform.LookAt(_playerPosition + PlayerHeightCorrection);
            }
            _projectileSpeed = Time.deltaTime * speed;
            _transform.position += _transform.forward * _projectileSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            
        }
        
        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}

