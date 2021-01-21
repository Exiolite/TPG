using Core;
using UnityEngine;

namespace Systems.Projectiles
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        
        private float _damage;
        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }


        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            transform.LookAt(GameManager.instance.playerBehaviour.transform);
            _rigidbody.AddForce(transform.forward * 2000);
        }

        private void OnCollisionEnter(Collision collision)
        {
            
        }
        
        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}

