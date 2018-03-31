using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utility.Gravity
{
    [Serializable]
    public sealed class Gravity
    {
        [SerializeField] private bool staticGravity;
        [SerializeField] private float gravityCap;
        [SerializeField] private float gravity;

        private Vector2 gravityForce;
        private float gravityCounter;

        public Gravity()
        {
            gravityForce = new Vector2();
            gravityCounter = 0f;
        }

        public void ApplyGravity(Rigidbody rigidbody, bool onGround)
        {
            if (onGround)
            {
                gravityCounter = 0f;
                return;
            }

            if (gravityCounter < gravityCap && !staticGravity)
                gravityCounter += Time.deltaTime * gravity;

            gravityForce = ((staticGravity) ? Vector2.down * gravity : Vector2.down * gravityCounter) * Time.deltaTime;

            rigidbody.AddForce(gravityForce);
        }

        public Vector2 GravityForce
        {
            get { return gravityForce; }
        }
    }
}
