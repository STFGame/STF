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
        [SerializeField] private AttackAnimation attack = new AttackAnimation();

        private ActorCombat actorCombat;
        private ActorMovement actorMovement;
        private ActorControl actorControl;
        private ActorSurvival actorSurvival;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            if (GetComponent<ActorMovement>() != null)
            {
                jump.Init<MovementBehaviour>(GetComponent<ActorMovement>());
                move.Init<MovementBehaviour>(GetComponent<ActorMovement>());
            }

            if (GetComponent<ActorCombat>() != null)
                attack.Init<StateMachineBehaviour>(this);

            actorCombat = GetComponent<ActorCombat>();
            actorMovement = GetComponent<ActorMovement>();
            actorControl = GetComponent<ActorControl>();
            actorSurvival = GetComponent<ActorSurvival>();
        }

        private void Update()
        {
            bool onGround = actorMovement.onGround;
            bool isFalling = jump.CrestReached(actorControl.device.RightBumper.Press, transform.position.y, onGround);

            PlayJumpAnim(actorControl.device.RightBumper.Press, isFalling, onGround, actorControl.device.LeftStick.Horizontal);

            PlayMoveAnim(actorControl.device.LeftStick.Horizontal, actorControl.device.LeftStick.Vertical);

            PlayAttackAnim(0);
        }

        public void PlayJumpAnim(bool startJump, bool isFalling, bool onGround, float lean)
        {
            float jLean = lean * transform.forward.x;

            jump.JumpStart(startJump);
            jump.JumpFall(isFalling);
            jump.JumpLand(onGround);
            jump.JumpDouble(actorMovement.JumpCount);
            jump.JumpLean(jLean);
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

        public void PlayAttackAnim(int attackNumber)
        {
            attack.PlayAttack(actorCombat.AttackNumber);
        }

        private void OnDrawGizmos() { }
    }
}
