using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{

    public class EndgameManager : MonoBehaviour
    {
        [SerializeField] private Image _background;

        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _victorySong;
        [SerializeField] private AudioClip _defeatSong;

        

        [SerializeField] private Sprite _victorySprite;
        [SerializeField] private Sprite _defeatSprite;
        
        [SerializeField] private Text _gameoverMessage;



        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            var wasVictory = GlobalData.Instance.IsVictory;
            _background.sprite = wasVictory ? _victorySprite : _defeatSprite;
            _gameoverMessage.text = wasVictory ? "VICTORY" : "DEFEAT";
            _gameoverMessage.fontSize = 160;
            _gameoverMessage.horizontalOverflow = HorizontalWrapMode.Overflow;
            _gameoverMessage.verticalOverflow = VerticalWrapMode.Truncate;
            _gameoverMessage.alignment = TextAnchor.UpperCenter;
            _gameoverMessage.fontStyle = FontStyle.Bold;
            _gameoverMessage.color = wasVictory ? new Color(0,0,0,225) : new Color(190,108,91,255); //TODO no se está aplicando bien

        }

        private void Awake()
        {
            var wasVictory = GlobalData.Instance.IsVictory;
            _audioSource.clip = wasVictory ? _victorySong : _defeatSong;
            _audioSource.Play();
        }
    }

}