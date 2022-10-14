using UnityEngine;

namespace Strategy
{
    public interface IPowerUp
    {
        string Name { get; }
        
        void Use(GameObject target);

        void Init();
    }
}