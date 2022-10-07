namespace Controllers
{
    public class ZombieLifeController : LifeController
    {
        
        private Zombie _zombie;
        public new int MaxLife => _zombie.Stats.MaxLife;
        
        private void Start()
        {
            _zombie = GetComponent<Zombie>();
            SetLife(MaxLife);
        }
        
        public override void Die()
        {
            _zombie.Die();
        }
    }
}