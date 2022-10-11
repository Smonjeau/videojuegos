using System.Collections.Generic;
using Command;
using Managers;
using Controllers;
using EventQueue;
using UnityEngine;

namespace Entities
{
    [RequireComponent( 
        typeof(MovementController),
        typeof(LifeController))]
    [RequireComponent( typeof(Rigidbody))]

    public class Character : MonoBehaviour
    {

        private MovementController _movementController;
        private LifeController _lifeController;
        private Gun _selectedGun;
        private int _selectedGunIndex=0;

        [SerializeField] private List<Gun> _guns;

        // MOVEMENT KEYS
        [SerializeField] private KeyCode _moveForward = KeyCode.W;
        [SerializeField] private KeyCode _moveBack = KeyCode.S;
        [SerializeField] private KeyCode _moveLeft = KeyCode.A;
        [SerializeField] private KeyCode _moveRight = KeyCode.D;
        
        
        
        
        private CmdMovement _cmdMoveForward;
        private CmdMovement _cmdMoveBack;
        private CmdMovement _cmdMoveRight;
        private CmdMovement _cmdMoveLeft;

        private CmdRotationX _cmdRotateLeft;
        private CmdRotationX _cmdRotateRight;
        private CmdRotationY _cmdRotateUp;
        private CmdRotationY _cmdRotateDown;

        
        public Vector3 movement;
        public Vector3 rotationX;
        public Vector3 rotationY;
        
        // ATTACK KEYS
        [SerializeField] private KeyCode _attack = KeyCode.Space;
        [SerializeField] private KeyCode _reload = KeyCode.R;
    
        // Start is called before the first frame update
        void Start() {
            _movementController = GetComponent<MovementController>();
            _lifeController = GetComponent<LifeController>();
            
            foreach (var gun in _guns) {gun.gameObject.SetActive(false);}
            _selectedGun = _guns[_selectedGunIndex];
            _selectedGun.gameObject.SetActive(true);
            _selectedGun.Reset();


            _cmdMoveForward = new CmdMovement(_movementController, Directions.Forward);
            _cmdMoveBack = new CmdMovement(_movementController, Directions.Backward);
            _cmdMoveLeft = new CmdMovement(_movementController, Directions.Left);
            _cmdMoveRight = new CmdMovement(_movementController, Directions.Right);
     

        }

        // Update is called once per frame
        void Update()
        {
            movement = new Vector3(0f, 0f, 0f);

            if (Input.GetKey(_moveForward)) EventQueueManager.Instance.AddMovementCommand(_cmdMoveForward);
            if (Input.GetKey(_moveBack))    EventQueueManager.Instance.AddMovementCommand(_cmdMoveBack);
            if (Input.GetKey(_moveLeft))    EventQueueManager.Instance.AddMovementCommand(_cmdMoveLeft);
            if (Input.GetKey(_moveRight))   EventQueueManager.Instance.AddMovementCommand(_cmdMoveRight);
            
            
            rotationX = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
            rotationY = new Vector3(Input.GetAxis("Mouse Y"), 0f, 0f);
        
        
            //TODO SACAME
            if (Input.GetKey(KeyCode.G)) EventsManager.Instance.EventGameOver(true);
            if (Input.GetKey(KeyCode.F)) EventsManager.Instance.EventGameOver(false);

            if (Input.GetKeyDown(_attack)) _selectedGun.Attack();
        
            if (Input.GetKeyDown(_reload)) _selectedGun.Reload();

            if (Input.GetKeyDown(KeyCode.Y))
            {
                _selectedGunIndex++;
                if (_selectedGunIndex >= _guns.Count)
                    _selectedGunIndex = 0;
                ChangeWeapon(_selectedGunIndex);
            }
        }
        
        private void FixedUpdate()
        {
            _movementController.RotateX(rotationX);
            _movementController.RotateY(-rotationY);
        }

        // private void GunSwitch(Gun gun)
        // {
        //     
        //     var newGunObj=Instantiate(gun.GunPrefab, transform.GetChild(0).position, transform.rotation);
        //     
        //     newGunObj.transform.SetAsFirstSibling();
        //     
        // }
        
        private void ChangeWeapon(int index)
        {
            // foreach (var gun in guns) gun.gameObject.SetActive(false);

            _selectedGun.gameObject.SetActive(false);
            _selectedGun = _guns[index];
            _selectedGun.gameObject.SetActive(true);
            _selectedGun.Reset();
            EventsManager.Instance.EventWeaponChange(index);
            EventsManager.Instance.EventAmmoChange(_selectedGun.CurrentMagSize,_selectedGun.MaxAmmo);

            // _cmdAttack = new CmdAttack(_currentGun);
            // EventsManager.instance.WeaponChange(index);
        }
    }
}
