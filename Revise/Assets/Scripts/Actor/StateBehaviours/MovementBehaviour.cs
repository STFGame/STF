using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public class MovementBehaviour : StateMachineBehaviour
    {
        public ActorMovement actorMovement;

        //Movement Hashes
        public readonly int idleHash = Animator.StringToHash("Base Layer.Movement.Idle");
        public readonly int crouchIdleHash = Animator.StringToHash("Base Layer.Movement.Crouch.CrouchIdle");
        public readonly int crouchWalkHash = Animator.StringToHash("Base Layer.Movement.Crouch.CrouchWalk");
        public readonly int locomotionHash = Animator.StringToHash("Base Layer.Movement.LocomotionTree");
        public readonly int turnHash = Animator.StringToHash("Base Layer.Movement.Turns.Turn");
        public readonly int dashSlideHash = Animator.StringToHash("Base Layer.Movement.Dash.DashSlide");
        public readonly int dashTurnHash = Animator.StringToHash("Base Layer.Movement.Dash.DashTurn");
        public readonly int dashStartHash = Animator.StringToHash("Base Layer.Movement.Dash.DashStart");
        public readonly int dashHash = Animator.StringToHash("Base Layer.Movement.Dash.Dash");

        //Jump Hashes
        public readonly int jumpStartHash = Animator.StringToHash("Base Layer.Movement.Jumps.JumpStart");
        public readonly int jumpLoopHash = Animator.StringToHash("Base Layer.Movement.Jumps.JumpLoopTree");
        public readonly int jumpFallHash = Animator.StringToHash("Base Layer.Movement.Jumps.JumpFallTree");
        public readonly int jumpLandHash = Animator.StringToHash("Base Layer.Movement.Jumps.JumpLand");
        public readonly int doubleJumpHash = Animator.StringToHash("Base Layer.Movement.Jumps.DoubleJump");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            MovementState(stateInfo.fullPathHash);
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //if (actorMovement.IsJumping)
                //animator.Play(jumpStartHash);

            base.OnStateUpdate(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        private void MovementState(int hash)
        {
            if (idleHash == hash)
                Debug.Log("Idle");
            else if (crouchIdleHash == hash)
                Debug.Log("Crouch Idle");
            else if (crouchWalkHash == hash)
                Debug.Log("Crouch Walk");
            else if (locomotionHash == hash)
                Debug.Log("Locomotion");
            else if (dashStartHash == hash)
                Debug.Log("Dash Start");
            else if (dashSlideHash == hash)
                Debug.Log("Dash Slide");
            else if (dashTurnHash == hash)
                Debug.Log("Dash Turn");
            else if (dashHash == hash)
                Debug.Log("Dash");
            else if (jumpStartHash == hash)
                Debug.Log("Jump Start");
            else if (jumpLoopHash == hash)
                Debug.Log("Jump Loop");
            else if (jumpFallHash == hash)
                Debug.Log("Jump Fall");
            else if (jumpLandHash == hash)
                Debug.Log("Jump Land");
        }
    }
}