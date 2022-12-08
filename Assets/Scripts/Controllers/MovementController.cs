using System;
using Command;
using Strategy;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : MonoBehaviour, IMoveable
    {

        public float Speed => _speed;
        [SerializeField] private float _speed = 11; 
        public float RotationSpeed => _rotationSpeed;
        [SerializeField] private float _rotationSpeed = 140;
        [SerializeField] private int _verticalAngleViewLimit =60; 

        [SerializeField] private Rigidbody _rigidbody;

        private Transform _cameraPosition;
        private Transform _arms;
       

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _cameraPosition = GameObject.FindGameObjectWithTag("cam-position").transform;
            _arms = transform.Find("Arms");

        }

        public void Travel(Directions direction)
        {
            var transform1 = transform;
            var forward = transform1.forward;
            var right = transform1.right;
            var dir = direction switch
            {
                Directions.Forward => forward,
                Directions.Backward => -forward,
                Directions.Left => -right,
                Directions.Right => right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            _rigidbody.MovePosition((transform1.position + dir  * (_speed * Time.deltaTime)));
        }

        public void RotateX(Vector3 direction)
        {
            var deltaRotation = Quaternion.Euler(direction * (_rotationSpeed * Time.deltaTime));
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

        }

        public void RotateY(Vector3 direction)
        {

            int limit = _verticalAngleViewLimit;
            Vector3 currRotation = _cameraPosition.eulerAngles;
            Vector3 rotation = direction * (Time.deltaTime * _rotationSpeed);
            if (currRotation.x>180)
            {

                currRotation.x = -(-currRotation.x + 360);
            }
            rotation.x = Mathf.Clamp(rotation.x, -limit-currRotation.x, limit-currRotation.x);
            
            _cameraPosition.Rotate(rotation);
            _arms.Rotate(rotation);
            
            
            
        }
    }
}