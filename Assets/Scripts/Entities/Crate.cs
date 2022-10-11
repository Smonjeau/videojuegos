using System;
using System.Collections.Generic;
using Controllers;
using Flyweight;
using Strategy;

namespace Entities
{
    using UnityEngine;

    public class Crate : MonoBehaviour, IDieable
    {
        
        /* Controllers */
        private LifeController _lifeController;
        
        /* Stat Values */
        public CrateStats Stats => _stats;
        [SerializeField] public CrateStats _stats;
        private float _powerUpChance => Stats.PowerUpChance;
        private List<GameObject> _powerUpPrefabs => Stats.PowerUpPrefabs;

        [Header("Whole Create")]
        public MeshRenderer wholeCrate;
        public BoxCollider boxCollider;
        [Header("Fractured Create")]
        public GameObject fracturedCrate;
        [Header("Audio")]
        public AudioSource crashAudioClip;

        private bool _hasPowerUp;
        private GameObject _powerUpPrefab;
        private GameObject _powerUp;

        private void Start()
        {
            _lifeController = GetComponent<LifeController>();
            _lifeController._maxLife = _stats.MaxLife;

            var random = Random.Range(0f, 1f);
            _hasPowerUp = random <= _powerUpChance;
            if (!_hasPowerUp) return;
            _powerUpPrefab = _powerUpPrefabs[Random.Range(0, _powerUpPrefabs.Count)];
            CreatePowerUp();
        }

        private void CreatePowerUp()
        {
            var position = transform.position;
            _powerUp = Instantiate(_powerUpPrefab, 
                new Vector3(position.x, 2.5f, position.z), 
                _powerUpPrefab.transform.rotation);
            _powerUp.SetActive(false);
        }

        public void Die()
        {
            wholeCrate.enabled = false;
            boxCollider.enabled = false;
            fracturedCrate.SetActive(true);
            crashAudioClip.Play();
            if (_hasPowerUp)
            {
                _powerUp.SetActive(true);
            }
            Destroy(gameObject, 2f);
        }
    }
}