using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actions
{
    [Serializable]
    public class ActionItem
    {
        [SerializeField] private string name;
        [SerializeField] private char key;
        [SerializeField] private float delay;

        public ActionItem()
        {
            name = "Input";
            key = 'A';
            delay = 0;
        }
    }
}
