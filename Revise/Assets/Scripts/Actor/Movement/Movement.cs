using System;
using UnityEngine;

namespace Actor.Movements
{
    public enum MovementState { None, Regular, Crouch, Dash }

    [Serializable]
    public sealed class Movement
    {
        [SerializeField] private Velocity regular = new Velocity();
        [SerializeField] private Velocity crouch = new Velocity();
        [SerializeField] private Velocity dash = new Velocity();

        [SerializeField] private float acceleration = 0f;

        private MovementState currentState;
        private MovementState previousState;

        private Vector3 velocity;

        public Vector3 GetVelocity(Vector3 direction, Vector3 currentVelocity, MovementState movementState)
        {
            float speed = GetSpeed();

            if (currentState != movementState)
            {
                previousState = currentState;
                currentState = movementState;
            }

            if (direction.x == 0f)
            {
                switch (previousState)
                {
                    case MovementState.Dash:
                        dash.ModifyAcceleration(direction.x, ref acceleration);
                        velocity = dash.GetVelocityX(direction, currentVelocity, acceleration);
                        break;
                    case MovementState.Crouch:
                        crouch.ModifyAcceleration(direction.x, ref acceleration);
                        velocity = crouch.GetVelocityX(direction, currentVelocity, acceleration);
                        break;
                    case MovementState.Regular:
                        regular.ModifyAcceleration(direction.x, ref acceleration);
                        velocity = regular.GetVelocityX(direction, currentVelocity, acceleration);
                        break;
                    default:
                        break;
                }

                return velocity;
            }

            switch(currentState)
            {
                case MovementState.Dash:
                    dash.ModifyAcceleration(direction.x, ref acceleration);
                    velocity = dash.GetVelocityX(direction, currentVelocity, acceleration);
                    break;
                case MovementState.Crouch:
                    crouch.ModifyAcceleration(direction.x, ref acceleration);
                    velocity = crouch.GetVelocityX(direction, currentVelocity, acceleration);
                    break;
                case MovementState.Regular:
                    regular.ModifyAcceleration(direction.x, ref acceleration);
                    velocity = regular.GetVelocityX(direction, currentVelocity, acceleration);
                    break;
                default:
                    break;
            }

            return velocity;
        }

        public float GetRotationSpeed()
        {
            float rotationSpeed = 0f;
            switch(currentState)
            {
                case MovementState.Dash:
                    rotationSpeed = dash.rotationSpeed;
                    break;
                case MovementState.Crouch:
                    rotationSpeed = crouch.rotationSpeed;
                    break;
                case MovementState.Regular:
                    rotationSpeed = regular.rotationSpeed;
                    break;
                default:
                    break;
            }

            return rotationSpeed;
        }

        private float GetSpeed()
        {
            float speed = 0f;
            switch (currentState)
            {
                case MovementState.Regular:
                    speed = regular.speed;
                    break;
                case MovementState.Crouch:
                    speed = crouch.speed;
                    break;
                case MovementState.Dash:
                    speed = dash.speed;
                    break;
                default:
                    break;
            }
            return regular.speed;
        }
    }
}
