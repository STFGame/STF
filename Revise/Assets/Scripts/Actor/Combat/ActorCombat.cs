using Actor.Animations;
using Actor.Bubbles;
using Combos;
using Controls;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public class ActorCombat : MonoBehaviour
    {
        [SerializeField] private Attack[] attacks = null;
        [SerializeField] private AttackAnimation attackAnimation = new AttackAnimation();

        private int attackId = 0;
        public int AttackIndex { get; private set; }

        #region Initialization
        private void Awake()
        {
            AttackIndex = 0;
            attackAnimation.Init(this);
        }

        private void Start()
        {
            InitializeAttacks();

            GetComponentInChildren<ComboManager>().comboBehaviour.ComboEvent += UpdateAttackId;
        }

        private void OnDisable()
        {
            GetComponentInChildren<ComboManager>().comboBehaviour.ComboEvent -= UpdateAttackId;
        }
        #endregion

        private void InitializeAttacks()
        {
            for (int i = 0; i < attacks.Length; i++)
            {
                for (int j = 0; j < attacks[i].attackData.Length; j++)
                {
                    GameObject gameObject = GetComponent<BubbleManager>().GetHitBubble(attacks[i].attackData[j].bodyArea);
                    attacks[i].attackData[j].Initiate(gameObject);
                }
                attacks[i].Init();
            }
        }

        public void PerformAttack()
        {
            for (int i = 0; i < attacks.Length; i++)
            {
                AttackIndex = attacks[i].PerformAttack(attackId);

                if (AttackIndex > 0)
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.right * 10f, ForceMode.VelocityChange);
                }
            }
        }

        public void PlayAttackAnimation()
        {
            attackAnimation.PlayAttackAnim(AttackIndex);
        }

        private void UpdateAttackId(int attackId)
        {
            this.attackId = attackId;
        }

        private void OnDrawGizmos() { }
    }
}