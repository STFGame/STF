using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls
{
    public class Xbox360Profile : ButtonQuery
    {
        public Xbox360Profile(int number)
        {
            buttonQuery = new string[10];

            buttonQuery[(int)ButtonInput.Action1] = "joystick " + number + " button " + 0;
            buttonQuery[(int)ButtonInput.Action2] = "joystick " + number + " button " + 2;
            buttonQuery[(int)ButtonInput.Action3] = "joystick " + number + " button " + 1;
            buttonQuery[(int)ButtonInput.Action4] = "joystick " + number + " button " + 3;
            buttonQuery[(int)ButtonInput.LeftStick] = "joystick " + number + " button " + 8;
            buttonQuery[(int)ButtonInput.RightStick] = "joystick " + number + " button " + 9;
            buttonQuery[(int)ButtonInput.LeftBumper] = "joystick " + number + " button " + 4;
            buttonQuery[(int)ButtonInput.RightBumper] = "joystick " + number + " button " + 5;
            buttonQuery[(int)ButtonInput.LeftTrigger] = "joystick " + number + " button " + 12;
            buttonQuery[(int)ButtonInput.RightTrigger] = "joystick " + number + " button " + 13;
        }
    }
}
