using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Player.CC
{
    [Serializable]
    public class Buttons
    {
        public KeyCode Jump = KeyCode.Space | KeyCode.Joystick1Button1;

        public KeyCode Action1;
        public KeyCode Action2;
        public KeyCode Action3;
        public KeyCode Action4;

        public KeyCode Action5;

        float _tick = 0f;

        #region Constructors
        public Buttons()
        {

        }
        #endregion

        public bool KeyHoldLength(KeyCode key, float maxVal)
        {
            if (Input.GetKey(KeyCode.C))
                _tick = Time.time;

            if (_tick - Time.time <= maxVal)
                return true;
            return false;
        }
    }
}