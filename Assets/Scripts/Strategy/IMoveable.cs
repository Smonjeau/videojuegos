using Command;
using UnityEngine;

namespace Strategy
{
    public interface IMoveable {
    
        float Speed { get; }
    
        float RotationSpeed { get; }
    
        void Travel(Directions direction);
    
        void RotateX(Vector3 direction);

        void RotateY(Vector3 direction);

    }
}
