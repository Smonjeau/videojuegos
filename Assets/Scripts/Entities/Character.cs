using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using Managers;
using Controllers;
using EventQueue;
using Strategy;
using UnityEngine;
using Weapons;

namespace Entities
{
    [RequireComponent( 
        typeof(MovementController),
        typeof(LifeController),
        typeof(SoldierSoundController))]
    [RequireComponent( typeof(Rigidbody))]

    public class Character : MonoBehaviour, IDieable
    {

        // CONTROLLERS
        private MovementController _movementController;
        private SoldierSoundController _soundEffectController;

        
        private Gun _selectedGun;
        private int _selectedGunIndex;
        private bool _isDeploying;

        [SerializeField] public List<Gun> Guns;
        [SerializeField] public MineDeployerWeapon Deployable;
        private GameObject _mineDeployer;

        // MOVEMENT KEYS
        [SerializeField] private KeyCode _moveForward = KeyCode.W;
        [SerializeField] private KeyCode _moveBack = KeyCode.S;
        [SerializeField] private KeyCode _moveLeft = KeyCode.A;
        [SerializeField] private KeyCode _moveRight = KeyCode.D;
        
        
        //COMBAT
        [SerializeField] private KeyCode _weaponSlot1 = KeyCode.Alpha1;
        [SerializeField] private KeyCode _weaponSlot2 = KeyCode.Alpha2;
        [SerializeField] private KeyCode _weaponSlot3 = KeyCode.Alpha3;
        [SerializeField] private KeyCode _weaponSlot4 = KeyCode.Alpha4;
        private CmdAttack _cmdAttack;
        private CmdDeploy _cmdDeploy;

        private bool _isFiring;
        
        [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
        [SerializeField] private KeyCode _reload = KeyCode.R;

        
        
        
        //MOVEMENT
        private CmdMovement _cmdMoveForward;
        private CmdMovement _cmdMoveBack;
        private CmdMovement _cmdMoveRight;
        private CmdMovement _cmdMoveLeft;

        private CmdRotationX _cmdRotateLeft;
        private CmdRotationX _cmdRotateRight;
        private CmdRotationY _cmdRotateUp;
        private CmdRotationY _cmdRotateDown;
        
        private Vector3 _rotationX;
        private Vector3 _rotationY;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");


        // Start is called before the first frame update
        void Start() {
            _movementController = GetComponent<MovementController>();
            _soundEffectController = GetComponent<SoldierSoundController>();

            foreach (var gun in Guns) {gun.gameObject.SetActive(false);}

            _selectedGun = Guns[_selectedGunIndex];
            _selectedGun.gameObject.SetActive(true);
            _selectedGun.Reset(false);
            _cmdAttack = new CmdAttack(_selectedGun);
            _cmdDeploy = new CmdDeploy(Deployable);



            _cmdMoveForward = new CmdMovement(_movementController, Directions.Forward);
            _cmdMoveBack = new CmdMovement(_movementController, Directions.Backward);
            _cmdMoveLeft = new CmdMovement(_movementController, Directions.Left);
            _cmdMoveRight = new CmdMovement(_movementController, Directions.Right);
     
            EventsManager.Instance.OnLowLife += OnLowLife;
            EventsManager.Instance.OnLifeHealed += OnLifeHealed;
            EventsManager.Instance.OnPlayerHitWithThrowable += OnPlayerHitWithThrowable;
            
            Transform rightHand = transform.GetChild(2).GetChild(0);

            _mineDeployer = rightHand.GetChild(rightHand.childCount - 1).gameObject;
            Debug.Log("CHARACTER");
            Debug.Log(_mineDeployer.name+"was found as mine deployer");
            _mineDeployer.SetActive(false);
            Debug.Log("mine is active? "+_mineDeployer.activeSelf);

        }

   

        void Update()
        {
            
           


            if (GlobalData.Instance.GamePaused) return;
            
           
            
            if (Input.GetKey(_moveForward)) EventQueueManager.Instance.AddMovementCommand(_cmdMoveForward);
            if (Input.GetKey(_moveBack))    EventQueueManager.Instance.AddMovementCommand(_cmdMoveBack);
            if (Input.GetKey(_moveLeft))    EventQueueManager.Instance.AddMovementCommand(_cmdMoveLeft);
            if (Input.GetKey(_moveRight))   EventQueueManager.Instance.AddMovementCommand(_cmdMoveRight);
            
            
            _rotationX = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
            _rotationY = new Vector3(Input.GetAxis("Mouse Y"), 0f, 0f);

            

            if(Input.GetKeyDown(_attack))
            {
                _isFiring = true;
            }

            if (Input.GetKeyUp(_attack))
            {
                _isFiring = false;
            }

            if (_isDeploying && _isFiring)
            {
                Debug.LogWarning("deploying, sending command to deploy");
                EventQueueManager.Instance.AddCommand(_cmdDeploy);
            }
            if (!_isDeploying &&_isFiring)
            {
                EventQueueManager.Instance.AddCommand(_cmdAttack);

                if (!_selectedGun.IsAutomatic || _selectedGun.CurrentMagSize == 0)
                    _isFiring = false;
            }




            if (Input.GetKeyDown(_reload)) _selectedGun.Reload();

            if (Input.GetKeyDown(KeyCode.Y))
            {
                _selectedGunIndex++;
                if (_selectedGunIndex >= Guns.Count)
                    _selectedGunIndex = 0;
                ChangeWeapon(_selectedGunIndex);
            }
            
            if (Input.GetKeyDown(_weaponSlot1)) ChangeWeapon(0);
            if (Input.GetKeyDown(_weaponSlot2)) ChangeWeapon(1);
            if (Input.GetKeyDown(_weaponSlot3)) ChangeWeapon(2);
            if (Input.GetKeyDown(_weaponSlot4)) ChangeWeapon(3);

        }

        private void OnLowLife()
        {
            _soundEffectController.Play();
        }

        private void OnLifeHealed(int life, int maxLife, int criticalLife)
        {
            _soundEffectController.Stop();
        }

        private void FixedUpdate()
        {
            _movementController.RotateX(_rotationX);
            _movementController.RotateY(-_rotationY);
        }
        
        private void ChangeWeapon(int index)
        {

            if (_selectedGun.IsReloading) return;
            if (_isDeploying)return;
            _selectedGun.gameObject.SetActive(false);
            _selectedGun = Guns[index];
            _selectedGunIndex = index;
            _selectedGun.gameObject.SetActive(true);
            _selectedGun.Reset(true);
            _cmdAttack = new CmdAttack(_selectedGun);

            EventsManager.Instance.EventWeaponChange(index);
            EventsManager.Instance.EventAmmoChange(_selectedGun.CurrentMagSize,_selectedGun.CurrentAmmo,_selectedGun.InfiniteAmmo);

        }

        public void Die()
        {
            EventsManager.Instance.EventGameOver(false);
            _soundEffectController.Stop();
            Destroy(gameObject);
        }
        private void OnPlayerHitWithThrowable()
        {
            _soundEffectController.PlayHitByThrowable();
        }

        public void ActivateDeployable()
        {
            foreach (var gun in Guns) {gun.gameObject.SetActive(false);}

            _isDeploying = true;
            Debug.Log("attempting to activate mine");
            _mineDeployer.SetActive(true);
            
            Debug.Log("mine active?"+_mineDeployer.activeSelf);
            
            _cmdDeploy = new CmdDeploy(_mineDeployer.GetComponent<MineDeployerWeapon>());
            // transform.Find("MineDeployer").gameObject.SetActive(true);
            // Deployable.gameObject.SetActive(true);

        }

        public void DeployableReset()
        {
         
            Debug.Log("Starting recovery");
            _isDeploying = false;
            _selectedGun.gameObject.SetActive(true);
            _selectedGun.Reset(true);
            _cmdAttack = new CmdAttack(_selectedGun);
            EventsManager.Instance.EventWeaponChange(_selectedGunIndex);
            EventsManager.Instance.EventAmmoChange(_selectedGun.CurrentMagSize,_selectedGun.CurrentAmmo,_selectedGun.InfiniteAmmo);
            _mineDeployer.SetActive(false);
            Debug.Log("ended recovery");
            
        }
    }
}
