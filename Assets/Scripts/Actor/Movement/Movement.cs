using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;

namespace Actor
{
    [Serializable]
    public sealed class Movement
    {
        [Header("Physical Movement")]
        [SerializeField] private Motion regular = new Motion(30f, 10f);
        [SerializeField] private Motion crouch = new Motion(20f, 10f);
        [SerializeField] private Motion dash = new Motion(45f, 20f);

        [SerializeField] private Velocity velocity = new Velocity();

        private bool isDashing = false;
        private bool isCrouching = false;
        private bool isTurning = false;

        [Header("Controls")]
        [SerializeField] [Range(0f, 1f)] private float dashMin = 0.1f;
        [SerializeField] [Range(0f, 1f)] private float dashMax = 0.75f;
        [SerializeField] [Range(0f, 1f)] private float dashSensitivity = 0.1f;
        [SerializeField] [Range(-1f, 0f)] private float crouchRange = -0.6f;

        private float dashTimer = 0f;

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

        public Vector3 HorizontalVelocity(Vector3 direction, Vector3 currentVelocity)
        {
            float speed = GetSpeed(direction);
            return velocity.VelocityX(speed, direction.x, currentVelocity);
        }

        private float GetSpeed(Vector3 direction)
        {
            IsDashing(direction.x);
            IsCrouching(direction.z);

            float actorSpeed = (isDashing) ? dash.speed : (isCrouching) ? crouch.speed : regular.speed;
            return actorSpeed;
        }

        private void IsDashing(float direction)
        {
            float absDirection = Mathf.Abs(direction);

            if (absDirection == 0f)
            {
                dashTimer = 0f;
                return;
            }

            if (absDirection > dashMin && absDirection < dashMax)
                dashTimer += Time.deltaTime;

            isDashing = (absDirection > dashMax && dashTimer < dashSensitivity);
        }

        private void IsCrouching(float direction)
        {
            isCrouching = (direction < crouchRange);
        }

        public bool IsTurning(float direction, Transform forward)
        {
            return (forward.forward.x * direction < 0f);
        }
    }
}
