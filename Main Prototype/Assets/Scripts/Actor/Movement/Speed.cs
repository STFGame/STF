using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor
{
    [Serializable]
    public struct Speed
    {
        [Range(1f, 100f)] public float speed;
        [Range(1f, 100f)] public float rotationSpeed;

        public Speed(float speed, float rotationSpeed)
        {
            this.speed = speed;
            this.rotationSpeed = rotationSpeed;
        }
    }
}
