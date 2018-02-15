using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Player
{
    class Damage
    {
        public delegate void DamageEvent();
        public event DamageEvent OnDamageTaken;

        public void Damaged(float amount)
        {
            if (OnDamageTaken != null)
                OnDamageTaken();
        }

    }
}
