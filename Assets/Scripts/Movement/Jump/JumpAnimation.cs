using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Actor.Animation
{
    [Serializable]
    public class JumpAnimation : IAnimation
    {
        [SerializeField] private string jumpName = "HasJumped";
        [SerializeField] private string fallName = "OnFall";
        [SerializeField] private string landName = "OnGround";

        public Animator Animator { get; private set; }
        public JumpBehaviour Behaviour { get; private set; }

        private Animator animator;
        private JumpBehaviour behaviour;

        public void Init(ActorJump jump)
        {
            animator = jump.GetComponent<Animator>();
            behaviour = animator.GetBehaviour<JumpBehaviour>();

            if (behaviour != null)
                behaviour.jump = jump;
        }

        public void SetJump(bool value) { animator.SetBool(jumpName, value); }

        public void SetFall(bool value) { animator.SetBool(fallName, value); }

        public void SetLand(bool value) { animator.SetBool(landName, value); }
    }
}
