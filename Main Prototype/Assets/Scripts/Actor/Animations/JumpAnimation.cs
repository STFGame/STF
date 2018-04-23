using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor.Animations
{
    [Serializable]
    public class JumpAnimation : Animation
    {
        [Header("Animation Names")]
        [SerializeField] private string jumpStartName = "IsJumping";
        [SerializeField] private string jumpFallName = "IsFalling";
        [SerializeField] private string jumpLandName = "OnGround";
        [SerializeField] private string jumpLeanName = "AerialHorizontal";
        [SerializeField] private string jumpDoubleName = "JumpIndex";

        [SerializeField] private float jumpLeanSmoothing = 0.1f;

        private bool jumpStart = false;
        private int jumpIndex = 0;

        private float crestHeight = 0f;

        public override void Init<T>(MonoBehaviour mono)
        {
            crestHeight = mono.transform.position.y;

            animator = mono.GetComponent<Animator>();
            behaviour = animator.GetBehaviour<T>();
        }

        public bool CrestReached(bool updateCrestHeight, float currentHeight, bool onGround)
        {
            if (updateCrestHeight)
                crestHeight = currentHeight;

            if (crestHeight <= currentHeight || onGround)
            {
                crestHeight = currentHeight;
                return false;
            }

            return true;
        }

        public void JumpStart(bool jumpStart)
        {
            this.jumpStart = (!this.jumpStart) ? jumpStart : this.jumpStart;

            if (this.jumpStart)
            {
                timer += Time.deltaTime;

                this.jumpStart = (timer < saveTimer);

                if (timer >= saveTimer)
                    timer = 0f;
            }

            animator.SetBool(jumpStartName, this.jumpStart);
        }

        public void JumpFall(bool jumpFall) { animator.SetBool(jumpFallName, jumpFall); }

        public void JumpLand(bool jumpLand) { animator.SetBool(jumpLandName, jumpLand); }

        public void JumpLean(float direction) { animator.SetFloat(jumpLeanName, direction); }

        public void JumpDouble(int number)
        {
            animator.SetInteger(jumpDoubleName, number);
        }
    }
}
