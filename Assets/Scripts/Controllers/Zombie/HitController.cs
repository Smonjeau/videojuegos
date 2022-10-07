using System;
using UnityEngine;

namespace Controllers
{
    public class HitController : MonoBehaviour, IDamageable
    {
        public int Life => 0;
        public int MaxLife => 0;
        
        [SerializeField] private float _damageRatio = 1;
        public float DamageRatio => _damageRatio;
        private LifeController _parentLifeController;
        public LifeController ParentLifeController => _parentLifeController;


        [SerializeField] private GameObject _parentObject;

        private void Start()
        {
            _parentObject = transform.parent.gameObject;
            _parentLifeController = _parentObject.GetComponent<LifeController>();
        }

        public virtual void TakeDamage(int damage)
        {
            _parentLifeController.TakeDamage(Convert.ToInt32(damage * DamageRatio));
        }
        
        public void Heal(int heal)
        {
            _parentLifeController.Heal(heal);
        }
        
        public void SetLife(int life)
        {
            _parentLifeController.SetLife(life);
        }
    }
}