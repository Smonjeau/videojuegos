using UnityEngine;

namespace Controllers
{
    public class DoorController : MonoBehaviour {
        // public GameObject DoorButton;
        [SerializeField] private float smooth;
 
        private Quaternion DoorOpen;
        private Quaternion DoorClosed;
        private bool open;
 
        void Start() {
            // DoorButton.SetActive(false);
        }
   
        void OnTriggerEnter(Collider player)
        {
            if (open || player.CompareTag("Player")) return;
 
            // if (cube.tag == "DoorButton")
            //     DoorButton.SetActive(true);
            // Debug.Log("button activated");
 
            DoorOpen = Quaternion.Euler(0, -90, 0);
            DoorClosed = transform.rotation;
 
            transform.rotation = Quaternion.Lerp(DoorClosed, DoorOpen, Time.deltaTime * smooth);
            Debug.Log("Door Opened");
            open = true;
        }
    }
}