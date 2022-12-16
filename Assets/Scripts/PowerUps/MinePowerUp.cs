using Entities;
using UnityEngine;

namespace PowerUps
{
    public class MinePowerUp : PowerUp
    {
        public override string Name => "Mine";
        
        public override void Use(GameObject target)
        {
            var character = target.GetComponent<Character>();
            if (character == null) return;
            if(!character.CanDeploy())return;
            character.ActivateDeployable();
            Die();
        }
    }
}