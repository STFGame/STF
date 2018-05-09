using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actions
{
    [Serializable]
    public class CharacterAction
    {
        [SerializeField] private string actionName;
        [SerializeField] private int actionID;
        [SerializeField] private int actionPriority;
        [SerializeField] private ActionItem[] action = null;

        public CharacterAction()
        {
            actionID = 0;
            actionPriority = 0;
            actionName = "Generic Action";
        }

        public int CheckAction(ActionItem[] action)
        {
            int id = 0;

            return id;
        }
    }
}
