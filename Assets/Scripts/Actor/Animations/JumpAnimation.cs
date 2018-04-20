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
        [SerializeField] private string jumpStartName;
        [SerializeField] private string jumpFallName;
        [SerializeField] private string jumpLandName;
        [SerializeField] private string jumpLeanName;
        [SerializeField] private string jumpMultipleName;

        [SerializeField] private float jumpLeanSmoothing;

        [Tooltip("Reset timer for force jump")]
        [SerializeField] private float timerReset = 0.5f;

        private float timer = 0f;
        private bool force = false;

        public override void Init<T>(MonoBehaviour mono)
        {
            animator = mono.GetComponent<Animator>();
            behaviour = animator.GetBehaviour<T>();
        }

        public void JumpStart(bool jumpStart){ animator.SetBool(jumpStartName, jumpStart); }

        public void JumpStartForce(bool jumpStart)
        {
            force = (!force) ? jumpStart : force;

            if (force)
                timer += Time.deltaTime;

            if (timer >= timerReset)
            {
                force = false;
                timer = 0f;
            }

            animator.SetBool(jumpStartName, force);
        }

        public void JumpFall(bool jumpFall) { animator.SetBool(jumpFallName, jumpFall); }

        public void JumpLand(bool jumpLand) { animator.SetBool(jumpLandName, jumpLand); }

        public void JumpLean(float direction) { animator.SetFloat(jumpLeanName, direction); }

        public void JumpMultiple(int number) { animator.SetInteger(jumpMultipleName, number); }
    }
}
