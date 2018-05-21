using Survival;
using UnityEngine;

namespace Boxes
{
    /// <summary>
    /// Class that is responsible for areas where the character can be hurt.
    /// </summary>
    public class Hurtbox : Box
    {
        private Health health = null;

        public delegate void HurtDelegate(int hurtIndex);

        private HurtDelegate m_HurtDelegate;

        private int hurtIndex = 0;

        private void Awake()
        {
            foreach (Health health in GetComponentsInParent<Health>())
                if (health)
                {
                    this.health = health;
                    break;
                }

            m_HurtDelegate = health.HitArea;

            if (boxArea == BoxArea.MidTorso || boxArea == BoxArea.RightThigh || boxArea == BoxArea.LeftThigh)
                hurtIndex = 2;
            else if (boxArea == BoxArea.LeftCalf || boxArea == BoxArea.RightCalf)
                hurtIndex = 3;
            else
                hurtIndex = 1;

            active = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            m_HurtDelegate(hurtIndex);
        }

        private void OnTriggerExit(Collider other)
        {
            m_HurtDelegate(0);
        }


        public void Enabled(bool enabled)
        {
            active = enabled;
            gameObject.layer = (enabled) ? (int)Layer.Hurtbox : (int)Layer.Dead;
        }
    }
}