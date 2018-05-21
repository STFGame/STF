using Actions;
using Controls;
using Managers;
using Movements;
using Survival;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Controller class for the character.
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {
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

        public void InitialiseDevice(int playerNumber)
        {
            if (playerNumber >= Input.GetJoystickNames().Length)
                return;

            m_Device = InputManager.GetDevice(playerNumber);
        }

        #region Updates
        private void Update()
        {
            if (Stop || m_Device == null)
                return;

            DeviceExecute();

            JumpExecute();

            AttackExecute();

            ShieldExecute();

            HealthExecute();
        }

        private void DeviceExecute()
        {
            //m_Device.Execute();

            float horizontal = m_Device.LeftHorizontal.Value;
            float vertical = m_Device.LeftVertical.Value;

            m_Move = new Vector2(horizontal, vertical);
        }

        private void FixedUpdate()
        {
            UpdateGravity();

            if (Stop || m_Shield.Shielding)
                return;

            MoveUpdate();

            EvadeUpdate();
        }
        #endregion

        #region Jump
        private void JumpExecute()
        {
            if (!m_Jump)
                return;

            bool jump = m_Device.L1.Press;
            bool jumpHold = m_Device.L1.Hold;

            m_Jump.Execute(m_Move, jump, jumpHold);

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
        private void AttackExecute()
        {
            if (!m_Attack)
                return;

            int attackID = 0;
            GetAttackID(ref attackID);

            m_Attack.Execute(attackID);
        }

        private void GetAttackID(ref int attackID)
        {
            float forward = transform.forward.x;

            attackID = (m_Device.Action1.Press) ? neutralAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftHorizontal.Value * forward > 0.1f) ? forwardAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftHorizontal.Value * forward < -0.1f) ? backAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftVertical.Value < -0.1) ? downAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftVertical.Value > 0.1) ? upAttack : attackID;
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

            m_Evasion.Execute(evasionMode, m_Move.x);

            m_Evasion.AnimateEvasion(evasionIndex);
        }
        #endregion

        #region Survival
        private void ShieldExecute()
        {
            if (!m_Shield)
                return;

            bool shield = m_Device.R1.Hold;

            m_Shield.Execute(m_Device.R1.Hold);
        }
        #endregion

        #region Health
        private void HealthExecute()
        {
            if (!m_Health)
                return;

            m_Health.Execute();
        }

        #endregion

        #region Gravity
        private void UpdateGravity()
        {
            if (!m_Gravity)
                return;

            m_Gravity.Execute();
        }
        #endregion

        #region Animations
        private void Animate()
        {

        }
        #endregion
    }
}