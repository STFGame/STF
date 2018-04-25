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

        private bool trigger;

        public bool Trigger
        {
            get
            {
                if (trigger)
                {
                    trigger = false;
                    return true;
                }
                return false;
            }
        }

        public int KeyID { get; private set; }

        private string query;

        public Button(string query)
        {
            this.query = query;
            KeyID = 0;
        }

        public void UpdateButton()
        {
            Press = Input.GetKeyDown(query);
            Hold = Input.GetKey(query);
            Release = Input.GetKeyUp(query);

            trigger = trigger || Press;

            SetKey();
        }

        private void SetKey()
        {
            if (Press || Trigger)
                KeyID = 1;
            else if (Hold)
                KeyID = 2;
            else if (Release)
                KeyID = 3;
            else
                KeyID = 0;
        }
    }
}
