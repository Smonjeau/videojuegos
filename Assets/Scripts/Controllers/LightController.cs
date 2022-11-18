using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    [RequireComponent(typeof(Light))]
    public class LightController : MonoBehaviour
    {
        private float _maxReduction = 0.2f;
        private float _maxIncrease = 0.2f;
        private float _rate = 0.1f;
        private float _strenght = 300;
        
        private Light _lightSource;
        private float _initialIntensity;
        
        private void Start()
        {
            _lightSource = GetComponent<Light>();
            _initialIntensity = _lightSource.intensity;

            StartCoroutine(DoFlickering());
        }

        private IEnumerator DoFlickering()
        {
            while (true)
            {
                _lightSource.intensity = Mathf.Lerp(_lightSource.intensity, 
                    Random.Range(_initialIntensity - _maxReduction, _initialIntensity + _maxIncrease), _strenght * Time.deltaTime);

                yield return new WaitForSeconds(_rate);
            }
        }
    }
}