
using System;
using Managers;
using Strategy;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Controllers
{
    public class LifeController : MonoBehaviour, IDamageable
    {
        [SerializeField] public int CriticLife = 20;
        public int Life => _life;
        [SerializeField] private int _life;
        public int MaxLife => _maxLife;
        [SerializeField] public int _maxLife = 100;
        public IDieable dieable;

        private void Start()
        {
            ResetLife();
            dieable = gameObject.GetComponent<IDieable>();
        }

        public virtual void TakeDamage(int damage)
        {
            if (IsDead()) return;

            _life -= damage;
            if (_life <= 0)
            {
                _life = 0;
                Die();
                return;
            }

            if (gameObject.CompareTag("Player"))
            {
                EventsManager.Instance.EventPlayerAttacked(_life, _maxLife, CriticLife);
                if (_life <= CriticLife) EventsManager.Instance.EventLowLife();
            }
        }

        public bool IsDead() => _life <= 0;
    
        public void Die()
        {
            if (dieable != null) dieable.Die();
            else Destroy(gameObject);
        }
        
        public void SetLife(int life) => _life = life;
        
        public void Heal(int heal)
        {
            _life += heal;
            if (_life > _maxLife) ResetLife();
            
            if(_life > CriticLife) EventsManager.Instance.EventLifeHealed(_life, _maxLife, CriticLife);
        }

        public void ResetLife()
        {
            _life = _maxLife;
        }
    }
    

}