using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character.Instrument
{
    public class Axle
    {
        private float[] axleX;
        private float[] axleY;

        private Vector2[] axleUnit;

        public Axle()
        {
            axleX = new float[2];
            axleY = new float[2];
            axleUnit = new Vector2[2];

            axleUnit[0] = new Vector2(axleX[0], axleY[0]);
        }

        public bool PressHorizontal()
        {
            if (axleX[0] == 0f || axleX[0] == axleX[1])
                return false;

            axleX[1] = axleX[0];

            return true;
        }

        public bool PressVertical()
        {
            if (axleY[0] == 0f || axleY[0] == axleY[1])
                return false;

            axleY[1] = axleY[0];

            return true;
        }

        public Vector2 AxleUnit
        {
            get { return axleUnit[0]; }
            set { axleUnit[0] = value; }
        }

        public float AxleX
        {
            get { return axleX[0]; }
            set { axleX[0] = value; }
        }

        public float AxleY
        {
            get { return axleY[0]; }
            set { axleY[0] = value; }
        }
    }
}
