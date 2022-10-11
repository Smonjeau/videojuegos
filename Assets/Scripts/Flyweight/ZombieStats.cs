using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "ZombieStats", menuName = "Stats/Zombie", order = 0)]
    public class ZombieStats : ScriptableObject
    {
        [SerializeField] private ZombieStatValues _statValues;
    
        public int MaxLife => _statValues.MaxLife;
        public float MovementSpeed => _statValues.MovementSpeed;
        public int AttackDamage => _statValues.AttackDamage;
        public float AttackSpeed => _statValues.AttackSpeed;
        public float AttackRange => _statValues.AttackRange;
        public GameObject ZombiePrefab => _statValues.ZombiePrefab;
    }

    [System.Serializable]
    public struct ZombieStatValues
    {
        public int MaxLife;
        public float MovementSpeed;
        public float AttackSpeed;
        public float AttackRange;
        public int AttackDamage;
        public GameObject ZombiePrefab;
    }
}