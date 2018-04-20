using Actor.Animations;
using Actor.Bubbles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    /// <summary>ActorMovement contains all functionality related to movement. These include <para/>
    /// running, jumping, and crouching. It also animates the actor.</summary>
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class ActorMovement : MonoBehaviour
    {
        [Header("Physical Movement")]
        [SerializeField] private ForceMode forceMode = ForceMode.VelocityChange;
        [SerializeField] private Movement movement = new Movement();
        [SerializeField] private Jump jump = new Jump();
        [SerializeField] private GroundBubble groundBubble = new GroundBubble();

        [Header("Gravity Settings")]
        [SerializeField] private Gravity gravity = new Gravity();

        private bool onGround = true;
        private float rotation = 0f;

        private new CapsuleCollider collider;
        private new Rigidbody rigidbody;

        public Jump Jumps { get { return jump; } }
        public bool HaltMovement { get; set; }
        public bool OnGround { get { return onGround; } }

        // Use this for initialization
        private void Awake()
        {
            SetRigidbody();

            if (GetComponentInChildren<SkinnedMeshRenderer>() != null)
                SetCapsuleCollider();
        }


        #region Component Settings
        //Sets the rigidbody to the correct settings.
        private void SetRigidbody()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rigidbody.useGravity = false;
        }

        //Sets the height of the capsule collider based on the skinned mesh height
        private void SetCapsuleCollider()
        {
            Vector3 center = GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
            float height = GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.y;

            collider = GetComponent<CapsuleCollider>();
            collider.height = height * 1.85f;
            collider.center = center;
        }
        #endregion

        /// <summary>The access method of ActorMovement that makes the Actor perform different movements.</summary>
        /// <param name="direction">Direction is the direction that the character wishes to move in</param>
        /// <param name="jump">A boolean value that determines whether the character is jumping or not</param>
        /// <param name="height">A boolean that is controls how high the character will jump</param>
        public void Perform(Vector3 direction, bool jump, bool height)
        {
            //Checks if the player is grounded or not
            groundBubble.Intersection(transform.position.y, ref onGround);

            Gravity();

            if (HaltMovement)
                return;

            Move(direction);

            Jump(jump, height);
        }

        //Moves the actor in the direction that it is instructed by
        private void Move(Vector3 direction)
        {
            Vector3 velocity = movement.HorizontalVelocity(direction, rigidbody.velocity);
            rigidbody.AddForce(velocity, forceMode);

            Rotate(direction.x);
        }

        //Rotates the actor to the instructed direction which can be with 0 or 180
        private void Rotate(float direction)
        {
            if (!onGround)
                return;

            rotation = (direction > 0.1f) ? 0f : (direction < -0.1f) ? -180f : rotation;

            Quaternion startRotation = transform.localRotation;
            Quaternion endRotation = Quaternion.Euler(0f, rotation, 0f);

            transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, movement.RotationSpeed);
        }

        //A method for jumping. The jump bool is for starting the jump and the height is 
        //for how high the actor jumps.
        private void Jump(bool jump, bool height)
        {
            float verticalSpeed = this.jump.VerticalVelocity(gravity.GravityForce);
            gravity.DecreaseGravity(this.jump.jumpTimer.HoldTimer(height));

            if (onGround)
                this.jump.JumpCounter = 0;

            if (jump && (onGround || this.jump.JumpsExceeded()))
            {
                StopAllCoroutines();
                StartCoroutine(this.jump.JumpStart(verticalSpeed, rigidbody));
            }
        }

        //Applies gravity to the actor
        private void Gravity()
        {
            Vector3 gravity = this.gravity.ApplyGravity(rigidbody.velocity);
            rigidbody.AddForce(gravity);
        }

        //Draws different collider and other gizmos.
        private void OnDrawGizmos()
        {
            groundBubble.DrawGizmo();
        }
    }
}