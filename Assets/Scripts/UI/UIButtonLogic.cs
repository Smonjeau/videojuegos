using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UI;
using UnityEngine;

using UnityEngine.SceneManagement;
public class UIButtonLogic : MonoBehaviour
{
    public void LoadMenuScene() => SceneManager.LoadScene("Main Menu");

    public void LoadGameScene() => SceneManager.LoadScene("SampleScene");

    public void LoadHelpScene() => Debug.Log("Help Scene");
    
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
        StartCoroutine(uiManager.FadeOut("Main Menu",true));
    }
}
