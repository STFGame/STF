using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class InputRegistration
{
    public InputMove[] inputMove = null;

    public void Init()
    {
        for (int i = 0; i < inputMove.Length; i++)
            inputMove[i].Init();

        SortInput();
    }

    private void SortInput()
    {
        for (int i = 0; i < inputMove.Length; i++)
        {
            for (int j = i + 1; j < inputMove.Length; j++)
            {
                if (inputMove[i].moveInput.Length < inputMove[j].moveInput.Length)
                {
                    InputMove temp = inputMove[i];
                    inputMove[i] = inputMove[j];
                    inputMove[j] = temp;
                }
            }
        }

        string length = "";
        for (int i = 0; i < inputMove.Length; i++)
            length += (inputMove[i].moveInput.Length + " " + inputMove[i].ID + "  : ");
        Debug.Log(length);
    }
}
