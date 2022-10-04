using UnityEngine;

namespace Strategy
{
    public interface IGun
    {
        GameObject BulletPrefab { get; }
        int MagSize { get; }
        int CurrentMagSize { get; }
        int Damage { get; }

        void Attack();
        void Reload();
    }
}