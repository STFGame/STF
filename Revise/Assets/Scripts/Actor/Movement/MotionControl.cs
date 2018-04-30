using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Movements
{
    [Serializable]
    public class MotionControl
    {
        [SerializeField] [Range(-1f, 1f)] public float minimum;
        [SerializeField] [Range(-1f, 1f)] public float maximum;
        [SerializeField] [Range(0f, 1f)] public float threshold;
        [SerializeField] private bool useTimer = true;

        private float timer = 0f;

        public MotionControl() { }

        public MotionControl(float minimum, float maximum, float threshold)
        {
            this.minimum = minimum;
            this.maximum = maximum;
            this.threshold = threshold;
        }

        public bool IsSuccessful(float direction)
        {
            float absDirection = Mathf.Abs(direction);
            minimum = Mathf.Abs(minimum);
            maximum = Mathf.Abs(maximum);

            if (absDirection == 0f)
            {
                timer = 0f;
                return false;
            }

            if (useTimer)
                if (absDirection > minimum && absDirection < maximum)
                    timer += Time.deltaTime;

            return (timer < threshold && absDirection > maximum);
        }
    }
}
