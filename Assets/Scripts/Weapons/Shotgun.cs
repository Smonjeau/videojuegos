using Entities;
using UnityEngine;

namespace Weapons
{
    public class Shotgun : Gun
    {

        public override void Attack()
        {
            if (!CanFire()) return; //not ready to fire
        
            if (_currentMagSize <= 0) //empty mag
            {
                _soundEffectController.PlayOnEmpty();
                return;
            }
            if (_currentMagSize > 0)
            {
                for (int i = 0; i < BulletCount; i++)
                {
                    var bullet = Instantiate(
                        BulletPrefab,
                        _barrelExitTransform.position + Random.insideUnitSphere * 0.6f, transform.rotation);
                    bullet.name = "Shotgun Bullet";
                    bullet.GetComponent<Bullet>().SetOwner(this);
                    _soundEffectController.PlayOnShot();

                }
                _cooldownTimer = ShotCooldown;

                _currentMagSize--;
                StartCoroutine(UI_AmmoUpdater(0));
            }
        }
    }
}