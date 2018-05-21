using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actions
{
    /// <summary>
    /// Class for making character perform different acts.
    /// </summary>
    [Serializable]
    public class Act
    {
        #region Act Variables
        //How long the act lasts
        [SerializeField] [Range(0f, 10f)] private float timeLength = 0.2f;

        //When the act starts
        [SerializeField] [Range(0f, 10f)] private float timeDelay = 0.1f;

        private bool enabled = false;
        private float timer = 0f;
        private float previousSpeed = 0f;

        public float TimeLength { get { return timeLength; } }
        #endregion

        #region Methods
        //Performs the action
        public void Perform(ref bool action)
        {
            if (!action)
            {
                timer = 0f;
                return;
            }

            timer += Time.deltaTime;

            enabled = (timer > timeDelay && timer < timeLength);

            if (timer > timeLength)
            {
                timer = 0f;
                action = false;
            }
        }

        public void Perform(ref float speed, ref bool action)
        {
            if (speed != 0)
                previousSpeed = speed;

            speed = 0f;

            Perform(ref action);

            if (!enabled)
                speed = previousSpeed;
        }

        public void Reset()
        {
            enabled = false;
            timer = 0f;
        }
        #endregion
    }
}
