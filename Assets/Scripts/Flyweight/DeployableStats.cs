using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "DeployableStats", menuName = "Stats/Deployables", order = 0)]
    public class DeployableStats:ScriptableObject
    {
        [SerializeField] private DeployableStatValues _deployableStatsValues;

        public GameObject DeployablePrefab => _deployableStatsValues.DeployablePrefab;
        public int Damage => _deployableStatsValues.Damage;
        public float Range => _deployableStatsValues.Range;

        

        public DeployableStats(DeployableStatValues deployableStatsValues)
        {
            _deployableStatsValues = deployableStatsValues;
        }
    }
    
    [System.Serializable]
    public struct DeployableStatValues
    {
        public GameObject DeployablePrefab;
        public int Damage;
        public float Range;
    }   
    
}