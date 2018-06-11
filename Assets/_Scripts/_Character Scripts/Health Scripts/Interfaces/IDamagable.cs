namespace Life
{
    /// <summary>
    /// Damage interface that all object that can take damage inherit from.
    /// </summary>
    public interface IDamagable
    {
        void TakeDamage(float damage);
    }
}
