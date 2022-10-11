using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Flyweight;
using Levels;
using Strategy;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject _levelsContainer;
        private Level[] _levels;
        
        public static LevelManager Instance;
        private int _currentLevelNumber;
        private Level _currentLevel;
        
        public event Action OnZombieKill;
        public event Action<LevelStats> OnNextLevel;
        
        private void Awake()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
        }
        
        private void Start()
        {
            _levels = _levelsContainer.GetComponents<Level>();
            foreach (var level in _levels) level.enabled = false;
            _currentLevelNumber = 1;
            PlayLevel(_currentLevelNumber);
            
        }

        public void CompletedLevel(Level level)
        {
            if (level.IsFinalLevel)
            {
                Destroy(_currentLevel);
                EventsManager.Instance.EventGameOver(true);
            }
            else
            {
                Destroy(_currentLevel);
                _currentLevelNumber++;
                PlayLevel(_currentLevelNumber);
            }
        }

        private void PlayLevel(int levelNumber)
        {
            _currentLevel = _levels[levelNumber - 1];
            OnNextLevel?.Invoke(_currentLevel._stats);
            _currentLevel.enabled = true;
            _currentLevel.StartLevel();
        }

        public void ZombieKill()
        {
            OnZombieKill?.Invoke();
        }
        
    }
}