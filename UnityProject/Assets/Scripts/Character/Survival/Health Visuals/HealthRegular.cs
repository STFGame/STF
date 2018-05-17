using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Survival
{
    /// <summary>
    /// A Scriptable Object that contains health "skin" objects. 
    /// </summary>
    [Serializable]
    public class HealthRegular : HealthVisual
    {
        #region HealthRegular Variables
        //Secondary health is the damage health that is slower to decrease.
        [SerializeField] private GameObject m_HealthSecondary = null;

        //The background of the health
        [SerializeField] private GameObject m_HealthBackground = null;

        private Animator m_SecondaryAnimator = null;
        #endregion

        #region Load
        //Instantiates the health visuals and sets the GameObjects equal to them.
        //Also sets the children in the correct order.
        public override void Load(Transform parent)
        {
            if (parent == null)
                parent.position = Vector3.zero;

            base.Load(parent);

            m_HealthSecondary = UnityEngine.Object.Instantiate(m_HealthSecondary, parent, false) as GameObject;
            m_HealthBackground = UnityEngine.Object.Instantiate(m_HealthBackground, parent, false) as GameObject;

            m_HealthMain.transform.SetSiblingIndex(0);
            m_HealthSecondary.transform.SetSiblingIndex(1);
            m_HealthBackground.transform.SetSiblingIndex(2);

            m_SecondaryAnimator = m_HealthSecondary.GetComponent<Animator>();
        }
        #endregion

        #region Decrease Health
        //Decreases the health by the specified amount
        public override void DecreaseHealth(float currentHealth, ref float previousHealth, float maxHealth)
        {
            float targetHealth = (currentHealth / maxHealth);

            AnimateDecrease(m_HealthAnimator, targetHealth);

            previousHealth = Mathf.SmoothDamp(previousHealth, targetHealth, ref m_HealthVelocity, m_SmoothDamp);

            AnimateDecrease(m_SecondaryAnimator, previousHealth);
        }

        //Animates the health decrease
        protected override void AnimateDecrease(Animator healthAnimator, float currentHealth)
        {
            healthAnimator.SetFloat("Current Health", currentHealth);
        }
        #endregion
    }
}
