using System.Collections;
using System.Runtime.CompilerServices;
using Strategy;
using UnityEngine;

namespace Controllers
{
    public class ZombieLifeController : LifeController
    {
        
        private Zombies.Zombie _zombie;
        public new int MaxLife => _zombie.Stats.MaxLife;
        
        private void Start()
        {
            _zombie = GetComponent<Zombies.Zombie>();
            SetLife(MaxLife);
            dieable = GetComponent<IDieable>();
        }

        public override void TakeDamage(int damage) {}

        public void TakePartDamage(int damage, bool removedHead)
        {
            if (removedHead)
                StartCoroutine(RemoveHeadAndDie(damage));
            else
                base.TakeDamage(damage);
        }
        
        IEnumerator RemoveHeadAndDie(int damage)
        {
            yield return new WaitForSeconds(0.6f);
            base.TakeDamage(damage);
        }
    }
}