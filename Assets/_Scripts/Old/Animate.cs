using UnityEngine;

namespace Old.Animation
{
    public struct AnimaData
    {

    }

    public class Animate
    {
        //Hash values for the different animation states
        //private int m_JumpHash = Animator.StringToHash("Base Layer.Jump");
        //private int m_IdleHash = Animator.StringToHash("Base Layer.Idle");
        //private int m_LocoHash = Animator.StringToHash("Base Layer.Locomotion");
        //private int m_IdleAttackHash = Animator.StringToHash("Idle Attack.Attack1");
        //private int m_LocoAttackHash = Animator.StringToHash("Base Layer.Locomotion Attack");

        private Animator m_Animator;
        private AnimatorStateInfo m_AnimStateInfo;

        private Vector3 m_AnimaMovement;

        #region Constructors
        public Animate()
        {

        }

        public Animate(Animator animator)
        {
            m_Animator = animator;
        }
        #endregion

        public void UpdateAnime(InputManager input, bool grounded)
        {
            m_AnimStateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);

            if (m_AnimStateInfo.fullPathHash != AnimaState.JumpState && grounded)
            {
                m_AnimaMovement = new Vector3(input.Joystick.x, 0f, 0f);
                m_Animator.SetFloat(AnimaParams.XAxis, m_AnimaMovement.magnitude, 0.05f, Time.deltaTime);
                m_Animator.SetBool(AnimaParams.Dash, input.Dash);
            }


            //if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                //AttackAnima(AttackType.LightAttack);

            //if (m_AnimStateInfo.IsTag("Attack"))
            //{
                //if (!m_Animator.IsInTransition(0))
                        //m_Animator.SetInteger("Attack", AttackType.None);
              
            //}
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                if (grounded)
                {
                    m_Animator.SetBool(AnimaParams.Jump, true);
                }
            }

            if (m_AnimStateInfo.fullPathHash == AnimaState.JumpState)
            {
                if (!m_Animator.IsInTransition(0))
                    m_Animator.SetBool("Jump", false);
            }

            m_Animator.SetBool("IsGrounded", grounded);
        }

        private void AttackAnima(int attackNum)
        {
            m_Animator.SetInteger("Attack", attackNum);
        }

        private void JumpAnima()
        {
            return;
        }

        #region Animation Getters
        public AnimatorStateInfo CurrentState
        {
            get
            {
                m_AnimStateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
                return m_AnimStateInfo;
            }
        }

        public AnimatorStateInfo NextState
        {
            get
            {
                m_AnimStateInfo = m_Animator.GetNextAnimatorStateInfo(0);
                return m_AnimStateInfo;
            }
        }

        //public int Jump { get { return m_JumpHash; } }

        //public int Idle { get { return m_IdleHash; } }

        //public int Locomotion { get { return m_LocoHash; } }

        //public int IdleAttack { get { return m_IdleAttackHash; } }
        #endregion
    }
}