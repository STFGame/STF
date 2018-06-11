using System;
using System.Collections;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Class that is responsible for handling evasion.
    /// </summary>
    public class Evasion : MonoBehaviour
    {
        [SerializeField] private float m_rollSpeed = 10f;

        //How long a roll lasts.
        [SerializeField] private float m_rollEnableLength = 0.2f;

        //How long a dodge lasts.
        [SerializeField] private float m_dodgeEnableLength = 0.2f;

        private Rigidbody m_rigidbody;
        private Animator m_evasionAnimator;

        //An event that notifies subscribers when character is evading.
        public Action<bool, float> EvasionEvent;

        private bool m_isRolling = false;
        private bool m_isDodging = false;

        public bool IsRolling { get { return m_isRolling; } }
        public bool IsDodging { get { return m_isDodging; } }

        // Use this for initialization
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_evasionAnimator = GetComponent<Animator>();
        }

        public void Execute(Vector2 direction, bool shield)
        {
            if ((direction.y <= -0.5f && shield) && !m_isDodging)
            {
                StopCoroutine(DodgeRoutine());
                StartCoroutine(DodgeRoutine());
            }
            else if ((direction.x > 0.2f || direction.x < -0.2f) && shield && !m_isRolling)
            {
                StopCoroutine(RollRoutine(direction));
                StartCoroutine(RollRoutine(direction));
            }
        }

        private IEnumerator RollRoutine(Vector2 direction)
        {
            AnimateEvasion(2);
            m_rigidbody.AddForce(direction * m_rollSpeed, ForceMode.VelocityChange);
            yield return new WaitForEndOfFrame();
            AnimateEvasion(0);

            Evade_Event(true, m_rollEnableLength, ref m_isRolling);
            yield return new WaitForSeconds(m_rollEnableLength);
            Evade_Event(false, m_rollEnableLength, ref m_isRolling);
        }

        private IEnumerator DodgeRoutine()
        {
            AnimateEvasion(1);
            yield return new WaitForEndOfFrame();
            AnimateEvasion(0);

            Evade_Event(true, m_dodgeEnableLength, ref m_isDodging);
            yield return new WaitForSeconds(m_dodgeEnableLength);
            Evade_Event(false, m_dodgeEnableLength, ref m_isDodging);
        }

        private void Evade_Event(bool evasion, float length, ref bool evadeType)
        {
            if (evadeType == evasion)
                return;

            evadeType = evasion;

            EvasionEvent?.Invoke(evasion, length);
        }

        private void AnimateEvasion(int evasionIndex)
        {
            m_evasionAnimator.SetInteger("EvasionIndex", evasionIndex);
            evasionIndex = 0;
        }
    }
}
