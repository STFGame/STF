using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Survival
{
    /// <summary>
    /// Class for displaying the health.
    /// </summary>
    [Serializable]
    public class HealthVisual
    {
        //Main health visual. Typically the green bar
        [SerializeField] protected GameObject m_HealthMain = null;

        [SerializeField] protected float m_SmoothDamp = 0.3f;

        protected float m_HealthVelocity = 0f;

        protected Animator m_HealthAnimator = null;

        //Instantiates the healthbar with it's parent
        public virtual void Load(Transform parent)
        {
            if (parent == null)
                return;

            m_HealthMain = GameObject.Instantiate(m_HealthMain, parent);
            m_HealthAnimator = m_HealthMain.GetComponent<Animator>();
        }

        #region Decrease Health
        //Decreases the health of the visual display.
        public virtual void DecreaseHealth(float currentHealth, ref float previousHealth, float maxHealth)
        {
            float targetHealth = (currentHealth / maxHealth);

            previousHealth = Mathf.SmoothDamp(previousHealth, targetHealth, ref m_HealthVelocity, m_SmoothDamp);

            AnimateDecrease(m_HealthAnimator, currentHealth);
        }

        //Animates the decrease of health
        protected virtual void AnimateDecrease(Animator healthAnimator, float currentHealth)
        {
            healthAnimator.SetFloat("Current Health", currentHealth);
        }
        #endregion
    }
}
