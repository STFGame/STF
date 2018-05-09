using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class InputMove
{
    [SerializeField] private string moveName = "Move Name";
    [SerializeField] private int id = 0;
    [SerializeField] private InputValue[] input;

    [HideInInspector] public int[] moveInput;

    public int ID { get { return id; } }

    public void Init()
    {
        moveInput = new int[input.Length];
        for (int i = 0; i < input.Length; i++)
            moveInput[i] = (int)input[i];
    }

    public int Length { get { return input.Length; } }
}
