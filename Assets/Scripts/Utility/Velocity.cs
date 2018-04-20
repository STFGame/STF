using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public class Velocity
    {
        [SerializeField] [Range(1f, 10f)] private float acceleration = 1f;

        public Vector3 VelocityX(float speed, float direction, Vector3 currentVelocity)
        {
            Vector3 targetVelocity = new Vector3(direction, 0f, 0f) * speed;
            Vector3 velocityChange = (targetVelocity - currentVelocity) * (acceleration * Time.deltaTime);

            velocityChange.x = Mathf.Clamp(velocityChange.x, -speed, speed);
            velocityChange.y = 0f;
            velocityChange.z = 0f;

            return velocityChange;
        }

        public Vector3 VelocityY(float speed, float direction, Vector3 currentVelocity)
        {
            Vector3 targetVelocity = new Vector3(0f, direction, 0f) * speed;
            Vector3 velocityChange = (targetVelocity - currentVelocity) * (acceleration * Time.deltaTime);

            velocityChange.x = 0f;
            velocityChange.y = Mathf.Clamp(velocityChange.y, -speed, speed);
            velocityChange.z = 0f;

            return velocityChange;
        }

        public Vector3 VelocityZ(float speed, float direction, Vector3 currentVelocity)
        {
            Vector3 targetVelocity = new Vector3(0f, 0f, direction) * speed;
            Vector3 velocityChange = (targetVelocity - currentVelocity) * (acceleration * Time.deltaTime);

            velocityChange.x = 0f;
            velocityChange.y = 0f;
            velocityChange.z = Mathf.Clamp(velocityChange.z, -speed, speed);

            return velocityChange;
        }
    }
}
