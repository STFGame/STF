using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Life
{
    /// <summary>
    /// Class for health that has more effects. 
    /// </summary>
    [Serializable]
    public class HealthUIAdvance : HealthUI
    {
        //Secondary health is the damage health that is slower to decrease.
        [SerializeField] private GameObject m_HealthSecondary = null;

        private Animator m_SecondaryAnimator = null;

        //Starts on awake.
        protected override void Awake()
        {
            base.Awake();

            m_HealthSecondary = Instantiate(m_HealthSecondary, transform, false);
            m_SecondaryAnimator = m_HealthSecondary.GetComponent<Animator>();
        }
    }
}
