using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable {
    
    float Speed { get; }
    
    float RotationSpeed { get; }
    
    void Travel(Vector3 direction,Rigidbody rb);
    
    void Rotate(Vector3 direction,Rigidbody rb);
}
