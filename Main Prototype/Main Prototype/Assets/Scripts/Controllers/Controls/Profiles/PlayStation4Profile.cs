using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls
{
    public class PlayStation4Profile : ButtonQuery
    {
        public PlayStation4Profile(int number)
        {
            buttonQuery = new string[10];

            buttonQuery[0] = "joystick " + number + " button " + 1;     //Action1
            buttonQuery[1] = "joystick " + number + " button " + 0;     //Action2
            buttonQuery[2] = "joystick " + number + " button " + 2;     //Action3
            buttonQuery[3] = "joystick " + number + " button " + 3;     //Action4
            buttonQuery[4] = "joystick " + number + " button " + 10;
            buttonQuery[5] = "joystick " + number + " button " + 11;
            buttonQuery[6] = "joystick " + number + " button " + 4;
            buttonQuery[7] = "joystick " + number + " button " + 5;
            buttonQuery[8] = "joystick " + number + " button " + 6;
            buttonQuery[9] = "joystick " + number + " button " + 7;
        }
    }
}
