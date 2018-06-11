using Actions;
using Boxes;
using Characters;
using System;
using System.Collections;
using UnityEngine;

namespace Combat
{
    [Serializable]
    public class AttackAction
    {
        [SerializeField] private string m_tagID = "JabOne";
        [SerializeField] [Range(0f, 5f)] private float m_initialDelay = 0f;
        [SerializeField] [Range(0f, 5f)] private float m_activeLength = 0.5f;
        [SerializeField] private Hitbox m_hitbox = null;

        [SerializeField] private Vector2 m_attackDirection;
        [SerializeField] private float m_attackForce = 0f;
        [SerializeField] private float m_damage = 0f;

        private Attack m_attack;
        private Rigidbody m_rigidbody;

        public bool IsAttacking { get; private set; }
        public int AttackID { get; private set; }

        public void Initialise(Attack attack)
        {
            AttackID = Animator.StringToHash(m_tagID);

            m_attack = attack;
            m_rigidbody = m_attack.GetComponent<Rigidbody>();

            m_hitbox.Enabled(false);
        }

        public void Execute(int attackID)
        {
            if (AttackID == attackID)
            {
                m_attack.StopCoroutine(AttackRoutine());
                m_attack.StartCoroutine(AttackRoutine());
            }

            if (m_hitbox.Hit)
            {
                IsAttacking = false;
                m_hitbox.Enabled(false);
                return;
            }
        }

        private IEnumerator AttackRoutine()
        {
            IsAttacking = true;

            yield return new WaitForSeconds(m_initialDelay);

            if (!IsAttacking)
                yield break;

            Vector3 adjustedVelocity = Vector3.zero;
            adjustedVelocity.x = (m_attackDirection.x * m_rigidbody.transform.forward.x) + m_rigidbody.velocity.x;
            adjustedVelocity.y = (m_attackDirection.y) + m_rigidbody.velocity.y;

            m_rigidbody.AddForce(m_attackDirection * m_attackForce, ForceMode.Impulse);
            m_hitbox.Enabled(true);

            m_hitbox.Damage = m_damage;

            yield return new WaitForSeconds(m_activeLength);

            m_hitbox.Damage = 0;

            IsAttacking = false;

            m_hitbox.Enabled(false);
        }

    }
}