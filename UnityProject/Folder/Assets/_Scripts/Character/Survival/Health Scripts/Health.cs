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
    public abstract class Health : MonoBehaviour, IHealth, IDamagable
    {
        [SerializeField] protected float m_MaxHealth;
        protected float m_CurrentHealth;

        protected int m_HurtIndex;

        public float MaxHealth { get { return m_MaxHealth; } }
        public float CurrentHealth { get { return m_CurrentHealth; } set { m_CurrentHealth = value; } }

        public abstract event Action<float> HealthChange;

        public abstract void Execute();
        public abstract void RestoreHealth(float amount);

        public abstract void TakeDamage(float damage);

        public void HitArea(int hurtIndex)
        {
            m_HurtIndex = hurtIndex;
        }
    }
}
