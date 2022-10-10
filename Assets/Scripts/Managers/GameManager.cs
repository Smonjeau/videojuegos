using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;

        [SerializeField] private bool _isGameOver = false;
        [SerializeField] private bool _isVictory = false;

        private void Start()
        {
            _uiManager = GetComponent<UIManager>();
            EventsManager.Instance.OnGameOver += OnGameOver;
            StartCoroutine(_uiManager.LoadGame());
        }

        private void OnGameOver(bool isVictory)
        {
            _isGameOver = true;
            _isVictory = isVictory;
            
            GlobalData.Instance.SetVictory(_isVictory);

            StartCoroutine(_uiManager.FadeOut());
        }


   
    }
}