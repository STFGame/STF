using Boxes;
using Broadcasts;
using Controls;
using Life;
using Managers;
using Particles;
using System;
using System.Linq;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Controller class for the character.
    /// </summary>
    public class CharacterManager : MonoBehaviour, IBroadcast
    {
        [SerializeField] private PlayerNumber m_playerNumber;
        [SerializeField] private ParticleList m_particleList;

        private BroadcastMessage m_message;

        //All of the different character components 
        private Jump m_jump;
        private Movement m_movement;
        private Attack m_Attack;
        private Health m_Health;
        private Shield m_shield;
        private Evasion m_Evasion;
        private Gravity m_Gravity;

        private Animator m_animator;

        private Transform m_hitPosition;
        #region Controls
        //Device for controlling the player
        private Device m_device = null;

        //Moves the character based on joystick input
        private Vector2 m_move;

        private bool m_jumpCommand;
        private bool m_jumpHold;

        private int m_hurtIndex;

        private int m_neutralAttack = 1;
        private int m_forwardAttack = 2;
        private int m_backAttack = 3;
        private int m_downAttack = 4;
        private int m_upAttack = 5;
        #endregion

        public int PlayerNumber { get; private set; }
        //Stops all updates that belong to this object
        public bool Stop { get; set; }

        private void Awake()
        {
            m_jump = GetComponent<Jump>();
            m_movement = GetComponent<Movement>();
            m_Attack = GetComponent<Attack>();
            m_Evasion = GetComponent<Evasion>();
            m_Health = GetComponent<Health>();
            m_shield = GetComponent<Shield>();
            m_Gravity = GetComponent<Gravity>();

            m_animator = GetComponent<Animator>();
        }

        private void Start()
        {
            BoxArea[] boxTypes = Enum.GetValues(typeof(BoxArea)).Cast<BoxArea>().ToArray();
            for (int i = 0; i < boxTypes.Length; i++)
            {
                Hurtbox hurtbox = GetComponent<BoxManager>().GetBox(BoxType.Hurtbox, boxTypes[i]) as Hurtbox;

                if (hurtbox)
                    hurtbox.HurtEvent += Update_HurtIndex;
            }
        }

        private void OnDisable()
        {
            BoxArea[] boxTypes = Enum.GetValues(typeof(BoxArea)).Cast<BoxArea>().ToArray();
            for (int i = 0; i < boxTypes.Length; i++)
            {
                Hurtbox hurtbox = GetComponent<BoxManager>().GetBox(BoxType.Hurtbox, boxTypes[i]) as Hurtbox;

                if (hurtbox)
                    hurtbox.HurtEvent -= Update_HurtIndex;
            }
        }

        public void Update_HurtIndex(int hurtIndex, Transform hitPosition)
        {
            m_hurtIndex = hurtIndex;
            m_hitPosition = hitPosition;
        }

        public void InitialiseDevice(int playerNumber)
        {
            PlayerNumber = playerNumber;
            if (playerNumber >= Input.GetJoystickNames().Length)
                return;

            m_device = InputManager.GetDevice(playerNumber);
        }

        private void Update()
        {
            if (Pause.IsPaused || m_message == Broadcasts.BroadcastMessage.Stop)
                return;

            AnimateJump();
            AnimateMove();
            AnimateHit();
            AnimateStun();

            if (m_message == Broadcasts.BroadcastMessage.Stunned ||
                m_message == Broadcasts.BroadcastMessage.Dead)
            {
                m_move = Vector3.zero;
                m_movement.ResetMove();
                m_jump.ResetJump();
                return;
            }

            if (Stop || m_device == null)
                return;

            ExecuteDevice();

            ExecuteJump();

            ExecuteAttack();

            ExecuteShield();
        }

        private void ExecuteDevice()
        {
            float horizontal = m_device.LeftHorizontal.Value;
            float vertical = m_device.LeftVertical.Value;

            m_move = new Vector2(horizontal, vertical);
            m_jumpCommand = m_device.L1.Press;
            m_jumpHold = m_device.L1.Hold;
        }

        private void FixedUpdate()
        {
            if (Pause.IsPaused || m_message == Broadcasts.BroadcastMessage.Stop)
                return;

            if (m_message == Broadcasts.BroadcastMessage.Stunned ||
                m_message == Broadcasts.BroadcastMessage.Dead)
                return;

            ExecuteGravity();

            if (!m_Evasion.IsRolling && !m_Evasion.IsDodging && !m_Attack.IsAttacking)
                ExecuteMove();

            ExecuteEvade();
        }

        public void PlayParticle(ParticleType particleType)
        {
            m_particleList.CreateParticle(transform, m_hitPosition.position, particleType);
        }

        private void ExecuteJump()
        {
            if (!m_jump)
                return;

            m_jump.Execute(m_move, m_jumpCommand, m_jumpHold);

            m_jump.Fall(transform.position.y);
        }

        private void ExecuteMove()
        {
            if (m_movement == null)
                return;

            m_movement.Move(m_move);
        }

        private void ExecuteAttack()
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

            attackID = (m_device.Action1.Press) ? m_neutralAttack : attackID;
            attackID = (m_device.Action1.Press && m_device.LeftHorizontal.Value * forward > 0.1f) ? m_forwardAttack : attackID;
            attackID = (m_device.Action1.Press && m_device.LeftHorizontal.Value * forward < -0.1f) ? m_backAttack : attackID;
            attackID = (m_device.Action1.Press && m_device.LeftVertical.Value < -0.1) ? m_downAttack : attackID;
            attackID = (m_device.Action1.Press && m_device.LeftVertical.Value > 0.1) ? m_upAttack : attackID;
        }

        private void ExecuteEvade()
        {
            if (!m_Evasion || !m_shield)
                return;

            m_Evasion.Execute(m_move, m_shield.Shielding);
        }

        private void ExecuteShield()
        {
            if (!m_shield)
                return;

            bool shield = m_device.R1.Hold;

            m_shield.Execute(m_device.R1.Hold);
        }

        private void ExecuteGravity()
        {
            if (!m_Gravity)
                return;

            m_Gravity.Execute();
        }

        #region Animation and Visual FX
        private void AnimateMove()
        {
            float speed = Mathf.Abs(m_move.x);
            float crouchSpeed = m_move.x * transform.forward.x;
            float crouchFactor = m_move.y;

            m_animator.SetFloat("Speed", speed);
            m_animator.SetFloat("Crouch Speed", crouchSpeed);
            m_animator.SetFloat("Crouch Factor", m_move.y);
            m_animator.SetBool("Crouching", m_movement.IsCrouching);
            m_animator.SetBool("Dashing", m_movement.IsDashing);

        }

        private void AnimateJump()
        {
            m_animator.SetBool("Jump", m_jumpCommand);
            m_animator.SetBool("Falling", m_jump.IsFalling);
            m_animator.SetBool("Grounded", m_jump.IsGrounded);
            m_animator.SetInteger("Jump Count", m_jump.JumpsRemaining);
        }

        private void AnimateHit()
        {
            m_animator.SetInteger("HitIndex", m_hurtIndex);

            m_hurtIndex = 0;
        }

        private void AnimateStun()
        {
            bool stunned = (m_message == Broadcasts.BroadcastMessage.Stunned);
            m_animator.SetBool("Stunned", stunned);
        }
        #endregion

        public void Inform(BroadcastMessage message)
        {
            m_message = message;
        }
    }
}