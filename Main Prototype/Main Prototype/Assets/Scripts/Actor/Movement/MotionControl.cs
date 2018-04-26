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
        [SerializeField] [Range(0f, 0.5f)] public float minimum;
        [SerializeField] [Range(0.5f, 1f)] public float maximum;
        [SerializeField] [Range(0f, 1f)] public float threshold;

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

            if(absDirection == 0f)
            {
                timer = 0f;
                return false;
            }

            if (absDirection > minimum && absDirection < maximum)
                timer += Time.deltaTime;

            return (timer < threshold && absDirection > maximum);
        }
    }
}
