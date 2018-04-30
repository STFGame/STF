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
        [SerializeField] private Attack[] attacks;

        private int attackId = 0;
        public int AttackIndex { get; private set; }

        private void Start()
        {
            AttackIndex = 0;
            InitializeAttacks();

            GetComponentInChildren<ComboManager>().comboBehaviour.ComboEvent += UpdateAttackId;
        }

        private void OnDisable()
        {
            GetComponentInChildren<ComboManager>().comboBehaviour.ComboEvent -= UpdateAttackId;
        }

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
                    ;
                }
            }
        }

        private void UpdateAttackId(int attackId)
        {
            this.attackId = attackId;
        }

        private void OnDrawGizmos() { }
    }
}