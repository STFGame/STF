using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    /// <summary>
    /// Class that helps keep track of joystick movement to determine whether a character is doing different actions </para>
    /// such as dashes, crouching, etc.
    /// </summary>
    [Serializable]
    public class ActionRange
    {
        #region ActionRange Variables
        public enum Axis { X, Y, Z }

        public enum AxisValue { Positive, Negative, Absolute }

        //Which axis the joystick should go along
        [SerializeField] private Axis axis = Axis.X;

        //What value the joystick should move along
        [SerializeField] private AxisValue axisValue = AxisValue.Positive;

        //The start of the timer
        [SerializeField] [Range(0f, 1f)] private float minRange = 0f;

        //The end of the timer
        [SerializeField] [Range(0f, 1f)] private float maxRange = 0f;

        //Value the timer must be less than
        [SerializeField] [Range(0f, 1f)] private float timeLimit = 0f;

        //Boolean for using time
        [SerializeField] private bool useTime = true;

        private float actionTimer = 0f;

        private bool previousSuccess = false;
        #endregion

        #region Methods
        public bool ActionSuccess(Vector3 direction)
        {
            if (direction == Vector3.zero)
                actionTimer = 0;

            bool success = false;

            float axisDirection = GetDirection(direction);

            SuccessByAxisValue(axisDirection, ref success);

            if (previousSuccess)
                return true;

            return previousSuccess = success;
        }

        public void Reset()
        {
            actionTimer = 0f;
            previousSuccess = false;
        }

        //Returns the axis that is supposed to be checked.
        private float GetDirection(Vector3 direction)
        {
            if (axis == Axis.X)
                return direction.x;
            else if (axis == Axis.Y)
                return direction.y;
            else
                return direction.z;
        }

        //Sets the timer up to the appropriate axis value.
        private void SuccessByAxisValue(float direction, ref bool success)
        {
            if (direction < maxRange)
                previousSuccess = false;

            if (axisValue == AxisValue.Positive)
                success = Successful(maxRange, minRange, direction);
            else if (axisValue == AxisValue.Negative)
                success = Successful(maxRange, minRange, direction * -1f);
            else
                success = Successful(maxRange, minRange, Mathf.Abs(direction));
        }

        //Method that determines whether the action was successful
        private bool Successful(float max, float min, float direction)
        {
            if (direction > min && direction < max && actionTimer <= timeLimit && useTime)
                actionTimer += Time.deltaTime;

            return ((direction > maxRange) && (actionTimer < timeLimit || !useTime));
        }
        #endregion
    }
}
