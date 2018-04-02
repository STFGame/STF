using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;

/* BUTTON
 * Sean Ryan
 * March 29, 2018
 * 
 * This class is responsible for all button related tasks. These tasks include, checking if the user has pressed a button,
 * released a button, or held a button. It also counts how many times a button was pressed in a 1 second. Finally, it
 * is capable of returning how long a button is held for.
 * 
 */ 

namespace Controller.Mechanism
{
    public class Button
    {
        private bool actionClick;
        private bool actionRelease;
        private bool actionHold;

        private float holdTimer = 0f;
        private float pressTimer = 0f;
        private int press = 0;

        public void OnUpdate(PlayerNumber playerType, int keyNum)
        {
            actionClick = Input.GetKeyDown("joystick " + (int)playerType + " button " + keyNum);
            actionRelease = Input.GetKeyUp("joystick " + (int)playerType + " button " + keyNum);
            actionHold = Input.GetKey("joystick " + (int)playerType + " button " + keyNum);

            holdTimer = (actionHold) ? TimerUpdate(holdTimer) : 0f;

            press = (actionClick) ? press + 1 : (pressTimer > 1.0f) ? 0 : press;

            TimesPressed();
        }

        private float TimerUpdate(float time)
        {
            return time += Time.deltaTime;
        }

        private void TimesPressed()
        {
            if (pressTimer < 1.0f && press > 0)
            {
                pressTimer = TimerUpdate(pressTimer);
                return;
            }
            pressTimer = 0f;
        }

        #region Properties
        public float HoldTimer
        {
            get { return holdTimer; }
        }

        public int Press
        {
            get { return press; }
        }

        public bool ActionClick
        {
            get { return actionClick; }
        }

        public bool ActionRelease
        {
            get { return actionRelease; }
        }

        public bool ActionHold
        {
            get { return actionHold; }
        }
        #endregion
    }
}
