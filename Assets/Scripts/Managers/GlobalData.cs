using UnityEngine;

namespace Managers
{
    public class GlobalData : MonoBehaviour
    {
        public static GlobalData Instance;

        public bool IsVictory => _isVictory;
        [SerializeField] private bool _isVictory;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);
            Instance = this;

            DontDestroyOnLoad(this);
        }

        public void SetVictory(bool isVictory) => _isVictory = isVictory;
    }
}