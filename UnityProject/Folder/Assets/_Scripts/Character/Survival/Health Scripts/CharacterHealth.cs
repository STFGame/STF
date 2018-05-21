using Survival;
using UnityEngine;
using Actions;
using Alerts;
using STF.Timers;
using System;

namespace Character
{
    /// <summary>
    /// Class that contains the character's health data.
    /// </summary>
    public class CharacterHealth : Health
    {
        [Header("Freeze Value")]
        [SerializeField] private Act m_Freeze = null;
        [SerializeField] private Act m_Immune = null;
        [SerializeField] private Act m_Death = null;

        private Shield m_Shield;
        private Rigidbody m_Rigidbody;
        private Animator m_HurtAnimator;

        public override event Action<float> HealthChange;

        //Properties that can be used by other classes
        public bool Immune { get; private set; }
        public bool Dead { get; set; }

        private void Awake()
        {
            m_Shield = GetComponent<Shield>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_HurtAnimator = GetComponent<Animator>();

            m_CurrentHealth = m_MaxHealth;
        }
        //Executes the health class.
        public override void Execute()
        {
            PlayHealthAnimations();

            if (Dead)
                Chronograph.Delay(this, 2f, Death);
        }

        //Method for when the character takes damage.
        public override void TakeDamage(float damage)
        {
            if (m_Shield)
                if (m_Shield.Shielding)
                    return;

            m_CurrentHealth -= damage;

            if (m_CurrentHealth <= 0)
            {
                Dead = true;
                m_CurrentHealth = 0f;
            }

            HealthChange?.Invoke(m_CurrentHealth);
        }

        //Restores health to the character
        public override void RestoreHealth(float restoreAmount)
        {
            m_CurrentHealth = restoreAmount;

            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0f, m_MaxHealth);

            Dead = false;
        }

        //Method that is called when the character's health is below or equal to 0.
        private void Death()
        {
            bool death = true;
            m_Death.Perform(ref death);

            if (!death)
            {
                Alert.Send<IAlert>(gameObject, (x, y) => x.Inform(AlertMessage.Dead));
                gameObject.layer = (int)Layer.Dead;
            }
        }

        private void PlayHealthAnimations()
        {
            m_HurtAnimator.SetInteger("Hit", m_HurtIndex);
            m_HurtAnimator.SetBool("Dead", Dead);

            Dead = false;
        }
    }
}