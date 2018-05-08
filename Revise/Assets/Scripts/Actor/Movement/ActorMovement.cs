using Actor.Animations;
using Actor.Bubbles;
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
        [Header("Movement")]
        [SerializeField] private Movement movement = new Movement();
        [SerializeField] private Jump jump = new Jump();
        [Header("Gravity")]
        [SerializeField] private Gravity gravity = new Gravity();
        [Header("Animation")]
        [SerializeField] private MovementAnimation movementAnimation = new MovementAnimation();

        private float rotation = 0f;
        private float rotationDirection = -180f;

        private new Rigidbody rigidbody;

        #region Movement Properties
        public bool IsDashing { get; set; }
        public bool IsCrouching { get; set; }
        public bool IsFastFalling { get { return jump.isFastFalling; } set { jump.isFastFalling = value; } }
        public bool IsFalling { get { return jump.isFalling; } set { jump.isFalling = value; } }
        public bool IsTurning { get; set; }
        public bool IsRotating { get; set; }
        public bool OnGround { get; set; }

        public Vector3 Velocity { get; private set; }
        public Vector3 Direction { get; private set; }
        public int CurrentDirection { get; private set; }
        public int JumpCount { get { return jump.jumpCounter; } private set { jump.jumpCounter = value; } }
        #endregion

        #region Initialization
        // Use this for initialization
        private void Awake()
        {
            #region Rigidbody
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rigidbody.useGravity = false;
            #endregion

            movementAnimation.Init(this);

            if (transform.parent.localEulerAngles.y > 90f)
                rotationDirection *= -1f;

            Direction = Vector3.right;
            Direction *= transform.forward.x;
        }

        private void Start()
        {
            GetComponentInChildren<BubbleManager>().GetGroundBubble(BodyArea.Base).GroundEvent += GroundUpdate;
            Debug.Log(GetComponentInChildren<Bubble>());
        }

        private void OnDisable()
        {
            GetComponentInChildren<BubbleManager>().GetGroundBubble(BodyArea.Base).GroundEvent -= GroundUpdate;
        }
        #endregion

        private void GroundUpdate(bool grounded) { OnGround = grounded; }

        private void FixedUpdate()
        {
            rigidbody.AddForce(gravity.ApplyGravity());
        }

        //Moves the actor in the direction that it is instructed by
        public void Move(Vector3 direction)
        {
            if (IsRotating)
                return;

            if (IsDashing)
                Velocity = movement.GetVelocity(direction, rigidbody.velocity, Movements.MovementState.Dash);
            else if (IsCrouching)
                Velocity = movement.GetVelocity(direction, rigidbody.velocity, Movements.MovementState.Crouch);
            else
                Velocity = movement.GetVelocity(direction, rigidbody.velocity, Movements.MovementState.Regular);

            rigidbody.AddForce(Velocity, forceMode);
        }

        //Rotates the actor to the instructed direction which can be with 0 or 180
        public void Rotate(Vector3 direction, bool stopTurn)
        {
            Quaternion startRotation = transform.localRotation;
            Quaternion endRotation = Quaternion.Euler(0f, rotation, 0f);

            if (!OnGround || stopTurn)
            {
                rigidbody.transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, 10f);

                IsRotating = false;
                return;
            }

            if (direction.x != 0f)
            {
                Vector3 angle = Vector3.right * direction.x;
                rotation = Vector3.Angle(Direction, angle);
                rotation *= -Direction.x;
            }

            rigidbody.transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, movement.GetRotationSpeed());

            IsRotating = (rigidbody.transform.localEulerAngles.y != rotation);
        }

        #region Jumps
        //A method for jumping. The jump bool is for starting the jump and the height is 
        //for how high the actor jumps.
        public void Jump(float direction, bool jumpCommand, bool holdCommand, ref Gravity gravity)
        {
            IsFalling = jump.IsCrestReached(rigidbody.transform.position.y, OnGround);

            float verticalSpeed = jump.VerticalVelocity(gravity.Counter);

            if (OnGround)
                JumpCount = 0;

            if (jumpCommand && (OnGround || !jump.IsJumpsExceeded))
                StartCoroutine(JumpDelay(verticalSpeed));

            float downwardForce = (IsFastFalling) ? jump.descentSpeed : 1f;

            this.gravity.ModifyGravity(-1 * downwardForce, jump.CanAscend(holdCommand));
        }

        //An IEnumerator to delay the jump to help it better align with the animation
        private IEnumerator JumpDelay(float verticalSpeed)
        {
            if (!OnGround)
            {
                rigidbody.velocity = new Vector3(Velocity.x, verticalSpeed, 0f);
                JumpCount++;
                yield break;
            }

            yield return new WaitForSeconds(jump.jumpDelay);
            rigidbody.velocity = new Vector3(Velocity.x, verticalSpeed, 0f);

            yield return new WaitUntil(() => OnGround == false);
            JumpCount++;
        }
        #endregion

        #region Movement Animations
        public void PlayMovementAnimations(Vector3 direction)
        {
            movementAnimation.PlayHorizontalAnim(direction);
            movementAnimation.PlayDashAnim(IsDashing);
            movementAnimation.PlayCrouchAnim(IsCrouching);
            movementAnimation.PlayTurnAnim(IsTurning);
        }

        public void PlayJumpAnimations(Vector3 direction, bool jumpCommand)
        {
            movementAnimation.PlayLandAnim(OnGround);
            movementAnimation.PlayFallAnim(IsFalling);
            movementAnimation.PlayJumpLeanAnim(direction);
            movementAnimation.PlayJumpAnim(jumpCommand);
        }
        #endregion
    }
}