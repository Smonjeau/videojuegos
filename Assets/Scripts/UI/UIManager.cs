using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        public Image blackoutImg;

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
    }
}