using System;
using System.Collections.Generic;
using Controllers;
using Flyweight;
using Managers;
using Strategy;
using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour, ILevel
    {
        public LevelStats Stats => _stats;
        [SerializeField] public LevelStats _stats;
        [SerializeField] public List<SpawnEnemyController> _spawners;

        public int LevelNumber => Stats.LevelNumber;
        public string LevelName => Stats.LevelName;
        private float _minRandomSpawnTime => Stats.MinRandomSpawnTime;
        private float _maxRandomSpawnTime => Stats.MaxRandomSpawnTime;
        private int _maxEnemyCount => Stats.MaxEnemyCount;
        private List<ZombieStats> _zombieStats => Stats.ZombieStats;
        private List<float> _zombieSpawnChance => Stats.EnemiesSpawnChance;
        private bool _isFinalLevel => Stats.IsFinalLevel;
        public bool IsFinalLevel => _isFinalLevel;
        

        private int _spawnersCount;
        private float _timeRemaining = 10;
        private int _enemyCount;
        
        private bool _isLevelStarted;
        private float[] _zombieChances;

        private int _zombieKills;

        public void Start()
        {
            _zombieChances = CalculateZombieChances();
            _spawnersCount = _spawners.Count;
        }

        private float[] CalculateZombieChances()
        {
            float[] chances = new float[_zombieSpawnChance.Count];
            float sum = 0;
            for (int i = 0; i < _zombieSpawnChance.Count; i++)
            {
                chances[i] = _zombieSpawnChance[i] + sum;
                sum += _zombieSpawnChance[i];
            }

            return chances;
        }

        public void StartLevel()
        {
            _enemyCount = 0;
            _timeRemaining = 5f;
            _isLevelStarted = true;
            LevelManager.Instance.OnZombieKill += OnZombieKill;
        }
        
        public void EndLevel()
        {
            LevelManager.Instance.CompletedLevel(this);
        }
        
        private void OnZombieKill()
        {
            _zombieKills++;
            if (_zombieKills == _maxEnemyCount) EndLevel();
        }
        
        private void Update()
        {

            if (!_isLevelStarted) return;
            
            if (_enemyCount >= _maxEnemyCount) return;
            
            if (_zombieKills == _maxEnemyCount) EndLevel();

            if (_timeRemaining <= 0)
            {
                SpawnZombie();
                _timeRemaining = UnityEngine.Random.Range(_minRandomSpawnTime, _maxRandomSpawnTime);
                _enemyCount++;
            }
            else  _timeRemaining -= Time.deltaTime;
        }

        private bool FinishedLevel()
        {
            return _zombieKills == _maxEnemyCount;
        }
        
        private void SpawnZombie()
        {
            var spawnerInt = UnityEngine.Random.Range(0, _spawnersCount);
            var spawner = _spawners[spawnerInt];
            var random = UnityEngine.Random.Range(0f, 1f);
            for (int i = 0; i < _zombieChances.Length; i++)
            {
                if (random < _zombieChances[i])
                {
                    spawner.Spawn(_zombieStats[i]);
                    break;
                }
            }
        }
    }
}