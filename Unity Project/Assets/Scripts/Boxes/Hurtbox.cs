using Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Boxes
{
    /// <summary>
    /// Class that is responsible for areas where the character can be hit.
    /// </summary>
    public class Hurtbox : Box
    {
        private CharacterHealth characterHealth = null;

        private int hurtIndex = 0;

        #region Load
        private void Awake()
        {
            foreach (CharacterHealth health in GetComponentsInParent<CharacterHealth>())
                if(health)
                {
                    characterHealth = health;
                    break;
                }

            if (boxArea == BoxArea.MidTorso)
                hurtIndex = 2;
            else
                hurtIndex = 1;
        }
        #endregion

        #region Triggers
        private void OnTriggerEnter(Collider other)
        {
            characterHealth.isHurt = true;
            characterHealth.hurtID = hurtIndex;
        }

        private void OnTriggerExit(Collider other)
        {
            //characterHealth.isHurt = false;
            characterHealth.hurtID = 0;
        }
        #endregion

        #region Enable
        public void Enabled(bool enabled)
        {
            gameObject.layer = (enabled) ? (int)Layer.Hurtbox : (int)Layer.Dead;
        }
        #endregion
    }
}