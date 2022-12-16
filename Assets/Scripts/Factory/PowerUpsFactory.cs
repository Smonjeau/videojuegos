using System;
using PowerUps;
using UnityEngine;

namespace Factory
{
    public class PowerUpsFactory : MonoBehaviour
    {
        public static PowerUpsFactory Instance;
        [SerializeField] private GameObject _ammoPrefab;
        [SerializeField] private GameObject _healthPrefab;
        [SerializeField] private GameObject _minePrefab;
        
        private void Awake()
        {
            Instance = this;
        }

        public GameObject CreatePowerUp(PowerUpType type, GameObject gameObject)
        {
            var position = gameObject.transform.position;
            GameObject powerUpPrefab = null;
            
            switch (type)
            {
                case PowerUpType.Ammo:
                    powerUpPrefab = _ammoPrefab;
                    break;
                case PowerUpType.Health:
                    powerUpPrefab = _healthPrefab;
                    break;
                case PowerUpType.Mine:
                    powerUpPrefab = _minePrefab;
                    break;
                default:
                    return null;
            }
            
            return Instantiate(powerUpPrefab, 
                new Vector3(position.x, 2.5f, position.z), 
                powerUpPrefab.transform.rotation);
            
        }
    }
    
    public enum PowerUpType
    {
        Ammo,
        Health,
        Mine
    }
}