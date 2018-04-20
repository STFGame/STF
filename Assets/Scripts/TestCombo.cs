using Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Combos
{
    [Serializable]
    public class TestCombo
    {
        [SerializeField] private ComboInput[] comboInputs;
        public Combo combo;

        public void Init()
        {
            combo = new Combo(comboInputs);
        }
    }
}
