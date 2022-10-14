using System.Collections.Generic;
using Managers;
using Strategy;
using UnityEngine;
using Weapons;

namespace Entities
{
    [RequireComponent(typeof(Collider),typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IBullet
    {
        public float LifeTime => _lifeTime;
        [SerializeField] private float _lifeTime;
    
        public float Speed => _speed;
        [SerializeField] private float _speed;

        public Gun Owner => _owner;
        [SerializeField] private Gun _owner;

        public Rigidbody Rigidbody => _rigidbody;
        [SerializeField] private Rigidbody _rigidbody;

        public Collider Collider => _collider;
        [SerializeField] private Collider _collider;

        [SerializeField] private List<int> _layerTarget;
        
        

        public void Travel() => transform.Translate(_speed * Time.deltaTime * Vector3.forward);

        public void OnTriggerEnter(Collider collider)
        {
            //if bullet hits player or something not damageable, ignore
            if (!_layerTarget.Contains(collider.gameObject.layer))
            {
                return;
            }
            if (collider.gameObject.CompareTag("Player"))
            {

                return;
            };

            //if whatever is hits is damageable, damage it
            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(_owner.Damage);
            EventsManager.Instance.EventHit();

            Destroy(gameObject);

        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            _collider.isTrigger = true;
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }

        private void Update()
        {
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0) Destroy(this.gameObject);

            Travel();
        }

        public void SetOwner(Gun owner) => _owner = owner;
    }
}