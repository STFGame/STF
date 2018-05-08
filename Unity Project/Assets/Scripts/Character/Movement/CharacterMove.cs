using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
    public class CharacterMove : MonoBehaviour
    {
        [SerializeField] private MoveInfo moveInfo = null;

        private new Rigidbody rigidbody;
        private Animator animator;

        private Vector3 startDirection = Vector3.right;

        private float speed = 0f;
        private float acceleration = 0f;
        private float rotation = 0f;

        private bool grounded = true;
        private bool halt = true;

        private MoveMode currentMode;
        private MoveMode previousMode;

        // Use this for initialization
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            startDirection *= transform.forward.x;
        }

        #region Ground Subscription
        private void OnEnable()
        {
            GetComponentInChildren<GroundCheck>().GroundEvent += Grounded;
        }

        private void OnDisable()
        {
            GetComponentInChildren<GroundCheck>().GroundEvent -= Grounded;
        }

        private void Grounded(bool grounded)
        {
            this.grounded = grounded;
        }
        #endregion

        #region Move
        public void Move(Vector2 direction, MoveMode moveMode)
        {
            if (direction.x != 0f)
                previousMode = currentMode;

            currentMode = moveMode;

            speed = GetSpeed(ref direction, ref acceleration, currentMode);

            if (direction.x == 0f)
                Decelerate(ref acceleration, previousMode);
            else
                halt = false;

            if (!grounded)
            {
                acceleration = moveInfo.aerial.acceleration;
                speed = moveInfo.aerial.speed;
            }

            Vector3 velocity = GetVelocity(direction, speed, acceleration);

            rigidbody.AddForce(velocity, moveInfo.forceMode);
        }
        #endregion

        #region Speed
        private float GetSpeed(ref Vector2 direction, ref float acceleration, MoveMode moveMode)
        {
            if (direction.x == 0)
            {
                if (halt == false)
                    acceleration = 0f;
                halt = true;
                return speed;
            }

            if (moveMode == MoveMode.Slow)
            {
                acceleration = moveInfo.slow.acceleration;
                return moveInfo.slow.speed;
            }
            else if (moveMode == MoveMode.Fast)
            {
                acceleration = moveInfo.fast.acceleration;

                if (direction.x > 0)
                    direction.x = 1;
                else
                    direction.x = -1;

                return moveInfo.fast.speed;
            }

            acceleration = moveInfo.normal.acceleration;
            return moveInfo.normal.speed;
        }
        #endregion

        #region Velocity
        private Vector3 GetVelocity(Vector2 direction, float speed, float acceleration)
        {
            Vector3 targetVelocity = new Vector3(direction.x, 0f, 0f) * speed;

            Vector3 velocityIncrease = (targetVelocity - rigidbody.velocity) * (Time.deltaTime * acceleration);

            velocityIncrease.x = Mathf.Clamp(velocityIncrease.x, -speed, speed);
            velocityIncrease.y = 0f;
            velocityIncrease.z = 0f;

            return velocityIncrease;
        }

        private void Decelerate(ref float acceleration, MoveMode moveMode)
        {
            if (moveMode == MoveMode.Slow)
            {
                acceleration += (moveInfo.slow.deceleration * Time.deltaTime);
                acceleration = Mathf.Clamp(acceleration, 0f, moveInfo.slow.acceleration);
            }
            else if (moveMode == MoveMode.Normal)
            {
                acceleration += (moveInfo.normal.deceleration * Time.deltaTime);
                acceleration = Mathf.Clamp(acceleration, 0f, moveInfo.normal.acceleration);
            }
            else if (moveMode == MoveMode.Fast)
            {
                acceleration += Time.deltaTime * moveInfo.fast.deceleration;
                acceleration = Mathf.Clamp(acceleration, 0f, moveInfo.normal.acceleration);
            }
        }
        #endregion

        public void Rotate(float direction)
        {
            Quaternion startRotation = transform.localRotation;
            Quaternion endRotation = Quaternion.Euler(0f, rotation, 0f);

            if (!grounded || currentMode == MoveMode.Slow)
            {
                transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, moveInfo.normal.rotationSpeed);

                return;
            }

            if (direction != 0f)
            {
                Vector3 angle = Vector3.right * direction;

                rotation = Vector3.Angle(startDirection, angle) * -startDirection.x;
            }

            transform.localRotation = Quaternion.RotateTowards(startRotation, endRotation, moveInfo.normal.rotationSpeed);
        }

        public void Stop()
        {
            rigidbody.velocity = Vector3.zero;
        }

        public void AnimateMove(Vector2 direction)
        {
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
    }
}