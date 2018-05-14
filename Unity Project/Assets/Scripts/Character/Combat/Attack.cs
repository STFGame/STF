using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Combat
{
    /// <summary>
    /// Class for the different attacks that the character can perform. 
    /// </summary>
    [Serializable]
    public class Attack
    {
        #region Attack Variables
        [SerializeField] private AttackAction[] attackActions = null;

        private int previousAttackID = 0;
        #endregion

        #region Updates
        public void CheckAttack(int attackID)
        {
            if (attackID == previousAttackID)
                return;

            previousAttackID = attackID;

            int index = 0;
            for (int i = 0; i < attackActions.Length; i++)
            {
                if(attackActions[i].AttackID == attackID)
                {
                    index = i;
                    break;
                }
            }
        }

        private void StartAttack()
        {

        }
        #endregion
    }
}