using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Actor
{
    /// <summary>
    /// ActorBlock is the blocking aspects of the Actor
    /// </summary>
    public class ActorBlock : MonoBehaviour
    {
        [SerializeField] private float shieldStrength;
        [SerializeField] private float shieldDuration;

        private float currentShield;
        private float stunDuration = 3.0f;

        private bool isStunned = false;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            currentShield = shieldStrength;
        }

        public void Block(bool block)
        {
            if (block && currentShield > 0f && !isStunned)
                currentShield -= shieldDuration;
            else if (currentShield != shieldStrength && currentShield > 0 && !isStunned)
                currentShield += shieldDuration;
            else if (currentShield <= 0f && !isStunned)
            {
                StopAllCoroutines();
                StartCoroutine(Stun());
            }

            if(!isStunned)
                animator.SetBool("IsBlocking", block);
            if(isStunned)
                animator.SetBool("IsBlocking", false);

            animator.SetBool("IsDisabled", isStunned);
        }

        private IEnumerator Stun()
        {
            isStunned = true;
            print("Stunned!");
            yield return new WaitForSeconds(stunDuration);
            print("Recovered!");
            currentShield = shieldStrength;
            isStunned = false;
        }
    }
}