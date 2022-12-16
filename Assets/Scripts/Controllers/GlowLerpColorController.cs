using System;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class GlowLerpColorController : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _startIntensity = 0f;
        [SerializeField] private float _endIntensity = 1f;
        [SerializeField] private Color _glowColor;
        [SerializeField] private Material _material;
        [SerializeField] private bool _repeatable;
        [SerializeField] private float _startTime;

        void Start()
        {
            _startTime = Time.time;
        }

        private void Update()
        {
            
            if (!_repeatable)
            {
                float t = (Time.time - _startTime) * _speed;
                Color intensity = Color.Lerp(_glowColor*_startIntensity, _glowColor*_endIntensity, t);
                _material.SetColor("_EmissionColor", intensity);
            }
            else
            {
                float t = (float) Math.Sin(Time.time - _startTime) * _speed;
                Color intensity = Color.Lerp(_glowColor*_startIntensity, _glowColor*_endIntensity, t);
                _material.SetColor("_EmissionColor", intensity);
            }
        }
    }
}