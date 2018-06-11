using System;
using System.Collections;
using UnityEngine;

namespace Life
{
    /// <summary>
    /// Component for health
    /// </summary>
    public class Health : MonoBehaviour, IHealth, IDamagable
    {
        [SerializeField] [Range(0f, 2000f)] protected float m_maxHealth;
        protected float m_currentHealth;

        public float MaxHealth { get { return m_maxHealth; } }
        public float CurrentHealth { get { return m_currentHealth; } }

        //An Action event that informs subscribers when health has changed
        public virtual event Action<float> HealthChange;

        private int _count = 0;
        private Shield m_shield;

        private void Awake()
        {
            m_currentHealth = m_maxHealth;

            m_shield = GetComponent<Shield>();
        }

        public virtual void RestoreHealth(float restoreAmount)
        {
            float restoreAdjust = (m_currentHealth + restoreAmount);
            if (restoreAmount > m_maxHealth)
                restoreAmount = m_maxHealth;

            if (m_currentHealth == restoreAdjust)
                return;

            m_currentHealth = restoreAdjust;

            HealthChange?.Invoke(m_currentHealth);
        }

        public virtual void TakeDamage(float damage)
        {
            if (m_shield)
                if (m_shield.Shielding)
                    return;

            if (_count > 0)
                return;

            if (m_currentHealth <= 0)
            {
                m_currentHealth = 0f;
                return;
            }

            _count++;
            float damageAdjust = (m_currentHealth - damage);

            if (damageAdjust <= 0)
                damageAdjust = 0;

            if (m_currentHealth == damageAdjust)
                return;

            m_currentHealth = damageAdjust;

            HealthChange?.Invoke(m_currentHealth);

            StopCoroutine(DamageDelay());
            StartCoroutine(DamageDelay());
        }

        private IEnumerator DamageDelay()
        {
            yield return new WaitForSeconds(0.4f);
            _count = 0;
        }
    }
}
