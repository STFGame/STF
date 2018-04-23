using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Controls
{
    public class Joystick
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public float AbsHorizontal { get; private set; }
        public float AbsVertical { get; private set; }

        public int RawHorizontal { get; private set; }
        public int RawVertical { get; private set; }

        public bool Up { get; private set; }
        public bool Down { get; private set; }

        public bool Right { get; private set; }
        public bool Left { get; private set; }

        private int number;

        public Joystick(int number)
        {
            this.number = number;
        }

        public void UpdateJoystick()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            AbsHorizontal = Mathf.Abs(Horizontal);
            AbsVertical = Mathf.Abs(Vertical);

            RawHorizontal = (int)Horizontal;
            RawVertical = (int)Vertical;

            Up = (RawVertical > 0);
            Down = (RawVertical < 0);

            Right = (RawHorizontal > 0);
            Left = (RawHorizontal < 0);
        }
    }
}
