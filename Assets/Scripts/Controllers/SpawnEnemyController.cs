using System;
using Flyweight;
using Strategy;
using UnityEngine;

namespace Controllers
{
    public class SpawnEnemyController : MonoBehaviour, ISpawner
    {
        private Vector3 _portalPosition;

        public void Start()
        {
            _portalPosition = transform.position;
        }

        public void Spawn(ZombieStats zombieStats)
        {
            Instantiate(zombieStats.ZombiePrefab,
                new Vector3(_portalPosition.x, _portalPosition.y, _portalPosition.z),
                transform.rotation
            );
        }

    }
}