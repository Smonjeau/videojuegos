using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable {
    
    float Speed { get; }
    
    float RotationSpeed { get; }
    
    void Travel(Vector3 direction);
    
    void RotateX(Vector3 direction);

    void RotateY(Vector3 direction);

}
