using Actor;
using ComboSystem;
using Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    [RequireComponent(typeof(Animator))]
    public class ControlManager : MonoBehaviour
    {
        //private Device device;

        private Animator animator;
        [HideInInspector] public InputBehaviour inputBehaviour;
        [HideInInspector] public ComboBehaviour comboBehaviour;

        public Instruction up = new Instruction(InputState.Up);
        public Instruction down = new Instruction(InputState.Down);
        public Instruction right = new Instruction(InputState.Right);
        public Instruction left = new Instruction(InputState.Left);

        public Instruction action1 = new Instruction(InputState.Press);
        public Instruction action2 = new Instruction(InputState.Press);
        public Instruction action3 = new Instruction(InputState.Press);
        public Instruction action4 = new Instruction(InputState.Press);

        private bool forward = false;
        private bool backward = false;

        public bool onGround = true;

        // Use this for initialization
        private void OnEnable()
        {
            animator = GetComponent<Animator>();
            inputBehaviour = animator.GetBehaviour<InputBehaviour>();
            comboBehaviour = animator.GetBehaviour<ComboBehaviour>();

            inputBehaviour.comboManager = this;
            comboBehaviour.comboManager = this;
        }

        public void UpdateControl(Device device)
        {
            animator.SetFloat("LeftStickX", device.LeftStick.Horizontal);
            animator.SetFloat("LeftStickY", device.LeftStick.Vertical);

            animator.SetInteger("Action1", device.Action1.KeyID);
            animator.SetInteger("Action2", device.Action2.KeyID);
            animator.SetInteger("Action3", device.Action3.KeyID);
            animator.SetInteger("Action4", device.Action4.KeyID);

            animator.SetBool("Forward", forward);
            animator.SetBool("Backward", backward);

            animator.SetBool("OnGround", onGround);

            CheckInstructions(device);
            SetInputCount();

            RegisterButtons(device);
        }

        private void CheckInstructions(Device device)
        {
            if (up.StartTimer)
                up.Reset(inputBehaviour.CurrentState);
            if (down.StartTimer)
                down.Reset(inputBehaviour.CurrentState);
            if (right.StartTimer)
                right.Reset(inputBehaviour.CurrentState);
            if (left.StartTimer)
                left.Reset(inputBehaviour.CurrentState);

            if (action1.StartTimer)
                action1.Reset();
            if (action2.StartTimer)
                action2.Reset();
            if (action3.StartTimer)
                action3.Reset();
            if (action4.StartTimer)
                action4.Reset();

            forward = (transform.forward.x * device.LeftStick.Horizontal > 0f);
            backward = (transform.forward.x * device.LeftStick.Horizontal < 0f);
        }

        private void RegisterButtons(Device device)
        {
            IncrementButtons(action1, "Action1Count", device.Action1.KeyID);
            IncrementButtons(action2, "Action2Count", device.Action2.KeyID);
            IncrementButtons(action3, "Action3Count", device.Action3.KeyID);
            IncrementButtons(action4, "Action4Count", device.Action4.KeyID);
        }

        private void IncrementButtons(Instruction instruction, string name, int key)
        {
            if (key == 1)
                instruction.count++;
            animator.SetInteger(name, instruction.count);
        }

        private void SetInputCount()
        {
            animator.SetInteger("UpCount", up.count);
            animator.SetInteger("DownCount", down.count);
            animator.SetInteger("RightCount", right.count);
            animator.SetInteger("LeftCount", left.count);
        }
    }
}