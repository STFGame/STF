using STF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor
{
    public enum ComboInput
    {
        Right,
        Left,
        Up,
        Down,

        Press,
        Hold,
    }

    [Serializable]
    public class Combo
    {
        private ComboInput[] combo;
        private int currentIndex = 0;

        [SerializeField] private float timeBetweenPresses = 0.3f;
        private float timeLastPress;

        private Joystick joystick = new Joystick();
        public Button[] button;

        public Combo(ComboInput[] c)
        {
            combo = c;
        }

        public bool CheckCombo2()
        {
            joystick.UpdateJoystick();
            for (int i = 0; i < button.Length; i++)
                button[i].UpdateButton();

            if (Time.time > timeLastPress + timeBetweenPresses) currentIndex = 0;
            {
                if (currentIndex < combo.Length)
                {
                    for (int i = 0; i < button.Length; i++)
                    {

                        if ((combo[currentIndex] == ComboInput.Down && joystick.RawVertical == -1) ||
                            (combo[currentIndex] == ComboInput.Up && joystick.RawVertical == 1) ||
                            (combo[currentIndex] == ComboInput.Left && joystick.RawHorizontal == -1) ||
                            (combo[currentIndex] == ComboInput.Right && joystick.RawHorizontal == 1) ||
                            (combo[currentIndex] == ComboInput.Press && button[i].Press) ||
                            (combo[currentIndex] == ComboInput.Hold && button[i].Hold))
                        {
                            timeLastPress = Time.time;
                            currentIndex++;
                        }
                    }
                }

                if (currentIndex >= combo.Length)
                {
                    currentIndex = 0;
                    return true;
                }
                return false;
            }
        }

        public bool CheckCombo()
        {
            if (Time.time > timeLastPress + timeBetweenPresses) currentIndex = 0;
            {
                if (currentIndex < combo.Length)
                {
                    if ((combo[currentIndex] == ComboInput.Down && Input.GetAxisRaw("Vertical") == -1) ||
                        (combo[currentIndex] == ComboInput.Up && Input.GetAxisRaw("Vertical") == 1) ||
                        (combo[currentIndex] == ComboInput.Left && Input.GetAxisRaw("Horizontal") == -1) ||
                        (combo[currentIndex] == ComboInput.Right && Input.GetAxisRaw("Horizontal") == 1))
                    //(combo[currentIndex] != 1 && combo[currentIndex] != 2 && combo[currentIndex] != 3 && combo[currentIndex] != 4 && Input.GetKeyDown(KeyCode.Joystick1Button0)))
                    {
                        timeLastPress = Time.time;
                        currentIndex++;
                    }
                }

                if (currentIndex >= combo.Length)
                {
                    currentIndex = 0;
                    return true;
                }
                return false;
            }
        }
    }
}
