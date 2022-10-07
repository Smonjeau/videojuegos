
using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class LifeController : MonoBehaviour, IDamageable
    {
        public int Life => _life;
        [SerializeField] private int _life;
        public int MaxLife => _maxLife;
        [SerializeField] private int _maxLife = 100;

        private void Start()
        {
            _life = _maxLife;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead()) return;
            
            _life -= damage;
            if (_life <= 0)
            {
                _life = 0;
                Die();
            }
        }

        public bool IsDead() => _life <= 0;
    
        public virtual void Die()
        {
            if(name == "Soldier") EventsManager.Instance.EventGameOver(false);
            Destroy(gameObject);
        }
        
        public void SetLife(int life)
        {
            _life = life;
        }
        
        public void Heal(int heal)
        {
            _life += heal;
            if (_life > _maxLife) _life = _maxLife;
        }

    }
    

}