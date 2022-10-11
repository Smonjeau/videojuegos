using System;
using Strategy;
using UnityEngine;

namespace Controllers
{
    public class ChildLifeController : MonoBehaviour, IDamageable
    {
        public int Life => 0;
        public int MaxLife => 0;
        
        [SerializeField] private float _damageRatio = 1;
        public float DamageRatio => _damageRatio;
        private ZombieLifeController _parentLifeController;
        public ZombieLifeController ParentLifeController => _parentLifeController;


        [SerializeField] private GameObject _parentObject;

        private void Start()
        {
            _parentObject = transform.parent.gameObject;
            _parentLifeController = _parentObject.GetComponent<ZombieLifeController>();
        }

        public virtual void TakeDamage(int damage)
        {
            _parentLifeController.TakePartDamage(Convert.ToInt32(damage * DamageRatio), false);
        }
        
        public void Heal(int heal) => _parentLifeController.Heal(heal);
        public void ResetLife() {}

        public void SetLife(int life) => _parentLifeController.SetLife(life);
    }
}