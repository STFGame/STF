using Actor.Combos;
using STF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor
{
    public class Test : MonoBehaviour
    {
        public TestCombo te = new TestCombo();

        private Joystick joystick = new Joystick();

        private void Awake()
        {
            te.Init();
        }

        private void Update()
        {
            if(te.combo.CheckCombo2())
            {
                Debug.Log("BASH");
            }
        }
    }
}
