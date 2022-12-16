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
        [SerializeField] private Level[] _levels;

        public static LevelManager Instance;
        private int _currentLevelNumber;
        private Level _currentLevel;

        public event Action OnZombieKill;
        public event Action OnBossKill;
        public event Action<LevelStats> OnNextLevel;
        public event Action OnLevelTransition;

        [SerializeField] private GameObject[] roomChangerColliders;
        [SerializeField] private GameObject[] doorColliderGameObjects;
        private Collider[] _doorColliders;

    private void Awake()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
        }
        
        private void Start()
        {
            _levels = GetComponentsInChildren<Level>();
            foreach (var level in _levels) level.gameObject.SetActive(false);
            foreach (var coll in roomChangerColliders) coll.SetActive(false);
            _doorColliders = new Collider[doorColliderGameObjects.Length];
            for (int i = 0;i<doorColliderGameObjects.Length;i++)
            {
                var coll = doorColliderGameObjects[i].GetComponent<Collider>();
                coll.isTrigger = false;
                _doorColliders[i] = coll;
            }

            _currentLevelNumber = 1;
            PlayLevel(_currentLevelNumber);
            
        }

        public void CompletedLevel(Level level)
        {
            //Si es el final termina el juego
            if (level.IsFinalLevel)
            {
                Destroy(_currentLevel.gameObject);
                EventsManager.Instance.EventGameOver(true);
            }
            //Si es un nivel normal, jugamos el siguiente
            else if(!level._stats.IsLevelFinisherRound)
            {
                Destroy(_currentLevel.gameObject);
                _currentLevelNumber++;
                PlayLevel(_currentLevelNumber);
            }
            //Si es un nivel de cambio de sala, hacemos el cambio
            else
            {
                OnLevelTransition?.Invoke();
                Debug.Log(_currentLevel._stats.RoomNumber);
                roomChangerColliders[_currentLevel._stats.RoomNumber-1].SetActive(true);
                _doorColliders[_currentLevel._stats.RoomNumber-1].isTrigger= true;
            }
        }

        private void PlayLevel(int levelNumber)
        {
            _currentLevel = _levels[levelNumber - 1];
            OnNextLevel?.Invoke(_currentLevel._stats);
            _currentLevel.gameObject.SetActive(true);
            _currentLevel.StartLevel();
        }

        public void ZombieKill()
        {
            OnZombieKill?.Invoke();
        }


        public void BossKill()
        {
            OnBossKill?.Invoke();
        }

        public void RoomChange()
        {
            doorColliderGameObjects[_currentLevel._stats.RoomNumber-1].GetComponent<DoorController>().CloseDoor();
            Destroy(_currentLevel.gameObject);
            _currentLevelNumber++;
            PlayLevel(_currentLevelNumber);
        }

    }
}