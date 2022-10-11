using Strategy;
using UnityEngine;

namespace PowerUps
{
    public class HealthPowerUp : PowerUp
    {
        public override string Name => "Health";
        
        public override void Use(GameObject target)
        {
            var damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.ResetLife();
                Die();
            }
            
        }
    }
}