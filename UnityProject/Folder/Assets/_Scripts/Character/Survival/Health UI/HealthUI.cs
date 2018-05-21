using Survival;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Life
{
    /// <summary>
    /// Class for displaying the health.
    /// </summary>
    [Serializable]
    public class HealthUI : MonoBehaviour
    {
        //Visuals for displaying the health bar.
        [SerializeField] protected GameObject m_HealthMain = null;
        [SerializeField] protected GameObject m_Healthbackground = null;

        //Smoothing for the health decrease
        [SerializeField] protected float m_SmoothDamp = 0.3f;

        protected float m_HealthVelocity = 0f;

        protected float m_MaxHealth;
        protected float m_TargetHealth;
        protected float m_PreviousHealth;

        protected Animator m_HealthAnimator = null;

        protected virtual void Awake()
        {
            m_HealthMain = Instantiate(m_HealthMain, transform, false);
            m_Healthbackground = Instantiate(m_Healthbackground, transform, false);

            m_HealthAnimator = m_HealthMain.GetComponent<Animator>();
        }

        //Initialises the health and subscribes it to a character's health
        public virtual void Initialise(IHealth healthRef)
        {
            m_MaxHealth = healthRef.MaxHealth;
            m_PreviousHealth = m_TargetHealth = (m_MaxHealth / m_MaxHealth);

            healthRef.HealthChange += (d) =>
            {
                m_TargetHealth = ((d) / m_MaxHealth);

                StopCoroutine(DecreaseHealth());
                StartCoroutine(DecreaseHealth());
            };
        }

        private IEnumerator DecreaseHealth()
        {
            while (m_PreviousHealth != m_TargetHealth)
            {
                m_PreviousHealth = Mathf.SmoothDamp(m_PreviousHealth, m_TargetHealth, ref m_HealthVelocity, m_SmoothDamp);

                AnimateHealth(m_HealthAnimator, m_PreviousHealth);
                yield return null;
            }
        }

        //Animates the decrease of health
        protected virtual void AnimateHealth(Animator healthAnimator, float targetHealth)
        {
            healthAnimator.SetFloat("Current Health", targetHealth);
        }
    }
}
