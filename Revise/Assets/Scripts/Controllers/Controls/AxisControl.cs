using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Movements
{
    public enum AxisValue { Positive, Negative }

    [Serializable]
    public class AxisControl
    {
        [SerializeField] AxisValue axisValue = AxisValue.Positive;
        [SerializeField] [Range(0f, 1f)] public float minimum;
        [SerializeField] [Range(0f, 1f)] public float maximum;
        [SerializeField] [Range(0f, 1f)] public float threshold;
        [SerializeField] private bool useTimer = true;

        private float timer = 0f;

        public AxisControl() { }

        public AxisControl(float minimum, float maximum, float threshold)
        {
            this.minimum = minimum;
            this.maximum = maximum;
            this.threshold = threshold;
        }

        public bool IsSuccessful(float direction)
        {
            bool isSuccessful = false;
            switch(axisValue)
            {
                case AxisValue.Positive:
                    isSuccessful = PositiveSuccess(direction);
                    break;
                case AxisValue.Negative:
                    isSuccessful = NegativeSuccess(direction);
                    break;
                default:
                    break;
            }

            return isSuccessful;
        }

        private bool PositiveSuccess(float direction)
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
                if (absDirection >= minimum && absDirection <= maximum)
                    timer += Time.deltaTime;

            return (timer <= threshold && absDirection >= maximum);
        }

        private bool NegativeSuccess(float direction)
        {
            if (direction > 0)
                return false;

            float negDirection = (-1) * Mathf.Abs(direction);
            minimum = (-1) * Mathf.Abs(minimum);
            maximum = (-1) * Mathf.Abs(maximum);

            if (direction == 0f)
            {
                timer = 0f;
                return false;
            }

            if (useTimer)
                if (negDirection <= minimum && negDirection >= maximum)
                    timer += Time.deltaTime;

            return (timer <= threshold && negDirection <= maximum);
        }
    }
}
