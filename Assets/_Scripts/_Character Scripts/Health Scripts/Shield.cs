using System;
using UnityEngine;

namespace Life
{
    /// <summary>
    /// Shield class for characters that have shielding.
    /// </summary>
    [Serializable]
    public class Shield : MonoBehaviour, IHealth, IDamagable
    {
        [SerializeField] [Range(0f, 2000f)] private float m_maxShield = 100f;
        [SerializeField] private float m_shieldReplenishRate;
        [SerializeField] private float m_shieldDepleteRate;

        //Allows the character to not be effected by hits as much
        [SerializeField] private float m_hitResist = 10f;

        public event Action<float> HealthChange;

        private Animator m_shieldAnimator;

        public bool Shielding { get; private set; }
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get { return m_maxShield; } }

        private void Awake()
        {
            CurrentHealth = m_maxShield;

            m_shieldAnimator = GetComponent<Animator>();
        }

        public void Execute(bool shield)
        {
            if (shield)
                TakeDamage(m_shieldDepleteRate);
            else
                RestoreHealth(m_shieldReplenishRate);

            Shielding = (shield && CurrentHealth > 0);

            AnimateShield(Shielding);
        }

        public void TakeDamage(float damage)
        {
            if (!Shielding)
                return;

            if (CurrentHealth <= 0f)
            {
                CurrentHealth = 0f;
                return;
            }

            float shieldAdjust = (CurrentHealth - damage);

            if (shieldAdjust <= 0)
                shieldAdjust = 0;

            if (CurrentHealth == shieldAdjust)
                return;

            CurrentHealth = shieldAdjust;

            HealthChange?.Invoke(CurrentHealth);
        }

        public void RestoreHealth(float restoreAmount)
        {
            if (CurrentHealth >= m_maxShield)
            {
                CurrentHealth = m_maxShield;
                return;
            }

            float shieldAdjust = (CurrentHealth + restoreAmount);
            if (shieldAdjust > MaxHealth)
                shieldAdjust = MaxHealth;

            if (CurrentHealth == shieldAdjust)
                return;

            CurrentHealth = shieldAdjust;

            HealthChange?.Invoke(CurrentHealth);
        }

        private void AnimateShield(bool shield)
        {
            m_shieldAnimator.SetBool("Shielding", shield);
        }
    }
}
