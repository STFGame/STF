using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character.Instrument.Profiles
{
    public class PlayStation4Profile : Instrument
    {
        public PlayStation4Profile()
        {
            Name = "PlayStation4 Controller";

            JoystickNames = new[]
            {
                "Wireless Controller"
            };
        }
    }
}
