using Combat;
using System;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Class that contains all of the attack info of the character.
    /// </summary>
    public class Attack : MonoBehaviour
    {
        [Header("Attack Values")]
        [SerializeField] private AttackAction[] m_attackActions = null;

        public Action<bool> HitEvent;

        private Rigidbody m_rigidbody;
        private Animator m_attackAnimator;

        public float Damage { get; private set; }
        public int AttackID { get; set; }
        public bool IsAttacking { get; private set; }

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_attackAnimator = GetComponent<Animator>();

            foreach (AttackBehaviour behaviour in m_attackAnimator.GetBehaviours<AttackBehaviour>())
                behaviour.characterAttack = this;

            InitialiseAttacks();
        }

        private void InitialiseAttacks()
        {
            for (int i = 0; i < m_attackActions.Length; i++)
                m_attackActions[i].Initialise(this);
        }

        public void Execute(int attackID)
        {
            IsAttacking = false;
            for (int i = 0; i < m_attackActions.Length; i++)
            {
                m_attackActions[i].Execute(AttackID);

                if (m_attackActions[i].IsAttacking)
                    IsAttacking = true;
            }
            AttackID = 0;

            AnimateAttack(attackID);
        }

        public void Hit_Event(bool hit)
        {
            HitEvent?.Invoke(hit);
        }

        private void AnimateAttack(int attackIndex)
        {
            m_attackAnimator.SetInteger("Attack", attackIndex);
        }
    }
}