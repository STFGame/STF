using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Character.Survival
{
    public interface IHealth
    {
        void Revive(float restoreHealth);

        float MaxHealth { get; }
        bool Dead { get; }
    }
}