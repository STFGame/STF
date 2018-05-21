using Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Survival
{
    /// <summary>
    /// Base class for health components.
    /// </summary>
    public abstract class Health : MonoBehaviour, IDamagable, IAlert
    {
        #region Health Variables
        [SerializeField] protected float m_MaxHealth;
        protected float m_CurrentHealth;

        [HideInInspector] public bool m_IsHurt;
        [HideInInspector] public int m_HurtIndex;

        public float MaxHealth { get { return m_MaxHealth; } }
        public float CurrentHealth { get { return m_CurrentHealth; } set { m_CurrentHealth = value; } }
        #endregion

        public abstract void Tick();

        #region Damagable
        public abstract void TakeDamage(float damage);
        #endregion

        #region Alerts
        public abstract void Inform(AlertValue message);
        #endregion
    }
}
