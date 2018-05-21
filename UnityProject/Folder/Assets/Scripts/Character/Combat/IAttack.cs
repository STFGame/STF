using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combat
{
    public interface IAttack
    {
        bool Hit { get; set; }
        float Damage { get; }
    }
}
