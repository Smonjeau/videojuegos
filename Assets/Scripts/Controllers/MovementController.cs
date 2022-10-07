using System;
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


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _cameraPosition = transform.Find("CM Position");

        }

        public void Travel(Vector3 direction) =>

            _rigidbody.MovePosition(transform.position + direction * (_speed * Time.deltaTime));

        public void RotateX(Vector3 direction)
        {
            var deltaRotation = Quaternion.Euler(direction * (_rotationSpeed * Time.deltaTime));
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

        }

        public void RotateY(Vector3 direction)
        {
            _cameraPosition.Rotate(direction * (Time.deltaTime * _rotationSpeed));

        }
    }
}