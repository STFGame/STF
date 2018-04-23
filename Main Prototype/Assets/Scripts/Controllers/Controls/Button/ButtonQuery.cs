using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Controls
{
    public class ButtonQuery : IButtonQuery
    {
        [SerializeField] protected ButtonSetup[] buttonSetup;
        protected string[] buttonQuery;

        public ButtonQuery()
        {
            if (buttonSetup == null)
                buttonSetup = new ButtonSetup[10];

            buttonQuery = new string[buttonSetup.Length];

            for (int i = 0; i < buttonQuery.Length; i++)
                buttonQuery[i] = "joystick " + buttonSetup[i].number + " button " + buttonSetup[i].key;
        }

        public string Query(int index)
        {
            return buttonQuery[index];
        }
    }
}
