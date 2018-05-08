using ComboSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class InputBehaviour : StateMachineBehaviour
    {
        public InputState CurrentState { get; private set; }

        public ControlManager comboManager;

        #region Hashes
        private int neutralHash = Animator.StringToHash("Base Layer.Neutral");
        private int upHash = Animator.StringToHash("Base Layer.Joystick.Up");
        private int downHash = Animator.StringToHash("Base Layer.Joystick.Down");
        private int rightHash = Animator.StringToHash("Base Layer.Joystick.Right");
        private int leftHash = Animator.StringToHash("Base Layer.Joystick.Left");
        #endregion

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            SetCurrentState(stateInfo.fullPathHash);

            Increment(CurrentState);

            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        private void Increment(InputState currentState)
        {
            switch (currentState)
            {
                case InputState.Right:
                    comboManager.right.count++;
                    break;
                case InputState.Left:
                    comboManager.left.count++;
                    break;
                case InputState.Up:
                    comboManager.up.count++;
                    break;
                case InputState.Down:
                    comboManager.down.count++;
                    break;
                default:
                    break;
            }
        }

        private void SetCurrentState(int hash)
        {
            if (hash == rightHash)
                CurrentState = InputState.Right;
            else if (hash == leftHash)
                CurrentState = InputState.Left;
            else if (hash == upHash)
                CurrentState = InputState.Up;
            else if (hash == downHash)
                CurrentState = InputState.Down;
            else
                CurrentState = InputState.Neutral;
        }

    }
}