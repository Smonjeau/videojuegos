using System.Collections;
using System.Collections.Generic;
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
    
    
    public Rigidbody Rigidbody => _rigidbody;
    [SerializeField] private Rigidbody _rigidbody;



    
    public Vector3 movement;
    public Vector3 rotation;
    
    // ATTACK KEYS
    // [SerializeField] private KeyCode _attack = KeyCode.Space;
    // [SerializeField] private KeyCode _reload = KeyCode.R;
    
    // Start is called before the first frame update
    void Start() {
        _movementController = GetComponent<MovementController>();
        _lifeController = GetComponent<LifeController>();
        _rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(0f, 0f, 0f);

        if (Input.GetKey(_moveForward)) movement = transform.forward;
        if (Input.GetKey(_moveBackward)) movement =  -transform.forward;
        if (Input.GetKey(_moveLeft))    movement =  -transform.right;
        if (Input.GetKey(_moveRight))   movement =  transform.right;

        
        
        rotation = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);

        // if (Input.GetKeyDown(_attack)) _gun.Shoot();;
        //
        // if (Input.GetKeyDown(_reload)) _gun.Reload();

    }
        private void FixedUpdate()
        {
            _movementController.Rotate(rotation,_rigidbody);
            _movementController.Travel(movement,_rigidbody);
        }
}
