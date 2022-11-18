using Controllers;
using Strategy;
using UnityEngine;

namespace Entities
{
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private GameObject _eplosionPrefab;
        [SerializeField] private float _delay = 3f;
        [SerializeField] private float _radius = 10f;
        [SerializeField] private float _force = 800f;
        [SerializeField] private int _damage = 50;
        private float _countdown;
        private bool _hasExploded;

        void Start()
        {
            _countdown = _delay;
        }

        void Update()
        {
            _countdown -= Time.deltaTime;
            if (_countdown <= 0 && !_hasExploded)
            {
                _hasExploded = true;
                Explode();
            }
        }

        private void Explode()
        {
            Instantiate(_eplosionPrefab, transform.position, transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(_force, transform.position, _radius); // hace que se mueva por culpa de la explosion
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