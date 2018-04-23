using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Movements
{
    [Serializable]
    public struct MotionControl
    {
        [SerializeField] [Range(0f, 0.5f)] public float minimum;
        [SerializeField] [Range(0.5f, 1f)] public float maximum;
        [SerializeField] [Range(0f, 1f)] public float sensitivity;

        public MotionControl(float minimum, float maximum, float sensitivity)
        {
            this.minimum = minimum;
            this.maximum = maximum;
            this.sensitivity = sensitivity;
        }
    }
}
