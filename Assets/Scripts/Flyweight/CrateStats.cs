using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "CrateStats", menuName = "Stats/Crates", order = 0)]
    public class CrateStats : ScriptableObject
    {
        [SerializeField] private CrateStatValues _stats;
        
        public GameObject CratePrefab => _stats.CratePrefab;
        public int MaxLife => _stats.Health;
        public float PowerUpChance => _stats.PowerUpChance;
        public List<GameObject> PowerUpPrefabs => _stats.PowerUpPrefabs;
        public List<float> PowerUpChances => _stats.PowerUpChances;
        
    }
    
    [System.Serializable]
    public struct CrateStatValues
    {
        public GameObject CratePrefab;
        public int Health;
        public float PowerUpChance;
        public List<GameObject> PowerUpPrefabs;
        public List<float> PowerUpChances;
    }
}
