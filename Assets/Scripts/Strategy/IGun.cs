using UnityEngine;

namespace Strategy
{
    public interface IGun
    {
        GameObject BulletPrefab { get; }
        GameObject GunPrefab { get; }
        int MagSize { get; }
        int CurrentMagSize { get; }
        int Damage { get; }
        string Name { get; }

        void Attack();
        void Reload();
        
    }
}