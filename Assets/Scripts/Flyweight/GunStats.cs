using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "GunStats", menuName = "Stats/Guns", order = 0)]
    public class GunStats:ScriptableObject
    {
        [SerializeField] private GunStatValues _gunStatsValues;

        // public GameObject BulletPrefab => _gunStatsValues.BulletPrefab;
        public int Damage => _gunStatsValues.Damage;
        public int MagSize => _gunStatsValues.MagSize;
        public int BulletCount => _gunStatsValues.BulletCount;
        public float ShotCooldown => _gunStatsValues.ShotCooldown;
        
    }
}


[System.Serializable]
public struct GunStatValues
{
    // public GameObject BulletPrefab;
    public int Damage;
    public int MagSize;
    public int BulletCount;
    public float ShotCooldown;
}