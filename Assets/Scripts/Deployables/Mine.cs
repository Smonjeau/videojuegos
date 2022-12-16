using System;
using Flyweight;
using Strategy;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody))]
    public class Mine : MonoBehaviour, IDeployable
    {
        [SerializeField] private int _targetLayer;
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private DeployableStats _stat;
        [SerializeField] private float _triggerRadius;
        private float _radius => _stat.Range;
        private float _explosionForce = 800f;
        private int _damage => _stat.Damage;
        private float _throwForce = 2f; // TODO: put in stats
        private Rigidbody _rigidbody;
        
        private CircleCollider2D _triggerCollider;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _triggerCollider = GetComponent<CircleCollider2D>();
            _triggerCollider.radius = _triggerRadius;
            
        }
        
        public void Deploy()
        {
            _rigidbody.AddForce(transform.forward * _throwForce, ForceMode.VelocityChange);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _targetLayer)
            {
                Explode();
            }
        }
        
        private void Explode()
        {
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(_explosionForce, transform.position, _radius);
                }

                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null && collider.gameObject.layer != 8)
                {
                    damageable.TakeDamage(_damage);
                }
            }
            Destroy(gameObject);
        }
    }
}