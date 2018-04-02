using System;
using UnityEngine;
using Utility.Enums;

namespace Controller.Mechanism
{
    public class Lever
    {
        private float horizontal;
        private float absoluteHorizontal;
        private float rawHorizontal;

        private float vertical;
        private float absoluteVertical;
        private float rawVertical;

        private Vector2 circle;

        private float timer = 0f;

        public void OnUpdate(PlayerNumber playerType)
        {
            horizontal = Input.GetAxis("joystick " + (int)playerType + " axis " + 0);
            vertical = Input.GetAxis("joystick " + (int)playerType + " axis " + 1);

            rawHorizontal = Input.GetAxisRaw("joystick " + (int)playerType + " axis " + 0);
            rawVertical = Input.GetAxisRaw("joystick " + (int)playerType + " axis " + 1);

            absoluteHorizontal = Math.Abs(horizontal);
            absoluteVertical = Math.Abs(vertical);

            circle = new Vector2(horizontal, vertical);

            LeverSpeed();
        }

        private void LeverSpeed()
        {
            float absoluteHorizontal = Math.Abs(horizontal);

            if (absoluteHorizontal == 0f)
            {
                timer = 0f;
                return;
            }

            timer = (absoluteHorizontal > 0f && absoluteHorizontal < 0.75f) ? timer + Time.deltaTime : timer;
        }

        #region Properties
        public float Horizontal
        {
            get { return horizontal; }
            set { horizontal = value; }
        }

        public float AbsoluteHorizontal
        {
            get { return absoluteHorizontal; }
        }

        public float RawHorizontal
        {
            get { return rawHorizontal; }
        }

        public float Vertical
        {
            get { return vertical; }
            set { vertical = value; }
        }

        public float AbsoluteVertical
        {
            get { return absoluteVertical; }
        }

        public float RawVertical
        {
            get { return rawVertical; }
        }

        public Vector2 Circle
        {
            get { return circle; }
        }

        public float Timer
        {
            get { return timer; }
        }
        #endregion
    }
}
