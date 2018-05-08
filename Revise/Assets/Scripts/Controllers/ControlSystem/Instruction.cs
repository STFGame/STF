using System;
using UnityEngine;

namespace ComboSystem
{
    public enum InputState { Neutral, Left, Right, Up, Down, Press }

    [Serializable]
    public class Instruction
    {
        [SerializeField] public int count;
        public InputState InstructionState { get; private set; }

        [SerializeField] [Range(0f, 10f)] private float resetTime = 0.5f;
        private float timer = 0f;

        public bool StartTimer { get { return count > 0; } }

        public Instruction(InputState state, int count = 0)
        {
            this.count = count;
            InstructionState = state;
        }

        #region Resets
        public void Reset()
        {
            if (count <= 0)
                return;

            timer += Time.deltaTime;

            if (timer >= resetTime)
            {
                timer = 0f;
                count = 0;
            }
        }

        public void Reset(InputState currentState)
        {
            if (count <= 0)
                return;

            timer += Time.deltaTime;

            if (timer >= resetTime)
            {
                if (InstructionState == currentState)
                {
                    count = 1;
                    timer = 0;
                    return;
                }
                timer = count = 0;
            }
        }
        #endregion
    }
}
