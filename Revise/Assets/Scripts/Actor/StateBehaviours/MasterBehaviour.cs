using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Actor.StateBehaviours
{
    public class MasterBehaviour : StateMachineBehaviour
    {
        private StateMachine currentState;
        private StateMachine previousState;

        public Action<StateMachine> StateEvent;

        #region Movement States
        public readonly int idleHash = Animator.StringToHash("Base Layer.Master State.Movement.Idle");
        public readonly int locomotionHash = Animator.StringToHash("Base Layer.Master State.Movement.LocomotionTree");
        public readonly int crouchIdleHash = Animator.StringToHash("Base Layer.Master State.Movement.Crouch.CrouchIdle");
        public readonly int crouchForwardWalkHash = Animator.StringToHash("Base Layer.Master State.Movement.Crouch.CrouchWalkForward");
        public readonly int crouchWalkBackHash = Animator.StringToHash("Base Layer.Master State.Movement.Crouch.CrouchWalkBack");
        public readonly int dashStartHash = Animator.StringToHash("Base Layer.Master State.Movement.Dash.DashStart");
        public readonly int dashHash = Animator.StringToHash("Base Layer.Master State.Movement.Dash.Dash");
        public readonly int dashSlideHash = Animator.StringToHash("Base Layer.Master State.Movement.Dash.DashSlide");
        public readonly int dashStartTwoHash = Animator.StringToHash("Base Layer.Master State.Movement.Dash.DashStartTwo");
        public readonly int jumpStartHash = Animator.StringToHash("Base Layer.Master State.Movement.Jumps.JumpStart");
        public readonly int jumpLoopTreeHash = Animator.StringToHash("Base Layer.Master State.Movement.Jumps.JumpLoopTree");
        public readonly int jumpFallTreeHash = Animator.StringToHash("Base Layer.Master State.Movement.Jumps.JumpFallTree");
        public readonly int jumpLandHash = Animator.StringToHash("Base Layer.Master State.Movement.Jumps.JumpLand");
        #endregion

        #region Survival States
        public readonly int blockHash = Animator.StringToHash("Base Layer.Master State.Survival.Block");
        public readonly int rollForwardHash = Animator.StringToHash("Base Layer.Master State.Survival.RollForward");
        public readonly int rollBackHash = Animator.StringToHash("Base Layer.Master State.Survival.RollBackwards");
        public readonly int spotDodgeHash = Animator.StringToHash("Base Layer.Master State.Survival.Dodge");
        #endregion

        #region Combat States

        #endregion

        #region Disable States
        public readonly int stunHash = Animator.StringToHash("Base Layer.Master State.Disables.Stun");

        #endregion

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateState(stateInfo.fullPathHash);

            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        private void UpdateState(int hash)
        {
            if (hash == idleHash)
                currentState = StateMachine.Idle;
            else if (hash == locomotionHash)
                currentState = StateMachine.Run;
            else if (hash == dashHash || hash == dashSlideHash ||
                     hash == dashStartHash || hash == dashStartTwoHash)
                currentState = StateMachine.Dash;
            else if (hash == jumpStartHash || hash == jumpLoopTreeHash)
                currentState = StateMachine.Jump;
            else if (hash == jumpLandHash)
                currentState = StateMachine.Land;
            else if (hash == jumpFallTreeHash)
                currentState = StateMachine.Fall;
            else if (hash == blockHash)
                currentState = StateMachine.Block;
            else if (hash == rollForwardHash || hash == rollBackHash)
                currentState = StateMachine.Roll;
            else if (hash == spotDodgeHash)
                currentState = StateMachine.Dodge;
            else if (hash == stunHash)
                currentState = StateMachine.Stun;

            if (currentState == previousState)
                return;

            previousState = currentState;

            if (StateEvent != null)
                StateEvent(currentState);
        }
    }
}