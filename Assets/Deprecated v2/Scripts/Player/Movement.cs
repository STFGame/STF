using InControl;
using Player.Collisioned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Player.Locomotion
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour, IUpdate
    {
        #region Locomotion Settings
        [Header("Movement Settings")]
        [SerializeField]
        float speed;
        [SerializeField] float dashSpeed;

        [Header("Jump Settings")]
        [SerializeField]
        float jumpHeight;
        [SerializeField] int jumpNumber;
        int jumpMax;
        #endregion

        bool isGrounded = true;

        Rigidbody rigidBody;
        Collisi collisions;

        private void Start()
        {
            #region RigidBody
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidBody.drag = 1f;
            rigidBody.useGravity = false;
            #endregion



            //collisions = GetComponent<Collisi>();
            //collisions.OnGrounded += Grounded;

            GetComponent<Collisi>().OnGrounded += Grounded;
            jumpMax = jumpNumber;
        }

        public void OnUpdate()
        {
            rigidBody.useGravity = !isGrounded;
        }

        public void OnFixedUpdate(InputDevice device)
        {
            Vector3 move = new Vector3(device.Direction.X, 0f, 0f);
            rigidBody.AddForce((move * speed) * Time.deltaTime, ForceMode.Impulse);

            if (isGrounded || jumpNumber > 0)
            {
                if (isGrounded)
                    jumpNumber = jumpMax;
                if (device.Action1.WasPressed)
                {
                    rigidBody.velocity = Vector3.up * jumpHeight;
                    jumpNumber--;
                }
            }
        }

        void Grounded(bool val)
        {
            isGrounded = val;
        }
    }
}