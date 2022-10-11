﻿using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "GunStats", menuName = "Stats/Guns", order = 0)]
    public class GunStats:ScriptableObject
    {
        [SerializeField] private GunStatValues _gunStatsValues;

        public GameObject BulletPrefab => _gunStatsValues.BulletPrefab;
        public GameObject GunPrefab => _gunStatsValues.GunPrefab;
        public int Damage => _gunStatsValues.Damage;
        public int MagSize => _gunStatsValues.MagSize;
        public int MaxAmmo => _gunStatsValues.MaxAmmo;
        public int BulletCount => _gunStatsValues.BulletCount;
        public float RateOfFire => _gunStatsValues.RateOfFire;
        public float ReloadCooldown => _gunStatsValues.ReloadCooldown;
        public string WeaponName => _gunStatsValues.WeaponName;
        public bool InfiniteAmmo => _gunStatsValues.InfiniteAmmo;

        public GunStats(GunStatValues gunStatsValues)
        {
            _gunStatsValues = gunStatsValues;
        }
    }
    
    [System.Serializable]
    public struct GunStatValues
    {
        public GameObject BulletPrefab;
        public GameObject GunPrefab;
        public int Damage;
        public int MagSize;
        public int MaxAmmo;
        public int BulletCount;
        public float RateOfFire;
        public float ReloadCooldown;
        public string WeaponName;
        public bool InfiniteAmmo;
    }
}
