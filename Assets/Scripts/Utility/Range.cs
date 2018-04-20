using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public struct Range
    {
        [SerializeField] private float min;
        [SerializeField] private float max;

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public bool InRange(float value)
        {
            return (value >= min && value <= max);
        }
    }
}
