using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;

namespace Actor
{
    [Serializable]
    public sealed class Jump
    {
        [Header("Jump Values")]
        [SerializeField] private float jumpHeight = 5f;
        [SerializeField] private int maxJumps = 2;
        [SerializeField] [Range(0f, 1f)] private float jumpDelay = 0.1f;

        public Timer jumpTimer = new Timer();

        private float apexHeight;

        public int JumpCounter { get; set; }
        public bool Apex { get; private set; }

        public Jump()
        {
            JumpCounter = 0;
        }

        public float VerticalVelocity(float gravity)
        {
            float verticalSpeed = Mathf.Sqrt(2f * gravity * jumpHeight);
            return verticalSpeed;
        }

        /// <summary> Compares the JumpCounter property to the maxJumps property (integers) </summary>
        /// <returns>Returns true if the JumpCounter property is less than the maxJumps variable</returns>
        public bool JumpsExceeded()
        {
            return JumpCounter < maxJumps;
        }

        public IEnumerator JumpStart(float velocity, Rigidbody rigidbody)
        {
            yield return new WaitForSeconds(jumpDelay);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, velocity, 0f);

            yield return new WaitForSeconds(0.2f);
            JumpCounter++;
        }

        private bool ApexReached(float currentHeight, bool onGround)
        {
            if (apexHeight <= currentHeight || onGround)
            {
                apexHeight = currentHeight;
                return Apex = false;
            }
            return Apex = true;
        }
    }
}
