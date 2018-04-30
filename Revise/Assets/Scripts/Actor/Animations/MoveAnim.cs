using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public class MoveAnim
    {
        [Header("Parameter Names")]
        [SerializeField] private string absoluteHorizontalName = "AbsoluteHorizontal";
        [SerializeField] private string verticalName = "Vertical";
        [SerializeField] private string dashName = "IsDashing";
        [SerializeField] private string crouchName = "IsCrouching";
        [SerializeField] private string turnName = "IsTurning";
        [SerializeField] private string jumpLandName = "OnGround";
        [SerializeField] private string horizontalName = "Horizontal";
        [SerializeField] private string jumpName = "IsJumping";
        [SerializeField] private string jumpFallName = "IsFalling";
        [SerializeField] private string jumpIndexName = "JumpIndex";

        private int absoluteHorizontalId = 0;
        private int dashId = 0;
        private int crouchId = 0;
        private int turnId = 0;
        private int jumpLandId = 0;
        private int horizontalId = 0;
        private int jumpId = 0;
        private int jumpFallId = 0;
        private int jumpIndexId = 0;
        private int verticalId = 0;

        [Header("Smoothing")]
        [SerializeField] private float movementSmoothing = 0.2f;
        [SerializeField] private float crouchSmoothing = 0.2f;
        [SerializeField] private float jumpLeanSmoothing = 0.2f;

        private Animator animator;
        private MovementBehaviour movementBehaviour;

        public void Init(ActorMovement actorMovement)
        {
            absoluteHorizontalId = Animator.StringToHash(absoluteHorizontalName);
            dashId = Animator.StringToHash(dashName);
            crouchId = Animator.StringToHash(crouchName);
            turnId = Animator.StringToHash(turnName);
            jumpLandId = Animator.StringToHash(jumpLandName);
            horizontalId = Animator.StringToHash(horizontalName);
            jumpId = Animator.StringToHash(jumpName);
            jumpFallId = Animator.StringToHash(jumpFallName);
            jumpIndexId = Animator.StringToHash(jumpIndexName);
            verticalId = Animator.StringToHash(verticalName);

            animator = actorMovement.GetComponent<Animator>();
            movementBehaviour = animator.GetBehaviour<MovementBehaviour>();

            if (movementBehaviour != null)
                movementBehaviour.actorMovement = actorMovement;

        }

        #region Movement Animations
        public void PlayHorizontalAnim(Vector3 direction)
        {
            float absoluteHorizontal = Mathf.Abs(direction.x);

            animator.SetFloat(absoluteHorizontalId, absoluteHorizontal, movementSmoothing, Time.deltaTime);
            animator.SetFloat(verticalId, direction.y, crouchSmoothing, Time.deltaTime);
        }

        public void PlayCrouchAnim(bool isCrouching) { animator.SetBool(crouchId, isCrouching); }

        public void PlayTurnAnim(bool isTurning) { animator.SetBool(turnId, isTurning); }

        public void PlayDashAnim(bool isDashing) { animator.SetBool(dashId, isDashing); }
        #endregion

        #region Jump Animations
        public void PlayJumpAnim(bool isJumping) { animator.SetBool(jumpId, isJumping); }

        public void PlayFallAnim(bool isFalling) { animator.SetBool(jumpFallId, isFalling); }

        public void PlayLandAnim(bool isGrounded) { animator.SetBool(jumpLandId, isGrounded); }

        public void PlayJumpLeanAnim(Vector3 direction)
        {
            animator.SetFloat(horizontalId, direction.x, jumpLeanSmoothing, Time.deltaTime);
            animator.SetFloat(horizontalId, direction.y, jumpLeanSmoothing, Time.deltaTime);
        }

        public void PlayJumpIndexAnim(int jumpIndex) { animator.SetInteger(jumpIndexId, jumpIndex); }
        #endregion
    }
}