using System;
using System.Collections.Generic;
using Controllers;
using Factory;
using Flyweight;
using Strategy;


namespace Entities
{
    using UnityEngine;

    [RequireComponent(typeof(LifeController))]
    public class Crate : MonoBehaviour, IDieable
    {
        
        /* Controllers */
        private LifeController _lifeController;
        
        /* Stat Values */
        public CrateStats Stats => _stats;
        [SerializeField] public CrateStats _stats;
        private float _powerUpChance => Stats.PowerUpChance;
        private List<PowerUpType> _powerUps => Stats.PowerUps;
        private List<float> _powerUpChances => Stats.PowerUpChances;

        public MeshRenderer wholeCrate;
        public BoxCollider boxCollider;
        public GameObject fracturedCrate;
        public AudioSource crashAudioClip;

        private bool _hasPowerUp;
        private PowerUpType _powerUpType;
        private GameObject _powerUp;

        private void Start()
        {
            _lifeController = GetComponent<LifeController>();
            _lifeController._maxLife = _stats.MaxLife;

            var random = Random.Range(0f, 1f);
            _hasPowerUp = random <= _powerUpChance;
            if (!_hasPowerUp) return;
            random = Random.Range(0f, 1f);
            var sum = 0f;
            for (var i = 0; i < _powerUpChances.Count; i++)
            {
                sum += _powerUpChances[i];
                if (random <= sum)
                {
                    _powerUpType = _powerUps[i];
                    break;
                }
            }
            // CreatePowerUp();
        }

        private void CreatePowerUp()
        {
            Debug.Log("Has power up...");
            _powerUp = PowerUpsFactory.Instance.CreatePowerUp(_powerUpType, gameObject);
            Debug.Log(_powerUp);
            _powerUp.GetComponent<IPowerUp>().Init();
            // _powerUp.SetActive(false);
        }

        public void Die()
        {
            wholeCrate.enabled = false;
            boxCollider.enabled = false;
            fracturedCrate.SetActive(true);
            crashAudioClip.Play();
            if (_hasPowerUp)
            {
                CreatePowerUp();
                // _powerUp.GetComponent<IPowerUp>().Init();
                // _powerUp.SetActive(true);
            }
            Destroy(gameObject, 2f);
        }
    }
}