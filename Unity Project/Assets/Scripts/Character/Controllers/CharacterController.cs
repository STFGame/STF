using Actions;
using Controls;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Controller class for the character.
    /// </summary>
    public class CharacterController : MonoBehaviour
    {
        #region CharacterController Variables
        [SerializeField] private PlayerNumber playerNumber;

        //All of the different character components 
        private CharacterJump characterJump;
        private CharacterMove characterMove;
        private CharacterAttack characterAttack;
        private CharacterHealth characterHealth;
        private CharacterShield characterShield;
        private CharacterEvasion characterEvasion;
        private Gravity gravity;

        #region Controls
        //Device for controlling the player
        private Device device = null;

        //Moves the character based on joystick input
        private Vector2 move;

        [Header("Actions")]
        [SerializeField] private ActionTracker actionTracker = new ActionTracker();

        [Header("Jump Inputs")]
        //Range for determining whether the character wants to fall fast
        [SerializeField] private ActionRange fallRange = new ActionRange();

        //How long the jump button can be held
        [SerializeField] private ActionHold jumpAction = new ActionHold();

        [Header("Movement Inputs")]
        //Range for determining if the character is dashing
        [SerializeField] private ActionRange dashRange = new ActionRange();

        //Range for determining if the character is crouching
        [SerializeField] private ActionRange crouchRange = new ActionRange();

        private int neutralAttack = 1;
        private int forwardAttack = 2;
        private int backAttack = 3;
        private int downAttack = 4;
        private int upAttack = 5;
        #endregion

        //Stops all updates that belong to this object
        public bool Stop { get; set; }
        #endregion

        #region Load
        private void Awake()
        {
            characterJump = GetComponent<CharacterJump>();
            characterMove = GetComponent<CharacterMove>();
            characterAttack = GetComponent<CharacterAttack>();
            characterEvasion = GetComponent<CharacterEvasion>();
            characterHealth = GetComponent<CharacterHealth>();
            characterShield = GetComponent<CharacterShield>();
            gravity = GetComponent<Gravity>();

            actionTracker.Load();
        }

        public void CreateDevice(PlayerNumber playerNumber)
        {
            if ((int)playerNumber - 1 >= Input.GetJoystickNames().Length)
                return;

            this.playerNumber = playerNumber;
            string deviceName = Input.GetJoystickNames()[(int)playerNumber - 1];
            device = new Device(deviceName, (int)playerNumber);
        }
        #endregion

        #region Updates
        private void Update()
        {
            if (device == null && (int)playerNumber - 1 < Input.GetJoystickNames().Length && playerNumber != PlayerNumber.None)
                device = new Device(Input.GetJoystickNames()[(int)playerNumber - 1], (int)playerNumber);

            if (Stop || device == null)
                return;

            #region Character Method Updates
            DeviceUpdate();

            JumpUpdate();

            AttackUpdate();

            ShieldUpdate();

            HealthUpdate();

            UpdateTracker();
            #endregion

        }

        private void DeviceUpdate()
        {
            device.UpdateDevice();

            float horizontal = device.LeftHorizontal.Value;
            float vertical = device.LeftVertical.Value;

            move = new Vector2(horizontal, vertical);
        }

        private void FixedUpdate()
        {
            UpdateGravity();

            if (Stop)
                return;

            MoveUpdate();

            EvadeUpdate();
        }
        #endregion

        #region Jump
        private void JumpUpdate()
        {
            if (!characterJump)
                return;

            bool jump = device.L1.Press;
            bool jumpHold = jumpAction.ActionSuccess((device.L1.Hold));

            #region Jump Mode
            JumpMode jumpMode = JumpMode.None;

            if (fallRange.ActionSuccess(move) && !characterJump.Grounded)
                jumpMode = JumpMode.Descend;

            #endregion

            characterJump.Jump(jump, jumpHold, jumpMode);

            characterJump.Fall(transform.position.y);

            characterJump.AnimateJump(jump);
        }
        #endregion

        #region Movement
        private void MoveUpdate()
        {
            if (!characterMove)
                return;

            #region MoveMode
            MoveMode moveMode = MoveMode.None;

            if (dashRange.ActionSuccess(move))
            {
                moveMode = MoveMode.Fast;

                if (move.x < 0)
                    move.x = -1;
                else if (move.x > 0)
                    move.x = 1;
            }
            else if (crouchRange.ActionSuccess(move))
                moveMode = MoveMode.Slow;
            else
                moveMode = MoveMode.Normal;
            #endregion

            if (!StopMove())
            {
                characterMove.Move(move, moveMode);

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

            if (characterHealth != null)
            {
                stop = (characterHealth.Immune);
                if (stop)
                    return stop;
            }

            return stop;
        }
        #endregion

        #region Combat
        private void AttackUpdate()
        {
            if (!characterAttack)
                return;

            int attackID = 0;
            RefAttackID(ref attackID);

            characterAttack.Attack(attackID);

            characterAttack.AnimateAttack(attackID);
        }

        private void RefAttackID(ref int attackID)
        {
            float forward = transform.forward.x;

            attackID = (device.Action1.Press) ? neutralAttack : attackID;
            attackID = (device.Action1.Press && device.LeftHorizontal.Value * forward > 0.25f) ? forwardAttack : attackID;
            attackID = (device.Action1.Press && device.LeftHorizontal.Value * forward < -0.25f) ? backAttack : attackID;
            attackID = (device.Action1.Press && device.LeftVertical.Value < -0.25) ? downAttack : attackID;
            attackID = (device.Action1.Press && device.LeftVertical.Value > 0.25) ? upAttack : attackID;

            //if (device.Action1.Press)
            //attackID = 1;
            //if (device.Action1.Press && device.LeftHorizontal.Value * transform.forward.x > 0.35f)
            //attackID = 2;
            //else if (device.Action1.Press && device.LeftHorizontal.Value * transform.forward.x < -0.35f)
            //attackID = 3;
            //else if (device.Action1.Press && device.LeftVertical.Value < -0.35f)
            //attackID = 4;
            //else if (device.Action1.Press && device.LeftVertical.Value > 0.35f)
            //attackID = 5;
        }
        #endregion

        #region Evasion
        private void EvadeUpdate()
        {
            if (!characterEvasion || !characterShield)
                return;

            EvasionMode evasionMode = EvasionMode.None;
            int evasionIndex = 0;

            evasionIndex = (characterShield.Shielding && device.LeftVertical.Value < -0.35) ? 1 : evasionIndex;
            evasionIndex = (characterShield.Shielding && device.LeftHorizontal.Value * transform.forward.x > 0.35f) ? 2 : evasionIndex;
            evasionIndex = (characterShield.Shielding && device.LeftHorizontal.Value * transform.forward.x < -0.35f) ? 3 : evasionIndex;

            evasionMode = (evasionIndex == 1) ? EvasionMode.Dodge : evasionMode;
            evasionMode = (evasionIndex == 2) ? EvasionMode.RollForward : evasionMode;
            evasionMode = (evasionIndex == 3) ? EvasionMode.RollBackward : evasionMode;

            characterEvasion.Evade(evasionMode, move.x);

            characterEvasion.AnimateEvasion(evasionIndex);
        }
        #endregion

        #region Survival
        private void ShieldUpdate()
        {
            if (!characterShield)
                return;

            bool shield = device.R1.Hold;

            characterShield.Shield(device.R1.Hold);
            characterShield.AnimateShield();
        }
        #endregion

        #region Health
        private void HealthUpdate()
        {
            if (!characterHealth)
                return;

            characterHealth.UpdateHealth();
        }

        #endregion

        #region Gravity
        private void UpdateGravity()
        {
            if (!gravity)
                return;

            gravity.UpdateGravity();
        }
        #endregion

        #region Action Tracker
        private void UpdateTracker()
        {
            //ActionInput currentAction = ActionInput.None;

            //if (device.LeftHorizontal.Value * transform.forward.x > 0.25f)
            //currentAction = ActionInput.Forward;
            //else if (device.LeftHorizontal.Value * transform.forward.x < -0.25f)
            //currentAction = ActionInput.Back;
            //else if (device.LeftVertical.Value < 0f)
            //currentAction = ActionInput.Down;
            //else if (device.LeftVertical.Value > 0)
            //currentAction = ActionInput.Up;

            //if (device.Action1.Press)
            //currentAction = ActionInput.Action1;
            //else if (device.Action2.Press)
            //currentAction = ActionInput.Action2;
            //else if (device.Action3.Press)
            //currentAction = ActionInput.Action3;
            //else if (device.Action4.Press)
            //currentAction = ActionInput.Action4;
        }

        #endregion
    }
}