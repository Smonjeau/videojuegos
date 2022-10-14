using System;
using System.Collections.Generic;
using Strategy;
using UnityEngine;

namespace PowerUps
{
    public class PowerUp : MonoBehaviour, IPowerUp
    {
        [SerializeField] private List<int> _layerTarget;
        public virtual string Name => "Base PowerUp";
        
        private float _timeToDestroy = 20f;
        
        public void Init()
        {
            Destroy(gameObject, _timeToDestroy);
        }

        public virtual void Use(GameObject target) {}

        private void OnTriggerEnter(Collider other)
        {
            if (_layerTarget.Contains(other.gameObject.layer))
            {
                Use(other.gameObject);
            }
        }
        
        protected void Die() => Destroy(gameObject);
    }
}