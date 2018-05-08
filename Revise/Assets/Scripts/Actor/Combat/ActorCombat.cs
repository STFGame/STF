using Actor.Animations;
using Actor.Bubbles;
using Controls;
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

        public Vector3 AttackVelocity { get; private set; }
        public bool IsAttacking { get; set; }

        #region Initialization
        private void Awake()
        {
            AttackIndex = 0;
            attackAnimation.Init(this);
        }

        private void Start()
        {
            InitializeAttacks();

            GetComponentInChildren<ControlManager>().comboBehaviour.ComboEvent += UpdateAttackId;
        }

        private void OnDisable()
        {
            GetComponentInChildren<ControlManager>().comboBehaviour.ComboEvent -= UpdateAttackId;
        }

        private void InitializeAttacks()
        {
            for (int i = 0; i < attacks.Length; i++)
            {
                for (int j = 0; j < attacks[i].attackInfo.Length; j++)
                {
                    GameObject gameObject = null;
                    if (attacks[i].attackInfo[j].bubbleType == BubbleType.ThrowBubble)
                        gameObject = GetComponent<BubbleManager>().GetThrowBubbleGB(attacks[i].attackInfo[j].bodyArea);
                    else
                        gameObject = GetComponent<BubbleManager>().GetHitBubbleGB(attacks[i].attackInfo[j].bodyArea);

                    attacks[i].attackInfo[j].Initiate(gameObject);
                }
                attacks[i].Init();
            }
        }
        #endregion

        public void PerformAttack()
        {
            for (int i = 0; i < attacks.Length; i++)
            {
                AttackIndex = attacks[i].PerformAttack(attackId);

                if (AttackIndex > 0)
                {
                    GetComponent<Rigidbody>().AddForce(attacks[i].AttackVelocity(Vector3.zero), ForceMode.VelocityChange);
                    break;
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