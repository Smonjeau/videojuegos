using UnityEngine;
using UnityEngine.UI;

namespace Managers
{

    public class EndgameManager : MonoBehaviour
    {
        [SerializeField] private Image _background;

        [SerializeField] private Sprite _victorySprite;
        [SerializeField] private Sprite _defeatSprite;
        
        [SerializeField] private Text _gameoverMessage;



        private void Start()
        {
            var wasVictory = GlobalData.Instance.IsVictory;
            _background.sprite = wasVictory ? _victorySprite : _defeatSprite;
            _gameoverMessage.text = wasVictory ? "VICTORY" : "DEFEAT";
            _gameoverMessage.fontSize = 160;
            _gameoverMessage.horizontalOverflow = HorizontalWrapMode.Overflow;
            _gameoverMessage.verticalOverflow = VerticalWrapMode.Truncate;
            _gameoverMessage.alignment = TextAnchor.UpperCenter;
            _gameoverMessage.fontStyle = FontStyle.Bold;
            _gameoverMessage.color = wasVictory ? new Color(0,0,0,225) : new Color(255,255,255,255);

        }
    }

}