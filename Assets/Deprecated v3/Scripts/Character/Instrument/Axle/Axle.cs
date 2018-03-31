using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character.Instrument
{
    public class Axle
    {
        private float horizontal;
        private float vertical;

        public float Horizontal
        {
            get { return horizontal; }
            set { horizontal = value; }
        }

        public float Vertical
        {
            get { return vertical; }
            set { vertical = value; }
        }
    }
}
