using System;
using UnityEngine;

namespace Actor
{
    [Serializable]
    public class Velocity
    {
        public enum ClampAxis { X, Y, Z, XY, XZ, YZ, XYZ }

        [SerializeField] private ClampAxis clamp;

        [Range(1f, 100f)] public float speed = 20f;
        [Range(1f, 100f)] public float rotationSpeed = 20f;

        [Range(0f, 50f)] public float accelerationRate = 10f;
        [Range(0f, 50f)] public float decelerationRate = 10f;

        [Range(1f, 50f)] public float maxAcceleration = 10f;
        [Range(0f, 50f)] public float minAcceleration = 0f;

        public Vector3 GetVelocityX(Vector3 direction, Vector3 currentVelocity, float acceleration)
        {
            Vector3 targetVelocity = new Vector3(direction.x, 0f, 0f) * speed;

            Vector3 velocityChange = (targetVelocity - currentVelocity);
            float accelerationModifer = (Time.deltaTime * acceleration);

            Vector3 velocity = velocityChange * accelerationModifer;

            #region Switch Statement
            switch (clamp)
            {
                case ClampAxis.X:
                    ClampX(ref velocity);
                    break;
                case ClampAxis.Y:
                    ClampY(ref velocity);
                    break;
                case ClampAxis.Z:
                    ClampZ(ref velocity);
                    break;
                case ClampAxis.XY:
                    ClampX(ref velocity);
                    ClampY(ref velocity);
                    break;
                case ClampAxis.XZ:
                    ClampX(ref velocity);
                    ClampZ(ref velocity);
                    break;
                case ClampAxis.YZ:
                    ClampY(ref velocity);
                    ClampZ(ref velocity);
                    break;
                case ClampAxis.XYZ:
                    ClampX(ref velocity);
                    ClampY(ref velocity);
                    ClampZ(ref velocity);
                    break;
                default:
                    break;
            }
            #endregion

            velocity.x = Mathf.Clamp(velocityChange.x, -speed, speed);
            velocity.y = 0f;
            velocity.z = 0f;

            return velocity;
        }

        #region Velocity Clamps
        private void ClampX(ref Vector3 velocity)
        {
            velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
            velocity.y = 0f;
            velocity.z = 0f;
        }

        private void ClampY(ref Vector3 velocity)
        {
            velocity.x = 0f;
            velocity.y = Mathf.Clamp(velocity.y, -speed, speed);
            velocity.z = 0f;
        }

        private void ClampZ(ref Vector3 velocity)
        {
            velocity.x = 0f;
            velocity.y = 0f;
            velocity.z = Mathf.Clamp(velocity.z, -speed, speed);
        }

        private void ClampXY(ref Vector3 velocity)
        {
            velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
            velocity.y = Mathf.Clamp(velocity.y, -speed, speed);
            velocity.z = 0f;
        }

        private void ClampXZ(ref Vector3 velocity)
        {
            velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
            velocity.y = 0f;
            velocity.z = Mathf.Clamp(velocity.z, -speed, speed);
        }

        private void ClampYZ(ref Vector3 velocity)
        {
            velocity.x = 0f;
            velocity.y = Mathf.Clamp(velocity.y, -speed, speed);
            velocity.z = Mathf.Clamp(velocity.z, -speed, speed);
        }

        private void ClampXYZ(ref Vector3 velocity)
        {
            velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
            velocity.y = Mathf.Clamp(velocity.y, -speed, speed);
            velocity.z = Mathf.Clamp(velocity.z, -speed, speed);
        }
        #endregion

        public void ModifyAcceleration(float direction, ref float acceleration)
        {
            if (direction == 0f)
            {
                if (acceleration > minAcceleration)
                    acceleration -= Time.deltaTime * decelerationRate;
            }
            else
            {
                if (acceleration < maxAcceleration)
                    acceleration += Time.deltaTime * accelerationRate;
            }

            if (acceleration < minAcceleration || acceleration > maxAcceleration)
                acceleration = Mathf.Clamp(acceleration, minAcceleration, maxAcceleration);
        }
    }
}
