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

        private string[] query;
        private int number = 0;

        public Joystick(int number)
        {
            this.number = number;

            query = new string[]
            {
                "joystick " + number + " axis " + 0,
                "joystick " + number + " axis " + 1
            };
        }

        public void UpdateJoystick()
        {
            Horizontal = Input.GetAxis(query[0]);
            Vertical = Input.GetAxis(query[1]);

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
