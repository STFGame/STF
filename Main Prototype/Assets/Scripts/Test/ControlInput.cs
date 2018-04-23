using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tests
{
    public class ControlInput
    {
        [SerializeField] private float timeBetweenPressed = 0.3f;

        private float lastPressTime;
        private int currentIndex;
        private int previousInput;


        private int[] input = new int[10];
        public int[] Input { get { return input; } private set { input = value; } }

        public void UpdateInput(Device device)
        {
            if (Time.time > lastPressTime + timeBetweenPressed) currentIndex = 0;
            {
                Register(device.LeftStick.Right, 1, ref lastPressTime);
                Register(device.LeftStick.Left, 2, ref lastPressTime);
                Register(device.LeftStick.Up, 3, ref lastPressTime);
                Register(device.LeftStick.Down, 4, ref lastPressTime);
            }
        }

        private void Register(bool value, int input, ref float lastPressTime)
        {
            if (value)
            {
                if (currentIndex >= Input.Length - 1)
                    currentIndex = 0;

                if (previousInput == input)
                    return;

                Input[currentIndex] = input;
                currentIndex++;

                previousInput = input;

                lastPressTime = Time.time;

                Debug.Log(Input[currentIndex] + " Current Index " + currentIndex);
            }
        }
    }
}

