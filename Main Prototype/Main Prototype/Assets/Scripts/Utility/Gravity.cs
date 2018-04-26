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
        [SerializeField] private bool isEnabled = true;

        [HideInInspector] public float counter = 0f;

        public Gravity()
        {
            counter = gravity;
        }

        public Vector3 Gravitational(float velocityY, bool modify)
        {
            if (!isEnabled)
                return Vector3.zero;

            if (modify && counter > 0f)
                counter -= (decrease / Time.deltaTime);
            else if (!modify && counter != gravity)
                counter += (increase / Time.deltaTime);

            counter = Mathf.Clamp(counter, 0f, gravity);

            Vector3 targetVelocity = new Vector3(0f, velocityY, 0f) * counter;
            targetVelocity.y = Mathf.Clamp(targetVelocity.y, -gravity, gravity);

            return targetVelocity;
        }
    }
}
