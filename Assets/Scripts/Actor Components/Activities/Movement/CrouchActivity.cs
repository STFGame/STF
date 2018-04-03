using System;
using UnityEngine;

/* CROUCH
 * Sean Ryan
 * March 26, 2018
 * 
 * This class derives from the Movement class and is responsible for the crouch
 * movement aspects of the entity
 */

namespace Actor.Activities
{
    [Serializable]
    public class CrouchActivity : Activity
    {
        [SerializeField] [Range(-1f, 0)] private float crouchThreshold = -0.65f;

        private bool isCrouching;

        public CrouchActivity() { }

        public override void OnUpdate()
        {
            base.OnUpdate();

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
