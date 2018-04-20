using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace STF.Controls
{
    [Serializable]
    public sealed class Button
    {
        [SerializeField] private int number;
        [SerializeField] private int key;

        private bool trigger;

        public bool Press { get; private set; }
        public bool Release { get; private set; }
        public bool Hold { get; private set; }

        public bool Trigger
        {
            get
            {
                if(trigger)
                {
                    trigger = false;
                    return true;
                }
                return false;
            }
        }

        public void UpdateButton(int number, int key)
        {
            Press = Input.GetKeyDown("joystick " + number + " button " + key);
            Release = Input.GetKeyUp("joystick " + number + " button " + key);
            Hold = Input.GetKey("joystick " + number + " button " + key);

            trigger = trigger || Press;
        }

        public void UpdateButton()
        {
            Press = Input.GetKeyDown("joystick " + number + " button " + key);
            Release = Input.GetKeyUp("joystick " + number + " button " + key);
            Hold = Input.GetKey("joystick " + number + " button " + key);

            trigger = trigger || Press;
        }
    }
}
