using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        

        [SerializeField] private bool _isVictory;

        [SerializeField] private GameObject _pauseMenu;

        [SerializeField] private bool _gamePaused;

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "Main Menu")
            {
                DontDestroyOnLoad(GameObject.Find("Menu Background Sound"));
            }

        }

        private void Start()
        {
            _uiManager = GetComponent<UIManager>();
            if (SceneManager.GetActiveScene().name != "SampleScene") return;
            _pauseMenu = GameObject.FindGameObjectWithTag("pause");
            EventsManager.Instance.OnGameOver += OnGameOver;
            StartCoroutine(_uiManager.LoadGame());
            Destroy(GameObject.Find("Menu Background Sound").gameObject);

        }




        private void Update()
        {
            if (SceneManager.GetActiveScene().name != "SampleScene") return;
            if (!Input.GetKeyDown(KeyCode.Escape) || _uiManager.IsHelpPopupActive()) return;
            if (_gamePaused) ResumeGame();
            else PauseGame();
            _gamePaused = !_gamePaused;
        }

        private void OnGameOver(bool isVictory)
        {
            _isVictory = isVictory;
            
            GlobalData.Instance.SetVictory(_isVictory);
            GlobalData.Instance.SetGameOver(true);
    
            StartCoroutine(_uiManager.FadeOut("Endgame",isVictory));
        }
        

        private void PauseGame()
        {
            foreach (RectTransform child in _pauseMenu.transform)
            {
                if(child.CompareTag("help-popup")) continue;
                child.gameObject.SetActive(true);
            }
            _uiManager.SetGunSight(false);
            GlobalData.Instance.SetGamePaused(true);
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            foreach (RectTransform child in _pauseMenu.transform)
            {
                if(child.CompareTag("help-popup")) continue;
                child.gameObject.SetActive(false);
            } 
            _uiManager.SetGunSight(true);
            GlobalData.Instance.SetGamePaused(false);

            Time.timeScale = 1;
        }


   
    }
}