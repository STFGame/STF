using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/* CROUCH
 * Sean Ryan
 * March 26, 2018
 * 
 * This class derives from the Movement class and is responsible for the crouch
 * movement aspects of the entity
 */

namespace Entity.Motions
{
    [Serializable]
    public class Crouch : Movement
    {
        [SerializeField] [Range(-1f, 0)] private float crouchThreshold = -0.65f;

        private bool isCrouching;

        public Crouch()
        {
            vivacity.ParameterName = "IsCrouching";
        }

        public override void OnUpdate()
        {
            isCrouching = (control.Lever.Vertical < crouchThreshold);
        }

        #region Properties
        public bool IsCrouching
        {
            get { return isCrouching; }
        }
        #endregion
    }
}
