using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survival
{
    /// <summary>
    /// Health interface that all objects with health inherit from
    /// </summary>
    public interface IHealth
    {
        void Restore(float restoreHealth);
        float CurrentHealth { get; set; }
        float MaxHealth { get; }
    }
}