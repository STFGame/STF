using Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Boxes
{
    /// <summary>
    /// A box for checking if the character is on the ground
    /// </summary>
    public class Groundbox : Box
    {
        #region Groundbox Variables
        //A list that holds all of the characters that wish to know if they're on the ground
        List<IGroundable> groundables = null;
        #endregion

        #region Load
        private void Awake()
        {
            groundables = new List<IGroundable>();

            foreach (IGroundable groundable in GetComponentsInParent<IGroundable>())
            {
                if(groundable != null)
                    groundables.Add(groundable);
            }
        }
        #endregion

        #region Triggers
        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            OnGround(true);
        }

        private void OnTriggerExit(UnityEngine.Collider other)
        {
            OnGround(false);
        }

        //Method that informs Groundables if they're on the ground or not.
        private void OnGround(bool onGround)
        {
            for (int i = 0; i < groundables.Count; i++)
                groundables[i].IsGrounded = onGround;
        }
        #endregion
    }
}
