using Entities;
using UnityEngine;

namespace Weapons
{
    public class GrenadeLauncher : Gun
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _throwForce = 30f;

        public override void Attack()
        {
            if (!CanFire() || _currentMagSize <= 0) return; //not ready to fire

            GameObject grenade = Instantiate(_bulletPrefab, _barrelExitTransform.position + transform.forward * 2, _barrelExitTransform.rotation);
            _soundEffectController.PlayOnShot();
            gunShootEffect.Play();
            
            grenade.GetComponent<Grenade>().SetDamage(Damage);
            
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * _throwForce, ForceMode.VelocityChange);
            _currentMagSize--;
            _recoilLogic.Recoil(transform.forward);
            StartCoroutine(UI_AmmoUpdater(0));
        }
        
    }
}