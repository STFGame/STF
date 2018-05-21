using System;

namespace Controls
{
    [Serializable]
    public class Device
    {
        private Button[] buttons = null;
        private Axis[] axes = null;

        private DeviceProfile deviceProfile = null;

        public int NumberOfButtons { get; private set; }

        public int NumberOfAxes { get; private set; }

        public Device(string name, int controlNumber)
        {
            deviceProfile = new DeviceProfile(name);

            NumberOfButtons = deviceProfile.ButtonCount;
            NumberOfAxes = deviceProfile.AxisCount;

            buttons = new Button[deviceProfile.ButtonCount];
            for (int i = 0; i < buttons.Length; i++)
                buttons[i] = new Button(deviceProfile.GetMap(i), controlNumber, i);

            axes = new Axis[deviceProfile.AxisCount];
            for (int i = 0; i < axes.Length; i++)
                axes[i] = new Axis(deviceProfile.GetMap(i + DeviceProfile.AXIS_MULTIPLIER), controlNumber, i);
        }

        public void Execute()
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].UpdateButton();

            for (int i = 0; i < axes.Length; i++)
                axes[i].UpdateAxis();
        }

        #region Buttons and Axes Control
        public Button GetButton(int buttonNumber) { return buttons[buttonNumber]; }

        public Button Action1 { get { return buttons[deviceProfile.Action1]; } }

        public Button Action2 { get { return buttons[deviceProfile.Action2]; } }

        public Button Action3 { get { return buttons[deviceProfile.Action3]; } }

        public Button Action4 { get { return buttons[deviceProfile.Action4]; } }

        public Button L1 { get { return buttons[deviceProfile.L1]; } }

        public Button R1 { get { return buttons[deviceProfile.R1]; } }

        public Button L2A { get { return buttons[deviceProfile.L2A]; } }

        public Button R2A { get { return buttons[deviceProfile.R2A]; } }

        public Button Select { get { return buttons[deviceProfile.Select]; } }

        public Button Start { get { return buttons[deviceProfile.Start]; } }

        public Button LeftStick { get { return buttons[deviceProfile.LeftStick]; } }

        public Button RightStick { get { return buttons[deviceProfile.RightStick]; } }

        public Button CenterButton { get { return buttons[deviceProfile.CenterButton]; } }

        public Button TouchPad { get { return buttons[deviceProfile.TouchPad]; } }

        public Axis LeftHorizontal { get { return axes[deviceProfile.LeftH]; } }

        public Axis LeftVertical { get { return axes[deviceProfile.LeftV]; } }

        public Axis RightHorizontal { get { return axes[deviceProfile.RightH]; } }

        public Axis RightVertical { get { return axes[deviceProfile.RightV]; } }

        public Axis L2B { get { return axes[deviceProfile.L2B]; } }

        public Axis R2B { get { return axes[deviceProfile.R2B]; } }

        public Axis DPadH { get { return axes[deviceProfile.DPadH]; } }

        public Axis DPadV { get { return axes[deviceProfile.DPadV]; } }
        #endregion

    }
}
