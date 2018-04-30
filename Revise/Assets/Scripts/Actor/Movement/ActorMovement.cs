using Actor.Animations;
using Actor.Movements;
using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace Actor
{
    public enum MovementState { None, Regular, Crouch, Dash, Turn, Jump, Descend }

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
        [SerializeField] private MoveAnim movementAnimation = new MoveAnim();

        private float rotation = 0f;
        private float rotationDirection = -180f;
        private bool onGround = true;

        private new Rigidbody rigidbody;

        #region Movement Properties
        public MovementState MovementState { get; private set; }
        public bool IsDashing { get { return movement.isDashing; } private set { movement.isDashing = value; } }
        public bool IsCrouching { get { return movement.isCrouching; } private set { movement.isCrouching = value; } }
        public bool IsTurning { get { return movement.isTurning; } private set { movement.isTurning = value; } }
        public bool IsFastFalling { get { return jump.isFastFalling; } private set { jump.isFastFalling = value; } }

        public Vector3 Velocity { get; private set; }
        public int JumpCount { get { return jump.jumpCounter; } private set { jump.jumpCounter = value; } }
        public int Direction { get { return (rotation == 0) ? 1 : -1; } }
        #endregion

        // Use this for initialization
        private void Awake()
        {
            SetRigidbody();
            movementAnimation.Init(this);

            if (transform.parent.localEulerAngles.y > 90f)
                rotationDirection *= -1f;
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
        #endregion

        //Updates which motion the character is doing
        public void UpdateMovementState(MovementState updateState)
        {
            MovementState = updateState;

            IsCrouching = (MovementState == MovementState.Crouch);
            IsDashing = (MovementState == MovementState.Dash);
            IsTurning = (MovementState == MovementState.Turn);
            IsFastFalling = (MovementState == MovementState.Descend);
        }

        //Moves the actor in the direction that it is instructed by
        public void Move(Vector3 direction)
        {
            if (!IsTurning)
            {
                Velocity = movement.HorizontalVelocity(direction, rigidbody.velocity);
                rigidbody.AddForce(Velocity, forceMode);
            }
        }

        //Rotates the actor to the instructed direction which can be with 0 or 180
        public void Rotate(float direction, bool stopTurn)
        {
            Quaternion startRotation = transform.localRotation;
            Quaternion endRotation = Quaternion.Euler(0f, rotation, 0f);

            if (!onGround || stopTurn)
            {
                endRotation = Quaternion.Euler(0, rotation, 0f);

                rigidbody.transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, 10f);

                IsTurning = false;
                return;
            }

            if (rotationDirection > 90f)
                rotation = (direction > 0.1f) ? rotationDirection : (direction < -0.1f) ? 0f : rotation;
            else
                rotation = (direction > 0.1f) ? 0f : (direction < -0.1f) ? rotationDirection : rotation;

            rigidbody.transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, movement.RotationSpeed);

            IsTurning = (transform.localEulerAngles.y != (-1) * rotation);
        }

        //A method for jumping. The jump bool is for starting the jump and the height is 
        //for how high the actor jumps.
        public void Jump(float direction, bool jumpCommand, bool holdCommand)
        {
            float verticalSpeed = jump.VerticalVelocity(gravity.counter);

            if (onGround)
                JumpCount = 0;

            if (jumpCommand && (onGround || !jump.IsJumpsExceeded))
                StartCoroutine(JumpDelay(verticalSpeed));

            groundCast.GroundCheck(transform.position.y, ref onGround);

            float downwardForce = (IsFastFalling) ? jump.descentSpeed : 1f;

            rigidbody.AddForce(gravity.Gravitational(-1 * downwardForce, jump.CanAscend(holdCommand)) * downwardForce);
        }

        //An IEnumerator to delay the jump to help it better align with the animation
        private IEnumerator JumpDelay(float verticalSpeed)
        {
            if (!onGround)
            {
                rigidbody.velocity = new Vector3(Velocity.x, verticalSpeed, 0f);
                JumpCount++;
                yield break;
            }

            yield return new WaitForSeconds(jump.jumpDelay);
            rigidbody.velocity = new Vector3(Velocity.x, verticalSpeed, 0f);

            Func<bool> Grounded = InverseOnGround;
            yield return new WaitUntil(Grounded);
            JumpCount++;
        }

        public void PlayMovementAnimations(Vector3 move)
        {
            movementAnimation.PlayHorizontalAnim(move);
            movementAnimation.PlayCrouchAnim(IsCrouching);
            movementAnimation.PlayDashAnim(IsDashing);
        }

        public void PlayJumpAnimations()
        {
            movementAnimation.PlayLandAnim(onGround);
        }

        public bool InverseOnGround() { return !onGround; }

        private void OnDrawGizmos()
        {
            groundCast.DrawGizmo();
        }
    }
}