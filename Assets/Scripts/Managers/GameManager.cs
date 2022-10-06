using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIController _uiController;

        [SerializeField] private bool _isGameOver = false;
        [SerializeField] private bool _isVictory = false;

        private void Start()
        {
            _uiController = GetComponent<UIController>();
            EventsManager.Instance.OnGameOver += OnGameOver;
        }

        private void OnGameOver(bool isVictory)
        {
            _isGameOver = true;
            _isVictory = isVictory;

            GlobalData.Instance.SetVictory(_isVictory);

            
            StartCoroutine(_uiController.FadeOut());
        }

   
    }
}