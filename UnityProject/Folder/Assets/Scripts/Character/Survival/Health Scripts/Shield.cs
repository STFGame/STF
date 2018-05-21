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

        protected AlertValue m_Alert;

        public override void TakeDamage(float damage)
        {
            m_CurrentHealth -= damage;
        }

        public virtual void Tick(bool shield) { }

        sealed public override void Tick()
        {
            throw new NotImplementedException();
        }

        #region Messaging
        public override void Inform(AlertValue alert)
        {
            m_Alert = alert;
        }
        #endregion
    }
}
