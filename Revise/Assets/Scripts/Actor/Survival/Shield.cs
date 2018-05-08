﻿using Actor.Movements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility;

namespace Actor.Survivability
{
    [Serializable]
    public sealed class Shield
    {
        [SerializeField] private float shieldStrength = 100f;
        [SerializeField] [Range(0f, 10f)] private float shieldDecreaseRate = 1f;
        [SerializeField] [Range(0f, 10f)] private float shieldIncreaseRate = 0.5f;
        [SerializeField] [Range(0f, 10f)] private float stunLength = 3f;

        private float stunTimer = 0f;
        private float currentShield;

        public bool isBlocking = false;
        public bool isStunned = false;

        public Shield()
        {
            currentShield = shieldStrength;
        }

        public void TakeDamage(float amount)
        {
            currentShield -= amount;

            isStunned = (currentShield <= 0f);
        }

        public void Block(bool block)
        {
            if (block && currentShield > 0f)
                currentShield -= shieldDecreaseRate;

            if (currentShield <= 0)
                isStunned = true;

            if (!isStunned && !block && currentShield < shieldStrength)
                currentShield += shieldIncreaseRate;

            if (isStunned)
                Stun();

            currentShield = Mathf.Clamp(currentShield, 0f, shieldStrength);

            isBlocking = (isStunned) ? false : block;
        }

        private void DebugShieldLog()
        {

        }

        private void Stun()
        {
            stunTimer += Time.deltaTime;

            if (stunTimer >= stunLength)
            {
                isStunned = false;
                stunTimer = 0f;
                currentShield = shieldStrength;
            }
        }
    }
}
