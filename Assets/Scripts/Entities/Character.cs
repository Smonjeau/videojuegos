using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent( typeof(Rigidbody))]

public class Character : MonoBehaviour
{

    private MovementController _movementController;
    private LifeController _lifeController;
    // [SerializeField] private Gun _gun;
    
    // MOVEMENT KEYS
    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBackward = KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;
    
    



    
    public Vector3 movement;
    public Vector3 rotationX;
    public Vector3 rotationY;

    
    // ATTACK KEYS
    // [SerializeField] private KeyCode _attack = KeyCode.Space;
    // [SerializeField] private KeyCode _reload = KeyCode.R;
    
    // Start is called before the first frame update
    void Start() {
        _movementController = GetComponent<MovementController>();
        _lifeController = GetComponent<LifeController>();


    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(0f, 0f, 0f);

        if (Input.GetKey(_moveForward)) movement = transform.forward;
        if (Input.GetKey(_moveBackward)) movement =  -transform.forward;
        if (Input.GetKey(_moveLeft))    movement =  -transform.right;
        if (Input.GetKey(_moveRight))   movement =  transform.right;

        
        
        rotationX = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
        rotationY = new Vector3(Input.GetAxis("Mouse Y"), 0f, 0f);
        
        
        //TODO SACAME
        if (Input.GetKey(KeyCode.G)) EventsManager.Instance.EventGameOver(true);
        // if (Input.GetKeyDown(_attack)) _gun.Shoot();;
        //
        // if (Input.GetKeyDown(_reload)) _gun.Reload();

    }
        private void FixedUpdate()
        {
            _movementController.RotateX(rotationX);
            _movementController.Travel(movement);
            _movementController.RotateY(-rotationY);
        }
}
