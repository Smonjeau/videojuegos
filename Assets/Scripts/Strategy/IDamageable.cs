namespace Strategy
{
    public interface IDamageable
    {
        int Life { get; }
        int MaxLife { get; }
        void TakeDamage(int damage);
        void SetLife(int life);
        void Heal(int heal);
        void ResetLife();
    }
}
