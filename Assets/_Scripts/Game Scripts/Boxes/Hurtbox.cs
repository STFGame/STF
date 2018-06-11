using UnityEngine;

namespace Boxes
{
    /// <summary>
    /// Class that is responsible for areas where the character can be hurt.
    /// </summary>
    public class Hurtbox : Box
    {
        public delegate void HurtDelegate(int hurtIndex, Transform position);
        public event HurtDelegate HurtEvent;

        private int m_hurtIndex = 0;
        //private int m_previousIndex = 0;

        private void Awake()
        {
            if (BoxArea == BoxArea.MidTorso || BoxArea == BoxArea.RightThigh || BoxArea == BoxArea.LeftThigh)
                m_hurtIndex = 2;
            else if (BoxArea == BoxArea.LeftCalf || BoxArea == BoxArea.RightCalf ||
                     BoxArea == BoxArea.LeftAnkle || BoxArea == BoxArea.RightAnkle ||
                     BoxArea == BoxArea.LeftFoot || BoxArea == BoxArea.RightFoot ||
                     BoxArea == BoxArea.LeftKnee || BoxArea == BoxArea.RightKnee)
                m_hurtIndex = 3;
            else
                m_hurtIndex = 1;

            m_active = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            Update_HitEvent(m_hurtIndex, transform);
        }

        private void Update_HitEvent(int hurtIndex, Transform position)
        {
            HurtEvent?.Invoke(hurtIndex, position);
        }

        public void Enabled(bool enabled)
        {
            m_active = enabled;
            gameObject.layer = (enabled) ? (int)Layer.Hurtbox : (int)Layer.Dead;
        }
    }
}