using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Class responsible for the gravity of the character.
    /// </summary>
    public class Gravity : MonoBehaviour
    {
        #region Gravity Variables
        [Header("Gravity Settings")]
        //The max force of gravity that is applied
        [SerializeField] private float maxGravity = 100f;

        //How fast gravity increases back to max
        [SerializeField] [Range(0f, 10f)] private float increaseRate = 1f;

        //How fast gravity decreases to 0
        [SerializeField] [Range(0f, 10f)] private float decreaseRate = 1f;

        //Gravity modifier that is responsible for the current gravity
        public float Gravitation { get; private set; }

        //The velocity of gravity
        private Vector3 gravityVelocity = Vector3.down;

        private new Rigidbody rigidbody;
        #endregion

        #region Load
        // Use this for initialization
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();

            Gravitation = maxGravity;
        }
        #endregion

        #region Updates
        // Update is called once per frame
        //private void FixedUpdate()
        //{
            //rigidbody.AddForce(gravityVelocity);
        //}

        //Call in FixedUpdate because of physics
        public void Execute()
        {
            rigidbody.AddForce(gravityVelocity);
        }
        #endregion

        #region Helpers
        //Function that decreases or increases gravity based on the bool "decrease"
        public void DecreaseGravity(bool decrease)
        {
            if (decrease)
            {
                if (Gravitation > 0f)
                    Gravitation -= decreaseRate / Time.deltaTime;
            }
            else
            {
                if (Gravitation < maxGravity)
                    Gravitation += increaseRate / Time.deltaTime;
            }

            Gravitation = Mathf.Clamp(Gravitation, 0f, maxGravity);

            ModifyVelocity(Gravitation);
        }

        //Modifies the gravity and sets the velocity that is applied
        private void ModifyVelocity(float gravitation)
        {
            Vector3 targetVelocity = Vector3.down * gravitation;
            gravityVelocity = (targetVelocity - rigidbody.velocity);

            gravityVelocity.x = rigidbody.velocity.x;
            gravityVelocity.y = Mathf.Clamp(gravityVelocity.y, -maxGravity, maxGravity);
            gravityVelocity.z = 0f;
        }
        #endregion
    }
}