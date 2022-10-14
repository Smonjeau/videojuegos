using System;
using UnityEngine;

namespace Managers
{
    public class EventsManager : MonoBehaviour
    {
        public static EventsManager Instance;

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
        }
        
        
        public event Action<bool> OnGameOver;
        public event Action<int, int, bool> OnAmmoChange;
        public event Action<int> OnWeaponChange;
        public event Action OnAttacked;
        public event Action OnHit;

        
        public void EventGameOver(bool isVictory)
        {
            OnGameOver?.Invoke(isVictory);
        }

        public void EventAmmoChange(int currentAmmo, int maxAmmo, bool InfiniteAmmo)
        {
            OnAmmoChange?.Invoke(currentAmmo, maxAmmo, InfiniteAmmo);

        }

        public void EventWeaponChange(int idx)
        {
            OnWeaponChange?.Invoke(idx);
        }
        
        public void EventPlayerAttacked()
        {
            OnAttacked?.Invoke();
        }


        public void EventHit()
        {
            OnHit?.Invoke();
        }
    }
}