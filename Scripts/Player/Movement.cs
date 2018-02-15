using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    class Movement
    {
        #region Variables
        [RangeAttribute(0f, 20f)] public float moveSpeed;
        public float jumpHeight;

        public Rigidbody rigidBody;

        public LayerMask layer;

        private bool IsGrounded;
        private int jumpCount = 0;
        private float hitDistance;

        protected ulong UpdateTick { get; private set; }
        #endregion

        public Movement()
        {
            moveSpeed = 10f;
            jumpHeight = 10f;
            rigidBody = null;

        }

        #region Methods
        public void Move(Vector3 move)
        {
            rigidBody.AddForce(move * moveSpeed, ForceMode.Impulse);
        }

        public void Jump()
        {
            if (IsGrounded)
                rigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        public void UpdateState(Transform position)
        {
            hitDistance = (IsGrounded) ? 0.35f : 0.15f;

            Vector3 other = new Vector3(0f, 0.85f, 0f);
            Vector3 origin = position.position - other;

            bool doubleJump = (jumpCount < 2) ? true : false;

            if(doubleJump)
                IsGrounded = Physics.Raycast(origin, -position.up, hitDistance, layer);

            jumpCount = (doubleJump) ? jumpCount + 1 : 0;

            Debug.Log(jumpCount);

            //if (Physics.Raycast(position.position - new Vector3(0f, 0.85f, 0), -position.up, hitDistance, layer))
            //{
                //IsGrounded = true;
            //}
            //else
            //{
                //if (jumpCount >= 2)
                //{
                    //IsGrounded = false;
                    //jumpCount = 0;
                //}
            //}
        }
        #endregion
    }
}