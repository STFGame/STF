using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survival
{
    /// <summary>
    /// Damage interface that all object that can take damage inherit from.
    /// </summary>
    public interface IDamagable
    {
        void TakeDamage(float damage);
    }
}
