using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Controls
{
    public class Button
    {
        public bool Press { get; private set; }
        public bool Release { get; private set; }
        public bool Hold { get; private set; }
        public bool Trigger { get; set; }

        public int KeyID { get; private set; }

        private string inputName;

        public Button(string inputName, int playerNumber, int keyID)
        {
            char[] split = new char[] { ' ' };

            string[] temp = inputName.Split(split);
            if (temp.Length > 3)
            {
                temp[1] = playerNumber.ToString();

                inputName = "";
                for (int i = 0; i < temp.Length; i++)
                    inputName += temp[i] + " ";
                inputName = inputName.TrimEnd();
            }
            this.inputName = inputName;

            KeyID = keyID;
        }

        public void UpdateButton()
        {
            Press = Input.GetKeyDown(inputName);
            Hold = Input.GetKey(inputName);
            Release = Input.GetKeyUp(inputName);
            Trigger = Trigger || Press;
        }
    }
}
