using System;
using UnityEngine;

namespace Player.CC
{
    [Serializable]
    public class Joystick
    {
        [SerializeField] private string xAxisName;
        [SerializeField] private string yAxisName;

        private float xAxis = 0.0f;
        private float yAxis = 0.0f;
        private Vector3 joystick;

        #region Constructors
        public Joystick() { }
        #endregion

        #region Joystick Methods
        Vector3 joystickPrev = new Vector2(0.0f, 0.0f);
        bool rapid = false;
        float _tick = 0.0f;
        public bool Rapid(float time)
        {
            // Keep track of last time we've switched direction or stopped at the middle.
            if (joystickPrev.x * joystick.x <= 0.0f)
            {
                rapid = false;
                _tick = Time.time;
            }

            joystickPrev.x = joystick.x;

            // If we go past .75 in either direction in .1 seconds since last flip or less, we flicked.
            if (joystick.x >= 0.75f || joystick.x <= -0.75f)
                if ((Time.time - _tick) <= time || rapid == true)
                {
                    rapid = true;
                    return true;
                }

            // nope, we didn't flick
            return false;
        }

        public bool Pivot(Transform position)
        {
            if (position.forward.x * joystick.x >= 0)
                return true;
            return false;
        }

        public Vector2 JStick()
        {
            xAxis = Input.GetAxis(xAxisName);
            yAxis = Input.GetAxis(yAxisName);
            joystick = new Vector2(xAxis, yAxis);
            return joystick;
        }
        #endregion
    }
}
