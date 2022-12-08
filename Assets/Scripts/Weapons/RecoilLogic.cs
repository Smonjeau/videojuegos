using System;
using System.Numerics;
using Cinemachine;
using Controllers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Weapons
{
    public class RecoilLogic : MonoBehaviour
    {

  
        [SerializeField] private float recoilY;
        [SerializeField] private float recoilX;
        [SerializeField] private float duration;
        private float _time;

        private MovementController _movementController;
        private CinemachineImpulseSource _screenShake;


        private Transform _camera;
        

        private void Start()
        {
           // _rightArm = GameObject.FindGameObjectWithTag("right-arm").transform;
            _movementController = GameObject.FindGameObjectWithTag("Player").
                GetComponent<MovementController>();
            _screenShake = GetComponent<CinemachineImpulseSource>();
            _camera = GameObject.FindGameObjectWithTag("cam-position").transform;


        }

        public void Recoil(Vector3 dir)
        {
            _time = duration;
            _screenShake.GenerateImpulse(dir);
        }

        private void Update()
        {
            if (!(_time > 0)) return;
            var dirY = Vector3.left * (recoilY/1000) / duration;
            var dirX = Vector3.up * (recoilX / 1000) / duration;
            _movementController.RotateY(dirY);
            //_camera.Rotate(dirX);
            _time -= Time.deltaTime;
            
        }

        public void SetValues(float recoilY, float recoilX, float recoilDuration)
        {
            this.recoilY = recoilY;
            this.recoilX = recoilX;
            this.duration = recoilDuration;

            
        }

    
    }
}