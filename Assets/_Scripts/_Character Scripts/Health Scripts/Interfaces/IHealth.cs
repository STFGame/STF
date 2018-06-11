using System;

namespace Life
{
    /// <summary>
    /// Interface for health.
    /// </summary>
    public interface IHealth
    {
        event Action<float> HealthChange;

        void RestoreHealth(float restoreAmount);
        float CurrentHealth { get; }
        float MaxHealth { get; }
    }
}
