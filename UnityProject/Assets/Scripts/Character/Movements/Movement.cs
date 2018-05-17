using Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Movements
{
    /// <summary>
    /// Base class for movement.
    /// </summary>
    public abstract class Movement : MonoBehaviour, IAlert
    {
        protected float m_Movement = 0f;
        protected float m_Acceleration = 0f;

        public abstract Vector2 Velocity { get; protected set; }

        public abstract void Move(Vector2 move);

        public abstract void Inform(AlertValue message);
    }
}
