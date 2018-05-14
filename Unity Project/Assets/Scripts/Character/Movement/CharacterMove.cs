using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Class that is responsible for the movement of the character.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
    public class CharacterMove : MonoBehaviour, IGroundable
    {
        #region Movement Variables
        [Header("Movement Values")]

        [SerializeField] private ForceMode forceMode = ForceMode.VelocityChange;

        //How slow the character moves while in slowed situations
        [SerializeField] private MoveSpeed slow = new MoveSpeed(10f, 10f, 10f, 10f);

        //How fast the character moves while moving normally
        [SerializeField] private MoveSpeed normal = new MoveSpeed(20f, 20f, 20f, 20f);

        //How fast the character moves when going at full speed
        [SerializeField] private MoveSpeed fast = new MoveSpeed(30f, 30f, 30f, 30f);

        //How fast the character moves while in the air
        [SerializeField] private MoveSpeed aerial = new MoveSpeed(10f, 10f, 10f, 10f);

        //Sets the inital ground state of the character
        [Tooltip("This value is only used when the character is Awoken.")]
        [SerializeField] private bool grounded = false;

        private new Rigidbody rigidbody;
        private Animator animator;

        //Used to help rotate the character in the correct direction
        private Vector3 startDirection = Vector3.right;

        //Values that are updated based on which movement mode the character is in;
        private float speed = 0f;
        private float acceleration = 0f;
        private float rotation = 0f;
        private bool decelerate = false;

        //MoveModes for determining how fast the character moves and how fast the character
        //decelerates
        private MoveMode currentMode;
        private MoveMode previousMode;

        public bool Grounded { get; set; }

        public bool Crouching { get; private set; }
        public bool Normal { get; private set; }
        public bool Dashing { get; private set; }
        #endregion

        #region Load
        // Use this for initialization
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            Grounded = grounded;

            startDirection *= transform.forward.x;
        }
        #endregion

        #region Updates
        //Moves the character based on the direction, and the speed based on the MoveMode.
        public void Move(Vector2 direction, MoveMode moveMode)
        {
            if (direction.x != 0)
                previousMode = currentMode;

            currentMode = moveMode;

            Crouching = (moveMode == MoveMode.Slow);
            Normal = (moveMode == MoveMode.Normal);
            Dashing = (moveMode == MoveMode.Fast);

            if (direction.x != 0f)
            {
                SetPace(currentMode, false);
                decelerate = false;
            }
            else
                SetPace(previousMode, true);

            Vector3 velocity = GetVelocity(direction, speed, acceleration);

            Rotate(direction.x);

            rigidbody.AddForce(velocity, forceMode);
        }

        private void SetPace(MoveMode moveMode, bool decelerate)
        {
            if (!Grounded)
            {
                speed = aerial.Speed;
                acceleration = aerial.Acceleration;

                return;
            }

            if (moveMode == MoveMode.Slow)
            {
                if (decelerate)
                {
                    Decelerate(ref acceleration, slow);
                    return;
                }

                GetPace(ref speed, ref acceleration, slow);
                return;
            }
            else if (moveMode == MoveMode.Fast)
            {
                if (decelerate)
                {
                    Decelerate(ref acceleration, fast);
                    return;
                }
                GetPace(ref speed, ref acceleration, fast);
                return;
            }

            if (decelerate)
            {
                Decelerate(ref acceleration, normal);
                return;
            }

            GetPace(ref speed, ref acceleration, normal);
        }

        private void Decelerate(ref float acceleration, MoveSpeed moveSpeed)
        {
            if (!decelerate)
            {
                acceleration = 0f;
                decelerate = true;
            }

            if (acceleration < moveSpeed.Acceleration)
                acceleration += (moveSpeed.Deceleration * Time.deltaTime);

            acceleration = Mathf.Clamp(acceleration, 0f, moveSpeed.Acceleration);
        }

        private void GetPace(ref float speed, ref float acceleration, MoveSpeed moveSpeed)
        {
            speed = moveSpeed.Speed;
            acceleration = moveSpeed.Acceleration;
        }

        //Sets the rotation of the character based on the direction entered
        public void Rotate(float direction)
        {
            Quaternion startRotation = transform.localRotation;
            Quaternion endRotation = Quaternion.Euler(0f, rotation, 0f);

            if (!Grounded || Crouching)
            {
                transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, normal.RotationSpeed);

                return;
            }

            if (direction != 0f)
            {
                Vector3 angle = Vector3.right * direction;

                rotation = Vector3.Angle(startDirection, angle) * -startDirection.x;
            }

            float rotationSpeed = (Dashing) ? fast.RotationSpeed : normal.RotationSpeed;

            transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, rotationSpeed);
        }
        #endregion

        #region Helper Methods

        //Returns the velocity that the character is going at
        private Vector3 GetVelocity(Vector2 direction, float speed, float acceleration)
        {
            Vector3 targetVelocity = new Vector3(direction.x, 0f, 0f) * speed;

            Vector3 velocityIncrease = (targetVelocity - rigidbody.velocity) * (Time.deltaTime * acceleration);

            velocityIncrease.x = Mathf.Clamp(velocityIncrease.x, -speed, speed);
            velocityIncrease.y = 0f;
            velocityIncrease.z = 0f;

            return velocityIncrease;
        }

        public void Stop()
        {
            rigidbody.velocity = Vector3.zero;
        }
        #endregion

        #region Visual FX and Animation
        //Function that animates the all of the characters movements.
        public void AnimateMove(Vector2 direction)
        {
            if (!animator)
                return;

            float speed = Mathf.Abs(direction.x);
            float crouchSpeed = direction.x * transform.forward.x;
            float crouchFactor = direction.y;

            bool crouching = (currentMode == MoveMode.Slow);
            bool dashing = (currentMode == MoveMode.Fast);

            animator.SetFloat("Speed", speed, 0.02f, Time.deltaTime);
            animator.SetFloat("Crouch Speed", crouchSpeed, 0.02f, Time.deltaTime);
            animator.SetFloat("Crouch Factor", direction.y, 0.02f, Time.deltaTime);
            animator.SetBool("Crouching", crouching);
            animator.SetBool("Dashing", dashing);
        }
        #endregion
    }
}