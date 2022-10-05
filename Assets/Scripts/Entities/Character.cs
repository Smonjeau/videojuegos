using System.Collections.Generic;
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
        [SerializeField] private KeyCode _moveBackward = KeyCode.S;
        [SerializeField] private KeyCode _moveLeft = KeyCode.A;
        [SerializeField] private KeyCode _moveRight = KeyCode.D;
    
    
        public Rigidbody Rigidbody => _rigidbody;
        [SerializeField] private Rigidbody _rigidbody;



    
        public Vector3 movement;
        public Vector3 rotation;
    
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

            if (Input.GetKeyDown(_attack)) _selectedGun.Attack();
        
            if (Input.GetKeyDown(_reload)) _selectedGun.Reload();

            if (Input.GetKeyDown(KeyCode.Y))
            {
                ChangeWeapon(++_selectedGunIndex>=_guns.Count?0:_selectedGunIndex);
            }
           
        

        }
        private void FixedUpdate()
        {
            _movementController.Rotate(rotation,_rigidbody);
            _movementController.Travel(movement,_rigidbody);
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

            // _cmdAttack = new CmdAttack(_currentGun);
            // EventsManager.instance.WeaponChange(index);
        }
    }
}
