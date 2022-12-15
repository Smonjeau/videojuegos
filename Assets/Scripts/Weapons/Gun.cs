using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Controllers;
using Entities;
using Flyweight;
using Managers;
using Strategy;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(SoundEffectController))]
    public abstract class Gun : MonoBehaviour, IGun
    {
        [SerializeField] private GunStats _stats;
        public GameObject GunPrefab => _stats.GunPrefab;
        public int MagSize => _stats.MagSize;
        public int MaxAmmo => _stats.MaxAmmo;
        public int Damage => _stats.Damage;
        public bool InfiniteAmmo => _stats.InfiniteAmmo;
        public string Name => _stats.WeaponName;
        public int BulletCount => _stats.BulletCount;
        public float ShotCooldown => 60/_stats.RateOfFire;
        public float ReloadCooldown => _stats.ReloadCooldown;

        public bool IsAutomatic => _stats.IsAutomatic;

        public int CurrentAmmo => _currentAmmo;

        public float RateOfFire => _stats.RateOfFire;

        public float Range => _stats.Range;

        public float RecoilY => _stats.RecoilY;
        public float RecoilX => _stats.RecoilX;
        public float RecoilDuration => _stats.RecoilDuration;


        private Animator _parentAnimator;

        protected RecoilLogic _recoilLogic;

        public float InaccuracyDistance => _stats.InaccuracyDistance;

        
        protected float _cooldownTimer = 0;
        protected Transform _barrelExitTransform;
        protected SoundEffectController _soundEffectController;
        private GameObject _parentArm;
        public int CurrentMagSize => _currentMagSize;
        public bool IsReloading => _isReloading;
        
        [SerializeField] protected int _currentMagSize;
        [SerializeField] private int _currentAmmo;
        [SerializeField] private bool _isReloading;
        
        [SerializeField] protected List<int> _layerTarget = new List<int>{6,7};

        [SerializeField] protected Camera fpsCamera;
        [SerializeField] protected ParticleSystem gunShootEffect;

        private static readonly int Reloading = Animator.StringToHash("reloading");

        private void Start()
        {
            _currentMagSize = MagSize;
            if (!InfiniteAmmo) _currentAmmo = MaxAmmo - _currentMagSize;
            EventsManager.Instance.EventAmmoChange(_currentMagSize,_currentAmmo,InfiniteAmmo);
            
            _barrelExitTransform = transform.GetChild(0);
            _soundEffectController = GetComponent<SoundEffectController>();
            _recoilLogic = GetComponent<RecoilLogic>();
            _recoilLogic.SetValues(RecoilY, RecoilX, RecoilDuration);
            
            _parentAnimator = transform.parent.parent.GetComponent<Animator>();
            
            SetShootingPosition(); 
        }
        
        private void SetShootingPosition()
        {
            gunShootEffect.transform.position = _barrelExitTransform.position;

        }

        private void Update()
        {
            _cooldownTimer -= Time.deltaTime;
        }

        public virtual void Attack()
        {
            if (!CanFire()) return; //not ready to fire
            
            if (_currentMagSize <= 0) //empty mag
            {
                _soundEffectController.PlayOnEmpty();
                return;
            }
            
            _soundEffectController.PlayOnShot();
            gunShootEffect.Play();
            
            RaycastHit hit;
            var cameraTransform = fpsCamera.transform;
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            _recoilLogic.Recoil(ray.direction);
            if (Physics.Raycast(ray, out hit, Range))
            {
                MakeDamage(hit.collider);
            }

            _currentMagSize--;
             StartCoroutine(UI_AmmoUpdater(0));

            _cooldownTimer = ShotCooldown;
        }
            
        
        public void Reload()
        {
            
            _isReloading = true;
            if (!CanFire() || OutOfAmmo() || HasFullMag()) {
                //not ready to fire
                _isReloading = false;
                return;
            }

            StartCoroutine(ReloadAnimation());
            _cooldownTimer = ReloadCooldown;
            
            if (InfiniteAmmo)
            {
                _currentMagSize = MagSize;
            }
            else
            {
                var ammoToAdd = _currentAmmo < MagSize ? _currentAmmo - _currentMagSize : MagSize - _currentMagSize;
                _currentMagSize += ammoToAdd;
                _currentAmmo -= ammoToAdd;
            }
            StartCoroutine(UI_AmmoUpdater(_cooldownTimer));
        }
        
        private IEnumerator ReloadAnimation()
        {
            
            _parentAnimator.SetBool(Reloading,true);
            _soundEffectController.PlayOnReloadStart();
            
            yield return new WaitForSeconds(ReloadCooldown-1);

            
            
            _parentAnimator.SetBool(Reloading,false);
           
            yield return new WaitForSeconds((float)0.5);
            _soundEffectController.PlayOnReloadEnd();
            yield return new WaitForSeconds((float)0.5);
           
            _isReloading = false;
            
        }

       
        
        

        public void AddAmmo(int amount)
        {
            if (InfiniteAmmo) return;
            if (_currentAmmo + amount > MaxAmmo) _currentAmmo = MaxAmmo;
            else _currentAmmo += amount;
        }

        public void FullAmmo()
        {
            _currentAmmo = MaxAmmo;
            if (gameObject.activeSelf) StartCoroutine(UI_AmmoUpdater(0));
        } 

        protected bool CanFire() => ! (_cooldownTimer > 0);

        public void Reset(bool updateShootingPosition)
        {
            _barrelExitTransform = transform.GetChild(0);
            if(updateShootingPosition)
                SetShootingPosition();
            _cooldownTimer = 0;
            
        }
         protected IEnumerator UI_AmmoUpdater(float secondsToSleep)
         {
             yield return new WaitForSeconds(secondsToSleep);
             EventsManager.Instance.EventAmmoChange(_currentMagSize, _currentAmmo,InfiniteAmmo);
         }

        private bool OutOfAmmo() => !InfiniteAmmo && _currentAmmo <= 0;

        private bool HasFullMag()
        {

            return _currentMagSize == MagSize;
        }

        protected void MakeDamage(Collider collider)
        {
            if (!_layerTarget.Contains(collider.gameObject.layer) ||
                collider.gameObject.CompareTag("Player")) return;
            
            
            //if whatever is hits is damageable, damage it
            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(Damage);
            EventsManager.Instance.EventHit();
        }

        
    }
    
}
