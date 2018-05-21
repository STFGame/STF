using UnityEngine;

namespace Movements
{
    /// <summary>
    /// Base class for movement.
    /// </summary>
    public abstract class Movement : MonoBehaviour
    {
        protected float m_Movement = 0f;
        protected float m_Acceleration = 0f;

        public abstract Vector2 Velocity { get; protected set; }

        public abstract void Move(Vector2 move);
    }
}
