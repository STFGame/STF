using System;
using UnityEngine;

namespace Actor
{
    [Serializable]
    public struct Speed
    {
        [Range(1f, 100f)] public float speed;
        [Range(1f, 100f)] public float rotationSpeed;
        [Range(0f, 50f)] public float acceleration;
        [Range(0f, 50f)] public float deceleration;
        [Range(1f, 50f)] public float maxAcceleration;

        public Speed(float speed, float rotationSpeed, float acceleration, float deceleration, float maxAcceleration)
        {
            this.speed = speed;
            this.rotationSpeed = rotationSpeed;
            this.acceleration = acceleration;
            this.deceleration = deceleration;
            this.maxAcceleration = maxAcceleration;
        }
    }
}
