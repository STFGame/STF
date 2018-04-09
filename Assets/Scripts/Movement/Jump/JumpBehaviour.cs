using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor
{
    public class JumpBehaviour : StateMachineBehaviour
    {
        [HideInInspector] public ActorJump jump;

        private int jumpStartHash = Animator.StringToHash("Base Layer.Jumps.JumpStart");
        private int jumpLoopHash = Animator.StringToHash("Base Layer.Jumps.JumpLoop");
        private int jumpFallHash = Animator.StringToHash("Base Layer.Jumps.jumpFall");
        private int jumpLandHash = Animator.StringToHash("Base Layer.Jumps.JumpLand");

        public float JumpDelay { get; private set; }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.fullPathHash == jumpStartHash)
                JumpDelay = stateInfo.length / stateInfo.speed;

            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        private void State(int state)
        {
            if (state == jumpStartHash)
                ;
            else if (state == jumpLoopHash)
                ;
            else if (state == jumpFallHash)
                ;
            else if (state == jumpLandHash)
                ;
        }
    }
}
