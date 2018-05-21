using Actions;
using Controls;
using Movements;
using Survival;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Controller class for the character.
    /// </summary>
    public class CharacterController : MonoBehaviour
    {
        #region CharacterController Variables
        [SerializeField] private PlayerNumber m_PlayerNumber;

        //All of the different character components 
        private Jump m_Jump;
        private Movement m_Movement;
        private CharacterAttack m_Attack;
        private Health m_Health;
        private Shield m_Shield;
        private CharacterEvasion m_Evasion;
        private Gravity m_Gravity;

        #region Controls
        //Device for controlling the player
        private Device m_Device = null;

        //Moves the character based on joystick input
        private Vector2 m_Move;

        [Header("Jump Inputs")]
        //Range for determining whether the character wants to fall fast
        [SerializeField] private ActionRange m_FallRange = new ActionRange();

        //How long the jump button can be held
        [SerializeField] private ActionHold m_JumpAction = new ActionHold();

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
            m_Jump = GetComponent<Jump>();
            m_Movement = GetComponent<Movement>();
            m_Attack = GetComponent<CharacterAttack>();
            m_Evasion = GetComponent<CharacterEvasion>();
            m_Health = GetComponent<Health>();
            m_Shield = GetComponent<Shield>();
            m_Gravity = GetComponent<Gravity>();
        }

        public void CreateDevice(PlayerNumber playerNumber)
        {
            if ((int)playerNumber - 1 >= Input.GetJoystickNames().Length)
                return;

            m_PlayerNumber = playerNumber;
            string deviceName = Input.GetJoystickNames()[(int)playerNumber - 1];
            m_Device = new Device(deviceName, (int)playerNumber);
        }
        #endregion

        #region Updates
        private void Update()
        {
            if (m_Device == null && (int)m_PlayerNumber - 1 < Input.GetJoystickNames().Length && m_PlayerNumber != PlayerNumber.None)
                m_Device = new Device(Input.GetJoystickNames()[(int)m_PlayerNumber - 1], (int)m_PlayerNumber);

            if (Stop || m_Device == null)
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
            m_Device.UpdateDevice();

            float horizontal = m_Device.LeftHorizontal.Value;
            float vertical = m_Device.LeftVertical.Value;

            m_Move = new Vector2(horizontal, vertical);
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
            if (!m_Jump)
                return;

            bool jump = m_Device.L1.Press;
            bool jumpHold = m_Device.L1.Hold;

            m_Jump.Spring(m_Move, jump, jumpHold);

            m_Jump.Fall(transform.position.y);
        }
        #endregion

        #region Movement
        private void MoveUpdate()
        {
            if (m_Movement == null)
                return;

            m_Movement.Move(m_Move);
        }
        #endregion

        #region Combat
        private void AttackUpdate()
        {
            if (!m_Attack)
                return;

            int attackID = 0;
            RefAttackID(ref attackID);

            m_Attack.Attack(attackID);

            m_Attack.AnimateAttack(attackID);
        }

        private void RefAttackID(ref int attackID)
        {
            float forward = transform.forward.x;

            attackID = (m_Device.Action1.Press) ? neutralAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftHorizontal.Value * forward > 0.25f) ? forwardAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftHorizontal.Value * forward < -0.25f) ? backAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftVertical.Value < -0.25) ? downAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftVertical.Value > 0.25) ? upAttack : attackID;
        }
        #endregion

        #region Evasion
        private void EvadeUpdate()
        {
            if (!m_Evasion || !m_Shield)
                return;

            EvasionMode evasionMode = EvasionMode.None;
            int evasionIndex = 0;

            //evasionIndex = (m_Shield.Shielding && m_Device.LeftVertical.Value < -0.35) ? 1 : evasionIndex;
            //evasionIndex = (m_Shield.Shielding && m_Device.LeftHorizontal.Value * transform.forward.x > 0.35f) ? 2 : evasionIndex;
            //evasionIndex = (m_Shield.Shielding && m_Device.LeftHorizontal.Value * transform.forward.x < -0.35f) ? 3 : evasionIndex;

            evasionMode = (evasionIndex == 1) ? EvasionMode.Dodge : evasionMode;
            evasionMode = (evasionIndex == 2) ? EvasionMode.RollForward : evasionMode;
            evasionMode = (evasionIndex == 3) ? EvasionMode.RollBackward : evasionMode;

            m_Evasion.Evade(evasionMode, m_Move.x);

            m_Evasion.AnimateEvasion(evasionIndex);
        }
        #endregion

        #region Survival
        private void ShieldUpdate()
        {
            if (!m_Shield)
                return;

            bool shield = m_Device.R1.Hold;

            m_Shield.Tick(m_Device.R1.Hold);
        }
        #endregion

        #region Health
        private void HealthUpdate()
        {
            if (!m_Health)
                return;

            m_Health.Tick();
        }

        #endregion

        #region Gravity
        private void UpdateGravity()
        {
            if (!m_Gravity)
                return;

            m_Gravity.UpdateGravity();
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