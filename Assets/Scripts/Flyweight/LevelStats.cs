using System.Collections.Generic;
using Controllers;
using Flyweight;
using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "LevelStats", menuName = "Stats/Levels", order = 0)]
    public class LevelStats : ScriptableObject
    {
        [SerializeField] private LevelStatValues _levelStatsValues;
        
        public int LevelNumber => _levelStatsValues.LevelNumber;
        public float MinRandomSpawnTime => _levelStatsValues.MinRandomSpawnTime;
        public float MaxRandomSpawnTime => _levelStatsValues.MaxRandomSpawnTime;
        public int MaxEnemyCount => _levelStatsValues.MaxEnemyCount;
        public List<ZombieStats> ZombieStats => _levelStatsValues.ZombieStats;
        public List<float> EnemiesSpawnChance => _levelStatsValues.EnemiesSpawnChance;
        public bool IsFinalLevel => _levelStatsValues.IsFinalLevel;
        public string LevelName => _levelStatsValues.LevelName;
        public float IncreaseZombieLife => _levelStatsValues.IncreaseZombieLife;
        public bool IsLevelFinisherRound => _levelStatsValues.IsLevelFinisherRound;
        public bool IsLevelStarterRound => _levelStatsValues.IsLevelStarterRound;
        public int RoomNumber => _levelStatsValues.RoomNumber;
        public ZombieStats BossStats => _levelStatsValues.BossStats;
    }
    
    [System.Serializable]
    public struct LevelStatValues
    {
        public int LevelNumber;
        public float MinRandomSpawnTime;
        public float MaxRandomSpawnTime;
        public int MaxEnemyCount;
        public List<ZombieStats> ZombieStats;
        public List<float> EnemiesSpawnChance;
        public bool IsFinalLevel;
        public string LevelName;
        public float IncreaseZombieLife;
        public bool IsLevelFinisherRound;
        public bool IsLevelStarterRound;
        public int RoomNumber;
        public ZombieStats BossStats;
    }
}
