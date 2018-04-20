using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace STF.Controls
{
    public sealed class Joystick
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public float AbsoluteHorizontal { get; private set; }
        public float AbsoluteVertical { get; private set; }

        public int RawHorizontal { get; private set; }
        public int RawVertical { get; private set; }

        public void UpdateJoystick()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            AbsoluteHorizontal = Mathf.Abs(Horizontal);
            AbsoluteVertical = Mathf.Abs(Vertical);

            RawHorizontal = (int)Horizontal;
            RawVertical = (int)Vertical;
        }
    }
}
