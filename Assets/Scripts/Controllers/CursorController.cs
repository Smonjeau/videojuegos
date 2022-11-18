using Managers;
using UI;
using UnityEngine;

namespace Controllers
{
    public class CursorController : MonoBehaviour
    {
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void Update()
        {

            if (GlobalData.Instance.GameOver)
            {
                Cursor.lockState = CursorLockMode.None;

                return;

            }
            bool gamePaused = GlobalData.Instance.GamePaused;
            switch (gamePaused)
            {
                case true when Cursor.lockState == CursorLockMode.Locked:
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case false:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        Cursor.lockState = CursorLockMode.None;
                    }
            
                    if (Input.GetMouseButtonDown(0))
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                    }

                    break;
                }
            }
        }
        
    }
}