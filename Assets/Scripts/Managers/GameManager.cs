using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;

        [SerializeField] private bool _isVictory;

        [SerializeField] private GameObject _pauseMenu;

        private void Start()
        {
            _uiManager = GetComponent<UIManager>();
            _pauseMenu = GameObject.FindGameObjectWithTag("pause");
            EventsManager.Instance.OnGameOver += OnGameOver;
            StartCoroutine(_uiManager.LoadGame());
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                PauseGame();
            if (Input.GetKey(KeyCode.LeftShift))
                ResumeGame();
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