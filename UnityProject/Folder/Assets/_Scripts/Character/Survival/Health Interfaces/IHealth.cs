using System;

namespace Survival
{
    /// <summary>
    /// Interface for health.
    /// </summary>
    public interface IHealth
    {
        event Action<float> HealthChange;
        float CurrentHealth { get; set; }
        float MaxHealth { get; }
    }
}
