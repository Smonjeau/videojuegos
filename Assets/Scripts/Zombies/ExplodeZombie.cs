using Strategy;
using UnityEngine;

namespace Zombies
{
    public class ExplodeZombie : Zombie
    {
        [SerializeField] private GameObject _eplosionPrefab;
        [SerializeField] private float _radius = 20f;
        [SerializeField] private float _force = 800f;

        public override void Attack()
        {
            _soundEffectController.PlayOnHit();
            Instantiate(_eplosionPrefab, transform.position, transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider collider in colliders)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null && collider.gameObject.CompareTag("Player"))
                {
                    damageable.TakeDamage(_hitDamage);
                }
            }
            Destroy(gameObject);
        }
    }
}