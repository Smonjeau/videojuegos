
using UnityEngine;

namespace Strategy
{
    public interface IBullet
    {
        Gun Owner { get; }
        float LifeTime { get; }
        float Speed { get; }
        Rigidbody Rigidbody { get; }
        Collider Collider { get; }

        void Travel();
        void OnTriggerEnter(Collider collider);
        void SetOwner(Gun gun);
    }
}