using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character.Instrument
{
    public class Instrument
    {
        protected string[] Name;

        float prevStick;
        float stick;

        protected Instrument(Instrument other)
        {
            this.Obj = other.Obj;
        }

        public object Obj { get; set; }

        private bool BuffStick()
        {
            stick = Mathf.Abs(Input.GetAxis("Horizontal"));
            if (stick > 0)
                stick = 1f;

            if (stick == prevStick || stick == 0f)
            {
                if (stick == 0f)
                    prevStick = 0f;
                return false;
            }

            prevStick = stick;
            return true;
        }
    }
}
