using System;
using UnityEngine;

/* DASH
 * Sean Ryan
 * March 26, 2018
 * 
 * This class derives from the Movement class and is responsible for the dash aspects of the 
 * entity movement
 */

namespace Entity.Motions
{
    [Serializable]
    public class Dash : Movement
    {
        [SerializeField] private bool canDash = true;
        [SerializeField] private float dashThreshold = 0.1f;

        private bool isDashing = false;

        public Dash()
        {
            vivacity.ParameterName = "IsDashing";
        }

        public override void OnUpdate()
        {
            isDashing = HasDashed(control.Lever.Horizontal);

            vivacity.SetBool(isDashing);
        }

        //Method that checks whether the character is dashing by using a timer to find out
        //Whether the player has moved the stick fast enough to dash
        private bool HasDashed(float horizontal)
        {
            if (!canDash)
                return false;

            float absoluteHorizontal = Math.Abs(horizontal);
            return (control.Lever.Timer < dashThreshold && absoluteHorizontal > 0.75f);
        }

        #region Properties
        public bool IsDashing
        {
            get { return isDashing; }
        }
        #endregion
    }
}
