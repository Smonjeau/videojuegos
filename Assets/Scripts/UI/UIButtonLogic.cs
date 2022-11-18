using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UI;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(GameManager), typeof(UIManager))]
    public class UIButtonLogic : MonoBehaviour
    {
        public void LoadMenuScene() => SceneManager.LoadScene("Main Menu");

        public void LoadGameScene()
        {
            SceneManager.LoadScene("Load Screen");
        }

        public void LoadHelpPopup()
        {
            var uiManager = GetComponent<UIManager>();
            uiManager.ShowHelpPopup();
        }
        
        public void CloseGame() => Application.Quit();

        public void ClosePauseMenu()
        {
            var gameManager = GetComponent<GameManager>();
            gameManager.ResumeGame();
        }

        public void LoadMenuSceneFromGame()
        {
            var uiManager = GetComponent<UIManager>();
            var gameManager = GetComponent<GameManager>();
            gameManager.ResumeGame();
            GlobalData.Instance.SetGameOver(true);
            StartCoroutine(uiManager.FadeOut("Main Menu",true));
        }

        public void CloseHelpPopup()
        {
            var uiManager = GetComponent<UIManager>();
            uiManager.CloseHelpPopup();
        }
    }
    
}
