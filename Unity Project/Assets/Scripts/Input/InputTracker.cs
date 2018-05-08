using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum InputValue
{
    Neutral = 0,
    Up = 10,
    Down = 101,
    Right = 1001,
    Left = 1011,

    Forward,
    Backwards,

    Action1 = 6,
    Action2 = 22,
    Action3 = 222,
    Action4 = 2222
}

[Serializable]
public class InputTracker
{
    [SerializeField] private float resetTime = 0.05f;
    private float timer = 0f;

    private float delayLength = 0.03f;
    private float delayTimer = 0f;

    #region Inputs
    private int[] buffer = new int[60];
    private int currentIndex = 0;

    private int currentDirection;
    private int previousDirection;

    private const int NEUTRAL = 0;
    private const int UP = 10;
    private const int DOWN = 101;
    private const int RIGHT = 1001;
    private const int LEFT = 10011;

    private const int FORWARD = 10111;
    private const int BACKWARDS = 10231;

    private const int ACTION1 = 6;
    private const int ACTION2 = 22;
    private const int ACTION3 = 222;
    private const int ACTION4 = 2222;
    #endregion

    public void UpdateBuffer(Device device)
    {
        currentIndex = (currentIndex >= buffer.Length) ? 0 : currentIndex;

        UpdateButton(device);

        UpdateDirection(device);

        ResetBuffer();
    }

    #region Match Check
    public void CheckMatch(int[] move)
    {
        if (currentIndex != move.Length)
            return;

        int count = 0;
        for (int i = 0; i < currentIndex; i++)
        {
            if (buffer[i] == move[i])
            {
                count++;
                continue;
            }
            break;
        }

        if (count == move.Length)
            Debug.Log("Match");
    }

    public int CheckMatch(InputMove[] move)
    {
        if (currentIndex == 0)
        {
            delayTimer = 0;
            return 0;
        }

        delayTimer += Time.deltaTime;

        if (delayTimer < delayLength)
            return 0;

        int matches = 0;
        for (int i = 0; i < move.Length; i++)
        {
            if (move[i].moveInput.Length != currentIndex)
                continue;

            int[] m = move[i].moveInput;

            bool broke = false;
            for (int j = 0; j < m.Length; j++)
            {
                if(m[j] != buffer[j])
                {
                    matches = 0;
                    broke = true;
                    break;
                }
                matches++;
            }

            if (broke)
                continue;

            if (matches == m.Length)
                return move[i].ID;
        }

        delayTimer = 0;

        return 0;
    }
    #endregion

    private void ResetBuffer()
    {
        if (buffer[0] == 0)
            return;

        timer += Time.deltaTime;

        if (timer < resetTime)
            return;

        int[] temp = new int[buffer.Length];
        for (int i = 1; i < buffer.Length; i++)
            temp[i - 1] = buffer[i];

        buffer = temp;

        currentIndex = 0;

        if (currentDirection != NEUTRAL)
            buffer[currentIndex++] = currentDirection;

        timer = 0f;
    }

    private void UpdateDirection(Device device)
    {
        int direction = 0;
        if (device.LeftStick.Up)
            direction = UP;
        else if (device.LeftStick.Down)
            direction = DOWN;
        else if (device.LeftStick.Right)
            direction = RIGHT;
        else if (device.LeftStick.Left)
            direction = LEFT;
        else
            direction = NEUTRAL;

        currentDirection = direction;

        if (currentDirection == previousDirection)
            return;

        previousDirection = currentDirection;

        if (currentDirection == NEUTRAL)
            return;

        buffer[currentIndex++] = currentDirection;

        string bufferString = "";
        for (int i = 0; i < currentIndex; i++)
            bufferString += buffer[i] + " ";
        Debug.Log(bufferString);
    }

    private void UpdateButton(Device device)
    {
        int button = 0;
        if (device.Action1.Press)
            button = ACTION1;
        else if (device.Action2.Press)
            button = ACTION2;
        else if (device.Action3.Press)
            button = ACTION3;
        else if (device.Action4.Press)
            button = ACTION4;

        if (button == 0)
            return;

        buffer[currentIndex++] = button;

        string bufferString = "";
        for (int i = 0; i < currentIndex; i++)
            bufferString += buffer[i] + " ";
    }
}
