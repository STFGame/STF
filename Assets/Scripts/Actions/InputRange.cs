using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class InputRange
{
    public enum Axis { X, Y, Z }
    public enum AxisDirection { Positive, Negative}

    [SerializeField] private Axis axis = Axis.X;
    [SerializeField] private AxisDirection axisDirection = AxisDirection.Negative;
    [SerializeField] [Range(-1f, 1f)] private float maximumRange = 1f;
    [SerializeField] [Range(-1f, 1f)] private float minimumRange = -1f;
    [SerializeField] [Range(0f, 1f)] private float endRange = 0.5f;

    private float inputTimer = 0f;

    public bool Succeeded(float direction)
    {
        bool successful = false;

        if (axis == Axis.X)
            CheckX(direction, ref successful);

        return successful;
    }

    public bool Succeeded(bool value)
    {
        if(value)
        {
            inputTimer += Time.deltaTime;

            if (inputTimer > maximumRange)
                return false;
            return true;
        }
        inputTimer = 0f;
        return false;
    }

    private void CheckX(float direction, ref bool successful)
    {
        if(axisDirection == AxisDirection.Positive)
        {
            direction = Mathf.Abs(direction);
            successful = InRangePositive(direction);
        }
        else if(axisDirection == AxisDirection.Negative)
        {
            if (direction > 0f)
                return;
            successful = InRangeNegative(direction);
        }
    }

    private bool InRangePositive(float direction)
    {
        if (direction == 0f)
            inputTimer = 0f;

        if (direction > minimumRange && direction < maximumRange)
            inputTimer += Time.deltaTime;

        return (direction > maximumRange && inputTimer < endRange);
    }

    private bool InRangeNegative(float direction)
    {
        if (direction == 0f)
            inputTimer = 0f;

        if (direction < minimumRange && direction > maximumRange)
            inputTimer += Time.deltaTime;

        return (direction < maximumRange && inputTimer < endRange);
    }
}
