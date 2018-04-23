using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ComboSystem
{
    public enum ComboInput { Right, Left, Up, Down, Press, Hold, Release }

    [Serializable]
    public class Combo
    {
        public string comboName = "Base Combo";

        [SerializeField] private ComboInput[] combo;
        [SerializeField] private ButtonInput buttonInput;
        [SerializeField] private float timeBetweenPresses = 0.3f;
        private float lastPressTime = 0f;

        private Device device;

        private int currentIndex;

        public void Init(Device device)
        {
            this.device = device;
        }

        public bool CheckCombo()
        {
            if (Time.time > lastPressTime + timeBetweenPresses) currentIndex = 0;
            {
                if (combo[currentIndex] == ComboInput.Right && device.LeftStick.Right ||
                    combo[currentIndex] == ComboInput.Left && device.LeftStick.Left ||
                    combo[currentIndex] == ComboInput.Up && device.LeftStick.Up ||
                    combo[currentIndex] == ComboInput.Down && device.LeftStick.Down ||
                    combo[currentIndex] == ComboInput.Press && device.GetButton(buttonInput).Press ||
                    combo[currentIndex] == ComboInput.Release && device.GetButton(buttonInput).Release ||
                    combo[currentIndex] == ComboInput.Hold && device.GetButton(buttonInput).Hold)
                {
                    lastPressTime = Time.time;
                    currentIndex++;
                }

                if (currentIndex >= combo.Length)
                {
                    currentIndex = 0;
                    return true;
                }
            }
            return false;
        }

    }
}
