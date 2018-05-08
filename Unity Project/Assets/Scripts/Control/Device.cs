using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls
{
    [Serializable]
    public class Device
    {
        private Joystick joystick;
        private Button[] buttons;
        private DeviceProfile deviceProfile;

        public readonly int maxButtons = 10;

        public Device(string name, int controlNumber)
        {
            deviceProfile = new DeviceProfile(name);
            buttons = new Button[maxButtons];

            for (int i = 0; i < buttons.Length; i++)
                buttons[i] = new Button(deviceProfile.GetProfile(controlNumber).Query(i));

            joystick = new Joystick(controlNumber);
        }

        public void UpdateDevice()
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].UpdateButton();

            joystick.UpdateJoystick();
        }

        public Button GetButton(ButtonInput input) { return buttons[(int)input]; }

        public Button GetButton(int input) { return buttons[input]; }

        public Button Action1 { get { return buttons[(int)ButtonInput.Action1]; } }

        public Button Action2 { get { return buttons[(int)ButtonInput.Action2]; } }

        public Button Action3 { get { return buttons[(int)ButtonInput.Action3]; } }

        public Button Action4 { get { return buttons[(int)ButtonInput.Action4]; } }

        public Button LeftTrigger { get { return buttons[(int)ButtonInput.LeftTrigger]; } }

        public Button RightTrigger { get { return buttons[(int)ButtonInput.RightTrigger]; } }

        public Button LeftBumper { get { return buttons[(int)ButtonInput.LeftBumper]; } }

        public Button RightBumper { get { return buttons[(int)ButtonInput.RightBumper]; } }

        public Button LeftStickPress { get { return buttons[(int)ButtonInput.LeftStick]; } }

        public Button RightStickPress { get { return buttons[(int)ButtonInput.RightStick]; } }

        public Joystick LeftStick { get { return joystick; } }

    }
}
