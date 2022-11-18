using UnityEngine;

namespace Managers
{
    public class GlobalData : MonoBehaviour
    {
        public static GlobalData Instance;

        public bool IsVictory => _isVictory;
        [SerializeField] private bool _isVictory;

        public bool GamePaused => _gamePaused;
        [SerializeField] private bool _gamePaused;

        public bool GameOver => _isGameOver;
        [SerializeField] private bool _isGameOver;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);
            Instance = this;

            DontDestroyOnLoad(this);
        }

        public void SetVictory(bool isVictory) => _isVictory = isVictory;

        public void SetGamePaused(bool paused) => _gamePaused = paused;

        public void SetGameOver(bool value) => _isGameOver = value;
    }
}