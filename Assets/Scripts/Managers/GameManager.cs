using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool _isGameOver = false;
        [SerializeField] private bool _isVictory = false;

        private void Start()
        {
            EventsManager.Instance.OnGameOver += OnGameOver;
        }

        private void OnGameOver(bool isVictory)
        {
            _isGameOver = true;
            _isVictory = isVictory;

            Debug.Log(isVictory);
            GlobalData.Instance.SetVictory(_isVictory);

            Invoke(nameof(LoadEndgameScene), 1);
        }

        private void LoadEndgameScene()
        {
            SceneManager.LoadScene("Endgame");
        }
    }
}