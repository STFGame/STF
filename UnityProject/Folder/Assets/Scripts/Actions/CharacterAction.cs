using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actions
{
    /// <summary>
    /// Creates an action based on input that a character can perform.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "Character Action", menuName = "Action", order = 5)]
    public class CharacterAction : ScriptableObject
    {
        #region CharacterAction Variables
        [SerializeField] private int actionPriority = 0;
        [SerializeField] private int actionID = 0;
        public ActionItem[] action = null;

        public int ActionPriority { get { return actionPriority; } }
        public int ActionID { get { return actionID; } }
        #endregion

        #region Searches
        public bool Search(ActionInput[] action, int currentIndex)
        {
            //Checks to see if the currentIndex is equal to the action length,
            //if it isn't then no match is possible
            if (currentIndex != this.action.Length)
                return false;

            int count = 0;
            for (int i = 0; i < currentIndex; i++)
            {
                if (action[i] != this.action[i].Key)
                    return false;
                count++;
            }

            return count == this.action.Length;
        }

        public bool PrioritySearch(ActionInput[] action, int currentIndex)
        {
            //Checks to see if the value is less then the action length
            // because if it is, then a match is possible
            if (currentIndex < this.action.Length)
                return false;

            int count = 0;
            for (int i = 0; i < this.action.Length; i++)
            {
                if (this.action[i].Key == action[i])
                    count++;
            }
            return (count == this.action.Length);
        }

        public bool EndSearch(ActionInput[] action, int currentIndex)
        {
            //Checks the end of the array for to see if the most recent input 
            //is an action
            int startIndex = 0;
            if (this.action.Length < currentIndex)
                startIndex = currentIndex - this.action.Length;
            else
                return false;

            int count = 0;
            for (int i = 0; i < this.action.Length; i++)
            {
                if (this.action[i].Key == action[startIndex])
                    count++;
                startIndex++;
            }

            return (count == this.action.Length);
        }
        #endregion
    }
}
