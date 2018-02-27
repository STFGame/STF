using UnityEngine;
/*WARNING*/
/*This code should not be edited unless you know what you are doing*/

namespace Old.Animation
{
    #region Animator States -- struct
    //This struct is used to store and get the hash values of the different states in the state machine
    public struct AnimaState
    {
        private static int m_JumpHash = Animator.StringToHash("Base Layer.Jump");
        private static int m_IdleHash = Animator.StringToHash("Base Layer.Idle");
        private static int m_LocoHash = Animator.StringToHash("Base Layer.Locomotion");
        private static int m_IdleAttackHash = Animator.StringToHash("Idle Attack.Attack1");
        private static int m_LocoAttackHash = Animator.StringToHash("Locomotion.Attack1");

        public static int JumpState { get { return m_JumpHash; } }
        public static int IdleState { get { return m_IdleHash; } }
        public static int LocomotionState { get { return m_LocoHash; } }
        public static int IdleAttackState { get { return m_IdleAttackHash; } }
        public static int LocoAttackState { get { return m_LocoAttackHash; } }
    }
    #endregion

    #region Animator Parameters -- struct
    //This struct is used to store the parameter names from the state machine
    public struct AnimaParams
    {
        private static string m_Attack = "Attack";
        private static string m_XAxis = "X-Axis";
        private static string m_IsGrounded = "IsGrounded";
        private static string m_Jump = "Jump";
        private static string m_Direction = "Direction";
        private static string m_ColliderHeight = "ColliderHeight";
        private static string m_DirectionChange = "DirectionChange";
        private static string m_Dash = "Dash";

        public static string Attack { get { return m_Attack; } }
        public static string XAxis { get { return m_XAxis; } }
        public static string IsGrounded { get { return m_IsGrounded; } }
        public static string Jump { get { return m_Jump; } }
        public static string Direction { get { return m_Direction; } }
        public static string ColliderHeight { get { return m_ColliderHeight; } }
        public static string DirectionChange { get { return m_DirectionChange; } }
        public static string Dash { get { return m_Dash; } }
    }
    #endregion
}