using Entities;
using Strategy;
using UnityEngine;

namespace PowerUps
{
    public class AmmoPowerUp : PowerUp
    {
        public override string Name => "Ammo";
        
        public override void Use(GameObject target)
        {
            var character = target.GetComponent<Character>();
            if (character == null) return;
            var guns = character.Guns;
            foreach (var gun in guns) gun.FullAmmo();
            Die();
        }
        
        
    }
}