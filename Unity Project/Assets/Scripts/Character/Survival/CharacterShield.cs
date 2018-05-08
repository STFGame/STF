﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character
{
    public class CharacterShield : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxShield = 100f;
        [SerializeField] private float hitDamping = 10f;
        [SerializeField] [Range(0f, 2f)] private float increaseRate = 1f;
        [SerializeField] [Range(0f, 2f)] private float decreaseRate = 1f;
        [SerializeField] private float stunLength = 2f;
        private float currentShield;

        private Animator animator;

        public bool Shielding { get; private set; }
        public bool ShieldStun { get; private set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();

            currentShield = maxShield;
        }

        public void TakeDamage(float damage)
        {
            currentShield -= damage;
        }

        #region Shield Methods
        public void Shield(bool shield)
        {
            Shielding = (!ShieldStun) ? shield : false;

            if (Shielding)
                Debug.Log(Shielding);

            if (Shielding)
                StartCoroutine(ShieldAction());

            if (currentShield <= 0)
                StartCoroutine(ShieldStunAction());
        }

        private IEnumerator ShieldStunAction()
        {
            ShieldStun = true;
            yield return new WaitForSeconds(stunLength);
            ShieldStun = false;

            currentShield = maxShield;
        }

        private IEnumerator ShieldAction()
        {
            while (currentShield > 0f)
            {
                currentShield -= Time.deltaTime * decreaseRate;

                if (!Shielding)
                    break;

                yield return null;
            }

            if (currentShield <= 0f)
                yield break;

            if (currentShield != maxShield && !ShieldStun)
            {
                while (currentShield < maxShield)
                {
                    currentShield += Time.deltaTime * increaseRate;

                    yield return null;
                }
            }
        }
        #endregion

        public void AnimateShield()
        {
            animator.SetBool("Shielding", Shielding);
            animator.SetBool("Stunned", ShieldStun);
        }
    }
}