using Strategy;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(AudioSource))]
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private GameObject _eplosionPrefab;
        [SerializeField] private float _radius = 10f;
        [SerializeField] private float _force = 800f;
        [SerializeField] private AudioClip _explosionSound;
        private int _damage;
        
        private AudioSource _audioSource;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _explosionSound;
            

        }

        private void OnTriggerEnter(Collider other)
        {
            Explode();
        }

        private void Explode()
        {
            _audioSource.Play();
            Instantiate(_eplosionPrefab, transform.position, transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

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
            Destroy(gameObject,0.4f);
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
        }
    }

}