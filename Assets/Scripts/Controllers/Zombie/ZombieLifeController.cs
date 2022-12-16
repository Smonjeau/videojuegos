using System.Collections;
using Strategy;
using UnityEngine;

namespace Controllers
{
    public class ZombieLifeController : LifeController
    {
        
        private Zombies.Zombie _zombie;
        private float _lifeIncrement;
        
        public override void Start()
        {
            _zombie = GetComponent<Zombies.Zombie>();
            _maxLife = (int)(_zombie.Stats.MaxLife*_lifeIncrement);
            SetLife(_maxLife);
            dieable = GetComponent<IDieable>();
        }

        public void SetLifeIncrement(float value) =>  _lifeIncrement = value;

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