﻿using UI;
using UnityEngine;


namespace Managers
{
    

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;

        [SerializeField] private bool _isVictory;

        [SerializeField] private GameObject _pauseMenu;

        [SerializeField] private bool _gamePaused;

        private void Start()
        {
            _uiManager = GetComponent<UIManager>();
            _pauseMenu = GameObject.FindGameObjectWithTag("pause");
            EventsManager.Instance.OnGameOver += OnGameOver;
            StartCoroutine(_uiManager.LoadGame());
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            if (_gamePaused) ResumeGame();
            else PauseGame();
            _gamePaused = !_gamePaused;
        }

        private void OnGameOver(bool isVictory)
        {
            _isVictory = isVictory;
            
            GlobalData.Instance.SetVictory(_isVictory);

            StartCoroutine(_uiManager.FadeOut("Endgame",isVictory));
        }
        

        private void PauseGame()
        {
            foreach (RectTransform child in _pauseMenu.transform) child.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            foreach (RectTransform child in _pauseMenu.transform) child.gameObject.SetActive(false);
            Time.timeScale = 1;
        }


   
    }
}