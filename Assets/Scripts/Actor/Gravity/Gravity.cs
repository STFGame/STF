using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;

namespace Actor
{
    [Serializable]
    public sealed class Gravity
    {
        [SerializeField] private Velocity velocity = new Velocity();
        [SerializeField] [Range(0f, 1000f)] private float gravityForce = 100f;
        [SerializeField] [Range(0f, 20f)] private float decreaseRate = 5f;
        [SerializeField] [Range(0f, 20f)] private float increaseRate = 5f;

        private float gravityCounter;

        public float GravityForce { get { return gravityForce; } }

        public Gravity()
        {
            gravityCounter = gravityForce;
        }

        public void DecreaseGravity(bool value)
        {
            gravityCounter = Mathf.Clamp(gravityCounter, 0f, gravityForce);

            if (value)
                if (gravityForce > 0f)
                    gravityCounter -= decreaseRate;
            else if(!value && gravityCounter != gravityForce)
                gravityCounter += increaseRate;

            gravityCounter = Mathf.Clamp(gravityCounter, 0f, gravityForce);
        }

        public Vector3 ApplyGravity(Vector3 velocity)
        {
            return this.velocity.VelocityY(gravityCounter, -1f, velocity);
            //Vector3 targetForce = Vector3.down * gravityCounter;
            //Vector3 velocityChange = (targetForce - velocity);

            //velocityChange.x = 0f;
            //velocityChange.y = Mathf.Clamp(velocityChange.y, -gravityForce, gravityForce);
            //velocityChange.z = 0f;

            //return velocityChange;
        }
    }
}
