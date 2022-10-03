using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (Input.GetKey(_moveForward))
            _movementController.Travel(transform.forward);
        
        if (Input.GetKey(_moveBackward))
            _movementController.Travel(-transform.forward);
        
        if (Input.GetKey(_moveLeft))
            _movementController.Rotate(-transform.up);
        
        if (Input.GetKey(_moveRight))
            _movementController.Rotate(transform.up);

        // if (Input.GetKeyDown(_attack)) _gun.Shoot();;
        //
        // if (Input.GetKeyDown(_reload)) _gun.Reload();
    }
}
