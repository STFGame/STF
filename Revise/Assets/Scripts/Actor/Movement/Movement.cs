using System;
using UnityEngine;

namespace Actor.Movements
{
    [Serializable]
    public sealed class Movement
    {
        [Header("Speeds")]
        [SerializeField] private Speed regular = new Speed(30f, 10f, 5f, 5f, 1f);
        [SerializeField] private Speed crouch = new Speed(20f, 10f, 5f, 5f, 1f);
        [SerializeField] private Speed dash = new Speed(45f, 20f, 5f, 5f, 1f);

        private float acceleration;

        public bool isDashing;
        public bool isCrouching;

        private MovementState currentState;
        private MovementState previousState;

        public Movement()
        {
            isDashing = false;
            isCrouching = false;
        }

        public Vector3 HorizontalVelocity(Vector3 direction, Vector3 currentVelocity)
        {
            float speed = GetSpeed(direction);

            Vector3 targetVelocity = new Vector3(direction.x, 0f, 0f) * speed;
            Vector3 velocityChange = (targetVelocity - currentVelocity) * (Time.deltaTime * Acceleration(direction.x));

            velocityChange.x = Mathf.Clamp(velocityChange.x, -speed, speed);
            velocityChange.y = 0f;
            velocityChange.z = 0f;

            return velocityChange;
        }

        private float Acceleration(float direction)
        {
            if (direction == 0f)
            {
                currentState = MovementState.Regular;

                BuildState(previousState, false);
                return acceleration;
            }

            previousState = currentState;

            if (isDashing)
                currentState = MovementState.Dash;
            else if(isCrouching)
                currentState = MovementState.Crouch;
            else
                currentState = MovementState.Regular;

            BuildState(currentState, true);

            return acceleration;
        }

        private void BuildState(MovementState state, bool shouldBuild)
        {
            switch(state)
            {
                case MovementState.Dash:
                    BuildAcceleration(dash, ref acceleration, shouldBuild);
                    break;
                case MovementState.Crouch:
                    BuildAcceleration(crouch, ref acceleration, shouldBuild);
                    break;
                case MovementState.Regular:
                    BuildAcceleration(regular, ref acceleration, shouldBuild);
                    break;
                default:
                    break;
            }
        }

        private void BuildAcceleration(Speed move, ref float acceleration, bool increase)
        {
            if (increase)
            {
                if (acceleration < move.maxAcceleration)
                    acceleration += (Time.deltaTime * move.acceleration);
            }
            else if (!increase)
            {
                if (acceleration > 0f)
                    acceleration -= (Time.deltaTime * move.deceleration);
            }

            acceleration = Mathf.Clamp(acceleration, 0f, move.maxAcceleration);
        }

        private float GetSpeed(Vector3 direction)
        {
            float actorSpeed = (isDashing) ? dash.speed : (isCrouching) ? crouch.speed : regular.speed;
            return actorSpeed;
        }

        public float RotationSpeed
        {
            get
            {
                if (isDashing)
                    return dash.rotationSpeed;
                else if (isCrouching)
                    return crouch.rotationSpeed;
                else
                    return regular.rotationSpeed;
            }
        }
    }
}
