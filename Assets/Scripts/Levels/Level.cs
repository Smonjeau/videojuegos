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
        private ZombieStats _bossStats => Stats.BossStats;

        private List<float> _zombieSpawnChance => Stats.EnemiesSpawnChance;
        private bool _isFinalLevel => Stats.IsFinalLevel;
        public bool IsFinalLevel => _isFinalLevel;
        public float IncreaseZombieLife => Stats.IncreaseZombieLife;
        public List<WeaponType> Weapons => Stats.Weapons;
        

        private int _spawnersCount;
        [SerializeField] private float _timeRemaining = 10;
        [SerializeField] private int _enemyCount;
        
        [SerializeField] private bool _isLevelStarted;
        private float[] _zombieChances;

        [SerializeField] private int _zombieKills;

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
            _zombieKills = 0;
            WeaponsManager.Instance.ChangeWeapons(Weapons);
            LevelManager.Instance.OnZombieKill += OnZombieKill;
            LevelManager.Instance.OnBossKill += OnBossKill;
        }
        
        public void EndLevel()
        {
            _isLevelStarted = false;
            LevelManager.Instance.OnZombieKill -= OnZombieKill;
            LevelManager.Instance.OnBossKill -= OnBossKill;

            LevelManager.Instance.CompletedLevel(this);
            Debug.Log("END LEVEL AL LEVEL " + LevelName);
        }
        
        private void OnZombieKill()
        {
            _zombieKills++;
            if (_zombieKills == _maxEnemyCount  )
            {
                if (_stats.IsLevelFinisherRound)
                    Invoke(nameof(SpawnBoss),3f);
                else 
                    EndLevel();
            };
        }
        
        private void Update()
        {

            if (!_isLevelStarted) return;
            
            if (_enemyCount >= _maxEnemyCount) return;
            
            if (_zombieKills == _maxEnemyCount && !_stats.IsLevelFinisherRound) EndLevel();
            
            if (_timeRemaining <= 0)
            {
                _enemyCount++;
                SpawnZombie();
                _timeRemaining = UnityEngine.Random.Range(_minRandomSpawnTime, _maxRandomSpawnTime);
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
                    spawner.Spawn(_zombieStats[i], IncreaseZombieLife);
                    break;
                }
            }
        }
        
        public void SpawnBoss()
        {
            var spawnerInt = UnityEngine.Random.Range(0, _spawnersCount);
            var spawner = _spawners[spawnerInt];
            spawner.Spawn(_bossStats,1f);
        }
        private void OnBossKill()
        {
            EndLevel();
        }
    }
}