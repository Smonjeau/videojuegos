using System;
using System.Collections;
using System.Collections.Generic;
using Flyweight;
using Levels;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        /* Text Reference */
        [SerializeField] private Text _nextLevel;
        [SerializeField] private Text _level;
        private bool _tranisitioningLevel;
        
        public Image blackoutImg;
        public Image redImg;

        [SerializeField] private Image _weapon;
        [SerializeField] private Text _ammo;
        [SerializeField] private List<Sprite> _weaponSprites;
        [SerializeField] private RawImage gunSight;
        [SerializeField] private RawImage hitmarker;
        [SerializeField] private GameObject _helpPopup;



        private void Start()
        {
            if (SceneManager.GetActiveScene().name != "SampleScene") return;
            LevelManager.Instance.OnNextLevel += OnNextLevel;
            LevelManager.Instance.OnLevelTransition += OnLevelTransition;
            EventsManager.Instance.OnAmmoChange += OnAmmoChange;
            EventsManager.Instance.OnWeaponChange += OnWeaponChange;
            EventsManager.Instance.OnAttacked += OnAttacked;
            EventsManager.Instance.OnLowLife += OnLowLife;
            EventsManager.Instance.OnLifeHealed += OnLifeHealed;
            EventsManager.Instance.OnHit += OnHit;
            _weapon.sprite = _weaponSprites[0];
            _tranisitioningLevel = false;

        }

        private void OnLifeHealed(int life, int maxLife, int criticalLife)
        {
            var opacity = CalculateBloodOpacity(life, criticalLife);
            PaintBloodOnScreen(opacity);
        }

        private void OnAttacked(int life, int maxLife, int criticalLife)
        {
            var opacity = CalculateBloodOpacity(life, maxLife);
            PaintBloodOnScreen(opacity);
            if (life > criticalLife*2)
                StartCoroutine(FadeTextToZeroAlpha(1f, redImg));
        }
        
        private void OnLowLife()
        {
            PaintBloodOnScreen(1f);
        }
        
        private float CalculateBloodOpacity(int life, int criticalLife)
        {
            var opacity = 1 - ((float)life / criticalLife);
            if (life > criticalLife) opacity = 0;
            return opacity;
        }

        public IEnumerator FadeOut(string scene,bool blackColor)
        {

            const int fadeSpeed = 3;
            const float rate = 1.0f / fadeSpeed;
            var progress = 0f;

            var black = Color.black;
            ColorUtility.TryParseHtmlString("#660000", out var red);
            var color = blackColor ? black : red;
            while (progress < 1f)
            {
                blackoutImg.color = Color.Lerp(Color.clear, color
                    , progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }

            SceneManager.LoadScene(scene);
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

        private void PaintBloodOnScreen(float opacity)
        {
            var color = redImg.color;
            color.a = opacity;
            redImg.color = color;
        }
        
        private void OnWeaponChange(int weaponId)
        {
            _weapon.sprite = _weaponSprites[weaponId];
        }
        
        private void OnAmmoChange(int currentAmmo, int maxAmmo,bool infiniteAmmo)
        {
            _ammo.text = infiniteAmmo ? $"{currentAmmo}/∞" : $"{currentAmmo}/{maxAmmo}";
        }

        private void OnNextLevel(LevelStats nextLevel)
        {
            string textToSet;
            if (nextLevel.IsLevelStarterRound)
            {
                if (_tranisitioningLevel)
                {
                    _nextLevel.text = "";
                    _tranisitioningLevel = false;
                }

                textToSet = "Room " + nextLevel.RoomNumber + "\nRound " + nextLevel.LevelName;
            }
            else
            {
                textToSet = "Round " + nextLevel.LevelName;

            }
            _nextLevel.text = textToSet;
            StartCoroutine(FadeTextToFullAlpha(2f, _nextLevel));
            _level.text = nextLevel.LevelName;
            Invoke(nameof(DisableLevelText), 5f);
        }

        private void OnLevelTransition()
        {
            _tranisitioningLevel = true;
            _nextLevel.text = "Go through the door to enter next room\nNew weapon available in next room";
            StartCoroutine(FadeTextToFullAlpha(2f, _nextLevel,100));
        }
        private void OnHit()
        {
            hitmarker.gameObject.SetActive(true);
            Invoke(nameof(DisableHitmarker),0.5f);
        }

        private void DisableHitmarker()
        {
            hitmarker.gameObject.SetActive(false);
        }
        
        private void DisableLevelText()
        {
            StartCoroutine(FadeTextToZeroAlpha(2f, _nextLevel));
        }
        
        private IEnumerator FadeTextToFullAlpha(float t, Text i,int fontSize = 300)
        {
            i.fontSize = fontSize;
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
            while (i.color.a < 1.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
                yield return null;
            }

        }
 
        private IEnumerator FadeTextToZeroAlpha(float t, MaskableGraphic i)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            while (i.color.a > 0.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
                yield return null;
            }
        }

        public void SetGunSight(bool active)
        {
            gunSight.gameObject.SetActive(active);
        }

        public void ShowHelpPopup()
        {
            _helpPopup.SetActive(true);
        }

        public void CloseHelpPopup()
        {
            _helpPopup.SetActive(false);
        }

        public bool IsHelpPopupActive()
        {
            return _helpPopup.activeSelf;
        }
        
    }
}