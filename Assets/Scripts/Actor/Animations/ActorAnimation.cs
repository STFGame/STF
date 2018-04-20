using Actor.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actor
{
    [RequireComponent(typeof(Animator))]
    public class ActorAnimation : MonoBehaviour
    {
        private Animator animator;

        [SerializeField] private JumpAnimation jump = new JumpAnimation();
        [SerializeField] private BlockAnimation block = new BlockAnimation();
        [SerializeField] private MovementAnimation move = new MovementAnimation();

        private void Awake()
        {
            animator = GetComponent<Animator>();

            if (GetComponent<ActorMovement>() != null)
            {
                jump.Init<MovementBehaviour>(GetComponent<ActorMovement>());
                move.Init<MovementBehaviour>(GetComponent<ActorMovement>());
            }
        }

        public void PlayJumpAnim(bool startJump, bool isFalling, bool onGround, float lean)
        {
            jump.JumpStart(startJump);
            jump.JumpFall(isFalling);
            jump.JumpLand(onGround);
            jump.JumpLean(lean);
        }

        public void PlayBlockAnim(bool isBlocking, bool isStunned)
        {
            block.Block(isBlocking);
            block.Stun(isStunned);
        }

        public void PlayMoveAnim(float horizontal, float vertical)
        {
            move.Move(horizontal);
            move.Crouch(vertical);
        }

        private void OnDrawGizmos() { }
    }
}
