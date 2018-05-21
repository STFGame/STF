using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actions
{
    /// <summary>
    /// Class for how long a boolean is true
    /// </summary>
    [Serializable]
    public class ActionHold
    {
        #region ActionHold Variables
        //The point that a button must be released
        [SerializeField] [Range(0f, 1f)] private float holdLimit = 0.65f;

        private float actionTimer = 0f;
        #endregion

        #region Methods
        public bool ActionSuccess(bool hold)
        {
            if (!hold)
            {
                actionTimer = 0f;
                return false;
            }

            if (actionTimer <= holdLimit)
                actionTimer += Time.deltaTime;

            return (actionTimer < holdLimit);
        }
        #endregion
    }
}
