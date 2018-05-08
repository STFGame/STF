using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Controls
{
    public class JoystickQuery : IControlQuery
    {
        [SerializeField] private ControlIdentity[] joystickIdentity;
        private string[] joystickQuery;

        public JoystickQuery(int number)
        {
            if (joystickIdentity == null)
                joystickIdentity = new ControlIdentity[2];

            joystickQuery = new string[joystickIdentity.Length];

            for (int i = 0; i < joystickQuery.Length; i++)
                joystickQuery[i] = "joystick " + number + " axis " + joystickIdentity[i].key;
        }

        public string Query(int index)
        {
            return joystickQuery[index];
        }
    }
}
