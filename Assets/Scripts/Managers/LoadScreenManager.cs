using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class LoadScreenManager : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private Text _progressValue;
        [SerializeField] private string _targetScene = "SampleScene";

        private void Start()
        {
            StartCoroutine(LoadAsync());
        }

        private IEnumerator LoadAsync()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_targetScene);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                var progress = operation.progress;
                _progressBar.fillAmount = progress + 0.1f;
                _progressValue.text = $"Cargando ... {Math.Floor(progress*100 + 10)} %";
                
                if(operation.progress >= .9f)
                {
                    _progressValue.text = "Espacio para continuar";
                
                    if(Input.GetKeyDown(KeyCode.Space)) operation.allowSceneActivation = true;

                }

                yield return null;
            }

        }
    }
}