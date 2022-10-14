using Controllers;
using UI;
using UnityEngine;


namespace Managers
{
    

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;

        private SoundEffectController _soundEffectController;
        

        [SerializeField] private bool _isVictory;

        [SerializeField] private GameObject _pauseMenu;

        [SerializeField] private bool _gamePaused;

        private void Start()
        {
            _uiManager = GetComponent<UIManager>();
            _soundEffectController = GetComponent<SoundEffectController>();

            _pauseMenu = GameObject.FindGameObjectWithTag("pause");
            EventsManager.Instance.OnGameOver += OnGameOver;
            EventsManager.Instance.OnLowLife += OnLowLife;
            EventsManager.Instance.OnLifeHealed += OnLifeHealed;
            StartCoroutine(_uiManager.LoadGame());
        }




        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || _uiManager.IsHelpPopupActive()) return;
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
        
        private void OnLowLife()
        {
            _soundEffectController.PlayOnLowLife();
            _uiManager.DisplayCriticLifeIndicator();
        }

        
        private void OnLifeHealed()
        {
            _soundEffectController.StopLowLifeSound();
        }


   
    }
}