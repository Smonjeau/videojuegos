using System;
using System.Collections;
using System.Collections.Generic;
using Flyweight;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image blackoutImg;
        

        [SerializeField] private Image _weapon;
        
        
        [SerializeField] private Text _ammo;

        
        [SerializeField] private List<Sprite> _weaponSprites;

        [SerializeField] private GunStats _stats;


        private void Start()
        {
            EventsManager.Instance.OnAmmoChange += OnAmmoChange;
            EventsManager.Instance.OnWeaponChange += OnWeaponChange;
            _weapon.sprite = _weaponSprites[0];
            
        }

        public IEnumerator FadeOut()
        {

            const int fadeSpeed = 3;
            const float rate = 1.0f / fadeSpeed;
            var progress = 0f;

            var wasVictory = GlobalData.Instance.IsVictory;
            var victoryColor = Color.black;
            ColorUtility.TryParseHtmlString("#660000", out var defeatColor);
            var color = wasVictory ? victoryColor : defeatColor;
            while (progress < 1f)
            {
                blackoutImg.color = Color.Lerp(Color.clear, color
                    , progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }

            SceneManager.LoadScene("Endgame");
        }
        
        public IEnumerator LoadGame()
        {

            const float fadeSpeed = 3f;
            const float rate = 1.0f / fadeSpeed;
            var progress = 0f;

            var color = Color.black;
            while (progress < 1f)
            {
                blackoutImg.color = Color.Lerp(color, Color.clear
                    , progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }

        }
        
        private void OnWeaponChange(int weaponId)
        {
            _weapon.sprite = _weaponSprites[weaponId];
        }
        
        private void OnAmmoChange(int currentAmmo, int maxAmmo)
        {
            _ammo.text = $"{currentAmmo}/{maxAmmo}";
        }
    }
}