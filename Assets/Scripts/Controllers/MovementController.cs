using System;
using Command;
using Strategy;
using UnityEngine;

namespace Controllers
{
    public class MovementController : MonoBehaviour, IMoveable
    {

        public float Speed => _speed;
        [SerializeField] private float _speed = 11; 
        public float RotationSpeed => _rotationSpeed;
        [SerializeField] private float _rotationSpeed = 140;

        [SerializeField] private Rigidbody _rigidbody;

        private Transform _cameraPosition;
        private Transform _leftArm;
        private Transform _rightArm;



        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _cameraPosition = transform.Find("CM Position");
            _leftArm = transform.Find("LeftArm");
            _rightArm = transform.Find("RightArm");

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
            _cameraPosition.Rotate(direction * (Time.deltaTime * _rotationSpeed));
            _leftArm.Rotate(direction * (Time.deltaTime * _rotationSpeed));
            _rightArm.Rotate(direction * (Time.deltaTime * _rotationSpeed));

            
            
        }
    }
}