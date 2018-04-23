using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class ActorControl : MonoBehaviour
    {
        [SerializeField] private PlayerNumber playerNumber;

        public Device device;

        // Use this for initialization
        void Start()
        {
            int playerNumber = (int)this.playerNumber;

            name = Input.GetJoystickNames()[playerNumber - 1];
            device = new Device(name, playerNumber);
        }

        // Update is called once per frame
        void Update() {
            device.UpdateDevice();
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10f, 10f, 200f, 100f), "Control Name: " + name);
            GUI.Label(new Rect(10f, 50f, 200f, 100f), "Action1: " + device.Action1.Press + " " + device.Action1.Hold + " " + device.Action1.Release);
            GUI.Label(new Rect(10f, 70f, 200f, 100f), "Action2: " + device.Action2.Press + " " + device.Action2.Hold + " " + device.Action2.Release);
            GUI.Label(new Rect(10f, 90f, 200f, 100f), "Action3: " + device.Action3.Press + " " + device.Action3.Hold + " " + device.Action3.Release);
            GUI.Label(new Rect(10f, 110f, 200f, 100f), "Action4: " + device.Action4.Press + " " + device.Action4.Hold + " " + device.Action4.Release);
            GUI.Label(new Rect(10f, 130f, 200f, 100f), "LeftTrigger: " + device.LeftTrigger.Press + " " + device.LeftTrigger.Hold + " " + device.LeftTrigger.Release);
            GUI.Label(new Rect(10f, 150f, 200f, 100f), "RightTrigger: " + device.RightTrigger.Press + " " + device.RightTrigger.Hold + " " + device.RightTrigger.Release);
            GUI.Label(new Rect(10f, 170f, 200f, 100f), "LeftBumper: " + device.LeftBumper.Press + " " + device.LeftBumper.Hold + " " + device.LeftBumper.Release);
            GUI.Label(new Rect(10f, 190f, 200f, 100f), "RightBumper: " + device.RightBumper.Press + " " + device.RightBumper.Hold + " " + device.RightBumper.Release);
            GUI.Label(new Rect(10f, 210f, 200f, 100f), "LeftStick: " + device.LeftStickPress.Press + " " + device.LeftStickPress.Hold + " " + device.LeftStickPress.Release);
            GUI.Label(new Rect(10f, 230f, 200f, 100f), "RightStick: " + device.RightStickPress.Press + " " + device.RightStickPress.Hold + " " + device.RightStickPress.Release);

        }
    }
}