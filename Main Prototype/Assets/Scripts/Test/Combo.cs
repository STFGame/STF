using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tests
{
    public enum ComboInput { None, Right, Left, Up, Down, Press, Hold }
    [Serializable]
    public class Combo
    {
        [SerializeField] private ComboInput[] combo;
        private int count = 0;

        public bool Update(ComboInput[] comboInput)
        {
            if (combo.Length <= 0 || comboInput.Length <= 0 || comboInput.Length < combo.Length)
                return false;

            for (int i = 0; i < combo.Length; i++)
            {
                if (combo[i] != comboInput[i])
                    break;

                if (combo[i] == comboInput[i])
                    count++;
            }
            return (count >= combo.Length && combo != null);
        }

    }
}
