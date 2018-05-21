using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alerts;
using UnityEngine;

namespace Survival
{
    /// <summary>
    /// Shield class for characters that have shielding.
    /// </summary>
    [Serializable]
    public class Shield : Health
    {
        //Allows the character to not be effected by hits as much
        [SerializeField] protected float hitResist = 10f;

        public override event Action<float> HealthChange;

        public bool Shielding { get; protected set; }

        public override void TakeDamage(float damage)
        {
            m_CurrentHealth -= damage;

            HealthChange?.Invoke(m_CurrentHealth);
        }

        public virtual void Execute(bool shield) { }

        sealed public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override void RestoreHealth(float amount)
        {
            m_CurrentHealth += amount;
        }
    }
}
