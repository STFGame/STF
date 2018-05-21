using Actions;
using Combat;
using System.Collections;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// Class that contains all of the attack info of the character.
    /// </summary>
    public class CharacterAttack : MonoBehaviour
    {
        [Header("Attack Values")]
        //Array of all of the attacks that a character can perform
        [SerializeField] private AttackMove m_AttackMove = null;

        private Rigidbody m_Rigidbody;
        private Animator m_AttackAnimator;

        private float m_AnimSpeed = 0f;

        public float Damage { get; private set; }
        public int AttackID { get; set; }

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_AttackAnimator = GetComponent<Animator>();

            foreach (AttackBehaviour behaviour in m_AttackAnimator.GetBehaviours<AttackBehaviour>())
                behaviour.characterAttack = this;

            InitialiseAttacks();
        }

        private void InitialiseAttacks()
        {
            m_AttackMove.Initialise();
        }

        public void Contact(bool hit, float damage)
        {
            StopCoroutine(ContactRoutine());
            StartCoroutine(ContactRoutine());
        }

        private IEnumerator ContactRoutine()
        {
            if (m_AttackAnimator.speed != 0f)
                m_AnimSpeed = m_AttackAnimator.speed;

            m_AttackAnimator.speed = 0f;
            m_Rigidbody.isKinematic = true;

            yield return new WaitForSeconds(0.2f);

            m_AttackAnimator.speed = m_AnimSpeed;
            m_Rigidbody.isKinematic = false;
        }

        public void Execute(int attackID)
        {
            AttackAction attackAction = null;
            attackAction = m_AttackMove.GetAttack(AttackID);

            if (AttackID != 0)
                AttackID = 0;

            if (attackAction != null)
                Damage = attackAction.damage.damage;

            AnimateAttack(attackID);
        }

        public void AnimateAttack(int attackIndex)
        {
            m_AttackAnimator.SetInteger("Attack", attackIndex);
        }
    }
}