using System;
using System.Collections;
using Controllers;
using Flyweight;
using Strategy;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(SoundEffectController))]
    public class Mine : MonoBehaviour
    {
        [SerializeField] private int _targetLayer;
        [SerializeField] private GameObject _eplosionPrefab;
        [SerializeField] private DeployableStats _stat;
        private GameObject _character;
        private SoundEffectController _soundEffectController;
        
        private float _radius => _stat.Range;
        private float _force = 800f;
        private int _damage => _stat.Damage;

        
        public void Start()
        {
            _soundEffectController = GetComponent<SoundEffectController>();

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
            Instantiate(_eplosionPrefab, transform.position, transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
            _soundEffectController.PlayOnShot();
            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(_force, transform.position, _radius);
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