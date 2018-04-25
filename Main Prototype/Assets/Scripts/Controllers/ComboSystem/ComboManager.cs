using Actor;
using ComboSystem;
using Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combos
{
    public class ComboManager : MonoBehaviour
    {
        private Device device;

        private Animator animator;
        [HideInInspector] public InputBehaviour inputBehaviour;
        [HideInInspector] public ComboBehaviour comboBehaviour;

        public Instruction up = new Instruction(InputState.Up);
        public Instruction down = new Instruction(InputState.Down);
        public Instruction right = new Instruction(InputState.Right);
        public Instruction left = new Instruction(InputState.Left);

        public bool onGround = true;

        // Use this for initialization
        private void OnEnable()
        {
            device = GetComponentInParent<ActorControl>().device;

            animator = GetComponent<Animator>();
            inputBehaviour = animator.GetBehaviour<InputBehaviour>();
            comboBehaviour = animator.GetBehaviour<ComboBehaviour>();

            inputBehaviour.comboManager = this;
            comboBehaviour.comboManager = this;
        }

        void Update()
        {
            animator.SetFloat("LeftStickX", device.LeftStick.Horizontal);
            animator.SetFloat("LeftStickY", device.LeftStick.Vertical);

            animator.SetInteger("Action1", device.Action1.KeyID);
            animator.SetInteger("Action2", device.Action2.KeyID);
            animator.SetInteger("Action3", device.Action3.KeyID);
            animator.SetInteger("Action4", device.Action4.KeyID);

            animator.SetBool("OnGround", onGround);

            CheckInstructions();
            SetInputCount();
        }

        private void CheckInstructions()
        {
            if (up.StartTimer)
                up.Reset(inputBehaviour.CurrentState);
            if (down.StartTimer)
                down.Reset(inputBehaviour.CurrentState);
            if (right.StartTimer)
                right.Reset(inputBehaviour.CurrentState);
            if (left.StartTimer)
                left.Reset(inputBehaviour.CurrentState);
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