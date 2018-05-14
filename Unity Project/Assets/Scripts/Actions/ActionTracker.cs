using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actions
{
    /// <summary>
    /// Trackes the action inputs that the character uses
    /// </summary>
    [Serializable]
    public class ActionTracker
    {
        #region ActionTracker Variables
        //Timer that must be exceeded before input array is reset
        [SerializeField] private float timeLimit = 0.2f;

        //Timer that must be exceeded before actionID is reset
        [SerializeField] private const float idTimeLimit = 0.05f;

        //An array of character actions that is filled out in the inspector
        [SerializeField] private CharacterAction[] characterActions = null;

        //Size of the array
        [SerializeField] private const int SIZE = 10;

        //Array that stores all of the inputs
        private ActionInput[] actionInputs = new ActionInput[SIZE];
        private int currentIndex = 0;

        private ActionInput currentInput = ActionInput.None;
        private ActionInput previousInput = ActionInput.None;

        //Timer before array is reset
        private float actionTimer = 0f;

        //The ActionID reset timer
        private float idTimer = 0f;

        //The ID of the action is that is performed
        public int ActionID { get; private set; }
        #endregion

        #region Load
        //Sorts the array of CharacterActions by their priority
        public void Load()
        {
            for (int i = 0; i < characterActions.Length - 1; i++)
                for (int j = i; j < characterActions.Length; j++)
                {
                    if (characterActions[i].ActionPriority < characterActions[j].ActionPriority)
                    {
                        CharacterAction temp = characterActions[i];
                        characterActions[i] = characterActions[j];
                        characterActions[j] = temp;
                    }
                }
        }
        #endregion

        #region Updates
        public void CheckAction(ActionInput action)
        {
            currentInput = action;

            //If an action has been performed, then it is reset after a certain amount of time
            if (ActionID > 0)
                ResetID();

            //Starts the timer if an action has been inputed
            if (currentIndex > 0)
                actionTimer += Time.deltaTime;

            if (actionTimer > timeLimit)
                SearchForMatch();

            if (currentInput == previousInput && currentInput != ActionInput.None)
            {
                StoreSameInput(currentInput);
                return;
            }

            previousInput = currentInput;


            StoreNewInput(currentInput);
        }

        private void SearchForMatch()
        {
            //Gets the index of the match. -1 means no match found.
            int matchIndex = -1;

            //Initial search of the array
            StartSearch(ref matchIndex);

            //If no match is found, than it searches the array based on priority.
            if (matchIndex < 0)
                PrioritySearch(ref matchIndex);

            //If still no match is found, checks the most recent input for match.
            if (matchIndex < 0)
                EndSearch(ref matchIndex);

            //If there is a match, update the ActionID to reflect the change
            if (matchIndex >= 0)
                ActionID = characterActions[matchIndex].ActionID;

            //Reset the values
            ResetActionInput(currentInput);
            actionTimer = 0;
        }

        private void StoreSameInput(ActionInput action)
        {
            if (currentIndex - 1 >= 0)
                actionInputs[currentIndex - 1] = action;
        }

        private void StoreNewInput(ActionInput action)
        {
            //If no action is performed, then return so that it is not entered
            if (action == ActionInput.None)
                return;

            //Resets the timer if new input is entered
            actionTimer = 0;

            //Modulus on the current index so that it never overflows the array
            currentIndex = (currentIndex % SIZE);

            //enters the action into the action array and then increments the current index
            if (actionInputs[currentIndex] == ActionInput.None)
            {
                actionInputs[currentIndex++] = action;
                return;
            }

            if (currentIndex + 1 >= SIZE)
                currentIndex = 0;

            Debug.Log(currentIndex);

            actionInputs[++currentIndex] = action;
        }
        #endregion

        #region Searches
        //A basic search of the array based on size
        private void StartSearch(ref int matchIndex)
        {
            for (int i = 0; i < characterActions.Length; i++)
            {
                if (characterActions[i].Search(actionInputs, currentIndex))
                {
                    matchIndex = i;
                    break;
                }
            }
        }

        //A search that is based on priority of the action
        private void PrioritySearch(ref int matchIndex)
        {
            for (int i = 0; i < characterActions.Length; i++)
                if (characterActions[i].PrioritySearch(actionInputs, currentIndex))
                {
                    matchIndex = i;
                    break;
                }
        }

        //Searches the most recent input based on the priority of the action
        private void EndSearch(ref int matchIndex)
        {
            for (int i = 0; i < characterActions.Length; i++)
            {
                if (characterActions[i].EndSearch(actionInputs, currentIndex))
                {
                    matchIndex = i;
                    break;
                }
            }
        }

        private void TotalSearch(ref int matchIndex)
        {
            for (int i = 0; i < characterActions.Length - 1; i++)
                for (int j = i + 1; j < characterActions.Length; j++)
                {
                    characterActions[j].Search(actionInputs, currentIndex);
                }
        }
        #endregion

        #region Resets
        //Resets the ActionID when timer exceeds limit
        private void ResetID()
        {
            if (idTimer < idTimeLimit)
                idTimer += Time.deltaTime;

            idTimer = 0;
            ActionID = 0;
        }

        //Resets the actionInput array when timer is exceeded
        private void ResetActionInput(ActionInput action)
        {
            for (int i = 0; i < actionInputs.Length; i++)
            {
                if (actionInputs[i] == ActionInput.None)
                    break;
                actionInputs[i] = ActionInput.None;
            }
            currentIndex = 0;

            if (currentInput != ActionInput.None)
                actionInputs[currentIndex++] = currentInput;
        }
        #endregion
    }
}
