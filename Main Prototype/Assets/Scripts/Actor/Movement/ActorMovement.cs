using Actor.Movements;
using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace Actor
{
    /// <summary>ActorMovement contains all functionality related to movement. These include <para/>
    /// running, jumping, and crouching. It also animates the actor.</summary>
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class ActorMovement : MonoBehaviour
    {
        [SerializeField] private ForceMode forceMode = ForceMode.VelocityChange;
        [SerializeField] private Movement movement = new Movement();
        [SerializeField] private Jump jump = new Jump();
        [SerializeField] private GroundCast groundCast = new GroundCast();
        [SerializeField] private Gravity gravity = new Gravity();

        private float rotation = 0f;

        private new CapsuleCollider collider;
        private new Rigidbody rigidbody;

        public bool onGround = true;

        public Vector3 Velocity { get; private set; }
        public int JumpCount { get { return jump.jumpCounter; } }
        public bool HaltMovement { get; set; }

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

        public void Perform(Vector3 direction, bool jumpCommand, bool holdCommand)
        {
            Move(direction);

            Jump(direction.y, jumpCommand, holdCommand);
        }

        //Moves the actor in the direction that it is instructed by
        private void Move(Vector3 direction)
        {
            Velocity = movement.HorizontalVelocity(direction, rigidbody.velocity);
            rigidbody.AddForce(Velocity, forceMode);

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
        private void Jump(float direction, bool jumpCommand, bool holdCommand)
        {
            jump.DescendFast(direction);

            groundCast.GroundCheck(transform.position.y, ref onGround);

            float verticalSpeed = jump.VerticalVelocity(gravity.counter);

            if (onGround)
                jump.jumpCounter = 0;

            if (jumpCommand && (onGround || !jump.IsJumpsExceeded))
                StartCoroutine(JumpDelay(verticalSpeed));

            float downwardForce = (jump.IsFastDescending) ? jump.descentSpeed : 1f;

            rigidbody.AddForce(gravity.Gravitational(-1 * downwardForce, jump.CanAscend(holdCommand)) * downwardForce);
        }

        //An IEnumerator to delay the jump to help it better align with the animation
        private IEnumerator JumpDelay(float verticalSpeed)
        {
            if (!onGround)
            {
                rigidbody.velocity = new Vector3(Velocity.x, verticalSpeed, 0f);
                jump.jumpCounter++;
                yield break;
            }

            yield return new WaitForSeconds(jump.jumpDelay);
            rigidbody.velocity = new Vector3(Velocity.x, verticalSpeed, 0f);

            Func<bool> Grounded = OnGround;
            yield return new WaitUntil(Grounded);
            jump.jumpCounter++;
        }

        private bool OnGround() { return !onGround; }

        private void OnDrawGizmos()
        {
            groundCast.DrawGizmo();
        }
    }
}