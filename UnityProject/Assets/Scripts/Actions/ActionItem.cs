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
        [SerializeField] private string name = "Generic Action";
        [SerializeField] private ActionInput key = ActionInput.None;

        public ActionInput Key { get { return key; } }
    }
}
