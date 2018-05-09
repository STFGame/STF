using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        #region Character Controller Variables
        private CharacterJump characterJump;
        private CharacterMove characterMove;
        private CharacterAttack characterAttack;
        private CharacterHealth characterHealth;
        private CharacterShield characterShield;
        private CharacterEvasion characterEvasion;
        private InputManager inputManager;

        private Vector2 move;

        private bool jump;
        private bool jumpHold;

        public bool Stop { get; set; }
        #endregion

        #region Initialization
        private void Start()
        {
            characterJump = GetComponent<CharacterJump>();
            characterMove = GetComponent<CharacterMove>();
            characterAttack = GetComponent<CharacterAttack>();
            characterEvasion = GetComponent<CharacterEvasion>();
            characterHealth = GetComponent<CharacterHealth>();
            characterShield = GetComponent<CharacterShield>();

            inputManager = GetComponent<InputManager>();
        }
        #endregion

        #region Updates
        private void Update()
        {
            if (Stop)
                return;

            if (!inputManager.Active)
                return;

            #region Controls Update
            float horizontal = inputManager.Device.LeftStick.Horizontal;
            float vertical = inputManager.Device.LeftStick.Vertical;

            move = new Vector2(horizontal, vertical);
            jump = inputManager.Device.LeftBumper.Press;
            jumpHold = inputManager.Device.LeftBumper.Hold;
            #endregion

            if (characterJump)
                Jump();

            if (characterAttack)
                Attack();

            if (characterShield)
                Shield();

        }

        private void FixedUpdate()
        {
            if (Stop)
                return;

            if (characterMove)
                Move();

            if (characterEvasion)
                Evade();
        }
        #endregion

        #region Jump
        private void Jump()
        {
            characterJump.Jump(jump, inputManager.HighJump);

            characterJump.Fall(transform.position.y);

            characterJump.AnimateJump(jump);
        }
        #endregion

        #region Movement
        private void Move()
        {
            #region Move Mode
            MoveMode moveMode = MoveMode.None;

            if (inputManager.Dashing)
                moveMode = MoveMode.Fast;
            else if (inputManager.Crouching)
                moveMode = MoveMode.Slow;
            else
                moveMode = MoveMode.Normal;
            #endregion

            if (!StopMove())
            {
                characterMove.Move(move, moveMode);

                characterMove.Rotate(move.x);

                characterMove.AnimateMove(move);

                return;
            }

            characterMove.Stop();
        }

        private bool StopMove()
        {
            bool stop = false;
            if (characterShield != null)
            {
                stop = (characterShield.Shielding || characterShield.ShieldStun);
                if (stop)
                    return stop;
            }

            if (characterEvasion != null)
            {
                stop = (characterEvasion.Dodging || characterEvasion.Rolling);
                if (stop)
                    return stop;
            }

            if(characterHealth != null)
            {
                stop = (characterHealth.Hurt);
                if (stop)
                    return stop;
            }

            return stop;
        }
        #endregion

        #region Attack
        private void Attack()
        {
            characterAttack.Attack();
            characterAttack.AnimateAttack(inputManager.Attacking);
        }
        #endregion

        #region Evasion
        private void Evade()
        {
            //characterEvasion.Roll((inputManager.Blocking && move.x != 0), move.x);

            characterEvasion.AnimateEvasion();
        }
        #endregion

        #region Survival
        private void Shield()
        {
            characterShield.Shield(inputManager.Device.RightBumper.Hold);
        }
        #endregion
    }
}