using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls
{
    public class DeviceProfile
    {
        private string name;

        public DeviceProfile(string name)
        {
            this.name = name;
        }

        public IControlQuery GetProfile(int number)
        {
            if (ProfileNameList.PlayStation.Contains(name))
            {
                PlayStation4Profile playStation = new PlayStation4Profile(number);
                return playStation;
            }
            else if(ProfileNameList.Xbox.Contains(name))
            {
                Xbox360Profile xbox = new Xbox360Profile(number);
                return xbox;
            }

            return null;
        }

        public IControlQuery GetJoystick(int number)
        {
            return new JoystickQuery(number);
        }
    }
}
