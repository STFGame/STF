using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public struct Graph
    {
        public float minimum;
        public float maximum;

        public Graph(float minimum, float maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }
    }
}
