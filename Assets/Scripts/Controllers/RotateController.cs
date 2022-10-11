using System;
using UnityEngine;

namespace Controllers
{
    public class RotateController : MonoBehaviour
    {
        
        [SerializeField] public bool Right;
        [SerializeField] public bool Forward;
        [SerializeField] public bool Up;
        [SerializeField] private AnimationCurve _animationCurve;
        private float _curveDeltaTime = 0.0f;
        private float _baseYPosition;

        private void Start()
        {
            _baseYPosition = transform.position.y;
        }


        void Update() 
        {
            if (Forward)
                transform.Rotate(100 * Time.deltaTime * Vector3.forward);
            if (Up)
                transform.Rotate(100 * Time.deltaTime * Vector3.up);
            if (Right)
                transform.Rotate(100 * Time.deltaTime * Vector3.right);

            var currentPosition = transform.position;
            _curveDeltaTime+= Time.deltaTime;    
            transform.position = new Vector3(currentPosition.x, _baseYPosition + _animationCurve.Evaluate(_curveDeltaTime), currentPosition.z);
        }
    }
}