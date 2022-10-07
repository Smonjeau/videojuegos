using System;
using UnityEngine;

namespace Controllers
{
    public class SpawnEnemyController : MonoBehaviour, ISpawner
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _spawnDelay;

        public float SpawnDelay
        {
            get { return _spawnDelay; }
            set { _spawnDelay = value; }
        }

        [SerializeField] private int _spawnCount;

        public int SpawnCount
        {
            get { return _spawnCount; }
            set { _spawnCount = value; }
        }

        private int _spawnedCount;
        private Vector3 _portalPosition;

        public void Start()
        {
            InvokeRepeating(nameof(Spawn), _spawnDelay, _spawnDelay);
            _portalPosition = gameObject.transform.position;
        }

        public void Spawn()
        {
            if (_spawnedCount >= _spawnCount) return;


            Instantiate(_enemyPrefab,
                new Vector3(_portalPosition.x, _portalPosition.y, _portalPosition.z),
                gameObject.transform.rotation
            );
            _spawnedCount++;
        }

    }
}