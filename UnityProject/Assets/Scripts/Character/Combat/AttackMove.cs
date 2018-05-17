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
    public class AttackMove
    {
        #region Attack Variables
        [SerializeField] private AttackAction[] attackActions = null;
        #endregion

        #region Load
        public void Load()
        {
            for (int i = 0; i < attackActions.Length; i++)
                attackActions[i].Load();
        }
        #endregion

        #region Updates
        public AttackAction GetAttack(int attackID)
        {
            for (int i = 0; i < attackActions.Length; i++)
            {
                attackActions[i].UpdateAttack(attackID);
            }
            return null;
        }
        #endregion
    }
}