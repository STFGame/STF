using System;
using UnityEngine;

/* DASH
 * Sean Ryan
 * March 26, 2018
 * 
 * This class derives from the Movement class and is responsible for the dash aspects of the 
 * entity movement
 */

namespace Actor.Activities
{
    [Serializable]
    public class DashActivity : Activity
    {
        [SerializeField] private bool canDash = true;
        [SerializeField] private float dashThreshold = 0.1f;

        private bool isDashing = false;

        public DashActivity() { }

        public override void OnUpdate()
        {
            base.OnUpdate();

            isDashing = HasDashed(control.Lever.AbsoluteHorizontal);
        }

        private bool HasDashed(float horizontal)
        {
            if (!canDash)
                return false;
            return (control.Lever.Timer < dashThreshold && horizontal > 0.75f);
        }

        #region Properties
        public bool IsDashing
        {
            get { return isDashing; }
        }
        #endregion
    }
}
