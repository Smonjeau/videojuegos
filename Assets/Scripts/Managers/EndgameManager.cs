using UnityEngine;
using UnityEngine.UI;

namespace Managers
{

    public class EndgameManager : MonoBehaviour
    {
        [SerializeField] private Image _background;

        [SerializeField] private Sprite _victorySprite;
        [SerializeField] private Sprite _defeatSprite;


        private void Start()
        {
            var wasVictory = GlobalData.Instance.IsVictory;

            _background.sprite = wasVictory ? _victorySprite : _defeatSprite;

        }
    }

}