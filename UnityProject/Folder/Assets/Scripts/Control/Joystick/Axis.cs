using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Controls
{
    public class Axis
    {
        private string axisName;

        public float Value { get; private set; }

        public float AbsValue { get; private set; }

        public int AxisID { get; private set; }

        public Axis(string axisName, int playerNumber, int axisID)
        {
            char[] split = new char[] { ' ' };

            string[] temp = axisName.Split(split);

            if (temp.Length > 3)
            {
                temp[1] = playerNumber.ToString();

                axisName = "";
                for (int i = 0; i < temp.Length; i++)
                    axisName += temp[i] + " ";
                axisName = axisName.TrimEnd();
            }

            this.axisName = axisName;
        }

        public void UpdateAxis()
        {
            Value = Input.GetAxis(axisName);

            AbsValue = Mathf.Abs(Value);
        }
    }
}
