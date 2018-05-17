using Character;
using Survival;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Boxes
{
    /// <summary>
    /// Class that is responsible for areas where the character can be hurt.
    /// </summary>
    public class Hurtbox : Box
    {
        private Health health = null;

        private int hurtIndex = 0;

        #region Load
        private void Awake()
        {
            foreach (Health health in GetComponentsInParent<Health>())
                if(health)
                {
                    this.health = health;
                    break;
                }

            if (boxArea == BoxArea.MidTorso)
                hurtIndex = 2;
            else
                hurtIndex = 1;

            active = true;
        }
        #endregion

        #region Triggers
        private void OnTriggerEnter(Collider other)
        {
            health.m_IsHurt = true;
            health.m_HurtIndex = hurtIndex;
        }

        private void OnTriggerExit(Collider other)
        {
            health.m_HurtIndex = 0;
        }
        #endregion

        #region Enable
        public void Enabled(bool enabled)
        {
            active = enabled;
            gameObject.layer = (enabled) ? (int)Layer.Hurtbox : (int)Layer.Dead;
        }
        #endregion
    }
}