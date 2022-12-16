using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public enum WeaponType
    {
        Pistol,
        Shotgun,
        AssaultRifle,
        RocketLauncher
    }
    
    public class WeaponsManager : MonoBehaviour
    {
        public static WeaponsManager Instance;
        public List<WeaponType> ActiveWeaponTypes = new List<WeaponType>();
        
        public event Action<List<WeaponType>> OnWeaponsChanged;
        
        private void Awake()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
        }
        
        public void ChangeWeapons(List<WeaponType> weapons)
        {
            ActiveWeaponTypes = new List<WeaponType>(weapons);
            OnWeaponsChanged?.Invoke(ActiveWeaponTypes);
        }
    }
}