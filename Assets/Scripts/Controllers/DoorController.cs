using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class DoorController : MonoBehaviour {
        // public GameObject DoorButton;
        [SerializeField] private float smooth;
 
        private Vector3 _doorOpen;
        private Vector3 _doorClosed;
        private bool _open;
 
        private void Start() {
            var position = transform.position;
            _doorOpen = position;
            _doorOpen.x = 0.56f;
            _doorClosed = position;
        }
   
        void OnTriggerEnter(Collider player)
        {
            if (_open || player.CompareTag("Player")) return;
 
            // if (cube.tag == "DoorButton")
            //     DoorButton.SetActive(true);
            // Debug.Log("button activated");

     
 
            StartCoroutine(WaitAndMove(0.2f,_doorClosed,_doorOpen));
            _open = true;
            GetComponent<Collider>().isTrigger = false;
        }

        public void CloseDoor()
        {
            if (_open)
            {
                StartCoroutine(WaitAndMove(0.2f,_doorOpen,_doorClosed));
                _open = false;
            }
        }

        private IEnumerator WaitAndMove(float delayTime,Vector3 from, Vector3 to)
        {
            yield return new WaitForSeconds(delayTime); 
            float startTime = Time.time; 
            while (Time.time - startTime <= 1)
            {
                // until one second passed
                transform.position = Vector3.Lerp(from, to, Time.time - startTime); // lerp from A to B in one second
                yield return 1; // wait for next frame
                
            }
        }
    }
}