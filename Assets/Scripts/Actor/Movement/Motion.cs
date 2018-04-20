using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor
{
    [Serializable]
    public struct Motion
    {
        public float speed;
        public float rotationSpeed;

        public Motion(float speed, float rotationSpeed)
        {
            this.speed = speed;
            this.rotationSpeed = rotationSpeed;
        }
    }
}
