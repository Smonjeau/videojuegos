using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class UIButtonLogic : MonoBehaviour
{
    public void LoadMenuScene() => SceneManager.LoadScene("Main Menu");

    public void LoadGameScene() => SceneManager.LoadScene("SampleScene");

    public void LoadHelpScene() => Debug.Log("Help Scene");
    
    public void CloseGame() => Application.Quit();
}
