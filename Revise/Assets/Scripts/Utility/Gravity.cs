using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public class Gravity
    {
        [SerializeField] [Range(1f, 1000f)] private float gravity = 40f;
        [SerializeField] [Range(1f, 1000f)] private float increase = 1f;
        [SerializeField] [Range(1f, 1000f)] private float decrease = 1f;
        public bool IsEnabled = true;

        private Vector3 targetVelocity = Vector3.zero;

        public float Counter { get; private set; }

        public Gravity()
        {
            Counter = gravity;
        }

        public void ModifyGravity(float velocityY, bool modify)
        {
            if (!IsEnabled)
                return;

            if (modify && Counter > 0f)
                Counter -= (decrease / Time.deltaTime);
            else if (!modify && Counter != gravity)
                Counter += (increase / Time.deltaTime);

            Counter = Mathf.Clamp(Counter, 0f, gravity);

            targetVelocity = new Vector3(0f, velocityY, 0f) * Counter;
            targetVelocity.y = Mathf.Clamp(targetVelocity.y, -gravity, gravity);
        }

        public Vector3 ApplyGravity()
        {
            return targetVelocity;
        }
    }
}
