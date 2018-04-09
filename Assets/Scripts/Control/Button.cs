using System;
using UnityEngine;
using Utility.Enums;

/* BUTTON
 * Sean Ryan
 * March 29, 2018
 */

namespace Controller.Mechanism
{
    /// <summary>
    /// This class is responsible for all button related tasks. These tasks include, checking if the user has pressed a button, <para />
    /// released a button, or held a button. It also counts how many times a button was pressed in a 1 second. Finally, it <para />
    /// is capable of returning how long a button is held for.
    /// </summary>
    [Serializable]
    public sealed class Button
    {
        [SerializeField] private float pressThreshold = 1.0f;                    //The max amount of time that a button can be pressed before it resets to 0
        private bool consume;

        ///<summary>A bool for determining whether the button is clicked</summary>
        public bool Click { get; set; }

        /// <summary>A bool that is designed to prevent input loss by consuming the Click bool</summary>
        public bool Consume
        {
            get
            {
                if(consume)
                {
                    consume = false;
                    return true;
                }
                return false;
            }
        }

        ///<summary>A bool that determines whether the button is released</summary>
        public bool Release { get; private set; }

        /// <summary>A bool that determines whether the button is being held</summary>
        public bool Hold { get; private set; }

        /// <summary>Measures how long a button is held for</summary>
        public float HoldTimer { get; private set; }

        /// <summary>Counts how many times a button is pressed within a time limit, <para />
        /// then resets back to 0</summary>
        public int Press { get; private set; }

        private float pressTimer = 0f;                                            //A variable that is added to time when a button is first clicked

        //Updates all of the buttons to their correct values every frame. The playerNumber variable
        //passes the controller number of the player and the keyNum is which button is being updated.
        public void UpdateButton(PlayerNumber playerNumber, int keyNum)
        {
            if (keyNum <= 0)
                return;

            Click = /*(!Click) ? */Input.GetKeyDown("joystick " + (int)playerNumber + " button " + keyNum); /*: Click;*/
            Release = Input.GetKeyUp("joystick " + (int)playerNumber + " button " + keyNum);
            Hold = Input.GetKey("joystick " + (int)playerNumber + " button " + keyNum);

            consume = (!consume) ? Click : consume;

            HoldTimer = (Hold) ? TimerUpdate(HoldTimer) : 0f;
            Press = (Click) ? Press + 1 : (pressTimer > pressThreshold) ? 0 : Press;

            TimesPressed();
        }

        //A method that adds a float to the Time.deltaTime
        private float TimerUpdate(float time)
        {
            return time += Time.deltaTime;
        }

        //Updates the press timer while the pressTimer is below the pressThreshold
        private void TimesPressed()
        {
            if (pressTimer < pressThreshold && Press > 0)
            {
                pressTimer = TimerUpdate(pressTimer);
                return;
            }
            pressTimer = 0f;
        }
    }
}
