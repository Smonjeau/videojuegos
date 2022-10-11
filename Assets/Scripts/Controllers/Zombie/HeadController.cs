using System;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class HeadController : ChildLifeController
    {
        
        public override void TakeDamage(int damage)
        {
            var fixedDamage = Convert.ToInt32(damage * DamageRatio);
            
            if (ParentLifeController.Life - fixedDamage <= 0)
            {
                ParentLifeController.TakePartDamage(damage, true);
                Destroy(gameObject);
            }
            else base.TakeDamage(damage);
        }
        
    }
}