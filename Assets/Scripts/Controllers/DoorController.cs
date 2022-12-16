using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class DoorController : MonoBehaviour {
        // public GameObject DoorButton;
        [SerializeField] private Vector3 doorOpen ;
 
        private Vector3 _doorClosed;
        //private Vector3 _doorClosed;
        private bool _open;
 
        private void Start() {
            _doorClosed = transform.position;
           
        }
   
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("EN ON TRIGGER DE PUERTA");
            Debug.Log("EL CHOQUE FUE CON el tag" + other.tag);
            Debug.Log("EL CHOQUE FUE CON el obejto" + other.name);

            if (_open || !other.CompareTag("Player")) return;
 
            // if (cube.tag == "DoorButton")
            //     DoorButton.SetActive(true);
            // Debug.Log("button activated");

            Debug.Log("ABRIENDO PUERTA");
 
            StartCoroutine(WaitAndMove(0.2f,_doorClosed,doorOpen));
            _open = true;
            GetComponent<Collider>().isTrigger = false;
        }

        public void CloseDoor()
        {
            if (_open)
            {
                StartCoroutine(WaitAndMove(0.2f,doorOpen,_doorClosed));
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