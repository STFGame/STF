using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character.Instrument
{
    public class Buffer
    {
        private int[] buffer;
        private int index = 0;
        private float timer = 0;
        private const int EMPTY = -1;


        public Buffer()
        {
            buffer = new int[4];
            EmptyBuffer();
        }

        public Buffer(int size)
        {
            buffer = new int[size];
            EmptyBuffer();
        }

        private void EmptyBuffer()
        {
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = EMPTY;
        }

        public void Commands(int val)
        {
            if (index > buffer.Length - 1)
                index = 0;
            if (HoldLength())
                EmptyBuffer();

            Push(val);
            index++;
        }

        private void Push(int val)
        {
            for (int i = buffer.Length - 1; i > 0; i--)
                buffer[i] = buffer[i - 1];
            buffer[0] = val;
        }

        public override string ToString()
        {
            string buff = "";
            for (int i = 0; i < buffer.Length; i++)
                buff += buffer[i] + " ";
            return buff;
        }

        private bool HoldLength()
        {
            if (buffer == null)
            {
                timer = 0;
                return false;
            }

            timer += Time.deltaTime;
            if (timer > 2.0f)
                return true;
            return false;
        }
    }
}
