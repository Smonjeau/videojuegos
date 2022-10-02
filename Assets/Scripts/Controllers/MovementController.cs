using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, IMoveable
{

    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
    
    [SerializeField] private float _speed = 30; 
    [SerializeField] private float _rotationSpeed = 80;
    
    public void Travel(Vector3 direction) => transform.Translate(Time.deltaTime * _speed * direction);
    
    public void Rotate(Vector3 direction) => transform.Rotate(Time.deltaTime * _rotationSpeed * direction);
}
