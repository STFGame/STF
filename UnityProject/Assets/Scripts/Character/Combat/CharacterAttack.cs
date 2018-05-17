using Actions;
using Boxes;
using Combat;
using Managers;
using Alerts;
using System;
using System.Collections.Generic;
using UnityEngine;
using Misc;

namespace Character
{
    /// <summary>
    /// Class that contains all of the attack info of the character.
    /// </summary>
    public class CharacterAttack : MonoBehaviour, IAttack
    {
        #region CharacterAttack Variables
        [Header("Attack Values")]
        //Array of all of the attacks that a character can perform
        [SerializeField] private AttackMove m_AttackMove = null;

        [Header("Freeze Values")]
        [SerializeField] private Act m_FreezeAction = null;

        //List of all of the hitboxes that the character has
        private List<Hitbox> m_Hitboxes = new List<Hitbox>();

        private Rigidbody m_Rigidbody;
        private Animator m_AttackAnimator;

        private float m_AnimSpeed = 0f;
        private bool m_Hit = false;

        public bool Hit { get { return m_Hit; } set { m_Hit = value; } }
        public float Damage { get; private set; }

        public int AttackID { get; set; }
        #endregion

        #region Load
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_AttackAnimator = GetComponent<Animator>();

            foreach (AttackBehaviour behaviour in m_AttackAnimator.GetBehaviours<AttackBehaviour>())
                behaviour.characterAttack = this;
        }

        private void Start()
        {
            LoadHitboxes();

            LoadAttacks();
        }

        //Fills the hitbox list with all of the hitboxes
        private void LoadHitboxes()
        {
            var boxAreas = Enum.GetValues(typeof(BoxArea));
            int count = 0;
            foreach (BoxArea boxArea in boxAreas)
            {
                Hitbox hitbox = (Hitbox)GetComponent<BoxManager>().GetBox(BoxType.Hitbox, boxArea);
                if (hitbox != null)
                {
                    m_Hitboxes.Add(hitbox);
                    m_Hitboxes[count].Enabled(false);
                    count++;
                }
            }
        }

        private void LoadAttacks()
        {
            m_AttackMove.Load();
        }
        #endregion

        #region Updates
        public void Attack(int actionID)
        {
            AttackAction attackAction = null;
            attackAction = m_AttackMove.GetAttack(AttackID);

            if (AttackID != 0)
                AttackID = 0;

            if (attackAction != null)
            {
                Damage = attackAction.damage.damage;
                Alert.Send<IAlert>(gameObject, (x, y) => x.Inform(AlertValue.Attacking));
            }

            HitFreeze();
        }

        private void HitFreeze()
        {
            if (m_AttackAnimator.speed != 0)
                m_AnimSpeed = m_AttackAnimator.speed;

            if (m_Hit)
            {
                float animSpeed = m_AttackAnimator.speed;
                m_FreezeAction.Perform(ref animSpeed, ref m_Hit);
                m_AttackAnimator.speed = animSpeed;

                m_Rigidbody.isKinematic = (m_Hit);
            }

            m_AttackAnimator.speed = (m_Hit) ? 0f : m_AnimSpeed;
        }

        public void AnimateAttack(int attackIndex)
        {
            m_AttackAnimator.SetInteger("Attack", attackIndex);
        }
        #endregion
    }
}