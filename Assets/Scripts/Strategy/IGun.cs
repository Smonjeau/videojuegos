using UnityEngine;

namespace Strategy
{
    public interface IGun
    {
        GameObject BulletPrefab { get; }
        GameObject GunPrefab { get; }
        int MagSize { get; }
        int MaxAmmo { get; }
        int CurrentMagSize { get; }
        int Damage { get; }
        string Name { get; }
        bool InfiniteAmmo { get; }

        bool IsReloading { get; }

        void Attack();
        void Reload();
        void AddAmmo(int amount);
        void FullAmmo();
        
    }
}