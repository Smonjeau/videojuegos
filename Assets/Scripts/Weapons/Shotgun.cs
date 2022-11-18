using Entities;
using Managers;
using Strategy;
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

            if (_currentMagSize <= 0) return;
            for (int i = 0; i < BulletCount; i++)
            {
                RaycastHit hit;
                if (Physics.Raycast(fpsCamera.transform.position, GetShootingDir(), 
                        out hit, Range))
                {
                    MakeDamage(hit.collider);
                }
                    
                _soundEffectController.PlayOnShot();

            }
                
            _cooldownTimer = ShotCooldown;

            _currentMagSize--;
            StartCoroutine(UI_AmmoUpdater(0));
        }
        private Vector3 GetShootingDir()
        {
            var camTransform = fpsCamera.transform;
            Vector3 target = camTransform.position +
                             camTransform.forward * Range;

            target += new Vector3(Random.Range(-InaccuracyDistance, InaccuracyDistance),
                Random.Range(-InaccuracyDistance, InaccuracyDistance),
                Random.Range(-InaccuracyDistance, InaccuracyDistance));


            Vector3 direction = target - camTransform.transform.position;

            return direction.normalized;
        }
        
    }
    
 
}