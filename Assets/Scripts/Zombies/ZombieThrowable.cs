using Managers;
using Strategy;
using UnityEngine;

namespace Zombies
{
    public class ZombieThrowable : MonoBehaviour
    {
        [SerializeField] private float _radius = 10f;
        [SerializeField] private float _force = 800f;
        [SerializeField] private int _damage = 30;


        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.name=="Soldier")
            {
                EventsManager.Instance.EventPlayerHitWithThrowable();

                IDamageable damageable = collider.GetComponent<IDamageable>();
                damageable?.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
        
    

    
    }
}