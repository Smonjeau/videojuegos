namespace Controllers
{
    public class HeadController : HitController
    {
        public override void TakeDamage(int damage)
        {
            if (ParentLifeController.Life - damage * DamageRatio <= 0)
            {
                Destroy(this);
            }
            base.TakeDamage(damage);
        }
    }
}