using Actions;
using Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerNumber playerNumber = PlayerNumber.None;

        [SerializeField] private CharacterAction[] characterActions;

        [SerializeField] private InputTracker inputTracker = new InputTracker();
        [SerializeField] private InputRegistration inputRegistration = new InputRegistration();

        [SerializeField] private InputRange dashRange = new InputRange();
        [SerializeField] private InputRange highJump = new InputRange();


        public Device Device { get; private set; }
        public bool Active { get; private set; }

        // Use this for initialization
        private void Start()
        {
            inputRegistration.Init();
            Active = false;
        }

        public void InitiateDevice(PlayerNumber playerNumber)
        {
            this.playerNumber = playerNumber;
            Device = new Device(Input.GetJoystickNames()[(int)playerNumber - 1], (int)playerNumber);
        }

        // Update is called once per frame
        private void Update()
        {
            int number = (int)playerNumber - 1;

            bool inRange = (number - Input.GetJoystickNames().Length) <= 0;

            if (playerNumber != PlayerNumber.None && Device == null && inRange)
                InitiateDevice(playerNumber);

            if (Device == null)
            {
                Active = false;
                return;
            }

            Active = true;

            Device.UpdateDevice();

            UpdateActions();

            UpdateTracker();
        }

        private void UpdateTracker()
        {
            inputTracker.UpdateBuffer(Device);
        }

        private void UpdateActions()
        {
            Dash(Device.LeftStick.Horizontal);
            Crouch(Device.LeftStick.Vertical);
            HoldJump(Device.LeftBumper.Hold);
            Block(Device.RightBumper.Hold);
            Roll(Blocking, Device.LeftStick.Horizontal);
            Attack();
        }

        #region Character Actions
        public Vector2 Moving { get; private set; }

        public bool Dashing { get; private set; }
        public bool Crouching { get; private set; }
        public bool HighJump { get; private set; }
        public bool Blocking { get; private set; }
        public bool Rolling { get; private set; }
        public int Attacking { get; private set; }

        private void Dash(float direction)
        {
            Dashing = dashRange.Succeeded(direction);
        }

        private void Attack()
        {
            Attacking = (Device.Action1.Press) ? 1 : 0;
        }

        private void Crouch(float direction)
        {
            Crouching = direction < -0.5f;
        }

        private void HoldJump(bool value)
        {
            HighJump = highJump.Succeeded(value);
        }

        private void Block(bool value)
        {
            Blocking = value;
        }

        private void Roll(bool value, float direction)
        {
            Rolling = (value && (direction * transform.forward.x > 0));
        }
        #endregion
    }
}