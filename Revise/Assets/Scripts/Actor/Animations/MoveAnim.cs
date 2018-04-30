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
        [SerializeField] private string horizontalName = "Horizontal";
        [SerializeField] private string dashingName = "IsDashing";
        [SerializeField] private string crouchName = "IsCrouching";
        [SerializeField] private string turnName = "IsTurning";
        [SerializeField] private string jumpLandName = "OnGround";
        [SerializeField] private string aerialHorizontalName = "AerialHorizontal";
        [SerializeField] private string jumpName = "IsJumping";
        [SerializeField] private string jumpFallName = "IsFalling";
        [SerializeField] private string jumpIndexName = "JumpIndex";

        private int horizontalId = 0;
        //private int dashingName = "IsDashing";
        //private string crouchName = "IsCrouching";
        //private string turnName = "IsTurning";
        //private string jumpLandName = "OnGround";
        //private string aerialHorizontalName = "AerialHorizontal";
        //private string jumpName = "IsJumping";
        //private string jumpFallName = "IsFalling";
        //private string jumpIndexName = "JumpIndex";

        [Header("Smoothing")]
        [SerializeField] private float movementSmoothing = 0.2f;
        [SerializeField] private float jumpLeanSmoothing = 0.2f;

        private Animator animator;
        private MovementBehaviour movementBehaviour;

        public void Init(ActorMovement actorMovement)
        {
            horizontalId = Animator.StringToHash(horizontalName);

            animator = actorMovement.GetComponent<Animator>();
            movementBehaviour = animator.GetBehaviour<MovementBehaviour>();

            if (movementBehaviour != null)
                movementBehaviour.actorMovement = actorMovement;

        }

        #region Movement Animations
        public void PlayHorizontalAnim(Vector3 direction)
        {
            animator.SetFloat(horizontalId, direction.x, movementSmoothing, Time.deltaTime);
            //animator.SetFloat(horizontalId, direction.y, movementSmoothing, Time.deltaTime);
        }

        public void PlayCrouchAnim(bool isCrouching) { animator.SetBool(crouchName, isCrouching); }

        public void PlayTurnAnim(bool isTurning) { animator.SetBool(turnName, isTurning); }

        public void PlayDashAnim(bool isDashing) { animator.SetBool(dashingName, isDashing); }
        #endregion

        #region Jump Animations
        public void PlayJumpAnim(bool isJumping) { animator.SetBool(jumpName, isJumping); }

        public void PlayFallAnim(bool isFalling) { animator.SetBool(jumpFallName, isFalling); }

        public void PlayLandAnim(bool isGrounded) { animator.SetBool(jumpLandName, isGrounded); }

        public void PlayJumpLeanAnim(Vector3 direction)
        {
            animator.SetFloat(aerialHorizontalName, direction.x, jumpLeanSmoothing, Time.deltaTime);
            animator.SetFloat(aerialHorizontalName, direction.y, jumpLeanSmoothing, Time.deltaTime);
        }

        public void PlayJumpIndexAnim(int jumpIndex) { animator.SetInteger(jumpIndexName, jumpIndex); }
        #endregion
    }
}