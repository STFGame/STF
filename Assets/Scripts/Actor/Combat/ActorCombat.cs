using Actor.Bubbles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Actor
{
    public class ActorCombat : MonoBehaviour
    {
        [SerializeField] private Attack[] attacks;
        [SerializeField] private HitBubble[] hitBubbles;

        private Animator animator;

        // Use this for initialization
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Perform(bool block, bool attack)
        {
            AttackAnimation(attack);
        }

        private void AttackAnimation(bool attack)
        {
            if (attack)
                animator.SetInteger("Attack", 1);
            else
                animator.SetInteger("Attack", 0);

        }

        private void OnDrawGizmos()
        {
        }
    }
}