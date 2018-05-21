using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls
{
    public class ProfileName
    {
        private static string[] PlayStation4 = new string[]
        {
            "Wireless Controller"
        };

        private static string[] PlayStation3 = new string[]
            {
                "MotionJoy Virtual Game Controller",
                "PLAYSTATION(R)3 Controller"
            };


        private static string[] Xbox360 = new string[]
        {
            "Controller (Afterglow Gamepad for Xbox 360)",
            "Controller (Batarang wired controller(XBOX))",
            "Controller (Gamepad for Xbox 360)",
            "Controller (Infinity Controller 360)",
            "Controller (Mad Catz FPS Pro GamePad",
            "Controller (MadCatz Call of Duty GamePad",
            "Controller (MadCatz GamePad)",
            "Controller (MLG GamePad for Xbox 360)",
            "Controller (Razer Sabertoothe Elite)",
            "Controller (Rock Candy Gamepad for Xbox 360)",
            "Controller (Xbox 360 For Windows)",
            "Controller (Xbox 360 Wireless Receiver for Windows)",
            "XBOX 360 For Windows (Controller)",
            "Controller (XEOX Gamepad)"
        };

        private static string[] XboxOne = new string[]
        {
            "Controller (XBOX One For Windows)",
        };

        public static string GetProfileName(string controlName)
        {
            if (PlayStation4.Contains(controlName))
                return "PlayStation4";
            else if (Xbox360.Contains(controlName))
                return "Xbox360";
            else if (PlayStation3.Contains(controlName))
                return "PlayStation3";
            else if (XboxOne.Contains(controlName))
                return "XboxOne";

            return "Keyboard";
        }
    }
}
