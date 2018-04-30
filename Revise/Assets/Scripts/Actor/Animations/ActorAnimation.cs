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
        [SerializeField] private HitAnimation hit = new HitAnimation();

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

            hit.Init<StateMachineBehaviour>(GetComponent<ActorSurvival>());
            block.Init<StateMachineBehaviour>(GetComponent<ActorSurvival>());

            if (GetComponent<ActorCombat>() != null)
                attack.Init<StateMachineBehaviour>(this);

            actorCombat = GetComponent<ActorCombat>();
            actorMovement = GetComponent<ActorMovement>();
            actorControl = GetComponent<ActorControl>();
            actorSurvival = GetComponent<ActorSurvival>();
        }

        private void Update()
        {
            bool onGround = !actorMovement.InverseOnGround();
            bool isFalling = jump.CrestReached(actorControl.device.RightBumper.Press, transform.position.y, onGround);
            bool jumpCommand = actorControl.device.LeftBumper.Press;
            Vector2 movement = new Vector2(actorControl.device.LeftStick.Horizontal, actorControl.device.LeftStick.Vertical);
            int attackIndex = actorCombat.AttackIndex;
            int hitIndex = actorSurvival.HitIndex;

            PlayJumpAnim(jumpCommand, isFalling, onGround, movement.x);
            PlayMoveAnim(movement.x, movement.y, actorMovement.IsDashing);
            PlayBlockAnim(actorControl.device.RightBumper.Hold, actorSurvival.IsStunned);
            PlayAttackAnim(attackIndex);
            PlayHitAnim(hitIndex);
        }

        public void PlayJumpAnim(bool startJump, bool isFalling, bool onGround, float lean)
        {
            float jumpLean = lean * transform.forward.x;

            jump.JumpStart(startJump);
            jump.JumpFall(isFalling);
            jump.JumpLand(onGround);
            jump.JumpDouble(actorMovement.JumpCount);
            jump.JumpLean(jumpLean);
        }

        public void PlayBlockAnim(bool isBlocking, bool isStunned)
        {
            block.Block(isBlocking);
            block.Stun(isStunned);
        }

        public void PlayMoveAnim(float horizontal, float vertical, bool isDashing)
        {
            move.PlayMove(horizontal);
            move.PlayCrouch(vertical);
            move.PlayDash(isDashing);
        }

        public void PlayAttackAnim(int attackIndex)
        {
            attack.PlayAttack(attackIndex);
        }

        public void PlayHitAnim(int hitIndex)
        {
            hit.PlayHit(hitIndex);
        }

        private void OnDrawGizmos() { }
    }
}
