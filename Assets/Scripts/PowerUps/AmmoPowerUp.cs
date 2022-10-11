using Strategy;
using UnityEngine;

namespace PowerUps
{
    public class AmmoPowerUp : PowerUp
    {
        public override string Name => "Ammo";
        
        public override void Use(GameObject target)
        {
            var guns = target.GetComponentsInChildren<IGun>();
            foreach (var gun in guns) gun.FullAmmo();
            Die();
        }
        
        
    }
}