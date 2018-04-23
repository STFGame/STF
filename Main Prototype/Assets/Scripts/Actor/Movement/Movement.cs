using System;
using UnityEngine;

namespace Actor.Movements
{
    [Serializable]
    public sealed class Movement
    {
        [Header("Speeds")]
        [SerializeField] private Speed regular = new Speed(30f, 10f);
        [SerializeField] private Speed crouch = new Speed(20f, 10f);
        [SerializeField] private Speed dash = new Speed(45f, 20f);
        [SerializeField] [Range(1f, 20f)] private float acceleration = 5f;

        [Header("Controls")]
        [SerializeField] MotionControl dashControl = new MotionControl(0.1f, 0.75f, 0.1f);
        [SerializeField] [Range(-1f, 0f)] private float crouchRange = -0.6f;

        private float dashTimer = 0f;

        public bool IsDashing { get; private set; }
        public bool IsCrouching { get; private set; }
        public bool IsTurning { get; private set; }

        public Movement()
        {
            IsDashing = false;
            IsCrouching = false;
            IsTurning = false;
        }

        public Vector3 HorizontalVelocity(Vector3 direction, Vector3 currentVelocity)
        {
            float speed = GetSpeed(direction);

            Vector3 targetVelocity = new Vector3(direction.x, 0f, 0f) * speed;
            Vector3 velocityChange = (targetVelocity - currentVelocity) * (Time.deltaTime * acceleration);

            velocityChange.x = Mathf.Clamp(velocityChange.x, -speed, speed);
            velocityChange.y = 0f;
            velocityChange.z = 0f;

            return velocityChange;
        }

        private float GetSpeed(Vector3 direction)
        {
            Dashing(direction.x);
            Crouching(direction.y);

            float actorSpeed = (IsDashing) ? dash.speed : (IsCrouching) ? crouch.speed : regular.speed;
            return actorSpeed;
        }

        private void Dashing(float direction)
        {
            float absDirection = Mathf.Abs(direction);

            if (absDirection == 0f)
            {
                dashTimer = 0f;
                return;
            }

            if (absDirection > dashControl.minimum && absDirection < dashControl.maximum)
                dashTimer += Time.deltaTime;

            IsDashing = (absDirection > dashControl.maximum && dashTimer < dashControl.sensitivity);
        }

        private void Crouching(float direction)
        {
            IsCrouching = (direction < crouchRange);
        }

        public bool Turning(float direction, Transform forward)
        {
            return IsTurning = (forward.forward.x * direction < 0f);
        }

        public float RotationSpeed
        {
            get
            {
                if (IsDashing)
                    return dash.rotationSpeed;
                else if (IsCrouching)
                    return crouch.rotationSpeed;
                else
                    return regular.rotationSpeed;
            }
        }
    }
}
