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
                    var rot = transform.rotation;
                    var random = new System.Random();
                    //rot *= Quaternion.Euler(Vector3.up * (1f * (random.Next(0, 1) == 0 ? 1 : -1)));
                    rot *= Quaternion.Euler(Vector3.left * (1.2f * (random.Next(0, 1) == 0 ? 1 : -1)));
                    var bullet = Instantiate(
                        BulletPrefab,
                        _barrelExitTransform.position + Random.insideUnitSphere * 0.3f, rot);
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