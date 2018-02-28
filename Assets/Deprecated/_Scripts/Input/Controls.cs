using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.CC
{
    [CreateAssetMenu(fileName = "Player", menuName = "Player/Controls", order = 1)]
    public class Controls : ScriptableObject
    {
        //public Dictionary<KeyCode, string> m_Buttons = new Dictionary<KeyCode, string>();

        public Buttons Buttons;
        public Joystick Joystick;


        #region Constructors
        public Controls(){}

        #endregion
    }
}
