using UnityEngine;

namespace Movements
{
    public abstract class Jump : MonoBehaviour
    {
        [SerializeField] [Range(0f, 20f)] protected float m_JumpHeight = 5f;
        [SerializeField] [Range(0f, 200f)] protected float m_DescendSpeed = 5f;
        [SerializeField] [Range(0f, 5f)] protected float m_JumpDelay = 0.15f;

        [SerializeField] [Range(0, 5)] protected sbyte m_MaxJumps = 2;

        protected float m_PreviousHeight;

        protected sbyte m_JumpCount = 0;

        public abstract void Execute(Vector2 direction, bool jump, bool jumpHold);

        public abstract void Fall(float currentHeight);
    }
}
