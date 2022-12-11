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
        public event Action<int, int,bool> OnAmmoChange;
        public event Action<int> OnWeaponChange;
        public event Action<int, int, int> OnAttacked;
        public event Action OnHit;
        public event Action OnLowLife;
        public event Action<int, int, int> OnLifeHealed;
        public event Action OnPlayerHitWithThrowable;

        
        public void EventGameOver(bool isVictory)
        {
            OnGameOver?.Invoke(isVictory);
        }

        public void EventAmmoChange(int currentAmmo, int maxAmmo, bool infiniteAmmo)
        {
            OnAmmoChange?.Invoke(currentAmmo, maxAmmo,infiniteAmmo);

        }

        public void EventWeaponChange(int idx)
        {
            OnWeaponChange?.Invoke(idx);
        }
        
        public void EventPlayerAttacked(int life, int maxLife, int criticalLife)
        {
            OnAttacked?.Invoke(life, maxLife, criticalLife);
        }


        public void EventHit()
        {
            OnHit?.Invoke();
        }

        public void EventLowLife()
        {
            OnLowLife?.Invoke();
        }

        public void EventLifeHealed(int life, int maxLife, int criticalLife)
        {
            OnLifeHealed?.Invoke(life, maxLife, criticalLife);
        }

        public void EventPlayerHitWithThrowable()
        {
            OnPlayerHitWithThrowable?.Invoke();
        }
    }
}