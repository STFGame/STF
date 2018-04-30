using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace Actor
{
    /// <summary>
    /// All the jump data.
    /// </summary>
    [Serializable]
    public sealed class Jump
    {
        [SerializeField] [Range(0f, 100f)] private float jumpHeight = 5f;
        [SerializeField] [Range(0, 20)] private int maxJumps = 2;
        [SerializeField] [Range(0f, 1f)] private float heightLimit = 1f;
        [SerializeField] [Range(1f, 10f)] public float descentSpeed = 5f;
        [Range(0f, 1f)] public float jumpDelay;

        public int jumpCounter = 0;

        private float heightTimer = 0f;

        public bool isFastFalling = false;
        public bool IsJumpsExceeded { get { return jumpCounter >= maxJumps; } }

        public float VerticalVelocity(float gravity)
        {
            float verticalSpeed = Mathf.Sqrt(2f * gravity * jumpHeight);
            return verticalSpeed;
        }

        public bool CanAscend(bool isAscending)
        {
            if (!isAscending || jumpCounter > maxJumps)
            {
                heightTimer = 0f;
                return false;
            }

            heightTimer += Time.deltaTime;

            return (heightTimer < heightLimit);
        }
    }
}
